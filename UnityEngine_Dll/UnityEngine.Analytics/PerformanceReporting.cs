using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Analytics
{
	[NativeHeader("Modules/PerformanceReporting/PerformanceReportingManager.h"), StaticAccessor("GetPerformanceReportingManager()", StaticAccessorType.Dot)]
	public static class PerformanceReporting
	{
		public static extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern long graphicsInitializationFinishTime
		{
			[NativeMethod("GetGfxDoneTime")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
