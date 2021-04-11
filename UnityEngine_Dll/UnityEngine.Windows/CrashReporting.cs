using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Windows
{
	public static class CrashReporting
	{
		public static extern string crashReportFolder
		{
			[NativeHeader("PlatformDependent/WinPlayer/Bindings/CrashReportingBindings.h"), ThreadSafe]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
