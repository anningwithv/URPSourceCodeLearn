using System;

namespace UnityEngine.Rendering
{
	public struct StencilState : IEquatable<StencilState>
	{
		private byte m_Enabled;

		private byte m_ReadMask;

		private byte m_WriteMask;

		private byte m_Padding;

		private byte m_CompareFunctionFront;

		private byte m_PassOperationFront;

		private byte m_FailOperationFront;

		private byte m_ZFailOperationFront;

		private byte m_CompareFunctionBack;

		private byte m_PassOperationBack;

		private byte m_FailOperationBack;

		private byte m_ZFailOperationBack;

		public static StencilState defaultValue
		{
			get
			{
				return new StencilState(true, 255, 255, CompareFunction.Always, StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
			}
		}

		public bool enabled
		{
			get
			{
				return Convert.ToBoolean(this.m_Enabled);
			}
			set
			{
				this.m_Enabled = Convert.ToByte(value);
			}
		}

		public byte readMask
		{
			get
			{
				return this.m_ReadMask;
			}
			set
			{
				this.m_ReadMask = value;
			}
		}

		public byte writeMask
		{
			get
			{
				return this.m_WriteMask;
			}
			set
			{
				this.m_WriteMask = value;
			}
		}

		public CompareFunction compareFunctionFront
		{
			get
			{
				return (CompareFunction)this.m_CompareFunctionFront;
			}
			set
			{
				this.m_CompareFunctionFront = (byte)value;
			}
		}

		public StencilOp passOperationFront
		{
			get
			{
				return (StencilOp)this.m_PassOperationFront;
			}
			set
			{
				this.m_PassOperationFront = (byte)value;
			}
		}

		public StencilOp failOperationFront
		{
			get
			{
				return (StencilOp)this.m_FailOperationFront;
			}
			set
			{
				this.m_FailOperationFront = (byte)value;
			}
		}

		public StencilOp zFailOperationFront
		{
			get
			{
				return (StencilOp)this.m_ZFailOperationFront;
			}
			set
			{
				this.m_ZFailOperationFront = (byte)value;
			}
		}

		public CompareFunction compareFunctionBack
		{
			get
			{
				return (CompareFunction)this.m_CompareFunctionBack;
			}
			set
			{
				this.m_CompareFunctionBack = (byte)value;
			}
		}

		public StencilOp passOperationBack
		{
			get
			{
				return (StencilOp)this.m_PassOperationBack;
			}
			set
			{
				this.m_PassOperationBack = (byte)value;
			}
		}

		public StencilOp failOperationBack
		{
			get
			{
				return (StencilOp)this.m_FailOperationBack;
			}
			set
			{
				this.m_FailOperationBack = (byte)value;
			}
		}

		public StencilOp zFailOperationBack
		{
			get
			{
				return (StencilOp)this.m_ZFailOperationBack;
			}
			set
			{
				this.m_ZFailOperationBack = (byte)value;
			}
		}

		public StencilState(bool enabled = true, byte readMask = 255, byte writeMask = 255, CompareFunction compareFunction = CompareFunction.Always, StencilOp passOperation = StencilOp.Keep, StencilOp failOperation = StencilOp.Keep, StencilOp zFailOperation = StencilOp.Keep)
		{
			this = new StencilState(enabled, readMask, writeMask, compareFunction, passOperation, failOperation, zFailOperation, compareFunction, passOperation, failOperation, zFailOperation);
		}

		public StencilState(bool enabled, byte readMask, byte writeMask, CompareFunction compareFunctionFront, StencilOp passOperationFront, StencilOp failOperationFront, StencilOp zFailOperationFront, CompareFunction compareFunctionBack, StencilOp passOperationBack, StencilOp failOperationBack, StencilOp zFailOperationBack)
		{
			this.m_Enabled = Convert.ToByte(enabled);
			this.m_ReadMask = readMask;
			this.m_WriteMask = writeMask;
			this.m_Padding = 0;
			this.m_CompareFunctionFront = (byte)compareFunctionFront;
			this.m_PassOperationFront = (byte)passOperationFront;
			this.m_FailOperationFront = (byte)failOperationFront;
			this.m_ZFailOperationFront = (byte)zFailOperationFront;
			this.m_CompareFunctionBack = (byte)compareFunctionBack;
			this.m_PassOperationBack = (byte)passOperationBack;
			this.m_FailOperationBack = (byte)failOperationBack;
			this.m_ZFailOperationBack = (byte)zFailOperationBack;
		}

		public void SetCompareFunction(CompareFunction value)
		{
			this.compareFunctionFront = value;
			this.compareFunctionBack = value;
		}

		public void SetPassOperation(StencilOp value)
		{
			this.passOperationFront = value;
			this.passOperationBack = value;
		}

		public void SetFailOperation(StencilOp value)
		{
			this.failOperationFront = value;
			this.failOperationBack = value;
		}

		public void SetZFailOperation(StencilOp value)
		{
			this.zFailOperationFront = value;
			this.zFailOperationBack = value;
		}

		public bool Equals(StencilState other)
		{
			return this.m_Enabled == other.m_Enabled && this.m_ReadMask == other.m_ReadMask && this.m_WriteMask == other.m_WriteMask && this.m_CompareFunctionFront == other.m_CompareFunctionFront && this.m_PassOperationFront == other.m_PassOperationFront && this.m_FailOperationFront == other.m_FailOperationFront && this.m_ZFailOperationFront == other.m_ZFailOperationFront && this.m_CompareFunctionBack == other.m_CompareFunctionBack && this.m_PassOperationBack == other.m_PassOperationBack && this.m_FailOperationBack == other.m_FailOperationBack && this.m_ZFailOperationBack == other.m_ZFailOperationBack;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is StencilState && this.Equals((StencilState)obj);
		}

		public override int GetHashCode()
		{
			int num = this.m_Enabled.GetHashCode();
			num = (num * 397 ^ this.m_ReadMask.GetHashCode());
			num = (num * 397 ^ this.m_WriteMask.GetHashCode());
			num = (num * 397 ^ this.m_CompareFunctionFront.GetHashCode());
			num = (num * 397 ^ this.m_PassOperationFront.GetHashCode());
			num = (num * 397 ^ this.m_FailOperationFront.GetHashCode());
			num = (num * 397 ^ this.m_ZFailOperationFront.GetHashCode());
			num = (num * 397 ^ this.m_CompareFunctionBack.GetHashCode());
			num = (num * 397 ^ this.m_PassOperationBack.GetHashCode());
			num = (num * 397 ^ this.m_FailOperationBack.GetHashCode());
			return num * 397 ^ this.m_ZFailOperationBack.GetHashCode();
		}

		public static bool operator ==(StencilState left, StencilState right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(StencilState left, StencilState right)
		{
			return !left.Equals(right);
		}
	}
}
