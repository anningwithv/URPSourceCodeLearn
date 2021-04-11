using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore
{
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphRect : IEquatable<GlyphRect>
	{
		[NativeName("x"), SerializeField]
		private int m_X;

		[NativeName("y"), SerializeField]
		private int m_Y;

		[NativeName("width"), SerializeField]
		private int m_Width;

		[NativeName("height"), SerializeField]
		private int m_Height;

		private static readonly GlyphRect s_ZeroGlyphRect = new GlyphRect(0, 0, 0, 0);

		public int x
		{
			get
			{
				return this.m_X;
			}
			set
			{
				this.m_X = value;
			}
		}

		public int y
		{
			get
			{
				return this.m_Y;
			}
			set
			{
				this.m_Y = value;
			}
		}

		public int width
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

		public int height
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

		public static GlyphRect zero
		{
			get
			{
				return GlyphRect.s_ZeroGlyphRect;
			}
		}

		public GlyphRect(int x, int y, int width, int height)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		public GlyphRect(Rect rect)
		{
			this.m_X = (int)rect.x;
			this.m_Y = (int)rect.y;
			this.m_Width = (int)rect.width;
			this.m_Height = (int)rect.height;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public bool Equals(GlyphRect other)
		{
			return base.Equals(other);
		}

		public static bool operator ==(GlyphRect lhs, GlyphRect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		public static bool operator !=(GlyphRect lhs, GlyphRect rhs)
		{
			return !(lhs == rhs);
		}
	}
}
