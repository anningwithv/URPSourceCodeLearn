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
	[NativeHeader("PlatformDependent/Win/Webcam/PhotoCapture.h"), StaticAccessor("PhotoCapture", StaticAccessorType.DoubleColon), MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[StructLayout(LayoutKind.Sequential)]
	public class PhotoCapture : IDisposable
	{
		public enum CaptureResultType
		{
			Success,
			UnknownError
		}

		public struct PhotoCaptureResult
		{
			public PhotoCapture.CaptureResultType resultType;

			public long hResult;

			public bool success
			{
				get
				{
					return this.resultType == PhotoCapture.CaptureResultType.Success;
				}
			}
		}

		public delegate void OnCaptureResourceCreatedCallback(PhotoCapture captureObject);

		public delegate void OnPhotoModeStartedCallback(PhotoCapture.PhotoCaptureResult result);

		public delegate void OnPhotoModeStoppedCallback(PhotoCapture.PhotoCaptureResult result);

		public delegate void OnCapturedToDiskCallback(PhotoCapture.PhotoCaptureResult result);

		public delegate void OnCapturedToMemoryCallback(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame);

		internal IntPtr m_NativePtr;

		private static Resolution[] s_SupportedResolutions;

		private static readonly long HR_SUCCESS = 0L;

		public static IEnumerable<Resolution> SupportedResolutions
		{
			get
			{
				bool flag = PhotoCapture.s_SupportedResolutions == null;
				if (flag)
				{
					PhotoCapture.s_SupportedResolutions = PhotoCapture.GetSupportedResolutions_Internal();
				}
				return PhotoCapture.s_SupportedResolutions;
			}
		}

		private static PhotoCapture.PhotoCaptureResult MakeCaptureResult(PhotoCapture.CaptureResultType resultType, long hResult)
		{
			return new PhotoCapture.PhotoCaptureResult
			{
				resultType = resultType,
				hResult = hResult
			};
		}

		private static PhotoCapture.PhotoCaptureResult MakeCaptureResult(long hResult)
		{
			PhotoCapture.PhotoCaptureResult result = default(PhotoCapture.PhotoCaptureResult);
			bool flag = hResult == PhotoCapture.HR_SUCCESS;
			PhotoCapture.CaptureResultType resultType;
			if (flag)
			{
				resultType = PhotoCapture.CaptureResultType.Success;
			}
			else
			{
				resultType = PhotoCapture.CaptureResultType.UnknownError;
			}
			result.resultType = resultType;
			result.hResult = hResult;
			return result;
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("GetSupportedResolutions")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Resolution[] GetSupportedResolutions_Internal();

		public static void CreateAsync(bool showHolograms, PhotoCapture.OnCaptureResourceCreatedCallback onCreatedCallback)
		{
			bool flag = onCreatedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCreatedCallback");
			}
			PhotoCapture.Instantiate_Internal(showHolograms, onCreatedCallback);
		}

		public static void CreateAsync(PhotoCapture.OnCaptureResourceCreatedCallback onCreatedCallback)
		{
			bool flag = onCreatedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCreatedCallback");
			}
			PhotoCapture.Instantiate_Internal(false, onCreatedCallback);
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("Instantiate")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Instantiate_Internal(bool showHolograms, PhotoCapture.OnCaptureResourceCreatedCallback onCreatedCallback);

		[RequiredByNativeCode]
		private static void InvokeOnCreatedResourceDelegate(PhotoCapture.OnCaptureResourceCreatedCallback callback, IntPtr nativePtr)
		{
			bool flag = nativePtr == IntPtr.Zero;
			if (flag)
			{
				callback(null);
			}
			else
			{
				callback(new PhotoCapture(nativePtr));
			}
		}

		private PhotoCapture(IntPtr nativeCaptureObject)
		{
			this.m_NativePtr = nativeCaptureObject;
		}

		public void StartPhotoModeAsync(CameraParameters setupParams, PhotoCapture.OnPhotoModeStartedCallback onPhotoModeStartedCallback)
		{
			bool flag = onPhotoModeStartedCallback == null;
			if (flag)
			{
				throw new ArgumentException("onPhotoModeStartedCallback");
			}
			bool flag2 = setupParams.cameraResolutionWidth == 0 || setupParams.cameraResolutionHeight == 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("setupParams", "The camera resolution must be set to a supported resolution.");
			}
			this.StartPhotoMode_Internal(setupParams, onPhotoModeStartedCallback);
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("StartPhotoMode")]
		private void StartPhotoMode_Internal(CameraParameters setupParams, PhotoCapture.OnPhotoModeStartedCallback onPhotoModeStartedCallback)
		{
			this.StartPhotoMode_Internal_Injected(ref setupParams, onPhotoModeStartedCallback);
		}

		[RequiredByNativeCode]
		private static void InvokeOnPhotoModeStartedDelegate(PhotoCapture.OnPhotoModeStartedCallback callback, long hResult)
		{
			callback(PhotoCapture.MakeCaptureResult(hResult));
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("StopPhotoMode")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopPhotoModeAsync(PhotoCapture.OnPhotoModeStoppedCallback onPhotoModeStoppedCallback);

		[RequiredByNativeCode]
		private static void InvokeOnPhotoModeStoppedDelegate(PhotoCapture.OnPhotoModeStoppedCallback callback, long hResult)
		{
			callback(PhotoCapture.MakeCaptureResult(hResult));
		}

		public void TakePhotoAsync(string filename, PhotoCaptureFileOutputFormat fileOutputFormat, PhotoCapture.OnCapturedToDiskCallback onCapturedPhotoToDiskCallback)
		{
			bool flag = onCapturedPhotoToDiskCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCapturedPhotoToDiskCallback");
			}
			bool flag2 = string.IsNullOrEmpty(filename);
			if (flag2)
			{
				throw new ArgumentNullException("filename");
			}
			filename = filename.Replace("/", "\\");
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
			this.CapturePhotoToDisk_Internal(filename, fileOutputFormat, onCapturedPhotoToDiskCallback);
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("CapturePhotoToDisk")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CapturePhotoToDisk_Internal(string filename, PhotoCaptureFileOutputFormat fileOutputFormat, PhotoCapture.OnCapturedToDiskCallback onCapturedPhotoToDiskCallback);

		[RequiredByNativeCode]
		private static void InvokeOnCapturedPhotoToDiskDelegate(PhotoCapture.OnCapturedToDiskCallback callback, long hResult)
		{
			callback(PhotoCapture.MakeCaptureResult(hResult));
		}

		public void TakePhotoAsync(PhotoCapture.OnCapturedToMemoryCallback onCapturedPhotoToMemoryCallback)
		{
			bool flag = onCapturedPhotoToMemoryCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCapturedPhotoToMemoryCallback");
			}
			this.CapturePhotoToMemory_Internal(onCapturedPhotoToMemoryCallback);
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("CapturePhotoToMemory")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CapturePhotoToMemory_Internal(PhotoCapture.OnCapturedToMemoryCallback onCapturedPhotoToMemoryCallback);

		[RequiredByNativeCode]
		private static void InvokeOnCapturedPhotoToMemoryDelegate(PhotoCapture.OnCapturedToMemoryCallback callback, long hResult, IntPtr photoCaptureFramePtr)
		{
			PhotoCaptureFrame photoCaptureFrame = null;
			bool flag = photoCaptureFramePtr != IntPtr.Zero;
			if (flag)
			{
				photoCaptureFrame = new PhotoCaptureFrame(photoCaptureFramePtr);
			}
			callback(PhotoCapture.MakeCaptureResult(hResult), photoCaptureFrame);
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("GetUnsafePointerToVideoDeviceController"), ThreadAndSerializationSafe]
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

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("Dispose")]
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

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("DisposeThreaded"), ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DisposeThreaded_Internal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StartPhotoMode_Internal_Injected(ref CameraParameters setupParams, PhotoCapture.OnPhotoModeStartedCallback onPhotoModeStartedCallback);
	}
}
