using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using Newtonsoft.Json;
using UnityEngine;

namespace NFTPort
{
    [System.Serializable]
    public class MetadataToUpload_model
    {
        [Tooltip("Main NFT File URL, use 'Storage File Upload' to get url")]
        public string file_url = "Required Field";
        public string name = "Required Field";
        public string description = "Required Field";
        [DefaultValue("-99")][Tooltip("Set to -99 to exclude in metadata")]
        public int edition;
        [DefaultValue(null)]
        public List<Trait> traits;
        [DefaultValue(null)]
        public List<Attribute> attributes;
        [DefaultValue("")]
        public string compiler;
        [DefaultValue("")]
        public string background_color;
        [DefaultValue("")]
        public string external_url;
        [DefaultValue("")]
        public string animation_url;
        [DefaultValue("")]
        public string dna;
        [DefaultValue("")]
        public string id;
    }
}

