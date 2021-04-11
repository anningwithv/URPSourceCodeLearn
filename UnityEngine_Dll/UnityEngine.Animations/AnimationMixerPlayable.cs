using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationMixerPlayable.bindings.h"), NativeHeader("Runtime/Director/Core/HPlayable.h"), NativeHeader("Modules/Animation/Director/AnimationMixerPlayable.h"), StaticAccessor("AnimationMixerPlayableBindings", StaticAccessorType.DoubleColon), RequiredByNativeCode]
	public struct AnimationMixerPlayable : IPlayable, IEquatable<AnimationMixerPlayable>
	{
		private PlayableHandle m_Handle;

		private static readonly AnimationMixerPlayable m_NullPlayable = new AnimationMixerPlayable(PlayableHandle.Null);

		public static AnimationMixerPlayable Null
		{
			get
			{
				return AnimationMixerPlayable.m_NullPlayable;
			}
		}

		public static AnimationMixerPlayable Create(PlayableGraph graph, int inputCount = 0, bool normalizeWeights = false)
		{
			PlayableHandle handle = AnimationMixerPlayable.CreateHandle(graph, inputCount, normalizeWeights);
			return new AnimationMixerPlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount = 0, bool normalizeWeights = false)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationMixerPlayable.CreateHandleInternal(graph, normalizeWeights, ref @null);
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

		internal AnimationMixerPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationMixerPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationMixerPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		public static implicit operator Playable(AnimationMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AnimationMixerPlayable(Playable playable)
		{
			return new AnimationMixerPlayable(playable.GetHandle());
		}

		public bool Equals(AnimationMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, bool normalizeWeights, ref PlayableHandle handle)
		{
			return AnimationMixerPlayable.CreateHandleInternal_Injected(ref graph, normalizeWeights, ref handle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, bool normalizeWeights, ref PlayableHandle handle);
	}
}
