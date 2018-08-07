using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

//[CustomEditor(typeof(USBToggle))]
public class USBToggleEditor : Editor
{

    SerializedProperty _Mark;

    void OnEnable()
    {
        _Mark = serializedObject.FindProperty("_Mark");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_Mark);
        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();

    }
}
