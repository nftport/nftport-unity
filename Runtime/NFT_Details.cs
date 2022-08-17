using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace NFTPort  
{ using Internal;
    
    /// <summary>
    /// Details of particular NFT
    /// </summary>
    [AddComponentMenu(PortConstants.BaseComponentMenu+PortConstants.FeatureName_NFT_Details)]
    [ExecuteAlways]
    [HelpURL(PortConstants.Docs_NFTDetails)]
    public class NFT_Details : MonoBehaviour
    {
        /// <summary>
        /// Currently Supported chains for this endpoint.
        /// </summary>
        public enum Chains
        {
            ethereum,
            polygon,
            rinkeby,
            solana
        }

        #region Parameter Defines

            [SerializeField]
            private Chains chain = Chains.ethereum;
            
            [SerializeField]
            [DrawIf("chain", Chains.solana , DrawIfAttribute.DisablingType.DontDrawInverse)]
            private string _contract_address = "Input Contract Address of the NFT collection";
            
            [SerializeField]
            [DrawIf("chain", Chains.solana , DrawIfAttribute.DisablingType.DontDrawInverse)]
            [Tooltip("Token ID of the NFT")]
            private int _token_id = 0;
            
            [DrawIf("chain", Chains.solana , DrawIfAttribute.DisablingType.DontDraw)]
            [SerializeField]
            [Tooltip(" Mint Address of the NFT on Solana")]
            private string _mint_address = "Input Mint Address of the NFT";

            [SerializeField]
            [Tooltip("Queues and refreshes the metadata of the token if it has changed since the updated_date. Useful for example, when NFT collections are revealed or upgraded")]
            private bool _refresh_metadata = true;

            private string RequestUriInit = "https://api.nftport.xyz/v0/nfts/";
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
        public static NFT_Details Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject(PortConstants.FeatureName_NFT_Details).AddComponent<NFT_Details>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = false;
                _this.debugErrorLog = false;
                return _this;
            }

        /// <summary>
        /// Set Parameters to retrieve NFT From.  ≧◔◡◔≦ .
        /// </summary>
        /// <param name="contract_address"> as string - EVM</param>
        /// <param name="token_id"> as int - EVM.</param>
        /// <param name="mint_address"> mint_address - Solana.</param>
        /// <param name="refresh_metadata"> Queues and refreshes the metadata of the token if it has changed since the updated_date. Useful for example, when NFT collections are revealed or upgraded</param>
        public NFT_Details SetParameters(string contract_address = null, int token_id = -1, string mint_address = null, bool refresh_metadata = false)
            {
                if(contract_address!=null)
                    this._contract_address = contract_address;
                if (token_id != -1)
                    _token_id = token_id;
                if (mint_address != null)
                    _mint_address = mint_address;
                if (refresh_metadata != _refresh_metadata)
                    _refresh_metadata = refresh_metadata;
                return this;
            }
            
            /// <summary>
            /// Blockchain from which to query NFTs.
            /// </summary>
            /// <param name="chain"> Choose from available 'Chains' enum</param>
            public NFT_Details SetChain(Chains chain)
            {
                this.chain = chain;
                return this;
            }

            /// <summary>
            /// Action on successful API Fetch. (*^∇^)ヾ(￣▽￣*)
            /// </summary>
            /// <param name="NFTs_OwnedByAnAccount_model.Root"> Use: .OnComplete(NFTs=> NFTsOfUser = NFTs) , where NFTsOfUser = NFTs_OwnedByAnAccount_model.Root;</param>
            /// <returns> NFTs_OwnedByAnAccount_model.Root </returns>
            public NFT_Details OnComplete(UnityAction<NFTs_model> action)
            {
                this.OnCompleteAction = action;
                return this;
            }
            
            /// <summary>
            /// Action on Error (⊙.◎)
            /// </summary>
            /// <param name="UnityAction action"> string.</param>
            /// <returns> Information on Error as string text.</returns>
            public NFT_Details OnError(UnityAction<string> action)
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
                    WEB_URL = "https://api.nftport.xyz/v0/solana/nft/" + _mint_address;
                }
                else
                {
                    WEB_URL = RequestUriInit + _contract_address + "/" + _token_id.ToString() + "?chain=" + chain.ToString().ToLower() + "&refresh_metadata=" + _refresh_metadata.ToString();
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
                        NFTs = null;
                        if(OnErrorAction!=null)
                            OnErrorAction($"Null data. Response code: {request.responseCode}. Result {jsonResult}");
                        if(debugErrorLog)
                            Debug.Log($"(⊙.◎) Null data. Response code: {request.responseCode}. Result {jsonResult}");
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
                            MissingMemberHandling = MissingMemberHandling.Ignore
                            });
                        
                        if(OnCompleteAction!=null)
                            OnCompleteAction.Invoke(NFTs);
                        
                        if(afterSuccess!=null)
                            afterSuccess.Invoke();
                        
                        if(debugErrorLog)
                            Debug.Log($" ´ ▽ ` )ﾉ Success , view NFT under NFTs model" );
                    }
                }
                request.Dispose();
                if(destroyAtEnd)
                    Destroy (this.gameObject);
            }
            
        #endregion
    }

}
