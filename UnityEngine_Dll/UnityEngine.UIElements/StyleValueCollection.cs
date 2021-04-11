using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	internal class StyleValueCollection
	{
		internal List<StyleValue> m_Values = new List<StyleValue>();

		public StyleLength GetStyleLength(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleLength result;
			if (flag)
			{
				result = new StyleLength(styleValue.length, styleValue.keyword);
			}
			else
			{
				result = StyleKeyword.Null;
			}
			return result;
		}

		public StyleFloat GetStyleFloat(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleFloat result;
			if (flag)
			{
				result = new StyleFloat(styleValue.number, styleValue.keyword);
			}
			else
			{
				result = StyleKeyword.Null;
			}
			return result;
		}

		public StyleInt GetStyleInt(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleInt result;
			if (flag)
			{
				result = new StyleInt((int)styleValue.number, styleValue.keyword);
			}
			else
			{
				result = StyleKeyword.Null;
			}
			return result;
		}

		public StyleColor GetStyleColor(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleColor result;
			if (flag)
			{
				result = new StyleColor(styleValue.color, styleValue.keyword);
			}
			else
			{
				result = StyleKeyword.Null;
			}
			return result;
		}

		public StyleBackground GetStyleBackground(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleBackground result;
			if (flag)
			{
				Texture2D v = styleValue.resource.IsAllocated ? (styleValue.resource.Target as Texture2D) : null;
				result = new StyleBackground(v, styleValue.keyword);
			}
			else
			{
				result = StyleKeyword.Null;
			}
			return result;
		}

		public StyleFont GetStyleFont(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleFont result;
			if (flag)
			{
				Font v = styleValue.resource.IsAllocated ? (styleValue.resource.Target as Font) : null;
				result = new StyleFont(v, styleValue.keyword);
			}
			else
			{
				result = StyleKeyword.Null;
			}
			return result;
		}

		public bool TryGetStyleValue(StylePropertyId id, ref StyleValue value)
		{
			value.id = StylePropertyId.Unknown;
			bool result;
			foreach (StyleValue current in this.m_Values)
			{
				bool flag = current.id == id;
				if (flag)
				{
					value = current;
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public void SetStyleValue(StyleValue value)
		{
			for (int i = 0; i < this.m_Values.Count; i++)
			{
				bool flag = this.m_Values[i].id == value.id;
				if (flag)
				{
					bool flag2 = value.keyword == StyleKeyword.Null;
					if (flag2)
					{
						this.m_Values.RemoveAt(i);
					}
					else
					{
						this.m_Values[i] = value;
					}
					return;
				}
			}
			this.m_Values.Add(value);
		}
	}
}
