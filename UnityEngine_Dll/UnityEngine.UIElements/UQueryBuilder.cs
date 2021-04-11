using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public struct UQueryBuilder<T> : IEquatable<UQueryBuilder<T>> where T : VisualElement
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly UQueryBuilder<T>.<>c <>9 = new UQueryBuilder<T>.<>c();

			public static Func<T, bool> <>9__30_0;

			public static Func<T, bool> <>9__31_0;

			internal bool <Visible>b__30_0(T e)
			{
				return e.visible;
			}

			internal bool <NotVisible>b__31_0(T e)
			{
				return !e.visible;
			}
		}

		private List<StyleSelector> m_StyleSelectors;

		private List<StyleSelectorPart> m_Parts;

		private VisualElement m_Element;

		private List<RuleMatcher> m_Matchers;

		private StyleSelectorRelationship m_Relationship;

		private int pseudoStatesMask;

		private int negatedPseudoStatesMask;

		private List<StyleSelector> styleSelectors
		{
			get
			{
				List<StyleSelector> arg_19_0;
				if ((arg_19_0 = this.m_StyleSelectors) == null)
				{
					arg_19_0 = (this.m_StyleSelectors = new List<StyleSelector>());
				}
				return arg_19_0;
			}
		}

		private List<StyleSelectorPart> parts
		{
			get
			{
				List<StyleSelectorPart> arg_19_0;
				if ((arg_19_0 = this.m_Parts) == null)
				{
					arg_19_0 = (this.m_Parts = new List<StyleSelectorPart>());
				}
				return arg_19_0;
			}
		}

		public UQueryBuilder(VisualElement visualElement)
		{
			this = default(UQueryBuilder<T>);
			this.m_Element = visualElement;
			this.m_Parts = null;
			this.m_StyleSelectors = null;
			this.m_Relationship = StyleSelectorRelationship.None;
			this.m_Matchers = new List<RuleMatcher>();
			this.pseudoStatesMask = (this.negatedPseudoStatesMask = 0);
		}

		public UQueryBuilder<T> Class(string classname)
		{
			this.AddClass(classname);
			return this;
		}

		public UQueryBuilder<T> Name(string id)
		{
			this.AddName(id);
			return this;
		}

		public UQueryBuilder<T2> Descendents<T2>(string name = null, params string[] classNames) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClasses(classNames);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Descendent);
		}

		public UQueryBuilder<T2> Descendents<T2>(string name = null, string classname = null) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClass(classname);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Descendent);
		}

		public UQueryBuilder<T2> Children<T2>(string name = null, params string[] classes) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClasses(classes);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Child);
		}

		public UQueryBuilder<T2> Children<T2>(string name = null, string className = null) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClass(className);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Child);
		}

		public UQueryBuilder<T2> OfType<T2>(string name = null, params string[] classes) where T2 : VisualElement
		{
			this.AddType<T2>();
			this.AddName(name);
			this.AddClasses(classes);
			return this.AddRelationship<T2>(StyleSelectorRelationship.None);
		}

		public UQueryBuilder<T2> OfType<T2>(string name = null, string className = null) where T2 : VisualElement
		{
			this.AddType<T2>();
			this.AddName(name);
			this.AddClass(className);
			return this.AddRelationship<T2>(StyleSelectorRelationship.None);
		}

		internal UQueryBuilder<T> SingleBaseType()
		{
			this.parts.Add(StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance));
			return this;
		}

		public UQueryBuilder<T> Where(Func<T, bool> selectorPredicate)
		{
			this.parts.Add(StyleSelectorPart.CreatePredicate(new UQuery.PredicateWrapper<T>(selectorPredicate)));
			return this;
		}

		private void AddClass(string c)
		{
			bool flag = c != null;
			if (flag)
			{
				this.parts.Add(StyleSelectorPart.CreateClass(c));
			}
		}

		private void AddClasses(params string[] classes)
		{
			bool flag = classes != null;
			if (flag)
			{
				for (int i = 0; i < classes.Length; i++)
				{
					this.AddClass(classes[i]);
				}
			}
		}

		private void AddName(string id)
		{
			bool flag = id != null;
			if (flag)
			{
				this.parts.Add(StyleSelectorPart.CreateId(id));
			}
		}

		private void AddType<T2>() where T2 : VisualElement
		{
			bool flag = typeof(T2) != typeof(VisualElement);
			if (flag)
			{
				this.parts.Add(StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T2>.s_Instance));
			}
		}

		private UQueryBuilder<T> AddPseudoState(PseudoStates s)
		{
			this.pseudoStatesMask |= (int)s;
			return this;
		}

		private UQueryBuilder<T> AddNegativePseudoState(PseudoStates s)
		{
			this.negatedPseudoStatesMask |= (int)s;
			return this;
		}

		public UQueryBuilder<T> Active()
		{
			return this.AddPseudoState(PseudoStates.Active);
		}

		public UQueryBuilder<T> NotActive()
		{
			return this.AddNegativePseudoState(PseudoStates.Active);
		}

		public UQueryBuilder<T> Visible()
		{
			Func<T, bool> arg_21_1;
			if ((arg_21_1 = UQueryBuilder<T>.<>c.<>9__30_0) == null)
			{
				arg_21_1 = (UQueryBuilder<T>.<>c.<>9__30_0 = new Func<T, bool>(UQueryBuilder<T>.<>c.<>9.<Visible>b__30_0));
			}
			return this.Where(arg_21_1);
		}

		public UQueryBuilder<T> NotVisible()
		{
			Func<T, bool> arg_21_1;
			if ((arg_21_1 = UQueryBuilder<T>.<>c.<>9__31_0) == null)
			{
				arg_21_1 = (UQueryBuilder<T>.<>c.<>9__31_0 = new Func<T, bool>(UQueryBuilder<T>.<>c.<>9.<NotVisible>b__31_0));
			}
			return this.Where(arg_21_1);
		}

		public UQueryBuilder<T> Hovered()
		{
			return this.AddPseudoState(PseudoStates.Hover);
		}

		public UQueryBuilder<T> NotHovered()
		{
			return this.AddNegativePseudoState(PseudoStates.Hover);
		}

		public UQueryBuilder<T> Checked()
		{
			return this.AddPseudoState(PseudoStates.Checked);
		}

		public UQueryBuilder<T> NotChecked()
		{
			return this.AddNegativePseudoState(PseudoStates.Checked);
		}

		[Obsolete("Use Checked() instead")]
		public UQueryBuilder<T> Selected()
		{
			return this.AddPseudoState(PseudoStates.Checked);
		}

		[Obsolete("Use NotChecked() instead")]
		public UQueryBuilder<T> NotSelected()
		{
			return this.AddNegativePseudoState(PseudoStates.Checked);
		}

		public UQueryBuilder<T> Enabled()
		{
			return this.AddNegativePseudoState(PseudoStates.Disabled);
		}

		public UQueryBuilder<T> NotEnabled()
		{
			return this.AddPseudoState(PseudoStates.Disabled);
		}

		public UQueryBuilder<T> Focused()
		{
			return this.AddPseudoState(PseudoStates.Focus);
		}

		public UQueryBuilder<T> NotFocused()
		{
			return this.AddNegativePseudoState(PseudoStates.Focus);
		}

		private UQueryBuilder<T2> AddRelationship<T2>(StyleSelectorRelationship relationship) where T2 : VisualElement
		{
			return new UQueryBuilder<T2>(this.m_Element)
			{
				m_Matchers = this.m_Matchers,
				m_Parts = this.m_Parts,
				m_StyleSelectors = this.m_StyleSelectors,
				m_Relationship = ((relationship == StyleSelectorRelationship.None) ? this.m_Relationship : relationship),
				pseudoStatesMask = this.pseudoStatesMask,
				negatedPseudoStatesMask = this.negatedPseudoStatesMask
			};
		}

		private void AddPseudoStatesRuleIfNecessasy()
		{
			bool flag = this.pseudoStatesMask != 0 || this.negatedPseudoStatesMask != 0;
			if (flag)
			{
				this.parts.Add(new StyleSelectorPart
				{
					type = StyleSelectorType.PseudoClass
				});
			}
		}

		private void FinishSelector()
		{
			this.FinishCurrentSelector();
			bool flag = this.styleSelectors.Count > 0;
			if (flag)
			{
				StyleComplexSelector styleComplexSelector = new StyleComplexSelector();
				styleComplexSelector.selectors = this.styleSelectors.ToArray();
				this.styleSelectors.Clear();
				this.m_Matchers.Add(new RuleMatcher
				{
					complexSelector = styleComplexSelector
				});
			}
		}

		private bool CurrentSelectorEmpty()
		{
			return this.parts.Count == 0 && this.m_Relationship == StyleSelectorRelationship.None && this.pseudoStatesMask == 0 && this.negatedPseudoStatesMask == 0;
		}

		private void FinishCurrentSelector()
		{
			bool flag = !this.CurrentSelectorEmpty();
			if (flag)
			{
				StyleSelector styleSelector = new StyleSelector();
				styleSelector.previousRelationship = this.m_Relationship;
				this.AddPseudoStatesRuleIfNecessasy();
				styleSelector.parts = this.m_Parts.ToArray();
				styleSelector.pseudoStateMask = this.pseudoStatesMask;
				styleSelector.negatedPseudoStateMask = this.negatedPseudoStatesMask;
				this.styleSelectors.Add(styleSelector);
				this.m_Parts.Clear();
				this.pseudoStatesMask = (this.negatedPseudoStatesMask = 0);
			}
		}

		public UQueryState<T> Build()
		{
			this.FinishSelector();
			return new UQueryState<T>(this.m_Element, this.m_Matchers);
		}

		public static implicit operator T(UQueryBuilder<T> s)
		{
			return s.First();
		}

		public static bool operator ==(UQueryBuilder<T> builder1, UQueryBuilder<T> builder2)
		{
			return builder1.Equals(builder2);
		}

		public static bool operator !=(UQueryBuilder<T> builder1, UQueryBuilder<T> builder2)
		{
			return !(builder1 == builder2);
		}

		public T First()
		{
			return this.Build().First();
		}

		public T Last()
		{
			return this.Build().Last();
		}

		public List<T> ToList()
		{
			return this.Build().ToList();
		}

		public void ToList(List<T> results)
		{
			this.Build().ToList(results);
		}

		public T AtIndex(int index)
		{
			return this.Build().AtIndex(index);
		}

		public void ForEach<T2>(List<T2> result, Func<T, T2> funcCall)
		{
			this.Build().ForEach<T2>(result, funcCall);
		}

		public List<T2> ForEach<T2>(Func<T, T2> funcCall)
		{
			return this.Build().ForEach<T2>(funcCall);
		}

		public void ForEach(Action<T> funcCall)
		{
			this.Build().ForEach(funcCall);
		}

		public bool Equals(UQueryBuilder<T> other)
		{
			return EqualityComparer<List<StyleSelector>>.Default.Equals(this.m_StyleSelectors, other.m_StyleSelectors) && EqualityComparer<List<StyleSelector>>.Default.Equals(this.styleSelectors, other.styleSelectors) && EqualityComparer<List<StyleSelectorPart>>.Default.Equals(this.m_Parts, other.m_Parts) && EqualityComparer<List<StyleSelectorPart>>.Default.Equals(this.parts, other.parts) && this.m_Element == other.m_Element && EqualityComparer<List<RuleMatcher>>.Default.Equals(this.m_Matchers, other.m_Matchers) && this.m_Relationship == other.m_Relationship && this.pseudoStatesMask == other.pseudoStatesMask && this.negatedPseudoStatesMask == other.negatedPseudoStatesMask;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is UQueryBuilder<T>);
			return !flag && this.Equals((UQueryBuilder<T>)obj);
		}

		public override int GetHashCode()
		{
			int num = -949812380;
			num = num * -1521134295 + EqualityComparer<List<StyleSelector>>.Default.GetHashCode(this.m_StyleSelectors);
			num = num * -1521134295 + EqualityComparer<List<StyleSelector>>.Default.GetHashCode(this.styleSelectors);
			num = num * -1521134295 + EqualityComparer<List<StyleSelectorPart>>.Default.GetHashCode(this.m_Parts);
			num = num * -1521134295 + EqualityComparer<List<StyleSelectorPart>>.Default.GetHashCode(this.parts);
			num = num * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.m_Element);
			num = num * -1521134295 + EqualityComparer<List<RuleMatcher>>.Default.GetHashCode(this.m_Matchers);
			num = num * -1521134295 + this.m_Relationship.GetHashCode();
			num = num * -1521134295 + this.pseudoStatesMask.GetHashCode();
			return num * -1521134295 + this.negatedPseudoStatesMask.GetHashCode();
		}
	}
}
