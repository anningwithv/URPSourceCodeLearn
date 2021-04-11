using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public class UxmlFloatAttributeDescription : TypedUxmlAttributeDescription<float>
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly UxmlFloatAttributeDescription.<>c <>9 = new UxmlFloatAttributeDescription.<>c();

			public static Func<string, float, float> <>9__3_0;

			public static Func<string, float, float> <>9__4_0;

			internal float <GetValueFromBag>b__3_0(string s, float f)
			{
				return UxmlFloatAttributeDescription.ConvertValueToFloat(s, f);
			}

			internal float <TryGetValueFromBag>b__4_0(string s, float f)
			{
				return UxmlFloatAttributeDescription.ConvertValueToFloat(s, f);
			}
		}

		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		public UxmlFloatAttributeDescription()
		{
			base.type = "float";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0f;
		}

		public override float GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			Func<string, float, float> arg_29_3;
			if ((arg_29_3 = UxmlFloatAttributeDescription.<>c.<>9__3_0) == null)
			{
				arg_29_3 = (UxmlFloatAttributeDescription.<>c.<>9__3_0 = new Func<string, float, float>(UxmlFloatAttributeDescription.<>c.<>9.<GetValueFromBag>b__3_0));
			}
			return base.GetValueFromBag<float>(bag, cc, arg_29_3, base.defaultValue);
		}

		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref float value)
		{
			Func<string, float, float> arg_2A_3;
			if ((arg_2A_3 = UxmlFloatAttributeDescription.<>c.<>9__4_0) == null)
			{
				arg_2A_3 = (UxmlFloatAttributeDescription.<>c.<>9__4_0 = new Func<string, float, float>(UxmlFloatAttributeDescription.<>c.<>9.<TryGetValueFromBag>b__4_0));
			}
			return base.TryGetValueFromBag<float>(bag, cc, arg_2A_3, base.defaultValue, ref value);
		}

		private static float ConvertValueToFloat(string v, float defaultValue)
		{
			float num;
			bool flag = v == null || !float.TryParse(v, out num);
			float result;
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
