using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	[NativeHeader("Modules/Animation/Director/AnimationSceneHandles.h"), MovedFrom("UnityEngine.Experimental.Animations")]
	public struct PropertySceneHandle
	{
		private uint valid;

		private int handleIndex;

		private bool createdByNative
		{
			get
			{
				return this.valid > 0u;
			}
		}

		private bool hasHandleIndex
		{
			get
			{
				return this.handleIndex != -1;
			}
		}

		public bool IsValid(AnimationStream stream)
		{
			return this.IsValidInternal(ref stream);
		}

		private bool IsValidInternal(ref AnimationStream stream)
		{
			return stream.isValid && this.createdByNative && this.hasHandleIndex && this.HasValidTransform(ref stream);
		}

		public void Resolve(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			this.ResolveInternal(ref stream);
		}

		public bool IsResolved(AnimationStream stream)
		{
			return this.IsValidInternal(ref stream) && this.IsBound(ref stream);
		}

		private void CheckIsValid(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			bool flag = !this.createdByNative || !this.hasHandleIndex;
			if (flag)
			{
				throw new InvalidOperationException("The PropertySceneHandle is invalid. Please use proper function to create the handle.");
			}
			bool flag2 = !this.HasValidTransform(ref stream);
			if (flag2)
			{
				throw new NullReferenceException("The transform is invalid.");
			}
		}

		public float GetFloat(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetFloatInternal(ref stream);
		}

		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetFloat(AnimationStream stream, float value)
		{
		}

		public int GetInt(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetIntInternal(ref stream);
		}

		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetInt(AnimationStream stream, int value)
		{
		}

		public bool GetBool(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetBoolInternal(ref stream);
		}

		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetBool(AnimationStream stream, bool value)
		{
		}

		[ThreadSafe]
		private bool HasValidTransform(ref AnimationStream stream)
		{
			return PropertySceneHandle.HasValidTransform_Injected(ref this, ref stream);
		}

		[ThreadSafe]
		private bool IsBound(ref AnimationStream stream)
		{
			return PropertySceneHandle.IsBound_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "Resolve", IsThreadSafe = true)]
		private void ResolveInternal(ref AnimationStream stream)
		{
			PropertySceneHandle.ResolveInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "GetFloat", IsThreadSafe = true)]
		private float GetFloatInternal(ref AnimationStream stream)
		{
			return PropertySceneHandle.GetFloatInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "GetInt", IsThreadSafe = true)]
		private int GetIntInternal(ref AnimationStream stream)
		{
			return PropertySceneHandle.GetIntInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "GetBool", IsThreadSafe = true)]
		private bool GetBoolInternal(ref AnimationStream stream)
		{
			return PropertySceneHandle.GetBoolInternal_Injected(ref this, ref stream);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasValidTransform_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsBound_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloatInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIntInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBoolInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);
	}
}
