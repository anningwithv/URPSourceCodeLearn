using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	[NativeHeader("Modules/ScreenCapture/Public/CaptureScreenshot.h")]
	public static class ScreenCapture
	{
		public enum StereoScreenCaptureMode
		{
			LeftEye = 1,
			RightEye,
			BothEyes
		}

		public static void CaptureScreenshot(string filename)
		{
			ScreenCapture.CaptureScreenshot(filename, 1, ScreenCapture.StereoScreenCaptureMode.LeftEye);
		}

		public static void CaptureScreenshot(string filename, int superSize)
		{
			ScreenCapture.CaptureScreenshot(filename, superSize, ScreenCapture.StereoScreenCaptureMode.LeftEye);
		}

		public static void CaptureScreenshot(string filename, ScreenCapture.StereoScreenCaptureMode stereoCaptureMode)
		{
			ScreenCapture.CaptureScreenshot(filename, 1, stereoCaptureMode);
		}

		public static Texture2D CaptureScreenshotAsTexture()
		{
			return ScreenCapture.CaptureScreenshotAsTexture(1, ScreenCapture.StereoScreenCaptureMode.LeftEye);
		}

		public static Texture2D CaptureScreenshotAsTexture(int superSize)
		{
			return ScreenCapture.CaptureScreenshotAsTexture(superSize, ScreenCapture.StereoScreenCaptureMode.LeftEye);
		}

		public static Texture2D CaptureScreenshotAsTexture(ScreenCapture.StereoScreenCaptureMode stereoCaptureMode)
		{
			return ScreenCapture.CaptureScreenshotAsTexture(1, stereoCaptureMode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CaptureScreenshotIntoRenderTexture(RenderTexture renderTexture);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CaptureScreenshot(string filename, [DefaultValue("1")] int superSize, [DefaultValue("1")] ScreenCapture.StereoScreenCaptureMode CaptureMode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Texture2D CaptureScreenshotAsTexture(int superSize, ScreenCapture.StereoScreenCaptureMode stereoScreenCaptureMode);
	}
}
