using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	internal class InlineStyleAccess : StyleValueCollection, IStyle
	{
		internal struct InlineRule
		{
			public StyleSheet sheet;

			public StyleProperty[] properties;

			public StylePropertyId[] propertyIds;
		}

		private static StylePropertyReader s_StylePropertyReader = new StylePropertyReader();

		private bool m_HasInlineCursor;

		private StyleCursor m_InlineCursor;

		private InlineStyleAccess.InlineRule m_InlineRule;

		private VisualElement ve
		{
			get;
			set;
		}

		public InlineStyleAccess.InlineRule inlineRule
		{
			get
			{
				return this.m_InlineRule;
			}
		}

		StyleCursor IStyle.cursor
		{
			get
			{
				StyleCursor styleCursor = default(StyleCursor);
				bool flag = this.TryGetInlineCursor(ref styleCursor);
				StyleCursor result;
				if (flag)
				{
					result = styleCursor;
				}
				else
				{
					result = StyleKeyword.Null;
				}
				return result;
			}
			set
			{
				bool flag = this.SetInlineCursor(value, this.ve.sharedStyle.cursor);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles);
				}
			}
		}

		StyleEnum<Align> IStyle.alignContent
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.AlignContent);
				return new StyleEnum<Align>((Align)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<Align> inlineValue = new StyleEnum<Align>(value.value, value.keyword);
				bool flag = this.SetStyleValue<Align>(StylePropertyId.AlignContent, inlineValue, this.ve.sharedStyle.alignContent);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.AlignContent = (YogaAlign)this.ve.computedStyle.alignContent.value;
				}
			}
		}

		StyleEnum<Align> IStyle.alignItems
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.AlignItems);
				return new StyleEnum<Align>((Align)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<Align> inlineValue = new StyleEnum<Align>(value.value, value.keyword);
				bool flag = this.SetStyleValue<Align>(StylePropertyId.AlignItems, inlineValue, this.ve.sharedStyle.alignItems);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.AlignItems = (YogaAlign)this.ve.computedStyle.alignItems.value;
				}
			}
		}

		StyleEnum<Align> IStyle.alignSelf
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.AlignSelf);
				return new StyleEnum<Align>((Align)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<Align> inlineValue = new StyleEnum<Align>(value.value, value.keyword);
				bool flag = this.SetStyleValue<Align>(StylePropertyId.AlignSelf, inlineValue, this.ve.sharedStyle.alignSelf);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.AlignSelf = (YogaAlign)this.ve.computedStyle.alignSelf.value;
				}
			}
		}

		StyleColor IStyle.backgroundColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BackgroundColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundColor, value, this.ve.sharedStyle.backgroundColor);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleBackground IStyle.backgroundImage
		{
			get
			{
				return base.GetStyleBackground(StylePropertyId.BackgroundImage);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundImage, value, this.ve.sharedStyle.backgroundImage);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleColor IStyle.borderBottomColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderBottomColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomColor, value, this.ve.sharedStyle.borderBottomColor);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleLength IStyle.borderBottomLeftRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderBottomLeftRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomLeftRadius, value, this.ve.sharedStyle.borderBottomLeftRadius);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		StyleLength IStyle.borderBottomRightRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderBottomRightRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomRightRadius, value, this.ve.sharedStyle.borderBottomRightRadius);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		StyleFloat IStyle.borderBottomWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderBottomWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomWidth, value, this.ve.sharedStyle.borderBottomWidth);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.yogaNode.BorderBottomWidth = this.ve.computedStyle.borderBottomWidth.value;
				}
			}
		}

		StyleColor IStyle.borderLeftColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderLeftColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderLeftColor, value, this.ve.sharedStyle.borderLeftColor);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleFloat IStyle.borderLeftWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderLeftWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderLeftWidth, value, this.ve.sharedStyle.borderLeftWidth);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.yogaNode.BorderLeftWidth = this.ve.computedStyle.borderLeftWidth.value;
				}
			}
		}

		StyleColor IStyle.borderRightColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderRightColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderRightColor, value, this.ve.sharedStyle.borderRightColor);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleFloat IStyle.borderRightWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderRightWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderRightWidth, value, this.ve.sharedStyle.borderRightWidth);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.yogaNode.BorderRightWidth = this.ve.computedStyle.borderRightWidth.value;
				}
			}
		}

		StyleColor IStyle.borderTopColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderTopColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopColor, value, this.ve.sharedStyle.borderTopColor);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleLength IStyle.borderTopLeftRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderTopLeftRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopLeftRadius, value, this.ve.sharedStyle.borderTopLeftRadius);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		StyleLength IStyle.borderTopRightRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderTopRightRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopRightRadius, value, this.ve.sharedStyle.borderTopRightRadius);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		StyleFloat IStyle.borderTopWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderTopWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopWidth, value, this.ve.sharedStyle.borderTopWidth);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.yogaNode.BorderTopWidth = this.ve.computedStyle.borderTopWidth.value;
				}
			}
		}

		StyleLength IStyle.bottom
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Bottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Bottom, value, this.ve.sharedStyle.bottom);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Bottom = this.ve.computedStyle.bottom.ToYogaValue();
				}
			}
		}

		StyleColor IStyle.color
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.Color);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Color, value, this.ve.sharedStyle.color);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleEnum<DisplayStyle> IStyle.display
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Display);
				return new StyleEnum<DisplayStyle>((DisplayStyle)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<DisplayStyle> inlineValue = new StyleEnum<DisplayStyle>(value.value, value.keyword);
				bool flag = this.SetStyleValue<DisplayStyle>(StylePropertyId.Display, inlineValue, this.ve.sharedStyle.display);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Display = (YogaDisplay)this.ve.computedStyle.display.value;
				}
			}
		}

		StyleLength IStyle.flexBasis
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.FlexBasis);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FlexBasis, value, this.ve.sharedStyle.flexBasis);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.FlexBasis = this.ve.computedStyle.flexBasis.ToYogaValue();
				}
			}
		}

		StyleEnum<FlexDirection> IStyle.flexDirection
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.FlexDirection);
				return new StyleEnum<FlexDirection>((FlexDirection)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<FlexDirection> inlineValue = new StyleEnum<FlexDirection>(value.value, value.keyword);
				bool flag = this.SetStyleValue<FlexDirection>(StylePropertyId.FlexDirection, inlineValue, this.ve.sharedStyle.flexDirection);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.FlexDirection = (YogaFlexDirection)this.ve.computedStyle.flexDirection.value;
				}
			}
		}

		StyleFloat IStyle.flexGrow
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.FlexGrow);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FlexGrow, value, this.ve.sharedStyle.flexGrow);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.FlexGrow = this.ve.computedStyle.flexGrow.value;
				}
			}
		}

		StyleFloat IStyle.flexShrink
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.FlexShrink);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FlexShrink, value, this.ve.sharedStyle.flexShrink);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.FlexShrink = this.ve.computedStyle.flexShrink.value;
				}
			}
		}

		StyleEnum<Wrap> IStyle.flexWrap
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.FlexWrap);
				return new StyleEnum<Wrap>((Wrap)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<Wrap> inlineValue = new StyleEnum<Wrap>(value.value, value.keyword);
				bool flag = this.SetStyleValue<Wrap>(StylePropertyId.FlexWrap, inlineValue, this.ve.sharedStyle.flexWrap);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Wrap = (YogaWrap)this.ve.computedStyle.flexWrap.value;
				}
			}
		}

		StyleLength IStyle.fontSize
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.FontSize);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FontSize, value, this.ve.sharedStyle.fontSize);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles);
				}
			}
		}

		StyleLength IStyle.height
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Height);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Height, value, this.ve.sharedStyle.height);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Height = this.ve.computedStyle.height.ToYogaValue();
				}
			}
		}

		StyleEnum<Justify> IStyle.justifyContent
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.JustifyContent);
				return new StyleEnum<Justify>((Justify)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<Justify> inlineValue = new StyleEnum<Justify>(value.value, value.keyword);
				bool flag = this.SetStyleValue<Justify>(StylePropertyId.JustifyContent, inlineValue, this.ve.sharedStyle.justifyContent);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.JustifyContent = (YogaJustify)this.ve.computedStyle.justifyContent.value;
				}
			}
		}

		StyleLength IStyle.left
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Left);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Left, value, this.ve.sharedStyle.left);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Left = this.ve.computedStyle.left.ToYogaValue();
				}
			}
		}

		StyleLength IStyle.marginBottom
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginBottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginBottom, value, this.ve.sharedStyle.marginBottom);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MarginBottom = this.ve.computedStyle.marginBottom.ToYogaValue();
				}
			}
		}

		StyleLength IStyle.marginLeft
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginLeft);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginLeft, value, this.ve.sharedStyle.marginLeft);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MarginLeft = this.ve.computedStyle.marginLeft.ToYogaValue();
				}
			}
		}

		StyleLength IStyle.marginRight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginRight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginRight, value, this.ve.sharedStyle.marginRight);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MarginRight = this.ve.computedStyle.marginRight.ToYogaValue();
				}
			}
		}

		StyleLength IStyle.marginTop
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginTop);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginTop, value, this.ve.sharedStyle.marginTop);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MarginTop = this.ve.computedStyle.marginTop.ToYogaValue();
				}
			}
		}

		StyleLength IStyle.maxHeight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MaxHeight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MaxHeight, value, this.ve.sharedStyle.maxHeight);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MaxHeight = this.ve.computedStyle.maxHeight.ToYogaValue();
				}
			}
		}

		StyleLength IStyle.maxWidth
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MaxWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MaxWidth, value, this.ve.sharedStyle.maxWidth);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MaxWidth = this.ve.computedStyle.maxWidth.ToYogaValue();
				}
			}
		}

		StyleLength IStyle.minHeight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MinHeight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MinHeight, value, this.ve.sharedStyle.minHeight);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MinHeight = this.ve.computedStyle.minHeight.ToYogaValue();
				}
			}
		}

		StyleLength IStyle.minWidth
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MinWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MinWidth, value, this.ve.sharedStyle.minWidth);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MinWidth = this.ve.computedStyle.minWidth.ToYogaValue();
				}
			}
		}

		StyleFloat IStyle.opacity
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.Opacity);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Opacity, value, this.ve.sharedStyle.opacity);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Opacity);
				}
			}
		}

		StyleEnum<Overflow> IStyle.overflow
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Overflow);
				return new StyleEnum<Overflow>((Overflow)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<OverflowInternal> inlineValue = new StyleEnum<OverflowInternal>((OverflowInternal)value.value, value.keyword);
				bool flag = this.SetStyleValue<OverflowInternal>(StylePropertyId.Overflow, inlineValue, this.ve.sharedStyle.overflow);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Overflow);
					this.ve.yogaNode.Overflow = (YogaOverflow)this.ve.computedStyle.overflow.value;
				}
			}
		}

		StyleLength IStyle.paddingBottom
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingBottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingBottom, value, this.ve.sharedStyle.paddingBottom);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.PaddingBottom = this.ve.computedStyle.paddingBottom.ToYogaValue();
				}
			}
		}

		StyleLength IStyle.paddingLeft
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingLeft);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingLeft, value, this.ve.sharedStyle.paddingLeft);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.PaddingLeft = this.ve.computedStyle.paddingLeft.ToYogaValue();
				}
			}
		}

		StyleLength IStyle.paddingRight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingRight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingRight, value, this.ve.sharedStyle.paddingRight);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.PaddingRight = this.ve.computedStyle.paddingRight.ToYogaValue();
				}
			}
		}

		StyleLength IStyle.paddingTop
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingTop);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingTop, value, this.ve.sharedStyle.paddingTop);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.PaddingTop = this.ve.computedStyle.paddingTop.ToYogaValue();
				}
			}
		}

		StyleEnum<Position> IStyle.position
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Position);
				return new StyleEnum<Position>((Position)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<Position> inlineValue = new StyleEnum<Position>(value.value, value.keyword);
				bool flag = this.SetStyleValue<Position>(StylePropertyId.Position, inlineValue, this.ve.sharedStyle.position);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.PositionType = (YogaPositionType)this.ve.computedStyle.position.value;
				}
			}
		}

		StyleLength IStyle.right
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Right);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Right, value, this.ve.sharedStyle.right);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Right = this.ve.computedStyle.right.ToYogaValue();
				}
			}
		}

		StyleEnum<TextOverflow> IStyle.textOverflow
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.TextOverflow);
				return new StyleEnum<TextOverflow>((TextOverflow)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<TextOverflow> inlineValue = new StyleEnum<TextOverflow>(value.value, value.keyword);
				bool flag = this.SetStyleValue<TextOverflow>(StylePropertyId.TextOverflow, inlineValue, this.ve.sharedStyle.textOverflow);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleLength IStyle.top
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Top);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Top, value, this.ve.sharedStyle.top);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Top = this.ve.computedStyle.top.ToYogaValue();
				}
			}
		}

		StyleColor IStyle.unityBackgroundImageTintColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.UnityBackgroundImageTintColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityBackgroundImageTintColor, value, this.ve.sharedStyle.unityBackgroundImageTintColor);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleEnum<ScaleMode> IStyle.unityBackgroundScaleMode
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityBackgroundScaleMode);
				return new StyleEnum<ScaleMode>((ScaleMode)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<ScaleMode> inlineValue = new StyleEnum<ScaleMode>(value.value, value.keyword);
				bool flag = this.SetStyleValue<ScaleMode>(StylePropertyId.UnityBackgroundScaleMode, inlineValue, this.ve.sharedStyle.unityBackgroundScaleMode);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleFont IStyle.unityFont
		{
			get
			{
				return base.GetStyleFont(StylePropertyId.UnityFont);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityFont, value, this.ve.sharedStyle.unityFont);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleEnum<FontStyle> IStyle.unityFontStyleAndWeight
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityFontStyleAndWeight);
				return new StyleEnum<FontStyle>((FontStyle)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<FontStyle> inlineValue = new StyleEnum<FontStyle>(value.value, value.keyword);
				bool flag = this.SetStyleValue<FontStyle>(StylePropertyId.UnityFontStyleAndWeight, inlineValue, this.ve.sharedStyle.unityFontStyleAndWeight);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleEnum<OverflowClipBox> IStyle.unityOverflowClipBox
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityOverflowClipBox);
				return new StyleEnum<OverflowClipBox>((OverflowClipBox)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<OverflowClipBox> inlineValue = new StyleEnum<OverflowClipBox>(value.value, value.keyword);
				bool flag = this.SetStyleValue<OverflowClipBox>(StylePropertyId.UnityOverflowClipBox, inlineValue, this.ve.sharedStyle.unityOverflowClipBox);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleInt IStyle.unitySliceBottom
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceBottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceBottom, value, this.ve.sharedStyle.unitySliceBottom);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleInt IStyle.unitySliceLeft
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceLeft);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceLeft, value, this.ve.sharedStyle.unitySliceLeft);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleInt IStyle.unitySliceRight
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceRight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceRight, value, this.ve.sharedStyle.unitySliceRight);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleInt IStyle.unitySliceTop
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceTop);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceTop, value, this.ve.sharedStyle.unitySliceTop);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleEnum<TextAnchor> IStyle.unityTextAlign
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityTextAlign);
				return new StyleEnum<TextAnchor>((TextAnchor)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<TextAnchor> inlineValue = new StyleEnum<TextAnchor>(value.value, value.keyword);
				bool flag = this.SetStyleValue<TextAnchor>(StylePropertyId.UnityTextAlign, inlineValue, this.ve.sharedStyle.unityTextAlign);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleEnum<TextOverflowPosition> IStyle.unityTextOverflowPosition
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityTextOverflowPosition);
				return new StyleEnum<TextOverflowPosition>((TextOverflowPosition)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<TextOverflowPosition> inlineValue = new StyleEnum<TextOverflowPosition>(value.value, value.keyword);
				bool flag = this.SetStyleValue<TextOverflowPosition>(StylePropertyId.UnityTextOverflowPosition, inlineValue, this.ve.sharedStyle.unityTextOverflowPosition);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleEnum<Visibility> IStyle.visibility
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Visibility);
				return new StyleEnum<Visibility>((Visibility)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<Visibility> inlineValue = new StyleEnum<Visibility>(value.value, value.keyword);
				bool flag = this.SetStyleValue<Visibility>(StylePropertyId.Visibility, inlineValue, this.ve.sharedStyle.visibility);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		StyleEnum<WhiteSpace> IStyle.whiteSpace
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.WhiteSpace);
				return new StyleEnum<WhiteSpace>((WhiteSpace)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<WhiteSpace> inlineValue = new StyleEnum<WhiteSpace>(value.value, value.keyword);
				bool flag = this.SetStyleValue<WhiteSpace>(StylePropertyId.WhiteSpace, inlineValue, this.ve.sharedStyle.whiteSpace);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles);
				}
			}
		}

		StyleLength IStyle.width
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Width);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Width, value, this.ve.sharedStyle.width);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Width = this.ve.computedStyle.width.ToYogaValue();
				}
			}
		}

		public InlineStyleAccess(VisualElement ve)
		{
			this.ve = ve;
			bool isShared = ve.computedStyle.isShared;
			if (isShared)
			{
				ComputedStyle computedStyle = ComputedStyle.Create(false);
				computedStyle.CopyShared(ve.m_SharedStyle);
				ve.m_Style = computedStyle;
			}
		}

		protected override void Finalize()
		{
			try
			{
				StyleValue styleValue = default(StyleValue);
				bool flag = base.TryGetStyleValue(StylePropertyId.BackgroundImage, ref styleValue);
				if (flag)
				{
					bool isAllocated = styleValue.resource.IsAllocated;
					if (isAllocated)
					{
						styleValue.resource.Free();
					}
				}
				bool flag2 = base.TryGetStyleValue(StylePropertyId.UnityFont, ref styleValue);
				if (flag2)
				{
					bool isAllocated2 = styleValue.resource.IsAllocated;
					if (isAllocated2)
					{
						styleValue.resource.Free();
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		public void SetInlineRule(StyleSheet sheet, StyleRule rule)
		{
			this.m_InlineRule.sheet = sheet;
			this.m_InlineRule.properties = rule.properties;
			this.m_InlineRule.propertyIds = StyleSheetCache.GetPropertyIds(rule);
			this.ApplyInlineStyles(this.ve.sharedStyle);
		}

		public void ApplyInlineStyles(ComputedStyle sharedStyle)
		{
			Debug.Assert(!this.ve.m_Style.isShared);
			this.ve.m_Style.CopyShared(sharedStyle);
			bool flag = this.m_InlineRule.sheet != null;
			if (flag)
			{
				VisualElement expr_55 = this.ve.hierarchy.parent;
				ComputedStyle parentStyle = (expr_55 != null) ? expr_55.computedStyle : null;
				InlineStyleAccess.s_StylePropertyReader.SetInlineContext(this.m_InlineRule.sheet, this.m_InlineRule.properties, this.m_InlineRule.propertyIds, 1f);
				this.ve.m_Style.ApplyProperties(InlineStyleAccess.s_StylePropertyReader, parentStyle);
			}
			foreach (StyleValue current in this.m_Values)
			{
				this.ApplyStyleValue(current);
			}
			bool flag2 = this.ve.style.cursor != StyleKeyword.Null;
			if (flag2)
			{
				this.ve.computedStyle.ApplyStyleCursor(this.ve.style.cursor);
			}
		}

		private bool SetStyleValue(StylePropertyId id, StyleLength inlineValue, StyleLength sharedValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			bool result;
			if (flag)
			{
				bool flag2 = styleValue.length == inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					result = false;
					return result;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					result = false;
					return result;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.length = inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			if (flag4)
			{
				styleValue.keyword = sharedValue.keyword;
				styleValue.length = sharedValue.value;
			}
			this.ApplyStyleValue(styleValue);
			result = true;
			return result;
		}

		private bool SetStyleValue(StylePropertyId id, StyleFloat inlineValue, StyleFloat sharedValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			bool result;
			if (flag)
			{
				bool flag2 = styleValue.number == inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					result = false;
					return result;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					result = false;
					return result;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.number = inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			if (flag4)
			{
				styleValue.keyword = sharedValue.keyword;
				styleValue.number = sharedValue.value;
			}
			this.ApplyStyleValue(styleValue);
			result = true;
			return result;
		}

		private bool SetStyleValue(StylePropertyId id, StyleInt inlineValue, StyleInt sharedValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			bool result;
			if (flag)
			{
				bool flag2 = styleValue.number == (float)inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					result = false;
					return result;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					result = false;
					return result;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.number = (float)inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			if (flag4)
			{
				styleValue.keyword = sharedValue.keyword;
				styleValue.number = (float)sharedValue.value;
			}
			this.ApplyStyleValue(styleValue);
			result = true;
			return result;
		}

		private bool SetStyleValue(StylePropertyId id, StyleColor inlineValue, StyleColor sharedValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			bool result;
			if (flag)
			{
				bool flag2 = styleValue.color == inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					result = false;
					return result;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					result = false;
					return result;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.color = inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			if (flag4)
			{
				styleValue.keyword = sharedValue.keyword;
				styleValue.color = sharedValue.value;
			}
			this.ApplyStyleValue(styleValue);
			result = true;
			return result;
		}

		private bool SetStyleValue<T>(StylePropertyId id, StyleEnum<T> inlineValue, StyleEnum<T> sharedValue) where T : struct, IConvertible
		{
			StyleValue styleValue = default(StyleValue);
			int num = UnsafeUtility.EnumToInt<T>(inlineValue.value);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			bool result;
			if (flag)
			{
				bool flag2 = styleValue.number == (float)num && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					result = false;
					return result;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					result = false;
					return result;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.number = (float)num;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			if (flag4)
			{
				styleValue.keyword = sharedValue.keyword;
				styleValue.number = (float)UnsafeUtility.EnumToInt<T>(sharedValue.value);
			}
			this.ApplyStyleValue(styleValue);
			result = true;
			return result;
		}

		private bool SetStyleValue(StylePropertyId id, StyleBackground inlineValue, StyleBackground sharedValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			bool result;
			if (flag)
			{
				VectorImage x = styleValue.resource.IsAllocated ? (styleValue.resource.Target as VectorImage) : null;
				Texture2D x2 = styleValue.resource.IsAllocated ? (styleValue.resource.Target as Texture2D) : null;
				bool flag2 = x == inlineValue.value.vectorImage && x2 == inlineValue.value.texture && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					result = false;
					return result;
				}
				bool isAllocated = styleValue.resource.IsAllocated;
				if (isAllocated)
				{
					styleValue.resource.Free();
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					result = false;
					return result;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			bool flag4 = inlineValue.value.vectorImage != null;
			if (flag4)
			{
				styleValue.resource = GCHandle.Alloc(inlineValue.value.vectorImage);
			}
			else
			{
				bool flag5 = inlineValue.value.texture != null;
				if (flag5)
				{
					styleValue.resource = GCHandle.Alloc(inlineValue.value.texture);
				}
				else
				{
					styleValue.resource = default(GCHandle);
				}
			}
			base.SetStyleValue(styleValue);
			bool flag6 = inlineValue.keyword == StyleKeyword.Null;
			if (flag6)
			{
				styleValue.keyword = sharedValue.keyword;
				bool flag7 = sharedValue.value.texture != null;
				if (flag7)
				{
					styleValue.resource = GCHandle.Alloc(sharedValue.value.texture);
				}
				else
				{
					bool flag8 = sharedValue.value.vectorImage != null;
					if (flag8)
					{
						styleValue.resource = GCHandle.Alloc(sharedValue.value.vectorImage);
					}
					else
					{
						styleValue.resource = default(GCHandle);
					}
				}
			}
			this.ApplyStyleValue(styleValue);
			result = true;
			return result;
		}

		private bool SetStyleValue(StylePropertyId id, StyleFont inlineValue, StyleFont sharedValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			bool result;
			if (flag)
			{
				bool isAllocated = styleValue.resource.IsAllocated;
				if (isAllocated)
				{
					Font x = styleValue.resource.IsAllocated ? (styleValue.resource.Target as Font) : null;
					bool flag2 = x == inlineValue.value && styleValue.keyword == inlineValue.keyword;
					if (flag2)
					{
						result = false;
						return result;
					}
					bool isAllocated2 = styleValue.resource.IsAllocated;
					if (isAllocated2)
					{
						styleValue.resource.Free();
					}
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					result = false;
					return result;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.resource = ((inlineValue.value != null) ? GCHandle.Alloc(inlineValue.value) : default(GCHandle));
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			if (flag4)
			{
				styleValue.keyword = sharedValue.keyword;
				styleValue.resource = ((sharedValue.value != null) ? GCHandle.Alloc(sharedValue.value) : default(GCHandle));
			}
			this.ApplyStyleValue(styleValue);
			result = true;
			return result;
		}

		private bool SetInlineCursor(StyleCursor inlineValue, StyleCursor sharedValue)
		{
			StyleCursor styleCursor = default(StyleCursor);
			bool flag = this.TryGetInlineCursor(ref styleCursor);
			bool result;
			if (flag)
			{
				bool flag2 = styleCursor.value == inlineValue.value && styleCursor.keyword == inlineValue.keyword;
				if (flag2)
				{
					result = false;
					return result;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					result = false;
					return result;
				}
			}
			styleCursor.value = inlineValue.value;
			styleCursor.keyword = inlineValue.keyword;
			this.SetInlineCursor(styleCursor);
			bool flag4 = styleCursor.keyword == StyleKeyword.Null;
			if (flag4)
			{
				styleCursor.keyword = sharedValue.keyword;
				styleCursor.value = sharedValue.value;
			}
			this.ve.computedStyle.ApplyStyleCursor(styleCursor);
			result = true;
			return result;
		}

		private void ApplyStyleValue(StyleValue value)
		{
			VisualElement expr_14 = this.ve.hierarchy.parent;
			ComputedStyle parentStyle = (expr_14 != null) ? expr_14.computedStyle : null;
			this.ve.computedStyle.ApplyStyleValue(value, parentStyle);
		}

		public bool TryGetInlineCursor(ref StyleCursor value)
		{
			bool hasInlineCursor = this.m_HasInlineCursor;
			bool result;
			if (hasInlineCursor)
			{
				value = this.m_InlineCursor;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public void SetInlineCursor(StyleCursor value)
		{
			this.m_InlineCursor = value;
			this.m_HasInlineCursor = true;
		}
	}
}
