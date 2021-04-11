using System;

namespace UnityEngine.UIElements.StyleSheets
{
	internal static class InitialStyle
	{
		private static ComputedStyle s_InitialStyle;

		public static StyleEnum<Align> alignContent
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.alignContent;
			}
		}

		public static StyleEnum<Align> alignItems
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.alignItems;
			}
		}

		public static StyleEnum<Align> alignSelf
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.alignSelf;
			}
		}

		public static StyleColor backgroundColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.backgroundColor;
			}
		}

		public static StyleBackground backgroundImage
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.backgroundImage;
			}
		}

		public static StyleColor borderBottomColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderBottomColor;
			}
		}

		public static StyleLength borderBottomLeftRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderBottomLeftRadius;
			}
		}

		public static StyleLength borderBottomRightRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderBottomRightRadius;
			}
		}

		public static StyleFloat borderBottomWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderBottomWidth;
			}
		}

		public static StyleColor borderLeftColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderLeftColor;
			}
		}

		public static StyleFloat borderLeftWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderLeftWidth;
			}
		}

		public static StyleColor borderRightColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderRightColor;
			}
		}

		public static StyleFloat borderRightWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderRightWidth;
			}
		}

		public static StyleColor borderTopColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderTopColor;
			}
		}

		public static StyleLength borderTopLeftRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderTopLeftRadius;
			}
		}

		public static StyleLength borderTopRightRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderTopRightRadius;
			}
		}

		public static StyleFloat borderTopWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderTopWidth;
			}
		}

		public static StyleLength bottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.bottom;
			}
		}

		public static StyleColor color
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.color;
			}
		}

		public static StyleCursor cursor
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.cursor;
			}
		}

		public static StyleEnum<DisplayStyle> display
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.display;
			}
		}

		public static StyleLength flexBasis
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.flexBasis;
			}
		}

		public static StyleEnum<FlexDirection> flexDirection
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.flexDirection;
			}
		}

		public static StyleFloat flexGrow
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.flexGrow;
			}
		}

		public static StyleFloat flexShrink
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.flexShrink;
			}
		}

		public static StyleEnum<Wrap> flexWrap
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.flexWrap;
			}
		}

		public static StyleLength fontSize
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.fontSize;
			}
		}

		public static StyleLength height
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.height;
			}
		}

		public static StyleEnum<Justify> justifyContent
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.justifyContent;
			}
		}

		public static StyleLength left
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.left;
			}
		}

		public static StyleLength marginBottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.marginBottom;
			}
		}

		public static StyleLength marginLeft
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.marginLeft;
			}
		}

		public static StyleLength marginRight
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.marginRight;
			}
		}

		public static StyleLength marginTop
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.marginTop;
			}
		}

		public static StyleLength maxHeight
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.maxHeight;
			}
		}

		public static StyleLength maxWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.maxWidth;
			}
		}

		public static StyleLength minHeight
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.minHeight;
			}
		}

		public static StyleLength minWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.minWidth;
			}
		}

		public static StyleFloat opacity
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.opacity;
			}
		}

		public static StyleEnum<OverflowInternal> overflow
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.overflow;
			}
		}

		public static StyleLength paddingBottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.paddingBottom;
			}
		}

		public static StyleLength paddingLeft
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.paddingLeft;
			}
		}

		public static StyleLength paddingRight
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.paddingRight;
			}
		}

		public static StyleLength paddingTop
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.paddingTop;
			}
		}

		public static StyleEnum<Position> position
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.position;
			}
		}

		public static StyleLength right
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.right;
			}
		}

		public static StyleEnum<TextOverflow> textOverflow
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.textOverflow;
			}
		}

		public static StyleLength top
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.top;
			}
		}

		public static StyleColor unityBackgroundImageTintColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unityBackgroundImageTintColor;
			}
		}

		public static StyleEnum<ScaleMode> unityBackgroundScaleMode
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unityBackgroundScaleMode;
			}
		}

		public static StyleFont unityFont
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.unityFont;
			}
		}

		public static StyleEnum<FontStyle> unityFontStyleAndWeight
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.unityFontStyleAndWeight;
			}
		}

		public static StyleEnum<OverflowClipBox> unityOverflowClipBox
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unityOverflowClipBox;
			}
		}

		public static StyleInt unitySliceBottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unitySliceBottom;
			}
		}

		public static StyleInt unitySliceLeft
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unitySliceLeft;
			}
		}

		public static StyleInt unitySliceRight
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unitySliceRight;
			}
		}

		public static StyleInt unitySliceTop
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unitySliceTop;
			}
		}

		public static StyleEnum<TextAnchor> unityTextAlign
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.unityTextAlign;
			}
		}

		public static StyleEnum<TextOverflowPosition> unityTextOverflowPosition
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unityTextOverflowPosition;
			}
		}

		public static StyleEnum<Visibility> visibility
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.visibility;
			}
		}

		public static StyleEnum<WhiteSpace> whiteSpace
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.whiteSpace;
			}
		}

		public static StyleLength width
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.width;
			}
		}

		public static ComputedStyle Get()
		{
			return InitialStyle.s_InitialStyle;
		}

		static InitialStyle()
		{
			InitialStyle.s_InitialStyle = ComputedStyle.CreateUninitialized(true);
			InitialStyle.s_InitialStyle.nonInheritedData.alignContent = Align.FlexStart;
			InitialStyle.s_InitialStyle.nonInheritedData.alignItems = Align.Stretch;
			InitialStyle.s_InitialStyle.nonInheritedData.alignSelf = Align.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.backgroundColor = Color.clear;
			InitialStyle.s_InitialStyle.nonInheritedData.backgroundImage = default(StyleBackground);
			InitialStyle.s_InitialStyle.nonInheritedData.borderBottomColor = Color.clear;
			InitialStyle.s_InitialStyle.nonInheritedData.borderBottomLeftRadius = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.borderBottomRightRadius = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.borderBottomWidth = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.borderLeftColor = Color.clear;
			InitialStyle.s_InitialStyle.nonInheritedData.borderLeftWidth = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.borderRightColor = Color.clear;
			InitialStyle.s_InitialStyle.nonInheritedData.borderRightWidth = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.borderTopColor = Color.clear;
			InitialStyle.s_InitialStyle.nonInheritedData.borderTopLeftRadius = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.borderTopRightRadius = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.borderTopWidth = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.bottom = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.inheritedData.color = Color.black;
			InitialStyle.s_InitialStyle.nonInheritedData.cursor = default(StyleCursor);
			InitialStyle.s_InitialStyle.nonInheritedData.display = DisplayStyle.Flex;
			InitialStyle.s_InitialStyle.nonInheritedData.flexBasis = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.flexDirection = FlexDirection.Column;
			InitialStyle.s_InitialStyle.nonInheritedData.flexGrow = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.flexShrink = 1f;
			InitialStyle.s_InitialStyle.nonInheritedData.flexWrap = Wrap.NoWrap;
			InitialStyle.s_InitialStyle.inheritedData.fontSize = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.height = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.justifyContent = Justify.FlexStart;
			InitialStyle.s_InitialStyle.nonInheritedData.left = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.marginBottom = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.marginLeft = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.marginRight = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.marginTop = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.maxHeight = StyleKeyword.None;
			InitialStyle.s_InitialStyle.nonInheritedData.maxWidth = StyleKeyword.None;
			InitialStyle.s_InitialStyle.nonInheritedData.minHeight = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.minWidth = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.opacity = 1f;
			InitialStyle.s_InitialStyle.nonInheritedData.overflow = OverflowInternal.Visible;
			InitialStyle.s_InitialStyle.nonInheritedData.paddingBottom = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.paddingLeft = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.paddingRight = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.paddingTop = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.position = Position.Relative;
			InitialStyle.s_InitialStyle.nonInheritedData.right = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.textOverflow = TextOverflow.Clip;
			InitialStyle.s_InitialStyle.nonInheritedData.top = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.unityBackgroundImageTintColor = Color.white;
			InitialStyle.s_InitialStyle.nonInheritedData.unityBackgroundScaleMode = ScaleMode.StretchToFill;
			InitialStyle.s_InitialStyle.inheritedData.unityFont = default(StyleFont);
			InitialStyle.s_InitialStyle.inheritedData.unityFontStyleAndWeight = FontStyle.Normal;
			InitialStyle.s_InitialStyle.nonInheritedData.unityOverflowClipBox = OverflowClipBox.PaddingBox;
			InitialStyle.s_InitialStyle.nonInheritedData.unitySliceBottom = 0;
			InitialStyle.s_InitialStyle.nonInheritedData.unitySliceLeft = 0;
			InitialStyle.s_InitialStyle.nonInheritedData.unitySliceRight = 0;
			InitialStyle.s_InitialStyle.nonInheritedData.unitySliceTop = 0;
			InitialStyle.s_InitialStyle.inheritedData.unityTextAlign = TextAnchor.UpperLeft;
			InitialStyle.s_InitialStyle.nonInheritedData.unityTextOverflowPosition = TextOverflowPosition.End;
			InitialStyle.s_InitialStyle.inheritedData.visibility = Visibility.Visible;
			InitialStyle.s_InitialStyle.inheritedData.whiteSpace = WhiteSpace.Normal;
			InitialStyle.s_InitialStyle.nonInheritedData.width = StyleKeyword.Auto;
		}
	}
}
