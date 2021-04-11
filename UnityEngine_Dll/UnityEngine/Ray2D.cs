using System;
using System.Globalization;

namespace UnityEngine
{
	public struct Ray2D : IFormattable
	{
		private Vector2 m_Origin;

		private Vector2 m_Direction;

		public Vector2 origin
		{
			get
			{
				return this.m_Origin;
			}
			set
			{
				this.m_Origin = value;
			}
		}

		public Vector2 direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				this.m_Direction = value.normalized;
			}
		}

		public Ray2D(Vector2 origin, Vector2 direction)
		{
			this.m_Origin = origin;
			this.m_Direction = direction.normalized;
		}

		public Vector2 GetPoint(float distance)
		{
			return this.m_Origin + this.m_Direction * distance;
		}

		public override string ToString()
		{
			return this.ToString("F1", CultureInfo.InvariantCulture.NumberFormat);
		}

		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin.ToString(format, formatProvider),
				this.m_Direction.ToString(format, formatProvider)
			});
		}
	}
}
