
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
        public string connectedNetworkID;
        
        [HideInInspector] public string MockconnectedWalletAddress;
        [HideInInspector] public string MockconnectedNetworkID;
        
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
            connectedWalletAddress = MockconnectedWalletAddress;
            connectedNetworkID = MockconnectedNetworkID;
            
            Port.ConnectedPlayerAddress = connectedWalletAddress;
            Port.ConnectedPlayerNetworkID = connectedNetworkID;
            Debug.Log("Editor Mock Wallet Connected , Address: " + Port.ConnectedPlayerAddress + " at Network ID:" + Port.ConnectedPlayerNetworkID +" | Access it via Port.ConnectedPlayerAddress");
            GetAddressSuccess();
#endif
#if !UNITY_EDITOR
            SendCallTo_GetAddress();
#endif
        }
        
        void Awake()
        {
            
#if UNITY_EDITOR
            
            
            Port.ConnectedPlayerAddress = connectedWalletAddress;
            Port.ConnectedPlayerNetworkID = connectedNetworkID;
#endif
          
            this.gameObject.name = "PlayerConnect_NFTPort";
        }


        //called from index - For WebGL
        public void WebHook_GetNetworkID(string networkID)
        {
            Port.ConnectedPlayerNetworkID = networkID;
            connectedNetworkID = networkID;
        }
        
        //called from index - For WebGL
        public void WebHook_GetAddress(string recievedaddress)
        {
            connectedWalletAddress = recievedaddress;
            Port.ConnectedPlayerAddress = connectedWalletAddress;
            GetAddressSuccess();
        }

        void GetAddressSuccess()
        {
            if(OnCompleteAction!=null)
                OnCompleteAction.Invoke(connectedWalletAddress,Port.ConnectedPlayerNetworkID );
                        
            if(afterSuccess!=null)
                afterSuccess.Invoke();
        }
      
    }
}