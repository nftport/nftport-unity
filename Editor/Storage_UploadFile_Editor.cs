using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;

    [CustomEditor(typeof(Storage_UploadFile))]
    public class Storage_UploadFile_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Storage_UploadFile myScript = (Storage_UploadFile)target;
            
            Texture banner = Resources.Load<Texture>("c_s_file");
            GUILayout.Box(banner);
            
            if (GUILayout.Button("Upload File to IPFS", GUILayout.Height(45)))
            {
                myScript.Run();
            }
            
            if(GUILayout.Button("Stop", GUILayout.Height(25)))
                myScript.Stop(false);
            
            
            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL("https://docs.nftport.xyz/docs/nftport/ZG9jOjUzMzQxMzcy-examples");
            DrawDefaultInspector();
        }
    }
}

