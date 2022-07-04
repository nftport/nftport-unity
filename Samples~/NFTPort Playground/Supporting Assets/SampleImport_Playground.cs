using System.Collections;
using System.Collections.Generic;
using System.IO;
using NFTPort.Internal;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
namespace NFTPort.Editor
{
    using Internal;
    //_sz_
    public static class SampleImport_Playground
    {
        private static bool notFirstLoad;

        private static NFTPort.Editor.Readme_NFTPort _readme;
        [InitializeOnLoadMethod]
        static void Recompile()
        {
            if(notFirstLoad)
                return;

            _readme = AssetDatabase.LoadAssetAtPath<Readme_NFTPort>(ReadMePath());

            if (!_readme.notFirstload)
                FirstLoad();
        }     

        static void FirstLoad()
        {
            SampleDependencyCheck();
            notFirstLoad = true;
            
            HighLightReadmeAsset(ReadMePath());

        }

        static void SampleDependencyCheck()
        {
            InstallPortDependencies.OnListCheckCompleteForGLTF(arg0 => GLTFDependencyAction(arg0));
            InstallPortDependencies.CheckPkgsListForGLTFt();
            
            InstallPortDependencies.OnListCheckCompleteForInputSys((arg0 => GLTFDependencyAction(arg0)));
            InstallPortDependencies.CheckPkgsListForInputsyst();
        }

        static void GLTFDependencyAction(bool exists)
        {
            if (!exists)
            {
                Debug.Log("This Sample needs GLTF Utility as it uses .glb models which makes the 3D NFT models compatible to show at browser and at marketplaces like opensea, and Unity's New Input System. Please install it via NFTPort/Install Dependencies");
                InstallPortDependencies.ShowWindow();
            }
            
             HighLightReadmeAsset(ReadMePath());
            _readme.notFirstload = true;
        }

        static void HighLightReadmeAsset(string path)
        {
            Selection.activeObject=AssetDatabase.LoadMainAssetAtPath(path);
            Debug.Log("View Read Me in samples folder");
        }

        static string ReadMePath()
        {
            string[] res = System.IO.Directory.GetFiles(Application.dataPath, "SampleImport_Playground.cs", SearchOption.AllDirectories);
            if (res.Length == 0)
            {
                //Debug.LogError("error message ....");
                return null;
            }
            string readmepath = "Assets" + res[0].Replace("\\", "/").Replace("Supporting Assets/SampleImport_Playground.cs", "").Replace(Application.dataPath,"") + "Readme.asset";
            return readmepath;
        }
        
    }

}
#endif
