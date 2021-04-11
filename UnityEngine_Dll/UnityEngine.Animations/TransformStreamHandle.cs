using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	[NativeHeader("Modules/Animation/Director/AnimationStreamHandles.h"), NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h"), MovedFrom("UnityEngine.Experimental.Animations")]
	public struct TransformStreamHandle
	{
		private uint m_AnimatorBindingsVersion;

		private int handleIndex;

		private int skeletonIndex;

		private bool createdByNative
		{
			get
			{
				return this.animatorBindingsVersion > 0u;
			}
		}

		private bool hasHandleIndex
		{
			get
			{
				return this.handleIndex != -1;
			}
		}

		private bool hasSkeletonIndex
		{
			get
			{
				return this.skeletonIndex != -1;
			}
		}

		internal uint animatorBindingsVersion
		{
			get
			{
				return this.m_AnimatorBindingsVersion;
			}
			private set
			{
				this.m_AnimatorBindingsVersion = value;
			}
		}

		public bool IsValid(AnimationStream stream)
		{
			return this.IsValidInternal(ref stream);
		}

		private bool IsValidInternal(ref AnimationStream stream)
		{
			return stream.isValid && this.createdByNative && this.hasHandleIndex;
		}

		private bool IsSameVersionAsStream(ref AnimationStream stream)
		{
			return this.animatorBindingsVersion == stream.animatorBindingsVersion;
		}

		public void Resolve(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
		}

		public bool IsResolved(AnimationStream stream)
		{
			return this.IsResolvedInternal(ref stream);
		}

		private bool IsResolvedInternal(ref AnimationStream stream)
		{
			return this.IsValidInternal(ref stream) && this.IsSameVersionAsStream(ref stream) && this.hasSkeletonIndex;
		}

		private void CheckIsValidAndResolve(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			bool flag = this.IsResolvedInternal(ref stream);
			if (!flag)
			{
				bool flag2 = !this.createdByNative || !this.hasHandleIndex;
				if (flag2)
				{
					throw new InvalidOperationException("The TransformStreamHandle is invalid. Please use proper function to create the handle.");
				}
				bool flag3 = !this.IsSameVersionAsStream(ref stream) || (this.hasHandleIndex && !this.hasSkeletonIndex);
				if (flag3)
				{
					this.ResolveInternal(ref stream);
				}
				bool flag4 = this.hasHandleIndex && !this.hasSkeletonIndex;
				if (flag4)
				{
					throw new InvalidOperationException("The TransformStreamHandle cannot be resolved.");
				}
			}
		}

		public Vector3 GetPosition(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetPositionInternal(ref stream);
		}

		public void SetPosition(AnimationStream stream, Vector3 position)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetPositionInternal(ref stream, position);
		}

		public Quaternion GetRotation(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetRotationInternal(ref stream);
		}

		public void SetRotation(AnimationStream stream, Quaternion rotation)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetRotationInternal(ref stream, rotation);
		}

		public Vector3 GetLocalPosition(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetLocalPositionInternal(ref stream);
		}

		public void SetLocalPosition(AnimationStream stream, Vector3 position)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalPositionInternal(ref stream, position);
		}

		public Quaternion GetLocalRotation(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetLocalRotationInternal(ref stream);
		}

		public void SetLocalRotation(AnimationStream stream, Quaternion rotation)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalRotationInternal(ref stream, rotation);
		}

		public Vector3 GetLocalScale(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetLocalScaleInternal(ref stream);
		}

		public void SetLocalScale(AnimationStream stream, Vector3 scale)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalScaleInternal(ref stream, scale);
		}

		public bool GetPositionReadMask(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetPositionReadMaskInternal(ref stream);
		}

		public bool GetRotationReadMask(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetRotationReadMaskInternal(ref stream);
		}

		public bool GetScaleReadMask(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetScaleReadMaskInternal(ref stream);
		}

		public void GetLocalTRS(AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.GetLocalTRSInternal(ref stream, out position, out rotation, out scale);
		}

		public void SetLocalTRS(AnimationStream stream, Vector3 position, Quaternion rotation, Vector3 scale, bool useMask)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalTRSInternal(ref stream, position, rotation, scale, useMask);
		}

		public void GetGlobalTR(AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.GetGlobalTRInternal(ref stream, out position, out rotation);
		}

		public void SetGlobalTR(AnimationStream stream, Vector3 position, Quaternion rotation, bool useMask)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetGlobalTRInternal(ref stream, position, rotation, useMask);
		}

		[NativeMethod(Name = "Resolve", IsThreadSafe = true)]
		private void ResolveInternal(ref AnimationStream stream)
		{
			TransformStreamHandle.ResolveInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Vector3 GetPositionInternal(ref AnimationStream stream)
		{
			Vector3 result;
			TransformStreamHandle.GetPositionInternal_Injected(ref this, ref stream, out result);
			return result;
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::SetPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetPositionInternal(ref AnimationStream stream, Vector3 position)
		{
			TransformStreamHandle.SetPositionInternal_Injected(ref this, ref stream, ref position);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Quaternion GetRotationInternal(ref AnimationStream stream)
		{
			Quaternion result;
			TransformStreamHandle.GetRotationInternal_Injected(ref this, ref stream, out result);
			return result;
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::SetRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetRotationInternal(ref AnimationStream stream, Quaternion rotation)
		{
			TransformStreamHandle.SetRotationInternal_Injected(ref this, ref stream, ref rotation);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Vector3 GetLocalPositionInternal(ref AnimationStream stream)
		{
			Vector3 result;
			TransformStreamHandle.GetLocalPositionInternal_Injected(ref this, ref stream, out result);
			return result;
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalPositionInternal(ref AnimationStream stream, Vector3 position)
		{
			TransformStreamHandle.SetLocalPositionInternal_Injected(ref this, ref stream, ref position);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Quaternion GetLocalRotationInternal(ref AnimationStream stream)
		{
			Quaternion result;
			TransformStreamHandle.GetLocalRotationInternal_Injected(ref this, ref stream, out result);
			return result;
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalRotationInternal(ref AnimationStream stream, Quaternion rotation)
		{
			TransformStreamHandle.SetLocalRotationInternal_Injected(ref this, ref stream, ref rotation);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalScaleInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Vector3 GetLocalScaleInternal(ref AnimationStream stream)
		{
			Vector3 result;
			TransformStreamHandle.GetLocalScaleInternal_Injected(ref this, ref stream, out result);
			return result;
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalScaleInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalScaleInternal(ref AnimationStream stream, Vector3 scale)
		{
			TransformStreamHandle.SetLocalScaleInternal_Injected(ref this, ref stream, ref scale);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetPositionReadMaskInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private bool GetPositionReadMaskInternal(ref AnimationStream stream)
		{
			return TransformStreamHandle.GetPositionReadMaskInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetRotationReadMaskInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private bool GetRotationReadMaskInternal(ref AnimationStream stream)
		{
			return TransformStreamHandle.GetRotationReadMaskInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetScaleReadMaskInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private bool GetScaleReadMaskInternal(ref AnimationStream stream)
		{
			return TransformStreamHandle.GetScaleReadMaskInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalTRSInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void GetLocalTRSInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			TransformStreamHandle.GetLocalTRSInternal_Injected(ref this, ref stream, out position, out rotation, out scale);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalTRSInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalTRSInternal(ref AnimationStream stream, Vector3 position, Quaternion rotation, Vector3 scale, bool useMask)
		{
			TransformStreamHandle.SetLocalTRSInternal_Injected(ref this, ref stream, ref position, ref rotation, ref scale, useMask);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetGlobalTRInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void GetGlobalTRInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			TransformStreamHandle.GetGlobalTRInternal_Injected(ref this, ref stream, out position, out rotation);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::SetGlobalTRInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetGlobalTRInternal(ref AnimationStream stream, Vector3 position, Quaternion rotation, bool useMask)
		{
			TransformStreamHandle.SetGlobalTRInternal_Injected(ref this, ref stream, ref position, ref rotation, useMask);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalScaleInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalScaleInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 scale);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetPositionReadMaskInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetRotationReadMaskInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetScaleReadMaskInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalTRSInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalTRSInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position, ref Quaternion rotation, ref Vector3 scale, bool useMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalTRInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 position, out Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalTRInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position, ref Quaternion rotation, bool useMask);
	}
}
