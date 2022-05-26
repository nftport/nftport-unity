using System.Collections;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using NFTPort.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace  NFTPort.Editor
{
    #if UNITY_EDITOR
    public class LatestRel : MonoBehaviour
    {
        public static bool running = false;
        private UnityAction<PkgJson> OnCompleteAction;
        ////â‰§â— â€¿â— â‰¦âœŒ _sz_ Î© //≧◠‿◠≦✌ _sz_ Ω
        public static LatestRel Initialize()
        {
            if (!running)
            {
                running = true;
                var _this = new GameObject("NFTPort initialise delete this gameobject").AddComponent<LatestRel>();
                return _this;
            }
            return null;
        }
        public LatestRel OnComplete(UnityAction<PkgJson> action)
        {
            this.OnCompleteAction = action;
            return this;
        }

        public void Run()
        {
            StopAllCoroutines();
            StartCoroutine(LatestReleaseversion());
            StartCoroutine(ForceEnd());
        }
        IEnumerator ForceEnd()
        {
            yield return new WaitForSeconds(3f);
            End();
        }

        private UnityWebRequest www;
        IEnumerator LatestReleaseversion()
        {
            string WEB_URL = "https://raw.githubusercontent.com/nftport/nftport-unity/com.nftport.nftport/package.json";
            www = UnityWebRequest.Get(WEB_URL);
            yield return www.SendWebRequest();
 
            if (www.result != UnityWebRequest.Result.Success) {
            
            }
            else
            {
                PkgJson pkgJson = JsonConvert.DeserializeObject<PkgJson>(www.downloadHandler.text);
                if(OnCompleteAction!=null)
                    OnCompleteAction.Invoke(pkgJson);
            }
            End();
        }

        void End()
        {
            www.Dispose();
            running = false;
            DestroyImmediate(gameObject);
        }
    }
    #endif
}
