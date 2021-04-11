using System;

namespace UnityEngine.UIElements
{
	public static class UQueryExtensions
	{
		private class MissingVisualElementException : Exception
		{
			public MissingVisualElementException()
			{
			}

			public MissingVisualElementException(string message) : base(message)
			{
			}
		}

		private static UQueryState<VisualElement> SingleElementEmptyQuery = new UQueryBuilder<VisualElement>(null).Build();

		private static UQueryState<VisualElement> SingleElementNameQuery = new UQueryBuilder<VisualElement>(null).Name(string.Empty).Build();

		private static UQueryState<VisualElement> SingleElementClassQuery = new UQueryBuilder<VisualElement>(null).Class(string.Empty).Build();

		private static UQueryState<VisualElement> SingleElementNameAndClassQuery = new UQueryBuilder<VisualElement>(null).Name(string.Empty).Class(string.Empty).Build();

		private static UQueryState<VisualElement> SingleElementTypeQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Build();

		private static UQueryState<VisualElement> SingleElementTypeAndNameQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Name(string.Empty).Build();

		private static UQueryState<VisualElement> SingleElementTypeAndClassQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Class(string.Empty).Build();

		private static UQueryState<VisualElement> SingleElementTypeAndNameAndClassQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Name(string.Empty).Class(string.Empty).Build();

		public static T Q<T>(this VisualElement e, string name = null, params string[] classes) where T : VisualElement
		{
			return e.Query(name, classes).Build().First();
		}

		public static VisualElement Q(this VisualElement e, string name = null, params string[] classes)
		{
			return e.Query(name, classes).Build().First();
		}

		public static T Q<T>(this VisualElement e, string name = null, string className = null) where T : VisualElement
		{
			bool flag = typeof(T) == typeof(VisualElement);
			T result;
			if (flag)
			{
				result = (e.Q(name, className) as T);
			}
			else
			{
				bool flag2 = name == null;
				if (flag2)
				{
					bool flag3 = className == null;
					if (flag3)
					{
						UQueryState<VisualElement> uQueryState = UQueryExtensions.SingleElementTypeQuery.RebuildOn(e);
						uQueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						result = (uQueryState.First() as T);
					}
					else
					{
						UQueryState<VisualElement> uQueryState = UQueryExtensions.SingleElementTypeAndClassQuery.RebuildOn(e);
						uQueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						uQueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateClass(className);
						result = (uQueryState.First() as T);
					}
				}
				else
				{
					bool flag4 = className == null;
					if (flag4)
					{
						UQueryState<VisualElement> uQueryState = UQueryExtensions.SingleElementTypeAndNameQuery.RebuildOn(e);
						uQueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						uQueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateId(name);
						result = (uQueryState.First() as T);
					}
					else
					{
						UQueryState<VisualElement> uQueryState = UQueryExtensions.SingleElementTypeAndNameAndClassQuery.RebuildOn(e);
						uQueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						uQueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateId(name);
						uQueryState.m_Matchers[0].complexSelector.selectors[0].parts[2] = StyleSelectorPart.CreateClass(className);
						result = (uQueryState.First() as T);
					}
				}
			}
			return result;
		}

		internal static T MandatoryQ<T>(this VisualElement e, string name, string className = null) where T : VisualElement
		{
			T t = e.Q(name, className);
			bool flag = t == null;
			if (flag)
			{
				throw new UQueryExtensions.MissingVisualElementException("Element not found: " + name);
			}
			return t;
		}

		public static VisualElement Q(this VisualElement e, string name = null, string className = null)
		{
			bool flag = name == null;
			VisualElement result;
			if (flag)
			{
				bool flag2 = className == null;
				if (flag2)
				{
					result = UQueryExtensions.SingleElementEmptyQuery.RebuildOn(e).First();
				}
				else
				{
					UQueryState<VisualElement> uQueryState = UQueryExtensions.SingleElementClassQuery.RebuildOn(e);
					uQueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreateClass(className);
					result = uQueryState.First();
				}
			}
			else
			{
				bool flag3 = className == null;
				if (flag3)
				{
					UQueryState<VisualElement> uQueryState = UQueryExtensions.SingleElementNameQuery.RebuildOn(e);
					uQueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreateId(name);
					result = uQueryState.First();
				}
				else
				{
					UQueryState<VisualElement> uQueryState = UQueryExtensions.SingleElementNameAndClassQuery.RebuildOn(e);
					uQueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreateId(name);
					uQueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateClass(className);
					result = uQueryState.First();
				}
			}
			return result;
		}

		internal static VisualElement MandatoryQ(this VisualElement e, string name, string className = null)
		{
			VisualElement visualElement = e.Q(name, className);
			bool flag = visualElement == null;
			if (flag)
			{
				throw new UQueryExtensions.MissingVisualElementException("Element not found: " + name);
			}
			return visualElement;
		}

		public static UQueryBuilder<VisualElement> Query(this VisualElement e, string name = null, params string[] classes)
		{
			return e.Query(name, classes);
		}

		public static UQueryBuilder<VisualElement> Query(this VisualElement e, string name = null, string className = null)
		{
			return e.Query(name, className);
		}

		public static UQueryBuilder<T> Query<T>(this VisualElement e, string name = null, params string[] classes) where T : VisualElement
		{
			return new UQueryBuilder<VisualElement>(e).OfType<T>(name, classes);
		}

		public static UQueryBuilder<T> Query<T>(this VisualElement e, string name = null, string className = null) where T : VisualElement
		{
			return new UQueryBuilder<VisualElement>(e).OfType<T>(name, className);
		}

		public static UQueryBuilder<VisualElement> Query(this VisualElement e)
		{
			return new UQueryBuilder<VisualElement>(e);
		}
	}
}
