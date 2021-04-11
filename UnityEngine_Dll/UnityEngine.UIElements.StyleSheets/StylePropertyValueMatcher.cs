using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	internal class StylePropertyValueMatcher : BaseStyleMatcher
	{
		private List<StylePropertyValue> m_Values;

		private StylePropertyValue current
		{
			get
			{
				return base.hasCurrent ? this.m_Values[this.m_CurrentIndex] : default(StylePropertyValue);
			}
		}

		public override int valueCount
		{
			get
			{
				return this.m_Values.Count;
			}
		}

		public override bool isVariable
		{
			get
			{
				return false;
			}
		}

		public MatchResult Match(Expression exp, List<StylePropertyValue> values)
		{
			MatchResult matchResult = new MatchResult
			{
				errorCode = MatchResultErrorCode.None
			};
			bool flag = values == null || values.Count == 0;
			MatchResult result;
			if (flag)
			{
				matchResult.errorCode = MatchResultErrorCode.EmptyValue;
				result = matchResult;
			}
			else
			{
				base.Initialize();
				this.m_Values = values;
				StyleValueHandle handle = this.m_Values[0].handle;
				bool flag2 = handle.valueType == StyleValueType.Keyword && handle.valueIndex == 1;
				bool flag3;
				if (flag2)
				{
					base.MoveNext();
					flag3 = true;
				}
				else
				{
					flag3 = base.Match(exp);
				}
				bool flag4 = !flag3;
				if (flag4)
				{
					StyleSheet sheet = this.current.sheet;
					matchResult.errorCode = MatchResultErrorCode.Syntax;
					matchResult.errorValue = sheet.ReadAsString(this.current.handle);
				}
				else
				{
					bool hasCurrent = base.hasCurrent;
					if (hasCurrent)
					{
						StyleSheet sheet2 = this.current.sheet;
						matchResult.errorCode = MatchResultErrorCode.ExpectedEndOfValue;
						matchResult.errorValue = sheet2.ReadAsString(this.current.handle);
					}
				}
				result = matchResult;
			}
			return result;
		}

		protected override bool MatchKeyword(string keyword)
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Keyword;
			bool result;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)current.handle.valueIndex;
				result = (valueIndex.ToUssString() == keyword.ToLower());
			}
			else
			{
				bool flag2 = current.handle.valueType == StyleValueType.Enum;
				if (flag2)
				{
					string a = current.sheet.ReadEnum(current.handle);
					result = (a == keyword.ToLower());
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		protected override bool MatchNumber()
		{
			return this.current.handle.valueType == StyleValueType.Float;
		}

		protected override bool MatchInteger()
		{
			return this.current.handle.valueType == StyleValueType.Float;
		}

		protected override bool MatchLength()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			bool result;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				result = (dimension.unit == Dimension.Unit.Pixel);
			}
			else
			{
				bool flag2 = current.handle.valueType == StyleValueType.Float;
				if (flag2)
				{
					float b = current.sheet.ReadFloat(current.handle);
					result = Mathf.Approximately(0f, b);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		protected override bool MatchPercentage()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			bool result;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				result = (dimension.unit == Dimension.Unit.Percent);
			}
			else
			{
				bool flag2 = current.handle.valueType == StyleValueType.Float;
				if (flag2)
				{
					float b = current.sheet.ReadFloat(current.handle);
					result = Mathf.Approximately(0f, b);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		protected override bool MatchColor()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Color;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = current.handle.valueType == StyleValueType.Enum;
				if (flag2)
				{
					Color clear = Color.clear;
					string text = current.sheet.ReadAsString(current.handle);
					bool flag3 = StyleSheetColor.TryGetColor(text.ToLower(), out clear);
					if (flag3)
					{
						result = true;
						return result;
					}
				}
				result = false;
			}
			return result;
		}

		protected override bool MatchResource()
		{
			return this.current.handle.valueType == StyleValueType.ResourcePath;
		}

		protected override bool MatchUrl()
		{
			return this.current.handle.valueType == StyleValueType.AssetReference;
		}
	}
}
