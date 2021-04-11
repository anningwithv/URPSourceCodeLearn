using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	internal static class StyleSelectorHelper
	{
		public static MatchResultInfo MatchesSelector(VisualElement element, StyleSelector selector)
		{
			bool flag = true;
			StyleSelectorPart[] parts = selector.parts;
			int num = parts.Length;
			int num2 = 0;
			while (num2 < num & flag)
			{
				switch (parts[num2].type)
				{
				case StyleSelectorType.Wildcard:
					break;
				case StyleSelectorType.Type:
					flag = (element.typeName == parts[num2].value);
					break;
				case StyleSelectorType.Class:
					flag = element.ClassListContains(parts[num2].value);
					break;
				case StyleSelectorType.PseudoClass:
					break;
				case StyleSelectorType.RecursivePseudoClass:
					goto IL_C7;
				case StyleSelectorType.ID:
					flag = (element.name == parts[num2].value);
					break;
				case StyleSelectorType.Predicate:
				{
					UQuery.IVisualPredicateWrapper visualPredicateWrapper = parts[num2].tempData as UQuery.IVisualPredicateWrapper;
					flag = (visualPredicateWrapper != null && visualPredicateWrapper.Predicate(element));
					break;
				}
				default:
					goto IL_C7;
				}
				IL_CB:
				num2++;
				continue;
				IL_C7:
				flag = false;
				goto IL_CB;
			}
			int num3 = 0;
			int num4 = 0;
			bool flag2 = flag;
			bool flag3 = flag2 && selector.pseudoStateMask != 0;
			if (flag3)
			{
				flag = ((selector.pseudoStateMask & (int)element.pseudoStates) == selector.pseudoStateMask);
				bool flag4 = flag;
				if (flag4)
				{
					num4 = selector.pseudoStateMask;
				}
				else
				{
					num3 = selector.pseudoStateMask;
				}
			}
			bool flag5 = flag2 && selector.negatedPseudoStateMask != 0;
			if (flag5)
			{
				flag &= ((selector.negatedPseudoStateMask & (int)(~(int)element.pseudoStates)) == selector.negatedPseudoStateMask);
				bool flag6 = flag;
				if (flag6)
				{
					num3 |= selector.negatedPseudoStateMask;
				}
				else
				{
					num4 |= selector.negatedPseudoStateMask;
				}
			}
			return new MatchResultInfo(flag, (PseudoStates)num3, (PseudoStates)num4);
		}

		public static bool MatchRightToLeft(VisualElement element, StyleComplexSelector complexSelector, Action<VisualElement, MatchResultInfo> processResult)
		{
			VisualElement visualElement = element;
			int i = complexSelector.selectors.Length - 1;
			VisualElement visualElement2 = null;
			int num = -1;
			bool result;
			while (i >= 0)
			{
				bool flag = visualElement == null;
				if (flag)
				{
					break;
				}
				MatchResultInfo matchResultInfo = StyleSelectorHelper.MatchesSelector(visualElement, complexSelector.selectors[i]);
				processResult(visualElement, matchResultInfo);
				bool flag2 = !matchResultInfo.success;
				if (flag2)
				{
					bool flag3 = i < complexSelector.selectors.Length - 1 && complexSelector.selectors[i + 1].previousRelationship == StyleSelectorRelationship.Descendent;
					if (flag3)
					{
						visualElement = visualElement.parent;
					}
					else
					{
						bool flag4 = visualElement2 != null;
						if (!flag4)
						{
							break;
						}
						visualElement = visualElement2;
						i = num;
					}
				}
				else
				{
					bool flag5 = i < complexSelector.selectors.Length - 1 && complexSelector.selectors[i + 1].previousRelationship == StyleSelectorRelationship.Descendent;
					if (flag5)
					{
						visualElement2 = visualElement.parent;
						num = i;
					}
					bool flag6 = --i < 0;
					if (flag6)
					{
						result = true;
						return result;
					}
					visualElement = visualElement.parent;
				}
			}
			result = false;
			return result;
		}

		private static void FastLookup(IDictionary<string, StyleComplexSelector> table, List<SelectorMatchRecord> matchedSelectors, StyleMatchingContext context, string input, ref SelectorMatchRecord record)
		{
			StyleComplexSelector nextInTable;
			bool flag = table.TryGetValue(input, out nextInTable);
			if (flag)
			{
				while (nextInTable != null)
				{
					bool flag2 = StyleSelectorHelper.MatchRightToLeft(context.currentElement, nextInTable, context.processResult);
					if (flag2)
					{
						record.complexSelector = nextInTable;
						matchedSelectors.Add(record);
					}
					nextInTable = nextInTable.nextInTable;
				}
			}
		}

		public static void FindMatches(StyleMatchingContext context, List<SelectorMatchRecord> matchedSelectors)
		{
			Debug.Assert(matchedSelectors.Count == 0);
			Debug.Assert(context.currentElement != null, "context.currentElement != null");
			VisualElement currentElement = context.currentElement;
			for (int i = 0; i < context.styleSheetStack.Count; i++)
			{
				StyleSheet styleSheet = context.styleSheetStack[i];
				SelectorMatchRecord selectorMatchRecord = new SelectorMatchRecord(styleSheet, i);
				StyleSelectorHelper.FastLookup(styleSheet.orderedTypeSelectors, matchedSelectors, context, currentElement.typeName, ref selectorMatchRecord);
				StyleSelectorHelper.FastLookup(styleSheet.orderedTypeSelectors, matchedSelectors, context, "*", ref selectorMatchRecord);
				bool flag = !string.IsNullOrEmpty(currentElement.name);
				if (flag)
				{
					StyleSelectorHelper.FastLookup(styleSheet.orderedNameSelectors, matchedSelectors, context, currentElement.name, ref selectorMatchRecord);
				}
				foreach (string current in currentElement.GetClassesForIteration())
				{
					StyleSelectorHelper.FastLookup(styleSheet.orderedClassSelectors, matchedSelectors, context, current, ref selectorMatchRecord);
				}
			}
		}
	}
}
