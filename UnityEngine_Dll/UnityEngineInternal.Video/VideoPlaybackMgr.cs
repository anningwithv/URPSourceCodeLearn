using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngineInternal.Video
{
	[NativeHeader("Modules/Video/Public/Base/VideoMediaPlayback.h"), UsedByNativeCode]
	internal class VideoPlaybackMgr : IDisposable
	{
		public delegate void Callback();

		public delegate void MessageCallback(string message);

		internal IntPtr m_Ptr;

		public extern ulong videoPlaybackCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public VideoPlaybackMgr()
		{
			this.m_Ptr = VideoPlaybackMgr.Internal_Create();
		}

		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				VideoPlaybackMgr.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern VideoPlayback CreateVideoPlayback(string fileName, VideoPlaybackMgr.MessageCallback errorCallback, VideoPlaybackMgr.Callback readyCallback, VideoPlaybackMgr.Callback reachedEndCallback, bool splitAlpha = false);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ReleaseVideoPlayback(VideoPlayback playback);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Update();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ProcessOSMainLoopMessagesForTesting();
	}
}
