using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(NFTs_OwnedByAnAccount))]
    public class NFTs_OfAccount_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            NFTs_OwnedByAnAccount myScript = (NFTs_OwnedByAnAccount)target;
            
            
            Texture banner = Resources.Load<Texture>("c_nftdata_account");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("GET NFTs of Account", GUILayout.Height(45)))
            {
                PortUser.SetFromEditorWin();
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.NFTs_OfAccount);
            DrawDefaultInspector();
        }
    }
}

