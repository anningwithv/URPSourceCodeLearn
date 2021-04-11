using System;

namespace UnityEngine.UIElements
{
	public struct StyleCursor : IStyleValue<Cursor>, IEquatable<StyleCursor>
	{
		private StyleKeyword m_Keyword;

		private Cursor m_Value;

		public Cursor value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Cursor);
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

		public StyleCursor(Cursor v)
		{
			this = new StyleCursor(v, StyleKeyword.Undefined);
		}

		public StyleCursor(StyleKeyword keyword)
		{
			this = new StyleCursor(default(Cursor), keyword);
		}

		internal StyleCursor(Cursor v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		public static bool operator ==(StyleCursor lhs, StyleCursor rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		public static bool operator !=(StyleCursor lhs, StyleCursor rhs)
		{
			return !(lhs == rhs);
		}

		public static implicit operator StyleCursor(StyleKeyword keyword)
		{
			return new StyleCursor(keyword);
		}

		public static implicit operator StyleCursor(Cursor v)
		{
			return new StyleCursor(v);
		}

		public bool Equals(StyleCursor other)
		{
			return other == this;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleCursor);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				StyleCursor lhs = (StyleCursor)obj;
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
			return this.DebugString<Cursor>();
		}
	}
}
