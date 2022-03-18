using UnityEditor;

namespace Kovu.EventSystems
{
    [CustomEditor(typeof(ButtonMask))]
    public class ButtonMaskEditor : Editor
    {
        private SerializedProperty value;

        private void OnEnable()
        {
            value = serializedObject.FindProperty("value");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var options = new string[] { "Left", "Right", "Middle" };
            value.intValue = EditorGUILayout.MaskField(value.displayName, value.intValue, options);

            serializedObject.ApplyModifiedProperties();
        }
    }
}