using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[UsedByNativeCode]
	public struct PlatformKeywordSet
	{
		private const int k_SizeInBits = 64;

		internal ulong m_Bits;

		private ulong ComputeKeywordMask(BuiltinShaderDefine define)
		{
			return (ulong)(1L << (int)(define % (BuiltinShaderDefine)64 & BuiltinShaderDefine.SHADER_API_GLES30));
		}

		public bool IsEnabled(BuiltinShaderDefine define)
		{
			return (this.m_Bits & this.ComputeKeywordMask(define)) > 0uL;
		}

		public void Enable(BuiltinShaderDefine define)
		{
			this.m_Bits |= this.ComputeKeywordMask(define);
		}

		public void Disable(BuiltinShaderDefine define)
		{
			this.m_Bits &= ~this.ComputeKeywordMask(define);
		}
	}
}
