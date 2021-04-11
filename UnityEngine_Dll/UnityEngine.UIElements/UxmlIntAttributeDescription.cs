using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public class UxmlIntAttributeDescription : TypedUxmlAttributeDescription<int>
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly UxmlIntAttributeDescription.<>c <>9 = new UxmlIntAttributeDescription.<>c();

			public static Func<string, int, int> <>9__3_0;

			public static Func<string, int, int> <>9__4_0;

			internal int <GetValueFromBag>b__3_0(string s, int i)
			{
				return UxmlIntAttributeDescription.ConvertValueToInt(s, i);
			}

			internal int <TryGetValueFromBag>b__4_0(string s, int i)
			{
				return UxmlIntAttributeDescription.ConvertValueToInt(s, i);
			}
		}

		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		public UxmlIntAttributeDescription()
		{
			base.type = "int";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0;
		}

		public override int GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			Func<string, int, int> arg_29_3;
			if ((arg_29_3 = UxmlIntAttributeDescription.<>c.<>9__3_0) == null)
			{
				arg_29_3 = (UxmlIntAttributeDescription.<>c.<>9__3_0 = new Func<string, int, int>(UxmlIntAttributeDescription.<>c.<>9.<GetValueFromBag>b__3_0));
			}
			return base.GetValueFromBag<int>(bag, cc, arg_29_3, base.defaultValue);
		}

		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref int value)
		{
			Func<string, int, int> arg_2A_3;
			if ((arg_2A_3 = UxmlIntAttributeDescription.<>c.<>9__4_0) == null)
			{
				arg_2A_3 = (UxmlIntAttributeDescription.<>c.<>9__4_0 = new Func<string, int, int>(UxmlIntAttributeDescription.<>c.<>9.<TryGetValueFromBag>b__4_0));
			}
			return base.TryGetValueFromBag<int>(bag, cc, arg_2A_3, base.defaultValue, ref value);
		}

		private static int ConvertValueToInt(string v, int defaultValue)
		{
			int num;
			bool flag = v == null || !int.TryParse(v, out num);
			int result;
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
