using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
//test1
namespace Khora.FSM
{
    [CustomEditor(typeof(StateMachineBehaviour))]
    public class StateMachineBehaviourEditor : UnityEditor.Editor
    {
        private SerializedProperty statesProp;
        private SerializedProperty initialStateIndexProp;
        private ReorderableList statesList;
        void OnEnable()
        {
            statesProp = serializedObject.FindProperty("States");
            initialStateIndexProp = serializedObject.FindProperty("initialStateIndex");
            statesList = new ReorderableList(
                serializedObject, statesProp, true, true, true, true
            );
            statesList.drawHeaderCallback = rect =>
                EditorGUI.LabelField(rect, "States (index):");
            // Use this to tell Unity how tall each item should be
            statesList.elementHeightCallback = index =>
            {
                var element = statesProp.GetArrayElementAtIndex(index);
                return EditorGUI.GetPropertyHeight(element, true) + 8;
            };
            statesList.drawElementCallback = (rect, index, active, focused) =>
            {
                var element = statesProp.GetArrayElementAtIndex(index);
                rect.y += 4;
                float height = EditorGUI.GetPropertyHeight(element, true);
                // Draw the index label
                var idxRect = new Rect(rect.x, rect.y, 30, EditorGUIUtility.singleLineHeight);
                EditorGUI.LabelField(idxRect, index.ToString(), EditorStyles.boldLabel);
                // Draw the StateData property field next to the label
                var fieldRect = new Rect(rect.x + 35, rect.y, rect.width - 35, height);
                EditorGUI.PropertyField(fieldRect, element, includeChildren: true);
            };
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(
                initialStateIndexProp,
                new GUIContent($"Initial State Index (0 – {statesProp.arraySize - 1})")
            );
            statesList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
