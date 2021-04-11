using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{

    public class CustomScriptableRender : UnityEngine.Rendering.Universal.ScriptableRenderer
    {
        //DrawObjectsPass m_RenderOpaqueForwardPass;
        //StencilState m_DefaultStencilState;

        DepthOnlyPass m_DepthPrepass;
        RenderTargetHandle m_DepthTexture;

        public CustomScriptableRender(CustomRenderData data) : base(data)
        {
            //m_DefaultStencilState = StencilState.defaultValue;
            //m_DefaultStencilState.enabled = false;

            //m_RenderOpaqueForwardPass = new DrawObjectsPass("CustomRenderOpaque", true,
            //    RenderPassEvent.BeforeRenderingOpaques, RenderQueueRange.opaque, data.opaqueLayerMask, m_DefaultStencilState, 0);

            m_DepthTexture.Init("_CustomCameraDepthTexture");

            m_DepthPrepass = new DepthOnlyPass(RenderPassEvent.BeforeRenderingPrepasses, RenderQueueRange.opaque, data.opaqueLayerMask);

        }

        public override void Setup(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            Camera camera = renderingData.cameraData.camera;
            ref CameraData cameraData = ref renderingData.cameraData;
            RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;

            //EnqueuePass(m_RenderOpaqueForwardPass);

            m_DepthPrepass.Setup(cameraTargetDescriptor, m_DepthTexture);
            EnqueuePass(m_DepthPrepass);

            for (int i = 0; i < rendererFeatures.Count; ++i)
            {
                if (rendererFeatures[i].isActive)
                    rendererFeatures[i].AddRenderPasses(this, ref renderingData);
            }
        }
    }
}