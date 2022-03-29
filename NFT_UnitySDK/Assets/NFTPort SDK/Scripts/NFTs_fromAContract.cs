using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine.Events;

namespace NFTPort
{
    public class NFTs_fromAContract : MonoBehaviour
    {

        public string Contract = "Input NFT Collection Contract";
        [StringInList("ethereum","polygon")]
        public string Chain;
        [StringInList("default","metadata", "all")]
        public string include;

        private string RequestUriInit = "https://api.nftport.xyz/v0/nfts/";
        //private string RequestUriInit = "http://0.0.0.0:80/v0/nfts/";
        private string WEB_URL;
        private string _apiKey;
        
        //Event Called after success, actions can be set within editor. This can be used to call UI updateing scripts and any further actions required after getting data from API.
        public UnityEvent AfterSuccess;
        
        
        //Gets filled with data and can be refrenced >
        public fromContract_model.Root _fromContractModel;

        private void Awake()
        {
            Port.Initialise();
            _apiKey = Port.GetUserApiKey();
        }
        
        public void Run()
        { 
            WEB_URL = BuildUrl();
            StartCoroutine(CallAPIProcess());
        }
        
        string BuildUrl()
        {
            WEB_URL = RequestUriInit  + Contract + "?chain=" + Chain + "&include=" + include;
            return WEB_URL;
        }
        
        IEnumerator CallAPIProcess()
        {
            //Make Request
            UnityWebRequest rq = UnityWebRequest.Get(WEB_URL);
            rq.SetRequestHeader("Content-Type", "application/json");
            rq.SetRequestHeader("Authorization",  _apiKey);

            {
                yield return rq.SendWebRequest();
                string jsonResult = System.Text.Encoding.UTF8.GetString(rq.downloadHandler.data);
                
                
               // Debug.Log(jsonResult);
               
               
               //Fill Data Model from recieved class
                _fromContractModel = JsonConvert.DeserializeObject<fromContract_model.Root>(jsonResult);
                if (_fromContractModel == null)
                {
                    Debug.LogError($"Null data. Response code: {rq.responseCode}.");
                    yield break;
                }
                
                if (_fromContractModel != null)
                    AfterSuccess.Invoke();
                
            }

            //TODO Fetch NFT Image
            //StartCoroutine(GetTexture(NFTPort_NFTModel.nft.file_url));
        }
        
    }
}

