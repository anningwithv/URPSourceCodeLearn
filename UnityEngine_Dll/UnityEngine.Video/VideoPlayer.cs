using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Video
{
	[NativeHeader("Modules/Video/Public/VideoPlayer.h"), RequireComponent(typeof(Transform)), RequiredByNativeCode]
	public sealed class VideoPlayer : Behaviour
	{
		public delegate void EventHandler(VideoPlayer source);

		public delegate void ErrorEventHandler(VideoPlayer source, string message);

		public delegate void FrameReadyEventHandler(VideoPlayer source, long frameIdx);

		public delegate void TimeEventHandler(VideoPlayer source, double seconds);

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event VideoPlayer.EventHandler prepareCompleted;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event VideoPlayer.EventHandler loopPointReached;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event VideoPlayer.EventHandler started;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event VideoPlayer.EventHandler frameDropped;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event VideoPlayer.ErrorEventHandler errorReceived;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event VideoPlayer.EventHandler seekCompleted;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event VideoPlayer.TimeEventHandler clockResyncOccurred;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event VideoPlayer.FrameReadyEventHandler frameReady;

		public extern VideoSource source
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("VideoUrl")]
		public extern string url
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("VideoClip")]
		public extern VideoClip clip
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern VideoRenderMode renderMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeHeader("Runtime/Camera/Camera.h")]
		public extern Camera targetCamera
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeHeader("Runtime/Graphics/RenderTexture.h")]
		public extern RenderTexture targetTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeHeader("Runtime/Graphics/Renderer.h")]
		public extern Renderer targetMaterialRenderer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern string targetMaterialProperty
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern VideoAspectRatio aspectRatio
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float targetCameraAlpha
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Video3DLayout targetCamera3DLayout
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeHeader("Runtime/Graphics/Texture.h")]
		public extern Texture texture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isPrepared
		{
			[NativeName("IsPrepared")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool waitForFirstFrame
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool playOnAwake
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isPlaying
		{
			[NativeName("IsPlaying")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isPaused
		{
			[NativeName("IsPaused")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool canSetTime
		{
			[NativeName("CanSetTime")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeName("SecPosition")]
		public extern double time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("FramePosition")]
		public extern long frame
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern double clockTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool canStep
		{
			[NativeName("CanStep")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool canSetPlaybackSpeed
		{
			[NativeName("CanSetPlaybackSpeed")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float playbackSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("Loop")]
		public extern bool isLooping
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool canSetTimeSource
		{
			[NativeName("CanSetTimeSource")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern VideoTimeSource timeSource
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern VideoTimeReference timeReference
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern double externalReferenceTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool canSetSkipOnDrop
		{
			[NativeName("CanSetSkipOnDrop")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool skipOnDrop
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ulong frameCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float frameRate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeName("Duration")]
		public extern double length
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint width
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint height
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint pixelAspectRatioNumerator
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint pixelAspectRatioDenominator
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ushort audioTrackCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ushort controlledAudioTrackMaxCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public ushort controlledAudioTrackCount
		{
			get
			{
				return this.GetControlledAudioTrackCount();
			}
			set
			{
				int controlledAudioTrackMaxCount = (int)VideoPlayer.controlledAudioTrackMaxCount;
				bool flag = (int)value > controlledAudioTrackMaxCount;
				if (flag)
				{
					throw new ArgumentException(string.Format("Cannot control more than {0} tracks.", controlledAudioTrackMaxCount), "value");
				}
				this.SetControlledAudioTrackCount(value);
			}
		}

		public extern VideoAudioOutputMode audioOutputMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool canSetDirectAudioVolume
		{
			[NativeName("CanSetDirectAudioVolume")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool sendFrameReadyEvents
		{
			[NativeName("AreFrameReadyEventsEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("EnableFrameReadyEvents")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Prepare();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Pause();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StepForward();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetAudioLanguageCode(ushort trackIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ushort GetAudioChannelCount(ushort trackIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetAudioSampleRate(ushort trackIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ushort GetControlledAudioTrackCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetControlledAudioTrackCount(ushort value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnableAudioTrack(ushort trackIndex, bool enabled);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsAudioTrackEnabled(ushort trackIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetDirectAudioVolume(ushort trackIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDirectAudioVolume(ushort trackIndex, float volume);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetDirectAudioMute(ushort trackIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDirectAudioMute(ushort trackIndex, bool mute);

		[NativeHeader("Modules/Audio/Public/AudioSource.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AudioSource GetTargetAudioSource(ushort trackIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTargetAudioSource(ushort trackIndex, AudioSource source);

		[RequiredByNativeCode]
		private static void InvokePrepareCompletedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.prepareCompleted != null;
			if (flag)
			{
				source.prepareCompleted(source);
			}
		}

		[RequiredByNativeCode]
		private static void InvokeFrameReadyCallback_Internal(VideoPlayer source, long frameIdx)
		{
			bool flag = source.frameReady != null;
			if (flag)
			{
				source.frameReady(source, frameIdx);
			}
		}

		[RequiredByNativeCode]
		private static void InvokeLoopPointReachedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.loopPointReached != null;
			if (flag)
			{
				source.loopPointReached(source);
			}
		}

		[RequiredByNativeCode]
		private static void InvokeStartedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.started != null;
			if (flag)
			{
				source.started(source);
			}
		}

		[RequiredByNativeCode]
		private static void InvokeFrameDroppedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.frameDropped != null;
			if (flag)
			{
				source.frameDropped(source);
			}
		}

		[RequiredByNativeCode]
		private static void InvokeErrorReceivedCallback_Internal(VideoPlayer source, string errorStr)
		{
			bool flag = source.errorReceived != null;
			if (flag)
			{
				source.errorReceived(source, errorStr);
			}
		}

		[RequiredByNativeCode]
		private static void InvokeSeekCompletedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.seekCompleted != null;
			if (flag)
			{
				source.seekCompleted(source);
			}
		}

		[RequiredByNativeCode]
		private static void InvokeClockResyncOccurredCallback_Internal(VideoPlayer source, double seconds)
		{
			bool flag = source.clockResyncOccurred != null;
			if (flag)
			{
				source.clockResyncOccurred(source, seconds);
			}
		}
	}
}
