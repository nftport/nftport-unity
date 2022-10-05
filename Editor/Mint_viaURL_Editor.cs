using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Mint_URL))]
    public class Mint_viaURL_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Mint_URL myScript = (Mint_URL)target;
            
            
            Texture banner = Resources.Load<Texture>("c_pminteasyURL");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("MINT", GUILayout.Height(45)))
            {
                PortUser.SetFromEditorWin();
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_Mint_URL);
            DrawDefaultInspector();
        }
    }
}

