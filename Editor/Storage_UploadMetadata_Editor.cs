using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;

    [CustomEditor(typeof(Storage_UploadMetadata))]
    public class Storage_UploadMetadata_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Storage_UploadMetadata myScript = (Storage_UploadMetadata)target;
            
            Texture banner = Resources.Load<Texture>("c_s_Metadata");
            GUILayout.Box(banner);
            
            if (GUILayout.Button("Upload Metadata to IPFS", GUILayout.Height(45)))
            {
                myScript.Run();
            }
            
            if(GUILayout.Button("Stop Upload", GUILayout.Height(25)))
                myScript.Stop(false);
            
            if(GUILayout.Button("Save File Locally", GUILayout.Height(25)))
                myScript.SaveFileasJson(myScript.metadata, myScript.saveToPath, myScript.fileName);

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL("https://docs.nftport.xyz/docs/nftport/ZG9jOjUzMzQxMzcy-examples");
            DrawDefaultInspector();
        }
    }
}

