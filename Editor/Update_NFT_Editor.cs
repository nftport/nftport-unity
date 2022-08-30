using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Update_NFT))]
    public class Update_NFT_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Update_NFT myScript = (Update_NFT)target;
            
            
            Texture banner = Resources.Load<Texture>("c_UPDATEtmint");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Update NFT", GUILayout.Height(45)))
            {
                PortUser.SetFromEditorWin();
                myScript.Run();
            }
        

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_Update_NFT);
            DrawDefaultInspector();
        }
    }
}

