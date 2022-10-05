using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace NFTPort
{
    using Internal;
    using Utils;

    /// <summary>
    /// Easy minting via File Upload, Create and mint any file to any wallet as NFT using file upload at any moment in the game. Usefull to make anything into NFT including game objects, audio, textures, files, unity prefabs, procedural objects and more.
    /// If you wish to customize the minting process e.g. use your own contract, set more metadata, see Customizable minting.
    /// </summary>
    [AddComponentMenu(PortConstants.BaseComponentMenu + PortConstants.FeatureName_Mint_File)]
    [ExecuteAlways]
    [HelpURL(PortConstants.Docs_Mint_File)]
    public class Mint_File : MonoBehaviour
    {
        public enum Chains
        {
            polygon,
            goerli
        }

        #region Parameter Defines

        [ReadOnly] public float uploadProgress = 0;
        [ReadOnly] public float uplodedBytes = 0;

        [Space(20)] [SerializeField] private Chains _chain = Chains.polygon;

        [SerializeField] private string _filePath = "Enter FilePath of the file to mint";
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

        [Header("Run Component when this Game Object is Set Active")] [SerializeField]
        private bool onEnable = false;

        public bool debugErrorLog = true;
        public bool debugLogRawApiResponse = false;

        [Header("Response after successful mint:")]
        public Minted_model minted;

        private UnityAction<string> OnStartedAction;
        private UnityAction<int> OnProgressAction;
        private UnityAction<string> OnErrorAction;
        private UnityAction<Minted_model> OnCompleteAction;

        private string RequestUriInit = "https://api.nftport.xyz/v0/mints/easy/files";
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
        public static Mint_File Initialize(bool destroyAtEnd = true)
        {
            var _this = new GameObject(PortConstants.FeatureName_Mint_File).AddComponent<Mint_File>();
            _this.destroyAtEnd = destroyAtEnd;
            _this.onEnable = false;
            _this.debugErrorLog = false;
            return _this;
        }

        /// <summary>
        /// Set Easy Mint NFT Parameters
        /// </summary>
        /// <param name="FilePath"> URL of the file to mint.</param>
        /// <param name="Name"> Name of the NFT.</param>
        /// <param name="Description"> Description of the NFT. </param>
        /// <param name="MintToAddress"> Blockchain address to mint to. </param>
        public Mint_File SetParameters(string FilePath = null, string Name = null, string Description = null,
            string MintToAddress = null)
        {
            if (Name != null)
                _name = Name;
            if (Description != null)
                _description = Description;
            if (FilePath != null)
                _filePath = FilePath;
            if (MintToAddress != null)
                _mintToAddress = MintToAddress;
            return this;
        }

        /// <summary>
        /// Blockchain from which to query NFTs.
        /// </summary>
        /// <param name="chain"> Choose from available 'Chains' enum</param>
        public Mint_File SetChain(Chains chain)
        {
            this._chain = chain;
            return this;
        }

        /// <summary>
        /// Action on File Upload Start ≧◔◡◔≦ .
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> string Sterted .</returns>
        public Mint_File OnStarted(UnityAction<string> action)
        {
            this.OnStartedAction = action;
            return this;
        }

        /// <summary>
        /// Action on File Upload Progress returning Progress percentage
        /// </summary>
        /// <returns> float uploadProgress .</returns>
        public Mint_File OnProgress(UnityAction<int> action)
        {
            this.OnProgressAction = action;
            return this;
        }

        /// <summary>
        /// Action on succesfull API Fetch.
        /// </summary>
        /// <param name="Minted_model"> Use: .OnComplete(model=> Model = model) , where Model = Minted_model;</param>
        /// <returns> Minted_model </returns>
        public Mint_File OnComplete(UnityAction<Minted_model> action)
        {
            this.OnCompleteAction = action;
            return this;
        }

        /// <summary>
        /// Action on Error
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> Information on Error as string text.</returns>
        public Mint_File OnError(UnityAction<string> action)
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
            FileLocate(_filePath);
            return minted;
        }

        public void Stop(bool destroy)
        {
            StopAllCoroutines();
            Debug.Log("File Upload Stopped");
            End();
        }

        void FileLocate(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                if (OnErrorAction != null)
                    OnErrorAction("(~_^) ERROR! Can't locate the file to upload: " + path);
                if (debugErrorLog)
                    Debug.Log("(~_^) ERROR! Can't locate the file to upload: " + path);
                End();
            }
            else
            {
                StartCoroutine((CallAPIProcess(path)));
            }
        }

        private string RequestURL;

        string BuildUrl()
        {
            //https: //api.nftport.xyz/v0/mints/easy/files?chain=polygon&name=s&description=sss&mint_to_address=s
            RequestURL = "?chain=" + _chain.ToString().ToLower() + "&name=" + _name + "&description=" + _description +
                         "&mint_to_address=" + _mintToAddress;
            WEB_URL = RequestUriInit + RequestURL;
            return WEB_URL;
        }

        private UnityWebRequest request;

        IEnumerator CallAPIProcess(string filePath)
        {
            if (debugErrorLog)
                Debug.Log("Mint  ⊂(▀¯▀⊂)   |  FILE");


            if (debugErrorLog)
                Debug.Log(RequestURL);

            var _FormParams = FormMaker.Formeet(filePath);
            request = new UnityWebRequest(WEB_URL, "POST");
            UploadHandler uploader = new UploadHandlerRaw(_FormParams.body);
            uploader.contentType = _FormParams.contentType;
            request.uploadHandler = uploader;
            request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            request.SetRequestHeader("source", PortUser.GetSource());
            request.SetRequestHeader("Authorization", _apiKey);
            request.SendWebRequest();


            if (OnStartedAction != null)
                OnStartedAction.Invoke("≧◔◡◔≦ Upload Started");
            if (OnRequestStarted != null)
                OnRequestStarted.Invoke();
            if (debugErrorLog)
                Debug.Log("≧◔◡◔≦ Upload Started");

            uploadProgress = 0;
            uplodedBytes = 0;

            bool uploaded = false;
            var lastprogress = uploadProgress;
            while (!request.isDone)
            {
                uploadProgress = request.uploadProgress * 100f;
                uplodedBytes = request.uploadedBytes;

                if ((int) lastprogress != (int) uploadProgress)
                {
                    var uploadPerc = (int) (request.uploadProgress * 100f);
                    if (OnProgressAction != null)
                        OnProgressAction(uploadPerc);
                    if (debugErrorLog)
                    {
                        if (!uploaded)
                        {
                            if (uploadPerc < 100)
                                Debug.Log("(ɔ◔‿◔)ɔ Uploading file. Progress " + uploadPerc + "%");
                            else if (uploadPerc == 100)
                            {
                                Debug.Log("≧◉◡◉≦ File Uploaded 100%, Awaiting API");
                                uploaded = true;
                            }
                        }
                    }

                    lastprogress = uploadProgress;
                }

                yield return null;
            }

            string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);

            if (debugLogRawApiResponse)
                Debug.Log(jsonResult);

            if (request.error != null)
            {
                if (OnErrorAction != null)
                    OnErrorAction($"Null data. Response code: {request.responseCode}. Result {jsonResult}");
                if (debugErrorLog)
                    Debug.Log($"(⊙.◎) Null data. Response code: {request.responseCode}. Result {jsonResult}");
                if (afterError != null)
                    afterError.Invoke();
            }
            else
            {
                //Fill Data Model from received class
                minted = JsonConvert.DeserializeObject<Minted_model>(jsonResult);

                if (OnCompleteAction != null)
                    OnCompleteAction.Invoke(minted);

                if (afterSuccess != null)
                    afterSuccess.Invoke();

                if (debugErrorLog)
                    Debug.Log($"NFTPort | Mint Success (⌐■_■) : at: {minted.transaction_external_url}");
            }

            End();

        }

        void End()
        {
            if (request != null)
                request.Dispose();
            StopAllCoroutines();
            if (destroyAtEnd)
            {
                if (Application.isEditor)
                    DestroyImmediate(gameObject);
                else
                    Destroy(this.gameObject);
            }
        }
    }
}