using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public class UxmlStringAttributeDescription : TypedUxmlAttributeDescription<string>
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly UxmlStringAttributeDescription.<>c <>9 = new UxmlStringAttributeDescription.<>c();

			public static Func<string, string, string> <>9__3_0;

			public static Func<string, string, string> <>9__4_0;

			internal string <GetValueFromBag>b__3_0(string s, string t)
			{
				return s;
			}

			internal string <TryGetValueFromBag>b__4_0(string s, string t)
			{
				return s;
			}
		}

		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue;
			}
		}

		public UxmlStringAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = "";
		}

		public override string GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			Func<string, string, string> arg_29_3;
			if ((arg_29_3 = UxmlStringAttributeDescription.<>c.<>9__3_0) == null)
			{
				arg_29_3 = (UxmlStringAttributeDescription.<>c.<>9__3_0 = new Func<string, string, string>(UxmlStringAttributeDescription.<>c.<>9.<GetValueFromBag>b__3_0));
			}
			return base.GetValueFromBag<string>(bag, cc, arg_29_3, base.defaultValue);
		}

		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref string value)
		{
			Func<string, string, string> arg_2A_3;
			if ((arg_2A_3 = UxmlStringAttributeDescription.<>c.<>9__4_0) == null)
			{
				arg_2A_3 = (UxmlStringAttributeDescription.<>c.<>9__4_0 = new Func<string, string, string>(UxmlStringAttributeDescription.<>c.<>9.<TryGetValueFromBag>b__4_0));
			}
			return base.TryGetValueFromBag<string>(bag, cc, arg_2A_3, base.defaultValue, ref value);
		}
	}
}
