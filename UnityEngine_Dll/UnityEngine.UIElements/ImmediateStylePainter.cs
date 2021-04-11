using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	[NativeHeader("Modules/UIElementsNative/ImmediateStylePainter.h")]
	[StructLayout(LayoutKind.Sequential)]
	internal class ImmediateStylePainter
	{
		internal static void DrawRect(Rect screenRect, Color color, Vector4 borderWidths, Vector4 borderRadiuses)
		{
			ImmediateStylePainter.DrawRect_Injected(ref screenRect, ref color, ref borderWidths, ref borderRadiuses);
		}

		internal static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, Color color, Vector4 borderWidths, Vector4 borderRadiuses, int leftBorder, int topBorder, int rightBorder, int bottomBorder, bool usePremultiplyAlpha)
		{
			ImmediateStylePainter.DrawTexture_Injected(ref screenRect, texture, ref sourceRect, ref color, ref borderWidths, ref borderRadiuses, leftBorder, topBorder, rightBorder, bottomBorder, usePremultiplyAlpha);
		}

		internal static void DrawText(Rect screenRect, string text, Font font, int fontSize, FontStyle fontStyle, Color fontColor, TextAnchor anchor, bool wordWrap, float wordWrapWidth, bool richText, TextClipping textClipping)
		{
			ImmediateStylePainter.DrawText_Injected(ref screenRect, text, font, fontSize, fontStyle, ref fontColor, anchor, wordWrap, wordWrapWidth, richText, textClipping);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawRect_Injected(ref Rect screenRect, ref Color color, ref Vector4 borderWidths, ref Vector4 borderRadiuses);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawTexture_Injected(ref Rect screenRect, Texture texture, ref Rect sourceRect, ref Color color, ref Vector4 borderWidths, ref Vector4 borderRadiuses, int leftBorder, int topBorder, int rightBorder, int bottomBorder, bool usePremultiplyAlpha);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawText_Injected(ref Rect screenRect, string text, Font font, int fontSize, FontStyle fontStyle, ref Color fontColor, TextAnchor anchor, bool wordWrap, float wordWrapWidth, bool richText, TextClipping textClipping);
	}
}
