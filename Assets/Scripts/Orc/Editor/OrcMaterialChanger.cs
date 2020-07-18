using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OrcController))]
public class OrcMaterialChanger : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        OrcController control = (OrcController)target;

        if (GUILayout.Button("Change Material"))
        {
            control.ChangeMaterial();
        }
    }
}
