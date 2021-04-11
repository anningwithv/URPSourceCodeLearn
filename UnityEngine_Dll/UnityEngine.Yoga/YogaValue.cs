using System;

namespace UnityEngine.Yoga
{
	internal struct YogaValue
	{
		private float value;

		private YogaUnit unit;

		public YogaUnit Unit
		{
			get
			{
				return this.unit;
			}
		}

		public float Value
		{
			get
			{
				return this.value;
			}
		}

		public static YogaValue Point(float value)
		{
			return new YogaValue
			{
				value = value,
				unit = (YogaConstants.IsUndefined(value) ? YogaUnit.Undefined : YogaUnit.Point)
			};
		}

		public bool Equals(YogaValue other)
		{
			return this.Unit == other.Unit && (this.Value.Equals(other.Value) || this.Unit == YogaUnit.Undefined);
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is YogaValue && this.Equals((YogaValue)obj);
		}

		public override int GetHashCode()
		{
			return this.Value.GetHashCode() * 397 ^ (int)this.Unit;
		}

		public static YogaValue Undefined()
		{
			return new YogaValue
			{
				value = float.NaN,
				unit = YogaUnit.Undefined
			};
		}

		public static YogaValue Auto()
		{
			return new YogaValue
			{
				value = float.NaN,
				unit = YogaUnit.Auto
			};
		}

		public static YogaValue Percent(float value)
		{
			return new YogaValue
			{
				value = value,
				unit = (YogaConstants.IsUndefined(value) ? YogaUnit.Undefined : YogaUnit.Percent)
			};
		}

		public static implicit operator YogaValue(float pointValue)
		{
			return YogaValue.Point(pointValue);
		}

		internal static YogaValue MarshalValue(YogaValue value)
		{
			return value;
		}
	}
}
