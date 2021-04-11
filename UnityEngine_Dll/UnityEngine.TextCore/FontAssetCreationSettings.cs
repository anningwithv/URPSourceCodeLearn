using System;

namespace UnityEngine.TextCore
{
	[Serializable]
	internal struct FontAssetCreationSettings
	{
		public string fontFileGUID;

		public int pointSizeSamplingMode;

		public int pointSize;

		public int padding;

		public int packingMode;

		public int atlasWidth;

		public int atlasHeight;

		public int characterSetSelectionMode;

		public string characterSequence;

		public string referencedFontAssetGUID;

		public string referencedTextAssetGUID;

		public int fontStyle;

		public float fontStyleModifier;

		public int renderMode;

		public bool includeFontFeatures;

		internal FontAssetCreationSettings(string fontFileGUID, int pointSize, int pointSizeSamplingMode, int padding, int packingMode, int atlasWidth, int atlasHeight, int characterSelectionMode, string characterSet, int renderMode)
		{
			this.fontFileGUID = fontFileGUID;
			this.pointSize = pointSize;
			this.pointSizeSamplingMode = pointSizeSamplingMode;
			this.padding = padding;
			this.packingMode = packingMode;
			this.atlasWidth = atlasWidth;
			this.atlasHeight = atlasHeight;
			this.characterSequence = characterSet;
			this.characterSetSelectionMode = characterSelectionMode;
			this.renderMode = renderMode;
			this.referencedFontAssetGUID = string.Empty;
			this.referencedTextAssetGUID = string.Empty;
			this.fontStyle = 0;
			this.fontStyleModifier = 0f;
			this.includeFontFeatures = false;
		}
	}
}
