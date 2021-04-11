using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Jobs
{
	[NativeHeader("Runtime/Transform/ScriptBindings/TransformAccess.bindings.h")]
	public struct TransformAccess
	{
		private IntPtr hierarchy;

		private int index;

		private bool isReadOnly;

		public Vector3 position
		{
			get
			{
				this.CheckHierarchyValid();
				Vector3 result;
				TransformAccess.GetPosition(ref this, out result);
				return result;
			}
			set
			{
				this.CheckHierarchyValid();
				this.CheckWriteAccess();
				TransformAccess.SetPosition(ref this, ref value);
			}
		}

		public Quaternion rotation
		{
			get
			{
				this.CheckHierarchyValid();
				Quaternion result;
				TransformAccess.GetRotation(ref this, out result);
				return result;
			}
			set
			{
				this.CheckHierarchyValid();
				this.CheckWriteAccess();
				TransformAccess.SetRotation(ref this, ref value);
			}
		}

		public Vector3 localPosition
		{
			get
			{
				this.CheckHierarchyValid();
				Vector3 result;
				TransformAccess.GetLocalPosition(ref this, out result);
				return result;
			}
			set
			{
				this.CheckHierarchyValid();
				this.CheckWriteAccess();
				TransformAccess.SetLocalPosition(ref this, ref value);
			}
		}

		public Quaternion localRotation
		{
			get
			{
				this.CheckHierarchyValid();
				Quaternion result;
				TransformAccess.GetLocalRotation(ref this, out result);
				return result;
			}
			set
			{
				this.CheckHierarchyValid();
				this.CheckWriteAccess();
				TransformAccess.SetLocalRotation(ref this, ref value);
			}
		}

		public Vector3 localScale
		{
			get
			{
				this.CheckHierarchyValid();
				Vector3 result;
				TransformAccess.GetLocalScale(ref this, out result);
				return result;
			}
			set
			{
				this.CheckHierarchyValid();
				this.CheckWriteAccess();
				TransformAccess.SetLocalScale(ref this, ref value);
			}
		}

		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				this.CheckHierarchyValid();
				Matrix4x4 result;
				TransformAccess.GetLocalToWorldMatrix(ref this, out result);
				return result;
			}
		}

		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				this.CheckHierarchyValid();
				Matrix4x4 result;
				TransformAccess.GetWorldToLocalMatrix(ref this, out result);
				return result;
			}
		}

		public bool isValid
		{
			get
			{
				return this.hierarchy != IntPtr.Zero;
			}
		}

		[NativeMethod(Name = "TransformAccessBindings::GetPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPosition(ref TransformAccess access, out Vector3 p);

		[NativeMethod(Name = "TransformAccessBindings::SetPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPosition(ref TransformAccess access, ref Vector3 p);

		[NativeMethod(Name = "TransformAccessBindings::GetRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotation(ref TransformAccess access, out Quaternion r);

		[NativeMethod(Name = "TransformAccessBindings::SetRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRotation(ref TransformAccess access, ref Quaternion r);

		[NativeMethod(Name = "TransformAccessBindings::GetLocalPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPosition(ref TransformAccess access, out Vector3 p);

		[NativeMethod(Name = "TransformAccessBindings::SetLocalPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalPosition(ref TransformAccess access, ref Vector3 p);

		[NativeMethod(Name = "TransformAccessBindings::GetLocalRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalRotation(ref TransformAccess access, out Quaternion r);

		[NativeMethod(Name = "TransformAccessBindings::SetLocalRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalRotation(ref TransformAccess access, ref Quaternion r);

		[NativeMethod(Name = "TransformAccessBindings::GetLocalScale", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalScale(ref TransformAccess access, out Vector3 r);

		[NativeMethod(Name = "TransformAccessBindings::SetLocalScale", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalScale(ref TransformAccess access, ref Vector3 r);

		[NativeMethod(Name = "TransformAccessBindings::GetLocalToWorldMatrix", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalToWorldMatrix(ref TransformAccess access, out Matrix4x4 m);

		[NativeMethod(Name = "TransformAccessBindings::GetWorldToLocalMatrix", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetWorldToLocalMatrix(ref TransformAccess access, out Matrix4x4 m);

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl((MethodImplOptions)256)]
		internal void CheckHierarchyValid()
		{
			bool flag = !this.isValid;
			if (flag)
			{
				throw new NullReferenceException("The TransformAccess is not valid and points to an invalid hierarchy");
			}
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl((MethodImplOptions)256)]
		internal void MarkReadWrite()
		{
			this.isReadOnly = false;
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl((MethodImplOptions)256)]
		internal void MarkReadOnly()
		{
			this.isReadOnly = true;
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl((MethodImplOptions)256)]
		private void CheckWriteAccess()
		{
			bool flag = this.isReadOnly;
			if (flag)
			{
				throw new InvalidOperationException("Cannot write to TransformAccess since the transform job was scheduled as read-only");
			}
		}
	}
}
