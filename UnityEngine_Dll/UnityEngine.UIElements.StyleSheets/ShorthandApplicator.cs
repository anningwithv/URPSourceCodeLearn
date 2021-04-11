using System;

namespace UnityEngine.UIElements.StyleSheets
{
	internal static class ShorthandApplicator
	{
		public static void ApplyBorderColor(StylePropertyReader reader, ComputedStyle computedStyle)
		{
			StyleColor borderTopColor;
			StyleColor borderRightColor;
			StyleColor borderBottomColor;
			StyleColor borderLeftColor;
			ShorthandApplicator.CompileBoxAreaNoKeyword(reader, out borderTopColor, out borderRightColor, out borderBottomColor, out borderLeftColor);
			computedStyle.nonInheritedData.borderTopColor = borderTopColor;
			computedStyle.nonInheritedData.borderRightColor = borderRightColor;
			computedStyle.nonInheritedData.borderBottomColor = borderBottomColor;
			computedStyle.nonInheritedData.borderLeftColor = borderLeftColor;
		}

		public static void ApplyBorderRadius(StylePropertyReader reader, ComputedStyle computedStyle)
		{
			StyleLength borderTopLeftRadius;
			StyleLength borderTopRightRadius;
			StyleLength borderBottomRightRadius;
			StyleLength borderBottomLeftRadius;
			ShorthandApplicator.CompileBoxAreaNoKeyword(reader, out borderTopLeftRadius, out borderTopRightRadius, out borderBottomRightRadius, out borderBottomLeftRadius);
			computedStyle.nonInheritedData.borderTopLeftRadius = borderTopLeftRadius;
			computedStyle.nonInheritedData.borderTopRightRadius = borderTopRightRadius;
			computedStyle.nonInheritedData.borderBottomRightRadius = borderBottomRightRadius;
			computedStyle.nonInheritedData.borderBottomLeftRadius = borderBottomLeftRadius;
		}

		public static void ApplyBorderWidth(StylePropertyReader reader, ComputedStyle computedStyle)
		{
			StyleFloat borderTopWidth;
			StyleFloat borderRightWidth;
			StyleFloat borderBottomWidth;
			StyleFloat borderLeftWidth;
			ShorthandApplicator.CompileBoxAreaNoKeyword(reader, out borderTopWidth, out borderRightWidth, out borderBottomWidth, out borderLeftWidth);
			computedStyle.nonInheritedData.borderTopWidth = borderTopWidth;
			computedStyle.nonInheritedData.borderRightWidth = borderRightWidth;
			computedStyle.nonInheritedData.borderBottomWidth = borderBottomWidth;
			computedStyle.nonInheritedData.borderLeftWidth = borderLeftWidth;
		}

		public static void ApplyFlex(StylePropertyReader reader, ComputedStyle computedStyle)
		{
			StyleFloat flexGrow;
			StyleFloat flexShrink;
			StyleLength flexBasis;
			ShorthandApplicator.CompileFlexShorthand(reader, out flexGrow, out flexShrink, out flexBasis);
			computedStyle.nonInheritedData.flexGrow = flexGrow;
			computedStyle.nonInheritedData.flexShrink = flexShrink;
			computedStyle.nonInheritedData.flexBasis = flexBasis;
		}

		public static void ApplyMargin(StylePropertyReader reader, ComputedStyle computedStyle)
		{
			StyleLength marginTop;
			StyleLength marginRight;
			StyleLength marginBottom;
			StyleLength marginLeft;
			ShorthandApplicator.CompileBoxArea(reader, out marginTop, out marginRight, out marginBottom, out marginLeft);
			computedStyle.nonInheritedData.marginTop = marginTop;
			computedStyle.nonInheritedData.marginRight = marginRight;
			computedStyle.nonInheritedData.marginBottom = marginBottom;
			computedStyle.nonInheritedData.marginLeft = marginLeft;
		}

		public static void ApplyPadding(StylePropertyReader reader, ComputedStyle computedStyle)
		{
			StyleLength paddingTop;
			StyleLength paddingRight;
			StyleLength paddingBottom;
			StyleLength paddingLeft;
			ShorthandApplicator.CompileBoxAreaNoKeyword(reader, out paddingTop, out paddingRight, out paddingBottom, out paddingLeft);
			computedStyle.nonInheritedData.paddingTop = paddingTop;
			computedStyle.nonInheritedData.paddingRight = paddingRight;
			computedStyle.nonInheritedData.paddingBottom = paddingBottom;
			computedStyle.nonInheritedData.paddingLeft = paddingLeft;
		}

