using System;
using UnityEngine.TextCore;

namespace UnityEngine.UIElements
{
	internal static class MeshGenerationContextUtils
	{
		public struct BorderParams
		{
			public Rect rect;

			public Color playmodeTintColor;

			public Color leftColor;

			public Color topColor;

			public Color rightColor;

			public Color bottomColor;

			public float leftWidth;

			public float topWidth;

			public float rightWidth;

			public float bottomWidth;

			public Vector2 topLeftRadius;

			public Vector2 topRightRadius;

			public Vector2 bottomRightRadius;

			public Vector2 bottomLeftRadius;

			public Material material;
		}

		public struct RectangleParams
		{
			public Rect rect;

			public Rect uv;

			public Color color;

			public Texture texture;

			public VectorImage vectorImage;

			public Material material;

			public ScaleMode scaleMode;

			public Color playmodeTintColor;

			public Vector2 topLeftRadius;

			public Vector2 topRightRadius;

			public Vector2 bottomRightRadius;

			public Vector2 bottomLeftRadius;

			public int leftSlice;

			public int topSlice;

			public int rightSlice;

			public int bottomSlice;

			public static MeshGenerationContextUtils.RectangleParams MakeSolid(Rect rect, Color color, ContextType panelContext)
			{
				Color color2 = (panelContext == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white;
				return new MeshGenerationContextUtils.RectangleParams
				{
					rect = rect,
					color = color,
					uv = new Rect(0f, 0f, 1f, 1f),
					playmodeTintColor = color2
				};
			}

			public static MeshGenerationContextUtils.RectangleParams MakeTextured(Rect rect, Rect uv, Texture texture, ScaleMode scaleMode, ContextType panelContext)
			{
				Color color = (panelContext == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white;
				float num = (float)texture.width * uv.width / ((float)texture.height * uv.height);
				float num2 = rect.width / rect.height;
				switch (scaleMode)
				{
				case ScaleMode.StretchToFill:
					break;
				case ScaleMode.ScaleAndCrop:
				{
					bool flag = num2 > num;
					if (flag)
					{
						float num3 = uv.height * (num / num2);
						float num4 = (uv.height - num3) * 0.5f;
						uv = new Rect(uv.x, uv.y + num4, uv.width, num3);
					}
					else
					{
						float num5 = uv.width * (num2 / num);
						float num6 = (uv.width - num5) * 0.5f;
						uv = new Rect(uv.x + num6, uv.y, num5, uv.height);
					}
					break;
				}
				case ScaleMode.ScaleToFit:
				{
					bool flag2 = num2 > num;
					if (flag2)
					{
						float num7 = num / num2;
						rect = new Rect(rect.xMin + rect.width * (1f - num7) * 0.5f, rect.yMin, num7 * rect.width, rect.height);
					}
					else
					{
						float num8 = num2 / num;
						rect = new Rect(rect.xMin, rect.yMin + rect.height * (1f - num8) * 0.5f, rect.width, num8 * rect.height);
					}
					break;
				}
				default:
					throw new NotImplementedException();
				}
				return new MeshGenerationContextUtils.RectangleParams
				{
					rect = rect,
					uv = uv,
					color = Color.white,
					texture = texture,
					scaleMode = scaleMode,
					playmodeTintColor = color
				};
			}

			public static MeshGenerationContextUtils.RectangleParams MakeVectorTextured(Rect rect, Rect uv, VectorImage vectorImage, ScaleMode scaleMode, ContextType panelContext)
			{
				Color color = (panelContext == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white;
				return new MeshGenerationContextUtils.RectangleParams
				{
					rect = rect,
					uv = uv,
					color = Color.white,
					vectorImage = vectorImage,
					scaleMode = scaleMode,
					playmodeTintColor = color
				};
			}

			internal bool HasRadius(float epsilon)
			{
				return (this.topLeftRadius.x > epsilon && this.topLeftRadius.y > epsilon) || (this.topRightRadius.x > epsilon && this.topRightRadius.y > epsilon) || (this.bottomRightRadius.x > epsilon && this.bottomRightRadius.y > epsilon) || (this.bottomLeftRadius.x > epsilon && this.bottomLeftRadius.y > epsilon);
			}
		}

		public struct TextParams
		{
			public Rect rect;

			public string text;

			public Font font;

			public int fontSize;

			public FontStyle fontStyle;

			public Color fontColor;

			public TextAnchor anchor;

			public bool wordWrap;

			public float wordWrapWidth;

			public bool richText;

			public Material material;

			public Color playmodeTintColor;

			public TextOverflowMode textOverflowMode;

			public TextOverflowPosition textOverflowPosition;

			public override int GetHashCode()
			{
				int num = this.rect.GetHashCode();
				num = (num * 397 ^ ((this.text != null) ? this.text.GetHashCode() : 0));
				num = (num * 397 ^ ((this.font != null) ? this.font.GetHashCode() : 0));
				num = (num * 397 ^ this.fontSize);
				num = (num * 397 ^ (int)this.fontStyle);
				num = (num * 397 ^ this.fontColor.GetHashCode());
				num = (num * 397 ^ (int)this.anchor);
				num = (num * 397 ^ this.wordWrap.GetHashCode());
				num = (num * 397 ^ this.wordWrapWidth.GetHashCode());
				num = (num * 397 ^ this.richText.GetHashCode());
				num = (num * 397 ^ ((this.material != null) ? this.material.GetHashCode() : 0));
				num = (num * 397 ^ this.playmodeTintColor.GetHashCode());
				num = (num * 397 ^ this.textOverflowMode.GetHashCode());
				return num * 397 ^ this.textOverflowPosition.GetHashCode();
			}

			internal static MeshGenerationContextUtils.TextParams MakeStyleBased(VisualElement ve, string text)
			{
				ComputedStyle computedStyle = ve.computedStyle;
				MeshGenerationContextUtils.TextParams result = default(MeshGenerationContextUtils.TextParams);
				result.rect = ve.contentRect;
				result.text = text;
				result.font = computedStyle.unityFont.value;
				result.fontSize = (int)computedStyle.fontSize.value.value;
				result.fontStyle = computedStyle.unityFontStyleAndWeight.value;
				result.fontColor = computedStyle.color.value;
				result.anchor = computedStyle.unityTextAlign.value;
				result.wordWrap = (computedStyle.whiteSpace.value == WhiteSpace.Normal);
				result.wordWrapWidth = ((computedStyle.whiteSpace.value == WhiteSpace.Normal) ? ve.contentRect.width : 0f);
				result.richText = false;
				IPanel expr_F2 = ve.panel;
				result.playmodeTintColor = ((expr_F2 != null && expr_F2.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white);
				result.textOverflowMode = MeshGenerationContextUtils.TextParams.GetTextOverflowMode(computedStyle);
				result.textOverflowPosition = computedStyle.unityTextOverflowPosition.value;
				return result;
			}

			public static TextOverflowMode GetTextOverflowMode(ComputedStyle style)
			{
				bool flag = style.textOverflow.value == TextOverflow.Clip;
				TextOverflowMode result;
				if (flag)
				{
					result = TextOverflowMode.Masking;
				}
				else
				{
					bool flag2 = style.textOverflow.value != TextOverflow.Ellipsis;
					if (flag2)
					{
						result = TextOverflowMode.Overflow;
					}
					else
					{
						bool flag3 = style.whiteSpace.value == WhiteSpace.NoWrap && style.overflow == OverflowInternal.Hidden;
						if (flag3)
						{
							result = TextOverflowMode.Ellipsis;
						}
						else
						{
							result = TextOverflowMode.Overflow;
						}
					}
				}
				return result;
			}

			internal static TextNativeSettings GetTextNativeSettings(MeshGenerationContextUtils.TextParams textParams, float scaling)
			{
				TextNativeSettings result = new TextNativeSettings
				{
					text = textParams.text,
					font = textParams.font,
					size = textParams.fontSize,
					scaling = scaling,
					style = textParams.fontStyle,
					color = textParams.fontColor,
					anchor = textParams.anchor,
					wordWrap = textParams.wordWrap,
					wordWrapWidth = textParams.wordWrapWidth,
					richText = textParams.richText
				};
				result.color *= textParams.playmodeTintColor;
				return result;
			}
		}

		public static void Rectangle(this MeshGenerationContext mgc, MeshGenerationContextUtils.RectangleParams rectParams)
		{
			mgc.painter.DrawRectangle(rectParams);
		}

		public static void Border(this MeshGenerationContext mgc, MeshGenerationContextUtils.BorderParams borderParams)
		{
			mgc.painter.DrawBorder(borderParams);
		}

		public static void Text(this MeshGenerationContext mgc, MeshGenerationContextUtils.TextParams textParams, TextHandle handle, float pixelsPerPoint)
		{
			bool flag = textParams.font != null;
			if (flag)
			{
				mgc.painter.DrawText(textParams, handle, pixelsPerPoint);
			}
		}

		private static Vector2 ConvertBorderRadiusPercentToPoints(Vector2 borderRectSize, Length length)
		{
			float num = length.value;
			float num2 = length.value;
			bool flag = length.unit == LengthUnit.Percent;
			if (flag)
			{
				num = borderRectSize.x * length.value / 100f;
				num2 = borderRectSize.y * length.value / 100f;
			}
			num = Mathf.Max(num, 0f);
			num2 = Mathf.Max(num2, 0f);
			return new Vector2(num, num2);
		}

		public static void GetVisualElementRadii(VisualElement ve, out Vector2 topLeft, out Vector2 bottomLeft, out Vector2 topRight, out Vector2 bottomRight)
		{
			IResolvedStyle resolvedStyle = ve.resolvedStyle;
			Vector2 borderRectSize = new Vector2(resolvedStyle.width, resolvedStyle.height);
			ComputedStyle computedStyle = ve.computedStyle;
			topLeft = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(borderRectSize, computedStyle.borderTopLeftRadius.value);
			bottomLeft = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(borderRectSize, computedStyle.borderBottomLeftRadius.value);
			topRight = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(borderRectSize, computedStyle.borderTopRightRadius.value);
			bottomRight = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(borderRectSize, computedStyle.borderBottomRightRadius.value);
		}
	}
}
