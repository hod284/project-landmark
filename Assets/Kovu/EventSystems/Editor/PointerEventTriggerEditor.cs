using UnityEngine;
using UnityEditor;
using System;

namespace Kovu.EventSystems
{
    [CustomEditor(typeof(PointerEventTrigger), true)]
    public class PointerEventTriggerEditor : Editor
    {
        private SerializedProperty m_ButtonMask;
        private SerializedProperty m_HandleDoubleClicks;
        private SerializedProperty m_AllowDragWhileClick;
        private SerializedProperty m_DelegatesProperty;
        private GUIContent m_IconToolbarMinus;
        private GUIContent m_EventIDName;
        private GUIContent[] m_EventTypes;
        private GUIContent m_AddButonContent;

        protected virtual void OnEnable()
        {
            m_ButtonMask = serializedObject.FindProperty("m_ButtonMask");
            m_HandleDoubleClicks = serializedObject.FindProperty("enableDoubleClicks");
            m_AllowDragWhileClick = serializedObject.FindProperty("allowDragWhileClick");
            m_DelegatesProperty = serializedObject.FindProperty("m_Delegates");
            m_AddButonContent = new GUIContent("Add New Event Type");
            m_EventIDName = new GUIContent("");
            // Have to create a copy since otherwise the tooltip will be overwritten.
            m_IconToolbarMinus = new GUIContent(EditorGUIUtility.IconContent("Toolbar Minus"));

            string[] eventNames = Enum.GetNames(typeof(PointerEventTriggerType));
            m_EventTypes = new GUIContent[eventNames.Length];
            for (int i = 0; i < eventNames.Length; ++i)
                m_EventTypes[i] = new GUIContent(eventNames[i]);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(m_HandleDoubleClicks);
            EditorGUILayout.PropertyField(m_AllowDragWhileClick);
            EditorGUILayout.PropertyField(m_ButtonMask);

            int toBeRemovedEntry = -1;
            Vector2 removeButtonSize = GUIStyle.none.CalcSize(m_IconToolbarMinus);

            for (int i = 0; i < m_DelegatesProperty.arraySize; ++i)
            {
                SerializedProperty delegateProperty = m_DelegatesProperty.GetArrayElementAtIndex(i);
                SerializedProperty eventProperty = delegateProperty.FindPropertyRelative("eventID");
                SerializedProperty callbacksProperty = delegateProperty.FindPropertyRelative("callback");
                m_EventIDName.text = eventProperty.enumDisplayNames[eventProperty.enumValueIndex];

                EditorGUILayout.PropertyField(callbacksProperty, m_EventIDName);
                Rect callbackRect = GUILayoutUtility.GetLastRect();

                Rect removeButtonPos = new Rect(callbackRect.xMax - removeButtonSize.x - 8, callbackRect.y + 1, removeButtonSize.x, removeButtonSize.y);
                if (GUI.Button(removeButtonPos, m_IconToolbarMinus, GUIStyle.none))
                {
                    toBeRemovedEntry = i;
                }

                EditorGUILayout.Space();
            }

            if (toBeRemovedEntry > -1)
            {
                RemoveEntry(toBeRemovedEntry);
            }

            Rect btPosition = GUILayoutUtility.GetRect(m_AddButonContent, GUI.skin.button);
            const float addButonWidth = 200f;
            btPosition.x = btPosition.x + (btPosition.width - addButonWidth) / 2;
            btPosition.width = addButonWidth;
            if (GUI.Button(btPosition, m_AddButonContent))
            {
                ShowAddTriggermenu();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void RemoveEntry(int index)
        {
            m_DelegatesProperty.DeleteArrayElementAtIndex(index);
        }

        private void OnAddNewSelected(object index)
        {
            Debug.Assert(index is int);
            int selected = (int)index;
            m_DelegatesProperty.arraySize += 1;
            SerializedProperty delegateEntry = m_DelegatesProperty.GetArrayElementAtIndex(m_DelegatesProperty.arraySize - 1);
            SerializedProperty eventProperty = delegateEntry.FindPropertyRelative("eventID");
            eventProperty.enumValueIndex = selected;
            serializedObject.ApplyModifiedProperties();
        }

        private void ShowAddTriggermenu()
        {
            GenericMenu menu = new GenericMenu();
            for (int i = 0; i < m_EventTypes.Length; ++i)
            {
                bool active = true;

                // Check if we already have a Entry for the current eventType, if so, disable it
                for (int p = 0; p < m_DelegatesProperty.arraySize; ++p)
                {
                    SerializedProperty delegateEntry = m_DelegatesProperty.GetArrayElementAtIndex(p);
                    SerializedProperty eventProperty = delegateEntry.FindPropertyRelative("eventID");
                    if (eventProperty.enumValueIndex == i)
                    {
                        active = false;
                        break;
                    }
                }

                if (active)
                    menu.AddItem(m_EventTypes[i], false, OnAddNewSelected, i);
                else
                    menu.AddDisabledItem(m_EventTypes[i]);
            }
            menu.ShowAsContext();
            Event.current.Use();
        }
    }
}