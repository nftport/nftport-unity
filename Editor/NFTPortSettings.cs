using System;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

namespace NFTPort.Editor
{ using Internal;
    [System.Serializable]
    public class NFTPortSettings : EditorWindow
    {
        
        public static string myAPIString = "Enter Your API Key";
    
        [MenuItem("NFTPort/Home")]
        public static void ShowWindow()
        {
            GetWindow<NFTPortSettings>("Home");
        }

        void OnGUI()
        {
            Texture banner = Resources.Load<Texture>("banner");
            GUILayout.Box(banner);
        
            GUILayout.Label("Welcome to NFTPort Unity SDK ", EditorStyles.boldLabel);

            myAPIString = EditorGUILayout.TextField("APIKEY", myAPIString);
            
            if (GUILayout.Button("Save"))
                SaveChanges();
        }

        void OnEnable()
        {
            ReadFromUserPrefs();
        }

        public override void SaveChanges()
        {
            WriteToUserPrefs();
        }
        
        
        

        #region ReadWrite UserPrefs
        
        [SerializeField]
        private PortUser.UserPrefs _userPrefs = new PortUser.UserPrefs();
        void ReadFromUserPrefs()
        {
            TextAsset targetFile = Resources.Load<TextAsset>("NFTPort UserPrefs");;
            if (targetFile != null)
            {
                _userPrefs = JsonConvert.DeserializeObject<PortUser.UserPrefs>(targetFile.text);
                myAPIString = _userPrefs.API_KEY;
            }
            else
            {
                PortUser._initialised = false;
                myAPIString = "Enter your API KEY here";
            }
                
        }
        void WriteToUserPrefs()
        {
            PortUser.SaveNewApi(myAPIString);
            base.SaveChanges();
        }

        #endregion
     
        
        
    }
}

