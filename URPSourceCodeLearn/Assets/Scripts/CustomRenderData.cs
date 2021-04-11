using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Rendering.Universal
{
    public class CustomRenderData : ScriptableRendererData
    {
        [SerializeField] LayerMask m_OpaqueLayerMask = -1;
        [SerializeField] LayerMask m_TransparentLayerMask = -1;

        public LayerMask opaqueLayerMask
        {
            get => m_OpaqueLayerMask;
            set
            {
                SetDirty();
                m_OpaqueLayerMask = value;
            }
        }

#if UNITY_EDITOR
        internal class CreateCustomRendererAsset : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var instance = CreateInstance<CustomRenderData>();
                AssetDatabase.CreateAsset(instance, pathName);
                ResourceReloader.ReloadAllNullIn(instance, UniversalRenderPipelineAsset.packagePath);
                Selection.activeObject = instance;
            }
        }

        [MenuItem("Assets/Create/Rendering/Universal Render Pipeline/Custome Renderer")]
        static void CreateCustomRendererData()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, CreateInstance<CreateCustomRendererAsset>(), "CustomForwardRendererData.asset", null, null);
        }
#endif

        protected override UnityEngine.Rendering.Universal.ScriptableRenderer Create()
        {
            return new CustomScriptableRender(this);
        }
    }
}