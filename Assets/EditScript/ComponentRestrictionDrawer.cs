using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(ComponentRestrictionAttribute))]
public class ComponentRestrictionDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var restriction = (ComponentRestrictionAttribute)attribute;

        if (property.propertyType == SerializedPropertyType.ObjectReference)
        {
            EditorGUI.ObjectField(position, property, restriction.type);
        }
        else
        {
            EditorGUI.PropertyField(position, property);
        }
    }
}
#endif