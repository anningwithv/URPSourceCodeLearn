using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[UsedByNativeCode]
	public struct ShaderKeywordSet
	{
		[CompilerGenerated, UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 56)]
		public struct <m_Bits>e__FixedBuffer
		{
			public uint FixedElementField;
		}

		private const int k_SizeInBits = 32;

		[FixedBuffer(typeof(uint), 14)]
		internal ShaderKeywordSet.<m_Bits>e__FixedBuffer m_Bits;

		private void ComputeSliceAndMask(ShaderKeyword keyword, out uint slice, out uint mask)
		{
			int index = keyword.index;
			slice = (uint)(index / 32);
			mask = 1u << index % 32;
		}

		public unsafe bool IsEnabled(ShaderKeyword keyword)
		{
			bool flag = !keyword.IsValid();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				uint num;
				uint num2;
				this.ComputeSliceAndMask(keyword, out num, out num2);
				uint* ptr = &this.m_Bits.FixedElementField;
				result = ((ptr[(ulong)num * 4uL / 4uL] & num2) > 0u);
			}
			return result;
		}

		public unsafe void Enable(ShaderKeyword keyword)
		{
			bool flag = !keyword.IsValid();
			if (!flag)
			{
				uint num;
				uint num2;
				this.ComputeSliceAndMask(keyword, out num, out num2);
				fixed (uint* ptr = &this.m_Bits.FixedElementField)
				{
					uint* ptr2 = ptr;
					ptr2[(ulong)num * 4uL / 4uL] |= num2;
				}
			}
		}

		public unsafe void Disable(ShaderKeyword keyword)
		{
			bool flag = !keyword.IsValid();
			if (!flag)
			{
				uint num;
				uint num2;
				this.ComputeSliceAndMask(keyword, out num, out num2);
				fixed (uint* ptr = &this.m_Bits.FixedElementField)
				{
					uint* ptr2 = ptr;
					ptr2[(ulong)num * 4uL / 4uL] &= ~num2;
				}
			}
		}

		public ShaderKeyword[] GetShaderKeywords()
		{
			ShaderKeyword[] array = new ShaderKeyword[448];
			int num = 0;
			for (int i = 0; i < 448; i++)
			{
				ShaderKeyword shaderKeyword = new ShaderKeyword(i);
				bool flag = this.IsEnabled(shaderKeyword);
				if (flag)
				{
					array[num] = shaderKeyword;
					num++;
				}
			}
			Array.Resize<ShaderKeyword>(ref array, num);
			return array;
		}
	}
}
