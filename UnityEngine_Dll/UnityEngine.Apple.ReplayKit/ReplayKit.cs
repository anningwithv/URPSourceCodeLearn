using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.Apple.ReplayKit
{
	[NativeHeader("PlatformDependent/iPhonePlayer/IOSScriptBindings.h")]
	public static class ReplayKit
	{
		public delegate void BroadcastStatusCallback(bool hasStarted, string errorMessage);

		public static extern bool APIAvailable
		{
			[FreeFunction("UnityReplayKitAPIAvailable"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool broadcastingAPIAvailable
		{
			[FreeFunction("UnityReplayKitBroadcastingAPIAvailable"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool recordingAvailable
		{
			[FreeFunction("UnityReplayKitRecordingAvailable"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool isRecording
		{
			[FreeFunction("UnityReplayKitIsRecording"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool isBroadcasting
		{
			[FreeFunction("UnityReplayKitIsBroadcasting"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool isBroadcastingPaused
		{
			[FreeFunction("UnityReplayKitIsBroadcastingPaused"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool isPreviewControllerActive
		{
			[FreeFunction("UnityReplayKitIsPreviewControllerActive"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool cameraEnabled
		{
			[FreeFunction("UnityReplayKitIsCameraEnabled"), NativeConditional("PLATFORM_IOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("UnityReplayKitSetCameraEnabled"), NativeConditional("PLATFORM_IOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool microphoneEnabled
		{
			[FreeFunction("UnityReplayKitIsMicrophoneEnabled"), NativeConditional("PLATFORM_IOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("UnityReplayKitSetMicrophoneEnabled"), NativeConditional("PLATFORM_IOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern string broadcastURL
		{
			[FreeFunction("UnityReplayKitGetBroadcastURL"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string lastError
		{
			[FreeFunction("UnityReplayKitLastError"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[FreeFunction("ReplayKitScripting::StartRecording"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool StartRecordingImpl(bool enableMicrophone, bool enableCamera);

		[FreeFunction("ReplayKitScripting::StartBroadcasting"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StartBroadcastingImpl(ReplayKit.BroadcastStatusCallback callback, bool enableMicrophone, bool enableCamera);

		public static bool StartRecording([DefaultValue("false")] bool enableMicrophone, [DefaultValue("false")] bool enableCamera)
		{
			return ReplayKit.StartRecordingImpl(enableMicrophone, enableCamera);
		}

		public static bool StartRecording(bool enableMicrophone)
		{
			return ReplayKit.StartRecording(enableMicrophone, false);
		}

		public static bool StartRecording()
		{
			return ReplayKit.StartRecording(false, false);
		}

		public static void StartBroadcasting(ReplayKit.BroadcastStatusCallback callback, [DefaultValue("false")] bool enableMicrophone, [DefaultValue("false")] bool enableCamera)
		{
			ReplayKit.StartBroadcastingImpl(callback, enableMicrophone, enableCamera);
		}

		public static void StartBroadcasting(ReplayKit.BroadcastStatusCallback callback, bool enableMicrophone)
		{
			ReplayKit.StartBroadcasting(callback, enableMicrophone, false);
		}

		public static void StartBroadcasting(ReplayKit.BroadcastStatusCallback callback)
		{
			ReplayKit.StartBroadcasting(callback, false, false);
		}

		[FreeFunction("UnityReplayKitStopRecording"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool StopRecording();

		[FreeFunction("UnityReplayKitStopBroadcasting"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StopBroadcasting();

		[FreeFunction("UnityReplayKitPauseBroadcasting"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PauseBroadcasting();

		[FreeFunction("UnityReplayKitResumeBroadcasting"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResumeBroadcasting();

		[FreeFunction("UnityReplayKitPreview"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Preview();

		[FreeFunction("UnityReplayKitDiscard"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Discard();

		public static bool ShowCameraPreviewAt(float posX, float posY)
		{
			return ReplayKit.ShowCameraPreviewAt(posX, posY, -1f, -1f);
		}

		[FreeFunction("UnityReplayKitShowCameraPreviewAt"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool ShowCameraPreviewAt(float posX, float posY, float width, float height);

		[FreeFunction("UnityReplayKitHideCameraPreview"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void HideCameraPreview();
	}
}
