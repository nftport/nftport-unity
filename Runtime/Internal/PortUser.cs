using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.Events;
#endif


namespace NFTPort.Internal
{
    public static class PortUser
    {
        //Global Class Model for UserPrefs
        [System.Serializable]
        public class UserPrefs{
            public string API_KEY;
            public string version = "x";
        }
        
        
        public static bool _initialised = false;
        private static UserPrefs _userPrefs = new UserPrefs();
        
        public static void Initialise()
        {
            if(_initialised==false)
                _userPrefs = getUserPrefs();
        }

        #region Get Set Gos

        public static void SetVersion(string pkgVersion)
        {
            _userPrefs.version = pkgVersion;
        }

        class Source
        {
            public string from = "NFTPort-Unity";
            public string version = _userPrefs.version;
            public string isEditor = "";

        }
        
        
        public static string GetSource()
        {
            Source src = new Source();
            src.version = _userPrefs.version;
            src.isEditor = Application.isEditor.ToString();

                string json = JsonConvert.SerializeObject(
                    src, 
                new JsonSerializerSettings
                {
                    //DefaultValueHandling = DefaultValueHandling.Ignore,
                    //NullValueHandling = NullValueHandling.Ignore
                });
            
            return json;
        }

        public static string GetUserApiKey()
        {
            if (_initialised)
            {
                return _userPrefs.API_KEY;
            }
            else if(targetFile==null)
            {
                return String.Empty;
            }
            else
                return " Make sure to input your APIKEYS in NFTPort/Home ";
        }
        

        #endregion
   

        #region Prefs File Read

        static UserPrefs getUserPrefs()
        {
            string _userfile = LoadUserPrefsTextfile();
            if (_userfile.Length != 0)
            {
                _initialised = true;
                return JsonConvert.DeserializeObject<UserPrefs>(_userfile);
            }
            else
            {
                _initialised = false;
                Debug.LogError("Unable to Initialise, Make sure to input your APIKEYS in NFTPort/Home in Unity Editor");
                return null;
            }
        }
        private static TextAsset targetFile;
        public static string LoadUserPrefsTextfile()
        {
            targetFile = Resources.Load<TextAsset>("NFTPort UserPrefs");;
            if (targetFile != null)
                return targetFile.text;
            else
                return string.Empty;
        }

        #endregion

        #region Prefs File Write
        #if UNITY_EDITOR
        
            public static void SaveNewApi(string newAPI)
            {
                _userPrefs.API_KEY = newAPI;
                if(_initialised == false)
                {
                    if (!AssetDatabase.IsValidFolder("Assets/NFTPort/Resources"))
                    {
                        CreateFolder();
                    }
                    
                    WriteToUserPrefs();
                }

            }
            
            static  void WriteToUserPrefs()
            {
                _initialised = false;
                string json = JsonUtility.ToJson(_userPrefs);
                System.IO.File.WriteAllText("Assets/NFTPort/Resources/" + "NFTPort UserPrefs.json", json);
                AssetDatabase.Refresh();
                PortUser.Initialise();
            }

           static void CreateFolder()
            {
                AssetDatabase.CreateFolder("Assets", "NFTPort");
                string guid = AssetDatabase.CreateFolder("Assets/NFTPort", "Resources");
                string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
            }
           
        #endif
        

        #endregion

    }
}
