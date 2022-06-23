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
        public override void OnInspectorGUI()
        {

            ConnectPlayerWallet myScript = (ConnectPlayerWallet) target;


            //Texture banner = Resources.Load<Texture>("c_pminteasyURL");
            GUILayout.BeginHorizontal();
            //GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_PlayerWalletConnect);
            EditorGUILayout.HelpBox("This feature currently only supports WebGL build with MetaMask wallet on EVM network", MessageType.Warning);
            EditorGUILayout.HelpBox("Input a wallet address below to mock a connected wallet in editor at 'Port.ConnectedPlayerAddress'", MessageType.Info);
            DrawDefaultInspector();
        }
    }
}