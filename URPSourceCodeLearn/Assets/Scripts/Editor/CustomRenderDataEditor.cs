
using UnityEditor;
using UnityEditor.Rendering.Universal;
using UnityEngine;

namespace UnityEngine.Rendering.Universal
{
    [CustomEditor(typeof(CustomRenderData), true)]
    public class CustomRenderDataEditor : ScriptableRendererDataEditor
    {
        private static class Styles
        {
            public static readonly GUIContent FilteringLabel = new GUIContent("Filtering", "Controls filter rendering settings for this renderer.");
            public static readonly GUIContent OpaqueMask = new GUIContent("Opaque Layer Mask", "Controls which opaque layers this renderer draws.");
            public static readonly GUIContent TransparentMask = new GUIContent("Transparent Layer Mask", "Controls which transparent layers this renderer draws.");

        }

        SerializedProperty m_OpaqueLayerMask;
        SerializedProperty m_TransparentLayerMask;


        private void OnEnable()
        {
            m_OpaqueLayerMask = serializedObject.FindProperty("m_OpaqueLayerMask");
            m_TransparentLayerMask = serializedObject.FindProperty("m_TransparentLayerMask");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField(Styles.FilteringLabel, EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(m_OpaqueLayerMask, Styles.OpaqueMask);
            EditorGUILayout.PropertyField(m_TransparentLayerMask, Styles.TransparentMask);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI(); // Draw the base UI, contains ScriptableRenderFeatures list
        }
    }
}
