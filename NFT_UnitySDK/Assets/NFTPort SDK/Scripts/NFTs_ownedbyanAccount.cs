using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace NFTPort  
{
    
    public class NFTs_ownedbyanAccount : MonoBehaviour
    {
  
        public string Address = "Input Account Address Contract";
        [StringInList("ethereum","polygon")]
        public string Chain;
        [StringInList("default","metadata", "contract_information")]
        public string include;

        private string RequestUriInit = "https://api.nftport.xyz/v0/accounts/";
        private string WEB_URL;
        private  string _apiKey;
        
        //Event Called after success, actions can be set within editor. This can be used to call UI updateing scripts and any further actions required after getting data from API.
        public UnityEvent AfterSuccess;

        //Gets filled with data and can be refrenced >
        public ownedbyAddress_model.Root _ownedbyAddreddModel;


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
            WEB_URL = RequestUriInit  + Address + "?chain=" + Chain + "&include=" + include;
            Debug.Log(WEB_URL);
            return WEB_URL;
            ;
        }
        
        IEnumerator CallAPIProcess()
        {
            //Make request
            UnityWebRequest rq = UnityWebRequest.Get(WEB_URL);
            rq.SetRequestHeader("Content-Type", "application/json");
            rq.SetRequestHeader("Authorization", _apiKey);

            {
                yield return rq.SendWebRequest();
                string jsonResult = System.Text.Encoding.UTF8.GetString(rq.downloadHandler.data);
                
                
                Debug.Log(jsonResult);
                
                //Fill Data Model from recieved class
                _ownedbyAddreddModel = JsonConvert.DeserializeObject<ownedbyAddress_model.Root>(jsonResult);
                if (_ownedbyAddreddModel == null)
                {
                    Debug.LogError($"Null data. Response code: {rq.responseCode}.");
                    yield break;
                }

                if(_ownedbyAddreddModel != null)
                {
                    AfterSuccess.Invoke();
                }
            }

            //TODO Fetch NFT Image
            // StartCoroutine(GetTexture(NFTPort_NFTModel.nft.file_url));
        }
    }

}
