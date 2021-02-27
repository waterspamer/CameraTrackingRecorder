namespace Antilatency.UnityPrototypingTools {

    using UnityEngine;
    using UnityEditor;
    using System.Collections;

    [CustomPropertyDrawer(typeof(GrayedOutFieldAttribute))]
    public class GrayedOutFieldDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            // https://forum.unity.com/threads/read-only-fields.68976/#post-2729947

            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}
