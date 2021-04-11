using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	internal class ComputedStyle : ICustomStyle
	{
		internal readonly bool isShared;

		internal YogaNode yogaNode;

		internal Dictionary<string, StylePropertyValue> m_CustomProperties;

		private float dpiScaling = 1f;

		public InheritedData inheritedData = default(InheritedData);

		public NonInheritedData nonInheritedData = default(NonInheritedData);

		public int customPropertiesCount
		{
			get
			{
				return (this.m_CustomProperties != null) ? this.m_CustomProperties.Count : 0;
			}
		}

		public StyleEnum<Align> alignContent
		{
			get
			{
				return this.nonInheritedData.alignContent;
			}
		}

		public StyleEnum<Align> alignItems
		{
			get
			{
				return this.nonInheritedData.alignItems;
			}
		}

		public StyleEnum<Align> alignSelf
		{
			get
			{
				return this.nonInheritedData.alignSelf;
			}
		}

		public StyleColor backgroundColor
		{
			get
			{
				return this.nonInheritedData.backgroundColor;
			}
		}

		public StyleBackground backgroundImage
		{
			get
			{
				return this.nonInheritedData.backgroundImage;
			}
		}

		public StyleColor borderBottomColor
		{
			get
			{
				return this.nonInheritedData.borderBottomColor;
			}
		}

		public StyleLength borderBottomLeftRadius
		{
			get
			{
				return this.nonInheritedData.borderBottomLeftRadius;
			}
		}

		public StyleLength borderBottomRightRadius
		{
			get
			{
				return this.nonInheritedData.borderBottomRightRadius;
			}
		}

		public StyleFloat borderBottomWidth
		{
			get
			{
				return this.nonInheritedData.borderBottomWidth;
			}
		}

		public StyleColor borderLeftColor
		{
			get
			{
				return this.nonInheritedData.borderLeftColor;
			}
		}

		public StyleFloat borderLeftWidth
		{
			get
			{
				return this.nonInheritedData.borderLeftWidth;
			}
		}

		public StyleColor borderRightColor
		{
			get
			{
				return this.nonInheritedData.borderRightColor;
			}
		}

		public StyleFloat borderRightWidth
		{
			get
			{
				return this.nonInheritedData.borderRightWidth;
			}
		}

		public StyleColor borderTopColor
		{
			get
			{
				return this.nonInheritedData.borderTopColor;
			}
		}

		public StyleLength borderTopLeftRadius
		{
			get
			{
				return this.nonInheritedData.borderTopLeftRadius;
			}
		}

		public StyleLength borderTopRightRadius
		{
			get
			{
				return this.nonInheritedData.borderTopRightRadius;
			}
		}

		public StyleFloat borderTopWidth
		{
			get
			{
				return this.nonInheritedData.borderTopWidth;
			}
		}

		public StyleLength bottom
		{
			get
			{
				return this.nonInheritedData.bottom;
			}
		}

		public StyleColor color
		{
			get
			{
				return this.inheritedData.color;
			}
		}

		public StyleCursor cursor
		{
			get
			{
				return this.nonInheritedData.cursor;
			}
		}

		public StyleEnum<DisplayStyle> display
		{
			get
			{
				return this.nonInheritedData.display;
			}
		}

		public StyleLength flexBasis
		{
			get
			{
				return this.nonInheritedData.flexBasis;
			}
		}

		public StyleEnum<FlexDirection> flexDirection
		{
			get
			{
				return this.nonInheritedData.flexDirection;
			}
		}

		public StyleFloat flexGrow
		{
			get
			{
				return this.nonInheritedData.flexGrow;
			}
		}

		public StyleFloat flexShrink
		{
			get
			{
				return this.nonInheritedData.flexShrink;
			}
		}

		public StyleEnum<Wrap> flexWrap
		{
			get
			{
				return this.nonInheritedData.flexWrap;
			}
		}

		public StyleLength fontSize
		{
			get
			{
				return this.inheritedData.fontSize;
			}
		}

		public StyleLength height
		{
			get
			{
				return this.nonInheritedData.height;
			}
		}

		public StyleEnum<Justify> justifyContent
		{
			get
			{
				return this.nonInheritedData.justifyContent;
			}
		}

		public StyleLength left
		{
			get
			{
				return this.nonInheritedData.left;
			}
		}

		public StyleLength marginBottom
		{
			get
			{
				return this.nonInheritedData.marginBottom;
			}
		}

		public StyleLength marginLeft
		{
			get
			{
				return this.nonInheritedData.marginLeft;
			}
		}

		public StyleLength marginRight
		{
			get
			{
				return this.nonInheritedData.marginRight;
			}
		}

		public StyleLength marginTop
		{
			get
			{
				return this.nonInheritedData.marginTop;
			}
		}

		public StyleLength maxHeight
		{
			get
			{
				return this.nonInheritedData.maxHeight;
			}
		}

		public StyleLength maxWidth
		{
			get
			{
				return this.nonInheritedData.maxWidth;
			}
		}

		public StyleLength minHeight
		{
			get
			{
				return this.nonInheritedData.minHeight;
			}
		}

		public StyleLength minWidth
		{
			get
			{
				return this.nonInheritedData.minWidth;
			}
		}

		public StyleFloat opacity
		{
			get
			{
				return this.nonInheritedData.opacity;
			}
		}

		public StyleEnum<OverflowInternal> overflow
		{
			get
			{
				return this.nonInheritedData.overflow;
			}
		}

		public StyleLength paddingBottom
		{
			get
			{
				return this.nonInheritedData.paddingBottom;
			}
		}

		public StyleLength paddingLeft
		{
			get
			{
				return this.nonInheritedData.paddingLeft;
			}
		}

		public StyleLength paddingRight
		{
			get
			{
				return this.nonInheritedData.paddingRight;
			}
		}

		public StyleLength paddingTop
		{
			get
			{
				return this.nonInheritedData.paddingTop;
			}
		}

		public StyleEnum<Position> position
		{
			get
			{
				return this.nonInheritedData.position;
			}
		}

		public StyleLength right
		{
			get
			{
				return this.nonInheritedData.right;
			}
		}

		public StyleEnum<TextOverflow> textOverflow
		{
			get
			{
				return this.nonInheritedData.textOverflow;
			}
		}

		public StyleLength top
		{
			get
			{
				return this.nonInheritedData.top;
			}
		}

		public StyleColor unityBackgroundImageTintColor
		{
			get
			{
				return this.nonInheritedData.unityBackgroundImageTintColor;
			}
		}

		public StyleEnum<ScaleMode> unityBackgroundScaleMode
		{
			get
			{
				return this.nonInheritedData.unityBackgroundScaleMode;
			}
		}

		public StyleFont unityFont
		{
			get
			{
				return this.inheritedData.unityFont;
			}
		}

		public StyleEnum<FontStyle> unityFontStyleAndWeight
		{
			get
			{
				return this.inheritedData.unityFontStyleAndWeight;
			}
		}

		public StyleEnum<OverflowClipBox> unityOverflowClipBox
		{
			get
			{
				return this.nonInheritedData.unityOverflowClipBox;
			}
		}

		public StyleInt unitySliceBottom
		{
			get
			{
				return this.nonInheritedData.unitySliceBottom;
			}
		}

		public StyleInt unitySliceLeft
		{
			get
			{
				return this.nonInheritedData.unitySliceLeft;
			}
		}

		public StyleInt unitySliceRight
		{
			get
			{
				return this.nonInheritedData.unitySliceRight;
			}
		}

		public StyleInt unitySliceTop
		{
			get
			{
				return this.nonInheritedData.unitySliceTop;
			}
		}

		public StyleEnum<TextAnchor> unityTextAlign
		{
			get
			{
				return this.inheritedData.unityTextAlign;
			}
		}

		public StyleEnum<TextOverflowPosition> unityTextOverflowPosition
		{
			get
			{
				return this.nonInheritedData.unityTextOverflowPosition;
			}
		}

		public StyleEnum<Visibility> visibility
		{
			get
			{
				return this.inheritedData.visibility;
			}
		}

		public StyleEnum<WhiteSpace> whiteSpace
		{
			get
			{
				return this.inheritedData.whiteSpace;
			}
		}

		public StyleLength width
		{
			get
			{
				return this.nonInheritedData.width;
			}
		}

		public static ComputedStyle Create(bool isShared = true)
		{
			ComputedStyle computedStyle = new ComputedStyle(isShared);
			computedStyle.CopyFrom(InitialStyle.Get());
			return computedStyle;
		}

		public static ComputedStyle Create(ComputedStyle parentStyle, bool isShared = true)
		{
			ComputedStyle computedStyle = ComputedStyle.Create(isShared);
			bool flag = parentStyle != null;
			if (flag)
			{
				computedStyle.inheritedData = parentStyle.inheritedData;
			}
			return computedStyle;
		}

		public static ComputedStyle CreateUninitialized(bool isShared = true)
		{
			return new ComputedStyle(isShared);
		}

		private ComputedStyle(bool isShared)
		{
			this.isShared = isShared;
		}

		public void CopyShared(ComputedStyle sharedStyle)
		{
			this.m_CustomProperties = sharedStyle.m_CustomProperties;
			this.CopyFrom(sharedStyle);
		}

		public void FinalizeApply(ComputedStyle parentStyle)
		{
			bool flag = this.yogaNode == null;
			if (flag)
			{
				this.yogaNode = new YogaNode(null);
			}
			bool flag2 = parentStyle != null;
			if (flag2)
			{
				bool flag3 = this.fontSize.value.unit == LengthUnit.Percent;
				if (flag3)
				{
					float value = parentStyle.fontSize.value.value;
					float value2 = value * this.fontSize.value.value / 100f;
					this.inheritedData.fontSize = new Length(value2);
				}
			}
			this.SyncWithLayout(this.yogaNode);
		}

		public void SyncWithLayout(YogaNode targetNode)
		{
			targetNode.Flex = float.NaN;
			targetNode.FlexGrow = this.flexGrow.value;
			targetNode.FlexShrink = this.flexShrink.value;
			targetNode.FlexBasis = this.flexBasis.ToYogaValue();
			targetNode.Left = this.left.ToYogaValue();
			targetNode.Top = this.top.ToYogaValue();
			targetNode.Right = this.right.ToYogaValue();
			targetNode.Bottom = this.bottom.ToYogaValue();
			targetNode.MarginLeft = this.marginLeft.ToYogaValue();
			targetNode.MarginTop = this.marginTop.ToYogaValue();
			targetNode.MarginRight = this.marginRight.ToYogaValue();
			targetNode.MarginBottom = this.marginBottom.ToYogaValue();
			targetNode.PaddingLeft = this.paddingLeft.ToYogaValue();
			targetNode.PaddingTop = this.paddingTop.ToYogaValue();
			targetNode.PaddingRight = this.paddingRight.ToYogaValue();
			targetNode.PaddingBottom = this.paddingBottom.ToYogaValue();
			targetNode.BorderLeftWidth = this.borderLeftWidth.value;
			targetNode.BorderTopWidth = this.borderTopWidth.value;
			targetNode.BorderRightWidth = this.borderRightWidth.value;
			targetNode.BorderBottomWidth = this.borderBottomWidth.value;
			targetNode.Width = this.width.ToYogaValue();
			targetNode.Height = this.height.ToYogaValue();
			targetNode.PositionType = (YogaPositionType)this.position.value;
			targetNode.Overflow = (YogaOverflow)this.overflow.value;
			targetNode.AlignSelf = (YogaAlign)this.alignSelf.value;
			targetNode.MaxWidth = this.maxWidth.ToYogaValue();
			targetNode.MaxHeight = this.maxHeight.ToYogaValue();
			targetNode.MinWidth = this.minWidth.ToYogaValue();
			targetNode.MinHeight = this.minHeight.ToYogaValue();
			targetNode.FlexDirection = (YogaFlexDirection)this.flexDirection.value;
			targetNode.AlignContent = (YogaAlign)this.alignContent.value;
			targetNode.AlignItems = (YogaAlign)this.alignItems.value;
			targetNode.JustifyContent = (YogaJustify)this.justifyContent.value;
			targetNode.Wrap = (YogaWrap)this.flexWrap.value;
			targetNode.Display = (YogaDisplay)this.display.value;
		}

		private bool ApplyGlobalKeyword(StylePropertyReader reader, ComputedStyle parentStyle)
		{
			StyleValueHandle handle = reader.GetValue(0).handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			bool result;
			if (flag)
			{
				bool flag2 = handle.valueIndex == 1;
				if (flag2)
				{
					this.ApplyInitialValue(reader);
					result = true;
					return result;
				}
				bool flag3 = handle.valueIndex == 3;
				if (flag3)
				{
					this.ApplyUnsetValue(reader, parentStyle);
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		private bool ApplyGlobalKeyword(StyleValue sv, ComputedStyle parentStyle)
		{
			bool flag = sv.keyword == StyleKeyword.Initial;
			bool result;
			if (flag)
			{
				this.ApplyInitialValue(sv.id);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		private void RemoveCustomStyleProperty(StylePropertyReader reader)
		{
			string name = reader.property.name;
			bool flag = this.m_CustomProperties == null || !this.m_CustomProperties.ContainsKey(name);
			if (!flag)
			{
				this.m_CustomProperties.Remove(name);
			}
		}

		private void ApplyCustomStyleProperty(StylePropertyReader reader)
		{
			this.dpiScaling = reader.dpiScaling;
			bool flag = this.m_CustomProperties == null;
			if (flag)
			{
				this.m_CustomProperties = new Dictionary<string, StylePropertyValue>();
			}
			StyleProperty property = reader.property;
			StylePropertyValue value = reader.GetValue(0);
			this.m_CustomProperties[property.name] = value;
		}

		public bool TryGetValue(CustomStyleProperty<float> property, out float value)
		{
			StylePropertyValue stylePropertyValue;
			bool flag = this.TryGetValue(property.name, StyleValueType.Float, out stylePropertyValue);
			bool result;
			if (flag)
			{
				bool flag2 = stylePropertyValue.sheet.TryReadFloat(stylePropertyValue.handle, out value);
				if (flag2)
				{
					result = true;
					return result;
				}
			}
			value = 0f;
			result = false;
			return result;
		}

		public bool TryGetValue(CustomStyleProperty<int> property, out int value)
		{
			StylePropertyValue stylePropertyValue;
			bool flag = this.TryGetValue(property.name, StyleValueType.Float, out stylePropertyValue);
			bool result;
			if (flag)
			{
				float num = 0f;
				bool flag2 = stylePropertyValue.sheet.TryReadFloat(stylePropertyValue.handle, out num);
				if (flag2)
				{
					value = (int)num;
					result = true;
					return result;
				}
			}
			value = 0;
			result = false;
			return result;
		}

		public bool TryGetValue(CustomStyleProperty<bool> property, out bool value)
		{
			StylePropertyValue stylePropertyValue;
			bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out stylePropertyValue);
			bool result;
			if (flag)
			{
				value = (stylePropertyValue.sheet.ReadKeyword(stylePropertyValue.handle) == StyleValueKeyword.True);
				result = true;
			}
			else
			{
				value = false;
				result = false;
			}
			return result;
		}

		public bool TryGetValue(CustomStyleProperty<Color> property, out Color value)
		{
			StylePropertyValue stylePropertyValue;
			bool flag = this.TryGetValue(property.name, StyleValueType.Color, out stylePropertyValue);
			bool result;
			if (flag)
			{
				bool flag2 = stylePropertyValue.sheet.TryReadColor(stylePropertyValue.handle, out value);
				if (flag2)
				{
					result = true;
					return result;
				}
			}
			value = Color.clear;
			result = false;
			return result;
		}

		public bool TryGetValue(CustomStyleProperty<Texture2D> property, out Texture2D value)
		{
			StylePropertyValue propertyValue;
			bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out propertyValue);
			bool result;
			if (flag)
			{
				ImageSource imageSource = default(ImageSource);
				bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(propertyValue, this.dpiScaling, out imageSource) && imageSource.texture != null;
				if (flag2)
				{
					value = imageSource.texture;
					result = true;
					return result;
				}
			}
			value = null;
			result = false;
			return result;
		}

		public bool TryGetValue(CustomStyleProperty<VectorImage> property, out VectorImage value)
		{
			StylePropertyValue propertyValue;
			bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out propertyValue);
			bool result;
			if (flag)
			{
				ImageSource imageSource = default(ImageSource);
				bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(propertyValue, this.dpiScaling, out imageSource) && imageSource.vectorImage != null;
				if (flag2)
				{
					value = imageSource.vectorImage;
					result = true;
					return result;
				}
			}
			value = null;
			result = false;
			return result;
		}

		public bool TryGetValue(CustomStyleProperty<string> property, out string value)
		{
			StylePropertyValue stylePropertyValue;
			bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out stylePropertyValue);
			bool result;
			if (flag)
			{
				value = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
				result = true;
			}
			else
			{
				value = string.Empty;
				result = false;
			}
			return result;
		}

		private bool TryGetValue(string propertyName, StyleValueType valueType, out StylePropertyValue customProp)
		{
			customProp = default(StylePropertyValue);
			bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(propertyName, out customProp);
			bool result;
			if (flag)
			{
				StyleValueHandle handle = customProp.handle;
				bool flag2 = handle.valueType != valueType;
				if (flag2)
				{
					Debug.LogWarning(string.Format("Trying to read value as {0} while parsed type is {1}", valueType, handle.valueType));
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		public void CopyFrom(ComputedStyle other)
		{
			this.inheritedData = other.inheritedData;
			this.nonInheritedData = other.nonInheritedData;
		}

		public void ApplyProperties(StylePropertyReader reader, ComputedStyle parentStyle)
		{
			for (StylePropertyId stylePropertyId = reader.propertyId; stylePropertyId != StylePropertyId.Unknown; stylePropertyId = reader.MoveNextProperty())
			{
				bool flag = this.ApplyGlobalKeyword(reader, parentStyle);
				if (!flag)
				{
					StylePropertyId stylePropertyId2 = stylePropertyId;
					StylePropertyId stylePropertyId3 = stylePropertyId2;
					switch (stylePropertyId3)
					{
					case StylePropertyId.Custom:
						this.ApplyCustomStyleProperty(reader);
						break;
					case StylePropertyId.Unknown:
						goto IL_849;
					case StylePropertyId.Color:
						this.inheritedData.color = reader.ReadStyleColor(0);
						break;
					case StylePropertyId.FontSize:
						this.inheritedData.fontSize = reader.ReadStyleLength(0);
						break;
					case StylePropertyId.UnityFont:
						this.inheritedData.unityFont = reader.ReadStyleFont(0);
						break;
					case StylePropertyId.UnityFontStyleAndWeight:
						this.inheritedData.unityFontStyleAndWeight = (FontStyle)reader.ReadStyleEnum(StyleEnumType.FontStyle, 0).value;
						break;
					case StylePropertyId.UnityTextAlign:
						this.inheritedData.unityTextAlign = (TextAnchor)reader.ReadStyleEnum(StyleEnumType.TextAnchor, 0).value;
						break;
					case StylePropertyId.Visibility:
						this.inheritedData.visibility = (Visibility)reader.ReadStyleEnum(StyleEnumType.Visibility, 0).value;
						break;
					case StylePropertyId.WhiteSpace:
						this.inheritedData.whiteSpace = (WhiteSpace)reader.ReadStyleEnum(StyleEnumType.WhiteSpace, 0).value;
						break;
					default:
						switch (stylePropertyId3)
						{
						case StylePropertyId.AlignContent:
							this.nonInheritedData.alignContent = (Align)reader.ReadStyleEnum(StyleEnumType.Align, 0).value;
							break;
						case StylePropertyId.AlignItems:
							this.nonInheritedData.alignItems = (Align)reader.ReadStyleEnum(StyleEnumType.Align, 0).value;
							break;
						case StylePropertyId.AlignSelf:
							this.nonInheritedData.alignSelf = (Align)reader.ReadStyleEnum(StyleEnumType.Align, 0).value;
							break;
						case StylePropertyId.BackgroundColor:
							this.nonInheritedData.backgroundColor = reader.ReadStyleColor(0);
							break;
						case StylePropertyId.BackgroundImage:
							this.nonInheritedData.backgroundImage = reader.ReadStyleBackground(0);
							break;
						case StylePropertyId.BorderBottomColor:
							this.nonInheritedData.borderBottomColor = reader.ReadStyleColor(0);
							break;
						case StylePropertyId.BorderBottomLeftRadius:
							this.nonInheritedData.borderBottomLeftRadius = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.BorderBottomRightRadius:
							this.nonInheritedData.borderBottomRightRadius = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.BorderBottomWidth:
							this.nonInheritedData.borderBottomWidth = reader.ReadStyleFloat(0);
							break;
						case StylePropertyId.BorderLeftColor:
							this.nonInheritedData.borderLeftColor = reader.ReadStyleColor(0);
							break;
						case StylePropertyId.BorderLeftWidth:
							this.nonInheritedData.borderLeftWidth = reader.ReadStyleFloat(0);
							break;
						case StylePropertyId.BorderRightColor:
							this.nonInheritedData.borderRightColor = reader.ReadStyleColor(0);
							break;
						case StylePropertyId.BorderRightWidth:
							this.nonInheritedData.borderRightWidth = reader.ReadStyleFloat(0);
							break;
						case StylePropertyId.BorderTopColor:
							this.nonInheritedData.borderTopColor = reader.ReadStyleColor(0);
							break;
						case StylePropertyId.BorderTopLeftRadius:
							this.nonInheritedData.borderTopLeftRadius = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.BorderTopRightRadius:
							this.nonInheritedData.borderTopRightRadius = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.BorderTopWidth:
							this.nonInheritedData.borderTopWidth = reader.ReadStyleFloat(0);
							break;
						case StylePropertyId.Bottom:
							this.nonInheritedData.bottom = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.Cursor:
							this.nonInheritedData.cursor = reader.ReadStyleCursor(0);
							break;
						case StylePropertyId.Display:
							this.nonInheritedData.display = (DisplayStyle)reader.ReadStyleEnum(StyleEnumType.DisplayStyle, 0).value;
							break;
						case StylePropertyId.FlexBasis:
							this.nonInheritedData.flexBasis = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.FlexDirection:
							this.nonInheritedData.flexDirection = (FlexDirection)reader.ReadStyleEnum(StyleEnumType.FlexDirection, 0).value;
							break;
						case StylePropertyId.FlexGrow:
							this.nonInheritedData.flexGrow = reader.ReadStyleFloat(0);
							break;
						case StylePropertyId.FlexShrink:
							this.nonInheritedData.flexShrink = reader.ReadStyleFloat(0);
							break;
						case StylePropertyId.FlexWrap:
							this.nonInheritedData.flexWrap = (Wrap)reader.ReadStyleEnum(StyleEnumType.Wrap, 0).value;
							break;
						case StylePropertyId.Height:
							this.nonInheritedData.height = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.JustifyContent:
							this.nonInheritedData.justifyContent = (Justify)reader.ReadStyleEnum(StyleEnumType.Justify, 0).value;
							break;
						case StylePropertyId.Left:
							this.nonInheritedData.left = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MarginBottom:
							this.nonInheritedData.marginBottom = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MarginLeft:
							this.nonInheritedData.marginLeft = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MarginRight:
							this.nonInheritedData.marginRight = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MarginTop:
							this.nonInheritedData.marginTop = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MaxHeight:
							this.nonInheritedData.maxHeight = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MaxWidth:
							this.nonInheritedData.maxWidth = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MinHeight:
							this.nonInheritedData.minHeight = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MinWidth:
							this.nonInheritedData.minWidth = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.Opacity:
							this.nonInheritedData.opacity = reader.ReadStyleFloat(0);
							break;
						case StylePropertyId.Overflow:
							this.nonInheritedData.overflow = (OverflowInternal)reader.ReadStyleEnum(StyleEnumType.OverflowInternal, 0).value;
							break;
						case StylePropertyId.PaddingBottom:
							this.nonInheritedData.paddingBottom = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.PaddingLeft:
							this.nonInheritedData.paddingLeft = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.PaddingRight:
							this.nonInheritedData.paddingRight = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.PaddingTop:
							this.nonInheritedData.paddingTop = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.Position:
							this.nonInheritedData.position = (Position)reader.ReadStyleEnum(StyleEnumType.Position, 0).value;
							break;
						case StylePropertyId.Right:
							this.nonInheritedData.right = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.TextOverflow:
							this.nonInheritedData.textOverflow = (TextOverflow)reader.ReadStyleEnum(StyleEnumType.TextOverflow, 0).value;
							break;
						case StylePropertyId.Top:
							this.nonInheritedData.top = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.UnityBackgroundImageTintColor:
							this.nonInheritedData.unityBackgroundImageTintColor = reader.ReadStyleColor(0);
							break;
						case StylePropertyId.UnityBackgroundScaleMode:
							this.nonInheritedData.unityBackgroundScaleMode = (ScaleMode)reader.ReadStyleEnum(StyleEnumType.ScaleMode, 0).value;
							break;
						case StylePropertyId.UnityOverflowClipBox:
							this.nonInheritedData.unityOverflowClipBox = (OverflowClipBox)reader.ReadStyleEnum(StyleEnumType.OverflowClipBox, 0).value;
							break;
						case StylePropertyId.UnitySliceBottom:
							this.nonInheritedData.unitySliceBottom = reader.ReadStyleInt(0);
							break;
						case StylePropertyId.UnitySliceLeft:
							this.nonInheritedData.unitySliceLeft = reader.ReadStyleInt(0);
							break;
						case StylePropertyId.UnitySliceRight:
							this.nonInheritedData.unitySliceRight = reader.ReadStyleInt(0);
							break;
						case StylePropertyId.UnitySliceTop:
							this.nonInheritedData.unitySliceTop = reader.ReadStyleInt(0);
							break;
						case StylePropertyId.UnityTextOverflowPosition:
							this.nonInheritedData.unityTextOverflowPosition = (TextOverflowPosition)reader.ReadStyleEnum(StyleEnumType.TextOverflowPosition, 0).value;
							break;
						case StylePropertyId.Width:
							this.nonInheritedData.width = reader.ReadStyleLength(0);
							break;
						default:
							switch (stylePropertyId3)
							{
							case StylePropertyId.BorderColor:
								ShorthandApplicator.ApplyBorderColor(reader, this);
								break;
							case StylePropertyId.BorderRadius:
								ShorthandApplicator.ApplyBorderRadius(reader, this);
								break;
							case StylePropertyId.BorderWidth:
								ShorthandApplicator.ApplyBorderWidth(reader, this);
								break;
							case StylePropertyId.Flex:
								ShorthandApplicator.ApplyFlex(reader, this);
								break;
							case StylePropertyId.Margin:
								ShorthandApplicator.ApplyMargin(reader, this);
								break;
							case StylePropertyId.Padding:
								ShorthandApplicator.ApplyPadding(reader, this);
								break;
							default:
								goto IL_849;
							}
							break;
						}
						break;
					}
					goto IL_862;
					IL_849:
					Debug.LogAssertion(string.Format("Unknown property id {0}", stylePropertyId));
				}
				IL_862:;
			}
		}

		public void ApplyStyleValue(StyleValue sv, ComputedStyle parentStyle)
		{
			bool flag = this.ApplyGlobalKeyword(sv, parentStyle);
			if (!flag)
			{
				StylePropertyId id = sv.id;
				StylePropertyId stylePropertyId = id;
				switch (stylePropertyId)
				{
				case StylePropertyId.Color:
					this.inheritedData.color = new StyleColor(sv.color, sv.keyword);
					break;
				case StylePropertyId.FontSize:
					this.inheritedData.fontSize = new StyleLength(sv.length, sv.keyword);
					break;
				case StylePropertyId.UnityFont:
					this.inheritedData.unityFont = new StyleFont(sv.resource, sv.keyword);
					break;
				case StylePropertyId.UnityFontStyleAndWeight:
					this.inheritedData.unityFontStyleAndWeight = new StyleEnum<FontStyle>((FontStyle)sv.number, sv.keyword);
					break;
				case StylePropertyId.UnityTextAlign:
					this.inheritedData.unityTextAlign = new StyleEnum<TextAnchor>((TextAnchor)sv.number, sv.keyword);
					break;
				case StylePropertyId.Visibility:
					this.inheritedData.visibility = new StyleEnum<Visibility>((Visibility)sv.number, sv.keyword);
					break;
				case StylePropertyId.WhiteSpace:
					this.inheritedData.whiteSpace = new StyleEnum<WhiteSpace>((WhiteSpace)sv.number, sv.keyword);
					break;
				default:
					switch (stylePropertyId)
					{
					case StylePropertyId.AlignContent:
					{
						this.nonInheritedData.alignContent = new StyleEnum<Align>((Align)sv.number, sv.keyword);
						bool flag2 = sv.keyword == StyleKeyword.Auto;
						if (flag2)
						{
							this.nonInheritedData.alignContent = Align.Auto;
						}
						return;
					}
					case StylePropertyId.AlignItems:
					{
						this.nonInheritedData.alignItems = new StyleEnum<Align>((Align)sv.number, sv.keyword);
						bool flag3 = sv.keyword == StyleKeyword.Auto;
						if (flag3)
						{
							this.nonInheritedData.alignItems = Align.Auto;
						}
						return;
					}
					case StylePropertyId.AlignSelf:
					{
						this.nonInheritedData.alignSelf = new StyleEnum<Align>((Align)sv.number, sv.keyword);
						bool flag4 = sv.keyword == StyleKeyword.Auto;
						if (flag4)
						{
							this.nonInheritedData.alignSelf = Align.Auto;
						}
						return;
					}
					case StylePropertyId.BackgroundColor:
						this.nonInheritedData.backgroundColor = new StyleColor(sv.color, sv.keyword);
						return;
					case StylePropertyId.BackgroundImage:
						this.nonInheritedData.backgroundImage = new StyleBackground(sv.resource, sv.keyword);
						return;
					case StylePropertyId.BorderBottomColor:
						this.nonInheritedData.borderBottomColor = new StyleColor(sv.color, sv.keyword);
						return;
					case StylePropertyId.BorderBottomLeftRadius:
						this.nonInheritedData.borderBottomLeftRadius = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.BorderBottomRightRadius:
						this.nonInheritedData.borderBottomRightRadius = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.BorderBottomWidth:
						this.nonInheritedData.borderBottomWidth = new StyleFloat(sv.number, sv.keyword);
						return;
					case StylePropertyId.BorderLeftColor:
						this.nonInheritedData.borderLeftColor = new StyleColor(sv.color, sv.keyword);
						return;
					case StylePropertyId.BorderLeftWidth:
						this.nonInheritedData.borderLeftWidth = new StyleFloat(sv.number, sv.keyword);
						return;
					case StylePropertyId.BorderRightColor:
						this.nonInheritedData.borderRightColor = new StyleColor(sv.color, sv.keyword);
						return;
					case StylePropertyId.BorderRightWidth:
						this.nonInheritedData.borderRightWidth = new StyleFloat(sv.number, sv.keyword);
						return;
					case StylePropertyId.BorderTopColor:
						this.nonInheritedData.borderTopColor = new StyleColor(sv.color, sv.keyword);
						return;
					case StylePropertyId.BorderTopLeftRadius:
						this.nonInheritedData.borderTopLeftRadius = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.BorderTopRightRadius:
						this.nonInheritedData.borderTopRightRadius = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.BorderTopWidth:
						this.nonInheritedData.borderTopWidth = new StyleFloat(sv.number, sv.keyword);
						return;
					case StylePropertyId.Bottom:
						this.nonInheritedData.bottom = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.Display:
					{
						this.nonInheritedData.display = new StyleEnum<DisplayStyle>((DisplayStyle)sv.number, sv.keyword);
						bool flag5 = sv.keyword == StyleKeyword.None;
						if (flag5)
						{
							this.nonInheritedData.display = DisplayStyle.None;
						}
						return;
					}
					case StylePropertyId.FlexBasis:
						this.nonInheritedData.flexBasis = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.FlexDirection:
						this.nonInheritedData.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)sv.number, sv.keyword);
						return;
					case StylePropertyId.FlexGrow:
						this.nonInheritedData.flexGrow = new StyleFloat(sv.number, sv.keyword);
						return;
					case StylePropertyId.FlexShrink:
						this.nonInheritedData.flexShrink = new StyleFloat(sv.number, sv.keyword);
						return;
					case StylePropertyId.FlexWrap:
						this.nonInheritedData.flexWrap = new StyleEnum<Wrap>((Wrap)sv.number, sv.keyword);
						return;
					case StylePropertyId.Height:
						this.nonInheritedData.height = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.JustifyContent:
						this.nonInheritedData.justifyContent = new StyleEnum<Justify>((Justify)sv.number, sv.keyword);
						return;
					case StylePropertyId.Left:
						this.nonInheritedData.left = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MarginBottom:
						this.nonInheritedData.marginBottom = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MarginLeft:
						this.nonInheritedData.marginLeft = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MarginRight:
						this.nonInheritedData.marginRight = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MarginTop:
						this.nonInheritedData.marginTop = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MaxHeight:
						this.nonInheritedData.maxHeight = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MaxWidth:
						this.nonInheritedData.maxWidth = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MinHeight:
						this.nonInheritedData.minHeight = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MinWidth:
						this.nonInheritedData.minWidth = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.Opacity:
						this.nonInheritedData.opacity = new StyleFloat(sv.number, sv.keyword);
						return;
					case StylePropertyId.Overflow:
						this.nonInheritedData.overflow = new StyleEnum<OverflowInternal>((OverflowInternal)sv.number, sv.keyword);
						return;
					case StylePropertyId.PaddingBottom:
						this.nonInheritedData.paddingBottom = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.PaddingLeft:
						this.nonInheritedData.paddingLeft = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.PaddingRight:
						this.nonInheritedData.paddingRight = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.PaddingTop:
						this.nonInheritedData.paddingTop = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.Position:
						this.nonInheritedData.position = new StyleEnum<Position>((Position)sv.number, sv.keyword);
						return;
					case StylePropertyId.Right:
						this.nonInheritedData.right = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.TextOverflow:
						this.nonInheritedData.textOverflow = new StyleEnum<TextOverflow>((TextOverflow)sv.number, sv.keyword);
						return;
					case StylePropertyId.Top:
						this.nonInheritedData.top = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.UnityBackgroundImageTintColor:
						this.nonInheritedData.unityBackgroundImageTintColor = new StyleColor(sv.color, sv.keyword);
						return;
					case StylePropertyId.UnityBackgroundScaleMode:
						this.nonInheritedData.unityBackgroundScaleMode = new StyleEnum<ScaleMode>((ScaleMode)sv.number, sv.keyword);
						return;
					case StylePropertyId.UnityOverflowClipBox:
						this.nonInheritedData.unityOverflowClipBox = new StyleEnum<OverflowClipBox>((OverflowClipBox)sv.number, sv.keyword);
						return;
					case StylePropertyId.UnitySliceBottom:
						this.nonInheritedData.unitySliceBottom = new StyleInt((int)sv.number, sv.keyword);
						return;
					case StylePropertyId.UnitySliceLeft:
						this.nonInheritedData.unitySliceLeft = new StyleInt((int)sv.number, sv.keyword);
						return;
					case StylePropertyId.UnitySliceRight:
						this.nonInheritedData.unitySliceRight = new StyleInt((int)sv.number, sv.keyword);
						return;
					case StylePropertyId.UnitySliceTop:
						this.nonInheritedData.unitySliceTop = new StyleInt((int)sv.number, sv.keyword);
						return;
					case StylePropertyId.UnityTextOverflowPosition:
						this.nonInheritedData.unityTextOverflowPosition = new StyleEnum<TextOverflowPosition>((TextOverflowPosition)sv.number, sv.keyword);
						return;
					case StylePropertyId.Width:
						this.nonInheritedData.width = new StyleLength(sv.length, sv.keyword);
						return;
					}
					Debug.LogAssertion(string.Format("Unexpected property id {0}", sv.id));
					break;
				}
			}
		}

		public void ApplyStyleCursor(StyleCursor sc)
		{
			this.nonInheritedData.cursor = sc;
		}

		public void ApplyInitialValue(StylePropertyReader reader)
		{
			StylePropertyId propertyId = reader.propertyId;
			StylePropertyId stylePropertyId = propertyId;
			if (stylePropertyId != StylePropertyId.Custom)
			{
				this.ApplyInitialValue(reader.propertyId);
			}
			else
			{
				this.RemoveCustomStyleProperty(reader);
			}
		}

		public void ApplyInitialValue(StylePropertyId id)
		{
			switch (id)
			{
			case StylePropertyId.Color:
				this.inheritedData.color = InitialStyle.color;
				break;
			case StylePropertyId.FontSize:
				this.inheritedData.fontSize = InitialStyle.fontSize;
				break;
			case StylePropertyId.UnityFont:
				this.inheritedData.unityFont = InitialStyle.unityFont;
				break;
			case StylePropertyId.UnityFontStyleAndWeight:
				this.inheritedData.unityFontStyleAndWeight = InitialStyle.unityFontStyleAndWeight;
				break;
			case StylePropertyId.UnityTextAlign:
				this.inheritedData.unityTextAlign = InitialStyle.unityTextAlign;
				break;
			case StylePropertyId.Visibility:
				this.inheritedData.visibility = InitialStyle.visibility;
				break;
			case StylePropertyId.WhiteSpace:
				this.inheritedData.whiteSpace = InitialStyle.whiteSpace;
				break;
			default:
				switch (id)
				{
				case StylePropertyId.AlignContent:
					this.nonInheritedData.alignContent = InitialStyle.alignContent;
					break;
				case StylePropertyId.AlignItems:
					this.nonInheritedData.alignItems = InitialStyle.alignItems;
					break;
				case StylePropertyId.AlignSelf:
					this.nonInheritedData.alignSelf = InitialStyle.alignSelf;
					break;
				case StylePropertyId.BackgroundColor:
					this.nonInheritedData.backgroundColor = InitialStyle.backgroundColor;
					break;
				case StylePropertyId.BackgroundImage:
					this.nonInheritedData.backgroundImage = InitialStyle.backgroundImage;
					break;
				case StylePropertyId.BorderBottomColor:
					this.nonInheritedData.borderBottomColor = InitialStyle.borderBottomColor;
					break;
				case StylePropertyId.BorderBottomLeftRadius:
					this.nonInheritedData.borderBottomLeftRadius = InitialStyle.borderBottomLeftRadius;
					break;
				case StylePropertyId.BorderBottomRightRadius:
					this.nonInheritedData.borderBottomRightRadius = InitialStyle.borderBottomRightRadius;
					break;
				case StylePropertyId.BorderBottomWidth:
					this.nonInheritedData.borderBottomWidth = InitialStyle.borderBottomWidth;
					break;
				case StylePropertyId.BorderLeftColor:
					this.nonInheritedData.borderLeftColor = InitialStyle.borderLeftColor;
					break;
				case StylePropertyId.BorderLeftWidth:
					this.nonInheritedData.borderLeftWidth = InitialStyle.borderLeftWidth;
					break;
				case StylePropertyId.BorderRightColor:
					this.nonInheritedData.borderRightColor = InitialStyle.borderRightColor;
					break;
				case StylePropertyId.BorderRightWidth:
					this.nonInheritedData.borderRightWidth = InitialStyle.borderRightWidth;
					break;
				case StylePropertyId.BorderTopColor:
					this.nonInheritedData.borderTopColor = InitialStyle.borderTopColor;
					break;
				case StylePropertyId.BorderTopLeftRadius:
					this.nonInheritedData.borderTopLeftRadius = InitialStyle.borderTopLeftRadius;
					break;
				case StylePropertyId.BorderTopRightRadius:
					this.nonInheritedData.borderTopRightRadius = InitialStyle.borderTopRightRadius;
					break;
				case StylePropertyId.BorderTopWidth:
					this.nonInheritedData.borderTopWidth = InitialStyle.borderTopWidth;
					break;
				case StylePropertyId.Bottom:
					this.nonInheritedData.bottom = InitialStyle.bottom;
					break;
				case StylePropertyId.Cursor:
					this.nonInheritedData.cursor = InitialStyle.cursor;
					break;
				case StylePropertyId.Display:
					this.nonInheritedData.display = InitialStyle.display;
					break;
				case StylePropertyId.FlexBasis:
					this.nonInheritedData.flexBasis = InitialStyle.flexBasis;
					break;
				case StylePropertyId.FlexDirection:
					this.nonInheritedData.flexDirection = InitialStyle.flexDirection;
					break;
				case StylePropertyId.FlexGrow:
					this.nonInheritedData.flexGrow = InitialStyle.flexGrow;
					break;
				case StylePropertyId.FlexShrink:
					this.nonInheritedData.flexShrink = InitialStyle.flexShrink;
					break;
				case StylePropertyId.FlexWrap:
					this.nonInheritedData.flexWrap = InitialStyle.flexWrap;
					break;
				case StylePropertyId.Height:
					this.nonInheritedData.height = InitialStyle.height;
					break;
				case StylePropertyId.JustifyContent:
					this.nonInheritedData.justifyContent = InitialStyle.justifyContent;
					break;
				case StylePropertyId.Left:
					this.nonInheritedData.left = InitialStyle.left;
					break;
				case StylePropertyId.MarginBottom:
					this.nonInheritedData.marginBottom = InitialStyle.marginBottom;
					break;
				case StylePropertyId.MarginLeft:
					this.nonInheritedData.marginLeft = InitialStyle.marginLeft;
					break;
				case StylePropertyId.MarginRight:
					this.nonInheritedData.marginRight = InitialStyle.marginRight;
					break;
				case StylePropertyId.MarginTop:
					this.nonInheritedData.marginTop = InitialStyle.marginTop;
					break;
				case StylePropertyId.MaxHeight:
					this.nonInheritedData.maxHeight = InitialStyle.maxHeight;
					break;
				case StylePropertyId.MaxWidth:
					this.nonInheritedData.maxWidth = InitialStyle.maxWidth;
					break;
				case StylePropertyId.MinHeight:
					this.nonInheritedData.minHeight = InitialStyle.minHeight;
					break;
				case StylePropertyId.MinWidth:
					this.nonInheritedData.minWidth = InitialStyle.minWidth;
					break;
				case StylePropertyId.Opacity:
					this.nonInheritedData.opacity = InitialStyle.opacity;
					break;
				case StylePropertyId.Overflow:
					this.nonInheritedData.overflow = InitialStyle.overflow;
					break;
				case StylePropertyId.PaddingBottom:
					this.nonInheritedData.paddingBottom = InitialStyle.paddingBottom;
					break;
				case StylePropertyId.PaddingLeft:
					this.nonInheritedData.paddingLeft = InitialStyle.paddingLeft;
					break;
				case StylePropertyId.PaddingRight:
					this.nonInheritedData.paddingRight = InitialStyle.paddingRight;
					break;
				case StylePropertyId.PaddingTop:
					this.nonInheritedData.paddingTop = InitialStyle.paddingTop;
					break;
				case StylePropertyId.Position:
					this.nonInheritedData.position = InitialStyle.position;
					break;
				case StylePropertyId.Right:
					this.nonInheritedData.right = InitialStyle.right;
					break;
				case StylePropertyId.TextOverflow:
					this.nonInheritedData.textOverflow = InitialStyle.textOverflow;
					break;
				case StylePropertyId.Top:
					this.nonInheritedData.top = InitialStyle.top;
					break;
				case StylePropertyId.UnityBackgroundImageTintColor:
					this.nonInheritedData.unityBackgroundImageTintColor = InitialStyle.unityBackgroundImageTintColor;
					break;
				case StylePropertyId.UnityBackgroundScaleMode:
					this.nonInheritedData.unityBackgroundScaleMode = InitialStyle.unityBackgroundScaleMode;
					break;
				case StylePropertyId.UnityOverflowClipBox:
					this.nonInheritedData.unityOverflowClipBox = InitialStyle.unityOverflowClipBox;
					break;
				case StylePropertyId.UnitySliceBottom:
					this.nonInheritedData.unitySliceBottom = InitialStyle.unitySliceBottom;
					break;
				case StylePropertyId.UnitySliceLeft:
					this.nonInheritedData.unitySliceLeft = InitialStyle.unitySliceLeft;
					break;
				case StylePropertyId.UnitySliceRight:
					this.nonInheritedData.unitySliceRight = InitialStyle.unitySliceRight;
					break;
				case StylePropertyId.UnitySliceTop:
					this.nonInheritedData.unitySliceTop = InitialStyle.unitySliceTop;
					break;
				case StylePropertyId.UnityTextOverflowPosition:
					this.nonInheritedData.unityTextOverflowPosition = InitialStyle.unityTextOverflowPosition;
					break;
				case StylePropertyId.Width:
					this.nonInheritedData.width = InitialStyle.width;
					break;
				default:
					switch (id)
					{
					case StylePropertyId.BorderColor:
						this.nonInheritedData.borderTopColor = InitialStyle.borderTopColor;
						this.nonInheritedData.borderRightColor = InitialStyle.borderRightColor;
						this.nonInheritedData.borderBottomColor = InitialStyle.borderBottomColor;
						this.nonInheritedData.borderLeftColor = InitialStyle.borderLeftColor;
						break;
					case StylePropertyId.BorderRadius:
						this.nonInheritedData.borderTopLeftRadius = InitialStyle.borderTopLeftRadius;
						this.nonInheritedData.borderTopRightRadius = InitialStyle.borderTopRightRadius;
						this.nonInheritedData.borderBottomRightRadius = InitialStyle.borderBottomRightRadius;
						this.nonInheritedData.borderBottomLeftRadius = InitialStyle.borderBottomLeftRadius;
						break;
					case StylePropertyId.BorderWidth:
						this.nonInheritedData.borderTopWidth = InitialStyle.borderTopWidth;
						this.nonInheritedData.borderRightWidth = InitialStyle.borderRightWidth;
						this.nonInheritedData.borderBottomWidth = InitialStyle.borderBottomWidth;
						this.nonInheritedData.borderLeftWidth = InitialStyle.borderLeftWidth;
						break;
					case StylePropertyId.Flex:
						this.nonInheritedData.flexGrow = InitialStyle.flexGrow;
						this.nonInheritedData.flexShrink = InitialStyle.flexShrink;
						this.nonInheritedData.flexBasis = InitialStyle.flexBasis;
						break;
					case StylePropertyId.Margin:
						this.nonInheritedData.marginTop = InitialStyle.marginTop;
						this.nonInheritedData.marginRight = InitialStyle.marginRight;
						this.nonInheritedData.marginBottom = InitialStyle.marginBottom;
						this.nonInheritedData.marginLeft = InitialStyle.marginLeft;
						break;
					case StylePropertyId.Padding:
						this.nonInheritedData.paddingTop = InitialStyle.paddingTop;
						this.nonInheritedData.paddingRight = InitialStyle.paddingRight;
						this.nonInheritedData.paddingBottom = InitialStyle.paddingBottom;
						this.nonInheritedData.paddingLeft = InitialStyle.paddingLeft;
						break;
					default:
						Debug.LogAssertion(string.Format("Unexpected property id {0}", id));
						break;
					}
					break;
				}
				break;
			}
		}

		public void ApplyUnsetValue(StylePropertyReader reader, ComputedStyle parentStyle)
		{
			StylePropertyId propertyId = reader.propertyId;
			StylePropertyId stylePropertyId = propertyId;
			if (stylePropertyId != StylePropertyId.Custom)
			{
				this.ApplyUnsetValue(reader.propertyId, parentStyle);
			}
			else
			{
				this.RemoveCustomStyleProperty(reader);
			}
		}

		public void ApplyUnsetValue(StylePropertyId id, ComputedStyle parentStyle)
		{
			switch (id)
			{
			case StylePropertyId.Color:
				this.inheritedData.color = parentStyle.color;
				break;
			case StylePropertyId.FontSize:
				this.inheritedData.fontSize = parentStyle.fontSize;
				break;
			case StylePropertyId.UnityFont:
				this.inheritedData.unityFont = parentStyle.unityFont;
				break;
			case StylePropertyId.UnityFontStyleAndWeight:
				this.inheritedData.unityFontStyleAndWeight = parentStyle.unityFontStyleAndWeight;
				break;
			case StylePropertyId.UnityTextAlign:
				this.inheritedData.unityTextAlign = parentStyle.unityTextAlign;
				break;
			case StylePropertyId.Visibility:
				this.inheritedData.visibility = parentStyle.visibility;
				break;
			case StylePropertyId.WhiteSpace:
				this.inheritedData.whiteSpace = parentStyle.whiteSpace;
				break;
			default:
				this.ApplyInitialValue(id);
				break;
			}
		}
	}
}
