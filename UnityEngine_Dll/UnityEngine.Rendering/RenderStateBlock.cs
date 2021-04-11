using System;

namespace UnityEngine.Rendering
{
	public struct RenderStateBlock : IEquatable<RenderStateBlock>
	{
		private BlendState m_BlendState;

		private RasterState m_RasterState;

		private DepthState m_DepthState;

		private StencilState m_StencilState;

		private int m_StencilReference;

		private RenderStateMask m_Mask;

		public BlendState blendState
		{
			get
			{
				return this.m_BlendState;
			}
			set
			{
				this.m_BlendState = value;
			}
		}

		public RasterState rasterState
		{
			get
			{
				return this.m_RasterState;
			}
			set
			{
				this.m_RasterState = value;
			}
		}

		public DepthState depthState
		{
			get
			{
				return this.m_DepthState;
			}
			set
			{
				this.m_DepthState = value;
			}
		}

		public StencilState stencilState
		{
			get
			{
				return this.m_StencilState;
			}
			set
			{
				this.m_StencilState = value;
			}
		}

		public int stencilReference
		{
			get
			{
				return this.m_StencilReference;
			}
			set
			{
				this.m_StencilReference = value;
			}
		}

		public RenderStateMask mask
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		public RenderStateBlock(RenderStateMask mask)
		{
			this.m_BlendState = BlendState.defaultValue;
			this.m_RasterState = RasterState.defaultValue;
			this.m_DepthState = DepthState.defaultValue;
			this.m_StencilState = StencilState.defaultValue;
			this.m_StencilReference = 0;
			this.m_Mask = mask;
		}

		public bool Equals(RenderStateBlock other)
		{
			return this.m_BlendState.Equals(other.m_BlendState) && this.m_RasterState.Equals(other.m_RasterState) && this.m_DepthState.Equals(other.m_DepthState) && this.m_StencilState.Equals(other.m_StencilState) && this.m_StencilReference == other.m_StencilReference && this.m_Mask == other.m_Mask;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderStateBlock && this.Equals((RenderStateBlock)obj);
		}

		public override int GetHashCode()
		{
			int num = this.m_BlendState.GetHashCode();
			num = (num * 397 ^ this.m_RasterState.GetHashCode());
			num = (num * 397 ^ this.m_DepthState.GetHashCode());
			num = (num * 397 ^ this.m_StencilState.GetHashCode());
			num = (num * 397 ^ this.m_StencilReference);
			return num * 397 ^ (int)this.m_Mask;
		}

		public static bool operator ==(RenderStateBlock left, RenderStateBlock right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(RenderStateBlock left, RenderStateBlock right)
		{
			return !left.Equals(right);
		}
	}
}
