using System;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

namespace NFTPort.Editor
{ using Internal;
    [System.Serializable]
    public class NFTPortSettings : EditorWindow
    {
        
        static string myAPIString = PortConstants.DefaultAPIKey;
         
        protected static Type WindowType = typeof(NFTPortSettings);
        private static bool windowopen = false;
        PkgJson releasedPkgJson = null;
        [MenuItem("NFTPort/Home")]
        public static void ShowWindow()
        {
            var win = GetWindow<NFTPortSettings>(PortConstants.HomeWindowName);
            SetSize(win);
        }
        
        /// <summary>   
        /// When the package is imported
        /// </summary>
        [UnityEditor.InitializeOnLoadMethod]
        public static void InitializeOnLoadMethod()
        {
            EditorApplication.delayCall += delayCall;
        }

        static void delayCall()
        {
            ReadFromUserPrefs();
        }

        void OnGUI()
        {
            Texture banner = Resources.Load<Texture>("banner");
            GUILayout.Box(banner);
        
            GUILayout.Label("Welcome to NFTPort Unity SDK ", EditorStyles.whiteLargeLabel);
            GUILayout.Label("\n" +
                            " Create cross-chain compatible NFT games \n and products in Unity with fast and reliable data.\n" +
                            "", EditorStyles.label);

            
            GUILayout.BeginHorizontal("box");
            var defaultColor = GUI.backgroundColor;
            if(APIkeyOk())
                GUI.color = UnityEngine.Color.green;
            else
            {
                GUI.color = UnityEngine.Color.red;
            }
            myAPIString = EditorGUILayout.TextField("APIKEY", myAPIString);
            GUI.color = defaultColor;
            GUILayout.EndHorizontal();
            
            
            if (GUILayout.Button("Save API Key", GUILayout.Height(25)))
                SaveChanges();
            
            EditorGUILayout.LabelField("");
            
            GuiLine();
            
            if (GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_GettingStarted);
            
            if (GUILayout.Button("Community & Support", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.DiscordInvite);
            
            GuiLine();
            
            EditorGUILayout.LabelField("");

            if (userModel != null)
            {
                EditorGUILayout.LabelField("   Welcome " + userModel.profile.name + "."); 
            }
            
            EditorGUILayout.LabelField("");
            
            GuiLine();
            
            GUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("installed version: " + PkgInfo.GetPackageVer());
            
            var ls = LatestRel.Initialize();
            if (ls != null)
            {
                ls.OnComplete(pkg => releasedPkgJson = pkg);
                ls.Run();
            }

            if (releasedPkgJson != null)
            {
                EditorGUILayout.LabelField("Latest release version: " + releasedPkgJson.version);
            }

            GUILayout.EndHorizontal();

        }

        void OnEnable()
        {
            ReadFromUserPrefs();
            windowopen = true;
        }

        private void OnDisable()
        {
            windowopen = false;
        }

        public override void SaveChanges()
        {
            WriteToUserPrefs();
            UserStats();
        }
        
        static void ShowHomeWindow()
        {
            if(myAPIString != PortConstants.DefaultAPIKey || windowopen)
                return;
            
            NFTPortSettings win = GetWindow(WindowType, false, PortConstants.HomeWindowName, true) as NFTPortSettings;
            if (win == null)
            {
                return;  
            }

            windowopen = true;
            SetSize(win);
            win.Show();
        }
        static void SetSize(NFTPortSettings win) 
        {
            win.minSize = new Vector2(530, 590);
            win.maxSize = new Vector2(530, 590);
        } 
        
        static void GuiLine( int i_height = 1 )
        {
            Rect rect = EditorGUILayout.GetControlRect(false, i_height );
            rect.height = i_height;
            EditorGUI.DrawRect(rect, new UnityEngine.Color ( 0.5f,0.5f,0.5f, 1 ) );
        }
        
        #region ReadWrite UserPrefs
        
        private static PortUser.UserPrefs _userPrefs = new PortUser.UserPrefs();
        private static TextAsset targetFile;
        static void ReadFromUserPrefs()
        {
            targetFile = Resources.Load<TextAsset>("NFTPort UserPrefs");
            if (targetFile != null)
            {
                _userPrefs = JsonConvert.DeserializeObject<PortUser.UserPrefs>(targetFile.text);
                myAPIString = _userPrefs.API_KEY;
                PortUser.Initialise();
            }
            else
            { 
                PortUser._initialised = false;
                myAPIString = PortConstants.DefaultAPIKey;
            }

            
            UserStats();
            
            ShowHomeWindow();

        }
        void WriteToUserPrefs()
        {
            PortUser.SaveNewApi(myAPIString);
            base.SaveChanges();
        }

        #endregion

        #region Userstats
        static User_model userModel;
        static void UserStats()
        {
            userModel = null;
            User_Settings
                .Initialize(true)
                .OnComplete(usermodel=> userModel = usermodel)
                .Run();
        }
         

        bool APIkeyOk()
        {
            if (userModel == null)
            {
                PortUser._initialised = false;
                return false;
            }

            if (userModel.response == "OK")
            {
                PortUser.Initialise();
                return true;
            }
            else
            {
                PortUser._initialised = false;
                return false;
            }
        }

        #endregion
         
        
    }
}

