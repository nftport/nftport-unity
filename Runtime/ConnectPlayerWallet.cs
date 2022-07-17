
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
        public string MockconnectedWalletAddress = "0x3691Ca2c8D2051f0B8b9d4aCb8941771aBc1bf9b";
        public string MockconnectedNetworkID = "1";
        [Space(30)]
        public string connectedWalletAddress;
        public string connectedNetworkID;

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
        /// Action when player successfully connects the wallet returning connected address and connected networkID
        /// </summary>
        /// <returns> Player Wallet Address String </returns>
        public ConnectPlayerWallet OnComplete(UnityAction<string,string> action)
        {
            this.OnCompleteAction = action;
            return this;
        }
        
        
        /// <summary>
        /// Use this function on a Button from inside Unity to Connect Account, If a mock wallet is entered It'll be connected only on editor level
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

        /// <summary>
        /// Use this to hook up other wallet connect features to NFTPort wallet connect to access Port.connectedplayeraddress
        /// </summary>
        /// <param name="walletaddress"></param>
        public void ConnectThisToNFTPortWalletConnect(string connectedWalletAddress)
        {
            WebHook_GetAddress(connectedWalletAddress);
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