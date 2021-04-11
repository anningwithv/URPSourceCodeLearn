using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal static class StyleCache
	{
		private static Dictionary<long, ComputedStyle> s_ComputedStyleCache = new Dictionary<long, ComputedStyle>();

		private static Dictionary<int, StyleVariableContext> s_StyleVariableContextCache = new Dictionary<int, StyleVariableContext>();

		public static bool TryGetValue(long hash, out ComputedStyle data)
		{
			return StyleCache.s_ComputedStyleCache.TryGetValue(hash, out data);
		}

		public static void SetValue(long hash, ComputedStyle data)
		{
			StyleCache.s_ComputedStyleCache[hash] = data;
		}

		public static bool TryGetValue(int hash, out StyleVariableContext data)
		{
			return StyleCache.s_StyleVariableContextCache.TryGetValue(hash, out data);
		}

		public static void SetValue(int hash, StyleVariableContext data)
		{
			StyleCache.s_StyleVariableContextCache[hash] = data;
		}

		public static void ClearStyleCache()
		{
			StyleCache.s_ComputedStyleCache.Clear();
			StyleCache.s_StyleVariableContextCache.Clear();
		}
	}
}
