using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public struct VisualElementStyleSheetSet : IEquatable<VisualElementStyleSheetSet>
	{
		private readonly VisualElement m_Element;

		public int count
		{
			get
			{
				bool flag = this.m_Element.styleSheetList == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = this.m_Element.styleSheetList.Count;
				}
				return result;
			}
		}

		public StyleSheet this[int index]
		{
			get
			{
				bool flag = this.m_Element.styleSheetList == null;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.m_Element.styleSheetList[index];
			}
		}

		internal VisualElementStyleSheetSet(VisualElement element)
		{
			this.m_Element = element;
		}

		public void Add(StyleSheet styleSheet)
		{
			bool flag = styleSheet == null;
			if (flag)
			{
				throw new ArgumentNullException("styleSheet");
			}
			bool flag2 = this.m_Element.styleSheetList == null;
			if (flag2)
			{
				this.m_Element.styleSheetList = new List<StyleSheet>();
			}
			else
			{
				bool flag3 = this.m_Element.styleSheetList.Contains(styleSheet);
				if (flag3)
				{
					return;
				}
			}
			this.m_Element.styleSheetList.Add(styleSheet);
			this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
		}

		public void Clear()
		{
			bool flag = this.m_Element.styleSheetList == null;
			if (!flag)
			{
				this.m_Element.styleSheetList = null;
				this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		public bool Remove(StyleSheet styleSheet)
		{
			bool flag = styleSheet == null;
			if (flag)
			{
				throw new ArgumentNullException("styleSheet");
			}
			bool flag2 = this.m_Element.styleSheetList != null && this.m_Element.styleSheetList.Remove(styleSheet);
			bool result;
			if (flag2)
			{
				bool flag3 = this.m_Element.styleSheetList.Count == 0;
				if (flag3)
				{
					this.m_Element.styleSheetList = null;
				}
				this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		internal void Swap(StyleSheet old, StyleSheet @new)
		{
			bool flag = old == null;
			if (flag)
			{
				throw new ArgumentNullException("old");
			}
			bool flag2 = @new == null;
			if (flag2)
			{
				throw new ArgumentNullException("new");
			}
			bool flag3 = this.m_Element.styleSheetList == null;
			if (!flag3)
			{
				int num = this.m_Element.styleSheetList.IndexOf(old);
				bool flag4 = num >= 0;
				if (flag4)
				{
					this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
					this.m_Element.styleSheetList[num] = @new;
				}
			}
		}

		public bool Contains(StyleSheet styleSheet)
		{
			bool flag = styleSheet == null;
			if (flag)
			{
				throw new ArgumentNullException("styleSheet");
			}
			bool flag2 = this.m_Element.styleSheetList != null;
			return flag2 && this.m_Element.styleSheetList.Contains(styleSheet);
		}

		public bool Equals(VisualElementStyleSheetSet other)
		{
			return object.Equals(this.m_Element, other.m_Element);
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisualElementStyleSheetSet && this.Equals((VisualElementStyleSheetSet)obj);
		}

		public override int GetHashCode()
		{
			return (this.m_Element != null) ? this.m_Element.GetHashCode() : 0;
		}

		public static bool operator ==(VisualElementStyleSheetSet left, VisualElementStyleSheetSet right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(VisualElementStyleSheetSet left, VisualElementStyleSheetSet right)
		{
			return !left.Equals(right);
		}
	}
}
