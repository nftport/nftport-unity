using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Storage_UploadMetadata))]
    public class Storage_UploadMetadata_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Storage_UploadMetadata myScript = (Storage_UploadMetadata)target;
            
            
            Texture banner = Resources.Load<Texture>("c_s_Metadata");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Upload Metadata to IPFS", GUILayout.Height(45)))
            {
                PortUser.SetFromEditorWin();
                myScript.Run();
            }
            
            if(GUILayout.Button("Stop Upload", GUILayout.Height(25)))
                myScript.Stop(false);
            
            if(GUILayout.Button("Save File Locally", GUILayout.Height(25)))
                myScript.SaveFile(myScript.saveToPath, myScript.fileName);

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_StorageMetadata);
            DrawDefaultInspector();
        }
    }
}

