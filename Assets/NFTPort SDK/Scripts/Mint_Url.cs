using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


namespace NFTPort
{
    public class Mint_Url : MonoBehaviour
    {
        public string fileurl = "Add file url to mint";
        [StringInList("ethereum","polygon")]
        public string Chain;

        public string Name = "name";
        public string Description = "description";
        public string MintToAddress = "wallet adrress";
        private string RequestUriInit = "https://api.nftport.xyz/v0/mints/easy/urls";
        private string WEB_URL;
        private string _apiKey;

        [SerializeField] private TextMeshProUGUI OutputText;

        
        private void Awake()
        {
            Port.Initialise();
            _apiKey = Port.GetUserApiKey();
        }
        
        public void Run()
        {
            WEB_URL = BuildUrl();
            createNFTClass();
        }

        string BuildUrl()
        {
            WEB_URL = RequestUriInit;
            return WEB_URL;
            
        }

        private class EasyMintNFT
        {
            public string chain;
            public string name;
            public string description;
            public string file_url;
            public string mint_to_address;
        }

        private EasyMintNFT nft;
        void createNFTClass() //Sets Values of NFT to Mint
        {
            nft = new EasyMintNFT();
            nft.chain = Chain;
            nft.name = Name;
            nft.description = Description;
            nft.file_url = fileurl;
            nft.mint_to_address = MintToAddress;
            
            StartCoroutine(CallAPIProcess());
        }
        
        IEnumerator CallAPIProcess()
        {

            string json = JsonUtility.ToJson(nft);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);


            var rq = new UnityWebRequest(WEB_URL, "POST");
            
                rq.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
                rq.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
                
                //headers
                rq.SetRequestHeader("Content-Type",  "application/json");
                rq.SetRequestHeader("Authorization", _apiKey);

                
                //Make request
                yield return rq.SendWebRequest();
                if (rq.result != UnityWebRequest.Result.Success)
                {  
                    Debug.Log(rq.error);
                }
                else
                {
                    Debug.Log(rq.downloadHandler.text);
                    Debug.Log("upload complete!");
                }

                //Outputs on a text field, do any further actions here after mint
                OutputText.text = rq.downloadHandler.text;

        }
    }

}
