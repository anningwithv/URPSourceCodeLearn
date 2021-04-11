using System;

namespace UnityEngine.UIElements
{
	public struct StyleFloat : IStyleValue<float>, IEquatable<StyleFloat>
	{
		private StyleKeyword m_Keyword;

		private float m_Value;

		public float value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : 0f;
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

		public StyleFloat(float v)
		{
			this = new StyleFloat(v, StyleKeyword.Undefined);
		}

		public StyleFloat(StyleKeyword keyword)
		{
			this = new StyleFloat(0f, keyword);
		}

		internal StyleFloat(float v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		public static bool operator ==(StyleFloat lhs, StyleFloat rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		public static bool operator !=(StyleFloat lhs, StyleFloat rhs)
		{
			return !(lhs == rhs);
		}

		public static implicit operator StyleFloat(StyleKeyword keyword)
		{
			return new StyleFloat(keyword);
		}

		public static implicit operator StyleFloat(float v)
		{
			return new StyleFloat(v);
		}

		public bool Equals(StyleFloat other)
		{
			return other == this;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleFloat);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				StyleFloat lhs = (StyleFloat)obj;
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
			return this.DebugString<float>();
		}
	}
}
