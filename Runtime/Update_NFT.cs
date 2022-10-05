using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace NFTPort
{   using Internal;
    
   
    /// <summary>
    ///  Update  NFT minted via customizable minting.
    /// </summary>
    ///
    [AddComponentMenu(PortConstants.BaseComponentMenu+PortConstants.FeatureName_Update_NFT)]
    [ExecuteAlways]
    [HelpURL(PortConstants.Docs_Update_NFT)]
    public class Update_NFT : MonoBehaviour
    {
        public enum Chains
        {
            polygon,
            goerli,
            ethereum
        }
        
        private class CustomNFT
        {
            public string chain;
            public string contract_address;
            public string metadata_uri;
            public string token_id;
            public bool freeze_metadata = false;
        }


        #region Parameter Defines
               
            [SerializeField]
            private Chains _chain = Chains.polygon;
            
            [SerializeField] private string _contract_address = "Enter previously deployed contract address using deploy feature";
            [SerializeField] [Tooltip("Token ID of NFT to update.")] private string _token_id = "0";
            [SerializeField] private string _metadata_uri = "Enter new Metadata URI, can be obtained from metadata or file upload feature";
            [Tooltip("If true, freezes the specified NFT token URI and further token metadata updates are blocked. You can still change the base_uri on contract level with Update a deployed contract for NFT products. If you wish to freeze all updates, then set freeze_metadata as true in Update a deployed product contract.")]
            [SerializeField] private bool _freeze_metadata = false;

            [Space(20)]
            //[Header("Called When API call starts")]
            public UnityEvent OnRequestStarted;
            //[Header("Called After Successful API call")]
            public UnityEvent afterSuccess;
            //[Header("Called After Error API call")]
            public UnityEvent afterError;
            
            [Header("Run Component when this Game Object is Set Active")]
            [SerializeField] private bool onEnable = false;
            public bool debugErrorLog = true;
            public bool debugLogRawApiResponse = false;

            [Header("Response after successful mint:")]
            public Minted_model minted;
            
            private UnityAction<string> OnErrorAction;
            private UnityAction<Minted_model> OnCompleteAction;
            
            private string RequestUriInit = "https://api.nftport.xyz/v0/mints/customizable";
            private string WEB_URL;
            private string _apiKey;
            private bool destroyAtEnd = false;
            
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
        
        /// <summary>
        /// Initialize creates a gameobject and assings this script as a component. This must be called if you are not refrencing the script any other way and it doesn't already exists in the scene.
        /// </summary>
        /// <param name="destroyAtEnd"> Optional bool parameter can set to false to avoid Spawned GameObject being destroyed after the Api process is complete. </param>
        public static Update_NFT Initialize(bool destroyAtEnd = true)
        {
            var _this = new GameObject(PortConstants.FeatureName_Update_NFT).AddComponent<Update_NFT>();
            _this.destroyAtEnd = destroyAtEnd;
            _this.onEnable = false;
            _this.debugErrorLog = false;
            return _this;
        }
        
        /// <summary>
        /// Set NFT Parameters ≧◔◡◔≦ .
        /// </summary>
        /// <param name="contract_address"> Previously deployed contract address of this user.</param>
        /// <param name="metadata_uri"> New Metadata URI obtained from metadata or file upload feature </param>
        /// <param name="token_id"> Int Token ID for the NFT</param>
        /// <param name="freeze_metadata"> bool , If true, freezes the specified NFT token URI and further token metadata updates are blocked. </param>
        
        public Update_NFT SetParameters(string contract_address = null, string metadata_uri = null , string token_id = null, bool freeze_metadata = false)
        {
            if(contract_address!=null)
                _contract_address = contract_address;
            if(metadata_uri!=null)
                _metadata_uri = metadata_uri;
            if(token_id!=null)
                _token_id = token_id;
            _freeze_metadata = freeze_metadata;
            return this;
        }
        
        /// <summary>
        /// Set Blockchain
        /// </summary>
        /// <param name="chain"> Choose from available 'Chains' enum</param>
        public Update_NFT SetChain(Chains chain)
        {
            this._chain = chain;
            return this;
        }
        
        /// <summary>
        /// Action on successful API Fetch.
        /// </summary>
        /// <param name="Minted_model"> Use: .OnComplete(model=> Model = model) , where Model = Minted_model;</param>
        /// <returns> Minted_model </returns>
        public Update_NFT OnComplete(UnityAction<Minted_model> action)
        {
            this.OnCompleteAction = action;
            return this;
        }
            
        /// <summary>
        /// Action on Error(⊙.◎)
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> Information on Error as string text.</returns>
        public Update_NFT OnError(UnityAction<string> action)
        {
            this.OnErrorAction = action;
            return this;
        }

        /// <summary>
        /// Run (ɔ◔‿◔)ɔ
        /// </summary>
        public Minted_model Run()
        {
            WEB_URL = BuildUrl();
            StopAllCoroutines();
            StartCoroutine(CallAPIProcess(CreateProductNFT()));
            return minted;
        }
        
        CustomNFT CreateProductNFT()
        {
            var nft = new CustomNFT();
            nft.chain = _chain.ToString().ToLower();
            nft.contract_address = _contract_address;
            nft.metadata_uri = _metadata_uri;
            nft.token_id = _token_id;
            nft.freeze_metadata = _freeze_metadata;
            return nft;
        }

        string BuildUrl()
        {
            WEB_URL = RequestUriInit;
            
            return WEB_URL;
        }
        
        
        IEnumerator CallAPIProcess(CustomNFT nft)
        {
            
            if(debugErrorLog)
                Debug.Log("Update NFT ⊂(▀¯▀⊂) | " + _contract_address + " tokenID: " + _token_id + "  on chain: " + _chain );

            string json = JsonConvert.SerializeObject(
                nft, 
                new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
             if(debugErrorLog)
                Debug.Log(json);
             
             byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
             var request = new UnityWebRequest(WEB_URL, "PUT");
            
            request.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
                
            //headers
            request.SetRequestHeader("Content-Type",  "application/json");
            request.SetRequestHeader("source", PortUser.GetSource());
            request.SetRequestHeader("Authorization", _apiKey);

            //Make request
            if(OnRequestStarted!=null)
                OnRequestStarted.Invoke();
            yield return request.SendWebRequest();
            string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
            
            
            if(debugLogRawApiResponse)
                Debug.Log(jsonResult);

            if (request.error != null)
            {
                if(OnErrorAction!=null)
                    OnErrorAction($"Null data. Response code: {request.responseCode}. Result {jsonResult}");
                if(debugErrorLog)
                    Debug.Log($"(⊙.◎) Null data. Response code: {request.responseCode}. Result {jsonResult}");
                if(afterError!=null)
                    afterError.Invoke();
            }
            else
            {
                //Fill Data Model from received class
                minted = JsonConvert.DeserializeObject<Minted_model>(jsonResult);
                        
                if(OnCompleteAction!=null)
                    OnCompleteAction.Invoke(minted);
                        
                if(afterSuccess!=null)
                    afterSuccess.Invoke();

                if(debugErrorLog)
                    Debug.Log($"NFTPort | NFT Update Success (⌐■_■) : at: {minted.transaction_external_url}" );
            }

            request.Dispose();
            if (destroyAtEnd)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
