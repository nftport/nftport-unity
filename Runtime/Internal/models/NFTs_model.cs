using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Object = UnityEngine.Object;

namespace NFTPort
{
    [Serializable]
    public class NFTs_model 
    {
        public string response;
        public List<Nft> nfts;
        public Contract contract;
        public int total;
        public object continuation;
    }
    
    [Serializable]
    public class Attribute 
    {
        public string trait_type;
        public string value;
    }
    
    [Serializable]
    public class Trait
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
        public List<Trait> traits;
        public List<Attribute> attributes;
        public string compiler;
        public string background_color;
        public string external_url;
        public string animation_url;
        public string dna;
        public string id;
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
        public string description;
        public string creator_address;
        public Assets assets;
    }
    
    [Serializable]
    public class Assets
    {
        [Header("Unity Assets which can be linked to Nft")]
        public Texture2D image_texture;
        public AudioClip audioClip;
        public VideoClip videoClip;
        public Animation animation;
        public GameObject gameObject;
        public Mesh mesh;
        public Dictionary<string, Object> Objects = new Dictionary<string, Object>(); //User definable.
    }

    [Serializable]
    public class Contract 
    {
        public string name;
        public string symbol;
        public string type;
    }
}