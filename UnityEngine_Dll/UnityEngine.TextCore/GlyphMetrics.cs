using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore
{
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphMetrics : IEquatable<GlyphMetrics>
	{
		[NativeName("width"), SerializeField]
		private float m_Width;

		[NativeName("height"), SerializeField]
		private float m_Height;

		[NativeName("horizontalBearingX"), SerializeField]
		private float m_HorizontalBearingX;

		[NativeName("horizontalBearingY"), SerializeField]
		private float m_HorizontalBearingY;

		[NativeName("horizontalAdvance"), SerializeField]
		private float m_HorizontalAdvance;

		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		public float height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		public float horizontalBearingX
		{
			get
			{
				return this.m_HorizontalBearingX;
			}
			set
			{
				this.m_HorizontalBearingX = value;
			}
		}

		public float horizontalBearingY
		{
			get
			{
				return this.m_HorizontalBearingY;
			}
			set
			{
				this.m_HorizontalBearingY = value;
			}
		}

		public float horizontalAdvance
		{
			get
			{
				return this.m_HorizontalAdvance;
			}
			set
			{
				this.m_HorizontalAdvance = value;
			}
		}

		public GlyphMetrics(float width, float height, float bearingX, float bearingY, float advance)
		{
			this.m_Width = width;
			this.m_Height = height;
			this.m_HorizontalBearingX = bearingX;
			this.m_HorizontalBearingY = bearingY;
			this.m_HorizontalAdvance = advance;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public bool Equals(GlyphMetrics other)
		{
			return base.Equals(other);
		}

		public static bool operator ==(GlyphMetrics lhs, GlyphMetrics rhs)
		{
			return lhs.width == rhs.width && lhs.height == rhs.height && lhs.horizontalBearingX == rhs.horizontalBearingX && lhs.horizontalBearingY == rhs.horizontalBearingY && lhs.horizontalAdvance == rhs.horizontalAdvance;
		}

		public static bool operator !=(GlyphMetrics lhs, GlyphMetrics rhs)
		{
			return !(lhs == rhs);
		}
	}
}
