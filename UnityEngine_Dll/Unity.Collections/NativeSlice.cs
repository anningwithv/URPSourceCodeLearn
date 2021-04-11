using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Internal;

namespace Unity.Collections
{
	[DebuggerDisplay("Length = {Length}"), DebuggerTypeProxy(typeof(NativeSliceDebugView<>)), NativeContainer, NativeContainerSupportsMinMaxWriteRestriction]
	public struct NativeSlice<T> : IEnumerable<T>, IEnumerable, IEquatable<NativeSlice<T>> where T : struct
	{
		[ExcludeFromDocs]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			private NativeSlice<T> m_Array;

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

			public Enumerator(ref NativeSlice<T> array)
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

		[NativeDisableUnsafePtrRestriction]
		internal unsafe byte* m_Buffer;

		internal int m_Stride;

		internal int m_Length;

		internal int m_MinIndex;

		internal int m_MaxIndex;

		internal AtomicSafetyHandle m_Safety;

		public unsafe T this[int index]
		{
			get
			{
				this.CheckReadIndex(index);
				return UnsafeUtility.ReadArrayElementWithStride<T>((void*)this.m_Buffer, index, this.m_Stride);
			}
			[WriteAccessRequired]
			set
			{
				this.CheckWriteIndex(index);
				UnsafeUtility.WriteArrayElementWithStride<T>((void*)this.m_Buffer, index, this.m_Stride, value);
			}
		}

		public int Stride
		{
			get
			{
				return this.m_Stride;
			}
		}

		public int Length
		{
			get
			{
				return this.m_Length;
			}
		}

		public NativeSlice(NativeSlice<T> slice, int start)
		{
			this = new NativeSlice<T>(slice, start, slice.Length - start);
		}

