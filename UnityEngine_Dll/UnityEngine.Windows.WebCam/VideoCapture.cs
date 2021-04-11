using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	[NativeHeader("PlatformDependent/Win/Webcam/VideoCaptureBindings.h"), StaticAccessor("VideoCaptureBindings", StaticAccessorType.DoubleColon), MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[StructLayout(LayoutKind.Sequential)]
	public class VideoCapture : IDisposable
	{
		public enum CaptureResultType
		{
			Success,
			UnknownError
		}

		public enum AudioState
		{
			MicAudio,
			ApplicationAudio,
			ApplicationAndMicAudio,
			None
		}

		public struct VideoCaptureResult
		{
			public VideoCapture.CaptureResultType resultType;

			public long hResult;

			public bool success
			{
				get
				{
					return this.resultType == VideoCapture.CaptureResultType.Success;
				}
			}
		}

		public delegate void OnVideoCaptureResourceCreatedCallback(VideoCapture captureObject);

		public delegate void OnVideoModeStartedCallback(VideoCapture.VideoCaptureResult result);

		public delegate void OnVideoModeStoppedCallback(VideoCapture.VideoCaptureResult result);

		public delegate void OnStartedRecordingVideoCallback(VideoCapture.VideoCaptureResult result);

		public delegate void OnStoppedRecordingVideoCallback(VideoCapture.VideoCaptureResult result);

		internal IntPtr m_NativePtr;

		private static Resolution[] s_SupportedResolutions;

		private static readonly long HR_SUCCESS = 0L;

		public static IEnumerable<Resolution> SupportedResolutions
		{
			get
			{
				bool flag = VideoCapture.s_SupportedResolutions == null;
				if (flag)
				{
					VideoCapture.s_SupportedResolutions = VideoCapture.GetSupportedResolutions_Internal();
				}
				return VideoCapture.s_SupportedResolutions;
			}
		}

		public extern bool IsRecording
		{
			[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeMethod("VideoCaptureBindings::IsRecording", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		private static VideoCapture.VideoCaptureResult MakeCaptureResult(VideoCapture.CaptureResultType resultType, long hResult)
		{
			return new VideoCapture.VideoCaptureResult
			{
				resultType = resultType,
				hResult = hResult
			};
		}

		private static VideoCapture.VideoCaptureResult MakeCaptureResult(long hResult)
		{
			VideoCapture.VideoCaptureResult result = default(VideoCapture.VideoCaptureResult);
			bool flag = hResult == VideoCapture.HR_SUCCESS;
			VideoCapture.CaptureResultType resultType;
			if (flag)
			{
				resultType = VideoCapture.CaptureResultType.Success;
			}
			else
			{
				resultType = VideoCapture.CaptureResultType.UnknownError;
			}
			result.resultType = resultType;
			result.hResult = hResult;
			return result;
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("GetSupportedResolutions")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Resolution[] GetSupportedResolutions_Internal();

		public static IEnumerable<float> GetSupportedFrameRatesForResolution(Resolution resolution)
		{
			return VideoCapture.GetSupportedFrameRatesForResolution_Internal(resolution.width, resolution.height);
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("GetSupportedFrameRatesForResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float[] GetSupportedFrameRatesForResolution_Internal(int resolutionWidth, int resolutionHeight);

		public static void CreateAsync(bool showHolograms, VideoCapture.OnVideoCaptureResourceCreatedCallback onCreatedCallback)
		{
			bool flag = onCreatedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCreatedCallback");
			}
			showHolograms = false;
			VideoCapture.Instantiate_Internal(showHolograms, onCreatedCallback);
		}

		public static void CreateAsync(VideoCapture.OnVideoCaptureResourceCreatedCallback onCreatedCallback)
		{
			bool flag = onCreatedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCreatedCallback");
			}
			VideoCapture.Instantiate_Internal(false, onCreatedCallback);
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("Instantiate")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Instantiate_Internal(bool showHolograms, VideoCapture.OnVideoCaptureResourceCreatedCallback onCreatedCallback);

		[RequiredByNativeCode]
		private static void InvokeOnCreatedVideoCaptureResourceDelegate(VideoCapture.OnVideoCaptureResourceCreatedCallback callback, IntPtr nativePtr)
		{
			bool flag = nativePtr == IntPtr.Zero;
			if (flag)
			{
				callback(null);
			}
			else
			{
				callback(new VideoCapture(nativePtr));
			}
		}

		private VideoCapture(IntPtr nativeCaptureObject)
		{
			this.m_NativePtr = nativeCaptureObject;
		}

		public void StartVideoModeAsync(CameraParameters setupParams, VideoCapture.AudioState audioState, VideoCapture.OnVideoModeStartedCallback onVideoModeStartedCallback)
		{
			bool flag = onVideoModeStartedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onVideoModeStartedCallback");
			}
			bool flag2 = setupParams.cameraResolutionWidth == 0 || setupParams.cameraResolutionHeight == 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("setupParams", "The camera resolution must be set to a supported resolution.");
			}
			bool flag3 = setupParams.frameRate == 0f;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("setupParams", "The camera frame rate must be set to a supported recording frame rate.");
			}
			this.StartVideoMode_Internal(setupParams, audioState, onVideoModeStartedCallback);
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeMethod("VideoCaptureBindings::StartVideoMode", HasExplicitThis = true)]
		private void StartVideoMode_Internal(CameraParameters cameraParameters, VideoCapture.AudioState audioState, VideoCapture.OnVideoModeStartedCallback onVideoModeStartedCallback)
		{
			this.StartVideoMode_Internal_Injected(ref cameraParameters, audioState, onVideoModeStartedCallback);
		}

		[RequiredByNativeCode]
		private static void InvokeOnVideoModeStartedDelegate(VideoCapture.OnVideoModeStartedCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeMethod("VideoCaptureBindings::StopVideoMode", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopVideoModeAsync([NotNull("ArgumentNullException")] VideoCapture.OnVideoModeStoppedCallback onVideoModeStoppedCallback);

		[RequiredByNativeCode]
		private static void InvokeOnVideoModeStoppedDelegate(VideoCapture.OnVideoModeStoppedCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		public void StartRecordingAsync(string filename, VideoCapture.OnStartedRecordingVideoCallback onStartedRecordingVideoCallback)
		{
			bool flag = onStartedRecordingVideoCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onStartedRecordingVideoCallback");
			}
			bool flag2 = string.IsNullOrEmpty(filename);
			if (flag2)
			{
				throw new ArgumentNullException("filename");
			}
			string directoryName = Path.GetDirectoryName(filename);
			bool flag3 = !string.IsNullOrEmpty(directoryName) && !System.IO.Directory.Exists(directoryName);
			if (flag3)
			{
				throw new ArgumentException("The specified directory does not exist.", "filename");
			}
			FileInfo fileInfo = new FileInfo(filename);
			bool flag4 = fileInfo.Exists && fileInfo.IsReadOnly;
			if (flag4)
			{
				throw new ArgumentException("Cannot write to the file because it is read-only.", "filename");
			}
			this.StartRecordingVideoToDisk_Internal(fileInfo.FullName, onStartedRecordingVideoCallback);
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeMethod("VideoCaptureBindings::StartRecordingVideoToDisk", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StartRecordingVideoToDisk_Internal(string filename, VideoCapture.OnStartedRecordingVideoCallback onStartedRecordingVideoCallback);

		[RequiredByNativeCode]
		private static void InvokeOnStartedRecordingVideoToDiskDelegate(VideoCapture.OnStartedRecordingVideoCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeMethod("VideoCaptureBindings::StopRecordingVideoToDisk", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopRecordingAsync([NotNull("ArgumentNullException")] VideoCapture.OnStoppedRecordingVideoCallback onStoppedRecordingVideoCallback);

		[RequiredByNativeCode]
		private static void InvokeOnStoppedRecordingVideoToDiskDelegate(VideoCapture.OnStoppedRecordingVideoCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeMethod("VideoCaptureBindings::GetUnsafePointerToVideoDeviceController", HasExplicitThis = true), ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetUnsafePointerToVideoDeviceController();

		public void Dispose()
		{
			bool flag = this.m_NativePtr != IntPtr.Zero;
			if (flag)
			{
				this.Dispose_Internal();
				this.m_NativePtr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeMethod("VideoCaptureBindings::Dispose", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Dispose_Internal();

		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_NativePtr != IntPtr.Zero;
				if (flag)
				{
					this.DisposeThreaded_Internal();
					this.m_NativePtr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeMethod("VideoCaptureBindings::DisposeThreaded", HasExplicitThis = true), ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DisposeThreaded_Internal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StartVideoMode_Internal_Injected(ref CameraParameters cameraParameters, VideoCapture.AudioState audioState, VideoCapture.OnVideoModeStartedCallback onVideoModeStartedCallback);
	}
}
