using System;

namespace UnityEngine.TextCore
{
	internal struct TextElementInfo
	{
		public char character;

		public int index;

		public TextElementType elementType;

		public TextElement textElement;

		public FontAsset fontAsset;

		public TextSpriteAsset spriteAsset;

		public int spriteIndex;

		public Material material;

		public int materialReferenceIndex;

		public bool isUsingAlternateTypeface;

		public float pointSize;

		public int lineNumber;

		public int pageNumber;

		public int vertexIndex;

		public TextVertex vertexTopLeft;

		public TextVertex vertexBottomLeft;

		public TextVertex vertexTopRight;

		public TextVertex vertexBottomRight;

		public Vector3 topLeft;

		public Vector3 bottomLeft;

		public Vector3 topRight;

		public Vector3 bottomRight;

		public float origin;

		public float ascender;

		public float baseLine;

		public float descender;

		public float xAdvance;

		public float aspectRatio;

		public float scale;

		public Color32 color;

		public Color32 underlineColor;

		public Color32 strikethroughColor;

		public Color32 highlightColor;

		public FontStyles style;

		public bool isVisible;
	}
}
