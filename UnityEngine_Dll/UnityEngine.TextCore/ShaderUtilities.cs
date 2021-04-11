using System;
using System.Linq;

namespace UnityEngine.TextCore
{
	internal static class ShaderUtilities
	{
		public static int ID_MainTex;

		public static int ID_FaceTex;

		public static int ID_FaceColor;

		public static int ID_FaceDilate;

		public static int ID_Shininess;

		public static int ID_UnderlayColor;

		public static int ID_UnderlayOffsetX;

		public static int ID_UnderlayOffsetY;

		public static int ID_UnderlayDilate;

		public static int ID_UnderlaySoftness;

		public static int ID_WeightNormal;

		public static int ID_WeightBold;

		public static int ID_OutlineTex;

		public static int ID_OutlineWidth;

		public static int ID_OutlineSoftness;

		public static int ID_OutlineColor;

		public static int ID_GradientScale;

		public static int ID_ScaleX;

		public static int ID_ScaleY;

		public static int ID_PerspectiveFilter;

		public static int ID_TextureWidth;

		public static int ID_TextureHeight;

		public static int ID_BevelAmount;

		public static int ID_GlowColor;

		public static int ID_GlowOffset;

		public static int ID_GlowPower;

		public static int ID_GlowOuter;

		public static int ID_LightAngle;

		public static int ID_EnvMap;

		public static int ID_EnvMatrix;

		public static int ID_EnvMatrixRotation;

		public static int ID_MaskCoord;

		public static int ID_ClipRect;

		public static int ID_MaskSoftnessX;

		public static int ID_MaskSoftnessY;

		public static int ID_VertexOffsetX;

		public static int ID_VertexOffsetY;

		public static int ID_UseClipRect;

		public static int ID_StencilID;

		public static int ID_StencilOp;

		public static int ID_StencilComp;

		public static int ID_StencilReadMask;

		public static int ID_StencilWriteMask;

		public static int ID_ShaderFlags;

		public static int ID_ScaleRatio_A;

		public static int ID_ScaleRatio_B;

		public static int ID_ScaleRatio_C;

		public static string Keyword_Bevel;

		public static string Keyword_Glow;

		public static string Keyword_Underlay;

		public static string Keyword_Ratios;

		public static string Keyword_MASK_SOFT;

		public static string Keyword_MASK_HARD;

		public static string Keyword_MASK_TEX;

		public static string Keyword_Outline;

		public static string ShaderTag_ZTestMode;

		public static string ShaderTag_CullMode;

		private static float m_clamp;

		public static bool isInitialized;

		private static Shader k_ShaderRef_MobileSDF;

		private static Shader k_ShaderRef_MobileBitmap;

		internal static Shader ShaderRef_MobileSDF
		{
			get
			{
				bool flag = ShaderUtilities.k_ShaderRef_MobileSDF == null;
				if (flag)
				{
					ShaderUtilities.k_ShaderRef_MobileSDF = Shader.Find("Hidden/TextCore/Distance Field SSD");
				}
				return ShaderUtilities.k_ShaderRef_MobileSDF;
			}
		}

		internal static Shader ShaderRef_MobileBitmap
		{
			get
			{
				bool flag = ShaderUtilities.k_ShaderRef_MobileBitmap == null;
				if (flag)
				{
					ShaderUtilities.k_ShaderRef_MobileBitmap = Shader.Find("Hidden/Internal-GUITextureClipText");
				}
				return ShaderUtilities.k_ShaderRef_MobileBitmap;
			}
		}

		static ShaderUtilities()
		{
			ShaderUtilities.Keyword_Bevel = "BEVEL_ON";
			ShaderUtilities.Keyword_Glow = "GLOW_ON";
			ShaderUtilities.Keyword_Underlay = "UNDERLAY_ON";
			ShaderUtilities.Keyword_Ratios = "RATIOS_OFF";
			ShaderUtilities.Keyword_MASK_SOFT = "MASK_SOFT";
			ShaderUtilities.Keyword_MASK_HARD = "MASK_HARD";
			ShaderUtilities.Keyword_MASK_TEX = "MASK_TEX";
			ShaderUtilities.Keyword_Outline = "OUTLINE_ON";
			ShaderUtilities.ShaderTag_ZTestMode = "unity_GUIZTestMode";
			ShaderUtilities.ShaderTag_CullMode = "_CullMode";
			ShaderUtilities.m_clamp = 1f;
			ShaderUtilities.GetShaderPropertyIDs();
		}

