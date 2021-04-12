using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{

    public class CustomDrawObjectRender : UnityEngine.Rendering.Universal.ScriptableRenderer
    {
        //DrawObjectsPass m_RenderOpaqueForwardPass;
        //StencilState m_DefaultStencilState;
        private CustomDrawObjectData m_CustomRenderData = null;

        StencilState m_DefaultStencilState;

        DrawObjectsPass m_DrawObjectPass;
        RenderTargetHandle m_DepthRti;

        public RenderTargetHandle DepthRti { get => m_DepthRti; }

        public CustomDrawObjectRender(CustomDrawObjectData data) : base(data)
        {
            m_CustomRenderData = data;

            StencilStateData stencilData = data.defaultStencilState;
            m_DefaultStencilState = StencilState.defaultValue;
            m_DefaultStencilState.enabled = stencilData.overrideStencilState;
            m_DefaultStencilState.SetCompareFunction(stencilData.stencilCompareFunction);
            m_DefaultStencilState.SetPassOperation(stencilData.passOperation);
            m_DefaultStencilState.SetFailOperation(stencilData.failOperation);
            m_DefaultStencilState.SetZFailOperation(stencilData.zFailOperation);

            m_DrawObjectPass = new DrawObjectsPass("CustomDrawObject", true, RenderPassEvent.BeforeRenderingOpaques, RenderQueueRange.opaque, data.opaqueLayerMask, m_DefaultStencilState, stencilData.stencilReference);

        }

        public override void Setup(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            //Camera camera = renderingData.cameraData.camera;
            //ref CameraData cameraData = ref renderingData.cameraData;
            //RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;

            EnqueuePass(m_DrawObjectPass);

            for (int i = 0; i < rendererFeatures.Count; ++i)
            {
                if (rendererFeatures[i].isActive)
                    rendererFeatures[i].AddRenderPasses(this, ref renderingData);
            }
        }
    }
}