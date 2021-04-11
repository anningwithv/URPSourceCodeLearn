using System;

namespace UnityEngine.UIElements
{
	public struct StyleLength : IStyleValue<Length>, IEquatable<StyleLength>
	{
		private StyleKeyword m_Keyword;

		private Length m_Value;

		public Length value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Length);
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

		public StyleLength(float v)
		{
			this = new StyleLength(new Length(v, LengthUnit.Pixel), StyleKeyword.Undefined);
		}

		public StyleLength(Length v)
		{
			this = new StyleLength(v, StyleKeyword.Undefined);
		}

		public StyleLength(StyleKeyword keyword)
		{
			this = new StyleLength(default(Length), keyword);
		}

		internal StyleLength(Length v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		public static bool operator ==(StyleLength lhs, StyleLength rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		public static bool operator !=(StyleLength lhs, StyleLength rhs)
		{
			return !(lhs == rhs);
		}

		public static implicit operator StyleLength(StyleKeyword keyword)
		{
			return new StyleLength(keyword);
		}

		public static implicit operator StyleLength(float v)
		{
			return new StyleLength(v);
		}

		public static implicit operator StyleLength(Length v)
		{
			return new StyleLength(v);
		}

		public bool Equals(StyleLength other)
		{
			return other == this;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleLength);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				StyleLength lhs = (StyleLength)obj;
				result = (lhs == this);
			}
			return result;
		}

		public override int GetHashCode()
		{
			int num = -1977396678;
			num = num * -1521134295 + this.m_Keyword.GetHashCode();
			return num * -1521134295 + this.m_Value.GetHashCode();
		}

		public override string ToString()
		{
			return this.DebugString<Length>();
		}
	}
}
