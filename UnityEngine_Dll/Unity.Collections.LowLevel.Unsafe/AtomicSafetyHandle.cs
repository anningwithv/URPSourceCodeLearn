using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Collections.LowLevel.Unsafe
{
	[NativeHeader("Runtime/Jobs/AtomicSafetyHandle.h"), NativeHeader("Runtime/Jobs/JobsDebugger.h"), UsedByNativeCode]
	public struct AtomicSafetyHandle
	{
		internal const int Read = 1;

		internal const int Write = 2;

		internal const int Dispose = 4;

		internal const int ReadCheck = -7;

		internal const int WriteCheck = -6;

		internal const int DisposeCheck = -4;

		internal const int ReadWriteDisposeCheck = -8;

		[NativeDisableUnsafePtrRestriction]
		internal IntPtr versionNode;

		internal int version;

		internal int staticSafetyId;

		[ThreadSafe]
		public static AtomicSafetyHandle Create()
		{
			AtomicSafetyHandle result;
			AtomicSafetyHandle.Create_Injected(out result);
			return result;
		}

		[ThreadSafe]
		public static AtomicSafetyHandle GetTempUnsafePtrSliceHandle()
		{
			AtomicSafetyHandle result;
			AtomicSafetyHandle.GetTempUnsafePtrSliceHandle_Injected(out result);
			return result;
		}

		[ThreadSafe]
		public static AtomicSafetyHandle GetTempMemoryHandle()
		{
			AtomicSafetyHandle result;
			AtomicSafetyHandle.GetTempMemoryHandle_Injected(out result);
			return result;
		}

		[ThreadSafe]
		public static bool IsTempMemoryHandle(AtomicSafetyHandle handle)
		{
			return AtomicSafetyHandle.IsTempMemoryHandle_Injected(ref handle);
		}

		[ThreadSafe]
		public static void Release(AtomicSafetyHandle handle)
		{
			AtomicSafetyHandle.Release_Injected(ref handle);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PrepareUndisposable(ref AtomicSafetyHandle handle);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UseSecondaryVersion(ref AtomicSafetyHandle handle);

		[ThreadSafe]
		public static void SetAllowSecondaryVersionWriting(AtomicSafetyHandle handle, bool allowWriting)
		{
			AtomicSafetyHandle.SetAllowSecondaryVersionWriting_Injected(ref handle, allowWriting);
		}

		[ThreadSafe]
		public static void SetBumpSecondaryVersionOnScheduleWrite(AtomicSafetyHandle handle, bool value)
		{
			AtomicSafetyHandle.SetBumpSecondaryVersionOnScheduleWrite_Injected(ref handle, value);
		}

		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true)]
		public static void SetAllowReadOrWriteAccess(AtomicSafetyHandle handle, bool allowReadWriteAccess)
		{
			AtomicSafetyHandle.SetAllowReadOrWriteAccess_Injected(ref handle, allowReadWriteAccess);
		}

		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true)]
		public static bool GetAllowReadOrWriteAccess(AtomicSafetyHandle handle)
		{
			return AtomicSafetyHandle.GetAllowReadOrWriteAccess_Injected(ref handle);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS"), ThreadSafe(ThrowsException = true)]
		public static void CheckWriteAndBumpSecondaryVersion(AtomicSafetyHandle handle)
		{
			AtomicSafetyHandle.CheckWriteAndBumpSecondaryVersion_Injected(ref handle);
		}

		[ThreadSafe]
		public static EnforceJobResult EnforceAllBufferJobsHaveCompleted(AtomicSafetyHandle handle)
		{
			return AtomicSafetyHandle.EnforceAllBufferJobsHaveCompleted_Injected(ref handle);
		}

		[ThreadSafe]
		public static EnforceJobResult EnforceAllBufferJobsHaveCompletedAndRelease(AtomicSafetyHandle handle)
		{
			return AtomicSafetyHandle.EnforceAllBufferJobsHaveCompletedAndRelease_Injected(ref handle);
		}

		[ThreadSafe]
		public static EnforceJobResult EnforceAllBufferJobsHaveCompletedAndDisableReadWrite(AtomicSafetyHandle handle)
		{
			return AtomicSafetyHandle.EnforceAllBufferJobsHaveCompletedAndDisableReadWrite_Injected(ref handle);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS"), ThreadSafe(ThrowsException = true)]
		internal static void CheckReadAndThrowNoEarlyOut(AtomicSafetyHandle handle)
		{
			AtomicSafetyHandle.CheckReadAndThrowNoEarlyOut_Injected(ref handle);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS"), ThreadSafe(ThrowsException = true)]
		internal static void CheckWriteAndThrowNoEarlyOut(AtomicSafetyHandle handle)
		{
			AtomicSafetyHandle.CheckWriteAndThrowNoEarlyOut_Injected(ref handle);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS"), ThreadSafe(ThrowsException = true)]
		public static void CheckDeallocateAndThrow(AtomicSafetyHandle handle)
		{
			AtomicSafetyHandle.CheckDeallocateAndThrow_Injected(ref handle);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS"), ThreadSafe(ThrowsException = true)]
		public static void CheckGetSecondaryDataPointerAndThrow(AtomicSafetyHandle handle)
		{
			AtomicSafetyHandle.CheckGetSecondaryDataPointerAndThrow_Injected(ref handle);
		}

		[ThreadSafe]
		public static int GetReaderArray(AtomicSafetyHandle handle, int maxCount, IntPtr output)
		{
			return AtomicSafetyHandle.GetReaderArray_Injected(ref handle, maxCount, output);
		}

		[ThreadSafe]
		public static JobHandle GetWriter(AtomicSafetyHandle handle)
		{
			JobHandle result;
			AtomicSafetyHandle.GetWriter_Injected(ref handle, out result);
			return result;
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		public unsafe static void CheckReadAndThrow(AtomicSafetyHandle handle)
		{
			int* ptr = (int*)((void*)handle.versionNode);
			bool flag = handle.version != (*ptr & -7);
			if (flag)
			{
				AtomicSafetyHandle.CheckReadAndThrowNoEarlyOut(handle);
			}
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		public unsafe static void CheckWriteAndThrow(AtomicSafetyHandle handle)
		{
			int* ptr = (int*)((void*)handle.versionNode);
			bool flag = handle.version != (*ptr & -6);
			if (flag)
			{
				AtomicSafetyHandle.CheckWriteAndThrowNoEarlyOut(handle);
			}
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		public unsafe static void CheckExistsAndThrow(AtomicSafetyHandle handle)
		{
			int* ptr = (int*)((void*)handle.versionNode);
			bool flag = handle.version != (*ptr & -8);
			if (flag)
			{
				throw new InvalidOperationException("The NativeArray has been deallocated, it is not allowed to access it");
			}
		}

		[ThreadSafe]
		public static string GetReaderName(AtomicSafetyHandle handle, int readerIndex)
		{
			return AtomicSafetyHandle.GetReaderName_Injected(ref handle, readerIndex);
		}

		[ThreadSafe]
		public static string GetWriterName(AtomicSafetyHandle handle)
		{
			return AtomicSafetyHandle.GetWriterName_Injected(ref handle);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int NewStaticSafetyId(byte* ownerTypeNameBytes, int byteCount);

		public unsafe static int NewStaticSafetyId<T>()
		{
			string s = typeof(T).ToString();
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			byte[] array;
			byte* ownerTypeNameBytes;
			if ((array = bytes) == null || array.Length == 0)
			{
				ownerTypeNameBytes = null;
			}
			else
			{
				ownerTypeNameBytes = &array[0];
			}
			return AtomicSafetyHandle.NewStaticSafetyId(ownerTypeNameBytes, bytes.Length);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS"), NativeThrows, ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void SetCustomErrorMessage(int staticSafetyId, AtomicSafetyErrorType errorType, byte* messageBytes, int byteCount);

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		public static void SetStaticSafetyId(ref AtomicSafetyHandle handle, int staticSafetyId)
		{
			handle.staticSafetyId = staticSafetyId;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Create_Injected(out AtomicSafetyHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTempUnsafePtrSliceHandle_Injected(out AtomicSafetyHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTempMemoryHandle_Injected(out AtomicSafetyHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTempMemoryHandle_Injected(ref AtomicSafetyHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Release_Injected(ref AtomicSafetyHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAllowSecondaryVersionWriting_Injected(ref AtomicSafetyHandle handle, bool allowWriting);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBumpSecondaryVersionOnScheduleWrite_Injected(ref AtomicSafetyHandle handle, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAllowReadOrWriteAccess_Injected(ref AtomicSafetyHandle handle, bool allowReadWriteAccess);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetAllowReadOrWriteAccess_Injected(ref AtomicSafetyHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CheckWriteAndBumpSecondaryVersion_Injected(ref AtomicSafetyHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EnforceJobResult EnforceAllBufferJobsHaveCompleted_Injected(ref AtomicSafetyHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EnforceJobResult EnforceAllBufferJobsHaveCompletedAndRelease_Injected(ref AtomicSafetyHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EnforceJobResult EnforceAllBufferJobsHaveCompletedAndDisableReadWrite_Injected(ref AtomicSafetyHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CheckReadAndThrowNoEarlyOut_Injected(ref AtomicSafetyHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CheckWriteAndThrowNoEarlyOut_Injected(ref AtomicSafetyHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CheckDeallocateAndThrow_Injected(ref AtomicSafetyHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CheckGetSecondaryDataPointerAndThrow_Injected(ref AtomicSafetyHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetReaderArray_Injected(ref AtomicSafetyHandle handle, int maxCount, IntPtr output);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetWriter_Injected(ref AtomicSafetyHandle handle, out JobHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetReaderName_Injected(ref AtomicSafetyHandle handle, int readerIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetWriterName_Injected(ref AtomicSafetyHandle handle);
	}
}
