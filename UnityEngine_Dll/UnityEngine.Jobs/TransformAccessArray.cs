using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Jobs
{
	[NativeType(Header = "Runtime/Transform/ScriptBindings/TransformAccess.bindings.h", CodegenOptions = CodegenOptions.Custom)]
	public struct TransformAccessArray : IDisposable
	{
		private IntPtr m_TransformArray;

		private AtomicSafetyHandle m_Safety;

		private DisposeSentinel m_DisposeSentinel;

		public bool isCreated
		{
			get
			{
				return this.m_TransformArray != IntPtr.Zero;
			}
		}

		public Transform this[int index]
		{
			get
			{
				AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
				return TransformAccessArray.GetTransform(this.m_TransformArray, index);
			}
			set
			{
				AtomicSafetyHandle.CheckWriteAndThrow(this.m_Safety);
				TransformAccessArray.SetTransform(this.m_TransformArray, index, value);
			}
		}

		public int capacity
		{
			get
			{
				AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
				return TransformAccessArray.GetCapacity(this.m_TransformArray);
			}
			set
			{
				AtomicSafetyHandle.CheckWriteAndThrow(this.m_Safety);
				TransformAccessArray.SetCapacity(this.m_TransformArray, value);
			}
		}

		public int length
		{
			get
			{
				AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
				return TransformAccessArray.GetLength(this.m_TransformArray);
			}
		}

		public TransformAccessArray(Transform[] transforms, int desiredJobCount = -1)
		{
			TransformAccessArray.Allocate(transforms.Length, desiredJobCount, out this);
			TransformAccessArray.SetTransforms(this.m_TransformArray, transforms);
		}

		public TransformAccessArray(int capacity, int desiredJobCount = -1)
		{
			TransformAccessArray.Allocate(capacity, desiredJobCount, out this);
		}

		public static void Allocate(int capacity, int desiredJobCount, out TransformAccessArray array)
		{
			array.m_TransformArray = TransformAccessArray.Create(capacity, desiredJobCount);
			DisposeSentinel.Create(out array.m_Safety, out array.m_DisposeSentinel, 1, Allocator.Persistent);
		}

		public void Dispose()
		{
			DisposeSentinel.Dispose(ref this.m_Safety, ref this.m_DisposeSentinel);
			TransformAccessArray.DestroyTransformAccessArray(this.m_TransformArray);
			this.m_TransformArray = IntPtr.Zero;
		}

		internal IntPtr GetTransformAccessArrayForSchedule()
		{
			AtomicSafetyHandle.CheckWriteAndThrow(this.m_Safety);
			return this.m_TransformArray;
		}

		public void Add(Transform transform)
		{
			AtomicSafetyHandle.CheckWriteAndThrow(this.m_Safety);
			TransformAccessArray.Add(this.m_TransformArray, transform);
		}

		public void RemoveAtSwapBack(int index)
		{
			AtomicSafetyHandle.CheckWriteAndThrow(this.m_Safety);
			TransformAccessArray.RemoveAtSwapBack(this.m_TransformArray, index);
		}

		public void SetTransforms(Transform[] transforms)
		{
			AtomicSafetyHandle.CheckWriteAndThrow(this.m_Safety);
			TransformAccessArray.SetTransforms(this.m_TransformArray, transforms);
		}

		[NativeMethod(Name = "TransformAccessArrayBindings::Create", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(int capacity, int desiredJobCount);

		[NativeMethod(Name = "DestroyTransformAccessArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyTransformAccessArray(IntPtr transformArray);

		[NativeMethod(Name = "TransformAccessArrayBindings::SetTransforms", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTransforms(IntPtr transformArrayIntPtr, Transform[] transforms);

		[NativeMethod(Name = "TransformAccessArrayBindings::AddTransform", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Add(IntPtr transformArrayIntPtr, Transform transform);

		[NativeMethod(Name = "TransformAccessArrayBindings::RemoveAtSwapBack", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveAtSwapBack(IntPtr transformArrayIntPtr, int index);

		[NativeMethod(Name = "TransformAccessArrayBindings::GetSortedTransformAccess", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSortedTransformAccess(IntPtr transformArrayIntPtr);

		[NativeMethod(Name = "TransformAccessArrayBindings::GetSortedToUserIndex", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSortedToUserIndex(IntPtr transformArrayIntPtr);

		[NativeMethod(Name = "TransformAccessArrayBindings::GetLength", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetLength(IntPtr transformArrayIntPtr);

		[NativeMethod(Name = "TransformAccessArrayBindings::GetCapacity", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetCapacity(IntPtr transformArrayIntPtr);

		[NativeMethod(Name = "TransformAccessArrayBindings::SetCapacity", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetCapacity(IntPtr transformArrayIntPtr, int capacity);

		[NativeMethod(Name = "TransformAccessArrayBindings::GetTransform", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Transform GetTransform(IntPtr transformArrayIntPtr, int index);

		[NativeMethod(Name = "TransformAccessArrayBindings::SetTransform", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetTransform(IntPtr transformArrayIntPtr, int index, Transform transform);
	}
}