		public static void GetShaderPropertyIDs()
		{
			bool flag = !ShaderUtilities.isInitialized;
			if (flag)
			{
				ShaderUtilities.isInitialized = true;
				ShaderUtilities.ID_MainTex = Shader.PropertyToID("_MainTex");
				ShaderUtilities.ID_FaceTex = Shader.PropertyToID("_FaceTex");
				ShaderUtilities.ID_FaceColor = Shader.PropertyToID("_FaceColor");
				ShaderUtilities.ID_FaceDilate = Shader.PropertyToID("_FaceDilate");
				ShaderUtilities.ID_Shininess = Shader.PropertyToID("_FaceShininess");
				ShaderUtilities.ID_UnderlayColor = Shader.PropertyToID("_UnderlayColor");
				ShaderUtilities.ID_UnderlayOffsetX = Shader.PropertyToID("_UnderlayOffsetX");
				ShaderUtilities.ID_UnderlayOffsetY = Shader.PropertyToID("_UnderlayOffsetY");
				ShaderUtilities.ID_UnderlayDilate = Shader.PropertyToID("_UnderlayDilate");
				ShaderUtilities.ID_UnderlaySoftness = Shader.PropertyToID("_UnderlaySoftness");
				ShaderUtilities.ID_WeightNormal = Shader.PropertyToID("_WeightNormal");
				ShaderUtilities.ID_WeightBold = Shader.PropertyToID("_WeightBold");
				ShaderUtilities.ID_OutlineTex = Shader.PropertyToID("_OutlineTex");
				ShaderUtilities.ID_OutlineWidth = Shader.PropertyToID("_OutlineWidth");
				ShaderUtilities.ID_OutlineSoftness = Shader.PropertyToID("_OutlineSoftness");
				ShaderUtilities.ID_OutlineColor = Shader.PropertyToID("_OutlineColor");
				ShaderUtilities.ID_GradientScale = Shader.PropertyToID("_GradientScale");
				ShaderUtilities.ID_ScaleX = Shader.PropertyToID("_ScaleX");
				ShaderUtilities.ID_ScaleY = Shader.PropertyToID("_ScaleY");
				ShaderUtilities.ID_PerspectiveFilter = Shader.PropertyToID("_PerspectiveFilter");
				ShaderUtilities.ID_TextureWidth = Shader.PropertyToID("_TextureWidth");
				ShaderUtilities.ID_TextureHeight = Shader.PropertyToID("_TextureHeight");
				ShaderUtilities.ID_BevelAmount = Shader.PropertyToID("_Bevel");
				ShaderUtilities.ID_LightAngle = Shader.PropertyToID("_LightAngle");
				ShaderUtilities.ID_EnvMap = Shader.PropertyToID("_Cube");
				ShaderUtilities.ID_EnvMatrix = Shader.PropertyToID("_EnvMatrix");
				ShaderUtilities.ID_EnvMatrixRotation = Shader.PropertyToID("_EnvMatrixRotation");
				ShaderUtilities.ID_GlowColor = Shader.PropertyToID("_GlowColor");
				ShaderUtilities.ID_GlowOffset = Shader.PropertyToID("_GlowOffset");
				ShaderUtilities.ID_GlowPower = Shader.PropertyToID("_GlowPower");
				ShaderUtilities.ID_GlowOuter = Shader.PropertyToID("_GlowOuter");
				ShaderUtilities.ID_MaskCoord = Shader.PropertyToID("_MaskCoord");
				ShaderUtilities.ID_ClipRect = Shader.PropertyToID("_ClipRect");
				ShaderUtilities.ID_UseClipRect = Shader.PropertyToID("_UseClipRect");
				ShaderUtilities.ID_MaskSoftnessX = Shader.PropertyToID("_MaskSoftnessX");
				ShaderUtilities.ID_MaskSoftnessY = Shader.PropertyToID("_MaskSoftnessY");
				ShaderUtilities.ID_VertexOffsetX = Shader.PropertyToID("_VertexOffsetX");
				ShaderUtilities.ID_VertexOffsetY = Shader.PropertyToID("_VertexOffsetY");
				ShaderUtilities.ID_StencilID = Shader.PropertyToID("_Stencil");
				ShaderUtilities.ID_StencilOp = Shader.PropertyToID("_StencilOp");
				ShaderUtilities.ID_StencilComp = Shader.PropertyToID("_StencilComp");
				ShaderUtilities.ID_StencilReadMask = Shader.PropertyToID("_StencilReadMask");
				ShaderUtilities.ID_StencilWriteMask = Shader.PropertyToID("_StencilWriteMask");
				ShaderUtilities.ID_ShaderFlags = Shader.PropertyToID("_ShaderFlags");
				ShaderUtilities.ID_ScaleRatio_A = Shader.PropertyToID("_ScaleRatioA");
				ShaderUtilities.ID_ScaleRatio_B = Shader.PropertyToID("_ScaleRatioB");
				ShaderUtilities.ID_ScaleRatio_C = Shader.PropertyToID("_ScaleRatioC");
			}
		}

