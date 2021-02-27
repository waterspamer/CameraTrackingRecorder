using System;
using UnityEditor;
using UnityEngine;

namespace Antilatency.UnityPrototypingTools {

    [CustomPropertyDrawer(typeof(HelpBoxDisplay))]
    public class HelpBoxDisplayDrawer : PropertyDrawer {

        public override bool CanCacheInspectorGUI(SerializedProperty property) {
            return true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {

            // Значения этих параметров взяты, по большому счёту, с потолка, поэтому
            // в некоторых экзотических случаях они, возможно, будут работать не очень
            // хорошо. Подредактируйте их, если в редакторе Unity вдруг кардинальным
            // образом изменится оформление инспектора объектов.
            const float widthMarginWhenIconPresent = 80;
            const float widthMarginWhenIconAbsent = 40;
            const float minHeight = 45;

            if (property.serializedObject.targetObjects.Length > 1)
                return 0f;

            var value = (HelpBoxDisplay)fieldInfo.GetValue(property.serializedObject.targetObject);
            if (value.Message == null)
                return 0f;

            var widthMargin = (value.Icon != HelpBoxDisplay.IconType.None)
                ? widthMarginWhenIconPresent
                : widthMarginWhenIconAbsent;

            var content = new GUIContent(value.Message);
            var style = GUI.skin.GetStyle("helpbox");
            return Mathf.Max(minHeight,
                style.CalcHeight(content, EditorGUIUtility.currentViewWidth - widthMargin));
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (property.serializedObject.targetObjects.Length > 1)
                return;

            var value = (HelpBoxDisplay)fieldInfo.GetValue(property.serializedObject.targetObject);
            if (value.Message != null) {
                // Изначально я хотел сделать так: (MessageType)(int)value.Icon, но потом
                // пришёл к выводу, что в Unity нигде не гарантируется, что эти константы
                // сохранят в будущем те же целочисленные значения.
                var type = (MessageType)Enum.Parse(typeof(MessageType), value.Icon.ToString());

                EditorGUI.HelpBox(position, value.Message, type);
            }
        }
    }
}
