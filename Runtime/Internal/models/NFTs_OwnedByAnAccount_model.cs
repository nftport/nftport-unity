using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTPort
{
    [Serializable]
    public class NFTs_OwnedByAnAccount_model : MonoBehaviour
    {
        [Serializable]
        public class Trait
        {
            public string trait_type;
            public string value;
        }
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
            public string id;
            public string image;
            public List<Trait> traits;
            public List<Attribute> attributes;
            public string compiler;
            public long? date;
            public string dna;
            public int? edition;
        }
        [Serializable]
        public class Nft
        {
            public string name;
            public string contract_address;
            public string token_id;
            public string description;
            public string file_url;
            public string cached_file_url;
            public string creator_address;
            public Metadata metadata;
            public string metadata_url;
        }
        [Serializable]
        public class Root
        {
            public string response;
            public List<Nft> nfts;
            public object total;
            public object continuation;
        }


    }

}
