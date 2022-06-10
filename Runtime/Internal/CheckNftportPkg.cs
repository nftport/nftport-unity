#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Events;

namespace NFTPort.Editor
{
    public static class CheckNftportPkg 
    {
        /// ///////// CHECK
        static ListRequest listRequest;
        
        public static void CheckPkgsList()
        {
            listRequest = Client.List();    // List packages installed for the Project
            EditorApplication.update += CheckPackageProgress;
        }
        
        private static UnityAction<string> OnListCheckCompleteActionPackageExists;
        public static void OnListCheckComplete(UnityAction<string> action)
        {
            OnListCheckCompleteActionPackageExists = action;
          
        }

        static void CheckPackageProgress()
        {
            var pkgexists = false;
            if (listRequest.IsCompleted)
            {
                if (listRequest.Status == StatusCode.Success)
                    foreach (var package in listRequest.Result)
                    {
                        if (package.name.Contains("nftport"))
                        {
                            pkgexists = true;
                        }
                    }
                else if (listRequest.Status >= StatusCode.Failure)
                    OnListCheckCompleteActionPackageExists.Invoke(StatusCode.Failure.ToString());
                
                if(pkgexists)
                    OnListCheckCompleteActionPackageExists.Invoke(true.ToString());
                else
                {
                    OnListCheckCompleteActionPackageExists.Invoke(false.ToString());
                }

                EndListCheck();
            }
        }
        static void EndListCheck()
        {
            EditorApplication.update -= CheckPackageProgress;
        }
    }

}

#endif