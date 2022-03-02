using System.Collections;
using System.Collections.Generic;
using NFTPort;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NFTPort_Interface))]
public class NFTPortInterface_editor : Editor
{
    public override void OnInspectorGUI()
    {
        Texture banner = Resources.Load<Texture>("logo");
        GUILayout.Box(banner, GUILayout.Width(300), GUILayout.Height(30));
    }
}
