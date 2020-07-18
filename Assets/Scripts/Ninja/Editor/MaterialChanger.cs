using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NinjaController))]
public class MaterialChanger : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NinjaController control = (NinjaController)target;

        if (GUILayout.Button("Change Material"))
        {
            control.ChangeMaterial();
        }
    }
}
