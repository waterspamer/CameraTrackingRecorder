using System;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

namespace TrendMe.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CsvToAnimHelper))]
    public class CsvToAnimHelperEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var myTarget = (CsvToAnimHelper)target;
            if (GUILayout.Button("Generate .anim file !"))
            {
                myTarget.GenerateAnimFile();
            }
        }
    }
}
