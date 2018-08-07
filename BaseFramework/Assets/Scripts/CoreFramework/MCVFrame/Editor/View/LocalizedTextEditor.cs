using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

[CustomEditor(typeof(LocalizedText))]
public class LocalizedTextEditor : Editor
{

    SerializedProperty _LocalizedName;

    void OnEnable()
    {
        _LocalizedName = serializedObject.FindProperty("_LocalizedName");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_LocalizedName);
        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();

    }
}
