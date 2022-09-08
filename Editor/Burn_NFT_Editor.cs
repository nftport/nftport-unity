using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Burn_NFT))]
    public class Burn_NFT_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Burn_NFT myScript = (Burn_NFT)target;
            
            
            Texture banner = Resources.Load<Texture>("c_BurnNft");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("BURN NFT", GUILayout.Height(45)))
            {
                PortUser.SetFromEditorWin();
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_Burn_NFT);
            
            EditorGUILayout.HelpBox("Burning is possible only if the token is owned by the contract owner and the token has not been transferred/sold yet.", MessageType.Warning);

            
            DrawDefaultInspector();
        }
    }
}

