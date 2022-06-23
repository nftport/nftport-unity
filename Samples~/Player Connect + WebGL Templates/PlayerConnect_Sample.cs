namespace NFTPort.Samples.PlayerConnect
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using NFTPort;

    public class PlayerConnect_Sample : MonoBehaviour
    {
        public ConnectPlayerWallet connectPlayerWallet;
        public Text addressText;
        public Text NetworkIDText;

        void Start()
        {
            connectPlayerWallet
                .OnComplete((address, networkID )=> UpdateUI(address,networkID));
        }

        void UpdateUI(string address, string networkID)
        {
            addressText.text = address;
            
            //This value can also be accessed from anywhere globally via Port.
            addressText.text = Port.ConnectedPlayerAddress;
            NetworkIDText.text = Port.ConnectedPlayerNetworkID;
        }
        
    }
}