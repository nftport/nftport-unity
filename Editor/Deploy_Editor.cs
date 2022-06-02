using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Deploy))]
    public class Deploy_Contract_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Deploy myScript = (Deploy)target;
            
            
            Texture banner = Resources.Load<Texture>("c_productcontract");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Deploy Product Contract", GUILayout.Height(45)))
            {
                myScript.Run();
            }
        

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_DeployContract);
            DrawDefaultInspector();
        }
    }
}

