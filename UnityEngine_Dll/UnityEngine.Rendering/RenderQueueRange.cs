using System;

namespace UnityEngine.Rendering
{
	public struct RenderQueueRange : IEquatable<RenderQueueRange>
	{
		private int m_LowerBound;

		private int m_UpperBound;

		private const int k_MinimumBound = 0;

		public static readonly int minimumBound = 0;

		private const int k_MaximumBound = 5000;

		public static readonly int maximumBound = 5000;

		public static RenderQueueRange all
		{
			get
			{
				return new RenderQueueRange
				{
					m_LowerBound = 0,
					m_UpperBound = 5000
				};
			}
		}

		public static RenderQueueRange opaque
		{
			get
			{
				return new RenderQueueRange
				{
					m_LowerBound = 0,
					m_UpperBound = 2500
				};
			}
		}

		public static RenderQueueRange transparent
		{
			get
			{
				return new RenderQueueRange
				{
					m_LowerBound = 2501,
					m_UpperBound = 5000
				};
			}
		}

		public int lowerBound
		{
			get
			{
				return this.m_LowerBound;
			}
			set
			{
				bool flag = value < 0 || value > 5000;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("The lower bound must be at least {0} and at most {1}.", 0, 5000));
				}
				this.m_LowerBound = value;
			}
		}

		public int upperBound
		{
			get
			{
				return this.m_UpperBound;
			}
			set
			{
				bool flag = value < 0 || value > 5000;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("The upper bound must be at least {0} and at most {1}.", 0, 5000));
				}
				this.m_UpperBound = value;
			}
		}

		public RenderQueueRange(int lowerBound, int upperBound)
		{
			bool flag = lowerBound < 0 || lowerBound > 5000;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("lowerBound", lowerBound, string.Format("The lower bound must be at least {0} and at most {1}.", 0, 5000));
			}
			bool flag2 = upperBound < 0 || upperBound > 5000;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("upperBound", upperBound, string.Format("The upper bound must be at least {0} and at most {1}.", 0, 5000));
			}
			this.m_LowerBound = lowerBound;
			this.m_UpperBound = upperBound;
		}

		public bool Equals(RenderQueueRange other)
		{
			return this.m_LowerBound == other.m_LowerBound && this.m_UpperBound == other.m_UpperBound;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderQueueRange && this.Equals((RenderQueueRange)obj);
		}

		public override int GetHashCode()
		{
			return this.m_LowerBound * 397 ^ this.m_UpperBound;
		}

		public static bool operator ==(RenderQueueRange left, RenderQueueRange right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(RenderQueueRange left, RenderQueueRange right)
		{
			return !left.Equals(right);
		}
	}
}
