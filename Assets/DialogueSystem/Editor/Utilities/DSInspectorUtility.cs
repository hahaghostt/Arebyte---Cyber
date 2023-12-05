using UnityEditor;
using UnityEngine;

namespace DS.Utilities
{
    public static class DSInspectorUtility
    {
        public static void DrawDisabledFieldsAround(System.Action action)
        {
            EditorGUI.BeginDisabledGroup(true);

            action.Invoke();

            EditorGUI.EndDisabledGroup();
        }

        public static void DrawHeader(string label, GUIStyle style = null)
        {
            if (style == null) { style = EditorStyles.boldLabel; }
            EditorGUILayout.LabelField(label, style);
        }

        public static void DrawPropertyField(this SerializedProperty serializedProperty)
        {
            EditorGUILayout.PropertyField(serializedProperty);
        }

        public static int DrawPopup(string label, SerializedProperty selectedIndexProperty, string[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndexProperty.intValue, options);
        }

        public static int DrawPopup(string label, int selectedIndex, string[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, options);
        }
    }
}
