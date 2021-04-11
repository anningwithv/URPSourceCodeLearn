using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements.Experimental
{
	internal static class Lerp
	{
		public static float Interpolate(float start, float end, float ratio)
		{
			return Mathf.LerpUnclamped(start, end, ratio);
		}

		public static int Interpolate(int start, int end, float ratio)
		{
			return Mathf.RoundToInt(Mathf.LerpUnclamped((float)start, (float)end, ratio));
		}

		public static Rect Interpolate(Rect r1, Rect r2, float ratio)
		{
			return new Rect(Mathf.LerpUnclamped(r1.x, r2.x, ratio), Mathf.LerpUnclamped(r1.y, r2.y, ratio), Mathf.LerpUnclamped(r1.width, r2.width, ratio), Mathf.LerpUnclamped(r1.height, r2.height, ratio));
		}

		public static Color Interpolate(Color start, Color end, float ratio)
		{
			return Color.LerpUnclamped(start, end, ratio);
		}

		public static Vector2 Interpolate(Vector2 start, Vector2 end, float ratio)
		{
			return Vector2.LerpUnclamped(start, end, ratio);
		}

		public static Vector3 Interpolate(Vector3 start, Vector3 end, float ratio)
		{
			return Vector3.LerpUnclamped(start, end, ratio);
		}

		public static Quaternion Interpolate(Quaternion start, Quaternion end, float ratio)
		{
			return Quaternion.SlerpUnclamped(start, end, ratio);
		}

		internal static StyleValues Interpolate(StyleValues start, StyleValues end, float ratio)
		{
			StyleValues result = default(StyleValues);
			foreach (StyleValue current in end.m_StyleValues.m_Values)
			{
				StyleValue styleValue = default(StyleValue);
				bool flag = !start.m_StyleValues.TryGetStyleValue(current.id, ref styleValue);
				if (flag)
				{
					throw new ArgumentException("Start StyleValues must contain the same values as end values. Missing property:" + current.id.ToString());
				}
				StylePropertyId id = current.id;
				StylePropertyId stylePropertyId = id;
				if (stylePropertyId <= StylePropertyId.Width)
				{
					switch (stylePropertyId)
					{
					case StylePropertyId.Custom:
					case StylePropertyId.Unknown:
					case StylePropertyId.UnityFont:
					case StylePropertyId.UnityFontStyleAndWeight:
					case StylePropertyId.UnityTextAlign:
					case StylePropertyId.Visibility:
					case StylePropertyId.WhiteSpace:
						goto IL_203;
					case StylePropertyId.Color:
						goto IL_1DF;
					case StylePropertyId.FontSize:
						break;
					default:
						switch (stylePropertyId)
						{
						case StylePropertyId.AlignContent:
						case StylePropertyId.AlignItems:
						case StylePropertyId.AlignSelf:
						case StylePropertyId.BackgroundImage:
						case StylePropertyId.BorderBottomColor:
						case StylePropertyId.BorderLeftColor:
						case StylePropertyId.BorderRightColor:
						case StylePropertyId.BorderTopColor:
						case StylePropertyId.Cursor:
						case StylePropertyId.Display:
						case StylePropertyId.FlexDirection:
						case StylePropertyId.FlexWrap:
						case StylePropertyId.JustifyContent:
						case StylePropertyId.Overflow:
						case StylePropertyId.Position:
						case StylePropertyId.TextOverflow:
						case StylePropertyId.UnityBackgroundScaleMode:
						case StylePropertyId.UnityOverflowClipBox:
						case StylePropertyId.UnitySliceBottom:
						case StylePropertyId.UnitySliceLeft:
						case StylePropertyId.UnitySliceRight:
						case StylePropertyId.UnitySliceTop:
						case StylePropertyId.UnityTextOverflowPosition:
							goto IL_203;
						case StylePropertyId.BackgroundColor:
						case StylePropertyId.UnityBackgroundImageTintColor:
							goto IL_1DF;
						case StylePropertyId.BorderBottomLeftRadius:
						case StylePropertyId.BorderBottomRightRadius:
						case StylePropertyId.BorderBottomWidth:
						case StylePropertyId.BorderLeftWidth:
						case StylePropertyId.BorderRightWidth:
						case StylePropertyId.BorderTopLeftRadius:
						case StylePropertyId.BorderTopRightRadius:
						case StylePropertyId.BorderTopWidth:
						case StylePropertyId.Bottom:
						case StylePropertyId.FlexBasis:
						case StylePropertyId.FlexGrow:
						case StylePropertyId.FlexShrink:
						case StylePropertyId.Height:
						case StylePropertyId.Left:
						case StylePropertyId.MarginBottom:
						case StylePropertyId.MarginLeft:
						case StylePropertyId.MarginRight:
						case StylePropertyId.MarginTop:
						case StylePropertyId.MaxHeight:
						case StylePropertyId.MaxWidth:
						case StylePropertyId.MinHeight:
						case StylePropertyId.MinWidth:
						case StylePropertyId.Opacity:
						case StylePropertyId.PaddingBottom:
						case StylePropertyId.PaddingLeft:
						case StylePropertyId.PaddingRight:
						case StylePropertyId.PaddingTop:
						case StylePropertyId.Right:
						case StylePropertyId.Top:
						case StylePropertyId.Width:
							break;
						default:
							goto IL_203;
						}
						break;
					}
					result.SetValue(current.id, Lerp.Interpolate(styleValue.number, current.number, ratio));
				}
				else
				{
					if (stylePropertyId == StylePropertyId.BorderColor)
					{
						goto IL_1DF;
					}
					if (stylePropertyId - StylePropertyId.BorderRadius > 4)
					{
						goto IL_203;
					}
					goto IL_203;
				}
				continue;
				IL_1DF:
				result.SetValue(current.id, Lerp.Interpolate(styleValue.color, current.color, ratio));
				continue;
				IL_203:
				throw new ArgumentException("Style Value can't be animated");
			}
			return result;
		}
	}
}
