using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTPort.Editor
{
    [Serializable]
    public class PkgJson 
    {
        [Serializable]
        public class Author
        {
            public string name;
            public string email;
            public string url;
        }

        public string name;
        public string displayName;
        public string version;
        public string description;
        public string type;
        public string documentationUrl;
        public List<string> keywords;
        public Author author;
        public List<Sample> samples;

        [Serializable]
        public class Sample
        {
            public string displayName;
            public string description;
            public string path;
        }
    } 
}

