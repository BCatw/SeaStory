using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScriptData))]
public class DialogueDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ScriptData dialogueData = (ScriptData)target;

        if (GUILayout.Button("Load Dialogue"))
        {
            dialogueData.OnLoadData();
        }
    }
}
