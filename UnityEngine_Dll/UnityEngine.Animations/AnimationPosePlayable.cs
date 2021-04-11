using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPosePlayable.bindings.h"), NativeHeader("Modules/Animation/Director/AnimationPosePlayable.h"), NativeHeader("Runtime/Director/Core/HPlayable.h"), StaticAccessor("AnimationPosePlayableBindings", StaticAccessorType.DoubleColon), RequiredByNativeCode]
	internal struct AnimationPosePlayable : IPlayable, IEquatable<AnimationPosePlayable>
	{
		private PlayableHandle m_Handle;

		private static readonly AnimationPosePlayable m_NullPlayable = new AnimationPosePlayable(PlayableHandle.Null);

		public static AnimationPosePlayable Null
		{
			get
			{
				return AnimationPosePlayable.m_NullPlayable;
			}
		}

		public static AnimationPosePlayable Create(PlayableGraph graph)
		{
			PlayableHandle handle = AnimationPosePlayable.CreateHandle(graph);
			return new AnimationPosePlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationPosePlayable.CreateHandleInternal(graph, ref @null);
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

		internal AnimationPosePlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationPosePlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationPosePlayable.");
				}
			}
			this.m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		public static implicit operator Playable(AnimationPosePlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AnimationPosePlayable(Playable playable)
		{
			return new AnimationPosePlayable(playable.GetHandle());
		}

		public bool Equals(AnimationPosePlayable other)
		{
			return this.Equals(other.GetHandle());
		}

		public bool GetMustReadPreviousPose()
		{
			return AnimationPosePlayable.GetMustReadPreviousPoseInternal(ref this.m_Handle);
		}

		public void SetMustReadPreviousPose(bool value)
		{
			AnimationPosePlayable.SetMustReadPreviousPoseInternal(ref this.m_Handle, value);
		}

		public bool GetReadDefaultPose()
		{
			return AnimationPosePlayable.GetReadDefaultPoseInternal(ref this.m_Handle);
		}

		public void SetReadDefaultPose(bool value)
		{
			AnimationPosePlayable.SetReadDefaultPoseInternal(ref this.m_Handle, value);
		}

		public bool GetApplyFootIK()
		{
			return AnimationPosePlayable.GetApplyFootIKInternal(ref this.m_Handle);
		}

		public void SetApplyFootIK(bool value)
		{
			AnimationPosePlayable.SetApplyFootIKInternal(ref this.m_Handle, value);
		}

		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationPosePlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetMustReadPreviousPoseInternal(ref PlayableHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMustReadPreviousPoseInternal(ref PlayableHandle handle, bool value);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetReadDefaultPoseInternal(ref PlayableHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetReadDefaultPoseInternal(ref PlayableHandle handle, bool value);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetApplyFootIKInternal(ref PlayableHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetApplyFootIKInternal(ref PlayableHandle handle, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle);
	}
}
