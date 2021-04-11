using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	[NativeHeader("Modules/Animation/Director/AnimationClipPlayable.h"), NativeHeader("Modules/Animation/ScriptBindings/AnimationClipPlayable.bindings.h"), StaticAccessor("AnimationClipPlayableBindings", StaticAccessorType.DoubleColon), RequiredByNativeCode]
	public struct AnimationClipPlayable : IPlayable, IEquatable<AnimationClipPlayable>
	{
		private PlayableHandle m_Handle;

		public static AnimationClipPlayable Create(PlayableGraph graph, AnimationClip clip)
		{
			PlayableHandle handle = AnimationClipPlayable.CreateHandle(graph, clip);
			return new AnimationClipPlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, AnimationClip clip)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationClipPlayable.CreateHandleInternal(graph, clip, ref @null);
			PlayableHandle result;
			if (flag)
			{
				result = PlayableHandle.Null;
			}
			else
			{
				result = @null;
			}
			return result;
		}

		internal AnimationClipPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationClipPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationClipPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		public static implicit operator Playable(AnimationClipPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AnimationClipPlayable(Playable playable)
		{
			return new AnimationClipPlayable(playable.GetHandle());
		}

		public bool Equals(AnimationClipPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		public AnimationClip GetAnimationClip()
		{
			return AnimationClipPlayable.GetAnimationClipInternal(ref this.m_Handle);
		}

		public bool GetApplyFootIK()
		{
			return AnimationClipPlayable.GetApplyFootIKInternal(ref this.m_Handle);
		}

		public void SetApplyFootIK(bool value)
		{
			AnimationClipPlayable.SetApplyFootIKInternal(ref this.m_Handle, value);
		}

		public bool GetApplyPlayableIK()
		{
			return AnimationClipPlayable.GetApplyPlayableIKInternal(ref this.m_Handle);
		}

		public void SetApplyPlayableIK(bool value)
		{
			AnimationClipPlayable.SetApplyPlayableIKInternal(ref this.m_Handle, value);
		}

		internal bool GetRemoveStartOffset()
		{
			return AnimationClipPlayable.GetRemoveStartOffsetInternal(ref this.m_Handle);
		}

		internal void SetRemoveStartOffset(bool value)
		{
			AnimationClipPlayable.SetRemoveStartOffsetInternal(ref this.m_Handle, value);
		}

		internal bool GetOverrideLoopTime()
		{
			return AnimationClipPlayable.GetOverrideLoopTimeInternal(ref this.m_Handle);
		}

		internal void SetOverrideLoopTime(bool value)
		{
			AnimationClipPlayable.SetOverrideLoopTimeInternal(ref this.m_Handle, value);
		}

		internal bool GetLoopTime()
		{
			return AnimationClipPlayable.GetLoopTimeInternal(ref this.m_Handle);
		}

		internal void SetLoopTime(bool value)
		{
			AnimationClipPlayable.SetLoopTimeInternal(ref this.m_Handle, value);
		}

		internal float GetSampleRate()
		{
			return AnimationClipPlayable.GetSampleRateInternal(ref this.m_Handle);
		}

		internal void SetSampleRate(float value)
		{
			AnimationClipPlayable.SetSampleRateInternal(ref this.m_Handle, value);
		}

		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, AnimationClip clip, ref PlayableHandle handle)
		{
			return AnimationClipPlayable.CreateHandleInternal_Injected(ref graph, clip, ref handle);
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationClip GetAnimationClipInternal(ref PlayableHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetApplyFootIKInternal(ref PlayableHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetApplyFootIKInternal(ref PlayableHandle handle, bool value);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetApplyPlayableIKInternal(ref PlayableHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetApplyPlayableIKInternal(ref PlayableHandle handle, bool value);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetRemoveStartOffsetInternal(ref PlayableHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRemoveStartOffsetInternal(ref PlayableHandle handle, bool value);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetOverrideLoopTimeInternal(ref PlayableHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetOverrideLoopTimeInternal(ref PlayableHandle handle, bool value);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetLoopTimeInternal(ref PlayableHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLoopTimeInternal(ref PlayableHandle handle, bool value);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetSampleRateInternal(ref PlayableHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSampleRateInternal(ref PlayableHandle handle, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, AnimationClip clip, ref PlayableHandle handle);
	}
}
