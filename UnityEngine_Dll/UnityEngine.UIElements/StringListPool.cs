using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal static class StringListPool
	{
		private static ObjectPool<List<string>> pool = new ObjectPool<List<string>>(20);

		public static List<string> Get()
		{
			return StringListPool.pool.Get();
		}

		public static void Release(List<string> elements)
		{
			elements.Clear();
			StringListPool.pool.Release(elements);
		}
	}
}
