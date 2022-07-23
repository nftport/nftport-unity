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
        static AddRequest Request2;
        private static RemoveRequest RmRequest;
        private static bool newtonsoftInstalled = false;
        private static bool GLTFInstalled = false;    
        private static bool InputsysInstalled = false;    

        private static bool refreshing = false;    
        [MenuItem("NFTPort/Install Dependencies")]
        public static void ShowWindow()
        {
            var win = GetWindow<InstallPortDependencies>("Port Dependencies");
            SetSize(win);
        }

        #region Scri[tinDefines
        public static bool CheckScriptingDefine(string scriptingDefine)
        {
            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            return defines.Contains(scriptingDefine);
        }

        public static void SetScriptingDefine(string scriptingDefine)
        {
            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            if (!defines.Contains(scriptingDefine))
            {
                defines += $";{scriptingDefine}";
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, defines);
            }
        }

        public static void RemoveScriptingDefine(string scriptingDefine)
        {
            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            if (defines.Contains(scriptingDefine))
            {
                string newDefines = defines.Replace(scriptingDefine, "");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, newDefines);
            }
        }
        

        #endregion
        
        
        
        void OnGUI()
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("\n" +
                            " NFTPort needs the following packages : \n" +
                            "", EditorStyles.label);
            
            if (GUILayout.Button("Refresh", GUILayout.Height(18)))
                OnEnable();
            GUILayout.EndHorizontal();
            
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
            GUILayout.Label("gltFast| Used by NFTPort Playground Sample");
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
            
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("Unity Input System | Used by NFTPort Playground Sample");
            if (InputsysInstalled)
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
                AddInputSys();
            
            if (GUILayout.Button("Remove Dependency", GUILayout.Height(25)))
                RemoveInputSys();
            GUILayout.EndHorizontal();
            
            
            GUI.contentColor = Color.yellow;
            if(refreshing)
                GUILayout.Label("refreshing...");
        }
        
        static void SetSize(InstallPortDependencies win) 
        {
            win.minSize = new Vector2(330*2, 290);
            win.maxSize =  new Vector2(330*2, 290);
        }

        private void OnEnable()
        {
         
            CheckPkgsListForNewtonsoft();
            CheckPkgsListForGLTFt();
            CheckPkgsListForInputsyst();
        }

        #region newtonsoft
       /// ///////// CHECK
        static ListRequest listRequest;
        public static void CheckPkgsListForNewtonsoft()
        {
            listRequest = Client.List();    // List packages installed for the Project
            EditorApplication.update += CheckPackageProgress;
            refreshing = true;
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
            refreshing = false;
                
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
            refreshing = true;
        }

        static void CheckPackageProgressGLTF()
        {
            if (listRequestgltf.IsCompleted)
            {
                if (listRequestgltf.Status == StatusCode.Success)
                    foreach (var package in listRequestgltf.Result)
                    {
                        GLTFInstalled = false;
                        if (package.name.Contains("com.atteneder.gltfast"))
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
            refreshing = false;
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
            Request = Client.Add("https://github.com/atteneder/glTFast.git");
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
            RmRequest = Client.Remove("com.atteneder.gltfast");
            EditorApplication.update += RemoveProgressGLTF;
        }
        
        static void RemoveProgressGLTF()
        {
            if (RmRequest.IsCompleted)
            {
                if (RmRequest.Status == StatusCode.Success)
                {
                    Debug.Log("removed: " + "com.atteneder.gltfast");
                    CheckPkgsListForGLTFt();
                }
                else if (RmRequest.Status >= StatusCode.Failure)
                    Debug.Log(RmRequest.Error.message);

                EditorApplication.update -= RemoveProgressGLTF;
            }
        }

        #endregion
        
        #region UnityInputSystem
        private static UnityAction<bool> OnListCheckCompleteActionInputSysxists;
        public static void OnListCheckCompleteForInputSys(UnityAction<bool> action)
        {
            OnListCheckCompleteActionInputSysxists = action;
          
        }
        /// ///////// CHECK
        static ListRequest listRequestinputsys;
        public static void CheckPkgsListForInputsyst()
        {
            listRequestinputsys = Client.List();    // List packages installed for the Project
            EditorApplication.update += CheckPackageProgressInputsys;
            refreshing = true;
        }

        static void CheckPackageProgressInputsys()
        {
            if (listRequestinputsys.IsCompleted)
            {
                if (listRequestinputsys.Status == StatusCode.Success)
                    foreach (var package in listRequestinputsys.Result)
                    {
                        InputsysInstalled = false;
                        if (package.name.Contains("com.unity.inputsystem"))
                        {
                            InputsysInstalled = true;
                            break;
                        }
                    }
                else if (listRequestinputsys.Status >= StatusCode.Failure)
                {
                    Debug.Log(listRequestinputsys.Error.message);
                }  
                
                EndListCheckInputsys();
            }
        }
        static void EndListCheckInputsys()
        {
            EditorApplication.update -= CheckPackageProgressInputsys;
            refreshing = false;
            if (InputsysInstalled)
            {
                if(OnListCheckCompleteActionInputSysxists!=null)
                    OnListCheckCompleteActionInputSysxists.Invoke(true);

                SetScriptingDefine("ENABLE_INPUT_SYSTEM");
            }
            else
            {
                if(OnListCheckCompleteActionInputSysxists!=null)
                    OnListCheckCompleteActionInputSysxists.Invoke(false); 
                RemoveScriptingDefine("ENABLE_INPUT_SYSTEM");
            }
        }
        
        
        //////////// ADD
        public static void AddInputSys()
        {
            // Add a package to the project
            Request2 = Client.Add("com.unity.inputsystem");
            EditorApplication.update += AddProgressInputSys;
        }
        
        static void AddProgressInputSys()
        {
            if (Request2.IsCompleted)
            {
                if (Request2.Status == StatusCode.Success)
                {
                    Debug.Log("Installed: " + Request2.Result.packageId);
                    CheckPackageProgressInputsys();
                }
                    
                else if (Request2.Status >= StatusCode.Failure)
                    Debug.Log(Request2.Error.message);

                EditorApplication.update -= AddProgressInputSys;  
            }
        }
        
        //////////// RM
        public static void RemoveInputSys()
        {
            RemoveScriptingDefine("ENABLE_INPUT_SYSTEM");
            RmRequest = Client.Remove("com.unity.inputsystem");
            EditorApplication.update += RemoveProgressInputSys;
        }
        
        static void RemoveProgressInputSys()
        {
            if (RmRequest.IsCompleted)
            {
                if (RmRequest.Status == StatusCode.Success)
                {
                    Debug.Log("removed: " + RmRequest.PackageIdOrName);
                }
                else if (RmRequest.Status >= StatusCode.Failure)
                    Debug.Log(RmRequest.Error.message);

                EditorApplication.update -= RemoveProgressInputSys;
                CheckPackageProgressInputsys();
            }
        }

        #endregion
    }
}