using System;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	public struct StyleBackground : IStyleValue<Background>, IEquatable<StyleBackground>
	{
		private StyleKeyword m_Keyword;

		private Background m_Value;

		public Background value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Background);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		public StyleKeyword keyword
		{
			get
			{
				return this.m_Keyword;
			}
			set
			{
				this.m_Keyword = value;
			}
		}

		public StyleBackground(Background v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		public StyleBackground(Texture2D v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		public StyleBackground(VectorImage v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		public StyleBackground(StyleKeyword keyword)
		{
			this = new StyleBackground(default(Background), keyword);
		}

		internal StyleBackground(Texture2D v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromTexture2D(v), keyword);
		}

		internal StyleBackground(VectorImage v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromVectorImage(v), keyword);
		}

		internal StyleBackground(GCHandle gcHandle, StyleKeyword keyword)
		{
			this = new StyleBackground(gcHandle.IsAllocated ? Background.FromObject(gcHandle.Target) : default(Background), keyword);
		}

		internal StyleBackground(Background v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		public static bool operator ==(StyleBackground lhs, StyleBackground rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		public static bool operator !=(StyleBackground lhs, StyleBackground rhs)
		{
			return !(lhs == rhs);
		}

		public static implicit operator StyleBackground(StyleKeyword keyword)
		{
			return new StyleBackground(keyword);
		}

		public static implicit operator StyleBackground(Background v)
		{
			return new StyleBackground(v);
		}

		public static implicit operator StyleBackground(Texture2D v)
		{
			return new StyleBackground(v);
		}

		public bool Equals(StyleBackground other)
		{
			return other == this;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleBackground);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				StyleBackground lhs = (StyleBackground)obj;
				result = (lhs == this);
			}
			return result;
		}

		public override int GetHashCode()
		{
			int num = 917506989;
			num = num * -1521134295 + this.m_Keyword.GetHashCode();
			return num * -1521134295 + this.m_Value.GetHashCode();
		}

		public override string ToString()
		{
			return this.DebugString<Background>();
		}
	}
}
