using System;

namespace UnityEngine.Rendering
{
	public struct RenderTargetBinding
	{
		private RenderTargetIdentifier[] m_ColorRenderTargets;

		private RenderTargetIdentifier m_DepthRenderTarget;

		private RenderBufferLoadAction[] m_ColorLoadActions;

		private RenderBufferStoreAction[] m_ColorStoreActions;

		private RenderBufferLoadAction m_DepthLoadAction;

		private RenderBufferStoreAction m_DepthStoreAction;

		private RenderTargetFlags m_Flags;

		public RenderTargetIdentifier[] colorRenderTargets
		{
			get
			{
				return this.m_ColorRenderTargets;
			}
			set
			{
				this.m_ColorRenderTargets = value;
			}
		}

		public RenderTargetIdentifier depthRenderTarget
		{
			get
			{
				return this.m_DepthRenderTarget;
			}
			set
			{
				this.m_DepthRenderTarget = value;
			}
		}

		public RenderBufferLoadAction[] colorLoadActions
		{
			get
			{
				return this.m_ColorLoadActions;
			}
			set
			{
				this.m_ColorLoadActions = value;
			}
		}

		public RenderBufferStoreAction[] colorStoreActions
		{
			get
			{
				return this.m_ColorStoreActions;
			}
			set
			{
				this.m_ColorStoreActions = value;
			}
		}

		public RenderBufferLoadAction depthLoadAction
		{
			get
			{
				return this.m_DepthLoadAction;
			}
			set
			{
				this.m_DepthLoadAction = value;
			}
		}

		public RenderBufferStoreAction depthStoreAction
		{
			get
			{
				return this.m_DepthStoreAction;
			}
			set
			{
				this.m_DepthStoreAction = value;
			}
		}

		public RenderTargetFlags flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		public RenderTargetBinding(RenderTargetIdentifier[] colorRenderTargets, RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions, RenderTargetIdentifier depthRenderTarget, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this.m_ColorRenderTargets = colorRenderTargets;
			this.m_DepthRenderTarget = depthRenderTarget;
			this.m_ColorLoadActions = colorLoadActions;
			this.m_ColorStoreActions = colorStoreActions;
			this.m_DepthLoadAction = depthLoadAction;
			this.m_DepthStoreAction = depthStoreAction;
			this.m_Flags = RenderTargetFlags.None;
		}

		public RenderTargetBinding(RenderTargetIdentifier colorRenderTarget, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depthRenderTarget, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this = new RenderTargetBinding(new RenderTargetIdentifier[]
			{
				colorRenderTarget
			}, new RenderBufferLoadAction[]
			{
				colorLoadAction
			}, new RenderBufferStoreAction[]
			{
				colorStoreAction
			}, depthRenderTarget, depthLoadAction, depthStoreAction);
		}

		public RenderTargetBinding(RenderTargetSetup setup)
		{
			this.m_ColorRenderTargets = new RenderTargetIdentifier[setup.color.Length];
			for (int i = 0; i < this.m_ColorRenderTargets.Length; i++)
			{
				this.m_ColorRenderTargets[i] = new RenderTargetIdentifier(setup.color[i], setup.mipLevel, setup.cubemapFace, setup.depthSlice);
			}
			this.m_DepthRenderTarget = setup.depth;
			this.m_ColorLoadActions = (RenderBufferLoadAction[])setup.colorLoad.Clone();
			this.m_ColorStoreActions = (RenderBufferStoreAction[])setup.colorStore.Clone();
			this.m_DepthLoadAction = setup.depthLoad;
			this.m_DepthStoreAction = setup.depthStore;
			this.m_Flags = RenderTargetFlags.None;
		}
	}
}
