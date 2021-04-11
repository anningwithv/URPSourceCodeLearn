using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Diagnostics
{
	[NativeHeader("Runtime/Export/Diagnostics/DiagnosticsUtils.bindings.h")]
	public static class Utils
	{
		[FreeFunction("DiagnosticsUtils_Bindings::ForceCrash", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ForceCrash(ForcedCrashCategory crashCategory);

		[FreeFunction("DiagnosticsUtils_Bindings::NativeAssert")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void NativeAssert(string message);

		[FreeFunction("DiagnosticsUtils_Bindings::NativeError")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void NativeError(string message);

		[FreeFunction("DiagnosticsUtils_Bindings::NativeWarning")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void NativeWarning(string message);
	}
}
