using System.Collections.Generic;
using UnityEngine;
using TriggerFlow.Runtime;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace TriggerFlow.Example.Constraints
{
    public class TagConstraint : TriggerConstraint
    {
        [TagSelector] public List<string> allowedTags = new();

        public override bool OnCheck(Collider other)
        {
            if (allowedTags != null && allowedTags.Count > 0)
            {
                return allowedTags.Contains(other.tag);
            }
            
            return false;
        }
    }

    public class TagSelectorAttribute : PropertyAttribute { }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(TagSelectorAttribute))]
    public class TagSelectorDrawer : PropertyDrawer
    {
        private ReorderableList m_list;

        private void EnsureList(SerializedProperty property)
        {
            if (m_list == null)
            {
                m_list = new ReorderableList(property.serializedObject, property, true, true, true, true);
                m_list.drawHeaderCallback = rect => EditorGUI.LabelField(rect, property.displayName);
                m_list.drawElementCallback = (rect, index, active, focused) =>
                {
                    var element = m_list.serializedProperty.GetArrayElementAtIndex(index);
                    string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
                    int currentIndex = System.Array.IndexOf(tags, element.stringValue);
                    if (currentIndex < 0) currentIndex = 0;
                    var dropdownRect = new Rect(rect.x, rect.y + 2, rect.width, EditorGUIUtility.singleLineHeight);
                    int selected = EditorGUI.Popup(dropdownRect, currentIndex, tags);
                    element.stringValue = tags[selected];
                };
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                return EditorGUIUtility.singleLineHeight;
            }
            
            if (property.isArray && property.propertyType == SerializedPropertyType.Generic)
            {
                EnsureList(property);
                return m_list.GetHeight();
            }

            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
                int index = System.Array.IndexOf(tags, property.stringValue);
                if (index < 0) index = 0;
                int selected = EditorGUI.Popup(position, label.text, index, tags);
                property.stringValue = tags[selected];
            }
            else if (property.isArray && property.propertyType == SerializedPropertyType.Generic)
            {
                EnsureList(property);
                m_list.DoList(position);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
    }
#endif
}
