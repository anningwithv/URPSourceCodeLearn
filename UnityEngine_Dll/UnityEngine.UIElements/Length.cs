using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	public struct Length : IEquatable<Length>
	{
		private float m_Value;

		private LengthUnit m_Unit;

		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		public LengthUnit unit
		{
			get
			{
				return this.m_Unit;
			}
			set
			{
				this.m_Unit = value;
			}
		}

		public static Length Percent(float value)
		{
			return new Length(value, LengthUnit.Percent);
		}

		public Length(float value)
		{
			this = new Length(value, LengthUnit.Pixel);
		}

		public Length(float value, LengthUnit unit)
		{
			this.m_Value = value;
			this.m_Unit = unit;
		}

		public static implicit operator Length(float value)
		{
			return new Length(value, LengthUnit.Pixel);
		}

		public static bool operator ==(Length lhs, Length rhs)
		{
			return lhs.m_Value == rhs.m_Value && lhs.m_Unit == rhs.m_Unit;
		}

		public static bool operator !=(Length lhs, Length rhs)
		{
			return !(lhs == rhs);
		}

		public bool Equals(Length other)
		{
			return other == this;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is Length);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Length lhs = (Length)obj;
				result = (lhs == this);
			}
			return result;
		}

		public override int GetHashCode()
		{
			int num = 851985039;
			num = num * -1521134295 + this.m_Value.GetHashCode();
			return num * -1521134295 + this.m_Unit.GetHashCode();
		}

		public override string ToString()
		{
			string str = string.Empty;
			LengthUnit unit = this.unit;
			LengthUnit lengthUnit = unit;
			if (lengthUnit != LengthUnit.Pixel)
			{
				if (lengthUnit == LengthUnit.Percent)
				{
					str = "%";
				}
			}
			else
			{
				bool flag = !Mathf.Approximately(0f, this.value);
				if (flag)
				{
					str = "px";
				}
			}
			return this.value.ToString(CultureInfo.InvariantCulture.NumberFormat) + str;
		}
	}
}
