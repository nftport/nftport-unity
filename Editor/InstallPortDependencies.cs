using System;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

namespace NFTPort.Internal {
    public class InstallPortDependencies : EditorWindow
    {
        static AddRequest Request;
        private static RemoveRequest RmRequest;
        private static bool newtonsoftInstalled = false;
        private static bool GLTFInstalled = false;    

        [MenuItem("NFTPort/Install Dependencies")]
        public static void ShowWindow()
        {
            var win = GetWindow<InstallPortDependencies>("Port Dependencies");
            SetSize(win);
        }
        void OnGUI()
        {
            GUILayout.Label("\n" +
                            " NFTPort needs the following packages : \n" +
                            "", EditorStyles.label);
            
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("com.unity.nuget.newtonsoft-json");
            var defaultcol = GUI.contentColor;

            if (newtonsoftInstalled)
            {
                GUI.contentColor = Color.green;
                GUILayout.Label(": installed");
                GUI.contentColor = defaultcol;
            }
            else
            {
                GUILayout.Label(": Not Installed in UPM");
            }
            
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Install Now", GUILayout.Height(25)))
                Add();
            
            //if (GUILayout.Button("Remove Dependency", GUILayout.Height(25)))
             //   Remove();
            GUILayout.EndHorizontal();
            
            GUILayout.Label("");
            
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("GLTF-Utility | Used by NFTPort Playground Sample");
            if (GLTFInstalled)
            {
                GUI.contentColor = Color.green;
                GUILayout.Label(": installed");
                GUI.contentColor = defaultcol;
            }
            else
            {
                GUILayout.Label(": Not Installed in UPM");
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Install Now", GUILayout.Height(25)))
                AddGLTF();
            
            if (GUILayout.Button("Remove Dependency", GUILayout.Height(25)))
               RemoveGLTF();
            GUILayout.EndHorizontal();
        }
        
        static void SetSize(InstallPortDependencies win) 
        {
            win.minSize = new Vector2(330*2, 120*2);
            win.maxSize =  new Vector2(330*2, 120*2);
        }

        private void OnEnable()
        {
         
            CheckPkgsListForNewtonsoft();
            CheckPkgsListForGLTFt();
        }

        #region newtonsoft
       /// ///////// CHECK
        static ListRequest listRequest;
        public static void CheckPkgsListForNewtonsoft()
        {
            listRequest = Client.List();    // List packages installed for the Project
            EditorApplication.update += CheckPackageProgress;
        }
        
        private static UnityAction<bool> OnListCheckCompleteActionNewtonPackageExists;
        public static void OnListCheckCompleteForNewtonSoft(UnityAction<bool> action)
        {
            OnListCheckCompleteActionNewtonPackageExists = action;
          
        }

        static void CheckPackageProgress()
        {
            if (listRequest.IsCompleted)
            {
                if (listRequest.Status == StatusCode.Success)
                    foreach (var package in listRequest.Result)
                    {
                        newtonsoftInstalled = false;
                        if (package.name.Contains("newtonsoft"))
                        {
                            newtonsoftInstalled = true;
                            EndListCheck();
                            break;
                        }
                    }
                else if (listRequest.Status >= StatusCode.Failure)
                {
                    Debug.Log(listRequest.Error.message);
                }
                    

                EndListCheck();
            }
        }
        static void EndListCheck()
        {
            EditorApplication.update -= CheckPackageProgress;
                
            if (newtonsoftInstalled)
            {
                if(OnListCheckCompleteActionNewtonPackageExists!=null)
                    OnListCheckCompleteActionNewtonPackageExists.Invoke(true); 
            }
            else
            {
                if(OnListCheckCompleteActionNewtonPackageExists!=null)
                    OnListCheckCompleteActionNewtonPackageExists.Invoke(false); 
            }
        }
        
        
          
        //////////// ADD
        public static void Add()
        {
            // Add a package to the project
            Request = Client.Add("com.unity.nuget.newtonsoft-json");
            EditorApplication.update += AddProgress;
        }
        
        static void AddProgress()
        {
            if (Request.IsCompleted)
            {
                if (Request.Status == StatusCode.Success)
                    Debug.Log("Installed: " + Request.Result.packageId);
                else if (Request.Status >= StatusCode.Failure)
                    Debug.Log(Request.Error.message);

                EditorApplication.update -= AddProgress;
                CheckPkgsListForNewtonsoft();
            }
        }
        
        //////////// RM
        public static void Remove()
        {
            RmRequest = Client.Remove("com.unity.nuget.newtonsoft-json");
            EditorApplication.update += RemoveProgress;
        }
        
        static void RemoveProgress()
        {
            if (RmRequest.IsCompleted)
            {
                if (RmRequest.Status == StatusCode.Success)
                {
                    Debug.Log("removed: " + "com.unity.nuget.newtonsoft-json");
                    CheckPkgsListForNewtonsoft();
                }
                else if (RmRequest.Status >= StatusCode.Failure)
                    Debug.Log(RmRequest.Error.message);

                EditorApplication.update -= RemoveProgress;
            }
        }
        

        #endregion

        #region gltfutility
        private static UnityAction<bool> OnListCheckCompleteActionGLTFxists;
        public static void OnListCheckCompleteForGLTF(UnityAction<bool> action)
        {
            OnListCheckCompleteActionGLTFxists = action;
          
        }
        /// ///////// CHECK
        static ListRequest listRequestgltf;
        public static void CheckPkgsListForGLTFt()
        {
            listRequestgltf = Client.List();    // List packages installed for the Project
            EditorApplication.update += CheckPackageProgressGLTF;
        }

        static void CheckPackageProgressGLTF()
        {
            if (listRequestgltf.IsCompleted)
            {
                if (listRequestgltf.Status == StatusCode.Success)
                    foreach (var package in listRequestgltf.Result)
                    {
                        GLTFInstalled = false;
                        if (package.name.Contains("com.siccity.gltfutility"))
                        {
                            GLTFInstalled = true;
                            break;
                        }
                    }
                else if (listRequestgltf.Status >= StatusCode.Failure)
                {
                    Debug.Log(listRequestgltf.Error.message);
                }  
                
                EndListCheckGLTF();
            }
        }
        static void EndListCheckGLTF()
        {
            EditorApplication.update -= CheckPackageProgressGLTF;
            
            if (GLTFInstalled)
            {
                if(OnListCheckCompleteActionGLTFxists!=null)
                    OnListCheckCompleteActionGLTFxists.Invoke(true); 
            }
            else
            {
                if(OnListCheckCompleteActionGLTFxists!=null)
                    OnListCheckCompleteActionGLTFxists.Invoke(false); 
            }
        }
        
        
        //////////// ADD
        public static void AddGLTF()
        {
            // Add a package to the project
            //TODO: if PR https://github.com/Siccity/GLTFUtility/pull/203 then change to https://github.com/Siccity/GLTFUtility.git ?? 
            Request = Client.Add("https://github.com/saszer/GLTFUtility.git");
            EditorApplication.update += AddProgressGLTF;
        }
        
        static void AddProgressGLTF()
        {
            if (Request.IsCompleted)
            {
                if (Request.Status == StatusCode.Success)
                {
                    Debug.Log("Installed: " + Request.Result.packageId);
                    CheckPkgsListForGLTFt();
                }
                    
                else if (Request.Status >= StatusCode.Failure)
                    Debug.Log(Request.Error.message);

                EditorApplication.update -= AddProgressGLTF;  
            }
        }
        
        //////////// RM
        public static void RemoveGLTF()
        {
            RmRequest = Client.Remove("com.siccity.gltfutility");
            EditorApplication.update += RemoveProgressGLTF;
        }
        
        static void RemoveProgressGLTF()
        {
            if (RmRequest.IsCompleted)
            {
                if (RmRequest.Status == StatusCode.Success)
                {
                    Debug.Log("removed: " + "https://github.com/Siccity/GLTFUtility.git");
                    CheckPkgsListForGLTFt();
                }
                else if (RmRequest.Status >= StatusCode.Failure)
                    Debug.Log(RmRequest.Error.message);

                EditorApplication.update -= RemoveProgressGLTF;
            }
        }

        #endregion
    }
}