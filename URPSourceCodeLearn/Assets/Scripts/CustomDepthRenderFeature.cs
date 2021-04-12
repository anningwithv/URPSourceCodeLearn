using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomDepthRenderFeature : ScriptableRendererFeature
{
    [Serializable]
    public class Setting
    {
        public RenderTexture outputTex;
        public RenderPassEvent renderPassEvent;
    }

    public Setting renderSetting = new Setting();

    private CustomDepthRenderPass m_DepthRenderPass;

    public override void Create()
    {
        m_DepthRenderPass = new CustomDepthRenderPass(this);

        // Configures where the render pass should be injected.
        m_DepthRenderPass.renderPassEvent = renderSetting.renderPassEvent;
    }

    public override void AddRenderPasses(UnityEngine.Rendering.Universal.ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        CustomScriptableRender customRender = (CustomScriptableRender)renderer;
        m_DepthRenderPass.SetUp(customRender.DepthRti.Identifier());
        renderer.EnqueuePass(m_DepthRenderPass);
    }
}


class CustomDepthRenderPass : ScriptableRenderPass
{
    CustomDepthRenderFeature m_Feature;

    private RenderTargetIdentifier m_Rti;

    public CustomDepthRenderPass(CustomDepthRenderFeature feature)
    {
        m_Feature = feature;
    }

    public void SetUp(RenderTargetIdentifier rti)
    {
        this.m_Rti = rti;
    }

    // This method is called before executing the render pass.
    // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
    // When empty this render pass will render to the active camera render target.
    // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
    // The render pipeline will ensure target setup and clearing happens in an performance manner.
    public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
    {
    }

    // Here you can implement the rendering logic.
    // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
    // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
    // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer command = CommandBufferPool.Get("CopyDepthRT");
        command.Blit(m_Rti, m_Feature.renderSetting.outputTex/*, m_Feature.renderSetting.material*/);
        context.ExecuteCommandBuffer(command);
        CommandBufferPool.Release(command);
    }

    /// Cleanup any allocated resources that were created during the execution of this render pass.
    public override void FrameCleanup(CommandBuffer cmd)
    {
    }
}