		public NativeSlice(NativeSlice<T> slice, int start, int length)
		{
			bool flag = start < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("start", string.Format("Slice start {0} < 0.", start));
			}
			bool flag2 = length < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length", string.Format("Slice length {0} < 0.", length));
			}
			bool flag3 = start + length > slice.Length;
			if (flag3)
			{
				throw new ArgumentException(string.Format("Slice start + length ({0}) range must be <= slice.Length ({1})", start + length, slice.Length));
			}
			bool flag4 = (slice.m_MinIndex != 0 || slice.m_MaxIndex != slice.m_Length - 1) && (start < slice.m_MinIndex || slice.m_MaxIndex < start || slice.m_MaxIndex < start + length - 1);
			if (flag4)
			{
				throw new ArgumentException("Slice may not be used on a restricted range slice", "slice");
			}
			this.m_MinIndex = 0;
			this.m_MaxIndex = length - 1;
			this.m_Safety = slice.m_Safety;
			this.m_Stride = slice.m_Stride;
			this.m_Buffer = slice.m_Buffer + this.m_Stride * start;
			this.m_Length = length;
		}

		public NativeSlice(NativeArray<T> array)
		{
			this = new NativeSlice<T>(array, 0, array.Length);
		}

		public NativeSlice(NativeArray<T> array, int start)
		{
			this = new NativeSlice<T>(array, start, array.Length - start);
		}

		public static implicit operator NativeSlice<T>(NativeArray<T> array)
		{
			return new NativeSlice<T>(array);
		}

		public unsafe NativeSlice(NativeArray<T> array, int start, int length)
		{
			bool flag = start < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("start", string.Format("Slice start {0} < 0.", start));
			}
			bool flag2 = length < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length", string.Format("Slice length {0} < 0.", length));
			}
			bool flag3 = start + length > array.Length;
			if (flag3)
			{
				throw new ArgumentException(string.Format("Slice start + length ({0}) range must be <= array.Length ({1})", start + length, array.Length));
			}
			bool flag4 = (array.m_MinIndex != 0 || array.m_MaxIndex != array.m_Length - 1) && (start < array.m_MinIndex || array.m_MaxIndex < start || array.m_MaxIndex < start + length - 1);
			if (flag4)
			{
				throw new ArgumentException("Slice may not be used on a restricted range array", "array");
			}
			this.m_MinIndex = 0;
			this.m_MaxIndex = length - 1;
			this.m_Safety = array.m_Safety;
			this.m_Stride = UnsafeUtility.SizeOf<T>();
			byte* buffer = (byte*)array.m_Buffer + this.m_Stride * start;
			this.m_Buffer = buffer;
			this.m_Length = length;
		}

		public NativeSlice<U> SliceConvert<U>() where U : struct
		{
			int num = UnsafeUtility.SizeOf<U>();
			NativeSlice<U> nativeSlice;
			nativeSlice.m_Buffer = this.m_Buffer;
			nativeSlice.m_Stride = num;
			nativeSlice.m_Length = this.m_Length * this.m_Stride / num;
			bool flag = this.m_Stride != UnsafeUtility.SizeOf<T>();
			if (flag)
			{
				throw new InvalidOperationException("SliceConvert requires that stride matches the size of the source type");
			}
			bool flag2 = this.m_MinIndex != 0 || this.m_MaxIndex != this.m_Length - 1;
			if (flag2)
			{
				throw new InvalidOperationException("SliceConvert may not be used on a restricted range array");
			}
			bool flag3 = this.m_Stride * this.m_Length % num != 0;
			if (flag3)
			{
				throw new InvalidOperationException("SliceConvert requires that Length * sizeof(T) is a multiple of sizeof(U).");
			}
			nativeSlice.m_MinIndex = 0;
			nativeSlice.m_MaxIndex = nativeSlice.m_Length - 1;
			nativeSlice.m_Safety = this.m_Safety;
			return nativeSlice;
		}

		public NativeSlice<U> SliceWithStride<U>(int offset) where U : struct
		{
			NativeSlice<U> result;
			result.m_Buffer = this.m_Buffer + offset;
			result.m_Stride = this.m_Stride;
			result.m_Length = this.m_Length;
			bool flag = offset < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("offset", "SliceWithStride offset must be >= 0");
			}
			bool flag2 = offset + UnsafeUtility.SizeOf<U>() > UnsafeUtility.SizeOf<T>();
			if (flag2)
			{
				throw new ArgumentException("SliceWithStride sizeof(U) + offset must be <= sizeof(T)", "offset");
			}
			result.m_MinIndex = this.m_MinIndex;
			result.m_MaxIndex = this.m_MaxIndex;
			result.m_Safety = this.m_Safety;
			return result;
		}

		public NativeSlice<U> SliceWithStride<U>() where U : struct
		{
			return this.SliceWithStride<U>(0);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private unsafe void CheckReadIndex(int index)
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
		private unsafe void CheckWriteIndex(int index)
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

		[WriteAccessRequired]
		public void CopyFrom(NativeSlice<T> slice)
		{
			bool flag = this.Length != slice.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("slice.Length ({0}) does not match the Length of this instance ({1}).", slice.Length, this.Length), "slice");
			}
			UnsafeUtility.MemCpyStride(this.GetUnsafePtr<T>(), this.Stride, slice.GetUnsafeReadOnlyPtr<T>(), slice.Stride, UnsafeUtility.SizeOf<T>(), this.m_Length);
		}

		[WriteAccessRequired]
		public unsafe void CopyFrom(T[] array)
		{
			bool flag = this.Length != array.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("array.Length ({0}) does not match the Length of this instance ({1}).", array.Length, this.Length), "array");
			}
			GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr value = gCHandle.AddrOfPinnedObject();
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride(this.GetUnsafePtr<T>(), this.Stride, (void*)value, num, num, this.m_Length);
			gCHandle.Free();
		}

		public void CopyTo(NativeArray<T> array)
		{
			bool flag = this.Length != array.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("array.Length ({0}) does not match the Length of this instance ({1}).", array.Length, this.Length), "array");
			}
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride(array.GetUnsafePtr<T>(), num, this.GetUnsafeReadOnlyPtr<T>(), this.Stride, num, this.m_Length);
		}

		public unsafe void CopyTo(T[] array)
		{
			bool flag = this.Length != array.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("array.Length ({0}) does not match the Length of this instance ({1}).", array.Length, this.Length), "array");
			}
			GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr value = gCHandle.AddrOfPinnedObject();
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride((void*)value, num, this.GetUnsafeReadOnlyPtr<T>(), this.Stride, num, this.m_Length);
			gCHandle.Free();
		}

		public T[] ToArray()
		{
			T[] array = new T[this.Length];
			this.CopyTo(array);
			return array;
		}

		public NativeSlice<T>.Enumerator GetEnumerator()
		{
			return new NativeSlice<T>.Enumerator(ref this);
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new NativeSlice<T>.Enumerator(ref this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public bool Equals(NativeSlice<T> other)
		{
			return this.m_Buffer == other.m_Buffer && this.m_Stride == other.m_Stride && this.m_Length == other.m_Length;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is NativeSlice<T> && this.Equals((NativeSlice<T>)obj);
		}

		public override int GetHashCode()
		{
			int num = this.m_Buffer;
			num = (num * 397 ^ this.m_Stride);
			return num * 397 ^ this.m_Length;
		}

		public static bool operator ==(NativeSlice<T> left, NativeSlice<T> right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(NativeSlice<T> left, NativeSlice<T> right)
		{
			return !left.Equals(right);
		}
	}
}
