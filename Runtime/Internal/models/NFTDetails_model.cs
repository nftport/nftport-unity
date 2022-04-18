using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTPort
{
    [Serializable]
    public class NFTDetails_model 
    {
        [Serializable]
        public class Metadata
        {
            public string description;
            public string background_color;
            public string external_url;
            public string image;
            public string name;
            public string animation_url;
        }
        [Serializable]
        public class FileInformation
        {
            public int height;
            public int width;
            public int file_size;
        }
        [Serializable]
        public class Nft
        {
            public string chain;
            public string contract_address;
            public string token_id;
            public string metadata_url;
            public Metadata metadata;
            public FileInformation file_information;
            public string file_url;
            public string cached_file_url;
            public string mint_date;
            public string updated_date;
        }
        [Serializable]
        public class Contract
        {
            public string name;
            public string symbol;
            public string type;
        }
        [Serializable]
        public class Root
        {
            public string response;
            public Nft nft;
            public Contract contract;
        }
    }
}
