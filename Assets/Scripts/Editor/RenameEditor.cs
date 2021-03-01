using UnityEditor;
using UnityEngine;

namespace SejDev.Editor
{
    [CustomPropertyDrawer(typeof(RenameAttribute))]
    public class RenameEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.text = property.name.Replace("<", string.Empty).Replace(">k__BackingField", string.Empty);
            EditorGUI.PropertyField(position, property, label);
        }
    }
}