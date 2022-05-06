namespace NFTPort.Samples.PlayerConnect
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using NFTPort;

    public class PlayerConnect_Sample : MonoBehaviour
    {
        public Text addressText;

        void Start()
        {
            ConnectPlayerWallet
                .Initialize()
                .OnComplete(address => UpdateUI(address));
        }

        void UpdateUI(string address)
        {
            addressText.text = address;
            
            //This value can also be accessed from anywhere globally via Port.
            addressText.text = Port.ConnectedPlayerAddress;
        }
        
    }
}