using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Internal;

namespace Unity.Collections
{
	[DebuggerDisplay("Length = {Length}"), DebuggerTypeProxy(typeof(NativeArrayDebugView<>)), NativeContainer, NativeContainerSupportsDeallocateOnJobCompletion, NativeContainerSupportsDeferredConvertListToArray, NativeContainerSupportsMinMaxWriteRestriction]
	public struct NativeArray<T> : IDisposable, IEnumerable<T>, IEnumerable, IEquatable<NativeArray<T>> where T : struct
	{
		[ExcludeFromDocs]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			private NativeArray<T> m_Array;

			private int m_Index;

			public T Current
			{
				get
				{
					return this.m_Array[this.m_Index];
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			public Enumerator(ref NativeArray<T> array)
			{
				this.m_Array = array;
				this.m_Index = -1;
			}

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_Array.Length;
			}

			public void Reset()
			{
				this.m_Index = -1;
			}
		}

		[DebuggerDisplay("Length = {Length}"), DebuggerTypeProxy(typeof(NativeArrayReadOnlyDebugView<>)), NativeContainer, NativeContainerIsReadOnly]
		public struct ReadOnly
		{
			[NativeDisableUnsafePtrRestriction]
			internal unsafe void* m_Buffer;

			internal int m_Length;

			internal AtomicSafetyHandle m_Safety;

			public int Length
			{
				get
				{
					return this.m_Length;
				}
			}

			public T this[int index]
			{
				get
				{
					this.CheckElementReadAccess(index);
					return UnsafeUtility.ReadArrayElement<T>(this.m_Buffer, index);
				}
			}

			internal unsafe ReadOnly(void* buffer, int length, ref AtomicSafetyHandle safety)
			{
				this.m_Buffer = buffer;
				this.m_Length = length;
				this.m_Safety = safety;
			}

			public void CopyTo(T[] array)
			{
				NativeArray<T>.Copy(this, array);
			}

			public void CopyTo(NativeArray<T> array)
			{
				NativeArray<T>.Copy(this, array);
			}

			public T[] ToArray()
			{
				T[] array = new T[this.m_Length];
				NativeArray<T>.Copy(this, array, this.m_Length);
				return array;
			}

