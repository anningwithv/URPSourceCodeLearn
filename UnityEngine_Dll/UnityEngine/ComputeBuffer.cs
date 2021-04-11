using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Shaders/ComputeShader.h"), NativeHeader("Runtime/Export/Shaders/ComputeShader.bindings.h"), UsedByNativeCode]
	public sealed class ComputeBuffer : IDisposable
	{
		internal IntPtr m_Ptr;

		private AtomicSafetyHandle m_Safety;

		public extern int count
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int stride
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		private extern ComputeBufferMode usage
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public string name
		{
			set
			{
				this.SetName(value);
			}
		}

		~ComputeBuffer()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				ComputeBuffer.DestroyBuffer(this);
			}
			else
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					Debug.LogWarning(string.Format("GarbageCollector disposing of ComputeBuffer allocated in {0} at line {1}. Please use ComputeBuffer.Release() or .Dispose() to manually release the buffer.", this.GetFileName(), this.GetLineNumber()));
				}
			}
			this.m_Ptr = IntPtr.Zero;
		}

		[FreeFunction("ComputeShader_Bindings::InitBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InitBuffer(int count, int stride, ComputeBufferType type, ComputeBufferMode usage);

		[FreeFunction("ComputeShader_Bindings::DestroyBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyBuffer(ComputeBuffer buf);

		public ComputeBuffer(int count, int stride) : this(count, stride, ComputeBufferType.Default, ComputeBufferMode.Immutable, 3)
		{
		}

		public ComputeBuffer(int count, int stride, ComputeBufferType type) : this(count, stride, type, ComputeBufferMode.Immutable, 3)
		{
		}

		public ComputeBuffer(int count, int stride, ComputeBufferType type, ComputeBufferMode usage) : this(count, stride, type, usage, 3)
		{
		}

		internal ComputeBuffer(int count, int stride, ComputeBufferType type, ComputeBufferMode usage, int stackDepth)
		{
			bool flag = count <= 0;
			if (flag)
			{
				throw new ArgumentException("Attempting to create a zero length compute buffer", "count");
			}
			bool flag2 = stride <= 0;
			if (flag2)
			{
				throw new ArgumentException("Attempting to create a compute buffer with a negative or null stride", "stride");
			}
			this.m_Ptr = ComputeBuffer.InitBuffer(count, stride, type, usage);
			this.SaveCallstack(stackDepth);
		}

		public void Release()
		{
			this.Dispose();
		}

		public bool IsValid()
		{
			return this.m_Ptr != IntPtr.Zero;
		}

		[SecuritySafeCritical]
		public void SetData(Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to ComputeBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalSetData(data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
		}

		[SecuritySafeCritical]
		public void SetData<T>(List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to ComputeBuffer.SetData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			this.InternalSetData(NoAllocHelpers.ExtractArrayFromList(data), 0, 0, NoAllocHelpers.SafeLength<T>(data), Marshal.SizeOf(typeof(T)));
		}

		[SecuritySafeCritical]
		public void SetData<T>(NativeArray<T> data) where T : struct
		{
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>());
		}

		[SecuritySafeCritical]
		public void SetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to ComputeBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", managedBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalSetData(data, managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		[SecuritySafeCritical]
		public void SetData<T>(List<T> data, int managedBufferStartIndex, int computeBufferStartIndex, int count) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to ComputeBuffer.SetData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag3 = managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", managedBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalSetData(NoAllocHelpers.ExtractArrayFromList(data), managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(typeof(T)));
		}

		[SecuritySafeCritical]
		public void SetData<T>(NativeArray<T> data, int nativeBufferStartIndex, int computeBufferStartIndex, int count) where T : struct
		{
			bool flag = nativeBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), nativeBufferStartIndex, computeBufferStartIndex, count, UnsafeUtility.SizeOf<T>());
		}

		[SecurityCritical, FreeFunction(Name = "ComputeShader_Bindings::InternalSetNativeData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetNativeData(IntPtr data, int nativeBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		[SecurityCritical, FreeFunction(Name = "ComputeShader_Bindings::InternalSetData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		[SecurityCritical]
		public void GetData(Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to ComputeBuffer.GetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalGetData(data, 0, 0, data.Length, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		[SecurityCritical]
		public void GetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to ComputeBuffer.GetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count argument (managedBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", managedBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalGetData(data, managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		[SecurityCritical, FreeFunction(Name = "ComputeShader_Bindings::InternalGetData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalGetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* BeginBufferWrite(int offset = 0, int size = 0);

		public unsafe NativeArray<T> BeginWrite<T>(int computeBufferStartIndex, int count) where T : struct
		{
			bool flag = this.usage != ComputeBufferMode.SubUpdates;
			if (flag)
			{
				throw new ArgumentException("ComputeBuffer must be created with usage mode ComputeBufferMode.SubUpdates to be able to be mapped with BeginWrite");
			}
			int num = UnsafeUtility.SizeOf<T>();
			bool flag2 = computeBufferStartIndex < 0 || count < 0 || (computeBufferStartIndex + count) * num > this.count * this.stride;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (computeBufferStartIndex:{0} count:{1} elementSize:{2}, this.count:{3}, this.stride{4})", new object[]
				{
					computeBufferStartIndex,
					count,
					num,
					this.count,
					this.stride
				}));
			}
			void* dataPointer = this.BeginBufferWrite(computeBufferStartIndex * num, count * num);
			NativeArray<T> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(dataPointer, count, Allocator.Invalid);
			this.m_Safety = AtomicSafetyHandle.Create();
			AtomicSafetyHandle.SetAllowSecondaryVersionWriting(this.m_Safety, true);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, this.m_Safety);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void EndBufferWrite(int bytesWritten = 0);

		public void EndWrite<T>(int countWritten) where T : struct
		{
			try
			{
				AtomicSafetyHandle.CheckExistsAndThrow(this.m_Safety);
				AtomicSafetyHandle.Release(this.m_Safety);
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException("ComputeBuffer.EndWrite was called without matching ComputeBuffer.BeginWrite", innerException);
			}
			bool flag = countWritten < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (countWritten:{0})", countWritten));
			}
			int num = UnsafeUtility.SizeOf<T>();
			this.EndBufferWrite(countWritten * num);
		}

		[FreeFunction(Name = "ComputeShader_Bindings::SetName", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetName(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetCounterValue(uint counterValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CopyCount(ComputeBuffer src, ComputeBuffer dst, int dstOffsetBytes);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetNativeBufferPtr();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string GetFileName();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetLineNumber();

		internal void SaveCallstack(int stackDepth)
		{
			StackFrame stackFrame = new StackFrame(stackDepth, true);
			this.SaveCallstack_Internal(stackFrame.GetFileName(), stackFrame.GetFileLineNumber());
		}

		[NativeName("SetAllocationData")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SaveCallstack_Internal(string fileName, int lineNumber);
	}
}
