using System;
using System.Linq;
using TriggerFlow.Runtime;
using UnityEditor;
using UnityEngine;

namespace Tools.TriggerFlow.Editor
{
    [CustomEditor(typeof(global::TriggerFlow.Runtime.TriggerFlow))]
    public class TriggerBehaviorEditor : UnityEditor.Editor
    {
        private SerializedProperty m_triggerTypeProp;
        private SerializedProperty m_constraintsProp;
        private SerializedProperty m_actionsProp;

        private static Type[] m_constraintTypes;
        private static Type[] m_actionTypes;

        private int m_constraintAddIndex = -1;
        private int m_actionAddIndex = -1;

        private void OnEnable()
        {
            m_triggerTypeProp = serializedObject.FindProperty("triggerType");
            m_constraintsProp = serializedObject.FindProperty("constraints");
            m_actionsProp = serializedObject.FindProperty("actions");

            // 查找所有 TriggerConstraint 子类
            m_constraintTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => t.IsSubclassOf(typeof(TriggerConstraint)) && !t.IsAbstract)
                .ToArray();

            // 查找所有 TriggerAction 子类
            m_actionTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => t.IsSubclassOf(typeof(TriggerAction)) && !t.IsAbstract)
                .ToArray();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_triggerTypeProp);

            DrawSerializableReferenceList(m_constraintsProp, m_constraintTypes, ref m_constraintAddIndex, "Constraints");
            DrawSerializableReferenceList(m_actionsProp, m_actionTypes, ref m_actionAddIndex, "Actions");

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawSerializableReferenceList(SerializedProperty listProp, Type[] types, ref int addIndex, string label)
        {
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);

            for (int i = 0; i < listProp.arraySize; i++)
            {
                var elem = listProp.GetArrayElementAtIndex(i);
                if (elem == null) continue;

                EditorGUILayout.BeginVertical("box");

                Type elemType = GetPropertyType(elem);
                string title = elemType != null ? elemType.Name : "Null";

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
                if (GUILayout.Button("Remove", GUILayout.Width(60)))
                {
                    listProp.DeleteArrayElementAtIndex(i);
                    serializedObject.ApplyModifiedProperties();
                    return;
                }
                EditorGUILayout.EndHorizontal();

                if (elem != null)
                {
                    SerializedProperty propCopy = elem.Copy();
                    SerializedProperty endProp = propCopy.GetEndProperty();
                    bool enterChildren = true;

                    EditorGUI.indentLevel++;
                    while (propCopy.NextVisible(enterChildren) && !SerializedProperty.EqualContents(propCopy, endProp))
                    {
                        EditorGUILayout.PropertyField(propCopy, true);
                        enterChildren = false;
                    }
                    EditorGUI.indentLevel--;
                }


                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace(); 
                
                float buttonWidth = Mathf.Max(EditorGUIUtility.currentViewWidth - 100, 60);
                if (GUILayout.Button($"Add {label}", GUILayout.Width(buttonWidth), GUILayout.Height(22)))
                {
                    GenericMenu menu = new GenericMenu();
                    for (int i = 0; i < types.Length; i++)
                    {
                        var type = types[i];
                        menu.AddItem(new GUIContent(type.Name), false, () =>
                        {
                            var instance = Activator.CreateInstance(type);
                            listProp.arraySize++;
                            var newElem = listProp.GetArrayElementAtIndex(listProp.arraySize - 1);
                            newElem.managedReferenceValue = instance;
                            serializedObject.ApplyModifiedProperties();
                        });
                    }
                    menu.ShowAsContext();
                }
                
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
        }

        private Type GetPropertyType(SerializedProperty property)
        {
            var value = property.managedReferenceValue;
            if (value == null) return null;
            return value.GetType();
        }
    }
}
