using System.Collections;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace NFTPort
{
    using Internal;
    using Utils;
    [AddComponentMenu(PortConstants.BaseComponentMenu+PortConstants.FeatureName_StorageMetadata)]
    [ExecuteAlways]
    [HelpURL(PortConstants.Docs_StorageMetadata)]
    public class Storage_UploadMetadata : MonoBehaviour
    {
        #region Parameter Defines

        public Storage_MetadataToUpload_model metadata;
        
        [Space(20)]
        [ReadOnly]public float uploadProgress = 0;
        [ReadOnly]public float uplodedBytes = 0;
        
        [Space(20)] [Header("Use Save file as Json function to save it to defined path")]
        public string saveToPath = "Assets/NFTPort/";
        public string fileName = "metadata.json";

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
        public bool debugLogRawJson = false;
        public bool debugLogRawApiResponse = false;

        [Header("Response after successful upload:")]
        public Storage_model.Storage storageModel;

        private UnityAction<string> OnStartedAction;
        private UnityAction<int> OnProgressAction;
        private UnityAction<string> OnErrorAction;
        private UnityAction<Storage_model.Storage> OnCompleteAction;
        private bool destroyAtEnd = false;

        private string RequestUriInit = "https://api.nftport.xyz/v0/metadata";
        private string WEB_URL;
        private string _apiKey;

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
        public static Storage_UploadMetadata Initialize(bool destroyAtEnd = true)
        {
            var _this = new GameObject("Storage_FileUpload").AddComponent<Storage_UploadMetadata>();
            _this.destroyAtEnd = destroyAtEnd;
            _this.onEnable = false;
            _this.debugErrorLog = false;
            return _this;
        }
        
        /// <summary>
        /// Set MetaData properties, This will override any values set in metadata in the editor ≧◔◡◔≦ .
        /// </summary>
        /// <param name="MetadataToUpload"> metadata.</param>
        public Storage_UploadMetadata SetMetadata(Storage_MetadataToUpload_model _metadata)
        {
            metadata = _metadata;
            return this;
        }

        /// <summary>
        /// Action on File Upload Start ≧◔◡◔≦ .
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> string Sterted .</returns>
        public Storage_UploadMetadata OnStarted(UnityAction<string> action)
        {
            this.OnStartedAction = action;
            return this;
        }
        
        /// <summary>
        /// Action on File Upload Progress returning Progress percentage
        /// </summary>
        /// <returns> float uploadProgress .</returns>
        public Storage_UploadMetadata OnProgress(UnityAction<int> action)
        {
            this.OnProgressAction = action;
            return this;
        }

        /// <summary>
        /// Action on succesfull Upload ≧◉◡◉≦.
        /// </summary>
        /// <param name="Storage_model.Storage"> Use: .OnComplete(_storageModel=> response = _storageModel) , where _storageModel = Storage_model.Storage;</param>
        /// <returns> Storage_model</returns>
        public Storage_UploadMetadata OnComplete(UnityAction<Storage_model.Storage> action)
        {
            this.OnCompleteAction = action;
            return this;
        }

        /// <summary>
        /// Action on Error (⊙.◎)
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> Information on Error as string text.</returns>
        public Storage_UploadMetadata OnError(UnityAction<string> action)
        {
            this.OnErrorAction = action;
            return this;
        }

        /// <summary>
        /// Runs the API (ɔ◔‿◔)ɔ
        /// </summary>
        public void Run()
        {
            WEB_URL = BuildUrl();
            StopAllCoroutines();
            StartCoroutine(CallAPIProcess());
        }
        
        /// <summary>
        /// Stop Any In progress calls
        /// </summary>
        public void Stop(bool destroy)
        {
            StopAllCoroutines();
            if(request!=null)
                request.Dispose();
            if (destroy)
            {
                if (Application.isEditor)
                    DestroyImmediate(gameObject);
                else
                    Destroy(this.gameObject);
            }
            Debug.Log("File Upload Stopped");
        }

        /// <summary>
        /// Save File as Json locally.
        /// </summary>
        /// <param name="saveToPath"> Path to save to as string.</param>
        /// <param name="fileName"> FileName as string</param>
        public void SaveFile(string saveToPath, string fileName)
        {
            string json = JsonConvert.SerializeObject(
                ProcessMetadataToUpload.Process(metadata), 
                    new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    });
                System.IO.File.WriteAllText(saveToPath + fileName, json);
#if UNITY_EDITOR
                AssetDatabase.Refresh();
#endif
            if(System.IO.File.Exists(saveToPath+fileName)){
                Debug.Log($"File Saved to: " + saveToPath + fileName);
            }
            else
            {
                Debug.Log($"Path Not Found: " + saveToPath);
            }
            
        }

        string BuildUrl()
        {
            WEB_URL = RequestUriInit;
            return WEB_URL;
        }

        private UnityWebRequest request;
        IEnumerator CallAPIProcess()
        {
            //var _FormParams = FormMaker.Formeet(filePath);

            var processedMetadata = ProcessMetadataToUpload.Process(metadata);
            
            string json = JsonConvert.SerializeObject(
                processedMetadata, 
                new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
            if (debugLogRawJson)
                Debug.Log(json);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

            request = new UnityWebRequest(WEB_URL, "POST");
            UploadHandler uploader = new UploadHandlerRaw(jsonToSend);
            //uploader.contentType = _FormParams.contentType;
            request.uploadHandler = uploader;
            request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type",  "application/json");
            request.SetRequestHeader("source", PortUser.GetSource());
            request.SetRequestHeader("Authorization", _apiKey);
            request.SendWebRequest();

            uploadProgress = 0;
            uplodedBytes = 0;
            
            if(OnStartedAction!=null)
                OnStartedAction.Invoke("≧◔◡◔≦ Upload Started");
            if(OnRequestStarted!=null)
                OnRequestStarted.Invoke();
            if(debugErrorLog)
                Debug.Log("≧◔◡◔≦ Upload Started");

            bool uploaded = false;
            var lastprogress = uploadProgress;
            while (!request.isDone)
            {
                uploadProgress = request.uploadProgress * 100f;
                uplodedBytes = request.uploadedBytes;

                if ((int)lastprogress != (int)uploadProgress)
                {
                    var uploadPerc = (int) (request.uploadProgress * 100f);
                    if (OnProgressAction != null)
                        OnProgressAction(uploadPerc);
                    if (debugErrorLog)
                    {
                        if (!uploaded)
                        {
                            if(uploadPerc<100)
                                Debug.Log("(ɔ◔‿◔)ɔ Uploading file. Progress " + uploadPerc + "%");
                            else if(uploadPerc==100)
                            {
                                Debug.Log("≧◉◡◉≦ File Uploaded 100%, Awaiting API" );
                                uploaded = true;
                            }
                        }
                    }
                    lastprogress = uploadProgress;
                }
                yield return null;
            }

            string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
            
            if(debugLogRawApiResponse)
                Debug.Log(jsonResult);

            if (request.error != null)
            {
                if (OnErrorAction != null)
                    OnErrorAction($"( ≖.≖) Response code: {request.responseCode}. Result {jsonResult}");
                if (debugErrorLog)
                    Debug.Log($"( ≖.≖) Response code: {request.responseCode}. Result {jsonResult}");
                if (afterError!=null)
                    afterError.Invoke();
            }
            else
            {
                //Fill Data Model from received class
                storageModel = JsonConvert.DeserializeObject<Storage_model.Storage>(jsonResult);

                if (OnCompleteAction != null)
                    OnCompleteAction.Invoke(storageModel);
                if (afterSuccess != null)
                    afterSuccess.Invoke();
                if (debugErrorLog)
                    Debug.Log("(̶◉͛‿◉̶) Upload Done : " + "Metadata at :" + storageModel.metadata_uri);
            }

            request.Dispose();
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

