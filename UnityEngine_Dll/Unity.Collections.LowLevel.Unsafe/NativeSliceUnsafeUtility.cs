using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	public static class NativeSliceUnsafeUtility
	{
		public static AtomicSafetyHandle GetAtomicSafetyHandle<T>(NativeSlice<T> slice) where T : struct
		{
			return slice.m_Safety;
		}

		public static void SetAtomicSafetyHandle<T>(ref NativeSlice<T> slice, AtomicSafetyHandle safety) where T : struct
		{
			slice.m_Safety = safety;
		}

		public unsafe static NativeSlice<T> ConvertExistingDataToNativeSlice<T>(void* dataPointer, int stride, int length) where T : struct
		{
			bool flag = length < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid length of '{0}'. It must be greater than 0.", length), "length");
			}
			bool flag2 = stride < 0;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Invalid stride '{0}'. It must be greater than 0.", stride), "stride");
			}
			return new NativeSlice<T>
			{
				m_Stride = stride,
				m_Buffer = (byte*)dataPointer,
				m_Length = length,
				m_MinIndex = 0,
				m_MaxIndex = length - 1
			};
		}

		public unsafe static void* GetUnsafePtr<T>(this NativeSlice<T> nativeSlice) where T : struct
		{
			AtomicSafetyHandle.CheckWriteAndThrow(nativeSlice.m_Safety);
			return (void*)nativeSlice.m_Buffer;
		}

		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeSlice<T> nativeSlice) where T : struct
		{
			AtomicSafetyHandle.CheckReadAndThrow(nativeSlice.m_Safety);
			return (void*)nativeSlice.m_Buffer;
		}
	}
}
