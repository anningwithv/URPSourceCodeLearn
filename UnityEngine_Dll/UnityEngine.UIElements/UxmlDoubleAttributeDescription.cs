using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public class UxmlDoubleAttributeDescription : TypedUxmlAttributeDescription<double>
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly UxmlDoubleAttributeDescription.<>c <>9 = new UxmlDoubleAttributeDescription.<>c();

			public static Func<string, double, double> <>9__3_0;

			public static Func<string, double, double> <>9__4_0;

			internal double <GetValueFromBag>b__3_0(string s, double d)
			{
				return UxmlDoubleAttributeDescription.ConvertValueToDouble(s, d);
			}

			internal double <TryGetValueFromBag>b__4_0(string s, double d)
			{
				return UxmlDoubleAttributeDescription.ConvertValueToDouble(s, d);
			}
		}

		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		public UxmlDoubleAttributeDescription()
		{
			base.type = "double";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0.0;
		}

		public override double GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			Func<string, double, double> arg_29_3;
			if ((arg_29_3 = UxmlDoubleAttributeDescription.<>c.<>9__3_0) == null)
			{
				arg_29_3 = (UxmlDoubleAttributeDescription.<>c.<>9__3_0 = new Func<string, double, double>(UxmlDoubleAttributeDescription.<>c.<>9.<GetValueFromBag>b__3_0));
			}
			return base.GetValueFromBag<double>(bag, cc, arg_29_3, base.defaultValue);
		}

		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref double value)
		{
			Func<string, double, double> arg_2A_3;
			if ((arg_2A_3 = UxmlDoubleAttributeDescription.<>c.<>9__4_0) == null)
			{
				arg_2A_3 = (UxmlDoubleAttributeDescription.<>c.<>9__4_0 = new Func<string, double, double>(UxmlDoubleAttributeDescription.<>c.<>9.<TryGetValueFromBag>b__4_0));
			}
			return base.TryGetValueFromBag<double>(bag, cc, arg_2A_3, base.defaultValue, ref value);
		}

		private static double ConvertValueToDouble(string v, double defaultValue)
		{
			double num;
			bool flag = v == null || !double.TryParse(v, out num);
			double result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = num;
			}
			return result;
		}
	}
}
