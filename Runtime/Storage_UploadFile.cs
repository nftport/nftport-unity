using System.Collections;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace NFTPort
{
    using Internal;
    using Utils;
    
    [AddComponentMenu(PortConstants.BaseComponentMenu+PortConstants.FeatureName_StorageFiles)]
    [ExecuteAlways]
    [HelpURL(PortConstants.Docs_StorageFile)]
    public class Storage_UploadFile : MonoBehaviour
    {
        #region Parameter Defines

        public string filePath;
        [ReadOnly]public float uploadProgress = 0;
        [ReadOnly]public float uplodedBytes = 0;

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

        [Header("Response after successful upload:")]
        public Storage_model.Storage storageModel;

        private UnityAction<string> OnStartedAction;
        private UnityAction<int> OnProgressAction;
        private UnityAction<string> OnErrorAction;
        private UnityAction<Storage_model.Storage> OnCompleteAction;
        private bool destroyAtEnd = false;

        private string RequestUriInit = "https://api.nftport.xyz/v0/files";
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
        public static Storage_UploadFile Initialize(bool destroyAtEnd = true)
        {
            var _this = new GameObject("Storage_FileUpload").AddComponent<Storage_UploadFile>();
            _this.destroyAtEnd = destroyAtEnd;
            _this.onEnable = false;
            _this.debugErrorLog = false;
            return _this;
        }

        public Storage_UploadFile SetFilePath(string _filePath)
        {
            filePath = _filePath;
            return this;
        }
        
        /// <summary>
        /// Action on File Upload Start ≧◔◡◔≦ .
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> string Sterted .</returns>
        public Storage_UploadFile OnStarted(UnityAction<string> action)
        {
            this.OnStartedAction = action;
            return this;
        }
        
        /// <summary>
        /// Action on File Upload Progress returining Progress percentage
        /// </summary>
        /// <returns> float uploadProgress .</returns>
        public Storage_UploadFile OnProgress(UnityAction<int> action)
        {
            this.OnProgressAction = action;
            return this;
        }

        /// <summary>
        /// Action on succesfull Upload ≧◉◡◉≦.
        /// </summary>
        /// <param name="Storage_model.Storage"> Use: .OnComplete(_storageModel=> response = _storageModel) , where _storageModel = Storage_model.Storage;</param>
        /// <returns> Storage_model</returns>
        public Storage_UploadFile OnComplete(UnityAction<Storage_model.Storage> action)
        {
            this.OnCompleteAction = action;
            return this;
        }

        /// <summary>
        /// Action on Error (⊙.◎)
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> Information on Error as string text.</returns>
        public Storage_UploadFile OnError(UnityAction<string> action)
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
            FileLocate(filePath);
        }
        
        public void Stop(bool destroy = false)
        {
            destroy = destroyAtEnd;
            StopAllCoroutines();
            if(debugErrorLog)
                Debug.Log("File Upload Stopped");
            End();
        }

        string BuildUrl()
        {
            WEB_URL = RequestUriInit;
            return WEB_URL;
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

        private UnityWebRequest request;
        IEnumerator CallAPIProcess(string filePath)
        {
            var _FormParams = FormMaker.Formeet(filePath);
            request = new UnityWebRequest(WEB_URL, "POST");
            UploadHandler uploader = new UploadHandlerRaw(_FormParams.body);
            uploader.contentType = _FormParams.contentType;
            request.uploadHandler = uploader;
            request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
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
            
            if (debugLogRawApiResponse)
                Debug.Log(jsonResult);

            if (request.error != null)
            {
                if (OnErrorAction != null)
                    OnErrorAction($"( ≖.≖) Response code: {request.responseCode}. Result {jsonResult}");
                if (debugErrorLog)
                    Debug.Log($"( ≖.≖) Response code: {request.responseCode}. Result {jsonResult}");
                if(afterError!=null)
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
                    Debug.Log("(̶◉͛‿◉̶) Upload Done" + " at :" + storageModel.ipfs_url);
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

