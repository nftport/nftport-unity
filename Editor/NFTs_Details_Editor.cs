using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(NFT_Details))]
    public class NFTs_Details_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            NFT_Details myScript = (NFT_Details)target;
            
            
            Texture banner = Resources.Load<Texture>("c_nftdata_details");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("GET NFT Details", GUILayout.Height(45)))
            {
                PortUser.SetFromEditorWin();
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_NFTDetails);
            DrawDefaultInspector();
        }
    }
}

