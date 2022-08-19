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

        private ConnectPlayerWallet myScript;
        public override void OnInspectorGUI()
        {

            myScript = (ConnectPlayerWallet) target;

            //Texture banner = Resources.Load<Texture>("c_pminteasyURL");
            GUILayout.BeginHorizontal();
            //GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_PlayerWalletConnect);
            EditorGUILayout.LabelField("");

            GuiLine();

            EditorGUILayout.LabelField("");
            if (GUILayout.Button("Mock Wallet Connect", GUILayout.Height(25)))
            {
                myScript.WebSend_GetAddress();
            }
                
            
            EditorGUILayout.HelpBox("Input a wallet address below to mock a connected wallet in editor at 'Port.ConnectedPlayerAddress'", MessageType.Info);

            DrawDefaultInspector();
        }

        static void GuiLine( int i_height = 1 )
        {
            Rect rect = EditorGUILayout.GetControlRect(false, i_height );
            rect.height = i_height;
            EditorGUI.DrawRect(rect, new UnityEngine.Color ( 0.5f,0.5f,0.5f, 1 ) );
        }
    }
}