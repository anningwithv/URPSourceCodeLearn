using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public class UxmlLongAttributeDescription : TypedUxmlAttributeDescription<long>
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly UxmlLongAttributeDescription.<>c <>9 = new UxmlLongAttributeDescription.<>c();

			public static Func<string, long, long> <>9__3_0;

			public static Func<string, long, long> <>9__4_0;

			internal long <GetValueFromBag>b__3_0(string s, long l)
			{
				return UxmlLongAttributeDescription.ConvertValueToLong(s, l);
			}

			internal long <TryGetValueFromBag>b__4_0(string s, long l)
			{
				return UxmlLongAttributeDescription.ConvertValueToLong(s, l);
			}
		}

		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		public UxmlLongAttributeDescription()
		{
			base.type = "long";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0L;
		}

		public override long GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			Func<string, long, long> arg_29_3;
			if ((arg_29_3 = UxmlLongAttributeDescription.<>c.<>9__3_0) == null)
			{
				arg_29_3 = (UxmlLongAttributeDescription.<>c.<>9__3_0 = new Func<string, long, long>(UxmlLongAttributeDescription.<>c.<>9.<GetValueFromBag>b__3_0));
			}
			return base.GetValueFromBag<long>(bag, cc, arg_29_3, base.defaultValue);
		}

		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref long value)
		{
			Func<string, long, long> arg_2A_3;
			if ((arg_2A_3 = UxmlLongAttributeDescription.<>c.<>9__4_0) == null)
			{
				arg_2A_3 = (UxmlLongAttributeDescription.<>c.<>9__4_0 = new Func<string, long, long>(UxmlLongAttributeDescription.<>c.<>9.<TryGetValueFromBag>b__4_0));
			}
			return base.TryGetValueFromBag<long>(bag, cc, arg_2A_3, base.defaultValue, ref value);
		}

		private static long ConvertValueToLong(string v, long defaultValue)
		{
			long num;
			bool flag = v == null || !long.TryParse(v, out num);
			long result;
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
