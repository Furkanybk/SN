using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MaterialSystem))]
public class MaterialChanger : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MaterialSystem control = (MaterialSystem)target;

        if (GUILayout.Button("Change Material"))
        {
            control.ChangeMaterial();
        }
    }
}
