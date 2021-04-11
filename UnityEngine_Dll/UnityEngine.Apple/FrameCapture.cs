using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Apple
{
	[NativeConditional("PLATFORM_IOS || PLATFORM_TVOS || PLATFORM_OSX"), NativeHeader("Runtime/Export/Apple/FrameCaptureMetalScriptBindings.h")]
	public class FrameCapture
	{
		private FrameCapture()
		{
		}

		[FreeFunction("FrameCaptureMetalScripting::IsDestinationSupported")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsDestinationSupportedImpl(FrameCaptureDestination dest);

		[FreeFunction("FrameCaptureMetalScripting::BeginCapture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginCaptureImpl(FrameCaptureDestination dest, string path);

		[FreeFunction("FrameCaptureMetalScripting::EndCapture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndCaptureImpl();

		[FreeFunction("FrameCaptureMetalScripting::CaptureNextFrame")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CaptureNextFrameImpl(FrameCaptureDestination dest, string path);

		public static bool IsDestinationSupported(FrameCaptureDestination dest)
		{
			bool flag = dest != FrameCaptureDestination.DevTools && dest != FrameCaptureDestination.GPUTraceDocument;
			if (flag)
			{
				throw new ArgumentException("dest", "Argument dest has bad value (not one of FrameCaptureDestination enum values)");
			}
			return FrameCapture.IsDestinationSupportedImpl(dest);
		}

		public static void BeginCaptureToXcode()
		{
			bool flag = !FrameCapture.IsDestinationSupported(FrameCaptureDestination.DevTools);
			if (flag)
			{
				throw new InvalidOperationException("Frame Capture with DevTools is not supported.");
			}
			FrameCapture.BeginCaptureImpl(FrameCaptureDestination.DevTools, null);
		}

		public static void BeginCaptureToFile(string path)
		{
			bool flag = !FrameCapture.IsDestinationSupported(FrameCaptureDestination.GPUTraceDocument);
			if (flag)
			{
				throw new InvalidOperationException("Frame Capture to file is not supported.");
			}
			bool flag2 = string.IsNullOrEmpty(path);
			if (flag2)
			{
				throw new ArgumentException("path", "Path must be supplied when capture destination is GPUTraceDocument.");
			}
			bool flag3 = Path.GetExtension(path) != ".gputrace";
			if (flag3)
			{
				throw new ArgumentException("path", "Destination file should have .gputrace extension.");
			}
			FrameCapture.BeginCaptureImpl(FrameCaptureDestination.GPUTraceDocument, new Uri(path).AbsoluteUri);
		}

		public static void EndCapture()
		{
			FrameCapture.EndCaptureImpl();
		}

		public static void CaptureNextFrameToXcode()
		{
			bool flag = !FrameCapture.IsDestinationSupported(FrameCaptureDestination.DevTools);
			if (flag)
			{
				throw new InvalidOperationException("Frame Capture with DevTools is not supported.");
			}
			FrameCapture.CaptureNextFrameImpl(FrameCaptureDestination.DevTools, null);
		}

		public static void CaptureNextFrameToFile(string path)
		{
			bool flag = !FrameCapture.IsDestinationSupported(FrameCaptureDestination.GPUTraceDocument);
			if (flag)
			{
				throw new InvalidOperationException("Frame Capture to file is not supported.");
			}
			bool flag2 = string.IsNullOrEmpty(path);
			if (flag2)
			{
				throw new ArgumentException("path", "Path must be supplied when capture destination is GPUTraceDocument.");
			}
			bool flag3 = Path.GetExtension(path) != ".gputrace";
			if (flag3)
			{
				throw new ArgumentException("path", "Destination file should have .gputrace extension.");
			}
			FrameCapture.CaptureNextFrameImpl(FrameCaptureDestination.GPUTraceDocument, new Uri(path).AbsoluteUri);
		}
	}
}
