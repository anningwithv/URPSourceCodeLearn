using System;

namespace UnityEngine.Rendering
{
	public struct BlendState : IEquatable<BlendState>
	{
		private RenderTargetBlendState m_BlendState0;

		private RenderTargetBlendState m_BlendState1;

		private RenderTargetBlendState m_BlendState2;

		private RenderTargetBlendState m_BlendState3;

		private RenderTargetBlendState m_BlendState4;

		private RenderTargetBlendState m_BlendState5;

		private RenderTargetBlendState m_BlendState6;

		private RenderTargetBlendState m_BlendState7;

		private byte m_SeparateMRTBlendStates;

		private byte m_AlphaToMask;

		private short m_Padding;

		public static BlendState defaultValue
		{
			get
			{
				return new BlendState(false, false);
			}
		}

		public bool separateMRTBlendStates
		{
			get
			{
				return Convert.ToBoolean(this.m_SeparateMRTBlendStates);
			}
			set
			{
				this.m_SeparateMRTBlendStates = Convert.ToByte(value);
			}
		}

		public bool alphaToMask
		{
			get
			{
				return Convert.ToBoolean(this.m_AlphaToMask);
			}
			set
			{
				this.m_AlphaToMask = Convert.ToByte(value);
			}
		}

		public RenderTargetBlendState blendState0
		{
			get
			{
				return this.m_BlendState0;
			}
			set
			{
				this.m_BlendState0 = value;
			}
		}

		public RenderTargetBlendState blendState1
		{
			get
			{
				return this.m_BlendState1;
			}
			set
			{
				this.m_BlendState1 = value;
			}
		}

		public RenderTargetBlendState blendState2
		{
			get
			{
				return this.m_BlendState2;
			}
			set
			{
				this.m_BlendState2 = value;
			}
		}

		public RenderTargetBlendState blendState3
		{
			get
			{
				return this.m_BlendState3;
			}
			set
			{
				this.m_BlendState3 = value;
			}
		}

		public RenderTargetBlendState blendState4
		{
			get
			{
				return this.m_BlendState4;
			}
			set
			{
				this.m_BlendState4 = value;
			}
		}

		public RenderTargetBlendState blendState5
		{
			get
			{
				return this.m_BlendState5;
			}
			set
			{
				this.m_BlendState5 = value;
			}
		}

		public RenderTargetBlendState blendState6
		{
			get
			{
				return this.m_BlendState6;
			}
			set
			{
				this.m_BlendState6 = value;
			}
		}

		public RenderTargetBlendState blendState7
		{
			get
			{
				return this.m_BlendState7;
			}
			set
			{
				this.m_BlendState7 = value;
			}
		}

		public BlendState(bool separateMRTBlend = false, bool alphaToMask = false)
		{
			this.m_BlendState0 = RenderTargetBlendState.defaultValue;
			this.m_BlendState1 = RenderTargetBlendState.defaultValue;
			this.m_BlendState2 = RenderTargetBlendState.defaultValue;
			this.m_BlendState3 = RenderTargetBlendState.defaultValue;
			this.m_BlendState4 = RenderTargetBlendState.defaultValue;
			this.m_BlendState5 = RenderTargetBlendState.defaultValue;
			this.m_BlendState6 = RenderTargetBlendState.defaultValue;
			this.m_BlendState7 = RenderTargetBlendState.defaultValue;
			this.m_SeparateMRTBlendStates = Convert.ToByte(separateMRTBlend);
			this.m_AlphaToMask = Convert.ToByte(alphaToMask);
			this.m_Padding = 0;
		}

		public bool Equals(BlendState other)
		{
			return this.m_BlendState0.Equals(other.m_BlendState0) && this.m_BlendState1.Equals(other.m_BlendState1) && this.m_BlendState2.Equals(other.m_BlendState2) && this.m_BlendState3.Equals(other.m_BlendState3) && this.m_BlendState4.Equals(other.m_BlendState4) && this.m_BlendState5.Equals(other.m_BlendState5) && this.m_BlendState6.Equals(other.m_BlendState6) && this.m_BlendState7.Equals(other.m_BlendState7) && this.m_SeparateMRTBlendStates == other.m_SeparateMRTBlendStates && this.m_AlphaToMask == other.m_AlphaToMask;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is BlendState && this.Equals((BlendState)obj);
		}

		public override int GetHashCode()
		{
			int num = this.m_BlendState0.GetHashCode();
			num = (num * 397 ^ this.m_BlendState1.GetHashCode());
			num = (num * 397 ^ this.m_BlendState2.GetHashCode());
			num = (num * 397 ^ this.m_BlendState3.GetHashCode());
			num = (num * 397 ^ this.m_BlendState4.GetHashCode());
			num = (num * 397 ^ this.m_BlendState5.GetHashCode());
			num = (num * 397 ^ this.m_BlendState6.GetHashCode());
			num = (num * 397 ^ this.m_BlendState7.GetHashCode());
			num = (num * 397 ^ this.m_SeparateMRTBlendStates.GetHashCode());
			return num * 397 ^ this.m_AlphaToMask.GetHashCode();
		}

		public static bool operator ==(BlendState left, BlendState right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(BlendState left, BlendState right)
		{
			return !left.Equals(right);
		}
	}
}