		private static bool CompileFlexShorthand(StylePropertyReader reader, out StyleFloat grow, out StyleFloat shrink, out StyleLength basis)
		{
			grow = 0f;
			shrink = 1f;
			basis = StyleKeyword.Auto;
			bool flag = false;
			int valueCount = reader.valueCount;
			bool flag2 = valueCount == 1 && reader.IsValueType(0, StyleValueType.Keyword);
			if (flag2)
			{
				bool flag3 = reader.IsKeyword(0, StyleValueKeyword.None);
				if (flag3)
				{
					flag = true;
					grow = 0f;
					shrink = 0f;
					basis = StyleKeyword.Auto;
				}
				else
				{
					bool flag4 = reader.IsKeyword(0, StyleValueKeyword.Auto);
					if (flag4)
					{
						flag = true;
						grow = 1f;
						shrink = 1f;
						basis = StyleKeyword.Auto;
					}
				}
			}
			else
			{
				bool flag5 = valueCount <= 3;
				if (flag5)
				{
					flag = true;
					grow = 0f;
					shrink = 1f;
					basis = Length.Percent(0f);
					bool flag6 = false;
					bool flag7 = false;
					int num = 0;
					while (num < valueCount & flag)
					{
						StyleValueType valueType = reader.GetValueType(num);
						bool flag8 = valueType == StyleValueType.Dimension || valueType == StyleValueType.Keyword;
						if (flag8)
						{
							bool flag9 = flag7;
							if (flag9)
							{
								flag = false;
								break;
							}
							flag7 = true;
							bool flag10 = valueType == StyleValueType.Keyword;
							if (flag10)
							{
								bool flag11 = reader.IsKeyword(num, StyleValueKeyword.Auto);
								if (flag11)
								{
									basis = StyleKeyword.Auto;
								}
							}
							else
							{
								bool flag12 = valueType == StyleValueType.Dimension;
								if (flag12)
								{
									basis = reader.ReadStyleLength(num);
								}
							}
							bool flag13 = flag6 && num != valueCount - 1;
							if (flag13)
							{
								flag = false;
							}
						}
						else
						{
							bool flag14 = valueType == StyleValueType.Float;
							if (flag14)
							{
								StyleFloat styleFloat = reader.ReadStyleFloat(num);
								bool flag15 = !flag6;
								if (flag15)
								{
									flag6 = true;
									grow = styleFloat;
								}
								else
								{
									shrink = styleFloat;
								}
							}
							else
							{
								flag = false;
							}
						}
						num++;
					}
				}
			}
			return flag;
		}

		private static void CompileBoxArea(StylePropertyReader reader, out StyleLength top, out StyleLength right, out StyleLength bottom, out StyleLength left)
		{
			top = 0f;
			right = 0f;
			bottom = 0f;
			left = 0f;
			switch (reader.valueCount)
			{
			case 0:
				break;
			case 1:
				top = (right = (bottom = (left = reader.ReadStyleLength(0))));
				break;
			case 2:
				top = (bottom = reader.ReadStyleLength(0));
				left = (right = reader.ReadStyleLength(1));
				break;
			case 3:
				top = reader.ReadStyleLength(0);
				left = (right = reader.ReadStyleLength(1));
				bottom = reader.ReadStyleLength(2);
				break;
			default:
				top = reader.ReadStyleLength(0);
				right = reader.ReadStyleLength(1);
				bottom = reader.ReadStyleLength(2);
				left = reader.ReadStyleLength(3);
				break;
			}
		}

		private static void CompileBoxAreaNoKeyword(StylePropertyReader reader, out StyleLength top, out StyleLength right, out StyleLength bottom, out StyleLength left)
		{
			ShorthandApplicator.CompileBoxArea(reader, out top, out right, out bottom, out left);
			bool flag = top.keyword > StyleKeyword.Undefined;
			if (flag)
			{
				top.value = 0f;
			}
			bool flag2 = right.keyword > StyleKeyword.Undefined;
			if (flag2)
			{
				right.value = 0f;
			}
			bool flag3 = bottom.keyword > StyleKeyword.Undefined;
			if (flag3)
			{
				bottom.value = 0f;
			}
			bool flag4 = left.keyword > StyleKeyword.Undefined;
			if (flag4)
			{
				left.value = 0f;
			}
		}

		private static void CompileBoxAreaNoKeyword(StylePropertyReader reader, out StyleFloat top, out StyleFloat right, out StyleFloat bottom, out StyleFloat left)
		{
			StyleLength styleLength;
			StyleLength styleLength2;
			StyleLength styleLength3;
			StyleLength styleLength4;
			ShorthandApplicator.CompileBoxAreaNoKeyword(reader, out styleLength, out styleLength2, out styleLength3, out styleLength4);
			top = styleLength.ToStyleFloat();
			right = styleLength2.ToStyleFloat();
			bottom = styleLength3.ToStyleFloat();
			left = styleLength4.ToStyleFloat();
		}

		private static void CompileBoxAreaNoKeyword(StylePropertyReader reader, out StyleColor top, out StyleColor right, out StyleColor bottom, out StyleColor left)
		{
			top = Color.clear;
			right = Color.clear;
			bottom = Color.clear;
			left = Color.clear;
			switch (reader.valueCount)
			{
			case 0:
				break;
			case 1:
				top = (right = (bottom = (left = reader.ReadStyleColor(0))));
				break;
			case 2:
				top = (bottom = reader.ReadStyleColor(0));
				left = (right = reader.ReadStyleColor(1));
				break;
			case 3:
				top = reader.ReadStyleColor(0);
				left = (right = reader.ReadStyleColor(1));
				bottom = reader.ReadStyleColor(2);
				break;
			default:
				top = reader.ReadStyleColor(0);
				right = reader.ReadStyleColor(1);
				bottom = reader.ReadStyleColor(2);
				left = reader.ReadStyleColor(3);
				break;
			}
			bool flag = top.keyword > StyleKeyword.Undefined;
			if (flag)
			{
				top.value = Color.clear;
			}
			bool flag2 = right.keyword > StyleKeyword.Undefined;
			if (flag2)
			{
				right.value = Color.clear;
			}
			bool flag3 = bottom.keyword > StyleKeyword.Undefined;
			if (flag3)
			{
				bottom.value = Color.clear;
			}
			bool flag4 = left.keyword > StyleKeyword.Undefined;
			if (flag4)
			{
				left.value = Color.clear;
			}
		}
	}
}
