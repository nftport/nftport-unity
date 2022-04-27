using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Video;

namespace NFTPort
{
    /// <summary>
    /// Downloads Asset from given url 
    /// </summary>
    public class AssetDownloader : MonoBehaviour
    {
        public bool debugErrorLog = true;
        private bool _destroyAtEnd = false;

        public UnityEvent OnFetchStart;
        public UnityEvent afterError;
        public UnityEvent afterSuccess;

        public static int assetsDownloaders = 0;

        public Texture2D lastGetImage;
        
        public class GetImage : AssetDownloader
        {
            private UnityAction<string> OnErrorAction;
            private UnityAction<UnityEngine.Texture2D> OnCompleteAction;
            private static UnityAction<string> OnAllAssetsDownloadEnded;
            
            /// <summary>
            /// Initialize creates a gameobject and assings this script as a component. This must be called if you are not refrencing the script any other way and it doesn't already exists in the scene.
            /// </summary>
            /// <param name="destroyAtEnd"> Optional bool parameter can set to false to avoid Spawned GameObject being destroyed after the Api process is complete. </param>
            public static GetImage Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject("AssetDownloader_Texture2D").AddComponent<GetImage>();
                _this._destroyAtEnd = destroyAtEnd;
                _this.debugErrorLog = false;
                return _this;
            }

            /// <summary>
            /// Action on succesfull Download
            /// </summary>
            /// <returns> Texture2D </returns>
            public GetImage OnComplete(UnityAction<Texture2D> action)
            {
                this.OnCompleteAction = action;
                return this;
            }
            
            /// <summary>
            /// Action on Error
            /// </summary>
            /// <param name="UnityAction action"> string.</param>
            /// <returns> Information on Error as string text.</returns>
            public GetImage OnError(UnityAction<string> action)
            {
                this.OnErrorAction = action;
                return this;
            }
            
            
            /// <summary>
            /// Useful when you are initialising multiple asset downloaders and want an event when all are done
            /// </summary>
            /// <param name="UnityAction action"> string.</param>
            /// <returns> String </returns>
            public GetImage OnAllAssetDownloadersDone(UnityAction<string> action)
            {
                OnAllAssetsDownloadEnded = action;
                return this;
            }

            /// <summary>
            /// Download
            /// </summary>
            /// <param name="URL"> Pass URL of Image asset as string.</param>
            /// <param name="bool isIPFS"> Pass true if URL is of format https://ipfs.io/ipfs/bafybeiejnu5pteh2rqqewes2vj2nyuos6zbrrm6ctoglpq6k6cdgbmn44i' </param>
            /// <param name="NFT_Assets in NFTs_model"> Optional nft reference can be passed here to assign fetched texture to Nft.assets.image_texture </param>
            public GetImage Download(string URL, Assets NFT_Assets = null, bool isIPFS = false)
            {
                if(isIPFS)
                    URL = "https://cloudflare-ipfs.com/ipfs/" + URL.Replace("ipfs://", "");

                if (OnFetchStart != null)
                    OnFetchStart.Invoke();
                
                StartCoroutine(DownloadTexture(URL, NFT_Assets));
                return this;
            }

            
            IEnumerator DownloadTexture(string URL, Assets NFT_Assets = null)
            {
                assetsDownloaders++;
                using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(URL))
                {
                    yield return request.SendWebRequest();

                    if(request.result != UnityWebRequest.Result.Success) {
                        if(OnErrorAction!=null)
                            OnErrorAction($"Null data. Response code: {request.responseCode}. Result {request.downloadHandler.text}");
                        if(debugErrorLog)
                            Debug.Log($"Null data. Response code: {request.responseCode}. Result {request.downloadHandler.text}");
                        if(afterError!=null)
                            afterError.Invoke();
                    }
                    else
                    {
                        lastGetImage = DownloadHandlerTexture.GetContent(request);
                        //UnityEngine.Texture2D NFTImage = ((DownloadHandlerTexture)request.downloadHandler).texture;
                        
                        request.Dispose();
                        //yield return request.result;

                        if (NFT_Assets != null)
                            NFT_Assets.image_texture = lastGetImage;
                        
                        if(OnCompleteAction!=null && lastGetImage!=null)
                            OnCompleteAction.Invoke(lastGetImage);
                        
                        if(afterSuccess!=null)
                            afterSuccess.Invoke();
                    }

                    assetsDownloaders--;
                    if (assetsDownloaders == 0)
                    {
                        if (OnAllAssetsDownloadEnded != null)
                            OnAllAssetsDownloadEnded("assetsDownloaders Ended");
                    }
                    
                    if(_destroyAtEnd)
                        Destroy (this.gameObject);
                }
            }
              
        }
    }
   
}