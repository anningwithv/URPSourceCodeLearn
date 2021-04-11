using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public class UxmlBoolAttributeDescription : TypedUxmlAttributeDescription<bool>
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly UxmlBoolAttributeDescription.<>c <>9 = new UxmlBoolAttributeDescription.<>c();

			public static Func<string, bool, bool> <>9__3_0;

			public static Func<string, bool, bool> <>9__4_0;

			internal bool <GetValueFromBag>b__3_0(string s, bool b)
			{
				return UxmlBoolAttributeDescription.ConvertValueToBool(s, b);
			}

			internal bool <TryGetValueFromBag>b__4_0(string s, bool b)
			{
				return UxmlBoolAttributeDescription.ConvertValueToBool(s, b);
			}
		}

		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString().ToLower();
			}
		}

		public UxmlBoolAttributeDescription()
		{
			base.type = "boolean";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = false;
		}

		public override bool GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			Func<string, bool, bool> arg_29_3;
			if ((arg_29_3 = UxmlBoolAttributeDescription.<>c.<>9__3_0) == null)
			{
				arg_29_3 = (UxmlBoolAttributeDescription.<>c.<>9__3_0 = new Func<string, bool, bool>(UxmlBoolAttributeDescription.<>c.<>9.<GetValueFromBag>b__3_0));
			}
			return base.GetValueFromBag<bool>(bag, cc, arg_29_3, base.defaultValue);
		}

		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref bool value)
		{
			Func<string, bool, bool> arg_2A_3;
			if ((arg_2A_3 = UxmlBoolAttributeDescription.<>c.<>9__4_0) == null)
			{
				arg_2A_3 = (UxmlBoolAttributeDescription.<>c.<>9__4_0 = new Func<string, bool, bool>(UxmlBoolAttributeDescription.<>c.<>9.<TryGetValueFromBag>b__4_0));
			}
			return base.TryGetValueFromBag<bool>(bag, cc, arg_2A_3, base.defaultValue, ref value);
		}

		private static bool ConvertValueToBool(string v, bool defaultValue)
		{
			bool flag2;
			bool flag = v == null || !bool.TryParse(v, out flag2);
			bool result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = flag2;
			}
			return result;
		}
	}
}
