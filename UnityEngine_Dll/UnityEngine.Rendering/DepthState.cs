using System;

namespace UnityEngine.Rendering
{
	public struct DepthState : IEquatable<DepthState>
	{
		private byte m_WriteEnabled;

		private sbyte m_CompareFunction;

		public static DepthState defaultValue
		{
			get
			{
				return new DepthState(true, CompareFunction.Less);
			}
		}

		public bool writeEnabled
		{
			get
			{
				return Convert.ToBoolean(this.m_WriteEnabled);
			}
			set
			{
				this.m_WriteEnabled = Convert.ToByte(value);
			}
		}

		public CompareFunction compareFunction
		{
			get
			{
				return (CompareFunction)this.m_CompareFunction;
			}
			set
			{
				this.m_CompareFunction = (sbyte)value;
			}
		}

		public DepthState(bool writeEnabled = true, CompareFunction compareFunction = CompareFunction.Less)
		{
			this.m_WriteEnabled = Convert.ToByte(writeEnabled);
			this.m_CompareFunction = (sbyte)compareFunction;
		}

		public bool Equals(DepthState other)
		{
			return this.m_WriteEnabled == other.m_WriteEnabled && this.m_CompareFunction == other.m_CompareFunction;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is DepthState && this.Equals((DepthState)obj);
		}

		public override int GetHashCode()
		{
			return this.m_WriteEnabled.GetHashCode() * 397 ^ this.m_CompareFunction.GetHashCode();
		}

		public static bool operator ==(DepthState left, DepthState right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(DepthState left, DepthState right)
		{
			return !left.Equals(right);
		}
	}
}
