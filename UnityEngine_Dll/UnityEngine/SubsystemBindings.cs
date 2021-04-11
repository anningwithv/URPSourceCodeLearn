using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	internal static class SubsystemBindings
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DestroySubsystem(IntPtr nativePtr);
	}
}
