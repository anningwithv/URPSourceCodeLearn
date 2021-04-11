using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	[Serializable]
	internal class StyleComplexSelector
	{
		private struct PseudoStateData
		{
			public readonly PseudoStates state;

			public readonly bool negate;

			public PseudoStateData(PseudoStates state, bool negate)
			{
				this.state = state;
				this.negate = negate;
			}
		}

		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly StyleComplexSelector.<>c <>9 = new StyleComplexSelector.<>c();

			public static Func<StyleSelector, string> <>9__20_0;

			internal string <ToString>b__20_0(StyleSelector x)
			{
				return x.ToString();
			}
		}

		[SerializeField]
		private int m_Specificity;

		[SerializeField]
		private StyleSelector[] m_Selectors;

		[SerializeField]
		internal int ruleIndex;

		[NonSerialized]
		internal StyleComplexSelector nextInTable;

		[NonSerialized]
		internal int orderInStyleSheet;

		private static Dictionary<string, StyleComplexSelector.PseudoStateData> s_PseudoStates;

		public int specificity
		{
			get
			{
				return this.m_Specificity;
			}
			internal set
			{
				this.m_Specificity = value;
			}
		}

		public StyleRule rule
		{
			get;
			internal set;
		}

		public bool isSimple
		{
			get
			{
				return this.selectors.Length == 1;
			}
		}

		public StyleSelector[] selectors
		{
			get
			{
				return this.m_Selectors;
			}
			internal set
			{
				this.m_Selectors = value;
			}
		}

		internal void CachePseudoStateMasks()
		{
			bool flag = StyleComplexSelector.s_PseudoStates == null;
			if (flag)
			{
				StyleComplexSelector.s_PseudoStates = new Dictionary<string, StyleComplexSelector.PseudoStateData>();
				StyleComplexSelector.s_PseudoStates["active"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Active, false);
				StyleComplexSelector.s_PseudoStates["hover"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Hover, false);
				StyleComplexSelector.s_PseudoStates["checked"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Checked, false);
				StyleComplexSelector.s_PseudoStates["selected"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Checked, false);
				StyleComplexSelector.s_PseudoStates["disabled"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Disabled, false);
				StyleComplexSelector.s_PseudoStates["focus"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Focus, false);
				StyleComplexSelector.s_PseudoStates["root"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Root, false);
				StyleComplexSelector.s_PseudoStates["inactive"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Active, true);
				StyleComplexSelector.s_PseudoStates["enabled"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Disabled, true);
			}
			int i = 0;
			int num = this.selectors.Length;
			while (i < num)
			{
				StyleSelector styleSelector = this.selectors[i];
				StyleSelectorPart[] parts = styleSelector.parts;
				PseudoStates pseudoStates = (PseudoStates)0;
				PseudoStates pseudoStates2 = (PseudoStates)0;
				for (int j = 0; j < styleSelector.parts.Length; j++)
				{
					bool flag2 = styleSelector.parts[j].type == StyleSelectorType.PseudoClass;
					if (flag2)
					{
						StyleComplexSelector.PseudoStateData pseudoStateData;
						bool flag3 = StyleComplexSelector.s_PseudoStates.TryGetValue(parts[j].value, out pseudoStateData);
						if (flag3)
						{
							bool flag4 = !pseudoStateData.negate;
							if (flag4)
							{
								pseudoStates |= pseudoStateData.state;
							}
							else
							{
								pseudoStates2 |= pseudoStateData.state;
							}
						}
						else
						{
							Debug.LogWarningFormat("Unknown pseudo class \"{0}\"", new object[]
							{
								parts[j].value
							});
						}
					}
				}
				styleSelector.pseudoStateMask = (int)pseudoStates;
				styleSelector.negatedPseudoStateMask = (int)pseudoStates2;
				i++;
			}
		}

		public override string ToString()
		{
			string arg_3F_0 = "[{0}]";
			string arg_3A_0 = ", ";
			IEnumerable<StyleSelector> arg_30_0 = this.m_Selectors;
			Func<StyleSelector, string> arg_30_1;
			if ((arg_30_1 = StyleComplexSelector.<>c.<>9__20_0) == null)
			{
				arg_30_1 = (StyleComplexSelector.<>c.<>9__20_0 = new Func<StyleSelector, string>(StyleComplexSelector.<>c.<>9.<ToString>b__20_0));
			}
			return string.Format(arg_3F_0, string.Join(arg_3A_0, arg_30_0.Select(arg_30_1).ToArray<string>()));
		}
	}
}
