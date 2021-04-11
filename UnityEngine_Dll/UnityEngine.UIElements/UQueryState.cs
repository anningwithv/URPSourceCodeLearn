using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public struct UQueryState<T> : IEquatable<UQueryState<T>> where T : VisualElement
	{
		private class ListQueryMatcher : UQuery.UQueryMatcher
		{
			public List<T> matches
			{
				get;
				set;
			}

			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				this.matches.Add(element as T);
				return false;
			}

			public void Reset()
			{
				this.matches = null;
			}
		}

		private class ActionQueryMatcher : UQuery.UQueryMatcher
		{
			internal Action<T> callBack
			{
				get;
				set;
			}

			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				T t = element as T;
				bool flag = t != null;
				if (flag)
				{
					this.callBack(t);
				}
				return false;
			}
		}

		private class DelegateQueryMatcher<TReturnType> : UQuery.UQueryMatcher
		{
			public static UQueryState<T>.DelegateQueryMatcher<TReturnType> s_Instance = new UQueryState<T>.DelegateQueryMatcher<TReturnType>();

			public Func<T, TReturnType> callBack
			{
				get;
				set;
			}

			public List<TReturnType> result
			{
				get;
				set;
			}

			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				T t = element as T;
				bool flag = t != null;
				if (flag)
				{
					this.result.Add(this.callBack(t));
				}
				return false;
			}
		}

		private static UQuery.FirstQueryMatcher s_First = new UQuery.FirstQueryMatcher();

		private static UQuery.LastQueryMatcher s_Last = new UQuery.LastQueryMatcher();

		private static UQuery.IndexQueryMatcher s_Index = new UQuery.IndexQueryMatcher();

		private static UQueryState<T>.ActionQueryMatcher s_Action = new UQueryState<T>.ActionQueryMatcher();

		private readonly VisualElement m_Element;

		internal readonly List<RuleMatcher> m_Matchers;

		private static readonly UQueryState<T>.ListQueryMatcher s_List = new UQueryState<T>.ListQueryMatcher();

		internal UQueryState(VisualElement element, List<RuleMatcher> matchers)
		{
			this.m_Element = element;
			this.m_Matchers = matchers;
		}

		public UQueryState<T> RebuildOn(VisualElement element)
		{
			return new UQueryState<T>(element, this.m_Matchers);
		}

		public T First()
		{
			UQueryState<T>.s_First.Run(this.m_Element, this.m_Matchers);
			T result = UQueryState<T>.s_First.match as T;
			UQueryState<T>.s_First.match = null;
			return result;
		}

		public T Last()
		{
			UQueryState<T>.s_Last.Run(this.m_Element, this.m_Matchers);
			T result = UQueryState<T>.s_Last.match as T;
			UQueryState<T>.s_Last.match = null;
			return result;
		}

		public void ToList(List<T> results)
		{
			UQueryState<T>.s_List.matches = results;
			UQueryState<T>.s_List.Run(this.m_Element, this.m_Matchers);
			UQueryState<T>.s_List.Reset();
		}

		public List<T> ToList()
		{
			List<T> list = new List<T>();
			this.ToList(list);
			return list;
		}

		public T AtIndex(int index)
		{
			UQueryState<T>.s_Index.matchIndex = index;
			UQueryState<T>.s_Index.Run(this.m_Element, this.m_Matchers);
			T result = UQueryState<T>.s_Index.match as T;
			UQueryState<T>.s_Index.match = null;
			return result;
		}

		public void ForEach(Action<T> funcCall)
		{
			UQueryState<T>.ActionQueryMatcher actionQueryMatcher = UQueryState<T>.s_Action;
			bool flag = actionQueryMatcher.callBack != null;
			if (flag)
			{
				actionQueryMatcher = new UQueryState<T>.ActionQueryMatcher();
			}
			try
			{
				actionQueryMatcher.callBack = funcCall;
				actionQueryMatcher.Run(this.m_Element, this.m_Matchers);
			}
			finally
			{
				actionQueryMatcher.callBack = null;
			}
		}

		public void ForEach<T2>(List<T2> result, Func<T, T2> funcCall)
		{
			UQueryState<T>.DelegateQueryMatcher<T2> delegateQueryMatcher = UQueryState<T>.DelegateQueryMatcher<T2>.s_Instance;
			bool flag = delegateQueryMatcher.callBack != null;
			if (flag)
			{
				delegateQueryMatcher = new UQueryState<T>.DelegateQueryMatcher<T2>();
			}
			try
			{
				delegateQueryMatcher.callBack = funcCall;
				delegateQueryMatcher.result = result;
				delegateQueryMatcher.Run(this.m_Element, this.m_Matchers);
			}
			finally
			{
				delegateQueryMatcher.callBack = null;
				delegateQueryMatcher.result = null;
			}
		}

		public List<T2> ForEach<T2>(Func<T, T2> funcCall)
		{
			List<T2> result = new List<T2>();
			this.ForEach<T2>(result, funcCall);
			return result;
		}

		public bool Equals(UQueryState<T> other)
		{
			return this.m_Element == other.m_Element && EqualityComparer<List<RuleMatcher>>.Default.Equals(this.m_Matchers, other.m_Matchers);
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is UQueryState<T>);
			return !flag && this.Equals((UQueryState<T>)obj);
		}

		public override int GetHashCode()
		{
			int num = 488160421;
			num = num * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.m_Element);
			return num * -1521134295 + EqualityComparer<List<RuleMatcher>>.Default.GetHashCode(this.m_Matchers);
		}

		public static bool operator ==(UQueryState<T> state1, UQueryState<T> state2)
		{
			return state1.Equals(state2);
		}

		public static bool operator !=(UQueryState<T> state1, UQueryState<T> state2)
		{
			return !(state1 == state2);
		}
	}
}
