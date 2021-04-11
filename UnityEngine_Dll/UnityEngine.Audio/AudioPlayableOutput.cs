using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Audio
{
	[NativeHeader("Modules/Audio/Public/AudioSource.h"), NativeHeader("Modules/Audio/Public/Director/AudioPlayableOutput.h"), NativeHeader("Modules/Audio/Public/ScriptBindings/AudioPlayableOutput.bindings.h"), StaticAccessor("AudioPlayableOutputBindings", StaticAccessorType.DoubleColon), RequiredByNativeCode]
	public struct AudioPlayableOutput : IPlayableOutput
	{
		private PlayableOutputHandle m_Handle;

		public static AudioPlayableOutput Null
		{
			get
			{
				return new AudioPlayableOutput(PlayableOutputHandle.Null);
			}
		}

		public static AudioPlayableOutput Create(PlayableGraph graph, string name, AudioSource target)
		{
			PlayableOutputHandle handle;
			bool flag = !AudioPlayableGraphExtensions.InternalCreateAudioOutput(ref graph, name, out handle);
			AudioPlayableOutput result;
			if (flag)
			{
				result = AudioPlayableOutput.Null;
			}
			else
			{
				AudioPlayableOutput audioPlayableOutput = new AudioPlayableOutput(handle);
				audioPlayableOutput.SetTarget(target);
				result = audioPlayableOutput;
			}
			return result;
		}

		internal AudioPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<AudioPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AudioPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		public static implicit operator PlayableOutput(AudioPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		public static explicit operator AudioPlayableOutput(PlayableOutput output)
		{
			return new AudioPlayableOutput(output.GetHandle());
		}

		public AudioSource GetTarget()
		{
			return AudioPlayableOutput.InternalGetTarget(ref this.m_Handle);
		}

		public void SetTarget(AudioSource value)
		{
			AudioPlayableOutput.InternalSetTarget(ref this.m_Handle, value);
		}

		public bool GetEvaluateOnSeek()
		{
			return AudioPlayableOutput.InternalGetEvaluateOnSeek(ref this.m_Handle);
		}

		public void SetEvaluateOnSeek(bool value)
		{
			AudioPlayableOutput.InternalSetEvaluateOnSeek(ref this.m_Handle, value);
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioSource InternalGetTarget(ref PlayableOutputHandle output);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetTarget(ref PlayableOutputHandle output, AudioSource target);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalGetEvaluateOnSeek(ref PlayableOutputHandle output);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetEvaluateOnSeek(ref PlayableOutputHandle output, bool value);
	}
}