		public static void UpdateShaderRatios(Material mat)
		{
			bool flag = !mat.shaderKeywords.Contains(ShaderUtilities.Keyword_Ratios);
			float @float = mat.GetFloat(ShaderUtilities.ID_GradientScale);
			float float2 = mat.GetFloat(ShaderUtilities.ID_FaceDilate);
			float float3 = mat.GetFloat(ShaderUtilities.ID_OutlineWidth);
			float float4 = mat.GetFloat(ShaderUtilities.ID_OutlineSoftness);
			float num = Mathf.Max(mat.GetFloat(ShaderUtilities.ID_WeightNormal), mat.GetFloat(ShaderUtilities.ID_WeightBold)) / 4f;
			float num2 = Mathf.Max(1f, num + float2 + float3 + float4);
			float value = flag ? ((@float - ShaderUtilities.m_clamp) / (@float * num2)) : 1f;
			mat.SetFloat(ShaderUtilities.ID_ScaleRatio_A, value);
			bool flag2 = mat.HasProperty(ShaderUtilities.ID_GlowOffset);
			if (flag2)
			{
				float float5 = mat.GetFloat(ShaderUtilities.ID_GlowOffset);
				float float6 = mat.GetFloat(ShaderUtilities.ID_GlowOuter);
				float num3 = (num + float2) * (@float - ShaderUtilities.m_clamp);
				num2 = Mathf.Max(1f, float5 + float6);
				float value2 = flag ? (Mathf.Max(0f, @float - ShaderUtilities.m_clamp - num3) / (@float * num2)) : 1f;
				mat.SetFloat(ShaderUtilities.ID_ScaleRatio_B, value2);
			}
			bool flag3 = mat.HasProperty(ShaderUtilities.ID_UnderlayOffsetX);
			if (flag3)
			{
				float float7 = mat.GetFloat(ShaderUtilities.ID_UnderlayOffsetX);
				float float8 = mat.GetFloat(ShaderUtilities.ID_UnderlayOffsetY);
				float float9 = mat.GetFloat(ShaderUtilities.ID_UnderlayDilate);
				float float10 = mat.GetFloat(ShaderUtilities.ID_UnderlaySoftness);
				float num4 = (num + float2) * (@float - ShaderUtilities.m_clamp);
				num2 = Mathf.Max(1f, Mathf.Max(Mathf.Abs(float7), Mathf.Abs(float8)) + float9 + float10);
				float value3 = flag ? (Mathf.Max(0f, @float - ShaderUtilities.m_clamp - num4) / (@float * num2)) : 1f;
				mat.SetFloat(ShaderUtilities.ID_ScaleRatio_C, value3);
			}
		}

		public static bool IsMaskingEnabled(Material material)
		{
			bool flag = material == null || !material.HasProperty(ShaderUtilities.ID_ClipRect);
			return !flag && (material.shaderKeywords.Contains(ShaderUtilities.Keyword_MASK_SOFT) || material.shaderKeywords.Contains(ShaderUtilities.Keyword_MASK_HARD) || material.shaderKeywords.Contains(ShaderUtilities.Keyword_MASK_TEX));
		}

