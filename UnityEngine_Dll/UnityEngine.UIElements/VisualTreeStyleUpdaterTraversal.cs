using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	internal class VisualTreeStyleUpdaterTraversal : HierarchyTraversal
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly VisualTreeStyleUpdaterTraversal.<>c <>9 = new VisualTreeStyleUpdaterTraversal.<>c();

			public static Comparison<SelectorMatchRecord> <>9__18_0;

			internal int <ProcessMatchedRules>b__18_0(SelectorMatchRecord a, SelectorMatchRecord b)
			{
				return SelectorMatchRecord.Compare(a, b);
			}
		}

		private StyleVariableContext m_ProcessVarContext = new StyleVariableContext();

		private HashSet<VisualElement> m_UpdateList = new HashSet<VisualElement>();

		private HashSet<VisualElement> m_ParentList = new HashSet<VisualElement>();

		private List<SelectorMatchRecord> m_TempMatchResults = new List<SelectorMatchRecord>();

		private StyleMatchingContext m_StyleMatchingContext;

		private StylePropertyReader m_StylePropertyReader;

		private float currentPixelsPerPoint
		{
			get;
			set;
		}

		public void PrepareTraversal(float pixelsPerPoint)
		{
			this.currentPixelsPerPoint = pixelsPerPoint;
		}

		public void AddChangedElement(VisualElement ve)
		{
			this.m_UpdateList.Add(ve);
			this.PropagateToChildren(ve);
			this.PropagateToParents(ve);
		}

		public void Clear()
		{
			this.m_UpdateList.Clear();
			this.m_ParentList.Clear();
			this.m_TempMatchResults.Clear();
		}

		private void PropagateToChildren(VisualElement ve)
		{
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				bool flag = this.m_UpdateList.Add(visualElement);
				bool flag2 = flag;
				if (flag2)
				{
					this.PropagateToChildren(visualElement);
				}
			}
		}

		private void PropagateToParents(VisualElement ve)
		{
			for (VisualElement parent = ve.hierarchy.parent; parent != null; parent = parent.hierarchy.parent)
			{
				bool flag = !this.m_ParentList.Add(parent);
				if (flag)
				{
					break;
				}
			}
		}

		private static void OnProcessMatchResult(VisualElement current, MatchResultInfo info)
		{
			current.triggerPseudoMask |= info.triggerPseudoMask;
			current.dependencyPseudoMask |= info.dependencyPseudoMask;
		}

		public override void TraverseRecursive(VisualElement element, int depth)
		{
			bool flag = this.ShouldSkipElement(element);
			if (!flag)
			{
				bool flag2 = this.m_UpdateList.Contains(element);
				bool flag3 = flag2;
				if (flag3)
				{
					element.triggerPseudoMask = (PseudoStates)0;
					element.dependencyPseudoMask = (PseudoStates)0;
				}
				int count = this.m_StyleMatchingContext.styleSheetStack.Count;
				bool flag4 = element.styleSheetList != null;
				if (flag4)
				{
					for (int i = 0; i < element.styleSheetList.Count; i++)
					{
						StyleSheet styleSheet = element.styleSheetList[i];
						bool flag5 = styleSheet.flattenedRecursiveImports != null;
						if (flag5)
						{
							for (int j = 0; j < styleSheet.flattenedRecursiveImports.Count; j++)
							{
								this.m_StyleMatchingContext.styleSheetStack.Add(styleSheet.flattenedRecursiveImports[j]);
							}
						}
						this.m_StyleMatchingContext.styleSheetStack.Add(styleSheet);
					}
				}
				int customPropertiesCount = element.computedStyle.customPropertiesCount;
				bool flag6 = flag2;
				if (flag6)
				{
					this.m_StyleMatchingContext.currentElement = element;
					StyleSelectorHelper.FindMatches(this.m_StyleMatchingContext, this.m_TempMatchResults);
					this.ProcessMatchedRules(element, this.m_TempMatchResults);
					element.inheritedStylesHash = element.computedStyle.inheritedData.GetHashCode();
					this.m_StyleMatchingContext.currentElement = null;
					this.m_TempMatchResults.Clear();
				}
				else
				{
					this.m_StyleMatchingContext.variableContext = element.variableContext;
				}
				bool flag7 = flag2 && (customPropertiesCount > 0 || element.computedStyle.customPropertiesCount > 0);
				if (flag7)
				{
					using (CustomStyleResolvedEvent pooled = EventBase<CustomStyleResolvedEvent>.GetPooled())
					{
						pooled.target = element;
						element.SendEvent(pooled);
					}
				}
				base.Recurse(element, depth);
				bool flag8 = this.m_StyleMatchingContext.styleSheetStack.Count > count;
				if (flag8)
				{
					this.m_StyleMatchingContext.styleSheetStack.RemoveRange(count, this.m_StyleMatchingContext.styleSheetStack.Count - count);
				}
			}
		}

		private bool ShouldSkipElement(VisualElement element)
		{
			return !this.m_ParentList.Contains(element) && !this.m_UpdateList.Contains(element);
		}

		private void ProcessMatchedRules(VisualElement element, List<SelectorMatchRecord> matchingSelectors)
		{
			Comparison<SelectorMatchRecord> arg_21_1;
			if ((arg_21_1 = VisualTreeStyleUpdaterTraversal.<>c.<>9__18_0) == null)
			{
				arg_21_1 = (VisualTreeStyleUpdaterTraversal.<>c.<>9__18_0 = new Comparison<SelectorMatchRecord>(VisualTreeStyleUpdaterTraversal.<>c.<>9.<ProcessMatchedRules>b__18_0));
			}
			matchingSelectors.Sort(arg_21_1);
			long num = (long)element.fullTypeName.GetHashCode();
			num = (num * 397L ^ (long)this.currentPixelsPerPoint.GetHashCode());
			int variableHash = this.m_StyleMatchingContext.variableContext.GetVariableHash();
			int num2 = 0;
			foreach (SelectorMatchRecord current in matchingSelectors)
			{
				num2 += current.complexSelector.rule.customPropertiesCount;
			}
			bool flag = num2 > 0;
			if (flag)
			{
				this.m_ProcessVarContext.AddInitialRange(this.m_StyleMatchingContext.variableContext);
			}
			foreach (SelectorMatchRecord current2 in matchingSelectors)
			{
				StyleRule rule = current2.complexSelector.rule;
				int specificity = current2.complexSelector.specificity;
				num = (num * 397L ^ (long)rule.GetHashCode());
				num = (num * 397L ^ (long)specificity);
				bool flag2 = rule.customPropertiesCount > 0;
				if (flag2)
				{
					this.ProcessMatchedVariables(current2.sheet, rule);
				}
			}
			VisualElement parent = element.hierarchy.parent;
			int num3 = (parent != null) ? parent.inheritedStylesHash : 0;
			num = (num * 397L ^ (long)num3);
			int num4 = variableHash;
			bool flag3 = num2 > 0;
			if (flag3)
			{
				num4 = this.m_ProcessVarContext.GetVariableHash();
			}
			num = (num * 397L ^ (long)num4);
			bool flag4 = variableHash != num4;
			if (flag4)
			{
				StyleVariableContext styleVariableContext;
				bool flag5 = !StyleCache.TryGetValue(num4, out styleVariableContext);
				if (flag5)
				{
					styleVariableContext = new StyleVariableContext(this.m_ProcessVarContext);
					StyleCache.SetValue(num4, styleVariableContext);
				}
				this.m_StyleMatchingContext.variableContext = styleVariableContext;
			}
			element.variableContext = this.m_StyleMatchingContext.variableContext;
			this.m_ProcessVarContext.Clear();
			ComputedStyle computedStyle;
			bool flag6 = StyleCache.TryGetValue(num, out computedStyle);
			if (flag6)
			{
				element.SetSharedStyles(computedStyle);
			}
			else
			{
				ComputedStyle parentStyle = (parent != null) ? parent.computedStyle : null;
				computedStyle = ComputedStyle.Create(parentStyle, true);
				float scaledPixelsPerPoint = element.scaledPixelsPerPoint;
				foreach (SelectorMatchRecord current3 in matchingSelectors)
				{
					this.m_StylePropertyReader.SetContext(current3.sheet, current3.complexSelector, this.m_StyleMatchingContext.variableContext, scaledPixelsPerPoint);
					computedStyle.ApplyProperties(this.m_StylePropertyReader, parentStyle);
				}
				computedStyle.FinalizeApply(parentStyle);
				StyleCache.SetValue(num, computedStyle);
				element.SetSharedStyles(computedStyle);
			}
		}

		private void ProcessMatchedVariables(StyleSheet sheet, StyleRule rule)
		{
			StyleProperty[] properties = rule.properties;
			for (int i = 0; i < properties.Length; i++)
			{
				StyleProperty styleProperty = properties[i];
				bool isCustomProperty = styleProperty.isCustomProperty;
				if (isCustomProperty)
				{
					StyleVariable sv = new StyleVariable(styleProperty.name, sheet, styleProperty.values);
					this.m_ProcessVarContext.Add(sv);
				}
			}
		}

		public VisualTreeStyleUpdaterTraversal()
		{
			this.<currentPixelsPerPoint>k__BackingField = 1f;
			this.m_StyleMatchingContext = new StyleMatchingContext(new Action<VisualElement, MatchResultInfo>(VisualTreeStyleUpdaterTraversal.OnProcessMatchResult));
			this.m_StylePropertyReader = new StylePropertyReader();
			base..ctor();
		}
	}
}
