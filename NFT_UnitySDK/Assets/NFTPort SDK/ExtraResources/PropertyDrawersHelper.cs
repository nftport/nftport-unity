using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//// from https://gist.github.com/ProGM/9cb9ae1f7c8c2a4bd3873e4df14a6687
public static class PropertyDrawersHelper {
#if UNITY_EDITOR

    public static string[] AllSceneNames()
    {
        var temp = new List<string>();
        foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
                string name = S.path.Substring(S.path.LastIndexOf('/')+1);
                name = name.Substring(0,name.Length-6);
                temp.Add(name);
            }
        }
        return temp.ToArray();
    }

#endif
}