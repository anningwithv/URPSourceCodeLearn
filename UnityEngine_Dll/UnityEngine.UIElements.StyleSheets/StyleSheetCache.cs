using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	internal static class StyleSheetCache
	{
		private struct SheetHandleKey
		{
			public readonly int sheetInstanceID;

			public readonly int index;

			public SheetHandleKey(StyleSheet sheet, int index)
			{
				this.sheetInstanceID = sheet.GetInstanceID();
				this.index = index;
			}
		}

		private class SheetHandleKeyComparer : IEqualityComparer<StyleSheetCache.SheetHandleKey>
		{
			public bool Equals(StyleSheetCache.SheetHandleKey x, StyleSheetCache.SheetHandleKey y)
			{
				return x.sheetInstanceID == y.sheetInstanceID && x.index == y.index;
			}

			public int GetHashCode(StyleSheetCache.SheetHandleKey key)
			{
				return key.sheetInstanceID.GetHashCode() ^ key.index.GetHashCode();
			}
		}

		private static StyleSheetCache.SheetHandleKeyComparer s_Comparer = new StyleSheetCache.SheetHandleKeyComparer();

		private static Dictionary<StyleSheetCache.SheetHandleKey, StylePropertyId[]> s_RulePropertyIdsCache = new Dictionary<StyleSheetCache.SheetHandleKey, StylePropertyId[]>(StyleSheetCache.s_Comparer);

		internal static void ClearCaches()
		{
			StyleSheetCache.s_RulePropertyIdsCache.Clear();
		}

		internal static StylePropertyId[] GetPropertyIds(StyleSheet sheet, int ruleIndex)
		{
			StyleSheetCache.SheetHandleKey key = new StyleSheetCache.SheetHandleKey(sheet, ruleIndex);
			StylePropertyId[] array;
			bool flag = !StyleSheetCache.s_RulePropertyIdsCache.TryGetValue(key, out array);
			if (flag)
			{
				StyleRule styleRule = sheet.rules[ruleIndex];
				array = new StylePropertyId[styleRule.properties.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = StyleSheetCache.GetPropertyId(styleRule, i);
				}
				StyleSheetCache.s_RulePropertyIdsCache.Add(key, array);
			}
			return array;
		}

		internal static StylePropertyId[] GetPropertyIds(StyleRule rule)
		{
			StylePropertyId[] array = new StylePropertyId[rule.properties.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = StyleSheetCache.GetPropertyId(rule, i);
			}
			return array;
		}

		private static StylePropertyId GetPropertyId(StyleRule rule, int index)
		{
			StyleProperty styleProperty = rule.properties[index];
			string name = styleProperty.name;
			StylePropertyId result;
			bool flag = !StylePropertyUtil.s_NameToId.TryGetValue(name, out result);
			if (flag)
			{
				result = (styleProperty.isCustomProperty ? StylePropertyId.Custom : StylePropertyId.Unknown);
			}
			return result;
		}
	}
}
