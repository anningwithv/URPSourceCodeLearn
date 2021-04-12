
using UnityEditor;
using UnityEditor.Rendering.Universal;
using UnityEngine;

namespace UnityEngine.Rendering.Universal
{
    [CustomEditor(typeof(CustomDrawObjectData), true)]
    public class CustomDrawObjectDataEditor : ScriptableRendererDataEditor
    {
        private static class Styles
        {
            public static readonly GUIContent stencilState = new GUIContent("StencilState", "");
            public static readonly GUIContent opaqueLayerMask = new GUIContent("OpaqueLayerMask", "");
            //public static readonly GUIContent defaultStencilStateLabel = EditorGUIUtility.TrTextContent("Default Stencil State", "Configure the stencil state for the opaque and transparent render passes.");
        }

        SerializedProperty m_OpaqueLayerMask;
        SerializedProperty m_StencilState;

        private void OnEnable()
        {
            m_OpaqueLayerMask = serializedObject.FindProperty("m_OpaqueLayerMask");
            m_StencilState = serializedObject.FindProperty("m_DefaultStencilState");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(m_OpaqueLayerMask, Styles.opaqueLayerMask);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(m_StencilState, Styles.stencilState);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
    }
}
