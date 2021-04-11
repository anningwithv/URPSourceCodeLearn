using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[NativeHeader("Runtime/Graphics/AsyncGPUReadbackManaged.h"), UsedByNativeCode]
	internal struct AsyncRequestNativeArrayData
	{
		public unsafe void* nativeArrayBuffer;

		public long lengthInBytes;

		public AtomicSafetyHandle safetyHandle;

		public unsafe static AsyncRequestNativeArrayData CreateAndCheckAccess<T>(NativeArray<T> array) where T : struct
		{
			AsyncRequestNativeArrayData result = default(AsyncRequestNativeArrayData);
			result.nativeArrayBuffer = array.GetUnsafePtr<T>();
			result.lengthInBytes = (long)array.Length * (long)UnsafeUtility.SizeOf<T>();
			AtomicSafetyHandle atomicSafetyHandle = NativeArrayUnsafeUtility.GetAtomicSafetyHandle<T>(array);
			int* ptr = (int*)((void*)atomicSafetyHandle.versionNode);
			bool flag = atomicSafetyHandle.version != (*ptr & -6);
			if (flag)
			{
				AtomicSafetyHandle.CheckWriteAndThrowNoEarlyOut(atomicSafetyHandle);
			}
			result.safetyHandle = atomicSafetyHandle;
			return result;
		}

		public unsafe static AsyncRequestNativeArrayData CreateAndCheckAccess<T>(NativeSlice<T> array) where T : struct
		{
			AsyncRequestNativeArrayData result = default(AsyncRequestNativeArrayData);
			result.nativeArrayBuffer = array.GetUnsafePtr<T>();
			result.lengthInBytes = (long)array.Length * (long)UnsafeUtility.SizeOf<T>();
			AtomicSafetyHandle atomicSafetyHandle = NativeSliceUnsafeUtility.GetAtomicSafetyHandle<T>(array);
			int* ptr = (int*)((void*)atomicSafetyHandle.versionNode);
			bool flag = atomicSafetyHandle.version != (*ptr & -6);
			if (flag)
			{
				AtomicSafetyHandle.CheckWriteAndThrowNoEarlyOut(atomicSafetyHandle);
			}
			result.safetyHandle = atomicSafetyHandle;
			return result;
		}
	}
}
