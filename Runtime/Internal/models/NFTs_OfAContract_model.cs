
using System;
using System.Collections.Generic;

namespace NFTPort
{
    [Serializable]
    public class NFTs_OfAContract_model
    {
        [Serializable]
        public class Attribute
        {
            public string trait_type;
            public string value;
        }

        [Serializable]
        public class Metadata
        {
            public string name;
            public string description;
            public string image;
            public int edition;
            public object date;
            public List<Attribute> attributes;
            public string compiler;
            public string background_color;
            public string external_url;
            public string animation_url;
            public string dna;
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
            public string name;
            public string chain;
            public string contract_address;
            public string token_id;
            public Metadata metadata;
            public string metadata_url;
            public string file_url;
            public string cached_file_url;
            public DateTime mint_date;
            public FileInformation file_information;
            public DateTime updated_date;
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
            public List<Nft> nfts;
            public Contract contract;
            public int total;
        }
    }
}