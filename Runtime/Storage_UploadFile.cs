using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NFTPort
{
    using Internal;

    public class Storage_UploadFile : MonoBehaviour
    {

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
        public Storage_model storageModel;

        private UnityAction<string> OnErrorAction;
        private UnityAction<Storage_model> OnCompleteAction;
        private bool destroyAtEnd = false;
        private static IPFS_UploadFile_Internal ipfsUploadFileInternal;
        string error;

        /// <summary>
        /// Initialize creates a gameobject and assings this script as a component. This must be called if you are not refrencing the script any other way and it doesn't already exists in the scene.
        /// </summary>
        /// <param name="destroyAtEnd"> Optional bool parameter can set to false to avoid Spawned GameObject being destroyed after the Api process is complete. </param>
        public static Storage_UploadFile Initialize(bool destroyAtEnd = true)
        {
            var _gameobject = new GameObject("Storage_UploadFile");

            ipfsUploadFileInternal
                .InitializeGameObject(_gameobject, false, false);

            var _this = _gameobject.AddComponent<Storage_UploadFile>();
            _this.destroyAtEnd = destroyAtEnd;
            _this.onEnable = false;
            _this.debugErrorLog = false;
            return _this;
        }

        /// <summary>
        /// Action on succesfull API Fetch.
        /// </summary>
        /// <param name="Storage_model"> Use: .OnComplete(_storageModel=> response = _storageModel) , where _storageModel = Storage_model;</param>
        /// <returns> Storage_model</returns>
        public Storage_UploadFile OnComplete(UnityAction<Storage_model> action)
        {
            this.OnCompleteAction = action;
            return this;
        }

        /// <summary>
        /// Action on Error
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> Information on Error as string text.</returns>
        public Storage_UploadFile OnError(UnityAction<string> action)
        {
            this.OnErrorAction = action;
            return this;
        }

        /// <summary>
        /// Runs the API ^_^
        /// </summary>
        public void Run()
        {
            if (ipfsUploadFileInternal == null)
            {
                ipfsUploadFileInternal
                    .InitializeGameObject(this.gameObject, debugErrorLog, debugLogRawApiResponse);
            }
            ipfsUploadFileInternal
                .SetFilePatth("F:/(0)/Screenshot 2022-05-14 044703.jpg")
                .OnComplete(_storageModel => InternalCompleted(_storageModel))
                .OnError(_error => InternalCompletedwithError(_error))
                .Run();
        }

        void InternalCompletedwithError(string _error)
        {
            OnErrorAction.Invoke(_error);
            
            if(afterError!=null)
                afterError.Invoke();
            
            if(destroyAtEnd)
                Destroy(this.gameObject);
        }

        void InternalCompleted(Storage_model _storageModel)
        {
            OnCompleteAction.Invoke(_storageModel);
            
            if(afterSuccess!=null)
                 afterSuccess.Invoke();
            
            if(destroyAtEnd)
                Destroy(this.gameObject);
        }
    }
}

