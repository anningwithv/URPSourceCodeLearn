using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	[NativeHeader("PlatformDependent/Win/Webcam/WebCam.h"), StaticAccessor("WebCam::GetInstance()", StaticAccessorType.Dot), MovedFrom("UnityEngine.XR.WSA.WebCam")]
	public class WebCam
	{
		public static extern WebCamMode Mode
		{
			[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("GetWebCamMode")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
