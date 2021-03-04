using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[CustomEditor(typeof(ExtendedSlider))]
public class ExtendedSliderEditor : SliderEditor
{
    
    SerializedProperty m_OnPointerDown;
    SerializedProperty m_OnPointerUp;


    protected override void OnEnable()
    {
        base.OnEnable();
        m_OnPointerDown = serializedObject.FindProperty("onPointerDown");
        m_OnPointerUp = serializedObject.FindProperty("onPointerUp");
    }

    public override void OnInspectorGUI()
    {
        
        var component = (ExtendedSlider)target;
        base.OnInspectorGUI();


        EditorGUILayout.PropertyField(m_OnPointerDown);
        EditorGUILayout.PropertyField(m_OnPointerUp);
        
        serializedObject.ApplyModifiedProperties();
        
    }
}
