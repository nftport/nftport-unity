using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace NFTPort  
{ using Internal;
    
    /// <summary>
    /// NFTs of a contract / collections
    /// </summary>
    [AddComponentMenu(PortConstants.BaseComponentMenu+PortConstants.FeatureName_NFTs_OfContract)]
    [ExecuteAlways]
    [HelpURL(PortConstants.NFTs_OfContract)]
    public class NFTs_OfACollection : MonoBehaviour
    {
        /// <summary>
        /// Currently Supported chains for this endpoint.
        /// </summary>
        public enum Chains
        {
            ethereum,
            polygon,
            goerli,
            solana
        }
        
        public enum Includes
        {
            Default,
            metadata, 
            all
        }
        
        #region Parameter Defines

            [SerializeField]
            private Chains chain = Chains.ethereum;
            
            [FormerlySerializedAs("contract_address")] [SerializeField]
            [Tooltip("Also known as contract_address")]
            private string collection = "Input Contract/Collection Address To Fetch NFT's from";

            [Header("Optional:")]
            
            [Header("Include optional data in the response.")]
            [Tooltip("default is the default response, metadata includes NFT metadata and cached_file_url, and all includes extra information like file_information and mint_date in Retrieve NFT details.")]
            [SerializeField]
            Includes include = Includes.all;

            [Tooltip(
                "One API call might not be able to provide all NFTs in one go if user holds a lot of NFTs and not filtered, this string is passed in API call which can be used in next one to continue from last query")]
            public int page_number = 0;

            
            private string RequestUriInit = "https://api.nftport.xyz/v0/nfts/";
            private string WEB_URL;
            private string _apiKey;
            private bool destroyAtEnd = false;


            private UnityAction<string> OnErrorAction;
            private UnityAction<NFTs_model> OnCompleteAction;
            
            [Space(20)]
            //[Header("Called After Successful API call")]
            public UnityEvent afterSuccess;
            //[Header("Called After Error API call")]
            public UnityEvent afterError;

            [Header("Run Component when this Game Object is Set Active")]
            [SerializeField] private bool onEnable = false;
            public bool debugErrorLog = true;
            public bool debugLogRawApiResponse = false;
            
            [Header("Gets filled with data and can be referenced:")]
            public NFTs_model NFTs;

        #endregion


        private void Awake()
        {
            PortUser.Initialise();
            _apiKey = PortUser.GetUserApiKey();
            
        }

        private void OnEnable()
        {
            if (onEnable & Application.isPlaying)
            {
                PortUser.SetFromOnEnable();
                Run();
            }
        }

        #region SetParams and Chain Functions

        /// <summary>
        /// Initialize creates a gameobject and assings this script as a component. This must be called if you are not refrencing the script any other way and it doesn't already exists in the scene.
        /// </summary>
        /// <param name="destroyAtEnd"> Optional bool parameter can set to false to avoid Spawned GameObject being destroyed after the Api process is complete. </param>
        public static NFTs_OfACollection Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject("NFTs Of a Contract").AddComponent<NFTs_OfACollection>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = false;
                _this.debugErrorLog = false;
                return _this;
            }
            
            /// <summary>
            /// Set Parameters to retrieve NFTs from as string
            /// </summary>
            /// <param name="collection"> as string.</param>
            /// <param name="include"> Choose from available 'Includes' enum </param>
            public NFTs_OfACollection SetParameters(string collection = null, Includes include = Includes.all )
            {
                if (this.include != include)
                    this.include = include;
                if(collection != null)
                    this.collection = collection;
                return this;
            }
            
            /// <summary>
            /// Blockchain from which to query NFTs.
            /// </summary>
            /// <param name="chain"> Choose from available 'Chains' enum</param>
            public NFTs_OfACollection SetChain(Chains chain)
            {
                this.chain = chain;
                return this;
            }

            /// <summary>
            /// Action on succesfull API Fetch.
            /// </summary>
            /// <param name="NFTs_OwnedByAnAccount_model.Root"> Use: .OnComplete(NFTs=> NFTsOfUser = NFTs) , where NFTsOfUser = NFTs_OwnedByAnAccount_model.Root;</param>
            /// <returns> NFTs_OwnedByAnAccount_model.Root </returns>
            public NFTs_OfACollection OnComplete(UnityAction<NFTs_model> action)
            {
                this.OnCompleteAction = action;
                return this;
            }
            
                 
            /// <summary>
            /// Set Continuation
            /// </summary>
            ///<param name="continuation"> page number as int.</param>
            public NFTs_OfACollection SetContinuation(int continuation)
            {
                this.page_number = continuation;
                return this;
            }
            
            /// <summary>
            /// Action on Error
            /// </summary>
            /// <param name="UnityAction action"> string.</param>
            /// <returns> Information on Error as string text.</returns>
            public NFTs_OfACollection OnError(UnityAction<string> action)
            {
                this.OnErrorAction = action;
                return this;
            }
            
        #endregion

        
        #region Run - API
            /// <summary>
            /// Runs the Api call and fills the corresponding model in the component on success.
            /// </summary>
            public NFTs_model Run()
            {
                WEB_URL = BuildUrl();
                StopAllCoroutines();
                StartCoroutine(CallAPIProcess());
                return NFTs;
            }

            string BuildUrl()
            {
                if (chain == Chains.solana)
                {
                    WEB_URL = "https://api.nftport.xyz/v0/solana/nfts/" + collection;
                    if (page_number != 0)
                    {
                        WEB_URL = WEB_URL + "?page_number=" + page_number.ToString() + "&include=" + include.ToString().ToLower();;
                    }
                    else
                    {
                        WEB_URL += "?include=" + include.ToString().ToLower();
                    }
                }
                else
                {
                    WEB_URL = RequestUriInit + collection + "?chain=" + chain.ToString().ToLower();
                    if (page_number != 0)
                    {
                        WEB_URL = WEB_URL + "&page_number=" + page_number.ToString();
                    }
                    WEB_URL = WEB_URL + "&include=" + include.ToString().ToLower();
                }
                return WEB_URL;
            }
            
            IEnumerator CallAPIProcess()
            {
                //Make request
                UnityWebRequest request = UnityWebRequest.Get(WEB_URL);
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("source", PortUser.GetSource());
                request.SetRequestHeader("Authorization", _apiKey);
                

                {
                    yield return request.SendWebRequest();
                    string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
                    
                    if(debugLogRawApiResponse)
                        Debug.Log(jsonResult);

                    if (request.error != null)
                    {
                        if(OnErrorAction!=null)
                            OnErrorAction($"Null data. Response code: {request.responseCode}. Result {jsonResult}");
                        if(debugErrorLog)
                            Debug.Log($"Null data. Response code: {request.responseCode}. Result {jsonResult}");
                        if(afterError!=null)
                            afterError.Invoke();
                        //yield break;
                    }
                    else
                    {
                        //Fill Data Model from recieved class
                        NFTs = JsonConvert.DeserializeObject<NFTs_model>(
                            jsonResult,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                MissingMemberHandling = MissingMemberHandling.Ignore,
                                Error = (sender, error) => error.ErrorContext.Handled = true
                            });
                        
                        if(OnCompleteAction!=null)
                            OnCompleteAction.Invoke(NFTs);
                        
                        if(afterSuccess!=null)
                            afterSuccess.Invoke();
                        
                        if(debugErrorLog)
                            Debug.Log($" ´ ▽ ` )ﾉ Success , view NFTs model" );
                    }
                }
                request.Dispose();
                if(destroyAtEnd)
                    Destroy (this.gameObject);
            }
            
        #endregion
    }

}
