using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(AssetDownloader))]
    public class AssetDownloader_Editor : Editor
    {

        private AssetDownloader myScript;
        public override void OnInspectorGUI()
        {

            myScript = (AssetDownloader) target;

            //Texture banner = Resources.Load<Texture>("c_pminteasyURL");
            GUILayout.BeginHorizontal();
            //GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            EditorGUILayout.HelpBox("Get Asset Content Type, Download Textures and more. Supports both IPFS and HTTP", MessageType.Info);

            if (GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_AssetDownloader);
            
            EditorGUILayout.LabelField("");
            
            DrawDefaultInspector();
        }
    }
}