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
        private CustomRenderData m_CustomRenderData = null;
        private ScriptableRenderContext m_ScriptableRenderContext;

        DepthOnlyPass m_DepthPrepass;
        RenderTargetHandle m_DepthRti;

        public RenderTargetHandle DepthRti { get => m_DepthRti; }

        public CustomScriptableRender(CustomRenderData data) : base(data)
        {
            m_CustomRenderData = data;
            //m_DefaultStencilState = StencilState.defaultValue;
            //m_DefaultStencilState.enabled = false;

            //m_RenderOpaqueForwardPass = new DrawObjectsPass("CustomRenderOpaque", true,
            //    RenderPassEvent.BeforeRenderingOpaques, RenderQueueRange.opaque, data.opaqueLayerMask, m_DefaultStencilState, 0);

            m_DepthRti.Init("_CustomCameraDepthTexture");

            m_DepthPrepass = new DepthOnlyPass(RenderPassEvent.BeforeRenderingPrepasses, RenderQueueRange.opaque, data.opaqueLayerMask);

        }

        public override void Setup(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            m_ScriptableRenderContext = context;

            Camera camera = renderingData.cameraData.camera;
            ref CameraData cameraData = ref renderingData.cameraData;
            RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;

            //EnqueuePass(m_RenderOpaqueForwardPass);

            m_DepthPrepass.Setup(cameraTargetDescriptor, m_DepthRti);
            EnqueuePass(m_DepthPrepass);

            for (int i = 0; i < rendererFeatures.Count; ++i)
            {
                if (rendererFeatures[i].isActive)
                    rendererFeatures[i].AddRenderPasses(this, ref renderingData);
            }
        }

        public override void FinishRendering(CommandBuffer cmd)
        {
            base.FinishRendering(cmd);

            //CommandBuffer command = CommandBufferPool.Get("CustomDepthRender1");
            //command.Blit(m_DepthTexture.Identifier(), m_CustomRenderData.OutputTexture);
            //m_ScriptableRenderContext.ExecuteCommandBuffer(command);
            //CommandBufferPool.Release(command);

        }
    }
}