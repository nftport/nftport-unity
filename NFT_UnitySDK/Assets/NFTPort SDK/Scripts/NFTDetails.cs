using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace NFTPort
{
    public class NFTDetails : MonoBehaviour
    {
        
        public string Contract = "Input NFT Collection Contract";
        public string Token_id = "Enter token ID";
        [StringInList("ethereum","polygon","rinkeby")]
        public string Chain;
       

        private string RequestUriInit = "https://api.nftport.xyz/v0/nfts/";
        
        private string WEB_URL;
        private string _apiKey;
        
        //Event Called after success, actions can be set within editor. This can be used to call UI updateing scripts and any further actions required after getting data from API.
        public UnityEvent AfterSuccess;
        
        //Gets filled with data and can be refrenced >
        public NFTDetails_model.Root _NftDetailsModel;
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
            WEB_URL = RequestUriInit + Contract + "/" + Token_id + "?chain=" + Chain;
            return WEB_URL;
        }


        IEnumerator CallAPIProcess()
        {

            UnityWebRequest rq = UnityWebRequest.Get(WEB_URL);
            rq.SetRequestHeader("Content-Type", "application/json");
            rq.SetRequestHeader("Authorization", _apiKey);

            {
                yield return rq.SendWebRequest();

                string jsonResult = System.Text.Encoding.UTF8.GetString(rq.downloadHandler.data);


                Debug.Log(jsonResult);
                _NftDetailsModel = JsonConvert.DeserializeObject<NFTDetails_model.Root>(jsonResult);
                if (_NftDetailsModel == null)
                {
                    Debug.LogError($"Null data. Response code: {rq.responseCode}.");
                    yield break;
                }
            }
            
            if (_NftDetailsModel != null)
                AfterSuccess.Invoke();
            
        }
    }

}
