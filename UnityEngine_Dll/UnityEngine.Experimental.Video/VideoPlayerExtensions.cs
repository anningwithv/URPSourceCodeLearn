using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Audio;
using UnityEngine.Video;

namespace UnityEngine.Experimental.Video
{
	[NativeHeader("VideoScriptingClasses.h"), NativeHeader("Modules/Video/Public/VideoPlayer.h"), NativeHeader("Modules/Video/Public/ScriptBindings/VideoPlayerExtensions.bindings.h"), StaticAccessor("VideoPlayerExtensionsBindings", StaticAccessorType.DoubleColon)]
	public static class VideoPlayerExtensions
	{
		public static AudioSampleProvider GetAudioSampleProvider(this VideoPlayer vp, ushort trackIndex)
		{
			ushort controlledAudioTrackCount = vp.controlledAudioTrackCount;
			bool flag = trackIndex >= controlledAudioTrackCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("trackIndex", trackIndex, "VideoPlayer is currently configured with " + controlledAudioTrackCount.ToString() + " tracks.");
			}
			VideoAudioOutputMode audioOutputMode = vp.audioOutputMode;
			bool flag2 = audioOutputMode != VideoAudioOutputMode.APIOnly;
			if (flag2)
			{
				throw new InvalidOperationException("VideoPlayer.GetAudioSampleProvider requires audioOutputMode to be APIOnly. Current: " + audioOutputMode.ToString());
			}
			AudioSampleProvider audioSampleProvider = AudioSampleProvider.Lookup(vp.InternalGetAudioSampleProviderId(trackIndex), vp, trackIndex);
			bool flag3 = audioSampleProvider == null;
			if (flag3)
			{
				throw new InvalidOperationException("VideoPlayer.GetAudioSampleProvider got null provider.");
			}
			bool flag4 = audioSampleProvider.owner != vp;
			if (flag4)
			{
				throw new InvalidOperationException("Internal error: VideoPlayer.GetAudioSampleProvider got provider used by another object.");
			}
			bool flag5 = audioSampleProvider.trackIndex != trackIndex;
			if (flag5)
			{
				throw new InvalidOperationException("Internal error: VideoPlayer.GetAudioSampleProvider got provider for track " + audioSampleProvider.trackIndex.ToString() + " instead of " + trackIndex.ToString());
			}
			return audioSampleProvider;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint InternalGetAudioSampleProviderId([NotNull("NullExceptionObject")] this VideoPlayer vp, ushort trackIndex);
	}
}
