using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.Rendering.HybridV2
{
	public class HybridV2ShaderReflection
	{
		[FreeFunction("ShaderScripting::GetDOTSInstancingCbuffersPointer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDOTSInstancingCbuffersPointer([NotNull("ArgumentNullException")] Shader shader, ref int cbufferCount);

		[FreeFunction("ShaderScripting::GetDOTSInstancingPropertiesPointer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDOTSInstancingPropertiesPointer([NotNull("ArgumentNullException")] Shader shader, ref int propertyCount);

		[FreeFunction("Shader::GetDOTSReflectionVersionNumber")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetDOTSReflectionVersionNumber();

		public unsafe static NativeArray<DOTSInstancingCbuffer> GetDOTSInstancingCbuffers(Shader shader)
		{
			bool flag = shader == null;
			NativeArray<DOTSInstancingCbuffer> result;
			if (flag)
			{
				result = default(NativeArray<DOTSInstancingCbuffer>);
			}
			else
			{
				int length = 0;
				IntPtr dOTSInstancingCbuffersPointer = HybridV2ShaderReflection.GetDOTSInstancingCbuffersPointer(shader, ref length);
				bool flag2 = dOTSInstancingCbuffersPointer == IntPtr.Zero;
				if (flag2)
				{
					result = default(NativeArray<DOTSInstancingCbuffer>);
				}
				else
				{
					NativeArray<DOTSInstancingCbuffer> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<DOTSInstancingCbuffer>((void*)dOTSInstancingCbuffersPointer, length, Allocator.Temp);
					AtomicSafetyHandle tempMemoryHandle = AtomicSafetyHandle.GetTempMemoryHandle();
					NativeArrayUnsafeUtility.SetAtomicSafetyHandle<DOTSInstancingCbuffer>(ref nativeArray, tempMemoryHandle);
					result = nativeArray;
				}
			}
			return result;
		}

		public unsafe static NativeArray<DOTSInstancingProperty> GetDOTSInstancingProperties(Shader shader)
		{
			bool flag = shader == null;
			NativeArray<DOTSInstancingProperty> result;
			if (flag)
			{
				result = default(NativeArray<DOTSInstancingProperty>);
			}
			else
			{
				int length = 0;
				IntPtr dOTSInstancingPropertiesPointer = HybridV2ShaderReflection.GetDOTSInstancingPropertiesPointer(shader, ref length);
				bool flag2 = dOTSInstancingPropertiesPointer == IntPtr.Zero;
				if (flag2)
				{
					result = default(NativeArray<DOTSInstancingProperty>);
				}
				else
				{
					NativeArray<DOTSInstancingProperty> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<DOTSInstancingProperty>((void*)dOTSInstancingPropertiesPointer, length, Allocator.Temp);
					AtomicSafetyHandle tempMemoryHandle = AtomicSafetyHandle.GetTempMemoryHandle();
					NativeArrayUnsafeUtility.SetAtomicSafetyHandle<DOTSInstancingProperty>(ref nativeArray, tempMemoryHandle);
					result = nativeArray;
				}
			}
			return result;
		}
	}
}
