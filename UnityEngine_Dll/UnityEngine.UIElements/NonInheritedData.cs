using System;

namespace UnityEngine.UIElements
{
	internal struct NonInheritedData : IEquatable<NonInheritedData>
	{
		public StyleEnum<Align> alignContent;

		public StyleEnum<Align> alignItems;

		public StyleEnum<Align> alignSelf;

		public StyleColor backgroundColor;

		public StyleBackground backgroundImage;

		public StyleColor borderBottomColor;

		public StyleLength borderBottomLeftRadius;

		public StyleLength borderBottomRightRadius;

		public StyleFloat borderBottomWidth;

		public StyleColor borderLeftColor;

		public StyleFloat borderLeftWidth;

		public StyleColor borderRightColor;

		public StyleFloat borderRightWidth;

		public StyleColor borderTopColor;

		public StyleLength borderTopLeftRadius;

		public StyleLength borderTopRightRadius;

		public StyleFloat borderTopWidth;

		public StyleLength bottom;

		public StyleCursor cursor;

		public StyleEnum<DisplayStyle> display;

		public StyleLength flexBasis;

		public StyleEnum<FlexDirection> flexDirection;

		public StyleFloat flexGrow;

		public StyleFloat flexShrink;

		public StyleEnum<Wrap> flexWrap;

		public StyleLength height;

		public StyleEnum<Justify> justifyContent;

		public StyleLength left;

		public StyleLength marginBottom;

		public StyleLength marginLeft;

		public StyleLength marginRight;

		public StyleLength marginTop;

		public StyleLength maxHeight;

		public StyleLength maxWidth;

		public StyleLength minHeight;

		public StyleLength minWidth;

		public StyleFloat opacity;

		public StyleEnum<OverflowInternal> overflow;

		public StyleLength paddingBottom;

		public StyleLength paddingLeft;

		public StyleLength paddingRight;

		public StyleLength paddingTop;

		public StyleEnum<Position> position;

		public StyleLength right;

		public StyleEnum<TextOverflow> textOverflow;

		public StyleLength top;

		public StyleColor unityBackgroundImageTintColor;

		public StyleEnum<ScaleMode> unityBackgroundScaleMode;

		public StyleEnum<OverflowClipBox> unityOverflowClipBox;

		public StyleInt unitySliceBottom;

		public StyleInt unitySliceLeft;

		public StyleInt unitySliceRight;

		public StyleInt unitySliceTop;

		public StyleEnum<TextOverflowPosition> unityTextOverflowPosition;

		public StyleLength width;

		public static bool operator ==(NonInheritedData lhs, NonInheritedData rhs)
		{
			return lhs.alignContent.value == rhs.alignContent.value && lhs.alignContent.keyword == rhs.alignContent.keyword && lhs.alignItems.value == rhs.alignItems.value && lhs.alignItems.keyword == rhs.alignItems.keyword && lhs.alignSelf.value == rhs.alignSelf.value && lhs.alignSelf.keyword == rhs.alignSelf.keyword && lhs.backgroundColor == rhs.backgroundColor && lhs.backgroundImage == rhs.backgroundImage && lhs.borderBottomColor == rhs.borderBottomColor && lhs.borderBottomLeftRadius == rhs.borderBottomLeftRadius && lhs.borderBottomRightRadius == rhs.borderBottomRightRadius && lhs.borderBottomWidth == rhs.borderBottomWidth && lhs.borderLeftColor == rhs.borderLeftColor && lhs.borderLeftWidth == rhs.borderLeftWidth && lhs.borderRightColor == rhs.borderRightColor && lhs.borderRightWidth == rhs.borderRightWidth && lhs.borderTopColor == rhs.borderTopColor && lhs.borderTopLeftRadius == rhs.borderTopLeftRadius && lhs.borderTopRightRadius == rhs.borderTopRightRadius && lhs.borderTopWidth == rhs.borderTopWidth && lhs.bottom == rhs.bottom && lhs.cursor == rhs.cursor && lhs.display.value == rhs.display.value && lhs.display.keyword == rhs.display.keyword && lhs.flexBasis == rhs.flexBasis && lhs.flexDirection.value == rhs.flexDirection.value && lhs.flexDirection.keyword == rhs.flexDirection.keyword && lhs.flexGrow == rhs.flexGrow && lhs.flexShrink == rhs.flexShrink && lhs.flexWrap.value == rhs.flexWrap.value && lhs.flexWrap.keyword == rhs.flexWrap.keyword && lhs.height == rhs.height && lhs.justifyContent.value == rhs.justifyContent.value && lhs.justifyContent.keyword == rhs.justifyContent.keyword && lhs.left == rhs.left && lhs.marginBottom == rhs.marginBottom && lhs.marginLeft == rhs.marginLeft && lhs.marginRight == rhs.marginRight && lhs.marginTop == rhs.marginTop && lhs.maxHeight == rhs.maxHeight && lhs.maxWidth == rhs.maxWidth && lhs.minHeight == rhs.minHeight && lhs.minWidth == rhs.minWidth && lhs.opacity == rhs.opacity && lhs.overflow.value == rhs.overflow.value && lhs.overflow.keyword == rhs.overflow.keyword && lhs.paddingBottom == rhs.paddingBottom && lhs.paddingLeft == rhs.paddingLeft && lhs.paddingRight == rhs.paddingRight && lhs.paddingTop == rhs.paddingTop && lhs.position.value == rhs.position.value && lhs.position.keyword == rhs.position.keyword && lhs.right == rhs.right && lhs.textOverflow.value == rhs.textOverflow.value && lhs.textOverflow.keyword == rhs.textOverflow.keyword && lhs.top == rhs.top && lhs.unityBackgroundImageTintColor == rhs.unityBackgroundImageTintColor && lhs.unityBackgroundScaleMode.value == rhs.unityBackgroundScaleMode.value && lhs.unityBackgroundScaleMode.keyword == rhs.unityBackgroundScaleMode.keyword && lhs.unityOverflowClipBox.value == rhs.unityOverflowClipBox.value && lhs.unityOverflowClipBox.keyword == rhs.unityOverflowClipBox.keyword && lhs.unitySliceBottom == rhs.unitySliceBottom && lhs.unitySliceLeft == rhs.unitySliceLeft && lhs.unitySliceRight == rhs.unitySliceRight && lhs.unitySliceTop == rhs.unitySliceTop && lhs.unityTextOverflowPosition.value == rhs.unityTextOverflowPosition.value && lhs.unityTextOverflowPosition.keyword == rhs.unityTextOverflowPosition.keyword && lhs.width == rhs.width;
		}

