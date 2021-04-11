using System;

namespace UnityEngine.TextCore
{
	internal struct WordWrapState
	{
		public int previousWordBreak;

		public int totalCharacterCount;

		public int visibleCharacterCount;

		public int visibleSpriteCount;

		public int visibleLinkCount;

		public int firstCharacterIndex;

		public int firstVisibleCharacterIndex;

		public int lastCharacterIndex;

		public int lastVisibleCharIndex;

		public int lineNumber;

		public float maxCapHeight;

		public float maxAscender;

		public float maxDescender;

		public float maxLineAscender;

		public float maxLineDescender;

		public float previousLineAscender;

		public float xAdvance;

		public float preferredWidth;

		public float preferredHeight;

		public float previousLineScale;

		public int wordCount;

		public FontStyles fontStyle;

		public float fontScale;

		public float fontScaleMultiplier;

		public float currentFontSize;

		public float baselineOffset;

		public float lineOffset;

		public TextInfo textInfo;

		public LineInfo lineInfo;

		public Color32 vertexColor;

		public Color32 underlineColor;

		public Color32 strikethroughColor;

		public Color32 highlightColor;

		public FontStyleStack basicStyleStack;

		public RichTextTagStack<Color32> colorStack;

		public RichTextTagStack<Color32> underlineColorStack;

		public RichTextTagStack<Color32> strikethroughColorStack;

		public RichTextTagStack<Color32> highlightColorStack;

		public RichTextTagStack<TextGradientPreset> colorGradientStack;

		public RichTextTagStack<float> sizeStack;

		public RichTextTagStack<float> indentStack;

		public RichTextTagStack<FontWeight> fontWeightStack;

		public RichTextTagStack<int> styleStack;

		public RichTextTagStack<float> baselineStack;

		public RichTextTagStack<int> actionStack;

		public RichTextTagStack<MaterialReference> materialReferenceStack;

		public RichTextTagStack<TextAlignment> lineJustificationStack;

		public int spriteAnimationId;

		public FontAsset currentFontAsset;

		public TextSpriteAsset currentSpriteAsset;

		public Material currentMaterial;

		public int currentMaterialIndex;

		public Extents meshExtents;

		public bool tagNoParsing;

		public bool isNonBreakingSpace;
	}
}
