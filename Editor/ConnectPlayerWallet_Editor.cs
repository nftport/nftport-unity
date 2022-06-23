using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(ConnectPlayerWallet))]
    public class ConnectPlayerWallet_Editor : Editor
    {
        public string mockWallet = "0x3691Ca2c8D2051f0B8b9d4aCb8941771aBc1bf9b";
        public string mockNetworkID = "1";

        private ConnectPlayerWallet myScript;
        public override void OnInspectorGUI()
        {

            myScript = (ConnectPlayerWallet) target;
            SetMocks();

            //Texture banner = Resources.Load<Texture>("c_pminteasyURL");
            GUILayout.BeginHorizontal();
            //GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_PlayerWalletConnect);
            
            EditorGUILayout.HelpBox("This feature currently only supports WebGL build with MetaMask wallet on EVM network", MessageType.Warning);
            EditorGUILayout.LabelField("");

            GuiLine();

            EditorGUILayout.LabelField("");
            if (GUILayout.Button("Mock Wallet Connect", GUILayout.Height(25)))
            {
                SetMocks();
                myScript.WebSend_GetAddress();
            }
                
            
            EditorGUILayout.HelpBox("Input a wallet address below to mock a connected wallet in editor at 'Port.ConnectedPlayerAddress'", MessageType.Info);
            
            mockWallet = EditorGUILayout.TextField("Mock Wallet", mockWallet);
            mockNetworkID = EditorGUILayout.TextField("Mock NetworkID", mockNetworkID);

            EditorGUILayout.LabelField("");
            
            GuiLine();
            
            EditorGUILayout.LabelField("");
            
            DrawDefaultInspector();
        }

        void SetMocks()
        {
            myScript.MockconnectedWalletAddress = mockWallet;
            myScript.MockconnectedNetworkID = mockNetworkID;
        }
        
        static void GuiLine( int i_height = 1 )
        {
            Rect rect = EditorGUILayout.GetControlRect(false, i_height );
            rect.height = i_height;
            EditorGUI.DrawRect(rect, new UnityEngine.Color ( 0.5f,0.5f,0.5f, 1 ) );
        }
    }
}