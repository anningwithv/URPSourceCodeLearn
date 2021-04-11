using System;
using UnityEngine.Bindings;

namespace UnityEngine.Apple
{
	[NativeHeader("Runtime/Export/Apple/FrameCaptureMetalScriptBindings.h")]
	public enum FrameCaptureDestination
	{
		DevTools = 1,
		GPUTraceDocument
	}
}
