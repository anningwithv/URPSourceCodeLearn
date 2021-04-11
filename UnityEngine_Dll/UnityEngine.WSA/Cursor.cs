using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.WSA
{
	[NativeConditional("PLATFORM_WINRT"), NativeHeader("PlatformDependent/MetroPlayer/MetroCursor.h")]
	public static class Cursor
	{
		[FreeFunction("Cursors::SetHardwareCursor")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCustomCursor(uint id);
	}
}
