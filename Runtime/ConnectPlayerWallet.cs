
using System;

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
        private UnityAction<string> OnCompleteAction;

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
        public ConnectPlayerWallet OnComplete(UnityAction<string> action)
        {
            this.OnCompleteAction = action;
            return this;
        }
        
        void Awake()
        {
            connectedWalletAddress = null;
            this.gameObject.name = "PlayerConnect_NFTPort";
        }
        
        //called from index - For WebGL
        public void WebHook_GetAddress(string recievedaddress)
        {
            connectedWalletAddress = recievedaddress;
            Port.ConnectedPlayerAddress = connectedWalletAddress;
            
            if(OnCompleteAction!=null)
                OnCompleteAction.Invoke(connectedWalletAddress);
                        
            if(afterSuccess!=null)
                afterSuccess.Invoke();
        }
    }
}