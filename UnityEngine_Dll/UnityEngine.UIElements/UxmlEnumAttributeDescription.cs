using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public class UxmlEnumAttributeDescription<T> : TypedUxmlAttributeDescription<T> where T : struct, IConvertible
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly UxmlEnumAttributeDescription<T>.<>c <>9 = new UxmlEnumAttributeDescription<T>.<>c();

			public static Func<string, T, T> <>9__3_0;

			public static Func<string, T, T> <>9__4_0;

			internal T <GetValueFromBag>b__3_0(string s, T convertible)
			{
				return UxmlEnumAttributeDescription<T>.ConvertValueToEnum<T>(s, convertible);
			}

			internal T <TryGetValueFromBag>b__4_0(string s, T convertible)
			{
				return UxmlEnumAttributeDescription<T>.ConvertValueToEnum<T>(s, convertible);
			}
		}

		public override string defaultValueAsString
		{
			get
			{
				T defaultValue = base.defaultValue;
				return defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		public UxmlEnumAttributeDescription()
		{
			bool flag = !typeof(T).IsEnum;
			if (flag)
			{
				throw new ArgumentException("T must be an enumerated type");
			}
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = Activator.CreateInstance<T>();
			UxmlEnumeration uxmlEnumeration = new UxmlEnumeration();
			List<string> list = new List<string>();
			using (IEnumerator enumerator = Enum.GetValues(typeof(T)).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					T t = (T)((object)enumerator.Current);
					list.Add(t.ToString(CultureInfo.InvariantCulture));
				}
			}
			uxmlEnumeration.values = list;
			base.restriction = uxmlEnumeration;
		}

		public override T GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			Func<string, T, T> arg_29_3;
			if ((arg_29_3 = UxmlEnumAttributeDescription<T>.<>c.<>9__3_0) == null)
			{
				arg_29_3 = (UxmlEnumAttributeDescription<T>.<>c.<>9__3_0 = new Func<string, T, T>(UxmlEnumAttributeDescription<T>.<>c.<>9.<GetValueFromBag>b__3_0));
			}
			return base.GetValueFromBag<T>(bag, cc, arg_29_3, base.defaultValue);
		}

		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref T value)
		{
			Func<string, T, T> arg_2A_3;
			if ((arg_2A_3 = UxmlEnumAttributeDescription<T>.<>c.<>9__4_0) == null)
			{
				arg_2A_3 = (UxmlEnumAttributeDescription<T>.<>c.<>9__4_0 = new Func<string, T, T>(UxmlEnumAttributeDescription<T>.<>c.<>9.<TryGetValueFromBag>b__4_0));
			}
			return base.TryGetValueFromBag<T>(bag, cc, arg_2A_3, base.defaultValue, ref value);
		}

		private static U ConvertValueToEnum<U>(string v, U defaultValue)
		{
			bool flag = v == null || !Enum.IsDefined(typeof(U), v);
			U result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				U u = (U)((object)Enum.Parse(typeof(U), v));
				result = u;
			}
			return result;
		}
	}
}
