using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Transfer_NFT))]
    public class Transfer_NFT_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Transfer_NFT myScript = (Transfer_NFT)target;
            
            
            Texture banner = Resources.Load<Texture>("c_transferNFT");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Transfer NFT", GUILayout.Height(45)))
            {
                PortUser.SetFromEditorWin();
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_Transfer_NFT);
            
            EditorGUILayout.HelpBox("Transferring is possible only if the token is owned by the contract owner and the token has not been transferred/sold yet.", MessageType.Info);

            
            DrawDefaultInspector();
        }
    }
}

