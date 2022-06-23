using System.Collections;
using System.Collections.Generic;
using System.IO;
using NFTPort.Internal;
using UnityEditor;
using UnityEngine;

namespace NFTPort.Editor
{
    using Internal;
    //_sz_
    public static class SampleImport_Playground
    {
        private static bool notFirstLoad;

        private static NFTPort.Editor.Readme _readme;
        [InitializeOnLoadMethod]
        static void Recompile()
        {
            if(notFirstLoad)
                return;

            _readme = AssetDatabase.LoadAssetAtPath<Readme>(ReadMePath());

            if (!_readme.notFirstload)
                FirstLoad();
        }     

        static void FirstLoad()
        {
            SampleDependencyCheck();
            notFirstLoad = true;
            
            HighLightReadmeAsset(ReadMePath());
            _readme.notFirstload = true;

        }

        static void SampleDependencyCheck()
        {
            InstallPortDependencies.OnListCheckCompleteForGLTF(arg0 => GLTFDependencyAction(arg0));
            InstallPortDependencies.CheckPkgsListForGLTFt();
        }

        static void GLTFDependencyAction(bool exists)
        {
            if (!exists)
            {
                InstallPortDependencies.ShowWindow();
            }
        }

        static void HighLightReadmeAsset(string path)
        {
            Selection.activeObject=AssetDatabase.LoadMainAssetAtPath(path);
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
