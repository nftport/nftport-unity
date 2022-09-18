using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace NFTPort  
{ using Internal;
    
    /// <summary>
    /// Get Transactions of an NFT
    /// </summary>
    [AddComponentMenu(PortConstants.BaseComponentMenu+PortConstants.FeatureName_Txn_NFT)]
    [ExecuteAlways]
    [HelpURL(PortConstants.Docs_Txns_NFT)]
    public class Txn_NFT : MonoBehaviour
    {
        /// <summary>
        /// Currently Supported chains for this endpoint.
        /// </summary>
        public enum Chains
        {
            ethereum,
            solana
        }
        
        public enum Type
        {
            all, mint, burn, transfer_from, transfer_to, list, buy, sell, make_bid , get_bid
        }

        #region Parameter Defines

        [SerializeField] private Chains chain;
            
            [FormerlySerializedAs("_contract_address")]
            [SerializeField]
            [DrawIf("chain", Chains.solana , DrawIfAttribute.DisablingType.DontDrawInverse)]
            private string _collection = "Input Contract/Collection Address of the NFT collection";
                
            [SerializeField]
            [DrawIf("chain", Chains.solana , DrawIfAttribute.DisablingType.DontDrawInverse)]
            [Tooltip("Token ID of the NFT")]
            private string _token_id = "0";
                
            [DrawIf("chain", Chains.solana , DrawIfAttribute.DisablingType.DontDraw)]
            [SerializeField]
            [Tooltip(" Mint Address of the NFT on Solana")]
            private string _mint_address = "Input Mint Address of the NFT";
        

            [SerializeField] private Type _type = Type.all;

            private string RequestUriInit = "https://api.nftport.xyz/v0/transactions/nfts/";
            private string WEB_URL;
            private string _apiKey;
            private bool destroyAtEnd = false;


            private UnityAction<string> OnErrorAction;
            private UnityAction<Txn_model> OnCompleteAction;
            
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
            public Txn_model txnModel;

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
        public static Txn_NFT Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject(PortConstants.FeatureName_Txn_NFT).AddComponent<Txn_NFT>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = false;
                _this.debugErrorLog = false;
                return _this;
            }

        /// <summary>
        /// Set Parameters to retrieve NFT From.  ≧◔◡◔≦ .
        /// </summary>
        /// <param name="collection"> as string - EVM</param>
        /// <param name="token_id"> as int - EVM.</param>
        /// <param name="mint_address"> mint_address - Solana.</param>
        /// <param name="type"> as Type{ all, mint, burn, transfer_from, transfer_to, list, buy, sell, make_bid , get_bid}.</param>
        public Txn_NFT SetParameters(string collection = null, string token_id = "-1", string mint_address = null, Type type = Type.all)
        {
            if(collection!=null)
                this._collection = collection;
            if (token_id != "-1")
                _token_id = token_id;
            if (mint_address != null)
                _mint_address = mint_address;
            if (_type != type)
                _type = type;
            return this;
        }

        /// <summary>
        /// Blockchain from which to query NFTs.
        /// </summary>
        /// <param name="chain"> Choose from available 'Chains' enum</param>
        public Txn_NFT SetChain(Chains chain)
        {
            this.chain = chain;
            return this;
        }

        /// <summary>
        /// Action on successful API Fetch. (*^∇^)ヾ(￣▽￣*)
        /// </summary>
        /// <param name="Txn_model"> Use: .OnComplete(Txns=> txns = Txns) , where txns is of type Txn_model;</param>
        /// <returns> NFTs_OwnedByAnAccount_model.Root </returns>
        public Txn_NFT OnComplete(UnityAction<Txn_model> action)
        {
            this.OnCompleteAction = action;
            return this;
        }
        
        /// <summary>
        /// Action on Error (;•͈́༚•͈̀)(•͈́༚•͈̀;)՞༘՞༘՞
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> Information on Error as string text.</returns>
        public Txn_NFT OnError(UnityAction<string> action)
        {
            this.OnErrorAction = action;
            return this;
        }
            
        #endregion

        
        #region Run - API
            /// <summary>
            /// Runs the Api call and fills the corresponding model in the component on success.
            /// </summary>
            public Txn_model Run()
            {
                WEB_URL = BuildUrl();
                StopAllCoroutines();
                StartCoroutine(CallAPIProcess());
                return txnModel;
            }

            string BuildUrl()
            {
                if (chain == Chains.solana)
                {
                    WEB_URL = "https://api.nftport.xyz/v0/solana/transactions/nft/" + _mint_address + "?type=" + _type.ToString();
                    
                    if(debugErrorLog)
                        Debug.Log("Querying Transactions of NFT: " + _mint_address + " on " + chain);
                }
                else
                {
                    WEB_URL = RequestUriInit + _collection + "/" + _token_id.ToString() + "?chain=" + chain.ToString().ToLower() + "&type=" + _type.ToString();
                    
                    if(debugErrorLog)
                        Debug.Log("Querying Transactions of NFT: " + _collection + "  token ID  " + _token_id + " on " + chain);
                }
                return WEB_URL;
            }
            
            IEnumerator CallAPIProcess()
            {
                //Make request
                UnityWebRequest request = UnityWebRequest.Get(WEB_URL);
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("source", PortUser.GetSource());
                request.SetRequestHeader("Authorization", _apiKey);
                


                {
                    yield return request.SendWebRequest();
                    string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
                    
                    if(debugLogRawApiResponse)
                        Debug.Log(jsonResult);

                    if (request.error != null)
                    {
                        if(OnErrorAction!=null)
                            OnErrorAction($"Null data. Response code: {request.responseCode}. Result {jsonResult}");
                        if(debugErrorLog)
                            Debug.Log($"(;•͈́༚•͈̀)(•͈́༚•͈̀;)՞༘՞༘՞ Null data. Response code: {request.responseCode}. Result {jsonResult}");
                        if(afterError!=null)
                            afterError.Invoke();

                        txnModel = null;
                        //yield break;
                    }
                    else
                    {
                        //Fill Data Model from recieved class
                        txnModel = JsonConvert.DeserializeObject<Txn_model>(
                            jsonResult,
                            new JsonSerializerSettings
                            {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                            });
                        
                        if(OnCompleteAction!=null)
                            OnCompleteAction.Invoke(txnModel);
                        
                        if(afterSuccess!=null)
                            afterSuccess.Invoke();
                        
                        if(debugErrorLog)
                            Debug.Log($"(*^∇^)ヾ(￣▽￣*) Success , view Txns model" );
                    }
                }
                request.Dispose();
                if(destroyAtEnd)
                    Destroy (this.gameObject);
            }
            
        #endregion
    }

}
