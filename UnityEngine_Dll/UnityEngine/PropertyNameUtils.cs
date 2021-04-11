using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Utilities/PropertyName.h")]
	internal class PropertyNameUtils
	{
		[FreeFunction]
		public static PropertyName PropertyNameFromString([Unmarshalled] string name)
		{
			PropertyName result;
			PropertyNameUtils.PropertyNameFromString_Injected(name, out result);
			return result;
		}

		[FreeFunction]
		public static string StringFromPropertyName(PropertyName propertyName)
		{
			return PropertyNameUtils.StringFromPropertyName_Injected(ref propertyName);
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ConflictCountForID(int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PropertyNameFromString_Injected(string name, out PropertyName ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string StringFromPropertyName_Injected(ref PropertyName propertyName);
	}
}
