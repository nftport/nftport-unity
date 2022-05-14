using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace NFTPort.Internal
{ using Utils;
    
    [ExecuteInEditMode] 
    public class IPFS_UploadFile_Internal : MonoBehaviour 
    {
    
        #region Parameter Defines

        public bool debugErrorLog = false;
        public bool debugLogRawApiResponse = false;

        private UnityAction<string> OnErrorAction;
        private UnityAction<Storage_model> OnCompleteAction;
        
        private string RequestUriInit = "https://api.nftport.xyz/v0/files";
        private string WEB_URL;
        private string _apiKey;

        private Storage_model _storageModel;

        public string filepath;

        #endregion
 
        
        private void Awake()
        {
            PortUser.Initialise();
            _apiKey = PortUser.GetUserApiKey();
            
        }

        private GameObject _gameObject;

        public IPFS_UploadFile_Internal InitializeGameObject(GameObject _gameObject, bool _debugErrorLog, bool _debugLogRawApiResponse)
        {
            var _this = _gameObject.AddComponent<IPFS_UploadFile_Internal>();
            _this.debugErrorLog = _debugErrorLog;
            _this.debugLogRawApiResponse = _debugLogRawApiResponse;
            return _this;
        }
        
        public IPFS_UploadFile_Internal OnComplete(UnityAction<Storage_model> action)
        {
            this.OnCompleteAction = action;
            return this;
        }
        
        public IPFS_UploadFile_Internal OnError(UnityAction<string> action)
        {
            this.OnErrorAction = action;
            return this;
        }
        public IPFS_UploadFile_Internal SetFilePatth(string _filePath)
        {
            filepath = _filePath;
            return this;
        }

        
        public void Run()
        {
            WEB_URL = BuildUrl();
            StopAllCoroutines();
            FileLocate(filepath);
        }
        
        
        string BuildUrl()
        {
            WEB_URL = RequestUriInit;
            return WEB_URL;
        }

        void FileLocate(string path)
        {
            //Path.Combine(Application.persistentDataPath + "/TicketInformation/", "Pic001.png"))
            if (!File.Exists(path))
            {
                Debug.Log("ERROR! Can't locate the file to upload: " + path);
            }
            else
            {
                Debug.Log(Application.persistentDataPath);
                StartCoroutine((CallAPIProcess(path)));
            }
        }

        public float progress = 0;
        public float uplodedBytes;
        IEnumerator CallAPIProcess(string filePath)
        { 
            string contentType = DetermineFileType.File(filePath);
           Debug.Log(contentType);

           //create multipart foorm
           var _FormParams = FormMaker.Formeet(filePath);
           
           UnityWebRequest request = new UnityWebRequest(WEB_URL, "POST");
           UploadHandler uploader = new UploadHandlerRaw(_FormParams.body);
           uploader.contentType = _FormParams.contentType;
           request.uploadHandler = uploader;
           request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
           
           request.SetRequestHeader("source", "NFTPort-Unity");
           request.SetRequestHeader("Authorization", _apiKey);
           request.SendWebRequest();
           
           while (!request.isDone)
           {
               progress = request.uploadProgress * 100f;
               uplodedBytes = request.uploadedBytes;
               if(debugErrorLog) 
                   Debug.Log("Uploading file. Progress " + (int)(request.uploadProgress * 100f) + "%"); // <-----------------
               yield return null;
           }
           string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
                
                
            if(debugLogRawApiResponse)
                    Debug.Log(jsonResult);

            if (request.error != null)
            {
                if(OnErrorAction!=null)
                    OnErrorAction($"Null data. Response code: {request.responseCode}. Result {jsonResult}");
                if(debugErrorLog)
                    Debug.Log($"Null data. Response code: {request.responseCode}. Result {jsonResult}");
            }
            else
            {
                //Fill Data Model from received class
                _storageModel = JsonConvert.DeserializeObject<Storage_model>(jsonResult);
                        
                if(OnCompleteAction!=null)
                    OnCompleteAction.Invoke(_storageModel);
                        
              //  if(afterSuccess!=null)
              //      afterSuccess.Invoke();
            }

            request.Dispose();
            Destroy (this);
        }
    }
}

