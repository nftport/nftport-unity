using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTPort
{
    [Serializable]
   public class fromContract_model
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
           public string id;
           public List<Attribute> attributes;
           public string image;
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
           public string token_id;
           public string chain;
           public string contract_address;
           public Metadata metadata;
           public string metadata_url;
           public string file_url;
           public string cached_file_url;
           public object mint_date;
           public FileInformation file_information;
           public String updated_date;
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
