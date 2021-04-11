using System;
using System.Collections.Generic;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.UIElements
{
	internal struct TextHandle
	{
		public bool useLegacy;

		private static Dictionary<Font, FontAsset> fontAssetCache = new Dictionary<Font, FontAsset>();

		private Vector2 m_PreferredSize;

		private int m_PreviousGenerationSettingsHash;

		private UnityEngine.TextCore.TextGenerationSettings m_CurrentGenerationSettings;

		private int m_PreviousLayoutSettingsHash;

		private UnityEngine.TextCore.TextGenerationSettings m_CurrentLayoutSettings;

		private TextInfo m_TextInfo;

		internal TextInfo textInfo
		{
			get
			{
				bool flag = this.m_TextInfo == null;
				if (flag)
				{
					this.m_TextInfo = new TextInfo();
				}
				return this.m_TextInfo;
			}
		}

		public static TextHandle New()
		{
			return new TextHandle
			{
				useLegacy = false,
				m_CurrentGenerationSettings = new UnityEngine.TextCore.TextGenerationSettings(),
				m_CurrentLayoutSettings = new UnityEngine.TextCore.TextGenerationSettings()
			};
		}

		private static FontAsset GetFontAsset(Font font)
		{
			FontAsset fontAsset = null;
			bool flag = TextHandle.fontAssetCache.TryGetValue(font, out fontAsset) && fontAsset != null;
			FontAsset result;
			if (flag)
			{
				result = fontAsset;
			}
			else
			{
				fontAsset = FontAsset.CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024, FontAsset.AtlasPopulationMode.Dynamic);
				result = (TextHandle.fontAssetCache[font] = fontAsset);
			}
			return result;
		}

		internal bool IsTextInfoAllocated()
		{
			return this.m_TextInfo != null;
		}

		public Vector2 GetCursorPosition(CursorPositionStylePainterParameters parms, float scaling)
		{
			bool flag = this.useLegacy;
			Vector2 cursorPosition;
			if (flag)
			{
				cursorPosition = TextNative.GetCursorPosition(parms.GetTextNativeSettings(scaling), parms.rect, parms.cursorIndex);
			}
			else
			{
				cursorPosition = UnityEngine.TextCore.TextGenerator.GetCursorPosition(this.textInfo, parms.rect, parms.cursorIndex);
			}
			return cursorPosition;
		}

		public float ComputeTextWidth(MeshGenerationContextUtils.TextParams parms, float scaling)
		{
			bool flag = this.useLegacy;
			float result;
			if (flag)
			{
				result = TextNative.ComputeTextWidth(MeshGenerationContextUtils.TextParams.GetTextNativeSettings(parms, scaling));
			}
			else
			{
				this.UpdatePreferredValues(parms);
				result = this.m_PreferredSize.x;
			}
			return result;
		}

		public float ComputeTextHeight(MeshGenerationContextUtils.TextParams parms, float scaling)
		{
			bool flag = this.useLegacy;
			float result;
			if (flag)
			{
				result = TextNative.ComputeTextHeight(MeshGenerationContextUtils.TextParams.GetTextNativeSettings(parms, scaling));
			}
			else
			{
				this.UpdatePreferredValues(parms);
				result = this.m_PreferredSize.y;
			}
			return result;
		}

		internal TextInfo Update(MeshGenerationContextUtils.TextParams parms, float pixelsPerPoint)
		{
			parms.rect = new Rect(Vector2.zero, parms.rect.size);
			int hashCode = parms.GetHashCode();
			bool flag = this.m_PreviousGenerationSettingsHash == hashCode;
			TextInfo textInfo;
			if (flag)
			{
				textInfo = this.textInfo;
			}
			else
			{
				TextHandle.UpdateGenerationSettingsCommon(parms, this.m_CurrentGenerationSettings);
				this.m_CurrentGenerationSettings.color = parms.fontColor;
				this.m_CurrentGenerationSettings.inverseYAxis = true;
				this.m_CurrentGenerationSettings.scale = pixelsPerPoint;
				this.m_CurrentGenerationSettings.overflowMode = parms.textOverflowMode;
				this.textInfo.isDirty = true;
				UnityEngine.TextCore.TextGenerator.GenerateText(this.m_CurrentGenerationSettings, this.textInfo);
				this.m_PreviousGenerationSettingsHash = hashCode;
				textInfo = this.textInfo;
			}
			return textInfo;
		}

		private void UpdatePreferredValues(MeshGenerationContextUtils.TextParams parms)
		{
			parms.rect = new Rect(Vector2.zero, parms.rect.size);
			int hashCode = parms.GetHashCode();
			bool flag = this.m_PreviousLayoutSettingsHash == hashCode;
			if (!flag)
			{
				TextHandle.UpdateGenerationSettingsCommon(parms, this.m_CurrentLayoutSettings);
				this.m_PreferredSize = UnityEngine.TextCore.TextGenerator.GetPreferredValues(this.m_CurrentLayoutSettings, this.textInfo);
				this.m_PreviousLayoutSettingsHash = hashCode;
			}
		}

		private static void UpdateGenerationSettingsCommon(MeshGenerationContextUtils.TextParams painterParams, UnityEngine.TextCore.TextGenerationSettings settings)
		{
			settings.fontAsset = TextHandle.GetFontAsset(painterParams.font);
			settings.material = settings.fontAsset.material;
			Rect rect = painterParams.rect;
			bool flag = float.IsNaN(rect.width);
			if (flag)
			{
				rect.width = painterParams.wordWrapWidth;
			}
			settings.screenRect = rect;
			settings.text = (string.IsNullOrEmpty(painterParams.text) ? " " : painterParams.text);
			settings.fontSize = (float)((painterParams.fontSize > 0) ? painterParams.fontSize : painterParams.font.fontSize);
			settings.fontStyle = TextGeneratorUtilities.LegacyStyleToNewStyle(painterParams.fontStyle);
			settings.textAlignment = TextGeneratorUtilities.LegacyAlignmentToNewAlignment(painterParams.anchor);
			settings.wordWrap = painterParams.wordWrap;
			settings.richText = false;
			settings.overflowMode = TextOverflowMode.Overflow;
		}

		public static float ComputeTextScaling(Matrix4x4 worldMatrix, float pixelsPerPoint)
		{
			Vector3 vector = new Vector3(worldMatrix.m00, worldMatrix.m10, worldMatrix.m20);
			Vector3 vector2 = new Vector3(worldMatrix.m01, worldMatrix.m11, worldMatrix.m21);
			float num = (vector.magnitude + vector2.magnitude) / 2f;
			return num * pixelsPerPoint;
		}
	}
}
