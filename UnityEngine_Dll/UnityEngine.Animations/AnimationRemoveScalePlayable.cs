using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	[NativeHeader("Runtime/Director/Core/HPlayable.h"), NativeHeader("Modules/Animation/Director/AnimationRemoveScalePlayable.h"), NativeHeader("Modules/Animation/ScriptBindings/AnimationRemoveScalePlayable.bindings.h"), StaticAccessor("AnimationRemoveScalePlayableBindings", StaticAccessorType.DoubleColon), RequiredByNativeCode]
	internal struct AnimationRemoveScalePlayable : IPlayable, IEquatable<AnimationRemoveScalePlayable>
	{
		private PlayableHandle m_Handle;

		private static readonly AnimationRemoveScalePlayable m_NullPlayable = new AnimationRemoveScalePlayable(PlayableHandle.Null);

		public static AnimationRemoveScalePlayable Null
		{
			get
			{
				return AnimationRemoveScalePlayable.m_NullPlayable;
			}
		}

		public static AnimationRemoveScalePlayable Create(PlayableGraph graph, int inputCount)
		{
			PlayableHandle handle = AnimationRemoveScalePlayable.CreateHandle(graph, inputCount);
			return new AnimationRemoveScalePlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationRemoveScalePlayable.CreateHandleInternal(graph, ref @null);
			PlayableHandle result;
			if (flag)
			{
				result = PlayableHandle.Null;
			}
			else
			{
				@null.SetInputCount(inputCount);
				result = @null;
			}
			return result;
		}

		internal AnimationRemoveScalePlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationRemoveScalePlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationRemoveScalePlayable.");
				}
			}
			this.m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		public static implicit operator Playable(AnimationRemoveScalePlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AnimationRemoveScalePlayable(Playable playable)
		{
			return new AnimationRemoveScalePlayable(playable.GetHandle());
		}

		public bool Equals(AnimationRemoveScalePlayable other)
		{
			return this.Equals(other.GetHandle());
		}

		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationRemoveScalePlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle);
	}
}
