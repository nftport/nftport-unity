using System.IO;
using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Storage_UploadFile))]
    public class Storage_UploadFile_Editor : Editor
    {
        private Storage_UploadFile myScript;
        public override void OnInspectorGUI()
        {
            myScript = (Storage_UploadFile)target;
            
            Texture banner = Resources.Load<Texture>("c_s_file");
            GUILayout.Box(banner);
            
            if (GUILayout.Button("Upload File to IPFS", GUILayout.Height(45)))
            {
                PortUser.SetFromEditorWin();
                myScript.Run();
            }
            
            if(GUILayout.Button("Stop Upload", GUILayout.Height(25)))
                myScript.Stop(false);
            
            
            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_StorageFile);

            if (GUILayout.Button("Select File", GUILayout.Height(35)))
            {
                OpenFile();
            }

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
            myScript.filePath = path;
        }
        
    }
}

