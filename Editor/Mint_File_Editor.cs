using System.IO;
using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Mint_File))]
    public class Mint_File_Editor : Editor
    {
        private Mint_File myScript;
        public override void OnInspectorGUI()
        {
            myScript = (Mint_File)target;
            
            
            Texture banner = Resources.Load<Texture>("c_pminteasyFile");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("UPLOAD FILE & MINT", GUILayout.Height(45)))
            {
                PortUser.SetFromEditorWin();
                myScript.Run();
            }
            if(GUILayout.Button("Stop Upload", GUILayout.Height(25)))
                myScript.Stop(false);

            if (GUILayout.Button("Select File", GUILayout.Height(35)))
            {
                OpenFile();
            }
            
            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_Mint_File);

            DrawDefaultInspector();
        }
        
        public void OpenFile()
        {
            //Get the path
            var path = EditorUtility.OpenFilePanel("Select File (⌐▨_▨) | NFTPort Upload", "", "*");
            if (string.IsNullOrEmpty(path))
                return;

            //Read
            var reader = new StreamReader(path);
            myScript.SetParameters(FilePath:path);
        }
    }
}

