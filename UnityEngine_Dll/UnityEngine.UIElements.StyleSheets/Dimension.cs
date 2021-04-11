using System;
using System.Globalization;

namespace UnityEngine.UIElements.StyleSheets
{
	[Serializable]
	internal struct Dimension : IEquatable<Dimension>
	{
		public enum Unit
		{
			Unitless,
			Pixel,
			Percent
		}

		public Dimension.Unit unit;

		public float value;

		public Dimension(float value, Dimension.Unit unit)
		{
			this.unit = unit;
			this.value = value;
		}

		public Length ToLength()
		{
			LengthUnit lengthUnit = (this.unit == Dimension.Unit.Percent) ? LengthUnit.Percent : LengthUnit.Pixel;
			return new Length(this.value, lengthUnit);
		}

		public static bool operator ==(Dimension lhs, Dimension rhs)
		{
			return lhs.value == rhs.value && lhs.unit == rhs.unit;
		}

		public static bool operator !=(Dimension lhs, Dimension rhs)
		{
			return !(lhs == rhs);
		}

		public bool Equals(Dimension other)
		{
			return other == this;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is Dimension);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Dimension lhs = (Dimension)obj;
				result = (lhs == this);
			}
			return result;
		}

		public override int GetHashCode()
		{
			int num = -799583767;
			num = num * -1521134295 + this.unit.GetHashCode();
			return num * -1521134295 + this.value.GetHashCode();
		}

		public override string ToString()
		{
			string str = string.Empty;
			Dimension.Unit unit = this.unit;
			Dimension.Unit unit2 = unit;
			if (unit2 != Dimension.Unit.Pixel)
			{
				if (unit2 == Dimension.Unit.Percent)
				{
					str = "%";
				}
			}
			else
			{
				str = "px";
			}
			return this.value.ToString(CultureInfo.InvariantCulture.NumberFormat) + str;
		}
	}
}
