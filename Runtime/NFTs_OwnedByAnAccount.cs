using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace NFTPort  
{ using Internal;
    
    /// <summary>
    /// NFTs owned by a given account (wallet address), Can also return each NFT metadata with include parameter and filter from specific collection.
    /// </summary>
    [AddComponentMenu(PortConstants.BaseComponentMenu+PortConstants.FeatureName_NFTs_OfAccount)]
    [ExecuteAlways]
    [HelpURL(PortConstants.NFTs_OfAccount)]
    public class NFTs_OwnedByAnAccount : MonoBehaviour
    {
        /// <summary>
        /// Currently Supported chains for this endpoint.
        /// </summary>
        public enum Chains
        {
            ethereum,
            polygon,
            goerli,
            solana,
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
            
            [FormerlySerializedAs("contract_address")]
            [Header("Optional: Filter by and return NFTs only from the given contract address/collection")]
           
            [SerializeField]
            [Tooltip("Filter from a collection, EVM only, Leave blank if not using")]
            [DrawIf("chain", Chains.solana , DrawIfAttribute.DisablingType.DontDrawInverse)]
            string collection;
            
            [Header("Include optional data in the response.")]
            [Tooltip("Default is the default response and metadata includes NFT metadata, like in Retrieve NFT details, and contract_information includes information of the NFT’s contract.")]
            [SerializeField]
            Includes include = Includes.metadata;

            [Tooltip("One API call might not be able to provide all NFTs in one go if user holds a lot of NFTs and not filtered, this string is passed in API call which can be used in next one to continue from last query")]
            public string continuation = "";
            
            private string RequestUriInit = "https://api.nftport.xyz/v0/accounts/";
            private string WEB_URL;
            private string _apiKey;
            private bool destroyAtEnd = false;


            private UnityAction<string> OnErrorAction;
            private UnityAction<NFTs_model> OnCompleteAction;
            
            [Space(20)]
            //[Header("Called After Successful API call")]
            public UnityEvent afterSuccess;
            //[Header("Called After Error API call")]
            public UnityEvent afterError;

            [Header("Run Component when this Game Object is Set Active")]
            [SerializeField] private bool onEnable = false;
            public bool debugErrorLog = true;
            public bool debugLogRawApiResponse = false;
            
            [Header("Gets filled with data and can be referenced:")]
            public NFTs_model NFTs;

        #endregion


        private void Awake()
        {
            PortUser.Initialise();
            _apiKey = PortUser.GetUserApiKey();
            
        }

        private void OnEnable()
        {
            if (onEnable & Application.isPlaying)
            {
                PortUser.SetFromOnEnable();
                Run();
            }
        }

        #region SetParams and Chain Functions

        /// <summary>
        /// Initialize creates a gameobject and assings this script as a component. This must be called if you are not refrencing the script any other way and it doesn't already exists in the scene.
        /// </summary>
        /// <param name="destroyAtEnd"> Optional bool parameter can set to false to avoid Spawned GameObject being destroyed after the Api process is complete. </param>
        public static NFTs_OwnedByAnAccount Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject("NFTs Of Account").AddComponent<NFTs_OwnedByAnAccount>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = false;
                _this.debugErrorLog = false;
                return _this;
            }
            
            /// <summary>
            /// Set Account Address to retrieve NFTs from as string
            /// </summary>
            /// <param name="account_address"> as string.</param>
            public NFTs_OwnedByAnAccount SetAddress(string account_address)
            {
                this.address = account_address;
                return this;
            }
            
            /// <summary>
            /// Blockchain from which to query NFTs.
            /// </summary>
            /// <param name="chain"> Choose from available 'Chains' enum</param>
            public NFTs_OwnedByAnAccount SetChain(Chains chain)
            {
                this.chain = chain;
                return this;
            }
            
            /// <summary>
            /// Include optional data in the response. default is the default response and metadata includes NFT metadata, like in Retrieve NFT details, and contract_information includes information of the NFT’s contract, Choose from Includes.
            /// </summary>
            /// <param name="include"> Choose from available 'Includes' enum </param>
            public NFTs_OwnedByAnAccount SetInclude(Includes include)
            {
                this.include = include;
                return this;
            }

            /// <summary>
            /// Set Filter by to return NFTs only from the given contract address/collection. 
            /// </summary>
            ///<param name="collection"> as string.</param>
            public NFTs_OwnedByAnAccount SetFilterFromCollection(string collection)
            {
                this.collection = collection;
                return this;
            }
            
            /// <summary>
            /// Set Continuation
            /// </summary>
            ///<param name="continuation"> as string.</param>
            public NFTs_OwnedByAnAccount SetContinuation(string continuation)
            {
                this.continuation = continuation;
                return this;
            }
            
            
            /// <summary>
            /// Action on succesfull API Fetch.
            /// </summary>
            /// <param name="NFTs_OwnedByAnAccount_model.Root"> Use: .OnComplete(NFTs=> NFTsOfUser = NFTs) , where NFTsOfUser = NFTs_model.Nfts[] ;</param>
            /// <returns> NFTs_model </returns>
            public NFTs_OwnedByAnAccount OnComplete(UnityAction<NFTs_model> action)
            {
                this.OnCompleteAction = action;
                return this;
            }
            
            /// <summary>
            /// Action on Error
            /// </summary>
            /// <param name="UnityAction action"> string.</param>
            /// <returns> Information on Error as string text.</returns>
            public NFTs_OwnedByAnAccount OnError(UnityAction<string> action)
            {
                this.OnErrorAction = action;
                return this;
            }
            
        #endregion

        
        #region Run - API
            /// <summary>
            /// Runs the Api call and fills the corresponding model in the component on success.
            /// </summary>
            public NFTs_model Run()
            {
                WEB_URL = BuildUrl();
                StopAllCoroutines();
                StartCoroutine(CallAPIProcess());
                return NFTs;
            }

            string BuildUrl()
            {
                if (chain == Chains.solana)
                {
                    WEB_URL = "https://api.nftport.xyz/v0/solana/accounts/" + address;
                    if (continuation != "")
                    {
                        WEB_URL += "?continuation=" + continuation + "?include=" + include;;
                    }
                    else
                    {
                        WEB_URL += "?include=" + include;
                    }
                }
                else
                {
                    WEB_URL = RequestUriInit + address + "?chain=" + chain.ToString().ToLower();
                    if (continuation != "")
                    {
                        WEB_URL = WEB_URL + "&continuation=" + continuation;
                    } 
                    WEB_URL = WEB_URL + "&include=" + include.ToString().ToLower();
             
                    if (collection != "")
                        WEB_URL = WEB_URL + "&contract_address=" + collection;
                }

                if (debugErrorLog)
                {
                    var s = "Querying NFTs owned of Account: " + address + " on " + chain;
                    if (collection != "")
                        s += " / Filter from collection: " + collection;
                    Debug.Log(s);

                }
                
                return WEB_URL;
            }
            
            IEnumerator CallAPIProcess()
            {
                //Make request
                UnityWebRequest request = UnityWebRequest.Get(WEB_URL);
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", _apiKey);
                request.SetRequestHeader("source", PortUser.GetSource());

                {
                    yield return request.SendWebRequest();
                    string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
                    
                    if(debugLogRawApiResponse)
                        Debug.Log(jsonResult);

                    if (request.error != null)
                    {
                        NFTs = null;
                        if(OnErrorAction!=null)
                            OnErrorAction($"Null data. Response code: {request.responseCode}. Result {jsonResult}");
                        if(debugErrorLog)
                            Debug.Log($" (⊙.◎) Null data. Response code: {request.responseCode}. Result {jsonResult}");
                        if(afterError!=null)
                            afterError.Invoke();
                        //yield break;
                    }
                    else
                    {
                        //Fill Data Model from recieved class
                        NFTs = JsonConvert.DeserializeObject<NFTs_model>(
                            jsonResult,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                MissingMemberHandling = MissingMemberHandling.Ignore,
                                Error = (sender, error) => error.ErrorContext.Handled = true
                            });
                        
                        if(OnCompleteAction!=null)
                            OnCompleteAction.Invoke(NFTs);
                        
                        if(afterSuccess!=null)
                            afterSuccess.Invoke();
                        
                        if(debugErrorLog)
                            Debug.Log($" ´ ▽ ` )ﾉ Success , view NFTs model" );
                    }
                }
                request.Dispose();
                if(destroyAtEnd)
                    Destroy (this.gameObject);
            }
            
        #endregion
    }

}
