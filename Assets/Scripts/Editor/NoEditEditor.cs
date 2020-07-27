using UnityEditor;
using UnityEngine;

namespace SejDev.Editor
{
    [CustomPropertyDrawer(typeof(NoEditAttribute))]
    public class NoEditEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}