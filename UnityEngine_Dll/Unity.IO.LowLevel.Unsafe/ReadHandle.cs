using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	public struct ReadHandle : IDisposable
	{
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr ptr;

		internal int version;

		public JobHandle JobHandle
		{
			get
			{
				bool flag = !ReadHandle.IsReadHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("ReadHandle.JobHandle cannot be called after the ReadHandle has been disposed");
				}
				return ReadHandle.GetJobHandle(this);
			}
		}

		public ReadStatus Status
		{
			get
			{
				bool flag = !ReadHandle.IsReadHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("ReadHandle.Status cannot be called after the ReadHandle has been disposed");
				}
				return ReadHandle.GetReadStatus(this);
			}
		}

		public bool IsValid()
		{
			return ReadHandle.IsReadHandleValid(this);
		}

		public void Dispose()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.Dispose cannot be called twice on the same ReadHandle");
			}
			bool flag2 = this.Status == ReadStatus.InProgress;
			if (flag2)
			{
				throw new InvalidOperationException("ReadHandle.Dispose cannot be called until the read operation completes");
			}
			ReadHandle.ReleaseReadHandle(this);
		}

		[FreeFunction("AsyncReadManagerManaged::GetReadStatus", IsThreadSafe = true), ThreadAndSerializationSafe]
		private static ReadStatus GetReadStatus(ReadHandle handle)
		{
			return ReadHandle.GetReadStatus_Injected(ref handle);
		}

		[FreeFunction("AsyncReadManagerManaged::ReleaseReadHandle", IsThreadSafe = true), ThreadAndSerializationSafe]
		private static void ReleaseReadHandle(ReadHandle handle)
		{
			ReadHandle.ReleaseReadHandle_Injected(ref handle);
		}

		[FreeFunction("AsyncReadManagerManaged::IsReadHandleValid", IsThreadSafe = true), ThreadAndSerializationSafe]
		private static bool IsReadHandleValid(ReadHandle handle)
		{
			return ReadHandle.IsReadHandleValid_Injected(ref handle);
		}

		[FreeFunction("AsyncReadManagerManaged::GetJobHandle", IsThreadSafe = true), ThreadAndSerializationSafe]
		private static JobHandle GetJobHandle(ReadHandle handle)
		{
			JobHandle result;
			ReadHandle.GetJobHandle_Injected(ref handle, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ReadStatus GetReadStatus_Injected(ref ReadHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseReadHandle_Injected(ref ReadHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsReadHandleValid_Injected(ref ReadHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetJobHandle_Injected(ref ReadHandle handle, out JobHandle ret);
	}
}
