using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace NFTPort
{   using Internal;
    
   
    /// <summary>
    /// Customizable minting. Mints an NFT to your previously contract for NFT products.
    /// </summary>
    ///
    [AddComponentMenu(PortConstants.BaseComponentMenu+PortConstants.FeatureName_Deploy)]
    [ExecuteAlways]
    [HelpURL(PortConstants.Docs_DeployContract)]
    public class Deploy : MonoBehaviour
    {
        public enum Chains
        {
            polygon,
            rinkeby
        }
        
        public enum Type
        {
            erc721,
            erc1155
        }
        
        private class Data
        {
            public string chain;
            public string name;
            public string symbol;
            public string owner_address;
            public bool metadata_updatable;
            public string type = Type.erc721.ToString();
            [System.ComponentModel.DefaultValue(0)] 
            public int royalties_share = 0;
            
            [System.ComponentModel.DefaultValue(null)] 
            public string royalties_address = null;

        }
      

        #region Parameter Defines
               
            [SerializeField]
            private Chains _chain = Chains.polygon;
            
            [SerializeField] private string _name = "Name of your product contract";
            [SerializeField] private string _symbol = "Symbol for your Contract";
            [SerializeField] private string _owner_address = "Enter Blockchain address to set owner.";
            [Header("Set this bool to true if you want to create dynamic, updatable NFTs")]
            [SerializeField] private bool _metadata_updatable = true;

            [SerializeField] private Type _type;
            
            [SerializeField][Tooltip("Secondary market royalty rate in basis points (100 bps = 1%). This value cannot exceed 10,000 bps.")] 
            private int _royalties_share = 0;

            [SerializeField][Tooltip("Address for royalties. Defaults to owner_address if not set.")] 
            private string _royalties_address;
            

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
            
            private string RequestUriInit = "https://api.nftport.xyz/v0/contracts";
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
                Run();
        }
        
        /// <summary>
        /// Initialize creates a gameobject and assings this script as a component. This must be called if you are not refrencing the script any other way and it doesn't already exists in the scene.
        /// </summary>
        /// <param name="destroyAtEnd"> Optional bool parameter can set to false to avoid Spawned GameObject being destroyed after the Api process is complete. </param>
        public static Deploy Initialize(bool destroyAtEnd = true)
        {
            var _this = new GameObject(PortConstants.FeatureName_Deploy).AddComponent<Deploy>();
            _this.destroyAtEnd = destroyAtEnd;
            _this.onEnable = false;
            _this.debugErrorLog = false;
            return _this;
        }
        
        /// <summary>
        /// Set  Mint NFT Parameters ≧◔◡◔≦ .
        /// </summary>
        /// <param name="name"> Name of the collection.</param>
        /// <param name="symbol"> Symbol for the contract </param>
        /// <param name="owner_address"> Owner of the contract. </param>
        /// <param name="metadata_updatable"> Set this to true if you want to create dynamic NFTs</param>
        /// <param name="type"> Choose betwwen erc721 vs erc1155</param>
        /// <param name="royalties_share"> Secondary market royalty rate in basis points (100 bps = 1%). This value cannot exceed 10,000 bps.</param>
        /// <param name="royalties_address"> Address for royalties. Defaults to owner_address if not set</param>
        public Deploy SetParameters(string name = null, string symbol = null , string owner_address = null , bool metadata_updatable = true, Type type = Type.erc721, int royalties_share = 0, string royalties_address = null)
        {
            if(name!=null)
                _name = name;
            if(symbol!=null)
                _symbol = symbol;
            if(owner_address!=null)
                _owner_address = owner_address;
            if(metadata_updatable!=_metadata_updatable)
                _metadata_updatable = metadata_updatable;
            if (type != _type)
                _type = type;
            if (royalties_share != 0)
                _royalties_share = royalties_share;
            if (royalties_address != null)
                _royalties_address = royalties_address;
            return this;
        }
        
        /// <summary>
        /// Blockchain to deploy Contract on.
        /// </summary>
        /// <param name="chain"> Choose from available 'Chains' enum</param>
        public Deploy SetChain(Chains chain)
        {
            this._chain = chain;
            return this;
        }
        
        /// <summary>
        /// Action on success.
        /// </summary>
        /// <param name="Minted_model"> Use: .OnComplete(model=> Model = model) , where Model = Minted_model;</param>
        /// <returns> Minted_model </returns>
        public Deploy OnComplete(UnityAction<Minted_model> action)
        {
            this.OnCompleteAction = action;
            return this;
        }
            
        /// <summary>
        /// Action on Error(⊙.◎)
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> Information on Error as string text.</returns>
        public Deploy OnError(UnityAction<string> action)
        {
            this.OnErrorAction = action;
            return this;
        }

        /// <summary>
        /// Deploys the Contract (ɔ◔‿◔)ɔ
        /// </summary>
        public Minted_model Run()
        {
            WEB_URL = BuildUrl();
            StopAllCoroutines();
            StartCoroutine(CallAPIProcess(CreateDataClass()));
            return minted;
        }

        Data CreateDataClass()
        {
            var data = new Data();
            data.chain = _chain.ToString().ToLower();
            data.name = _name;
            data.symbol = _symbol;
            data.owner_address = _owner_address;
            data.metadata_updatable = _metadata_updatable;
            data.type = _type.ToString();
            data.royalties_share = _royalties_share;
            data.royalties_address = _royalties_address;

            return data;
        }

        string BuildUrl()
        {
            WEB_URL = RequestUriInit;
            return WEB_URL;
        }
        
        
        IEnumerator CallAPIProcess(Data data)
        {
             if(debugErrorLog)
                 Debug.Log("Deploying Contract σ(＊▼‐▼＊) ");
             
             string json = JsonConvert.SerializeObject(
                data, 
                new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
             if(debugErrorLog)
                Debug.Log(json);
             
             byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
             var request = new UnityWebRequest(WEB_URL, "POST");
            
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
                    Debug.Log($"Deploy Product Contract Success ヾ(*▼・▼)ﾉ⌒☆ : at: {minted.transaction_external_url}" );
            }

            request.Dispose();
            if (destroyAtEnd)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
