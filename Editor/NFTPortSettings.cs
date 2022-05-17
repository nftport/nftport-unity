using System;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

namespace NFTPort.Editor
{ using Internal;
    [System.Serializable]
    public class NFTPortSettings : EditorWindow
    {
        
        public static string myAPIString = PortConstants.DefaultAPIKey;
         
        protected static Type WindowType = typeof(NFTPortSettings);
        private static bool windowopen = false;
        [MenuItem("NFTPort/Home")]
        public static void ShowWindow()
        {
            GetWindow<NFTPortSettings>(PortConstants.HomeWindowName);
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

            myAPIString = EditorGUILayout.TextField("APIKEY", myAPIString);
            
            if (GUILayout.Button("Save", GUILayout.Height(25)))
                SaveChanges();
            
            if (GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_GettingStarted);
            
            if (GUILayout.Button("Support", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.DiscordInvite);
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
            win.minSize = new Vector2(555, 450);
            win.maxSize = new Vector2(555, 450);
            win.Show();
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
            }
            else
            {
                PortUser._initialised = false;
                myAPIString = PortConstants.DefaultAPIKey;
            }

            ShowHomeWindow();

        }
        void WriteToUserPrefs()
        {
            PortUser.SaveNewApi(myAPIString);
            base.SaveChanges();
        }

        #endregion
     
        
        
    }
}

