using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace NFTPort
{
    public static class Port
    {

        private static bool _initialised = false;
           
        [System.Serializable]
        public class UserPrefs{
            public string API_KEY;
        }
        
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
            else
            {
                return "Run Port.Initialise() before calling GetUserApiKey()";
            }
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
                return null;
            }
        }
        
        public static string LoadUserPrefsTextfile()
        {
            TextAsset targetFile = Resources.Load<TextAsset>("UserPrefs");;
            if (targetFile != null)
                return targetFile.text;
            else
                return string.Empty;
        }
    }

}
