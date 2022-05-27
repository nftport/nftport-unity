using UnityEngine;

namespace NFTPort.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Product_Mint))]
    public class Product_Mint_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Product_Mint myScript = (Product_Mint)target;
            
            
            Texture banner = Resources.Load<Texture>("c_productmint");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Mint Custom NFT", GUILayout.Height(45)))
            {
                myScript.Run();
            }
        

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_GettingStarted);
            DrawDefaultInspector();
        }
    }
}

