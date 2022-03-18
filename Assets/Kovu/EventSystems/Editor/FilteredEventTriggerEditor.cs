using UnityEditor;
using UnityEditor.EventSystems;

namespace Kovu.EventSystem
{
    [CustomEditor(typeof(FilteredEventTrigger))]
    public class FilteredEventTriggerEditor : EventTriggerEditor
    {
        private SerializedProperty _filter;
        private SerializedProperty _allowDragWhileClick;
        private SerializedProperty _enableDoubleClick;

        protected override void OnEnable()
        {
            base.OnEnable();
            _allowDragWhileClick = serializedObject.FindProperty("allowDragWhileClick");
            _filter = serializedObject.FindProperty("filter");
            _enableDoubleClick = serializedObject.FindProperty("enableDoubleClick");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_allowDragWhileClick);
            EditorGUILayout.PropertyField(_enableDoubleClick);
            EditorGUILayout.PropertyField(_filter);
            serializedObject.ApplyModifiedProperties();
        }
    }
}