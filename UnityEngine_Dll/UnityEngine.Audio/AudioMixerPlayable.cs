using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Audio
{
	[NativeHeader("Runtime/Director/Core/HPlayable.h"), NativeHeader("Modules/Audio/Public/Director/AudioMixerPlayable.h"), NativeHeader("Modules/Audio/Public/ScriptBindings/AudioMixerPlayable.bindings.h"), StaticAccessor("AudioMixerPlayableBindings", StaticAccessorType.DoubleColon), RequiredByNativeCode]
	public struct AudioMixerPlayable : IPlayable, IEquatable<AudioMixerPlayable>
	{
		private PlayableHandle m_Handle;

		public static AudioMixerPlayable Create(PlayableGraph graph, int inputCount = 0, bool normalizeInputVolumes = false)
		{
			PlayableHandle handle = AudioMixerPlayable.CreateHandle(graph, inputCount, normalizeInputVolumes);
			return new AudioMixerPlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount, bool normalizeInputVolumes)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AudioMixerPlayable.CreateAudioMixerPlayableInternal(ref graph, normalizeInputVolumes, ref @null);
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

		internal AudioMixerPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AudioMixerPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AudioMixerPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		public static implicit operator Playable(AudioMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AudioMixerPlayable(Playable playable)
		{
			return new AudioMixerPlayable(playable.GetHandle());
		}

		public bool Equals(AudioMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateAudioMixerPlayableInternal(ref PlayableGraph graph, bool normalizeInputVolumes, ref PlayableHandle handle);
	}
}
