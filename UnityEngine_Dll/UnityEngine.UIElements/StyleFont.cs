using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	public struct StyleFont : IStyleValue<Font>, IEquatable<StyleFont>
	{
		private StyleKeyword m_Keyword;

		private Font m_Value;

		public Font value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : null;
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

		public StyleFont(Font v)
		{
			this = new StyleFont(v, StyleKeyword.Undefined);
		}

		public StyleFont(StyleKeyword keyword)
		{
			this = new StyleFont(null, keyword);
		}

		internal StyleFont(GCHandle gcHandle, StyleKeyword keyword)
		{
			this = new StyleFont(gcHandle.IsAllocated ? (gcHandle.Target as Font) : null, keyword);
		}

		internal StyleFont(Font v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		public static bool operator ==(StyleFont lhs, StyleFont rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		public static bool operator !=(StyleFont lhs, StyleFont rhs)
		{
			return !(lhs == rhs);
		}

		public static implicit operator StyleFont(StyleKeyword keyword)
		{
			return new StyleFont(keyword);
		}

		public static implicit operator StyleFont(Font v)
		{
			return new StyleFont(v);
		}

		public bool Equals(StyleFont other)
		{
			return other == this;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleFont);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				StyleFont lhs = (StyleFont)obj;
				result = (lhs == this);
			}
			return result;
		}

		public override int GetHashCode()
		{
			int num = 917506989;
			num = num * -1521134295 + this.m_Keyword.GetHashCode();
			return num * -1521134295 + EqualityComparer<Font>.Default.GetHashCode(this.m_Value);
		}

		public override string ToString()
		{
			return this.DebugString<Font>();
		}
	}
}
