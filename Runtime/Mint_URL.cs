using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace NFTPort
{   using Internal;
    
    /// <summary>
    /// Easy minting w/URL, If you wish to customize the minting process e.g. use your own contract, set more metadata, see Customizable minting.
    /// </summary>
    [AddComponentMenu(PortConstants.BaseComponentMenu + PortConstants.FeatureName_Mint_URL)]
    [ExecuteAlways]
    [HelpURL(PortConstants.Docs_Mint_URL)]
    public class Mint_URL : MonoBehaviour
    {
        public enum Chains
        {
            polygon,
            goerli
        }
        
        private class EasyMintNFT
        {
            public string chain;
            public string name;
            public string description;
            public string file_url;
            public string mint_to_address;
        }
        
        #region Parameter Defines
               
            [SerializeField]
            private Chains _chain = Chains.polygon;
            
            [SerializeField] private string _fileURL = "Enter URL of the file to mint";
            [SerializeField] private string _name = "Enter Name of the NFT";
            [SerializeField] private string _description = "Enter Description of the NFT.";
            [SerializeField] private string _mintToAddress = "Enter Blockchain address to mint to.";

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
            
            private string RequestUriInit = "https://api.nftport.xyz/v0/mints/easy/urls";
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
        public static Mint_URL Initialize(bool destroyAtEnd = true)
        {
            var _this = new GameObject("Easy Mint w/ URL").AddComponent<Mint_URL>();
            _this.destroyAtEnd = destroyAtEnd;
            _this.onEnable = false;
            _this.debugErrorLog = false;
            return _this;
        }
        
        /// <summary>
        /// Set Easy Mint NFT Parameters
        /// </summary>
        /// <param name="FileURL"> URL of the file to mint.</param>
        /// <param name="Name"> Name of the NFT.</param>
        /// <param name="Description"> Description of the NFT. </param>
        /// <param name="MintToAddress"> Blockchain address to mint to. </param>
        public Mint_URL SetParameters(string FileURL = null, string Name = null, string Description = null, string MintToAddress = null)
        {
            if(Name!=null)
                _name = Name;
            if(Description!=null)
                _description = Description;
            if(FileURL!=null)
                _fileURL = FileURL;
            if(MintToAddress!=null)
                _mintToAddress = MintToAddress;
            return this;
        }
        
        /// <summary>
        /// Blockchain from which to query NFTs.
        /// </summary>
        /// <param name="chain"> Choose from available 'Chains' enum</param>
        public Mint_URL SetChain(Chains chain)
        {
            this._chain = chain;
            return this;
        }
        
        /// <summary>
        /// Action on succesfull API Fetch.
        /// </summary>
        /// <param name="Minted_model"> Use: .OnComplete(model=> Model = model) , where Model = Minted_model;</param>
        /// <returns> Minted_model </returns>
        public Mint_URL OnComplete(UnityAction<Minted_model> action)
        {
            this.OnCompleteAction = action;
            return this;
        }
            
        /// <summary>
        /// Action on Error
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> Information on Error as string text.</returns>
        public Mint_URL OnError(UnityAction<string> action)
        {
            this.OnErrorAction = action;
            return this;
        }

        /// <summary>
        /// Runs the Mint ^_^
        /// </summary>
        public Minted_model Run()
        {
            WEB_URL = BuildUrl();
            StopAllCoroutines();
            StartCoroutine(CallAPIProcess(CreateEasyNFT()));
            return minted;
        }

        EasyMintNFT CreateEasyNFT()
        {
            var nft = new EasyMintNFT();
            nft.chain = _chain.ToString().ToLower();
            nft.name = _name;
            nft.description = _description;
            nft.file_url = _fileURL;
            nft.mint_to_address = _mintToAddress;
            return nft;
        }
        
        string BuildUrl()
        {
            WEB_URL = RequestUriInit;
            return WEB_URL;
        }
        
        
        IEnumerator CallAPIProcess(EasyMintNFT nft)
        {
            if(debugErrorLog)
                Debug.Log("Mint Started ⊂(▀¯▀⊂)   |  URL");
            
            string json = JsonUtility.ToJson(nft);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            
            if(debugErrorLog)
                Debug.Log(json);
            
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
                    Debug.Log($"NFTPort | Mint Success (⌐■_■) : at: {minted.transaction_external_url}" );
            }

            request.Dispose();
            if (destroyAtEnd)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
