using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ButtonExtension))]
public class ButtonExtensionEditor : UnityEditor.UI.ButtonEditor
{
    public override void OnInspectorGUI()
    {
        
        var component = (ButtonExtension)target;
        
        base.OnInspectorGUI();

        component.circle = (GameObject)EditorGUILayout.ObjectField("Circle Game Object", 
            component.circle, typeof(GameObject), true);

    }
}
