using System;

namespace UnityEngine.UIElements
{
	public struct StyleColor : IStyleValue<Color>, IEquatable<StyleColor>
	{
		private StyleKeyword m_Keyword;

		private Color m_Value;

		public Color value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : Color.clear;
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

		public StyleColor(Color v)
		{
			this = new StyleColor(v, StyleKeyword.Undefined);
		}

		public StyleColor(StyleKeyword keyword)
		{
			this = new StyleColor(Color.clear, keyword);
		}

		internal StyleColor(Color v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		public static bool operator ==(StyleColor lhs, StyleColor rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		public static bool operator !=(StyleColor lhs, StyleColor rhs)
		{
			return !(lhs == rhs);
		}

		public static bool operator ==(StyleColor lhs, Color rhs)
		{
			StyleColor rhs2 = new StyleColor(rhs);
			return lhs == rhs2;
		}

		public static bool operator !=(StyleColor lhs, Color rhs)
		{
			return !(lhs == rhs);
		}

		public static implicit operator StyleColor(StyleKeyword keyword)
		{
			return new StyleColor(keyword);
		}

		public static implicit operator StyleColor(Color v)
		{
			return new StyleColor(v);
		}

		public bool Equals(StyleColor other)
		{
			return other == this;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleColor);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				StyleColor lhs = (StyleColor)obj;
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
			return this.DebugString<Color>();
		}
	}
}
