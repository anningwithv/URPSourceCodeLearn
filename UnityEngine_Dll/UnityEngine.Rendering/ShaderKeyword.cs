using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h"), NativeHeader("Runtime/Shaders/ShaderKeywords.h"), UsedByNativeCode]
	public struct ShaderKeyword
	{
		internal const int k_MaxShaderKeywords = 448;

		private const int k_InvalidKeyword = -1;

		internal int m_KeywordIndex;

		public int index
		{
			get
			{
				return this.m_KeywordIndex;
			}
		}

		[FreeFunction("ShaderScripting::GetGlobalKeywordIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetGlobalKeywordIndex(string keyword);

		[FreeFunction("ShaderScripting::GetKeywordIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetKeywordIndex(Shader shader, string keyword);

		[FreeFunction("ShaderScripting::GetKeywordIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetComputeShaderKeywordIndex(ComputeShader shader, string keyword);

		[FreeFunction("ShaderScripting::GetGlobalKeywordName")]
		public static string GetGlobalKeywordName(ShaderKeyword index)
		{
			return ShaderKeyword.GetGlobalKeywordName_Injected(ref index);
		}

		[FreeFunction("ShaderScripting::GetGlobalKeywordType")]
		public static ShaderKeywordType GetGlobalKeywordType(ShaderKeyword index)
		{
			return ShaderKeyword.GetGlobalKeywordType_Injected(ref index);
		}

		[FreeFunction("ShaderScripting::IsKeywordLocal")]
		public static bool IsKeywordLocal(ShaderKeyword index)
		{
			return ShaderKeyword.IsKeywordLocal_Injected(ref index);
		}

		[FreeFunction("ShaderScripting::GetKeywordName")]
		public static string GetKeywordName(Shader shader, ShaderKeyword index)
		{
			return ShaderKeyword.GetKeywordName_Injected(shader, ref index);
		}

		[FreeFunction("ShaderScripting::GetKeywordType")]
		public static ShaderKeywordType GetKeywordType(Shader shader, ShaderKeyword index)
		{
			return ShaderKeyword.GetKeywordType_Injected(shader, ref index);
		}

		[FreeFunction("ShaderScripting::GetKeywordName")]
		internal static string GetComputeShaderKeywordName(ComputeShader shader, ShaderKeyword index)
		{
			return ShaderKeyword.GetComputeShaderKeywordName_Injected(shader, ref index);
		}

		[FreeFunction("ShaderScripting::GetKeywordType")]
		internal static ShaderKeywordType GetComputeShaderKeywordType(ComputeShader shader, ShaderKeyword index)
		{
			return ShaderKeyword.GetComputeShaderKeywordType_Injected(shader, ref index);
		}

		public static string GetKeywordName(ComputeShader shader, ShaderKeyword index)
		{
			return ShaderKeyword.GetComputeShaderKeywordName(shader, index);
		}

		public static ShaderKeywordType GetKeywordType(ComputeShader shader, ShaderKeyword index)
		{
			return ShaderKeyword.GetComputeShaderKeywordType(shader, index);
		}

		internal ShaderKeyword(int keywordIndex)
		{
			this.m_KeywordIndex = keywordIndex;
		}

		public ShaderKeyword(string keywordName)
		{
			this.m_KeywordIndex = ShaderKeyword.GetGlobalKeywordIndex(keywordName);
		}

		public ShaderKeyword(Shader shader, string keywordName)
		{
			this.m_KeywordIndex = ShaderKeyword.GetKeywordIndex(shader, keywordName);
		}

		public ShaderKeyword(ComputeShader shader, string keywordName)
		{
			this.m_KeywordIndex = ShaderKeyword.GetComputeShaderKeywordIndex(shader, keywordName);
		}

		public bool IsValid()
		{
			return this.m_KeywordIndex >= 0 && this.m_KeywordIndex < 448 && this.m_KeywordIndex != -1;
		}

		[Obsolete("GetKeywordType is deprecated. Use ShaderKeyword.GetGlobalKeywordType instead.")]
		public ShaderKeywordType GetKeywordType()
		{
			return ShaderKeyword.GetGlobalKeywordType(this);
		}

		[Obsolete("GetKeywordName is deprecated. Use ShaderKeyword.GetGlobalKeywordName instead.")]
		public string GetKeywordName()
		{
			return ShaderKeyword.GetGlobalKeywordName(this);
		}

		[Obsolete("GetName() has been deprecated. Use ShaderKeyword.GetGlobalKeywordName instead.")]
		public string GetName()
		{
			return this.GetKeywordName();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetGlobalKeywordName_Injected(ref ShaderKeyword index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ShaderKeywordType GetGlobalKeywordType_Injected(ref ShaderKeyword index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsKeywordLocal_Injected(ref ShaderKeyword index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetKeywordName_Injected(Shader shader, ref ShaderKeyword index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ShaderKeywordType GetKeywordType_Injected(Shader shader, ref ShaderKeyword index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetComputeShaderKeywordName_Injected(ComputeShader shader, ref ShaderKeyword index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ShaderKeywordType GetComputeShaderKeywordType_Injected(ComputeShader shader, ref ShaderKeyword index);
	}
}
