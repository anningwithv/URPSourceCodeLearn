using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	internal static class StyleDebug
	{
		internal const int UnitySpecificity = -1;

		internal const int UndefinedSpecificity = 0;

		internal const int InheritedSpecificity = 2147483646;

		internal const int InlineSpecificity = 2147483647;

		public static string[] GetStylePropertyNames()
		{
			List<string> list = StylePropertyUtil.s_NameToId.Keys.ToList<string>();
			list.Sort();
			return list.ToArray();
		}

		public static string[] GetLonghandPropertyNames(string shorthandName)
		{
			StylePropertyId id;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(shorthandName, out id);
			string[] result;
			if (flag)
			{
				bool flag2 = StyleDebug.IsShorthandProperty(id);
				if (flag2)
				{
					result = StyleDebug.GetLonghandPropertyNames(id);
					return result;
				}
			}
			result = null;
			return result;
		}

		public static StylePropertyId GetStylePropertyIdFromName(string name)
		{
			StylePropertyId stylePropertyId;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(name, out stylePropertyId);
			StylePropertyId result;
			if (flag)
			{
				result = stylePropertyId;
			}
			else
			{
				result = StylePropertyId.Unknown;
			}
			return result;
		}

		public static object GetComputedStyleValue(ComputedStyle computedStyle, string name)
		{
			StylePropertyId id;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(name, out id);
			object result;
			if (flag)
			{
				result = StyleDebug.GetComputedStyleValue(computedStyle, id);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static object GetInlineStyleValue(IStyle style, string name)
		{
			StylePropertyId id;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(name, out id);
			object result;
			if (flag)
			{
				result = StyleDebug.GetInlineStyleValue(style, id);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static void SetInlineStyleValue(IStyle style, string name, object value)
		{
			StylePropertyId id;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(name, out id);
			if (flag)
			{
				StyleDebug.SetInlineStyleValue(style, id, value);
			}
		}

		public static Type GetComputedStyleType(string name)
		{
			StylePropertyId id;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(name, out id);
			Type result;
			if (flag)
			{
				bool flag2 = !StyleDebug.IsShorthandProperty(id);
				if (flag2)
				{
					result = StyleDebug.GetComputedStyleType(id);
					return result;
				}
			}
			result = null;
			return result;
		}

		public static void FindSpecifiedStyles(ComputedStyle computedStyle, IEnumerable<SelectorMatchRecord> matchRecords, Dictionary<StylePropertyId, int> result)
		{
			result.Clear();
			bool flag = computedStyle == null;
			if (!flag)
			{
				foreach (SelectorMatchRecord current in matchRecords)
				{
					int value = current.complexSelector.specificity;
					bool isUnityStyleSheet = current.sheet.isUnityStyleSheet;
					if (isUnityStyleSheet)
					{
						value = -1;
					}
					StyleProperty[] properties = current.complexSelector.rule.properties;
					StyleProperty[] array = properties;
					for (int i = 0; i < array.Length; i++)
					{
						StyleProperty styleProperty = array[i];
						StylePropertyId stylePropertyId;
						bool flag2 = StylePropertyUtil.s_NameToId.TryGetValue(styleProperty.name, out stylePropertyId);
						if (flag2)
						{
							bool flag3 = StyleDebug.IsShorthandProperty(stylePropertyId);
							if (flag3)
							{
								string[] longhandPropertyNames = StyleDebug.GetLonghandPropertyNames(stylePropertyId);
								string[] array2 = longhandPropertyNames;
								for (int j = 0; j < array2.Length; j++)
								{
									string name = array2[j];
									StylePropertyId stylePropertyIdFromName = StyleDebug.GetStylePropertyIdFromName(name);
									result[stylePropertyIdFromName] = value;
								}
							}
							else
							{
								result[stylePropertyId] = value;
							}
						}
					}
				}
				StylePropertyId[] inheritedProperties = StyleDebug.GetInheritedProperties();
				StylePropertyId[] array3 = inheritedProperties;
				for (int k = 0; k < array3.Length; k++)
				{
					StylePropertyId stylePropertyId2 = array3[k];
					bool flag4 = result.ContainsKey(stylePropertyId2);
					if (!flag4)
					{
						object computedStyleValue = StyleDebug.GetComputedStyleValue(computedStyle, stylePropertyId2);
						object computedStyleValue2 = StyleDebug.GetComputedStyleValue(InitialStyle.Get(), stylePropertyId2);
						bool flag5 = !computedStyleValue.Equals(computedStyleValue2);
						if (flag5)
						{
							result[stylePropertyId2] = 2147483646;
						}
					}
				}
			}
		}

		public static object GetComputedStyleValue(ComputedStyle computedStyle, StylePropertyId id)
		{
			object result;
			switch (id)
			{
			case StylePropertyId.Color:
				result = computedStyle.color;
				break;
			case StylePropertyId.FontSize:
				result = computedStyle.fontSize;
				break;
			case StylePropertyId.UnityFont:
				result = computedStyle.unityFont;
				break;
			case StylePropertyId.UnityFontStyleAndWeight:
				result = computedStyle.unityFontStyleAndWeight;
				break;
			case StylePropertyId.UnityTextAlign:
				result = computedStyle.unityTextAlign;
				break;
			case StylePropertyId.Visibility:
				result = computedStyle.visibility;
				break;
			case StylePropertyId.WhiteSpace:
				result = computedStyle.whiteSpace;
				break;
			default:
				switch (id)
				{
				case StylePropertyId.AlignContent:
					result = computedStyle.alignContent;
					break;
				case StylePropertyId.AlignItems:
					result = computedStyle.alignItems;
					break;
				case StylePropertyId.AlignSelf:
					result = computedStyle.alignSelf;
					break;
				case StylePropertyId.BackgroundColor:
					result = computedStyle.backgroundColor;
					break;
				case StylePropertyId.BackgroundImage:
					result = computedStyle.backgroundImage;
					break;
				case StylePropertyId.BorderBottomColor:
					result = computedStyle.borderBottomColor;
					break;
				case StylePropertyId.BorderBottomLeftRadius:
					result = computedStyle.borderBottomLeftRadius;
					break;
				case StylePropertyId.BorderBottomRightRadius:
					result = computedStyle.borderBottomRightRadius;
					break;
				case StylePropertyId.BorderBottomWidth:
					result = computedStyle.borderBottomWidth;
					break;
				case StylePropertyId.BorderLeftColor:
					result = computedStyle.borderLeftColor;
					break;
				case StylePropertyId.BorderLeftWidth:
					result = computedStyle.borderLeftWidth;
					break;
				case StylePropertyId.BorderRightColor:
					result = computedStyle.borderRightColor;
					break;
				case StylePropertyId.BorderRightWidth:
					result = computedStyle.borderRightWidth;
					break;
				case StylePropertyId.BorderTopColor:
					result = computedStyle.borderTopColor;
					break;
				case StylePropertyId.BorderTopLeftRadius:
					result = computedStyle.borderTopLeftRadius;
					break;
				case StylePropertyId.BorderTopRightRadius:
					result = computedStyle.borderTopRightRadius;
					break;
				case StylePropertyId.BorderTopWidth:
					result = computedStyle.borderTopWidth;
					break;
				case StylePropertyId.Bottom:
					result = computedStyle.bottom;
					break;
				case StylePropertyId.Cursor:
					result = computedStyle.cursor;
					break;
				case StylePropertyId.Display:
					result = computedStyle.display;
					break;
				case StylePropertyId.FlexBasis:
					result = computedStyle.flexBasis;
					break;
				case StylePropertyId.FlexDirection:
					result = computedStyle.flexDirection;
					break;
				case StylePropertyId.FlexGrow:
					result = computedStyle.flexGrow;
					break;
				case StylePropertyId.FlexShrink:
					result = computedStyle.flexShrink;
					break;
				case StylePropertyId.FlexWrap:
					result = computedStyle.flexWrap;
					break;
				case StylePropertyId.Height:
					result = computedStyle.height;
					break;
				case StylePropertyId.JustifyContent:
					result = computedStyle.justifyContent;
					break;
				case StylePropertyId.Left:
					result = computedStyle.left;
					break;
				case StylePropertyId.MarginBottom:
					result = computedStyle.marginBottom;
					break;
				case StylePropertyId.MarginLeft:
					result = computedStyle.marginLeft;
					break;
				case StylePropertyId.MarginRight:
					result = computedStyle.marginRight;
					break;
				case StylePropertyId.MarginTop:
					result = computedStyle.marginTop;
					break;
				case StylePropertyId.MaxHeight:
					result = computedStyle.maxHeight;
					break;
				case StylePropertyId.MaxWidth:
					result = computedStyle.maxWidth;
					break;
				case StylePropertyId.MinHeight:
					result = computedStyle.minHeight;
					break;
				case StylePropertyId.MinWidth:
					result = computedStyle.minWidth;
					break;
				case StylePropertyId.Opacity:
					result = computedStyle.opacity;
					break;
				case StylePropertyId.Overflow:
					result = computedStyle.overflow;
					break;
				case StylePropertyId.PaddingBottom:
					result = computedStyle.paddingBottom;
					break;
				case StylePropertyId.PaddingLeft:
					result = computedStyle.paddingLeft;
					break;
				case StylePropertyId.PaddingRight:
					result = computedStyle.paddingRight;
					break;
				case StylePropertyId.PaddingTop:
					result = computedStyle.paddingTop;
					break;
				case StylePropertyId.Position:
					result = computedStyle.position;
					break;
				case StylePropertyId.Right:
					result = computedStyle.right;
					break;
				case StylePropertyId.TextOverflow:
					result = computedStyle.textOverflow;
					break;
				case StylePropertyId.Top:
					result = computedStyle.top;
					break;
				case StylePropertyId.UnityBackgroundImageTintColor:
					result = computedStyle.unityBackgroundImageTintColor;
					break;
				case StylePropertyId.UnityBackgroundScaleMode:
					result = computedStyle.unityBackgroundScaleMode;
					break;
				case StylePropertyId.UnityOverflowClipBox:
					result = computedStyle.unityOverflowClipBox;
					break;
				case StylePropertyId.UnitySliceBottom:
					result = computedStyle.unitySliceBottom;
					break;
				case StylePropertyId.UnitySliceLeft:
					result = computedStyle.unitySliceLeft;
					break;
				case StylePropertyId.UnitySliceRight:
					result = computedStyle.unitySliceRight;
					break;
				case StylePropertyId.UnitySliceTop:
					result = computedStyle.unitySliceTop;
					break;
				case StylePropertyId.UnityTextOverflowPosition:
					result = computedStyle.unityTextOverflowPosition;
					break;
				case StylePropertyId.Width:
					result = computedStyle.width;
					break;
				default:
					Debug.LogAssertion(string.Format("Cannot get computed style value for property id {0}", id));
					result = null;
					break;
				}
				break;
			}
			return result;
		}

		public static object GetInlineStyleValue(IStyle style, StylePropertyId id)
		{
			object result;
			switch (id)
			{
			case StylePropertyId.Color:
				result = style.color;
				break;
			case StylePropertyId.FontSize:
				result = style.fontSize;
				break;
			case StylePropertyId.UnityFont:
				result = style.unityFont;
				break;
			case StylePropertyId.UnityFontStyleAndWeight:
				result = style.unityFontStyleAndWeight;
				break;
			case StylePropertyId.UnityTextAlign:
				result = style.unityTextAlign;
				break;
			case StylePropertyId.Visibility:
				result = style.visibility;
				break;
			case StylePropertyId.WhiteSpace:
				result = style.whiteSpace;
				break;
			default:
				switch (id)
				{
				case StylePropertyId.AlignContent:
					result = style.alignContent;
					break;
				case StylePropertyId.AlignItems:
					result = style.alignItems;
					break;
				case StylePropertyId.AlignSelf:
					result = style.alignSelf;
					break;
				case StylePropertyId.BackgroundColor:
					result = style.backgroundColor;
					break;
				case StylePropertyId.BackgroundImage:
					result = style.backgroundImage;
					break;
				case StylePropertyId.BorderBottomColor:
					result = style.borderBottomColor;
					break;
				case StylePropertyId.BorderBottomLeftRadius:
					result = style.borderBottomLeftRadius;
					break;
				case StylePropertyId.BorderBottomRightRadius:
					result = style.borderBottomRightRadius;
					break;
				case StylePropertyId.BorderBottomWidth:
					result = style.borderBottomWidth;
					break;
				case StylePropertyId.BorderLeftColor:
					result = style.borderLeftColor;
					break;
				case StylePropertyId.BorderLeftWidth:
					result = style.borderLeftWidth;
					break;
				case StylePropertyId.BorderRightColor:
					result = style.borderRightColor;
					break;
				case StylePropertyId.BorderRightWidth:
					result = style.borderRightWidth;
					break;
				case StylePropertyId.BorderTopColor:
					result = style.borderTopColor;
					break;
				case StylePropertyId.BorderTopLeftRadius:
					result = style.borderTopLeftRadius;
					break;
				case StylePropertyId.BorderTopRightRadius:
					result = style.borderTopRightRadius;
					break;
				case StylePropertyId.BorderTopWidth:
					result = style.borderTopWidth;
					break;
				case StylePropertyId.Bottom:
					result = style.bottom;
					break;
				case StylePropertyId.Cursor:
					result = style.cursor;
					break;
				case StylePropertyId.Display:
					result = style.display;
					break;
				case StylePropertyId.FlexBasis:
					result = style.flexBasis;
					break;
				case StylePropertyId.FlexDirection:
					result = style.flexDirection;
					break;
				case StylePropertyId.FlexGrow:
					result = style.flexGrow;
					break;
				case StylePropertyId.FlexShrink:
					result = style.flexShrink;
					break;
				case StylePropertyId.FlexWrap:
					result = style.flexWrap;
					break;
				case StylePropertyId.Height:
					result = style.height;
					break;
				case StylePropertyId.JustifyContent:
					result = style.justifyContent;
					break;
				case StylePropertyId.Left:
					result = style.left;
					break;
				case StylePropertyId.MarginBottom:
					result = style.marginBottom;
					break;
				case StylePropertyId.MarginLeft:
					result = style.marginLeft;
					break;
				case StylePropertyId.MarginRight:
					result = style.marginRight;
					break;
				case StylePropertyId.MarginTop:
					result = style.marginTop;
					break;
				case StylePropertyId.MaxHeight:
					result = style.maxHeight;
					break;
				case StylePropertyId.MaxWidth:
					result = style.maxWidth;
					break;
				case StylePropertyId.MinHeight:
					result = style.minHeight;
					break;
				case StylePropertyId.MinWidth:
					result = style.minWidth;
					break;
				case StylePropertyId.Opacity:
					result = style.opacity;
					break;
				case StylePropertyId.Overflow:
					result = style.overflow;
					break;
				case StylePropertyId.PaddingBottom:
					result = style.paddingBottom;
					break;
				case StylePropertyId.PaddingLeft:
					result = style.paddingLeft;
					break;
				case StylePropertyId.PaddingRight:
					result = style.paddingRight;
					break;
				case StylePropertyId.PaddingTop:
					result = style.paddingTop;
					break;
				case StylePropertyId.Position:
					result = style.position;
					break;
				case StylePropertyId.Right:
					result = style.right;
					break;
				case StylePropertyId.TextOverflow:
					result = style.textOverflow;
					break;
				case StylePropertyId.Top:
					result = style.top;
					break;
				case StylePropertyId.UnityBackgroundImageTintColor:
					result = style.unityBackgroundImageTintColor;
					break;
				case StylePropertyId.UnityBackgroundScaleMode:
					result = style.unityBackgroundScaleMode;
					break;
				case StylePropertyId.UnityOverflowClipBox:
					result = style.unityOverflowClipBox;
					break;
				case StylePropertyId.UnitySliceBottom:
					result = style.unitySliceBottom;
					break;
				case StylePropertyId.UnitySliceLeft:
					result = style.unitySliceLeft;
					break;
				case StylePropertyId.UnitySliceRight:
					result = style.unitySliceRight;
					break;
				case StylePropertyId.UnitySliceTop:
					result = style.unitySliceTop;
					break;
				case StylePropertyId.UnityTextOverflowPosition:
					result = style.unityTextOverflowPosition;
					break;
				case StylePropertyId.Width:
					result = style.width;
					break;
				default:
					Debug.LogAssertion(string.Format("Cannot get inline style value for property id {0}", id));
					result = null;
					break;
				}
				break;
			}
			return result;
		}

		public static void SetInlineStyleValue(IStyle style, StylePropertyId id, object value)
		{
			switch (id)
			{
			case StylePropertyId.Color:
				style.color = (StyleColor)value;
				break;
			case StylePropertyId.FontSize:
				style.fontSize = (StyleLength)value;
				break;
			case StylePropertyId.UnityFont:
				style.unityFont = (StyleFont)value;
				break;
			case StylePropertyId.UnityFontStyleAndWeight:
				style.unityFontStyleAndWeight = (StyleEnum<FontStyle>)value;
				break;
			case StylePropertyId.UnityTextAlign:
				style.unityTextAlign = (StyleEnum<TextAnchor>)value;
				break;
			case StylePropertyId.Visibility:
				style.visibility = (StyleEnum<Visibility>)value;
				break;
			case StylePropertyId.WhiteSpace:
				style.whiteSpace = (StyleEnum<WhiteSpace>)value;
				break;
			default:
				switch (id)
				{
				case StylePropertyId.AlignContent:
					style.alignContent = (StyleEnum<Align>)value;
					break;
				case StylePropertyId.AlignItems:
					style.alignItems = (StyleEnum<Align>)value;
					break;
				case StylePropertyId.AlignSelf:
					style.alignSelf = (StyleEnum<Align>)value;
					break;
				case StylePropertyId.BackgroundColor:
					style.backgroundColor = (StyleColor)value;
					break;
				case StylePropertyId.BackgroundImage:
					style.backgroundImage = (StyleBackground)value;
					break;
				case StylePropertyId.BorderBottomColor:
					style.borderBottomColor = (StyleColor)value;
					break;
				case StylePropertyId.BorderBottomLeftRadius:
					style.borderBottomLeftRadius = (StyleLength)value;
					break;
				case StylePropertyId.BorderBottomRightRadius:
					style.borderBottomRightRadius = (StyleLength)value;
					break;
				case StylePropertyId.BorderBottomWidth:
					style.borderBottomWidth = (StyleFloat)value;
					break;
				case StylePropertyId.BorderLeftColor:
					style.borderLeftColor = (StyleColor)value;
					break;
				case StylePropertyId.BorderLeftWidth:
					style.borderLeftWidth = (StyleFloat)value;
					break;
				case StylePropertyId.BorderRightColor:
					style.borderRightColor = (StyleColor)value;
					break;
				case StylePropertyId.BorderRightWidth:
					style.borderRightWidth = (StyleFloat)value;
					break;
				case StylePropertyId.BorderTopColor:
					style.borderTopColor = (StyleColor)value;
					break;
				case StylePropertyId.BorderTopLeftRadius:
					style.borderTopLeftRadius = (StyleLength)value;
					break;
				case StylePropertyId.BorderTopRightRadius:
					style.borderTopRightRadius = (StyleLength)value;
					break;
				case StylePropertyId.BorderTopWidth:
					style.borderTopWidth = (StyleFloat)value;
					break;
				case StylePropertyId.Bottom:
					style.bottom = (StyleLength)value;
					break;
				case StylePropertyId.Cursor:
					style.cursor = (StyleCursor)value;
					break;
				case StylePropertyId.Display:
					style.display = (StyleEnum<DisplayStyle>)value;
					break;
				case StylePropertyId.FlexBasis:
					style.flexBasis = (StyleLength)value;
					break;
				case StylePropertyId.FlexDirection:
					style.flexDirection = (StyleEnum<FlexDirection>)value;
					break;
				case StylePropertyId.FlexGrow:
					style.flexGrow = (StyleFloat)value;
					break;
				case StylePropertyId.FlexShrink:
					style.flexShrink = (StyleFloat)value;
					break;
				case StylePropertyId.FlexWrap:
					style.flexWrap = (StyleEnum<Wrap>)value;
					break;
				case StylePropertyId.Height:
					style.height = (StyleLength)value;
					break;
				case StylePropertyId.JustifyContent:
					style.justifyContent = (StyleEnum<Justify>)value;
					break;
				case StylePropertyId.Left:
					style.left = (StyleLength)value;
					break;
				case StylePropertyId.MarginBottom:
					style.marginBottom = (StyleLength)value;
					break;
				case StylePropertyId.MarginLeft:
					style.marginLeft = (StyleLength)value;
					break;
				case StylePropertyId.MarginRight:
					style.marginRight = (StyleLength)value;
					break;
				case StylePropertyId.MarginTop:
					style.marginTop = (StyleLength)value;
					break;
				case StylePropertyId.MaxHeight:
					style.maxHeight = (StyleLength)value;
					break;
				case StylePropertyId.MaxWidth:
					style.maxWidth = (StyleLength)value;
					break;
				case StylePropertyId.MinHeight:
					style.minHeight = (StyleLength)value;
					break;
				case StylePropertyId.MinWidth:
					style.minWidth = (StyleLength)value;
					break;
				case StylePropertyId.Opacity:
					style.opacity = (StyleFloat)value;
					break;
				case StylePropertyId.Overflow:
					style.overflow = (StyleEnum<Overflow>)value;
					break;
				case StylePropertyId.PaddingBottom:
					style.paddingBottom = (StyleLength)value;
					break;
				case StylePropertyId.PaddingLeft:
					style.paddingLeft = (StyleLength)value;
					break;
				case StylePropertyId.PaddingRight:
					style.paddingRight = (StyleLength)value;
					break;
				case StylePropertyId.PaddingTop:
					style.paddingTop = (StyleLength)value;
					break;
				case StylePropertyId.Position:
					style.position = (StyleEnum<Position>)value;
					break;
				case StylePropertyId.Right:
					style.right = (StyleLength)value;
					break;
				case StylePropertyId.TextOverflow:
					style.textOverflow = (StyleEnum<TextOverflow>)value;
					break;
				case StylePropertyId.Top:
					style.top = (StyleLength)value;
					break;
				case StylePropertyId.UnityBackgroundImageTintColor:
					style.unityBackgroundImageTintColor = (StyleColor)value;
					break;
				case StylePropertyId.UnityBackgroundScaleMode:
					style.unityBackgroundScaleMode = (StyleEnum<ScaleMode>)value;
					break;
				case StylePropertyId.UnityOverflowClipBox:
					style.unityOverflowClipBox = (StyleEnum<OverflowClipBox>)value;
					break;
				case StylePropertyId.UnitySliceBottom:
					style.unitySliceBottom = (StyleInt)value;
					break;
				case StylePropertyId.UnitySliceLeft:
					style.unitySliceLeft = (StyleInt)value;
					break;
				case StylePropertyId.UnitySliceRight:
					style.unitySliceRight = (StyleInt)value;
					break;
				case StylePropertyId.UnitySliceTop:
					style.unitySliceTop = (StyleInt)value;
					break;
				case StylePropertyId.UnityTextOverflowPosition:
					style.unityTextOverflowPosition = (StyleEnum<TextOverflowPosition>)value;
					break;
				case StylePropertyId.Width:
					style.width = (StyleLength)value;
					break;
				default:
					Debug.LogAssertion(string.Format("Cannot set inline style value for property id {0}", id));
					break;
				}
				break;
			}
		}

		public static Type GetComputedStyleType(StylePropertyId id)
		{
			Type result;
			switch (id)
			{
			case StylePropertyId.Color:
				result = typeof(StyleColor);
				break;
			case StylePropertyId.FontSize:
				result = typeof(StyleLength);
				break;
			case StylePropertyId.UnityFont:
				result = typeof(StyleFont);
				break;
			case StylePropertyId.UnityFontStyleAndWeight:
				result = typeof(StyleEnum<FontStyle>);
				break;
			case StylePropertyId.UnityTextAlign:
				result = typeof(StyleEnum<TextAnchor>);
				break;
			case StylePropertyId.Visibility:
				result = typeof(StyleEnum<Visibility>);
				break;
			case StylePropertyId.WhiteSpace:
				result = typeof(StyleEnum<WhiteSpace>);
				break;
			default:
				switch (id)
				{
				case StylePropertyId.AlignContent:
					result = typeof(StyleEnum<Align>);
					break;
				case StylePropertyId.AlignItems:
					result = typeof(StyleEnum<Align>);
					break;
				case StylePropertyId.AlignSelf:
					result = typeof(StyleEnum<Align>);
					break;
				case StylePropertyId.BackgroundColor:
					result = typeof(StyleColor);
					break;
				case StylePropertyId.BackgroundImage:
					result = typeof(StyleBackground);
					break;
				case StylePropertyId.BorderBottomColor:
					result = typeof(StyleColor);
					break;
				case StylePropertyId.BorderBottomLeftRadius:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.BorderBottomRightRadius:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.BorderBottomWidth:
					result = typeof(StyleFloat);
					break;
				case StylePropertyId.BorderLeftColor:
					result = typeof(StyleColor);
					break;
				case StylePropertyId.BorderLeftWidth:
					result = typeof(StyleFloat);
					break;
				case StylePropertyId.BorderRightColor:
					result = typeof(StyleColor);
					break;
				case StylePropertyId.BorderRightWidth:
					result = typeof(StyleFloat);
					break;
				case StylePropertyId.BorderTopColor:
					result = typeof(StyleColor);
					break;
				case StylePropertyId.BorderTopLeftRadius:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.BorderTopRightRadius:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.BorderTopWidth:
					result = typeof(StyleFloat);
					break;
				case StylePropertyId.Bottom:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.Cursor:
					result = typeof(StyleCursor);
					break;
				case StylePropertyId.Display:
					result = typeof(StyleEnum<DisplayStyle>);
					break;
				case StylePropertyId.FlexBasis:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.FlexDirection:
					result = typeof(StyleEnum<FlexDirection>);
					break;
				case StylePropertyId.FlexGrow:
					result = typeof(StyleFloat);
					break;
				case StylePropertyId.FlexShrink:
					result = typeof(StyleFloat);
					break;
				case StylePropertyId.FlexWrap:
					result = typeof(StyleEnum<Wrap>);
					break;
				case StylePropertyId.Height:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.JustifyContent:
					result = typeof(StyleEnum<Justify>);
					break;
				case StylePropertyId.Left:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.MarginBottom:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.MarginLeft:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.MarginRight:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.MarginTop:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.MaxHeight:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.MaxWidth:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.MinHeight:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.MinWidth:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.Opacity:
					result = typeof(StyleFloat);
					break;
				case StylePropertyId.Overflow:
					result = typeof(StyleEnum<Overflow>);
					break;
				case StylePropertyId.PaddingBottom:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.PaddingLeft:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.PaddingRight:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.PaddingTop:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.Position:
					result = typeof(StyleEnum<Position>);
					break;
				case StylePropertyId.Right:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.TextOverflow:
					result = typeof(StyleEnum<TextOverflow>);
					break;
				case StylePropertyId.Top:
					result = typeof(StyleLength);
					break;
				case StylePropertyId.UnityBackgroundImageTintColor:
					result = typeof(StyleColor);
					break;
				case StylePropertyId.UnityBackgroundScaleMode:
					result = typeof(StyleEnum<ScaleMode>);
					break;
				case StylePropertyId.UnityOverflowClipBox:
					result = typeof(StyleEnum<OverflowClipBox>);
					break;
				case StylePropertyId.UnitySliceBottom:
					result = typeof(StyleInt);
					break;
				case StylePropertyId.UnitySliceLeft:
					result = typeof(StyleInt);
					break;
				case StylePropertyId.UnitySliceRight:
					result = typeof(StyleInt);
					break;
				case StylePropertyId.UnitySliceTop:
					result = typeof(StyleInt);
					break;
				case StylePropertyId.UnityTextOverflowPosition:
					result = typeof(StyleEnum<TextOverflowPosition>);
					break;
				case StylePropertyId.Width:
					result = typeof(StyleLength);
					break;
				default:
					Debug.LogAssertion(string.Format("Cannot get computed style type for property id {0}", id));
					result = null;
					break;
				}
				break;
			}
			return result;
		}

		public static string[] GetLonghandPropertyNames(StylePropertyId id)
		{
			string[] result;
			switch (id)
			{
			case StylePropertyId.BorderColor:
				result = new string[]
				{
					"border-top-color",
					"border-right-color",
					"border-bottom-color",
					"border-left-color"
				};
				break;
			case StylePropertyId.BorderRadius:
				result = new string[]
				{
					"border-top-left-radius",
					"border-top-right-radius",
					"border-bottom-right-radius",
					"border-bottom-left-radius"
				};
				break;
			case StylePropertyId.BorderWidth:
				result = new string[]
				{
					"border-top-width",
					"border-right-width",
					"border-bottom-width",
					"border-left-width"
				};
				break;
			case StylePropertyId.Flex:
				result = new string[]
				{
					"flex-grow",
					"flex-shrink",
					"flex-basis"
				};
				break;
			case StylePropertyId.Margin:
				result = new string[]
				{
					"margin-top",
					"margin-right",
					"margin-bottom",
					"margin-left"
				};
				break;
			case StylePropertyId.Padding:
				result = new string[]
				{
					"padding-top",
					"padding-right",
					"padding-bottom",
					"padding-left"
				};
				break;
			default:
				Debug.LogAssertion(string.Format("Cannot get longhand property names for property id {0}", id));
				result = null;
				break;
			}
			return result;
		}

		public static bool IsShorthandProperty(StylePropertyId id)
		{
			bool result;
			switch (id)
			{
			case StylePropertyId.BorderColor:
				result = true;
				break;
			case StylePropertyId.BorderRadius:
				result = true;
				break;
			case StylePropertyId.BorderWidth:
				result = true;
				break;
			case StylePropertyId.Flex:
				result = true;
				break;
			case StylePropertyId.Margin:
				result = true;
				break;
			case StylePropertyId.Padding:
				result = true;
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		public static bool IsInheritedProperty(StylePropertyId id)
		{
			bool result;
			switch (id)
			{
			case StylePropertyId.Color:
				result = true;
				break;
			case StylePropertyId.FontSize:
				result = true;
				break;
			case StylePropertyId.UnityFont:
				result = true;
				break;
			case StylePropertyId.UnityFontStyleAndWeight:
				result = true;
				break;
			case StylePropertyId.UnityTextAlign:
				result = true;
				break;
			case StylePropertyId.Visibility:
				result = true;
				break;
			case StylePropertyId.WhiteSpace:
				result = true;
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		public static StylePropertyId[] GetInheritedProperties()
		{
			return new StylePropertyId[]
			{
				StylePropertyId.Color,
				StylePropertyId.FontSize,
				StylePropertyId.UnityFont,
				StylePropertyId.UnityFontStyleAndWeight,
				StylePropertyId.UnityTextAlign,
				StylePropertyId.Visibility,
				StylePropertyId.WhiteSpace
			};
		}
	}
}
