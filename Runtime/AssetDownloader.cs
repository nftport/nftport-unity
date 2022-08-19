using System;
using System.Collections;
using System.ComponentModel.Design;
using NFTPort.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Video;

namespace NFTPort
{
    /// <summary>
    /// Downloads Asset from given url 
    /// </summary>
    [AddComponentMenu(PortConstants.BaseComponentMenu + PortConstants.FeatureName_AssetDownloader)]
    [HelpURL(PortConstants.Docs_AssetDownloader)]
    public class AssetDownloader : MonoBehaviour
    {
        public bool debugErrorLog = true;
        private bool _destroyAtEnd = false;

        public UnityEvent OnFetchStart;
        public UnityEvent afterError;
        public UnityEvent afterSuccess;

        public static int assetsDownloaders = 0;

        [ReadOnly] public Texture2D lastGetImage;
        [ReadOnly] public string lastDetemineURLContentType;
        public Nft OnCompleteLinkNft;


        /*
        public enum Type
        {
            NotSupported,
            Texture2D,
            GIF
        }
        */
        public class DetemineURLContentType : AssetDownloader
        {
            private UnityAction<string> OnErrorAction;
            private UnityAction<Nft, string> OnCompleteAction;
            //private Type type;
            /// <summary>
            /// Initialize creates a gameobject and assings this script as a component. This must be called if you are not refrencing the script any other way and it doesn't already exists in the scene.
            /// </summary>
            /// <param name="destroyAtEnd"> Optional bool parameter can set to false to avoid Spawned GameObject being destroyed after the Api process is complete. </param>
            public static DetemineURLContentType Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject("AssetDownloader_Type?").AddComponent<DetemineURLContentType>();
                _this._destroyAtEnd = destroyAtEnd;
                _this.debugErrorLog = false;
                return _this;
            }
            
            /// <summary>
            /// Action on success
            /// </summary>
            /// <returns>  Type </returns>
            public DetemineURLContentType OnComplete(UnityAction<Nft, string> action)
            {
                this.OnCompleteAction = action;
                return this;
            }

            /// <summary>
            /// Action on Error
            /// </summary>
            /// <param name="UnityAction action"> string.</param>
            /// <returns> Information on Error as string text.</returns>
            public DetemineURLContentType OnError(UnityAction<string> action)
            {
                this.OnErrorAction = action;
                return this;
            }
            
            
            /// <summary>
            /// Run , Nft class is passed and returned to maintain the link.
            /// </summary>
            public DetemineURLContentType Run(Nft Nft,string URL, bool isIPFS = false)
            {
                if (URL.Contains("ipfs://")) //check just in case user is passing both types of strings
                    isIPFS = true;
                
                if(isIPFS)
                    URL = "https://cloudflare-ipfs.com/ipfs/" + URL.Replace("ipfs://", "");

                StopAllCoroutines();
                StartCoroutine(Determine(URL, Nft));
                return this;
            }

            UnityWebRequest request;
            IEnumerator Determine(string URL, Nft nft)
            {
                assetsDownloaders++;
                using (request = UnityWebRequest.Get(URL))
                {
                    yield return request.SendWebRequest();

                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        if (OnErrorAction != null)
                            OnErrorAction(
                                $"Null data. Response code: {request.responseCode}. Result {request.downloadHandler.text}");
                        if (debugErrorLog)
                            Debug.Log(
                                $"Null data. Response code: {request.responseCode}. Result {request.downloadHandler.text}");
                        lastDetemineURLContentType = String.Empty;
                        End();
                    }
                    else
                    {
                        string x = request.GetResponseHeader("content-type");
                        lastDetemineURLContentType = x;
                        if (debugErrorLog)
                            Debug.Log(x);
                        
                        /*
                        //TypeChecks
                        if (x.Contains("image") && !x.Contains("gif"))
                        {
                            type = Type.Texture2D;
                        }else if (x.Contains("gif"))
                        {
                            type = Type.GIF;
                        }
                        //URLChecks and link //Ad-Hoc
                        if (nft.cached_file_url == URL)
                        {
                            nft.cached_file_urlType = type;
                        }
                        if (nft.cached_animation_url == URL)
                        {
                            nft.cached_animation_urlType = type;
                        }
                        */
                        
                        if(OnCompleteAction!=null)
                            OnCompleteAction.Invoke(nft,x);
                        
                        End();
                    }
                }
            }
            public void Stop(bool destroy = true)
            {
                StopAllCoroutines();
                End();
            }

