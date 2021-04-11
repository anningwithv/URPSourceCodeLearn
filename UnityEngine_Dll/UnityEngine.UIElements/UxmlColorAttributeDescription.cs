using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public class UxmlColorAttributeDescription : TypedUxmlAttributeDescription<Color>
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly UxmlColorAttributeDescription.<>c <>9 = new UxmlColorAttributeDescription.<>c();

			public static Func<string, Color, Color> <>9__3_0;

			public static Func<string, Color, Color> <>9__4_0;

			internal Color <GetValueFromBag>b__3_0(string s, Color color)
			{
				return UxmlColorAttributeDescription.ConvertValueToColor(s, color);
			}

			internal Color <TryGetValueFromBag>b__4_0(string s, Color color)
			{
				return UxmlColorAttributeDescription.ConvertValueToColor(s, color);
			}
		}

		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString();
			}
		}

		public UxmlColorAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = new Color(0f, 0f, 0f, 1f);
		}

		public override Color GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			Func<string, Color, Color> arg_29_3;
			if ((arg_29_3 = UxmlColorAttributeDescription.<>c.<>9__3_0) == null)
			{
				arg_29_3 = (UxmlColorAttributeDescription.<>c.<>9__3_0 = new Func<string, Color, Color>(UxmlColorAttributeDescription.<>c.<>9.<GetValueFromBag>b__3_0));
			}
			return base.GetValueFromBag<Color>(bag, cc, arg_29_3, base.defaultValue);
		}

		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref Color value)
		{
			Func<string, Color, Color> arg_2A_3;
			if ((arg_2A_3 = UxmlColorAttributeDescription.<>c.<>9__4_0) == null)
			{
				arg_2A_3 = (UxmlColorAttributeDescription.<>c.<>9__4_0 = new Func<string, Color, Color>(UxmlColorAttributeDescription.<>c.<>9.<TryGetValueFromBag>b__4_0));
			}
			return base.TryGetValueFromBag<Color>(bag, cc, arg_2A_3, base.defaultValue, ref value);
		}

		private static Color ConvertValueToColor(string v, Color defaultValue)
		{
			Color color;
			bool flag = v == null || !ColorUtility.TryParseHtmlString(v, out color);
			Color result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = color;
			}
			return result;
		}
	}
}
