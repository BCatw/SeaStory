using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CGList))]
public class CGListEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CGList cGList = (CGList)target;

        if (GUILayout.Button("Load CG Data"))
        {
            cGList.OnLoadData();
        }
    }
}
