using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Scripting/NoAllocHelpers.bindings.h")]
	internal sealed class NoAllocHelpers
	{
		public static void ResizeList<T>(List<T> list, int size)
		{
			bool flag = list == null;
			if (flag)
			{
				throw new ArgumentNullException("list");
			}
			bool flag2 = size < 0 || size > list.Capacity;
			if (flag2)
			{
				throw new ArgumentException("invalid size to resize.", "list");
			}
			bool flag3 = size != list.Count;
			if (flag3)
			{
				NoAllocHelpers.Internal_ResizeList(list, size);
			}
		}

		public static void EnsureListElemCount<T>(List<T> list, int count)
		{
			list.Clear();
			bool flag = list.Capacity < count;
			if (flag)
			{
				list.Capacity = count;
			}
			NoAllocHelpers.ResizeList<T>(list, count);
		}

		public static int SafeLength(Array values)
		{
			return (values != null) ? values.Length : 0;
		}

		public static int SafeLength<T>(List<T> values)
		{
			return (values != null) ? values.Count : 0;
		}

		public static T[] ExtractArrayFromListT<T>(List<T> list)
		{
			return (T[])NoAllocHelpers.ExtractArrayFromList(list);
		}

		[FreeFunction("NoAllocHelpers_Bindings::Internal_ResizeList")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_ResizeList(object list, int size);

		[FreeFunction("NoAllocHelpers_Bindings::ExtractArrayFromList")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Array ExtractArrayFromList(object list);
	}
}
