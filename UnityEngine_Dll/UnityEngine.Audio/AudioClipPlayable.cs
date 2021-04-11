using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Audio
{
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioClipPlayable.bindings.h"), NativeHeader("Runtime/Director/Core/HPlayable.h"), NativeHeader("Modules/Audio/Public/Director/AudioClipPlayable.h"), StaticAccessor("AudioClipPlayableBindings", StaticAccessorType.DoubleColon), RequiredByNativeCode]
	public struct AudioClipPlayable : IPlayable, IEquatable<AudioClipPlayable>
	{
		private PlayableHandle m_Handle;

		public static AudioClipPlayable Create(PlayableGraph graph, AudioClip clip, bool looping)
		{
			PlayableHandle handle = AudioClipPlayable.CreateHandle(graph, clip, looping);
			AudioClipPlayable audioClipPlayable = new AudioClipPlayable(handle);
			bool flag = clip != null;
			if (flag)
			{
				audioClipPlayable.SetDuration((double)clip.length);
			}
			return audioClipPlayable;
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, AudioClip clip, bool looping)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AudioClipPlayable.InternalCreateAudioClipPlayable(ref graph, clip, looping, ref @null);
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

		internal AudioClipPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AudioClipPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AudioClipPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		public static implicit operator Playable(AudioClipPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AudioClipPlayable(Playable playable)
		{
			return new AudioClipPlayable(playable.GetHandle());
		}

		public bool Equals(AudioClipPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		public AudioClip GetClip()
		{
			return AudioClipPlayable.GetClipInternal(ref this.m_Handle);
		}

		public void SetClip(AudioClip value)
		{
			AudioClipPlayable.SetClipInternal(ref this.m_Handle, value);
		}

		public bool GetLooped()
		{
			return AudioClipPlayable.GetLoopedInternal(ref this.m_Handle);
		}

		public void SetLooped(bool value)
		{
			AudioClipPlayable.SetLoopedInternal(ref this.m_Handle, value);
		}

		internal float GetVolume()
		{
			return AudioClipPlayable.GetVolumeInternal(ref this.m_Handle);
		}

		internal void SetVolume(float value)
		{
			bool flag = value < 0f || value > 1f;
			if (flag)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable volume outside of range (0.0 - 1.0): " + value.ToString());
			}
			AudioClipPlayable.SetVolumeInternal(ref this.m_Handle, value);
		}

		internal float GetStereoPan()
		{
			return AudioClipPlayable.GetStereoPanInternal(ref this.m_Handle);
		}

		internal void SetStereoPan(float value)
		{
			bool flag = value < -1f || value > 1f;
			if (flag)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable stereo pan outside of range (-1.0 - 1.0): " + value.ToString());
			}
			AudioClipPlayable.SetStereoPanInternal(ref this.m_Handle, value);
		}

		internal float GetSpatialBlend()
		{
			return AudioClipPlayable.GetSpatialBlendInternal(ref this.m_Handle);
		}

		internal void SetSpatialBlend(float value)
		{
			bool flag = value < 0f || value > 1f;
			if (flag)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable spatial blend outside of range (0.0 - 1.0): " + value.ToString());
			}
			AudioClipPlayable.SetSpatialBlendInternal(ref this.m_Handle, value);
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("IsPlaying() has been deprecated. Use IsChannelPlaying() instead (UnityUpgradable) -> IsChannelPlaying()", true)]
		public bool IsPlaying()
		{
			return this.IsChannelPlaying();
		}

		public bool IsChannelPlaying()
		{
			return AudioClipPlayable.GetIsChannelPlayingInternal(ref this.m_Handle);
		}

		public double GetStartDelay()
		{
			return AudioClipPlayable.GetStartDelayInternal(ref this.m_Handle);
		}

		internal void SetStartDelay(double value)
		{
			AudioClipPlayable.SetStartDelayInternal(ref this.m_Handle, value);
		}

		public double GetPauseDelay()
		{
			return AudioClipPlayable.GetPauseDelayInternal(ref this.m_Handle);
		}

		internal void GetPauseDelay(double value)
		{
			double pauseDelayInternal = AudioClipPlayable.GetPauseDelayInternal(ref this.m_Handle);
			bool flag = this.m_Handle.GetPlayState() == PlayState.Playing && (value < 0.05 || (pauseDelayInternal != 0.0 && pauseDelayInternal < 0.05));
			if (flag)
			{
				throw new ArgumentException("AudioClipPlayable.pauseDelay: Setting new delay when existing delay is too small or 0.0 (" + pauseDelayInternal.ToString() + "), audio system will not be able to change in time");
			}
			AudioClipPlayable.SetPauseDelayInternal(ref this.m_Handle, value);
		}

		public void Seek(double startTime, double startDelay)
		{
			this.Seek(startTime, startDelay, 0.0);
		}

		public void Seek(double startTime, double startDelay, [DefaultValue("0")] double duration)
		{
			AudioClipPlayable.SetStartDelayInternal(ref this.m_Handle, startDelay);
			bool flag = duration > 0.0;
			if (flag)
			{
				this.m_Handle.SetDuration(duration + startTime);
				AudioClipPlayable.SetPauseDelayInternal(ref this.m_Handle, startDelay + duration);
			}
			else
			{
				this.m_Handle.SetDuration(1.7976931348623157E+308);
				AudioClipPlayable.SetPauseDelayInternal(ref this.m_Handle, 0.0);
			}
			this.m_Handle.SetTime(startTime);
			this.m_Handle.Play();
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioClip GetClipInternal(ref PlayableHandle hdl);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetClipInternal(ref PlayableHandle hdl, AudioClip clip);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetLoopedInternal(ref PlayableHandle hdl);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLoopedInternal(ref PlayableHandle hdl, bool looped);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetVolumeInternal(ref PlayableHandle hdl);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVolumeInternal(ref PlayableHandle hdl, float volume);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetStereoPanInternal(ref PlayableHandle hdl);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStereoPanInternal(ref PlayableHandle hdl, float stereoPan);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetSpatialBlendInternal(ref PlayableHandle hdl);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSpatialBlendInternal(ref PlayableHandle hdl, float spatialBlend);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIsChannelPlayingInternal(ref PlayableHandle hdl);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetStartDelayInternal(ref PlayableHandle hdl);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStartDelayInternal(ref PlayableHandle hdl, double delay);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetPauseDelayInternal(ref PlayableHandle hdl);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPauseDelayInternal(ref PlayableHandle hdl, double delay);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalCreateAudioClipPlayable(ref PlayableGraph graph, AudioClip clip, bool looping, ref PlayableHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidateType(ref PlayableHandle hdl);
	}
}
