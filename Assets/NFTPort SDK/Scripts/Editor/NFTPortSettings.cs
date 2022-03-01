using System;
using UnityEngine;
using UnityEditor;

namespace NFTPort.Editor
{
    [System.Serializable]
    public class NFTPortSettings : EditorWindow
    {
    
        //TODO fix editor not saving api bug
        public static string myAPIString = "Enter Your API Key";
    
        [MenuItem("Window/NFTPort")]
        public static void ShowWindow()
        {
            GetWindow<NFTPortSettings>("NFTPort");
        }

        void OnGUI()
        {
            Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Assets/NFTPort SDK/banner.png", typeof(Texture));
            GUILayout.Box(banner);
        
            GUILayout.Label("Welcome to NFTPort Unity SDK", EditorStyles.boldLabel);

            myAPIString = EditorGUILayout.TextField("APIKEY", myAPIString);
            
            if (GUILayout.Button("Save"))
                SaveChanges();
        }

        void OnEnable()
        {
            myAPIString = PlayerPrefs.GetString("APIKEY");
        }
        
        [System.Serializable]
        public class UserPrefs{
            public string API_KEY;
        }
        
        [SerializeField]private UserPrefs _userPrefs = new UserPrefs();
        public override void SaveChanges()
        {
            _userPrefs.API_KEY = myAPIString;
            string _json = JsonUtility.ToJson(_userPrefs);
            System.IO.File.WriteAllText("Assets/NFTPort SDK/Resources/" + "UserPrefs.json", _json);
            Debug.Log($"{this} saved successfully!!!");

            base.SaveChanges();
        }
        
        
    }
}

