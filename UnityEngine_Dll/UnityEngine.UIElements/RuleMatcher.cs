using System;

namespace UnityEngine.UIElements
{
	internal struct RuleMatcher
	{
		public StyleSheet sheet;

		public StyleComplexSelector complexSelector;

		public RuleMatcher(StyleSheet sheet, StyleComplexSelector complexSelector, int styleSheetIndexInStack)
		{
			this.sheet = sheet;
			this.complexSelector = complexSelector;
		}

		public override string ToString()
		{
			return this.complexSelector.ToString();
		}
	}
}