            public void End()
            {
                if(request != null)
                    request.Dispose();
                StopAllCoroutines();
                if(_destroyAtEnd)
                    Destroy (this.gameObject);
            }
            
        }
        
        
        public class GetImage : AssetDownloader
        {
            private UnityAction<string> OnErrorAction;
            private UnityAction<UnityEngine.Texture2D> OnCompleteAction;
            private UnityAction<Nft> OnCompleteReturnLinkedNftAction;
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
            /// <returns> Texture2D of Nft</returns>
            public GetImage OnComplete(UnityAction<Texture2D> action)
            {
                this.OnCompleteAction = action;
                return this;
            }

            /// <summary>
            /// Action on succesfull Download
            /// </summary>
            /// <returns>  Nft class with attched Texture2D</returns>
            public GetImage OnCompleteReturnLinkedNft(Nft Nft, UnityAction<Nft> action)
            {
                OnCompleteLinkNft = Nft;
                this.OnCompleteReturnLinkedNftAction = action;
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
            /// <param name="NFT in NFTs_model"> Optional nft reference can be passed here to assign fetched texture to NFT.assets.image_texture </param>
            public GetImage Download(string URL, Nft NFT = null, bool isIPFS = false)
            {
                if (URL.Contains("ipfs://")) //check just in case user is passing both types of strings
                    isIPFS = true;
                if(isIPFS)
                    URL = "https://cloudflare-ipfs.com/ipfs/" + URL.Replace("ipfs://", "");

                if (OnFetchStart != null)
                    OnFetchStart.Invoke();
                
                StopAllCoroutines();
                StartCoroutine(DownloadTexture(URL, NFT));
                return this;
            }
            
            public void Stop(bool destroy = true)
            {
                StopAllCoroutines();
                End();
            }

            private UnityWebRequest request;
            IEnumerator DownloadTexture(string URL, Nft NFT = null)
            {
                assetsDownloaders++;
                using (request = UnityWebRequestTexture.GetTexture(URL))
                {
                    yield return request.SendWebRequest();
                   
                    if(request.result != UnityWebRequest.Result.Success) {
                        if(OnErrorAction!=null)
                            OnErrorAction($"Null data. Response code: {request.responseCode}. Result {request.downloadHandler.text}");
                        if(debugErrorLog)
                            Debug.Log($"Null data. Response code: {request.responseCode}. Result {request.downloadHandler.text}");
                        if(afterError!=null)
                            afterError.Invoke();

                        End();
                    }
                    else
                    {
                        lastGetImage = DownloadHandlerTexture.GetContent(request);
                        //UnityEngine.Texture2D NFTImage = ((DownloadHandlerTexture)request.downloadHandler).texture;
                        
                        request.Dispose();
                        //yield return request.result;

                        if (NFT != null)
                            NFT.assets.image_texture = lastGetImage;

                        
                        if ((OnCompleteReturnLinkedNftAction != null && OnCompleteLinkNft != null && lastGetImage != null))
                        {
                            OnCompleteLinkNft.assets.image_texture = lastGetImage;
                            OnCompleteReturnLinkedNftAction.Invoke(OnCompleteLinkNft);
                        }
                        if(OnCompleteAction!=null && lastGetImage!=null)
                            OnCompleteAction.Invoke(lastGetImage);
                        
                        if(afterSuccess!=null)
                            afterSuccess.Invoke();
                    }
                    if (assetsDownloaders == 0)
                    {
                        if (OnAllAssetsDownloadEnded != null)
                            OnAllAssetsDownloadEnded("assetsDownloaders Ended");
                    }

                    End();

                }
            }

            public void End()
            {
                assetsDownloaders--;
                if(request != null)
                    request.Dispose();
                StopAllCoroutines();
                if(_destroyAtEnd)
                    Destroy (this.gameObject);
            }
              
        }
    }
   
}