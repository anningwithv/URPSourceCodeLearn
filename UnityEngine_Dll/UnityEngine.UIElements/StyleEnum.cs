using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements
{
	public struct StyleEnum<T> : IStyleValue<T>, IEquatable<StyleEnum<T>> where T : struct, IConvertible
	{
		private StyleKeyword m_Keyword;

		private T m_Value;

		public T value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(T);
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

		public StyleEnum(T v)
		{
			this = new StyleEnum<T>(v, StyleKeyword.Undefined);
		}

		public StyleEnum(StyleKeyword keyword)
		{
			this = new StyleEnum<T>(default(T), keyword);
		}

		internal StyleEnum(T v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		public static bool operator ==(StyleEnum<T> lhs, StyleEnum<T> rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && UnsafeUtility.EnumEquals<T>(lhs.m_Value, rhs.m_Value);
		}

		public static bool operator !=(StyleEnum<T> lhs, StyleEnum<T> rhs)
		{
			return !(lhs == rhs);
		}

		public static implicit operator StyleEnum<T>(StyleKeyword keyword)
		{
			return new StyleEnum<T>(keyword);
		}

		public static implicit operator StyleEnum<T>(T v)
		{
			return new StyleEnum<T>(v);
		}

		public bool Equals(StyleEnum<T> other)
		{
			return other == this;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleEnum<T>);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				StyleEnum<T> lhs = (StyleEnum<T>)obj;
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
			return this.DebugString<T>();
		}
	}
}
