using System;
using System.Diagnostics;

namespace Unity.Collections.LowLevel.Unsafe
{
	public static class NativeArrayUnsafeUtility
	{
		public static AtomicSafetyHandle GetAtomicSafetyHandle<T>(NativeArray<T> array) where T : struct
		{
			return array.m_Safety;
		}

		public static void SetAtomicSafetyHandle<T>(ref NativeArray<T> array, AtomicSafetyHandle safety) where T : struct
		{
			array.m_Safety = safety;
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckConvertArguments<T>(int length, Allocator allocator) where T : struct
		{
			bool flag = length < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("length", "Length must be >= 0");
			}
			NativeArray<T>.IsUnmanagedAndThrow();
		}

		public unsafe static NativeArray<T> ConvertExistingDataToNativeArray<T>(void* dataPointer, int length, Allocator allocator) where T : struct
		{
			NativeArrayUnsafeUtility.CheckConvertArguments<T>(length, allocator);
			return new NativeArray<T>
			{
				m_Buffer = dataPointer,
				m_Length = length,
				m_AllocatorLabel = allocator,
				m_MinIndex = 0,
				m_MaxIndex = length - 1
			};
		}

		public unsafe static void* GetUnsafePtr<T>(this NativeArray<T> nativeArray) where T : struct
		{
			AtomicSafetyHandle.CheckWriteAndThrow(nativeArray.m_Safety);
			return nativeArray.m_Buffer;
		}

		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeArray<T> nativeArray) where T : struct
		{
			AtomicSafetyHandle.CheckReadAndThrow(nativeArray.m_Safety);
			return nativeArray.m_Buffer;
		}

		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeArray<T>.ReadOnly nativeArray) where T : struct
		{
			AtomicSafetyHandle.CheckReadAndThrow(nativeArray.m_Safety);
			return nativeArray.m_Buffer;
		}

		public unsafe static void* GetUnsafeBufferPointerWithoutChecks<T>(NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}
	}
}
