using System;

namespace UnityEngine.UIElements.StyleSheets
{
	internal struct SelectorMatchRecord
	{
		public StyleSheet sheet;

		public int styleSheetIndexInStack;

		public StyleComplexSelector complexSelector;

		public SelectorMatchRecord(StyleSheet sheet, int styleSheetIndexInStack)
		{
			this = default(SelectorMatchRecord);
			this.sheet = sheet;
			this.styleSheetIndexInStack = styleSheetIndexInStack;
		}

		public static int Compare(SelectorMatchRecord a, SelectorMatchRecord b)
		{
			bool flag = a.sheet.isUnityStyleSheet != b.sheet.isUnityStyleSheet;
			int result;
			if (flag)
			{
				result = (a.sheet.isUnityStyleSheet ? -1 : 1);
			}
			else
			{
				int num = a.complexSelector.specificity.CompareTo(b.complexSelector.specificity);
				bool flag2 = num == 0;
				if (flag2)
				{
					num = a.styleSheetIndexInStack.CompareTo(b.styleSheetIndexInStack);
				}
				bool flag3 = num == 0;
				if (flag3)
				{
					num = a.complexSelector.orderInStyleSheet.CompareTo(b.complexSelector.orderInStyleSheet);
				}
				result = num;
			}
			return result;
		}
	}
}
