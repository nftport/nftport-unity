using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace NFTPort  
{
    /// <summary>
    /// Returns NFTs owned by a given account (i.e. wallet) address. Can also return each NFT metadata with include parameter and filter from specific collection.
    /// </summary>
    public class NFTs_OwnedByAnAccount : MonoBehaviour
    {
        /// <summary>
        /// Currently Supported chains for this endpoint.
        /// </summary>
        public enum Chains
        {
            ethereum,
            polygon,
            rinkeby
        }
        
        public enum Includes
        {
            Default,
            metadata, 
            contract_information
        }
        
        #region Parameter Defines

            [SerializeField]
            private Chains chain = Chains.ethereum;
            
            [SerializeField]
            private string address = "Input Account Address To Fetch NFT's from";
            
            [Header("Filter by and return NFTs only from the given contract address/collection")]
            
            [Header("Optional:")]
           
            [SerializeField]
            [Tooltip("Leave blank if not using")]
            string contract_address;
            
            [Header("Include optional data in the response.")]
            [Tooltip("Default is the default response and metadata includes NFT metadata, like in Retrieve NFT details, and contract_information includes information of the NFT’s contract.")]
            [SerializeField]
            Includes include = Includes.Default;

            
            private string RequestUriInit = "https://api.nftport.xyz/v0/accounts/";
            private string WEB_URL;
            private  string _apiKey;
            private bool destroyAtEnd = false;


            private UnityAction<string> OnErrorAction;
            private UnityAction OnCompleteAction;


            [Space(10)]
            //Event Called after success, actions can be set within editor. 
            public UnityEvent afterSuccess;

            //Gets filled with data and can be refrenced >
            public NFTs_OwnedByAnAccount_model.Root ownedbyAddressModel;

        #endregion


        private void Awake()
        {
            Port.Initialise();
            _apiKey = Port.GetUserApiKey();
            
        }
        

        #region SetParams and Chain Functions

            /// <summary>
            /// Initialize creates a gameobject and assings this script as a component. This must be called if you are not refrencing the script any other way and it doesn't already exists in the scene.
            /// Optional bool parameter destroyAtEnd can be passed here and set to false to avoid Spawned GameObject being destroyed after the Api process is complete.
            /// </summary>
            public static NFTs_OwnedByAnAccount Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject("NFTs Of Account").AddComponent<NFTs_OwnedByAnAccount>();
                _this.destroyAtEnd = destroyAtEnd;
                return _this;
            }
            
            /// <summary>
            /// Set Account Address to retrieve NFTs from as string
            /// </summary>
            public NFTs_OwnedByAnAccount SetAddress(string account_address)
            {
                this.address = account_address;
                return this;
            }
            
            /// <summary>
            /// Blockchain from which to query NFTs. Choose from Chains.
            /// </summary>
            public NFTs_OwnedByAnAccount SetChain(Chains chain)
            {
                this.chain = chain;
                return this;
            }
            
            /// <summary>
            /// Include optional data in the response. default is the default response and metadata includes NFT metadata, like in Retrieve NFT details, and contract_information includes information of the NFT’s contract, Choose from Includes.
            /// </summary>
            public NFTs_OwnedByAnAccount SetInclude(Includes include)
            {
                this.include = include;
                return this;
            }

            /// <summary>
            /// Filter by and return NFTs only from the given contract address/collection. Add as string.
            /// </summary>
            public NFTs_OwnedByAnAccount SetFilterFromContract(string contract_address)
            {
                this.contract_address = contract_address;
                return this;
            }
            
            public NFTs_OwnedByAnAccount OnComplete(UnityAction action)
            {
                this.OnCompleteAction = action;
                return this;
            }
            
            public NFTs_OwnedByAnAccount OnError(UnityAction<string> action)
            {
                this.OnErrorAction = action;
                return this;
            }
            
        #endregion

        
        #region Run - API
            /// <summary>
            /// Runs the Api call and returns the corresponding model on success.
            /// </summary>
            public NFTs_OwnedByAnAccount_model.Root Run()
            {
                WEB_URL = BuildUrl();
                StartCoroutine(CallAPIProcess());
                return ownedbyAddressModel;
            }

            string BuildUrl()
            {
                WEB_URL = RequestUriInit  + address + "?chain=" + chain.ToString().ToLower() + "&include=" + include.ToString().ToLower();
                if (contract_address != "")
                    WEB_URL = WEB_URL + "&contract_address=" + contract_address;
                
                return WEB_URL;
            }
            
            IEnumerator CallAPIProcess()
            {
                //Make request
                UnityWebRequest rq = UnityWebRequest.Get(WEB_URL);
                rq.SetRequestHeader("Content-Type", "application/json");
                rq.SetRequestHeader("Authorization", _apiKey);

                {
                    yield return rq.SendWebRequest();
                    string jsonResult = System.Text.Encoding.UTF8.GetString(rq.downloadHandler.data);
                    
                    Debug.Log(rq.responseCode);
                    Debug.Log(jsonResult);
                    
                    //Fill Data Model from recieved class
                    ownedbyAddressModel = JsonConvert.DeserializeObject<NFTs_OwnedByAnAccount_model.Root>(jsonResult);
                    
                    if (ownedbyAddressModel == null)
                    {
                        if(OnErrorAction!=null)
                            OnErrorAction($"Null data. Response code: {rq.responseCode}.");
                        yield break;
                    }

                    if(ownedbyAddressModel != null)
                    {
                        if(OnCompleteAction!=null)
                            OnCompleteAction.Invoke();
                        
                        if(afterSuccess!=null)
                            afterSuccess.Invoke();
                        
                        yield return ownedbyAddressModel;
                    }

                }
                
                rq.Dispose();
                if(destroyAtEnd)
                    Destroy (this.gameObject);
            }
            
        #endregion
    }

}
