using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Audio;
using UnityEngine.Scripting;

namespace UnityEngineInternal.Video
{
	[NativeHeader("Modules/Video/Public/Base/MediaComponent.h"), UsedByNativeCode]
	internal class VideoPlayback
	{
		public delegate void Callback();

		internal IntPtr m_Ptr;

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StartPlayback();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PausePlayback();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopPlayback();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern VideoError GetStatus();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsReady();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsPlaying();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Step();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool CanStep();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetWidth();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetHeight();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetFrameRate();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetDuration();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ulong GetFrameCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetPixelAspectRatioNumerator();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetPixelAspectRatioDenominator();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern VideoPixelFormat GetPixelFormat();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool CanNotSkipOnDrop();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetSkipOnDrop(bool skipOnDrop);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetTexture(Texture texture, out long outputFrameNum);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SeekToFrame(long frameIndex, VideoPlayback.Callback seekCompletedCallback);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SeekToTime(double secs, VideoPlayback.Callback seekCompletedCallback);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetPlaybackSpeed();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPlaybackSpeed(float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetLoop();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLoop(bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAdjustToLinearSpace(bool enable);

		[NativeHeader("Modules/Audio/Public/AudioSource.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ushort GetAudioTrackCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ushort GetAudioChannelCount(ushort trackIdx);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetAudioSampleRate(ushort trackIdx);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAudioTarget(ushort trackIdx, bool enabled, bool softwareOutput, AudioSource audioSource);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern uint GetAudioSampleProviderId(ushort trackIndex);

		public AudioSampleProvider GetAudioSampleProvider(ushort trackIndex)
		{
			bool flag = trackIndex >= this.GetAudioTrackCount();
			if (flag)
			{
				throw new ArgumentOutOfRangeException("trackIndex", trackIndex, "VideoPlayback has " + this.GetAudioTrackCount().ToString() + " tracks.");
			}
			AudioSampleProvider audioSampleProvider = AudioSampleProvider.Lookup(this.GetAudioSampleProviderId(trackIndex), null, trackIndex);
			bool flag2 = audioSampleProvider == null;
			if (flag2)
			{
				throw new InvalidOperationException("VideoPlayback.GetAudioSampleProvider got null provider.");
			}
			bool flag3 = audioSampleProvider.owner != null;
			if (flag3)
			{
				throw new InvalidOperationException("Internal error: VideoPlayback.GetAudioSampleProvider got unexpected non-null provider owner.");
			}
			bool flag4 = audioSampleProvider.trackIndex != trackIndex;
			if (flag4)
			{
				throw new InvalidOperationException("Internal error: VideoPlayback.GetAudioSampleProvider got provider for track " + audioSampleProvider.trackIndex.ToString() + " instead of " + trackIndex.ToString());
			}
			return audioSampleProvider;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool PlatformSupportsH265();
	}
}
