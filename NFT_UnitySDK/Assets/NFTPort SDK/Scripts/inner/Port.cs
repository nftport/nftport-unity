using System;
using Newtonsoft.Json;
using UnityEngine;

namespace NFTPort
{
    public static class Port
    {

        //Global Class Model for UserPrefs
        [System.Serializable]
        public class UserPrefs{
            public string API_KEY;
        }
        
        
        public static bool _initialised = false;
        private static UserPrefs _userPrefs = new UserPrefs();
        
        public static void Initialise()
        {
            if(_initialised==false)
                _userPrefs = getUserPrefs();
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
                return "Run Port.Initialise() before calling GetUserApiKey()";
        }

        
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
                Debug.LogError("Unable to Initialise, Make sure to input your APIKEYS in Window/NFTPort in Unity Editor");
                return null;
            }
        }

        private static TextAsset targetFile;
        public static string LoadUserPrefsTextfile()
        {
            targetFile = Resources.Load<TextAsset>("UserPrefs");;
            if (targetFile != null)
                return targetFile.text;
            else
                return string.Empty;
        }
    }

}
