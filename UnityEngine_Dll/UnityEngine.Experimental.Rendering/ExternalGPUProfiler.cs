using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public static class ExternalGPUProfiler
	{
		[FreeFunction("ExternalGPUProfilerBindings::BeginGPUCapture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginGPUCapture();

		[FreeFunction("ExternalGPUProfilerBindings::EndGPUCapture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndGPUCapture();

		[FreeFunction("ExternalGPUProfilerBindings::IsAttached")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsAttached();
	}
}
