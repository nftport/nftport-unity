
using System;
using System.Runtime.InteropServices;

namespace NFTPort
{
    using UnityEngine;
    using UnityEngine.Events;
    using Internal;
    
    [AddComponentMenu(PortConstants.BaseComponentMenu + PortConstants.FeatureName_ConnectUserWallet)]
    [HelpURL(PortConstants.Docs_ConnectUserWallet)]
    public class ConnectPlayerWallet : MonoBehaviour
    {
        public string connectedWalletAddress;
        
        public UnityEvent afterSuccess;
        private UnityAction<string,string> OnCompleteAction;

        [DllImport("__Internal")]
        private static extern void SendCallTo_GetAddress();

        
        /// <summary>
        /// Initialize creates a gameobject and assings this script as a component. This must be called if it doesn't already exists in the scene on gameObject named PlayerConnect_NFTPort.
        /// </summary>
        public static ConnectPlayerWallet Initialize()
        {
            var _this = new GameObject("PlayerConnect_NFTPort").AddComponent<ConnectPlayerWallet>();
            return _this;
        }
        
        /// <summary>
        /// Action when player successfully connects the wallet.
        /// </summary>
        /// <returns> Player Wallet Address String </returns>
        public ConnectPlayerWallet OnComplete(UnityAction<string,string> action)
        {
            this.OnCompleteAction = action;
            return this;
        }
        
        
        /// <summary>
        /// Use this function on a Button from inside Unity to Connect Account
        /// </summary>
        public void WebSend_GetAddress()
        {
#if UNITY_EDITOR
            Port.ConnectedPlayerAddress = connectedWalletAddress;
            Debug.Log("Editor Mock Wallet Connect , Address: " + Port.ConnectedPlayerAddress);
#endif
#if !UNITY_EDITOR
            SendCallTo_GetAddress();
#endif
        }
        
        void Awake()
        {
            
#if UNITY_EDITOR
            Port.ConnectedPlayerAddress = connectedWalletAddress;
#endif
          
            this.gameObject.name = "PlayerConnect_NFTPort";
        }


        //called from index - For WebGL
        public void WebHook_GetNetworkID(string networkID)
        {
            Port.ConnectedPlayerNetworkID = networkID;
        }
        
        //called from index - For WebGL
        public void WebHook_GetAddress(string recievedaddress)
        {
            connectedWalletAddress = recievedaddress;
            Port.ConnectedPlayerAddress = connectedWalletAddress;
           
            
            if(OnCompleteAction!=null)
                OnCompleteAction.Invoke(connectedWalletAddress,Port.ConnectedPlayerNetworkID );
                        
            if(afterSuccess!=null)
                afterSuccess.Invoke();
        }
      
    }
}