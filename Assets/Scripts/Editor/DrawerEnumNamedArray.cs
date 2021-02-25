using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnumNamedArrayAttribute))]
public class DrawerEnumNamedArray : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EnumNamedArrayAttribute enumNames = attribute as EnumNamedArrayAttribute;
        //propertyPath returns something like component_hp_max.Array.data[4]
        //so get the index from there
        var subString = property.propertyPath.Substring(property.propertyPath.LastIndexOf("["))
            .Replace("[", "").Replace("]", "");
        int index = System.Convert.ToInt32(subString);
        //change the label
        if (index < enumNames.names.Length)
        {
            label.text = enumNames.names[index];
        }

        //draw field
        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}