using System;

namespace UnityEngine.TextCore
{
	internal class TextGenerationSettings
	{
		public string text = null;

		public Rect screenRect;

		public Vector4 margins;

		public float scale = 1f;

		public FontAsset fontAsset;

		public Material material;

		public TextSpriteAsset spriteAsset;

		public FontStyles fontStyle = FontStyles.Normal;

		public TextAlignment textAlignment = TextAlignment.TopLeft;

		public TextOverflowMode overflowMode = TextOverflowMode.Overflow;

		public bool wordWrap = false;

		public float wordWrappingRatio;

		public Color color = Color.white;

		public TextGradientPreset fontColorGradient;

		public bool tintSprites;

		public bool overrideRichTextColors;

		public float fontSize = 18f;

		public bool autoSize;

		public float fontSizeMin;

		public float fontSizeMax;

		public bool enableKerning = true;

		public bool richText;

		public bool isRightToLeft;

		public bool extraPadding;

		public bool parseControlCharacters = true;

		public float characterSpacing;

		public float wordSpacing;

		public float lineSpacing;

		public float paragraphSpacing;

		public float lineSpacingMax;

		public int maxVisibleCharacters = 99999;

		public int maxVisibleWords = 99999;

		public int maxVisibleLines = 99999;

		public int firstVisibleCharacter = 0;

		public bool useMaxVisibleDescender;

		public FontWeight fontWeight = FontWeight.Regular;

		public int pageToDisplay = 1;

		public TextureMapping horizontalMapping = TextureMapping.Character;

		public TextureMapping verticalMapping = TextureMapping.Character;

		public float uvLineOffset;

		public VertexSortingOrder geometrySortingOrder = VertexSortingOrder.Normal;

		public bool inverseYAxis;

		public float charWidthMaxAdj;

