using System;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

namespace NFTPort.Editor
{
    [System.Serializable]
    public class NFTPortSettings : EditorWindow
    {
        
        public static string myAPIString = "Enter Your API Key";
    
        [MenuItem("Window/NFTPort")]
        public static void ShowWindow()
        {
            GetWindow<NFTPortSettings>("NFTPort");
        }

        void OnGUI()
        {
            Texture banner = Resources.Load<Texture>("banner");
            GUILayout.Box(banner);
        
            GUILayout.Label("Welcome to NFTPort Unity SDK (Pre-Alpha) ", EditorStyles.boldLabel);

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
        private Port.UserPrefs _userPrefs = new Port.UserPrefs();
        void ReadFromUserPrefs()
        {
          
            TextAsset targetFile = Resources.Load<TextAsset>("UserPrefs");;
            if (targetFile != null)
            {
                _userPrefs = JsonConvert.DeserializeObject<Port.UserPrefs>(targetFile.text);
                myAPIString = _userPrefs.API_KEY;
            }
            else
            {
                Port._initialised = false;
                myAPIString = "Enter your API KEY here";
            }
                
        }
        void WriteToUserPrefs()
        {
            _userPrefs.API_KEY = myAPIString;
            string json = JsonUtility.ToJson(_userPrefs);
            System.IO.File.WriteAllText("Assets/NFTPort SDK/Resources/" + "UserPrefs.json", json);
            AssetDatabase.Refresh();
            Port._initialised = false;
            base.SaveChanges();
        }

        #endregion
     
        
        
    }
}