			public NativeArray<U>.ReadOnly Reinterpret<U>() where U : struct
			{
				NativeArray<T>.CheckReinterpretSize<U>();
				return new NativeArray<U>.ReadOnly(this.m_Buffer, this.m_Length, ref this.m_Safety);
			}

			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private unsafe void CheckElementReadAccess(int index)
			{
				bool flag = index < 0 || index >= this.m_Length;
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("Index {0} is out of range (must be between 0 and {1}).", index, this.m_Length - 1));
				}
				int* ptr = (int*)((void*)this.m_Safety.versionNode);
				bool flag2 = this.m_Safety.version != (*ptr & -7);
				if (flag2)
				{
					AtomicSafetyHandle.CheckReadAndThrowNoEarlyOut(this.m_Safety);
				}
			}
		}

		[NativeDisableUnsafePtrRestriction]
		internal unsafe void* m_Buffer;

		internal int m_Length;

		internal int m_MinIndex;

		internal int m_MaxIndex;

		internal AtomicSafetyHandle m_Safety;

		[NativeSetClassTypeToNullOnSchedule]
		internal DisposeSentinel m_DisposeSentinel;

		private static int s_staticSafetyId;

		internal Allocator m_AllocatorLabel;

		public int Length
		{
			get
			{
				return this.m_Length;
			}
		}

		public T this[int index]
		{
			get
			{
				this.CheckElementReadAccess(index);
				return UnsafeUtility.ReadArrayElement<T>(this.m_Buffer, index);
			}
			[WriteAccessRequired]
			set
			{
				this.CheckElementWriteAccess(index);
				UnsafeUtility.WriteArrayElement<T>(this.m_Buffer, index, value);
			}
		}

		public bool IsCreated
		{
			get
			{
				return this.m_Buffer != null;
			}
		}

		[BurstDiscard]
		private static void InitStaticSafetyId(ref AtomicSafetyHandle handle)
		{
			bool flag = NativeArray<T>.s_staticSafetyId == 0;
			if (flag)
			{
				NativeArray<T>.s_staticSafetyId = AtomicSafetyHandle.NewStaticSafetyId<NativeArray<T>>();
			}
			AtomicSafetyHandle.SetStaticSafetyId(ref handle, NativeArray<T>.s_staticSafetyId);
		}

		public NativeArray(int length, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			NativeArray<T>.Allocate(length, allocator, out this);
			bool flag = (options & NativeArrayOptions.ClearMemory) == NativeArrayOptions.ClearMemory;
			if (flag)
			{
				UnsafeUtility.MemClear(this.m_Buffer, (long)this.Length * (long)UnsafeUtility.SizeOf<T>());
			}
		}

		public NativeArray(T[] array, Allocator allocator)
		{
			bool flag = array == null;
			if (flag)
			{
				throw new ArgumentNullException("array");
			}
			NativeArray<T>.Allocate(array.Length, allocator, out this);
			NativeArray<T>.Copy(array, this);
		}

		public NativeArray(NativeArray<T> array, Allocator allocator)
		{
			AtomicSafetyHandle.CheckReadAndThrow(array.m_Safety);
			NativeArray<T>.Allocate(array.Length, allocator, out this);
			NativeArray<T>.Copy(array, 0, this, 0, array.Length);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckAllocateArguments(int length, Allocator allocator, long totalSize)
		{
			bool flag = allocator <= Allocator.None;
			if (flag)
			{
				throw new ArgumentException("Allocator must be Temp, TempJob or Persistent", "allocator");
			}
			bool flag2 = length < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length", "Length must be >= 0");
			}
			NativeArray<T>.IsUnmanagedAndThrow();
		}

		private static void Allocate(int length, Allocator allocator, out NativeArray<T> array)
		{
			long num = (long)UnsafeUtility.SizeOf<T>() * (long)length;
			NativeArray<T>.CheckAllocateArguments(length, allocator, num);
			array = default(NativeArray<T>);
			array.m_Buffer = UnsafeUtility.Malloc(num, UnsafeUtility.AlignOf<T>(), allocator);
			array.m_Length = length;
			array.m_AllocatorLabel = allocator;
			array.m_MinIndex = 0;
			array.m_MaxIndex = length - 1;
			DisposeSentinel.Create(out array.m_Safety, out array.m_DisposeSentinel, 1, allocator);
			NativeArray<T>.InitStaticSafetyId(ref array.m_Safety);
		}

		[BurstDiscard]
		internal static void IsUnmanagedAndThrow()
		{
			bool flag = !UnsafeUtility.IsValidNativeContainerElementType<T>();
			if (flag)
			{
				throw new InvalidOperationException(string.Format("{0} used in NativeArray<{1}> must be unmanaged (contain no managed types) and cannot itself be a native container type.", typeof(T), typeof(T)));
			}
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private unsafe void CheckElementReadAccess(int index)
		{
			bool flag = index < this.m_MinIndex || index > this.m_MaxIndex;
			if (flag)
			{
				this.FailOutOfRangeError(index);
			}
			int* ptr = (int*)((void*)this.m_Safety.versionNode);
			bool flag2 = this.m_Safety.version != (*ptr & -7);
			if (flag2)
			{
				AtomicSafetyHandle.CheckReadAndThrowNoEarlyOut(this.m_Safety);
			}
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private unsafe void CheckElementWriteAccess(int index)
		{
			bool flag = index < this.m_MinIndex || index > this.m_MaxIndex;
			if (flag)
			{
				this.FailOutOfRangeError(index);
			}
			int* ptr = (int*)((void*)this.m_Safety.versionNode);
			bool flag2 = this.m_Safety.version != (*ptr & -6);
			if (flag2)
			{
				AtomicSafetyHandle.CheckWriteAndThrowNoEarlyOut(this.m_Safety);
			}
		}

		[WriteAccessRequired]
		public void Dispose()
		{
			bool flag = this.m_Buffer == null;
			if (flag)
			{
				throw new ObjectDisposedException("The NativeArray is already disposed.");
			}
			bool flag2 = this.m_AllocatorLabel == Allocator.Invalid;
			if (flag2)
			{
				throw new InvalidOperationException("The NativeArray can not be Disposed because it was not allocated with a valid allocator.");
			}
			bool flag3 = this.m_AllocatorLabel > Allocator.None;
			if (flag3)
			{
				DisposeSentinel.Dispose(ref this.m_Safety, ref this.m_DisposeSentinel);
				UnsafeUtility.Free(this.m_Buffer, this.m_AllocatorLabel);
				this.m_AllocatorLabel = Allocator.Invalid;
			}
			this.m_Buffer = null;
			this.m_Length = 0;
		}

		public JobHandle Dispose(JobHandle inputDeps)
		{
			bool flag = this.m_AllocatorLabel == Allocator.Invalid;
			if (flag)
			{
				throw new InvalidOperationException("The NativeArray can not be Disposed because it was not allocated with a valid allocator.");
			}
			bool flag2 = this.m_Buffer == null;
			if (flag2)
			{
				throw new InvalidOperationException("The NativeArray is already disposed.");
			}
			bool flag3 = this.m_AllocatorLabel > Allocator.None;
			JobHandle result;
			if (flag3)
			{
				DisposeSentinel.Clear(ref this.m_DisposeSentinel);
				JobHandle jobHandle = new NativeArrayDisposeJob
				{
					Data = new NativeArrayDispose
					{
						m_Buffer = this.m_Buffer,
						m_AllocatorLabel = this.m_AllocatorLabel,
						m_Safety = this.m_Safety
					}
				}.Schedule(inputDeps);
				AtomicSafetyHandle.Release(this.m_Safety);
				this.m_Buffer = null;
				this.m_Length = 0;
				this.m_AllocatorLabel = Allocator.Invalid;
				result = jobHandle;
			}
			else
			{
				this.m_Buffer = null;
				this.m_Length = 0;
				result = inputDeps;
			}
			return result;
		}

		[WriteAccessRequired]
		public void CopyFrom(T[] array)
		{
			NativeArray<T>.Copy(array, this);
		}

		[WriteAccessRequired]
		public void CopyFrom(NativeArray<T> array)
		{
			NativeArray<T>.Copy(array, this);
		}

		public void CopyTo(T[] array)
		{
			NativeArray<T>.Copy(this, array);
		}

		public void CopyTo(NativeArray<T> array)
		{
			NativeArray<T>.Copy(this, array);
		}

		public T[] ToArray()
		{
			T[] array = new T[this.Length];
			NativeArray<T>.Copy(this, array, this.Length);
			return array;
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void FailOutOfRangeError(int index)
		{
			bool flag = index < this.Length && (this.m_MinIndex != 0 || this.m_MaxIndex != this.Length - 1);
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of restricted IJobParallelFor range [{1}...{2}] in ReadWriteBuffer.\n", index, this.m_MinIndex, this.m_MaxIndex) + "ReadWriteBuffers are restricted to only read & write the element at the job index. You can use double buffering strategies to avoid race conditions due to reading & writing in parallel to the same elements from a job.");
			}
			throw new IndexOutOfRangeException(string.Format("Index {0} is out of range of '{1}' Length.", index, this.Length));
		}

		public NativeArray<T>.Enumerator GetEnumerator()
		{
			return new NativeArray<T>.Enumerator(ref this);
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new NativeArray<T>.Enumerator(ref this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public bool Equals(NativeArray<T> other)
		{
			return this.m_Buffer == other.m_Buffer && this.m_Length == other.m_Length;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is NativeArray<T> && this.Equals((NativeArray<T>)obj);
		}

		public override int GetHashCode()
		{
			return this.m_Buffer * 397 ^ this.m_Length;
		}

		public static bool operator ==(NativeArray<T> left, NativeArray<T> right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(NativeArray<T> left, NativeArray<T> right)
		{
			return !left.Equals(right);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCopyLengths(int srcLength, int dstLength)
		{
			bool flag = srcLength != dstLength;
			if (flag)
			{
				throw new ArgumentException("source and destination length must be the same");
			}
		}

		public static void Copy(NativeArray<T> src, NativeArray<T> dst)
		{
			AtomicSafetyHandle.CheckReadAndThrow(src.m_Safety);
			AtomicSafetyHandle.CheckWriteAndThrow(dst.m_Safety);
			NativeArray<T>.CheckCopyLengths(src.Length, dst.Length);
			NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
		}

		public static void Copy(NativeArray<T>.ReadOnly src, NativeArray<T> dst)
		{
			AtomicSafetyHandle.CheckReadAndThrow(src.m_Safety);
			AtomicSafetyHandle.CheckWriteAndThrow(dst.m_Safety);
			NativeArray<T>.CheckCopyLengths(src.Length, dst.Length);
			NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
		}

		public static void Copy(T[] src, NativeArray<T> dst)
		{
			AtomicSafetyHandle.CheckWriteAndThrow(dst.m_Safety);
			NativeArray<T>.CheckCopyLengths(src.Length, dst.Length);
			NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
		}

		public static void Copy(NativeArray<T> src, T[] dst)
		{
			AtomicSafetyHandle.CheckReadAndThrow(src.m_Safety);
			NativeArray<T>.CheckCopyLengths(src.Length, dst.Length);
			NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
		}

		public static void Copy(NativeArray<T>.ReadOnly src, T[] dst)
		{
			AtomicSafetyHandle.CheckReadAndThrow(src.m_Safety);
			NativeArray<T>.CheckCopyLengths(src.Length, dst.Length);
			NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
		}

		public static void Copy(NativeArray<T> src, NativeArray<T> dst, int length)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, length);
		}

		public static void Copy(NativeArray<T>.ReadOnly src, NativeArray<T> dst, int length)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, length);
		}

		public static void Copy(T[] src, NativeArray<T> dst, int length)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, length);
		}

		public static void Copy(NativeArray<T> src, T[] dst, int length)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, length);
		}

		public static void Copy(NativeArray<T>.ReadOnly src, T[] dst, int length)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, length);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCopyArguments(int srcLength, int srcIndex, int dstLength, int dstIndex, int length)
		{
			bool flag = length < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("length", "length must be equal or greater than zero.");
			}
			bool flag2 = srcIndex < 0 || srcIndex > srcLength || (srcIndex == srcLength && srcLength > 0);
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("srcIndex", "srcIndex is outside the range of valid indexes for the source NativeArray.");
			}
			bool flag3 = dstIndex < 0 || dstIndex > dstLength || (dstIndex == dstLength && dstLength > 0);
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("dstIndex", "dstIndex is outside the range of valid indexes for the destination NativeArray.");
			}
			bool flag4 = srcIndex + length > srcLength;
			if (flag4)
			{
				throw new ArgumentException("length is greater than the number of elements from srcIndex to the end of the source NativeArray.", "length");
			}
			bool flag5 = dstIndex + length > dstLength;
			if (flag5)
			{
				throw new ArgumentException("length is greater than the number of elements from dstIndex to the end of the destination NativeArray.", "length");
			}
		}

		public unsafe static void Copy(NativeArray<T> src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			AtomicSafetyHandle.CheckReadAndThrow(src.m_Safety);
			AtomicSafetyHandle.CheckWriteAndThrow(dst.m_Safety);
			NativeArray<T>.CheckCopyArguments(src.Length, srcIndex, dst.Length, dstIndex, length);
			UnsafeUtility.MemCpy((void*)((byte*)dst.m_Buffer + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
		}

		public unsafe static void Copy(NativeArray<T>.ReadOnly src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			AtomicSafetyHandle.CheckReadAndThrow(src.m_Safety);
			AtomicSafetyHandle.CheckWriteAndThrow(dst.m_Safety);
			NativeArray<T>.CheckCopyArguments(src.Length, srcIndex, dst.Length, dstIndex, length);
			UnsafeUtility.MemCpy((void*)((byte*)dst.m_Buffer + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
		}

		public unsafe static void Copy(T[] src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			AtomicSafetyHandle.CheckWriteAndThrow(dst.m_Safety);
			bool flag = src == null;
			if (flag)
			{
				throw new ArgumentNullException("src");
			}
			NativeArray<T>.CheckCopyArguments(src.Length, srcIndex, dst.Length, dstIndex, length);
			GCHandle gCHandle = GCHandle.Alloc(src, GCHandleType.Pinned);
			IntPtr value = gCHandle.AddrOfPinnedObject();
			UnsafeUtility.MemCpy((void*)((byte*)dst.m_Buffer + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)((void*)value) + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
			gCHandle.Free();
		}

		public unsafe static void Copy(NativeArray<T> src, int srcIndex, T[] dst, int dstIndex, int length)
		{
			AtomicSafetyHandle.CheckReadAndThrow(src.m_Safety);
			bool flag = dst == null;
			if (flag)
			{
				throw new ArgumentNullException("dst");
			}
			NativeArray<T>.CheckCopyArguments(src.Length, srcIndex, dst.Length, dstIndex, length);
			GCHandle gCHandle = GCHandle.Alloc(dst, GCHandleType.Pinned);
			IntPtr value = gCHandle.AddrOfPinnedObject();
			UnsafeUtility.MemCpy((void*)((byte*)((void*)value) + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
			gCHandle.Free();
		}

		public unsafe static void Copy(NativeArray<T>.ReadOnly src, int srcIndex, T[] dst, int dstIndex, int length)
		{
			AtomicSafetyHandle.CheckReadAndThrow(src.m_Safety);
			bool flag = dst == null;
			if (flag)
			{
				throw new ArgumentNullException("dst");
			}
			NativeArray<T>.CheckCopyArguments(src.Length, srcIndex, dst.Length, dstIndex, length);
			GCHandle gCHandle = GCHandle.Alloc(dst, GCHandleType.Pinned);
			IntPtr value = gCHandle.AddrOfPinnedObject();
			UnsafeUtility.MemCpy((void*)((byte*)((void*)value) + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
			gCHandle.Free();
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReinterpretLoadRange<U>(int sourceIndex) where U : struct
		{
			long num = (long)UnsafeUtility.SizeOf<T>();
			AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			long num2 = (long)UnsafeUtility.SizeOf<U>();
			long num3 = (long)this.Length * num;
			long num4 = (long)sourceIndex * num;
			long num5 = num4 + num2;
			bool flag = num4 < 0L || num5 > num3;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", "loaded byte range must fall inside container bounds");
			}
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReinterpretStoreRange<U>(int destIndex) where U : struct
		{
			long num = (long)UnsafeUtility.SizeOf<T>();
			AtomicSafetyHandle.CheckWriteAndThrow(this.m_Safety);
			long num2 = (long)UnsafeUtility.SizeOf<U>();
			long num3 = (long)this.Length * num;
			long num4 = (long)destIndex * num;
			long num5 = num4 + num2;
			bool flag = num4 < 0L || num5 > num3;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("destIndex", "stored byte range must fall inside container bounds");
			}
		}

		public unsafe U ReinterpretLoad<U>(int sourceIndex) where U : struct
		{
			this.CheckReinterpretLoadRange<U>(sourceIndex);
			byte* source = (byte*)this.m_Buffer + (long)UnsafeUtility.SizeOf<T>() * (long)sourceIndex;
			return UnsafeUtility.ReadArrayElement<U>((void*)source, 0);
		}

		public unsafe void ReinterpretStore<U>(int destIndex, U data) where U : struct
		{
			this.CheckReinterpretStoreRange<U>(destIndex);
			byte* destination = (byte*)this.m_Buffer + (long)UnsafeUtility.SizeOf<T>() * (long)destIndex;
			UnsafeUtility.WriteArrayElement<U>((void*)destination, 0, data);
		}

		private NativeArray<U> InternalReinterpret<U>(int length) where U : struct
		{
			NativeArray<U> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<U>(this.m_Buffer, length, this.m_AllocatorLabel);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<U>(ref result, this.m_Safety);
			this.SetDisposeSentinel<U>(ref result);
			return result;
		}

		[BurstDiscard]
		private void SetDisposeSentinel<U>(ref NativeArray<U> result) where U : struct
		{
			result.m_DisposeSentinel = this.m_DisposeSentinel;
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckReinterpretSize<U>() where U : struct
		{
			bool flag = UnsafeUtility.SizeOf<T>() != UnsafeUtility.SizeOf<U>();
			if (flag)
			{
				throw new InvalidOperationException(string.Format("Types {0} and {1} are different sizes - direct reinterpretation is not possible. If this is what you intended, use Reinterpret(<type size>)", typeof(T), typeof(U)));
			}
		}

		public NativeArray<U> Reinterpret<U>() where U : struct
		{
			NativeArray<T>.CheckReinterpretSize<U>();
			return this.InternalReinterpret<U>(this.Length);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReinterpretSize<U>(long tSize, long uSize, int expectedTypeSize, long byteLen, long uLen)
		{
			bool flag = tSize != (long)expectedTypeSize;
			if (flag)
			{
				throw new InvalidOperationException(string.Format("Type {0} was expected to be {1} but is {2} bytes", typeof(T), expectedTypeSize, tSize));
			}
			bool flag2 = uLen * uSize != byteLen;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("Types {0} (array length {1}) and {2} cannot be aliased due to size constraints. The size of the types and lengths involved must line up.", typeof(T), this.Length, typeof(U)));
			}
		}

		public NativeArray<U> Reinterpret<U>(int expectedTypeSize) where U : struct
		{
			long num = (long)UnsafeUtility.SizeOf<T>();
			long num2 = (long)UnsafeUtility.SizeOf<U>();
			long num3 = (long)this.Length * num;
			long num4 = num3 / num2;
			this.CheckReinterpretSize<U>(num, num2, expectedTypeSize, num3, num4);
			return this.InternalReinterpret<U>((int)num4);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckGetSubArrayArguments(int start, int length)
		{
			bool flag = start < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("start", "start must be >= 0");
			}
			bool flag2 = start + length > this.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length", string.Format("sub array range {0}-{1} is outside the range of the native array 0-{2}", start, start + length - 1, this.Length - 1));
			}
		}

		public unsafe NativeArray<T> GetSubArray(int start, int length)
		{
			this.CheckGetSubArrayArguments(start, length);
			NativeArray<T> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)((byte*)this.m_Buffer + (long)UnsafeUtility.SizeOf<T>() * (long)start), length, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, this.m_Safety);
			result.m_DisposeSentinel = null;
			return result;
		}

		public NativeArray<T>.ReadOnly AsReadOnly()
		{
			return new NativeArray<T>.ReadOnly(this.m_Buffer, this.m_Length, ref this.m_Safety);
		}
	}
}
