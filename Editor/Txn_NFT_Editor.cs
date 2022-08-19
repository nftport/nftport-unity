using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Txn_NFT))]
    public class Txn_NFT_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Txn_NFT myScript = (Txn_NFT)target;
            
            
            Texture banner = Resources.Load<Texture>("c_tx_nft");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Get NFT Transactions", GUILayout.Height(45)))
            {
                PortUser.SetFromEditorWin();
                myScript.Run();
            }
        

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_Txns_NFT);
            DrawDefaultInspector();
        }
    }
}

