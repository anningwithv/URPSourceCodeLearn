using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	internal static class StylePropertyUtil
	{
		public const int k_GroupOffset = 16;

		internal static readonly Dictionary<string, StylePropertyId> s_NameToId = new Dictionary<string, StylePropertyId>
		{
			{
				"align-content",
				StylePropertyId.AlignContent
			},
			{
				"align-items",
				StylePropertyId.AlignItems
			},
			{
				"align-self",
				StylePropertyId.AlignSelf
			},
			{
				"background-color",
				StylePropertyId.BackgroundColor
			},
			{
				"background-image",
				StylePropertyId.BackgroundImage
			},
			{
				"border-bottom-color",
				StylePropertyId.BorderBottomColor
			},
			{
				"border-bottom-left-radius",
				StylePropertyId.BorderBottomLeftRadius
			},
			{
				"border-bottom-right-radius",
				StylePropertyId.BorderBottomRightRadius
			},
			{
				"border-bottom-width",
				StylePropertyId.BorderBottomWidth
			},
			{
				"border-color",
				StylePropertyId.BorderColor
			},
			{
				"border-left-color",
				StylePropertyId.BorderLeftColor
			},
			{
				"border-left-width",
				StylePropertyId.BorderLeftWidth
			},
			{
				"border-radius",
				StylePropertyId.BorderRadius
			},
			{
				"border-right-color",
				StylePropertyId.BorderRightColor
			},
			{
				"border-right-width",
				StylePropertyId.BorderRightWidth
			},
			{
				"border-top-color",
				StylePropertyId.BorderTopColor
			},
			{
				"border-top-left-radius",
				StylePropertyId.BorderTopLeftRadius
			},
			{
				"border-top-right-radius",
				StylePropertyId.BorderTopRightRadius
			},
			{
				"border-top-width",
				StylePropertyId.BorderTopWidth
			},
			{
				"border-width",
				StylePropertyId.BorderWidth
			},
			{
				"bottom",
				StylePropertyId.Bottom
			},
			{
				"color",
				StylePropertyId.Color
			},
			{
				"cursor",
				StylePropertyId.Cursor
			},
			{
				"display",
				StylePropertyId.Display
			},
			{
				"flex",
				StylePropertyId.Flex
			},
			{
				"flex-basis",
				StylePropertyId.FlexBasis
			},
			{
				"flex-direction",
				StylePropertyId.FlexDirection
			},
			{
				"flex-grow",
				StylePropertyId.FlexGrow
			},
			{
				"flex-shrink",
				StylePropertyId.FlexShrink
			},
			{
				"flex-wrap",
				StylePropertyId.FlexWrap
			},
			{
				"font-size",
				StylePropertyId.FontSize
			},
			{
				"height",
				StylePropertyId.Height
			},
			{
				"justify-content",
				StylePropertyId.JustifyContent
			},
			{
				"left",
				StylePropertyId.Left
			},
			{
				"margin",
				StylePropertyId.Margin
			},
			{
				"margin-bottom",
				StylePropertyId.MarginBottom
			},
			{
				"margin-left",
				StylePropertyId.MarginLeft
			},
			{
				"margin-right",
				StylePropertyId.MarginRight
			},
			{
				"margin-top",
				StylePropertyId.MarginTop
			},
			{
				"max-height",
				StylePropertyId.MaxHeight
			},
			{
				"max-width",
				StylePropertyId.MaxWidth
			},
			{
				"min-height",
				StylePropertyId.MinHeight
			},
			{
				"min-width",
				StylePropertyId.MinWidth
			},
			{
				"opacity",
				StylePropertyId.Opacity
			},
			{
				"overflow",
				StylePropertyId.Overflow
			},
			{
				"padding",
				StylePropertyId.Padding
			},
			{
				"padding-bottom",
				StylePropertyId.PaddingBottom
			},
			{
				"padding-left",
				StylePropertyId.PaddingLeft
			},
			{
				"padding-right",
				StylePropertyId.PaddingRight
			},
			{
				"padding-top",
				StylePropertyId.PaddingTop
			},
			{
				"position",
				StylePropertyId.Position
			},
			{
				"right",
				StylePropertyId.Right
			},
			{
				"text-overflow",
				StylePropertyId.TextOverflow
			},
			{
				"top",
				StylePropertyId.Top
			},
			{
				"-unity-background-image-tint-color",
				StylePropertyId.UnityBackgroundImageTintColor
			},
			{
				"-unity-background-scale-mode",
				StylePropertyId.UnityBackgroundScaleMode
			},
			{
				"-unity-font",
				StylePropertyId.UnityFont
			},
			{
				"-unity-font-style",
				StylePropertyId.UnityFontStyleAndWeight
			},
			{
				"-unity-overflow-clip-box",
				StylePropertyId.UnityOverflowClipBox
			},
			{
				"-unity-slice-bottom",
				StylePropertyId.UnitySliceBottom
			},
			{
				"-unity-slice-left",
				StylePropertyId.UnitySliceLeft
			},
			{
				"-unity-slice-right",
				StylePropertyId.UnitySliceRight
			},
			{
				"-unity-slice-top",
				StylePropertyId.UnitySliceTop
			},
			{
				"-unity-text-align",
				StylePropertyId.UnityTextAlign
			},
			{
				"-unity-text-overflow-position",
				StylePropertyId.UnityTextOverflowPosition
			},
			{
				"visibility",
				StylePropertyId.Visibility
			},
			{
				"white-space",
				StylePropertyId.WhiteSpace
			},
			{
				"width",
				StylePropertyId.Width
			}
		};

		public static int GetEnumIntValue(StyleEnumType enumType, string value)
		{
			int result;
			switch (enumType)
			{
			case StyleEnumType.Align:
			{
				bool flag = string.Equals(value, "auto", StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					result = 0;
				}
				else
				{
					bool flag2 = string.Equals(value, "flex-start", StringComparison.OrdinalIgnoreCase);
					if (flag2)
					{
						result = 1;
					}
					else
					{
						bool flag3 = string.Equals(value, "center", StringComparison.OrdinalIgnoreCase);
						if (flag3)
						{
							result = 2;
						}
						else
						{
							bool flag4 = string.Equals(value, "flex-end", StringComparison.OrdinalIgnoreCase);
							if (flag4)
							{
								result = 3;
							}
							else
							{
								bool flag5 = string.Equals(value, "stretch", StringComparison.OrdinalIgnoreCase);
								if (flag5)
								{
									result = 4;
								}
								else
								{
									result = 0;
								}
							}
						}
					}
				}
				break;
			}
			case StyleEnumType.DisplayStyle:
			{
				bool flag6 = string.Equals(value, "flex", StringComparison.OrdinalIgnoreCase);
				if (flag6)
				{
					result = 0;
				}
				else
				{
					bool flag7 = string.Equals(value, "none", StringComparison.OrdinalIgnoreCase);
					if (flag7)
					{
						result = 1;
					}
					else
					{
						result = 0;
					}
				}
				break;
			}
			case StyleEnumType.FlexDirection:
			{
				bool flag8 = string.Equals(value, "column", StringComparison.OrdinalIgnoreCase);
				if (flag8)
				{
					result = 0;
				}
				else
				{
					bool flag9 = string.Equals(value, "column-reverse", StringComparison.OrdinalIgnoreCase);
					if (flag9)
					{
						result = 1;
					}
					else
					{
						bool flag10 = string.Equals(value, "row", StringComparison.OrdinalIgnoreCase);
						if (flag10)
						{
							result = 2;
						}
						else
						{
							bool flag11 = string.Equals(value, "row-reverse", StringComparison.OrdinalIgnoreCase);
							if (flag11)
							{
								result = 3;
							}
							else
							{
								result = 0;
							}
						}
					}
				}
				break;
			}
			case StyleEnumType.FontStyle:
			{
				bool flag12 = string.Equals(value, "normal", StringComparison.OrdinalIgnoreCase);
				if (flag12)
				{
					result = 0;
				}
				else
				{
					bool flag13 = string.Equals(value, "bold", StringComparison.OrdinalIgnoreCase);
					if (flag13)
					{
						result = 1;
					}
					else
					{
						bool flag14 = string.Equals(value, "italic", StringComparison.OrdinalIgnoreCase);
						if (flag14)
						{
							result = 2;
						}
						else
						{
							bool flag15 = string.Equals(value, "bold-and-italic", StringComparison.OrdinalIgnoreCase);
							if (flag15)
							{
								result = 3;
							}
							else
							{
								result = 0;
							}
						}
					}
				}
				break;
			}
			case StyleEnumType.Justify:
			{
				bool flag16 = string.Equals(value, "flex-start", StringComparison.OrdinalIgnoreCase);
				if (flag16)
				{
					result = 0;
				}
				else
				{
					bool flag17 = string.Equals(value, "center", StringComparison.OrdinalIgnoreCase);
					if (flag17)
					{
						result = 1;
					}
					else
					{
						bool flag18 = string.Equals(value, "flex-end", StringComparison.OrdinalIgnoreCase);
						if (flag18)
						{
							result = 2;
						}
						else
						{
							bool flag19 = string.Equals(value, "space-between", StringComparison.OrdinalIgnoreCase);
							if (flag19)
							{
								result = 3;
							}
							else
							{
								bool flag20 = string.Equals(value, "space-around", StringComparison.OrdinalIgnoreCase);
								if (flag20)
								{
									result = 4;
								}
								else
								{
									result = 0;
								}
							}
						}
					}
				}
				break;
			}
			case StyleEnumType.Overflow:
			{
				bool flag21 = string.Equals(value, "visible", StringComparison.OrdinalIgnoreCase);
				if (flag21)
				{
					result = 0;
				}
				else
				{
					bool flag22 = string.Equals(value, "hidden", StringComparison.OrdinalIgnoreCase);
					if (flag22)
					{
						result = 1;
					}
					else
					{
						result = 0;
					}
				}
				break;
			}
			case StyleEnumType.OverflowClipBox:
			{
				bool flag23 = string.Equals(value, "padding-box", StringComparison.OrdinalIgnoreCase);
				if (flag23)
				{
					result = 0;
				}
				else
				{
					bool flag24 = string.Equals(value, "content-box", StringComparison.OrdinalIgnoreCase);
					if (flag24)
					{
						result = 1;
					}
					else
					{
						result = 0;
					}
				}
				break;
			}
			case StyleEnumType.OverflowInternal:
			{
				bool flag25 = string.Equals(value, "visible", StringComparison.OrdinalIgnoreCase);
				if (flag25)
				{
					result = 0;
				}
				else
				{
					bool flag26 = string.Equals(value, "hidden", StringComparison.OrdinalIgnoreCase);
					if (flag26)
					{
						result = 1;
					}
					else
					{
						bool flag27 = string.Equals(value, "scroll", StringComparison.OrdinalIgnoreCase);
						if (flag27)
						{
							result = 2;
						}
						else
						{
							result = 0;
						}
					}
				}
				break;
			}
			case StyleEnumType.Position:
			{
				bool flag28 = string.Equals(value, "relative", StringComparison.OrdinalIgnoreCase);
				if (flag28)
				{
					result = 0;
				}
				else
				{
					bool flag29 = string.Equals(value, "absolute", StringComparison.OrdinalIgnoreCase);
					if (flag29)
					{
						result = 1;
					}
					else
					{
						result = 0;
					}
				}
				break;
			}
			case StyleEnumType.ScaleMode:
			{
				bool flag30 = string.Equals(value, "stretch-to-fill", StringComparison.OrdinalIgnoreCase);
				if (flag30)
				{
					result = 0;
				}
				else
				{
					bool flag31 = string.Equals(value, "scale-and-crop", StringComparison.OrdinalIgnoreCase);
					if (flag31)
					{
						result = 1;
					}
					else
					{
						bool flag32 = string.Equals(value, "scale-to-fit", StringComparison.OrdinalIgnoreCase);
						if (flag32)
						{
							result = 2;
						}
						else
						{
							result = 0;
						}
					}
				}
				break;
			}
			case StyleEnumType.TextAnchor:
			{
				bool flag33 = string.Equals(value, "upper-left", StringComparison.OrdinalIgnoreCase);
				if (flag33)
				{
					result = 0;
				}
				else
				{
					bool flag34 = string.Equals(value, "upper-center", StringComparison.OrdinalIgnoreCase);
					if (flag34)
					{
						result = 1;
					}
					else
					{
						bool flag35 = string.Equals(value, "upper-right", StringComparison.OrdinalIgnoreCase);
						if (flag35)
						{
							result = 2;
						}
						else
						{
							bool flag36 = string.Equals(value, "middle-left", StringComparison.OrdinalIgnoreCase);
							if (flag36)
							{
								result = 3;
							}
							else
							{
								bool flag37 = string.Equals(value, "middle-center", StringComparison.OrdinalIgnoreCase);
								if (flag37)
								{
									result = 4;
								}
								else
								{
									bool flag38 = string.Equals(value, "middle-right", StringComparison.OrdinalIgnoreCase);
									if (flag38)
									{
										result = 5;
									}
									else
									{
										bool flag39 = string.Equals(value, "lower-left", StringComparison.OrdinalIgnoreCase);
										if (flag39)
										{
											result = 6;
										}
										else
										{
											bool flag40 = string.Equals(value, "lower-center", StringComparison.OrdinalIgnoreCase);
											if (flag40)
											{
												result = 7;
											}
											else
											{
												bool flag41 = string.Equals(value, "lower-right", StringComparison.OrdinalIgnoreCase);
												if (flag41)
												{
													result = 8;
												}
												else
												{
													result = 0;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				break;
			}
			case StyleEnumType.TextOverflow:
			{
				bool flag42 = string.Equals(value, "clip", StringComparison.OrdinalIgnoreCase);
				if (flag42)
				{
					result = 0;
				}
				else
				{
					bool flag43 = string.Equals(value, "ellipsis", StringComparison.OrdinalIgnoreCase);
					if (flag43)
					{
						result = 1;
					}
					else
					{
						result = 0;
					}
				}
				break;
			}
			case StyleEnumType.TextOverflowPosition:
			{
				bool flag44 = string.Equals(value, "start", StringComparison.OrdinalIgnoreCase);
				if (flag44)
				{
					result = 1;
				}
				else
				{
					bool flag45 = string.Equals(value, "middle", StringComparison.OrdinalIgnoreCase);
					if (flag45)
					{
						result = 2;
					}
					else
					{
						bool flag46 = string.Equals(value, "end", StringComparison.OrdinalIgnoreCase);
						if (flag46)
						{
							result = 0;
						}
						else
						{
							result = 0;
						}
					}
				}
				break;
			}
			case StyleEnumType.Visibility:
			{
				bool flag47 = string.Equals(value, "visible", StringComparison.OrdinalIgnoreCase);
				if (flag47)
				{
					result = 0;
				}
				else
				{
					bool flag48 = string.Equals(value, "hidden", StringComparison.OrdinalIgnoreCase);
					if (flag48)
					{
						result = 1;
					}
					else
					{
						result = 0;
					}
				}
				break;
			}
			case StyleEnumType.WhiteSpace:
			{
				bool flag49 = string.Equals(value, "normal", StringComparison.OrdinalIgnoreCase);
				if (flag49)
				{
					result = 0;
				}
				else
				{
					bool flag50 = string.Equals(value, "nowrap", StringComparison.OrdinalIgnoreCase);
					if (flag50)
					{
						result = 1;
					}
					else
					{
						result = 0;
					}
				}
				break;
			}
			case StyleEnumType.Wrap:
			{
				bool flag51 = string.Equals(value, "nowrap", StringComparison.OrdinalIgnoreCase);
				if (flag51)
				{
					result = 0;
				}
				else
				{
					bool flag52 = string.Equals(value, "wrap", StringComparison.OrdinalIgnoreCase);
					if (flag52)
					{
						result = 1;
					}
					else
					{
						bool flag53 = string.Equals(value, "wrap-reverse", StringComparison.OrdinalIgnoreCase);
						if (flag53)
						{
							result = 2;
						}
						else
						{
							result = 0;
						}
					}
				}
				break;
			}
			default:
				result = 0;
				break;
			}
			return result;
		}
	}
}