		public static bool operator !=(NonInheritedData lhs, NonInheritedData rhs)
		{
			return !(lhs == rhs);
		}

		public bool Equals(NonInheritedData other)
		{
			return other == this;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is NonInheritedData && this.Equals((NonInheritedData)obj);
		}

		public override int GetHashCode()
		{
			int num = this.alignContent.GetHashCode();
			num = (num * 397 ^ this.alignItems.GetHashCode());
			num = (num * 397 ^ this.alignSelf.GetHashCode());
			num = (num * 397 ^ this.backgroundColor.GetHashCode());
			num = (num * 397 ^ this.backgroundImage.GetHashCode());
			num = (num * 397 ^ this.borderBottomColor.GetHashCode());
			num = (num * 397 ^ this.borderBottomLeftRadius.GetHashCode());
			num = (num * 397 ^ this.borderBottomRightRadius.GetHashCode());
			num = (num * 397 ^ this.borderBottomWidth.GetHashCode());
			num = (num * 397 ^ this.borderLeftColor.GetHashCode());
			num = (num * 397 ^ this.borderLeftWidth.GetHashCode());
			num = (num * 397 ^ this.borderRightColor.GetHashCode());
			num = (num * 397 ^ this.borderRightWidth.GetHashCode());
			num = (num * 397 ^ this.borderTopColor.GetHashCode());
			num = (num * 397 ^ this.borderTopLeftRadius.GetHashCode());
			num = (num * 397 ^ this.borderTopRightRadius.GetHashCode());
			num = (num * 397 ^ this.borderTopWidth.GetHashCode());
			num = (num * 397 ^ this.bottom.GetHashCode());
			num = (num * 397 ^ this.cursor.GetHashCode());
			num = (num * 397 ^ this.display.GetHashCode());
			num = (num * 397 ^ this.flexBasis.GetHashCode());
			num = (num * 397 ^ this.flexDirection.GetHashCode());
			num = (num * 397 ^ this.flexGrow.GetHashCode());
			num = (num * 397 ^ this.flexShrink.GetHashCode());
			num = (num * 397 ^ this.flexWrap.GetHashCode());
			num = (num * 397 ^ this.height.GetHashCode());
			num = (num * 397 ^ this.justifyContent.GetHashCode());
			num = (num * 397 ^ this.left.GetHashCode());
			num = (num * 397 ^ this.marginBottom.GetHashCode());
			num = (num * 397 ^ this.marginLeft.GetHashCode());
			num = (num * 397 ^ this.marginRight.GetHashCode());
			num = (num * 397 ^ this.marginTop.GetHashCode());
			num = (num * 397 ^ this.maxHeight.GetHashCode());
			num = (num * 397 ^ this.maxWidth.GetHashCode());
			num = (num * 397 ^ this.minHeight.GetHashCode());
			num = (num * 397 ^ this.minWidth.GetHashCode());
			num = (num * 397 ^ this.opacity.GetHashCode());
			num = (num * 397 ^ this.overflow.GetHashCode());
			num = (num * 397 ^ this.paddingBottom.GetHashCode());
			num = (num * 397 ^ this.paddingLeft.GetHashCode());
			num = (num * 397 ^ this.paddingRight.GetHashCode());
			num = (num * 397 ^ this.paddingTop.GetHashCode());
			num = (num * 397 ^ this.position.GetHashCode());
			num = (num * 397 ^ this.right.GetHashCode());
			num = (num * 397 ^ this.textOverflow.GetHashCode());
			num = (num * 397 ^ this.top.GetHashCode());
			num = (num * 397 ^ this.unityBackgroundImageTintColor.GetHashCode());
			num = (num * 397 ^ this.unityBackgroundScaleMode.GetHashCode());
			num = (num * 397 ^ this.unityOverflowClipBox.GetHashCode());
			num = (num * 397 ^ this.unitySliceBottom.GetHashCode());
			num = (num * 397 ^ this.unitySliceLeft.GetHashCode());
			num = (num * 397 ^ this.unitySliceRight.GetHashCode());
			num = (num * 397 ^ this.unitySliceTop.GetHashCode());
			num = (num * 397 ^ this.unityTextOverflowPosition.GetHashCode());
			return num * 397 ^ this.width.GetHashCode();
		}
	}
}
