using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Profiling;

namespace UnityEngine.TextCore.LowLevel
{
	[NativeHeader("Modules/TextCore/Native/FontEngine/FontEngine.h")]
	public sealed class FontEngine
	{
		private static Glyph[] s_Glyphs = new Glyph[16];

		private static uint[] s_GlyphIndexes_MarshallingArray_A;

		private static uint[] s_GlyphIndexes_MarshallingArray_B;

		private static GlyphMarshallingStruct[] s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[16];

		private static GlyphMarshallingStruct[] s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[16];

		private static GlyphRect[] s_FreeGlyphRects = new GlyphRect[16];

		private static GlyphRect[] s_UsedGlyphRects = new GlyphRect[16];

		private static GlyphPairAdjustmentRecord[] s_PairAdjustmentRecords_MarshallingArray;

		private static Dictionary<uint, Glyph> s_GlyphLookupDictionary = new Dictionary<uint, Glyph>();

		internal static extern bool isProcessingDone
		{
			[NativeMethod(Name = "TextCore::FontEngine::GetIsProcessingDone", IsFreeFunction = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static extern float generationProgress
		{
			[NativeMethod(Name = "TextCore::FontEngine::GetGenerationProgress", IsFreeFunction = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal FontEngine()
		{
		}

		public static FontEngineError InitializeFontEngine()
		{
			return (FontEngineError)FontEngine.InitializeFontEngine_Internal();
		}

		[NativeMethod(Name = "TextCore::FontEngine::InitFontEngine", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InitializeFontEngine_Internal();

		public static FontEngineError DestroyFontEngine()
		{
			return (FontEngineError)FontEngine.DestroyFontEngine_Internal();
		}

		[NativeMethod(Name = "TextCore::FontEngine::DestroyFontEngine", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int DestroyFontEngine_Internal();

		internal static void SendCancellationRequest()
		{
			FontEngine.SendCancellationRequest_Internal();
		}

		[NativeMethod(Name = "TextCore::FontEngine::SendCancellationRequest", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendCancellationRequest_Internal();

		public static FontEngineError LoadFontFace(string filePath)
		{
			return (FontEngineError)FontEngine.LoadFontFace_Internal(filePath);
		}

		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_Internal(string filePath);

		public static FontEngineError LoadFontFace(string filePath, int pointSize)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_Internal(filePath, pointSize);
		}

		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_Internal(string filePath, int pointSize);

		public static FontEngineError LoadFontFace(byte[] sourceFontFile)
		{
			bool flag = sourceFontFile.Length == 0;
			FontEngineError result;
			if (flag)
			{
				result = FontEngineError.Invalid_File;
			}
			else
			{
				result = (FontEngineError)FontEngine.LoadFontFace_FromSourceFontFile_Internal(sourceFontFile);
			}
			return result;
		}

		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_FromSourceFontFile_Internal(byte[] sourceFontFile);

		public static FontEngineError LoadFontFace(byte[] sourceFontFile, int pointSize)
		{
			bool flag = sourceFontFile.Length == 0;
			FontEngineError result;
			if (flag)
			{
				result = FontEngineError.Invalid_File;
			}
			else
			{
				result = (FontEngineError)FontEngine.LoadFontFace_With_Size_FromSourceFontFile_Internal(sourceFontFile, pointSize);
			}
			return result;
		}

		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_FromSourceFontFile_Internal(byte[] sourceFontFile, int pointSize);

		public static FontEngineError LoadFontFace(Font font)
		{
			return (FontEngineError)FontEngine.LoadFontFace_FromFont_Internal(font);
		}

		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_FromFont_Internal(Font font);

		public static FontEngineError LoadFontFace(Font font, int pointSize)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_FromFont_Internal(font, pointSize);
		}

		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_FromFont_Internal(Font font, int pointSize);

		public static FontEngineError SetFaceSize(int pointSize)
		{
			return (FontEngineError)FontEngine.SetFaceSize_Internal(pointSize);
		}

		[NativeMethod(Name = "TextCore::FontEngine::SetFaceSize", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SetFaceSize_Internal(int pointSize);

		public static FaceInfo GetFaceInfo()
		{
			FaceInfo result = default(FaceInfo);
			FontEngine.GetFaceInfo_Internal(ref result);
			return result;
		}

		[NativeMethod(Name = "TextCore::FontEngine::GetFaceInfo", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFaceInfo_Internal(ref FaceInfo faceInfo);

		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphIndex", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetGlyphIndex(uint unicode);

		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphIndex", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool TryGetGlyphIndex(uint unicode, out uint glyphIndex);

		internal static FontEngineError LoadGlyph(uint unicode, GlyphLoadFlags flags)
		{
			return (FontEngineError)FontEngine.LoadGlyph_Internal(unicode, flags);
		}

		[NativeMethod(Name = "TextCore::FontEngine::LoadGlyph", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadGlyph_Internal(uint unicode, GlyphLoadFlags loadFlags);

		public static bool TryGetGlyphWithUnicodeValue(uint unicode, GlyphLoadFlags flags, out Glyph glyph)
		{
			GlyphMarshallingStruct glyphStruct = default(GlyphMarshallingStruct);
			bool flag = FontEngine.TryGetGlyphWithUnicodeValue_Internal(unicode, flags, ref glyphStruct);
			bool result;
			if (flag)
			{
				glyph = new Glyph(glyphStruct);
				result = true;
			}
			else
			{
				glyph = null;
				result = false;
			}
			return result;
		}

		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphWithUnicodeValue", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetGlyphWithUnicodeValue_Internal(uint unicode, GlyphLoadFlags loadFlags, ref GlyphMarshallingStruct glyphStruct);

		public static bool TryGetGlyphWithIndexValue(uint glyphIndex, GlyphLoadFlags flags, out Glyph glyph)
		{
			GlyphMarshallingStruct glyphStruct = default(GlyphMarshallingStruct);
			bool flag = FontEngine.TryGetGlyphWithIndexValue_Internal(glyphIndex, flags, ref glyphStruct);
			bool result;
			if (flag)
			{
				glyph = new Glyph(glyphStruct);
				result = true;
			}
			else
			{
				glyph = null;
				result = false;
			}
			return result;
		}

		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphWithIndexValue", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetGlyphWithIndexValue_Internal(uint glyphIndex, GlyphLoadFlags loadFlags, ref GlyphMarshallingStruct glyphStruct);

		internal static bool TryPackGlyphInAtlas(Glyph glyph, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects)
		{
			GlyphMarshallingStruct glyphMarshallingStruct = new GlyphMarshallingStruct(glyph);
			int count = freeGlyphRects.Count;
			int count2 = usedGlyphRects.Count;
			int num = count + count2;
			bool flag = FontEngine.s_FreeGlyphRects.Length < num || FontEngine.s_UsedGlyphRects.Length < num;
			if (flag)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				FontEngine.s_FreeGlyphRects = new GlyphRect[num2];
				FontEngine.s_UsedGlyphRects = new GlyphRect[num2];
			}
			int num3 = Mathf.Max(count, count2);
			for (int i = 0; i < num3; i++)
			{
				bool flag2 = i < count;
				if (flag2)
				{
					FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				bool flag3 = i < count2;
				if (flag3)
				{
					FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			bool flag4 = FontEngine.TryPackGlyphInAtlas_Internal(ref glyphMarshallingStruct, padding, packingMode, renderMode, width, height, FontEngine.s_FreeGlyphRects, ref count, FontEngine.s_UsedGlyphRects, ref count2);
			bool result;
			if (flag4)
			{
				glyph.glyphRect = glyphMarshallingStruct.glyphRect;
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				num3 = Mathf.Max(count, count2);
				for (int j = 0; j < num3; j++)
				{
					bool flag5 = j < count;
					if (flag5)
					{
						freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
					}
					bool flag6 = j < count2;
					if (flag6)
					{
						usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
					}
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		[NativeMethod(Name = "TextCore::FontEngine::TryPackGlyph", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryPackGlyphInAtlas_Internal(ref GlyphMarshallingStruct glyph, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount);

		internal static bool TryPackGlyphsInAtlas(List<Glyph> glyphsToAdd, List<Glyph> glyphsAdded, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects)
		{
			int count = glyphsToAdd.Count;
			int count2 = glyphsAdded.Count;
			int count3 = freeGlyphRects.Count;
			int count4 = usedGlyphRects.Count;
			int num = count + count2 + count3 + count4;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < num || FontEngine.s_GlyphMarshallingStruct_OUT.Length < num || FontEngine.s_FreeGlyphRects.Length < num || FontEngine.s_UsedGlyphRects.Length < num;
			if (flag)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num2];
				FontEngine.s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[num2];
				FontEngine.s_FreeGlyphRects = new GlyphRect[num2];
				FontEngine.s_UsedGlyphRects = new GlyphRect[num2];
			}
			FontEngine.s_GlyphLookupDictionary.Clear();
			for (int i = 0; i < num; i++)
			{
				bool flag2 = i < count;
				if (flag2)
				{
					GlyphMarshallingStruct glyphMarshallingStruct = new GlyphMarshallingStruct(glyphsToAdd[i]);
					FontEngine.s_GlyphMarshallingStruct_IN[i] = glyphMarshallingStruct;
					bool flag3 = !FontEngine.s_GlyphLookupDictionary.ContainsKey(glyphMarshallingStruct.index);
					if (flag3)
					{
						FontEngine.s_GlyphLookupDictionary.Add(glyphMarshallingStruct.index, glyphsToAdd[i]);
					}
				}
				bool flag4 = i < count2;
				if (flag4)
				{
					GlyphMarshallingStruct glyphMarshallingStruct2 = new GlyphMarshallingStruct(glyphsAdded[i]);
					FontEngine.s_GlyphMarshallingStruct_OUT[i] = glyphMarshallingStruct2;
					bool flag5 = !FontEngine.s_GlyphLookupDictionary.ContainsKey(glyphMarshallingStruct2.index);
					if (flag5)
					{
						FontEngine.s_GlyphLookupDictionary.Add(glyphMarshallingStruct2.index, glyphsAdded[i]);
					}
				}
				bool flag6 = i < count3;
				if (flag6)
				{
					FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				bool flag7 = i < count4;
				if (flag7)
				{
					FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			bool result = FontEngine.TryPackGlyphsInAtlas_Internal(FontEngine.s_GlyphMarshallingStruct_IN, ref count, FontEngine.s_GlyphMarshallingStruct_OUT, ref count2, padding, packingMode, renderMode, width, height, FontEngine.s_FreeGlyphRects, ref count3, FontEngine.s_UsedGlyphRects, ref count4);
			glyphsToAdd.Clear();
			glyphsAdded.Clear();
			freeGlyphRects.Clear();
			usedGlyphRects.Clear();
			for (int j = 0; j < num; j++)
			{
				bool flag8 = j < count;
				if (flag8)
				{
					GlyphMarshallingStruct glyphMarshallingStruct3 = FontEngine.s_GlyphMarshallingStruct_IN[j];
					Glyph glyph = FontEngine.s_GlyphLookupDictionary[glyphMarshallingStruct3.index];
					glyph.metrics = glyphMarshallingStruct3.metrics;
					glyph.glyphRect = glyphMarshallingStruct3.glyphRect;
					glyph.scale = glyphMarshallingStruct3.scale;
					glyph.atlasIndex = glyphMarshallingStruct3.atlasIndex;
					glyphsToAdd.Add(glyph);
				}
				bool flag9 = j < count2;
				if (flag9)
				{
					GlyphMarshallingStruct glyphMarshallingStruct4 = FontEngine.s_GlyphMarshallingStruct_OUT[j];
					Glyph glyph2 = FontEngine.s_GlyphLookupDictionary[glyphMarshallingStruct4.index];
					glyph2.metrics = glyphMarshallingStruct4.metrics;
					glyph2.glyphRect = glyphMarshallingStruct4.glyphRect;
					glyph2.scale = glyphMarshallingStruct4.scale;
					glyph2.atlasIndex = glyphMarshallingStruct4.atlasIndex;
					glyphsAdded.Add(glyph2);
				}
				bool flag10 = j < count3;
				if (flag10)
				{
					freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
				}
				bool flag11 = j < count4;
				if (flag11)
				{
					usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
				}
			}
			return result;
		}

		[NativeMethod(Name = "TextCore::FontEngine::TryPackGlyphs", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryPackGlyphsInAtlas_Internal([Out] GlyphMarshallingStruct[] glyphsToAdd, ref int glyphsToAddCount, [Out] GlyphMarshallingStruct[] glyphsAdded, ref int glyphsAddedCount, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount);

		internal static FontEngineError RenderGlyphToTexture(Glyph glyph, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			GlyphMarshallingStruct glyphStruct = new GlyphMarshallingStruct(glyph);
			return (FontEngineError)FontEngine.RenderGlyphToTexture_Internal(glyphStruct, padding, renderMode, texture);
		}

		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphToTexture", IsFreeFunction = true)]
		private static int RenderGlyphToTexture_Internal(GlyphMarshallingStruct glyphStruct, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			return FontEngine.RenderGlyphToTexture_Internal_Injected(ref glyphStruct, padding, renderMode, texture);
		}

		internal static FontEngineError RenderGlyphsToTexture(List<Glyph> glyphs, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			int count = glyphs.Count;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < count;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num];
			}
			for (int i = 0; i < count; i++)
			{
				FontEngine.s_GlyphMarshallingStruct_IN[i] = new GlyphMarshallingStruct(glyphs[i]);
			}
			return (FontEngineError)FontEngine.RenderGlyphsToTexture_Internal(FontEngine.s_GlyphMarshallingStruct_IN, count, padding, renderMode, texture);
		}

		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphsToTexture", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RenderGlyphsToTexture_Internal(GlyphMarshallingStruct[] glyphs, int glyphCount, int padding, GlyphRenderMode renderMode, Texture2D texture);

		internal static FontEngineError RenderGlyphsToTexture(List<Glyph> glyphs, int padding, GlyphRenderMode renderMode, byte[] texBuffer, int texWidth, int texHeight)
		{
			int count = glyphs.Count;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < count;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num];
			}
			for (int i = 0; i < count; i++)
			{
				FontEngine.s_GlyphMarshallingStruct_IN[i] = new GlyphMarshallingStruct(glyphs[i]);
			}
			return (FontEngineError)FontEngine.RenderGlyphsToTextureBuffer_Internal(FontEngine.s_GlyphMarshallingStruct_IN, count, padding, renderMode, texBuffer, texWidth, texHeight);
		}

		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphsToTextureBuffer", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RenderGlyphsToTextureBuffer_Internal(GlyphMarshallingStruct[] glyphs, int glyphCount, int padding, GlyphRenderMode renderMode, [Out] byte[] texBuffer, int texWidth, int texHeight);

		internal static FontEngineError RenderGlyphsToSharedTexture(List<Glyph> glyphs, int padding, GlyphRenderMode renderMode)
		{
			int count = glyphs.Count;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < count;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num];
			}
			for (int i = 0; i < count; i++)
			{
				FontEngine.s_GlyphMarshallingStruct_IN[i] = new GlyphMarshallingStruct(glyphs[i]);
			}
			return (FontEngineError)FontEngine.RenderGlyphsToSharedTexture_Internal(FontEngine.s_GlyphMarshallingStruct_IN, count, padding, renderMode);
		}

		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphsToSharedTexture", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RenderGlyphsToSharedTexture_Internal(GlyphMarshallingStruct[] glyphs, int glyphCount, int padding, GlyphRenderMode renderMode);

		[NativeMethod(Name = "TextCore::FontEngine::SetSharedTextureData", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetSharedTexture(Texture2D texture);

		[NativeMethod(Name = "TextCore::FontEngine::ReleaseSharedTextureData", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReleaseSharedTexture();

		internal static bool TryAddGlyphToTexture(uint glyphIndex, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture, out Glyph glyph)
		{
			int count = freeGlyphRects.Count;
			int count2 = usedGlyphRects.Count;
			int num = count + count2;
			bool flag = FontEngine.s_FreeGlyphRects.Length < num || FontEngine.s_UsedGlyphRects.Length < num;
			if (flag)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				FontEngine.s_FreeGlyphRects = new GlyphRect[num2];
				FontEngine.s_UsedGlyphRects = new GlyphRect[num2];
			}
			int num3 = Mathf.Max(count, count2);
			for (int i = 0; i < num3; i++)
			{
				bool flag2 = i < count;
				if (flag2)
				{
					FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				bool flag3 = i < count2;
				if (flag3)
				{
					FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			GlyphMarshallingStruct glyphStruct;
			bool flag4 = FontEngine.TryAddGlyphToTexture_Internal(glyphIndex, padding, packingMode, FontEngine.s_FreeGlyphRects, ref count, FontEngine.s_UsedGlyphRects, ref count2, renderMode, texture, out glyphStruct);
			bool result;
			if (flag4)
			{
				glyph = new Glyph(glyphStruct);
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				num3 = Mathf.Max(count, count2);
				for (int j = 0; j < num3; j++)
				{
					bool flag5 = j < count;
					if (flag5)
					{
						freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
					}
					bool flag6 = j < count2;
					if (flag6)
					{
						usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
					}
				}
				result = true;
			}
			else
			{
				glyph = null;
				result = false;
			}
			return result;
		}

		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryAddGlyphToTexture_Internal(uint glyphIndex, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture, out GlyphMarshallingStruct glyph);

		internal static bool TryAddGlyphsToTexture(List<Glyph> glyphsToAdd, List<Glyph> glyphsAdded, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture)
		{
			Profiler.BeginSample("FontEngine.TryAddGlyphsToTexture");
			int count = glyphsToAdd.Count;
			int num = 0;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < count || FontEngine.s_GlyphMarshallingStruct_OUT.Length < count;
			if (flag)
			{
				int newSize = Mathf.NextPowerOfTwo(count + 1);
				bool flag2 = FontEngine.s_GlyphMarshallingStruct_IN.Length < count;
				if (flag2)
				{
					Array.Resize<GlyphMarshallingStruct>(ref FontEngine.s_GlyphMarshallingStruct_IN, newSize);
				}
				bool flag3 = FontEngine.s_GlyphMarshallingStruct_OUT.Length < count;
				if (flag3)
				{
					Array.Resize<GlyphMarshallingStruct>(ref FontEngine.s_GlyphMarshallingStruct_OUT, newSize);
				}
			}
			int count2 = freeGlyphRects.Count;
			int count3 = usedGlyphRects.Count;
			int num2 = count2 + count3 + count;
			bool flag4 = FontEngine.s_FreeGlyphRects.Length < num2 || FontEngine.s_UsedGlyphRects.Length < num2;
			if (flag4)
			{
				int newSize2 = Mathf.NextPowerOfTwo(num2 + 1);
				bool flag5 = FontEngine.s_FreeGlyphRects.Length < num2;
				if (flag5)
				{
					Array.Resize<GlyphRect>(ref FontEngine.s_FreeGlyphRects, newSize2);
				}
				bool flag6 = FontEngine.s_UsedGlyphRects.Length < num2;
				if (flag6)
				{
					Array.Resize<GlyphRect>(ref FontEngine.s_UsedGlyphRects, newSize2);
				}
			}
			FontEngine.s_GlyphLookupDictionary.Clear();
			int num3 = 0;
			bool flag7 = true;
			while (flag7)
			{
				flag7 = false;
				bool flag8 = num3 < count;
				if (flag8)
				{
					Glyph glyph = glyphsToAdd[num3];
					FontEngine.s_GlyphMarshallingStruct_IN[num3] = new GlyphMarshallingStruct(glyph);
					FontEngine.s_GlyphLookupDictionary.Add(glyph.index, glyph);
					flag7 = true;
				}
				bool flag9 = num3 < count2;
				if (flag9)
				{
					FontEngine.s_FreeGlyphRects[num3] = freeGlyphRects[num3];
					flag7 = true;
				}
				bool flag10 = num3 < count3;
				if (flag10)
				{
					FontEngine.s_UsedGlyphRects[num3] = usedGlyphRects[num3];
					flag7 = true;
				}
				num3++;
			}
			bool result = FontEngine.TryAddGlyphsToTexture_Internal_MultiThread(FontEngine.s_GlyphMarshallingStruct_IN, ref count, FontEngine.s_GlyphMarshallingStruct_OUT, ref num, padding, packingMode, FontEngine.s_FreeGlyphRects, ref count2, FontEngine.s_UsedGlyphRects, ref count3, renderMode, texture);
			glyphsToAdd.Clear();
			glyphsAdded.Clear();
			freeGlyphRects.Clear();
			usedGlyphRects.Clear();
			num3 = 0;
			flag7 = true;
			while (flag7)
			{
				flag7 = false;
				bool flag11 = num3 < count;
				if (flag11)
				{
					uint index = FontEngine.s_GlyphMarshallingStruct_IN[num3].index;
					glyphsToAdd.Add(FontEngine.s_GlyphLookupDictionary[index]);
					flag7 = true;
				}
				bool flag12 = num3 < num;
				if (flag12)
				{
					uint index2 = FontEngine.s_GlyphMarshallingStruct_OUT[num3].index;
					Glyph glyph2 = FontEngine.s_GlyphLookupDictionary[index2];
					glyph2.atlasIndex = FontEngine.s_GlyphMarshallingStruct_OUT[num3].atlasIndex;
					glyph2.scale = FontEngine.s_GlyphMarshallingStruct_OUT[num3].scale;
					glyph2.glyphRect = FontEngine.s_GlyphMarshallingStruct_OUT[num3].glyphRect;
					glyph2.metrics = FontEngine.s_GlyphMarshallingStruct_OUT[num3].metrics;
					glyphsAdded.Add(glyph2);
					flag7 = true;
				}
				bool flag13 = num3 < count2;
				if (flag13)
				{
					freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[num3]);
					flag7 = true;
				}
				bool flag14 = num3 < count3;
				if (flag14)
				{
					usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[num3]);
					flag7 = true;
				}
				num3++;
			}
			Profiler.EndSample();
			return result;
		}

		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphsToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryAddGlyphsToTexture_Internal_MultiThread([Out] GlyphMarshallingStruct[] glyphsToAdd, ref int glyphsToAddCount, [Out] GlyphMarshallingStruct[] glyphsAdded, ref int glyphsAddedCount, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture);

		internal static bool TryAddGlyphsToTexture(List<uint> glyphIndexes, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture, out Glyph[] glyphs)
		{
			Profiler.BeginSample("FontEngine.TryAddGlyphsToTexture");
			glyphs = null;
			bool flag = glyphIndexes == null || glyphIndexes.Count == 0;
			bool result;
			if (flag)
			{
				Profiler.EndSample();
				result = false;
			}
			else
			{
				int count = glyphIndexes.Count;
				bool flag2 = FontEngine.s_GlyphIndexes_MarshallingArray_A == null || FontEngine.s_GlyphIndexes_MarshallingArray_A.Length < count;
				if (flag2)
				{
					bool flag3 = FontEngine.s_GlyphIndexes_MarshallingArray_A == null;
					if (flag3)
					{
						FontEngine.s_GlyphIndexes_MarshallingArray_A = new uint[count];
					}
					else
					{
						int num = Mathf.NextPowerOfTwo(count + 1);
						FontEngine.s_GlyphIndexes_MarshallingArray_A = new uint[num];
					}
				}
				int count2 = freeGlyphRects.Count;
				int count3 = usedGlyphRects.Count;
				int num2 = count2 + count3 + count;
				bool flag4 = FontEngine.s_FreeGlyphRects.Length < num2 || FontEngine.s_UsedGlyphRects.Length < num2;
				if (flag4)
				{
					int num3 = Mathf.NextPowerOfTwo(num2 + 1);
					FontEngine.s_FreeGlyphRects = new GlyphRect[num3];
					FontEngine.s_UsedGlyphRects = new GlyphRect[num3];
				}
				bool flag5 = FontEngine.s_GlyphMarshallingStruct_OUT.Length < count;
				if (flag5)
				{
					int num4 = Mathf.NextPowerOfTwo(count + 1);
					FontEngine.s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[num4];
				}
				int num5 = FontEngineUtilities.MaxValue(count2, count3, count);
				for (int i = 0; i < num5; i++)
				{
					bool flag6 = i < count;
					if (flag6)
					{
						FontEngine.s_GlyphIndexes_MarshallingArray_A[i] = glyphIndexes[i];
					}
					bool flag7 = i < count2;
					if (flag7)
					{
						FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
					}
					bool flag8 = i < count3;
					if (flag8)
					{
						FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
					}
				}
				bool flag9 = FontEngine.TryAddGlyphsToTexture_Internal(FontEngine.s_GlyphIndexes_MarshallingArray_A, padding, packingMode, FontEngine.s_FreeGlyphRects, ref count2, FontEngine.s_UsedGlyphRects, ref count3, renderMode, texture, FontEngine.s_GlyphMarshallingStruct_OUT, ref count);
				bool flag10 = FontEngine.s_Glyphs == null || FontEngine.s_Glyphs.Length <= count;
				if (flag10)
				{
					FontEngine.s_Glyphs = new Glyph[Mathf.NextPowerOfTwo(count + 1)];
				}
				FontEngine.s_Glyphs[count] = null;
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				num5 = FontEngineUtilities.MaxValue(count2, count3, count);
				for (int j = 0; j < num5; j++)
				{
					bool flag11 = j < count;
					if (flag11)
					{
						FontEngine.s_Glyphs[j] = new Glyph(FontEngine.s_GlyphMarshallingStruct_OUT[j]);
					}
					bool flag12 = j < count2;
					if (flag12)
					{
						freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
					}
					bool flag13 = j < count3;
					if (flag13)
					{
						usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
					}
				}
				glyphs = FontEngine.s_Glyphs;
				Profiler.EndSample();
				result = flag9;
			}
			return result;
		}

		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphsToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryAddGlyphsToTexture_Internal(uint[] glyphIndex, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture, [Out] GlyphMarshallingStruct[] glyphs, ref int glyphCount);

		[NativeMethod(Name = "TextCore::FontEngine::GetOpenTypeFontFeatures", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetOpenTypeFontFeatureTable();

		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentTable(uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, out num);
			bool flag = num == 0;
			GlyphPairAdjustmentRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetGlyphPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[num] = default(GlyphPairAdjustmentRecord);
				result = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return result;
		}

		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphPairAdjustmentTable", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGlyphPairAdjustmentTable_Internal(uint[] glyphIndexes, [Out] GlyphPairAdjustmentRecord[] glyphPairAdjustmentRecords, out int adjustmentRecordCount);

		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphPairAdjustmentRecord", IsFreeFunction = true)]
		internal static GlyphPairAdjustmentRecord GetGlyphPairAdjustmentRecord(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			GlyphPairAdjustmentRecord result;
			FontEngine.GetGlyphPairAdjustmentRecord_Injected(firstGlyphIndex, secondGlyphIndex, out result);
			return result;
		}

		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentRecords(List<uint> newGlyphIndexes, List<uint> allGlyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref newGlyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			FontEngine.GenericListToMarshallingArray<uint>(ref allGlyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_B);
			int num;
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_for_NewlyAddedGlyphIndexes(FontEngine.s_GlyphIndexes_MarshallingArray_A, FontEngine.s_GlyphIndexes_MarshallingArray_B, out num);
			bool flag = num == 0;
			GlyphPairAdjustmentRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetGlyphPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[num] = default(GlyphPairAdjustmentRecord);
				result = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return result;
		}

		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentRecords(List<uint> glyphIndexes, out int recordCount)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_from_GlyphIndexes(FontEngine.s_GlyphIndexes_MarshallingArray_A, out recordCount);
			bool flag = recordCount == 0;
			GlyphPairAdjustmentRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, recordCount);
				FontEngine.GetGlyphPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[recordCount] = default(GlyphPairAdjustmentRecord);
				result = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return result;
		}

		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentRecords(uint glyphIndex, out int recordCount)
		{
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_from_GlyphIndex(glyphIndex, out recordCount);
			bool flag = recordCount == 0;
			GlyphPairAdjustmentRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, recordCount);
				FontEngine.GetGlyphPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[recordCount] = default(GlyphPairAdjustmentRecord);
				result = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return result;
		}

		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, out int recordCount);

		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_for_NewlyAddedGlyphIndexes(uint[] newGlyphIndexes, uint[] allGlyphIndexes, out int recordCount);

		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_from_GlyphIndex(uint glyphIndex, out int recordCount);

		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphPairAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGlyphPairAdjustmentRecordsFromMarshallingArray([Out] GlyphPairAdjustmentRecord[] glyphPairAdjustmentRecords);

		private static void GenericListToMarshallingArray<T>(ref List<T> srcList, ref T[] dstArray)
		{
			int count = srcList.Count;
			bool flag = dstArray == null || dstArray.Length <= count;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				bool flag2 = dstArray == null;
				if (flag2)
				{
					dstArray = new T[num];
				}
				else
				{
					Array.Resize<T>(ref dstArray, num);
				}
			}
			for (int i = 0; i < count; i++)
			{
				dstArray[i] = srcList[i];
			}
			dstArray[count] = default(T);
		}

		private static void SetMarshallingArraySize<T>(ref T[] marshallingArray, int recordCount)
		{
			bool flag = marshallingArray == null || marshallingArray.Length <= recordCount;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(recordCount + 1);
				bool flag2 = marshallingArray == null;
				if (flag2)
				{
					marshallingArray = new T[num];
				}
				else
				{
					Array.Resize<T>(ref marshallingArray, num);
				}
			}
		}

		[NativeMethod(Name = "TextCore::FontEngine::ResetAtlasTexture", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ResetAtlasTexture(Texture2D texture);

		[NativeMethod(Name = "TextCore::FontEngine::RenderToTexture", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void RenderBufferToTexture(Texture2D srcTexture, int padding, GlyphRenderMode renderMode, Texture2D dstTexture);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RenderGlyphToTexture_Internal_Injected(ref GlyphMarshallingStruct glyphStruct, int padding, GlyphRenderMode renderMode, Texture2D texture);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlyphPairAdjustmentRecord_Injected(uint firstGlyphIndex, uint secondGlyphIndex, out GlyphPairAdjustmentRecord ret);
	}
}
