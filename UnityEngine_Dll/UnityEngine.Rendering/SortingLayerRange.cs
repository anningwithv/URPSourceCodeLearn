using System;

namespace UnityEngine.Rendering
{
	public struct SortingLayerRange : IEquatable<SortingLayerRange>
	{
		private short m_LowerBound;

		private short m_UpperBound;

		public short lowerBound
		{
			get
			{
				return this.m_LowerBound;
			}
			set
			{
				this.m_LowerBound = value;
			}
		}

		public short upperBound
		{
			get
			{
				return this.m_UpperBound;
			}
			set
			{
				this.m_UpperBound = value;
			}
		}

		public static SortingLayerRange all
		{
			get
			{
				return new SortingLayerRange
				{
					m_LowerBound = -32768,
					m_UpperBound = 32767
				};
			}
		}

		public SortingLayerRange(short lowerBound, short upperBound)
		{
			this.m_LowerBound = lowerBound;
			this.m_UpperBound = upperBound;
		}

		public bool Equals(SortingLayerRange other)
		{
			return this.m_LowerBound == other.m_LowerBound && this.m_UpperBound == other.m_UpperBound;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is SortingLayerRange);
			return !flag && this.Equals((SortingLayerRange)obj);
		}

		public static bool operator !=(SortingLayerRange lhs, SortingLayerRange rhs)
		{
			return !lhs.Equals(rhs);
		}

		public static bool operator ==(SortingLayerRange lhs, SortingLayerRange rhs)
		{
			return lhs.Equals(rhs);
		}

		public override int GetHashCode()
		{
			return (int)this.m_UpperBound << 16 | ((int)this.m_LowerBound & 65535);
		}
	}
}
