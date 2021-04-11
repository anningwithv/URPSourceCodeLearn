using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStream.bindings.h"), NativeHeader("Modules/Animation/Director/AnimationStream.h"), MovedFrom("UnityEngine.Experimental.Animations"), RequiredByNativeCode]
	public struct AnimationStream
	{
		private uint m_AnimatorBindingsVersion;

		private IntPtr constant;

		private IntPtr input;

		private IntPtr output;

		private IntPtr workspace;

		private IntPtr inputStreamAccessor;

		private IntPtr animationHandleBinder;

		internal const int InvalidIndex = -1;

		internal uint animatorBindingsVersion
		{
			get
			{
				return this.m_AnimatorBindingsVersion;
			}
		}

		public bool isValid
		{
			get
			{
				return this.m_AnimatorBindingsVersion >= 2u && this.constant != IntPtr.Zero && this.input != IntPtr.Zero && this.output != IntPtr.Zero && this.workspace != IntPtr.Zero && this.animationHandleBinder != IntPtr.Zero;
			}
		}

		public float deltaTime
		{
			get
			{
				this.CheckIsValid();
				return this.GetDeltaTime();
			}
		}

		public Vector3 velocity
		{
			get
			{
				this.CheckIsValid();
				return this.GetVelocity();
			}
			set
			{
				this.CheckIsValid();
				this.SetVelocity(value);
			}
		}

		public Vector3 angularVelocity
		{
			get
			{
				this.CheckIsValid();
				return this.GetAngularVelocity();
			}
			set
			{
				this.CheckIsValid();
				this.SetAngularVelocity(value);
			}
		}

		public Vector3 rootMotionPosition
		{
			get
			{
				this.CheckIsValid();
				return this.GetRootMotionPosition();
			}
		}

		public Quaternion rootMotionRotation
		{
			get
			{
				this.CheckIsValid();
				return this.GetRootMotionRotation();
			}
		}

		public bool isHumanStream
		{
			get
			{
				this.CheckIsValid();
				return this.GetIsHumanStream();
			}
		}

		public int inputStreamCount
		{
			get
			{
				this.CheckIsValid();
				return this.GetInputStreamCount();
			}
		}

		internal void CheckIsValid()
		{
			bool flag = !this.isValid;
			if (flag)
			{
				throw new InvalidOperationException("The AnimationStream is invalid.");
			}
		}

		public AnimationHumanStream AsHuman()
		{
			this.CheckIsValid();
			bool flag = !this.GetIsHumanStream();
			if (flag)
			{
				throw new InvalidOperationException("Cannot create an AnimationHumanStream for a generic rig.");
			}
			return this.GetHumanStream();
		}

		public AnimationStream GetInputStream(int index)
		{
			this.CheckIsValid();
			return this.InternalGetInputStream(index);
		}

		public float GetInputWeight(int index)
		{
			this.CheckIsValid();
			return this.InternalGetInputWeight(index);
		}

		public void CopyAnimationStreamMotion(AnimationStream animationStream)
		{
			this.CheckIsValid();
			animationStream.CheckIsValid();
			this.CopyAnimationStreamMotionInternal(animationStream);
		}

		private void ReadSceneTransforms()
		{
			this.CheckIsValid();
			this.InternalReadSceneTransforms();
		}

		private void WriteSceneTransforms()
		{
			this.CheckIsValid();
			this.InternalWriteSceneTransforms();
		}

		[NativeMethod(Name = "AnimationStreamBindings::CopyAnimationStreamMotion", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void CopyAnimationStreamMotionInternal(AnimationStream animationStream)
		{
			AnimationStream.CopyAnimationStreamMotionInternal_Injected(ref this, ref animationStream);
		}

		[NativeMethod(IsThreadSafe = true)]
		private float GetDeltaTime()
		{
			return AnimationStream.GetDeltaTime_Injected(ref this);
		}

		[NativeMethod(IsThreadSafe = true)]
		private bool GetIsHumanStream()
		{
			return AnimationStream.GetIsHumanStream_Injected(ref this);
		}

		[NativeMethod(Name = "AnimationStreamBindings::GetVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetVelocity()
		{
			Vector3 result;
			AnimationStream.GetVelocity_Injected(ref this, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationStreamBindings::SetVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void SetVelocity(Vector3 velocity)
		{
			AnimationStream.SetVelocity_Injected(ref this, ref velocity);
		}

		[NativeMethod(Name = "AnimationStreamBindings::GetAngularVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetAngularVelocity()
		{
			Vector3 result;
			AnimationStream.GetAngularVelocity_Injected(ref this, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationStreamBindings::SetAngularVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void SetAngularVelocity(Vector3 velocity)
		{
			AnimationStream.SetAngularVelocity_Injected(ref this, ref velocity);
		}

		[NativeMethod(Name = "AnimationStreamBindings::GetRootMotionPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetRootMotionPosition()
		{
			Vector3 result;
			AnimationStream.GetRootMotionPosition_Injected(ref this, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationStreamBindings::GetRootMotionRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion GetRootMotionRotation()
		{
			Quaternion result;
			AnimationStream.GetRootMotionRotation_Injected(ref this, out result);
			return result;
		}

		[NativeMethod(IsThreadSafe = true)]
		private int GetInputStreamCount()
		{
			return AnimationStream.GetInputStreamCount_Injected(ref this);
		}

		[NativeMethod(Name = "GetInputStream", IsThreadSafe = true)]
		private AnimationStream InternalGetInputStream(int index)
		{
			AnimationStream result;
			AnimationStream.InternalGetInputStream_Injected(ref this, index, out result);
			return result;
		}

		[NativeMethod(Name = "GetInputWeight", IsThreadSafe = true)]
		private float InternalGetInputWeight(int index)
		{
			return AnimationStream.InternalGetInputWeight_Injected(ref this, index);
		}

		[NativeMethod(IsThreadSafe = true)]
		private AnimationHumanStream GetHumanStream()
		{
			AnimationHumanStream result;
			AnimationStream.GetHumanStream_Injected(ref this, out result);
			return result;
		}

		[NativeMethod(Name = "ReadSceneTransforms", IsThreadSafe = true)]
		private void InternalReadSceneTransforms()
		{
			AnimationStream.InternalReadSceneTransforms_Injected(ref this);
		}

		[NativeMethod(Name = "WriteSceneTransforms", IsThreadSafe = true)]
		private void InternalWriteSceneTransforms()
		{
			AnimationStream.InternalWriteSceneTransforms_Injected(ref this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyAnimationStreamMotionInternal_Injected(ref AnimationStream _unity_self, ref AnimationStream animationStream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetDeltaTime_Injected(ref AnimationStream _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIsHumanStream_Injected(ref AnimationStream _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVelocity_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVelocity_Injected(ref AnimationStream _unity_self, ref Vector3 velocity);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAngularVelocity_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAngularVelocity_Injected(ref AnimationStream _unity_self, ref Vector3 velocity);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRootMotionPosition_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRootMotionRotation_Injected(ref AnimationStream _unity_self, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetInputStreamCount_Injected(ref AnimationStream _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetInputStream_Injected(ref AnimationStream _unity_self, int index, out AnimationStream ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float InternalGetInputWeight_Injected(ref AnimationStream _unity_self, int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetHumanStream_Injected(ref AnimationStream _unity_self, out AnimationHumanStream ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalReadSceneTransforms_Injected(ref AnimationStream _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalWriteSceneTransforms_Injected(ref AnimationStream _unity_self);
	}
}
