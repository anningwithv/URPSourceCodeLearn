using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	public static class UQuery
	{
		internal interface IVisualPredicateWrapper
		{
			bool Predicate(object e);
		}

		internal class IsOfType<T> : UQuery.IVisualPredicateWrapper where T : VisualElement
		{
			public static UQuery.IsOfType<T> s_Instance = new UQuery.IsOfType<T>();

			public bool Predicate(object e)
			{
				return e is T;
			}
		}

		internal class PredicateWrapper<T> : UQuery.IVisualPredicateWrapper where T : VisualElement
		{
			private Func<T, bool> predicate;

			public PredicateWrapper(Func<T, bool> p)
			{
				this.predicate = p;
			}

			public bool Predicate(object e)
			{
				T t = e as T;
				bool flag = t != null;
				return flag && this.predicate(t);
			}
		}

		internal abstract class UQueryMatcher : HierarchyTraversal
		{
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				public static readonly UQuery.UQueryMatcher.<>c <>9 = new UQuery.UQueryMatcher.<>c();

				public static Action<VisualElement, MatchResultInfo> <>9__5_0;

				internal void <TraverseRecursive>b__5_0(VisualElement e, MatchResultInfo i)
				{
					UQuery.UQueryMatcher.NoProcessResult(e, i);
				}
			}

			internal List<RuleMatcher> m_Matchers;

			public override void Traverse(VisualElement element)
			{
				base.Traverse(element);
			}

			protected virtual bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				return false;
			}

			private static void NoProcessResult(VisualElement e, MatchResultInfo i)
			{
			}

			public override void TraverseRecursive(VisualElement element, int depth)
			{
				int count = this.m_Matchers.Count;
				int count2 = this.m_Matchers.Count;
				for (int i = 0; i < count2; i++)
				{
					RuleMatcher ruleMatcher = this.m_Matchers[i];
					StyleComplexSelector arg_51_1 = ruleMatcher.complexSelector;
					Action<VisualElement, MatchResultInfo> arg_51_2;
					if ((arg_51_2 = UQuery.UQueryMatcher.<>c.<>9__5_0) == null)
					{
						arg_51_2 = (UQuery.UQueryMatcher.<>c.<>9__5_0 = new Action<VisualElement, MatchResultInfo>(UQuery.UQueryMatcher.<>c.<>9.<TraverseRecursive>b__5_0));
					}
					bool flag = StyleSelectorHelper.MatchRightToLeft(element, arg_51_1, arg_51_2);
					if (flag)
					{
						bool flag2 = this.OnRuleMatchedElement(ruleMatcher, element);
						if (flag2)
						{
							return;
						}
					}
				}
				base.Recurse(element, depth);
				bool flag3 = this.m_Matchers.Count > count;
				if (flag3)
				{
					this.m_Matchers.RemoveRange(count, this.m_Matchers.Count - count);
					return;
				}
			}

			public virtual void Run(VisualElement root, List<RuleMatcher> matchers)
			{
				this.m_Matchers = matchers;
				this.Traverse(root);
			}
		}

		internal abstract class SingleQueryMatcher : UQuery.UQueryMatcher
		{
			public VisualElement match
			{
				get;
				set;
			}

			public override void Run(VisualElement root, List<RuleMatcher> matchers)
			{
				this.match = null;
				base.Run(root, matchers);
			}
		}

		internal class FirstQueryMatcher : UQuery.SingleQueryMatcher
		{
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				bool flag = base.match == null;
				if (flag)
				{
					base.match = element;
				}
				return true;
			}
		}

		internal class LastQueryMatcher : UQuery.SingleQueryMatcher
		{
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				base.match = element;
				return false;
			}
		}

		internal class IndexQueryMatcher : UQuery.SingleQueryMatcher
		{
			private int matchCount = -1;

			private int _matchIndex;

			public int matchIndex
			{
				get
				{
					return this._matchIndex;
				}
				set
				{
					this.matchCount = -1;
					this._matchIndex = value;
				}
			}

			public override void Run(VisualElement root, List<RuleMatcher> matchers)
			{
				this.matchCount = -1;
				base.Run(root, matchers);
			}

			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				this.matchCount++;
				bool flag = this.matchCount == this._matchIndex;
				if (flag)
				{
					base.match = element;
				}
				return this.matchCount >= this._matchIndex;
			}
		}
	}
}
