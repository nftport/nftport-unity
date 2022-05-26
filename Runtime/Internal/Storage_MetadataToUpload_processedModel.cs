using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine;

namespace NFTPort.Internal
{
    [Serializable]
    public class Storage_MetadataToUpload_processedModel
    {
        [Tooltip("URL of the file that you wish to link with the metadata and turn into an NFT., use 'Storage File Upload' to get url")]
        public string file_url = "Required Field";
        public string name = "Required Field";
        public string description = "Required Field";

        [DefaultValue("")][Tooltip("URL that will appear below the NFT on some of the NFT marketplaces such as OpenSea.")]
        public string external_url;
        
        [DefaultValue("")][Tooltip("URL to a multimedia attachment with all filetypes supported. If you want to make sure the file is supported by OpenSea, then see their docs. When using animation_url, set the file_url as the multimedia preview which will be displayed on the NFT marketplaces e.g. if your animation_url is a video then set file_url as the preview image for it.")]
        public string animation_url;

        [DefaultValue(null)]
        [Tooltip(
            "Allows you to extend the metadata schema with your own arbitrary fields. You can pass anything here as long as it is follows “key”: “value” format inside a dictionary. All of the fields will be flattened and added to the top-level namespace e.g. like name, description, etc. ")]
        public Dictionary<string, string> custom_fields;
        
        [DefaultValue(null)]
        public List<ProcessedAttribute> attributes;
       
    }
    public class ProcessedAttribute 
    {
        public string trait_type;
        public string value;
        public int max_value;
        [DefaultValue(null)]
        public string display_type;
    }
}