		protected bool Equals(TextGenerationSettings other)
		{
			return string.Equals(this.text, other.text) && this.screenRect.Equals(other.screenRect) && this.margins.Equals(other.margins) && this.scale.Equals(other.scale) && object.Equals(this.fontAsset, other.fontAsset) && object.Equals(this.material, other.material) && object.Equals(this.spriteAsset, other.spriteAsset) && this.fontStyle == other.fontStyle && this.textAlignment == other.textAlignment && this.overflowMode == other.overflowMode && this.wordWrap == other.wordWrap && this.wordWrappingRatio.Equals(other.wordWrappingRatio) && this.color.Equals(other.color) && object.Equals(this.fontColorGradient, other.fontColorGradient) && this.tintSprites == other.tintSprites && this.overrideRichTextColors == other.overrideRichTextColors && this.fontSize.Equals(other.fontSize) && this.autoSize == other.autoSize && this.fontSizeMin.Equals(other.fontSizeMin) && this.fontSizeMax.Equals(other.fontSizeMax) && this.enableKerning == other.enableKerning && this.richText == other.richText && this.isRightToLeft == other.isRightToLeft && this.extraPadding == other.extraPadding && this.parseControlCharacters == other.parseControlCharacters && this.characterSpacing.Equals(other.characterSpacing) && this.wordSpacing.Equals(other.wordSpacing) && this.lineSpacing.Equals(other.lineSpacing) && this.paragraphSpacing.Equals(other.paragraphSpacing) && this.lineSpacingMax.Equals(other.lineSpacingMax) && this.maxVisibleCharacters == other.maxVisibleCharacters && this.maxVisibleWords == other.maxVisibleWords && this.maxVisibleLines == other.maxVisibleLines && this.firstVisibleCharacter == other.firstVisibleCharacter && this.useMaxVisibleDescender == other.useMaxVisibleDescender && this.fontWeight == other.fontWeight && this.pageToDisplay == other.pageToDisplay && this.horizontalMapping == other.horizontalMapping && this.verticalMapping == other.verticalMapping && this.uvLineOffset.Equals(other.uvLineOffset) && this.geometrySortingOrder == other.geometrySortingOrder && this.inverseYAxis == other.inverseYAxis && this.charWidthMaxAdj.Equals(other.charWidthMaxAdj);
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == obj;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = obj.GetType() != base.GetType();
					result = (!flag3 && this.Equals((TextGenerationSettings)obj));
				}
			}
			return result;
		}

		public override int GetHashCode()
		{
			int num = (this.text != null) ? this.text.GetHashCode() : 0;
			num = (num * 397 ^ this.screenRect.GetHashCode());
			num = (num * 397 ^ this.margins.GetHashCode());
			num = (num * 397 ^ this.scale.GetHashCode());
			num = (num * 397 ^ ((this.fontAsset != null) ? this.fontAsset.GetHashCode() : 0));
			num = (num * 397 ^ ((this.material != null) ? this.material.GetHashCode() : 0));
			num = (num * 397 ^ ((this.spriteAsset != null) ? this.spriteAsset.GetHashCode() : 0));
			num = (num * 397 ^ (int)this.fontStyle);
			num = (num * 397 ^ (int)this.textAlignment);
			num = (num * 397 ^ (int)this.overflowMode);
			num = (num * 397 ^ this.wordWrap.GetHashCode());
			num = (num * 397 ^ this.wordWrappingRatio.GetHashCode());
			num = (num * 397 ^ this.color.GetHashCode());
			num = (num * 397 ^ ((this.fontColorGradient != null) ? this.fontColorGradient.GetHashCode() : 0));
			num = (num * 397 ^ this.tintSprites.GetHashCode());
			num = (num * 397 ^ this.overrideRichTextColors.GetHashCode());
			num = (num * 397 ^ this.fontSize.GetHashCode());
			num = (num * 397 ^ this.autoSize.GetHashCode());
			num = (num * 397 ^ this.fontSizeMin.GetHashCode());
			num = (num * 397 ^ this.fontSizeMax.GetHashCode());
			num = (num * 397 ^ this.enableKerning.GetHashCode());
			num = (num * 397 ^ this.richText.GetHashCode());
			num = (num * 397 ^ this.isRightToLeft.GetHashCode());
			num = (num * 397 ^ this.extraPadding.GetHashCode());
			num = (num * 397 ^ this.parseControlCharacters.GetHashCode());
			num = (num * 397 ^ this.characterSpacing.GetHashCode());
			num = (num * 397 ^ this.wordSpacing.GetHashCode());
			num = (num * 397 ^ this.lineSpacing.GetHashCode());
			num = (num * 397 ^ this.paragraphSpacing.GetHashCode());
			num = (num * 397 ^ this.lineSpacingMax.GetHashCode());
			num = (num * 397 ^ this.maxVisibleCharacters);
			num = (num * 397 ^ this.maxVisibleWords);
			num = (num * 397 ^ this.maxVisibleLines);
			num = (num * 397 ^ this.firstVisibleCharacter);
			num = (num * 397 ^ this.useMaxVisibleDescender.GetHashCode());
			num = (num * 397 ^ (int)this.fontWeight);
			num = (num * 397 ^ this.pageToDisplay);
			num = (num * 397 ^ (int)this.horizontalMapping);
			num = (num * 397 ^ (int)this.verticalMapping);
			num = (num * 397 ^ this.uvLineOffset.GetHashCode());
			num = (num * 397 ^ (int)this.geometrySortingOrder);
			num = (num * 397 ^ this.inverseYAxis.GetHashCode());
			return num * 397 ^ this.charWidthMaxAdj.GetHashCode();
		}

		public static bool operator ==(TextGenerationSettings left, TextGenerationSettings right)
		{
			return object.Equals(left, right);
		}

		public static bool operator !=(TextGenerationSettings left, TextGenerationSettings right)
		{
			return !object.Equals(left, right);
		}

		public void Copy(TextGenerationSettings other)
		{
			bool flag = other == null;
			if (!flag)
			{
				this.text = other.text;
				this.screenRect = other.screenRect;
				this.margins = other.margins;
				this.scale = other.scale;
				this.fontAsset = other.fontAsset;
				this.material = other.material;
				this.spriteAsset = other.spriteAsset;
				this.fontStyle = other.fontStyle;
				this.textAlignment = other.textAlignment;
				this.overflowMode = other.overflowMode;
				this.wordWrap = other.wordWrap;
				this.wordWrappingRatio = other.wordWrappingRatio;
				this.color = other.color;
				this.fontColorGradient = other.fontColorGradient;
				this.tintSprites = other.tintSprites;
				this.overrideRichTextColors = other.overrideRichTextColors;
				this.fontSize = other.fontSize;
				this.autoSize = other.autoSize;
				this.fontSizeMin = other.fontSizeMin;
				this.fontSizeMax = other.fontSizeMax;
				this.enableKerning = other.enableKerning;
				this.richText = other.richText;
				this.isRightToLeft = other.isRightToLeft;
				this.extraPadding = other.extraPadding;
				this.parseControlCharacters = other.parseControlCharacters;
				this.characterSpacing = other.characterSpacing;
				this.wordSpacing = other.wordSpacing;
				this.lineSpacing = other.lineSpacing;
				this.paragraphSpacing = other.paragraphSpacing;
				this.lineSpacingMax = other.lineSpacingMax;
				this.maxVisibleCharacters = other.maxVisibleCharacters;
				this.maxVisibleWords = other.maxVisibleWords;
				this.maxVisibleLines = other.maxVisibleLines;
				this.firstVisibleCharacter = other.firstVisibleCharacter;
				this.useMaxVisibleDescender = other.useMaxVisibleDescender;
				this.fontWeight = other.fontWeight;
				this.pageToDisplay = other.pageToDisplay;
				this.horizontalMapping = other.horizontalMapping;
				this.verticalMapping = other.verticalMapping;
				this.uvLineOffset = other.uvLineOffset;
				this.geometrySortingOrder = other.geometrySortingOrder;
				this.inverseYAxis = other.inverseYAxis;
				this.charWidthMaxAdj = other.charWidthMaxAdj;
			}
		}
	}
}