		public static float GetPadding(Material material, bool enableExtraPadding, bool isBold)
		{
			bool flag = !ShaderUtilities.isInitialized;
			if (flag)
			{
				ShaderUtilities.GetShaderPropertyIDs();
			}
			bool flag2 = material == null;
			float result;
			if (flag2)
			{
				result = 0f;
			}
			else
			{
				int num = enableExtraPadding ? 4 : 0;
				bool flag3 = !material.HasProperty(ShaderUtilities.ID_GradientScale);
				if (flag3)
				{
					result = (float)num;
				}
				else
				{
					Vector4 vector = Vector4.zero;
					Vector4 zero = Vector4.zero;
					float num2 = 0f;
					float num3 = 0f;
					float num4 = 0f;
					float num5 = 0f;
					float num6 = 0f;
					float num7 = 0f;
					float num8 = 0f;
					float num9 = 0f;
					ShaderUtilities.UpdateShaderRatios(material);
					string[] shaderKeywords = material.shaderKeywords;
					bool flag4 = material.HasProperty(ShaderUtilities.ID_ScaleRatio_A);
					if (flag4)
					{
						num5 = material.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
					}
					bool flag5 = material.HasProperty(ShaderUtilities.ID_FaceDilate);
					if (flag5)
					{
						num2 = material.GetFloat(ShaderUtilities.ID_FaceDilate) * num5;
					}
					bool flag6 = material.HasProperty(ShaderUtilities.ID_OutlineSoftness);
					if (flag6)
					{
						num3 = material.GetFloat(ShaderUtilities.ID_OutlineSoftness) * num5;
					}
					bool flag7 = material.HasProperty(ShaderUtilities.ID_OutlineWidth);
					if (flag7)
					{
						num4 = material.GetFloat(ShaderUtilities.ID_OutlineWidth) * num5;
					}
					float num10 = num4 + num3 + num2;
					bool flag8 = material.HasProperty(ShaderUtilities.ID_GlowOffset) && shaderKeywords.Contains(ShaderUtilities.Keyword_Glow);
					if (flag8)
					{
						bool flag9 = material.HasProperty(ShaderUtilities.ID_ScaleRatio_B);
						if (flag9)
						{
							num6 = material.GetFloat(ShaderUtilities.ID_ScaleRatio_B);
						}
						num8 = material.GetFloat(ShaderUtilities.ID_GlowOffset) * num6;
						num9 = material.GetFloat(ShaderUtilities.ID_GlowOuter) * num6;
					}
					num10 = Mathf.Max(num10, num2 + num8 + num9);
					bool flag10 = material.HasProperty(ShaderUtilities.ID_UnderlaySoftness) && shaderKeywords.Contains(ShaderUtilities.Keyword_Underlay);
					if (flag10)
					{
						bool flag11 = material.HasProperty(ShaderUtilities.ID_ScaleRatio_C);
						if (flag11)
						{
							num7 = material.GetFloat(ShaderUtilities.ID_ScaleRatio_C);
						}
						float num11 = material.GetFloat(ShaderUtilities.ID_UnderlayOffsetX) * num7;
						float num12 = material.GetFloat(ShaderUtilities.ID_UnderlayOffsetY) * num7;
						float num13 = material.GetFloat(ShaderUtilities.ID_UnderlayDilate) * num7;
						float num14 = material.GetFloat(ShaderUtilities.ID_UnderlaySoftness) * num7;
						vector.x = Mathf.Max(vector.x, num2 + num13 + num14 - num11);
						vector.y = Mathf.Max(vector.y, num2 + num13 + num14 - num12);
						vector.z = Mathf.Max(vector.z, num2 + num13 + num14 + num11);
						vector.w = Mathf.Max(vector.w, num2 + num13 + num14 + num12);
					}
					vector.x = Mathf.Max(vector.x, num10);
					vector.y = Mathf.Max(vector.y, num10);
					vector.z = Mathf.Max(vector.z, num10);
					vector.w = Mathf.Max(vector.w, num10);
					vector.x += (float)num;
					vector.y += (float)num;
					vector.z += (float)num;
					vector.w += (float)num;
					vector.x = Mathf.Min(vector.x, 1f);
					vector.y = Mathf.Min(vector.y, 1f);
					vector.z = Mathf.Min(vector.z, 1f);
					vector.w = Mathf.Min(vector.w, 1f);
					zero.x = ((zero.x < vector.x) ? vector.x : zero.x);
					zero.y = ((zero.y < vector.y) ? vector.y : zero.y);
					zero.z = ((zero.z < vector.z) ? vector.z : zero.z);
					zero.w = ((zero.w < vector.w) ? vector.w : zero.w);
					float @float = material.GetFloat(ShaderUtilities.ID_GradientScale);
					vector *= @float;
					num10 = Mathf.Max(vector.x, vector.y);
					num10 = Mathf.Max(vector.z, num10);
					num10 = Mathf.Max(vector.w, num10);
					result = num10 + 0.5f;
				}
			}
			return result;
		}
	}
}
