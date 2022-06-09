using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Mint_Custom))]
    public class Mint_Custom_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Mint_Custom myScript = (Mint_Custom)target;
            
            
            Texture banner = Resources.Load<Texture>("c_productmint");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Mint Custom NFT", GUILayout.Height(45)))
            {
                PortUser.SetFromEditorWin();
                myScript.Run();
            }
        

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_Mint_Custom);
            DrawDefaultInspector();
        }
    }
}

