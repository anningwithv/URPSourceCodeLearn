using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationClip.bindings.h"), NativeType("Modules/Animation/AnimationClip.h")]
	public sealed class AnimationClip : Motion
	{
		[NativeProperty("Length", false, TargetType.Function)]
		public extern float length
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("StartTime", false, TargetType.Function)]
		internal extern float startTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("StopTime", false, TargetType.Function)]
		internal extern float stopTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("SampleRate", false, TargetType.Function)]
		public extern float frameRate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("WrapMode", false, TargetType.Function)]
		public extern WrapMode wrapMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("Bounds", false, TargetType.Function)]
		public Bounds localBounds
		{
			get
			{
				Bounds result;
				this.get_localBounds_Injected(out result);
				return result;
			}
			set
			{
				this.set_localBounds_Injected(ref value);
			}
		}

		public new extern bool legacy
		{
			[NativeMethod("IsLegacy")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetLegacy")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool humanMotion
		{
			[NativeMethod("IsHumanMotion")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool empty
		{
			[NativeMethod("IsEmpty")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool hasGenericRootTransform
		{
			[NativeMethod("HasGenericRootTransform")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool hasMotionFloatCurves
		{
			[NativeMethod("HasMotionFloatCurves")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool hasMotionCurves
		{
			[NativeMethod("HasMotionCurves")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool hasRootCurves
		{
			[NativeMethod("HasRootCurves")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal extern bool hasRootMotion
		{
			[FreeFunction(Name = "AnimationClipBindings::Internal_GetHasRootMotion", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public AnimationEvent[] events
		{
			get
			{
				return (AnimationEvent[])this.GetEventsInternal();
			}
			set
			{
				this.SetEventsInternal(value);
			}
		}

		public AnimationClip()
		{
			AnimationClip.Internal_CreateAnimationClip(this);
		}

		[FreeFunction("AnimationClipBindings::Internal_CreateAnimationClip")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateAnimationClip([Writable] AnimationClip self);

		public void SampleAnimation(GameObject go, float time)
		{
			AnimationClip.SampleAnimation(go, this, time, this.wrapMode);
		}

		[FreeFunction, NativeHeader("Modules/Animation/AnimationUtility.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SampleAnimation([NotNull("ArgumentNullException")] GameObject go, [NotNull("ArgumentNullException")] AnimationClip clip, float inTime, WrapMode wrapMode);

		[FreeFunction("AnimationClipBindings::Internal_SetCurve", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetCurve([NotNull("ArgumentNullException")] string relativePath, [NotNull("ArgumentNullException")] Type type, [NotNull("ArgumentNullException")] string propertyName, AnimationCurve curve);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnsureQuaternionContinuity();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearCurves();

		public void AddEvent(AnimationEvent evt)
		{
			bool flag = evt == null;
			if (flag)
			{
				throw new ArgumentNullException("evt");
			}
			this.AddEventInternal(evt);
		}

		[FreeFunction(Name = "AnimationClipBindings::AddEventInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddEventInternal(object evt);

		[FreeFunction(Name = "AnimationClipBindings::SetEventsInternal", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetEventsInternal(Array value);

		[FreeFunction(Name = "AnimationClipBindings::GetEventsInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Array GetEventsInternal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localBounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_localBounds_Injected(ref Bounds value);
	}
}
