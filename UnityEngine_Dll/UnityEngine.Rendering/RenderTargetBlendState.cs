using System;

namespace UnityEngine.Rendering
{
	public struct RenderTargetBlendState : IEquatable<RenderTargetBlendState>
	{
		private byte m_WriteMask;

		private byte m_SourceColorBlendMode;

		private byte m_DestinationColorBlendMode;

		private byte m_SourceAlphaBlendMode;

		private byte m_DestinationAlphaBlendMode;

		private byte m_ColorBlendOperation;

		private byte m_AlphaBlendOperation;

		private byte m_Padding;

		public static RenderTargetBlendState defaultValue
		{
			get
			{
				return new RenderTargetBlendState(ColorWriteMask.All, BlendMode.One, BlendMode.Zero, BlendMode.One, BlendMode.Zero, BlendOp.Add, BlendOp.Add);
			}
		}

		public ColorWriteMask writeMask
		{
			get
			{
				return (ColorWriteMask)this.m_WriteMask;
			}
			set
			{
				this.m_WriteMask = (byte)value;
			}
		}

		public BlendMode sourceColorBlendMode
		{
			get
			{
				return (BlendMode)this.m_SourceColorBlendMode;
			}
			set
			{
				this.m_SourceColorBlendMode = (byte)value;
			}
		}

		public BlendMode destinationColorBlendMode
		{
			get
			{
				return (BlendMode)this.m_DestinationColorBlendMode;
			}
			set
			{
				this.m_DestinationColorBlendMode = (byte)value;
			}
		}

		public BlendMode sourceAlphaBlendMode
		{
			get
			{
				return (BlendMode)this.m_SourceAlphaBlendMode;
			}
			set
			{
				this.m_SourceAlphaBlendMode = (byte)value;
			}
		}

		public BlendMode destinationAlphaBlendMode
		{
			get
			{
				return (BlendMode)this.m_DestinationAlphaBlendMode;
			}
			set
			{
				this.m_DestinationAlphaBlendMode = (byte)value;
			}
		}

		public BlendOp colorBlendOperation
		{
			get
			{
				return (BlendOp)this.m_ColorBlendOperation;
			}
			set
			{
				this.m_ColorBlendOperation = (byte)value;
			}
		}

		public BlendOp alphaBlendOperation
		{
			get
			{
				return (BlendOp)this.m_AlphaBlendOperation;
			}
			set
			{
				this.m_AlphaBlendOperation = (byte)value;
			}
		}

		public RenderTargetBlendState(ColorWriteMask writeMask = ColorWriteMask.All, BlendMode sourceColorBlendMode = BlendMode.One, BlendMode destinationColorBlendMode = BlendMode.Zero, BlendMode sourceAlphaBlendMode = BlendMode.One, BlendMode destinationAlphaBlendMode = BlendMode.Zero, BlendOp colorBlendOperation = BlendOp.Add, BlendOp alphaBlendOperation = BlendOp.Add)
		{
			this.m_WriteMask = (byte)writeMask;
			this.m_SourceColorBlendMode = (byte)sourceColorBlendMode;
			this.m_DestinationColorBlendMode = (byte)destinationColorBlendMode;
			this.m_SourceAlphaBlendMode = (byte)sourceAlphaBlendMode;
			this.m_DestinationAlphaBlendMode = (byte)destinationAlphaBlendMode;
			this.m_ColorBlendOperation = (byte)colorBlendOperation;
			this.m_AlphaBlendOperation = (byte)alphaBlendOperation;
			this.m_Padding = 0;
		}

		public bool Equals(RenderTargetBlendState other)
		{
			return this.m_WriteMask == other.m_WriteMask && this.m_SourceColorBlendMode == other.m_SourceColorBlendMode && this.m_DestinationColorBlendMode == other.m_DestinationColorBlendMode && this.m_SourceAlphaBlendMode == other.m_SourceAlphaBlendMode && this.m_DestinationAlphaBlendMode == other.m_DestinationAlphaBlendMode && this.m_ColorBlendOperation == other.m_ColorBlendOperation && this.m_AlphaBlendOperation == other.m_AlphaBlendOperation;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderTargetBlendState && this.Equals((RenderTargetBlendState)obj);
		}

		public override int GetHashCode()
		{
			int num = this.m_WriteMask.GetHashCode();
			num = (num * 397 ^ this.m_SourceColorBlendMode.GetHashCode());
			num = (num * 397 ^ this.m_DestinationColorBlendMode.GetHashCode());
			num = (num * 397 ^ this.m_SourceAlphaBlendMode.GetHashCode());
			num = (num * 397 ^ this.m_DestinationAlphaBlendMode.GetHashCode());
			num = (num * 397 ^ this.m_ColorBlendOperation.GetHashCode());
			return num * 397 ^ this.m_AlphaBlendOperation.GetHashCode();
		}

		public static bool operator ==(RenderTargetBlendState left, RenderTargetBlendState right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(RenderTargetBlendState left, RenderTargetBlendState right)
		{
			return !left.Equals(right);
		}
	}
}
