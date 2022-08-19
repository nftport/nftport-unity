using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Txn_Account))]
    public class Txn_Account_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Txn_Account myScript = (Txn_Account)target;
            
            
            Texture banner = Resources.Load<Texture>("c_tx_account");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Get Account NFT Transactions", GUILayout.Height(45)))
            {
                PortUser.SetFromEditorWin();
                myScript.Run();
            }
        

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_Txns_Account);
            DrawDefaultInspector();
        }
    }
}

