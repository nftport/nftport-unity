using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(NFTs_OfACollection))]
    public class NFTs_OfContract_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            NFTs_OfACollection myScript = (NFTs_OfACollection)target;
            
            
            Texture banner = Resources.Load<Texture>("c_nftdata_contract");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("GET NFTs of Contract", GUILayout.Height(45)))
            {
                PortUser.SetFromEditorWin();
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.NFTs_OfContract);
            DrawDefaultInspector();
        }
    }
}

