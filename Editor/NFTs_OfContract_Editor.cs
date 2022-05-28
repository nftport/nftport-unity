using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(NFTs_OfAContract))]
    public class NFTs_OfContract_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            NFTs_OfAContract myScript = (NFTs_OfAContract)target;
            
            
            Texture banner = Resources.Load<Texture>("c_nftdata_contract");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("GET NFTs of Contract", GUILayout.Height(45)))
            {
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.NFTs_OfContract);
            DrawDefaultInspector();
        }
    }
}

