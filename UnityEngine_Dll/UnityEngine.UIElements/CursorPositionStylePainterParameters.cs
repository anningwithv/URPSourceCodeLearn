using System;

namespace UnityEngine.UIElements
{
	internal struct CursorPositionStylePainterParameters
	{
		public Rect rect;

		public string text;

		public Font font;

		public int fontSize;

		public FontStyle fontStyle;

		public TextAnchor anchor;

		public float wordWrapWidth;

		public bool richText;

		public int cursorIndex;

		public static CursorPositionStylePainterParameters GetDefault(VisualElement ve, string text)
		{
			ComputedStyle computedStyle = ve.computedStyle;
			return new CursorPositionStylePainterParameters
			{
				rect = ve.contentRect,
				text = text,
				font = computedStyle.unityFont.value,
				fontSize = (int)computedStyle.fontSize.value.value,
				fontStyle = computedStyle.unityFontStyleAndWeight.value,
				anchor = computedStyle.unityTextAlign.value,
				wordWrapWidth = ((computedStyle.whiteSpace.value == WhiteSpace.Normal) ? ve.contentRect.width : 0f),
				richText = false,
				cursorIndex = 0
			};
		}

		internal TextNativeSettings GetTextNativeSettings(float scaling)
		{
			return new TextNativeSettings
			{
				text = this.text,
				font = this.font,
				size = this.fontSize,
				scaling = scaling,
				style = this.fontStyle,
				color = Color.white,
				anchor = this.anchor,
				wordWrap = true,
				wordWrapWidth = this.wordWrapWidth,
				richText = this.richText
			};
		}
	}
}
