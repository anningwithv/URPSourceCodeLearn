using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Yoga
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void YogaLogger(IntPtr unmanagedConfigPtr, IntPtr unmanagedNotePtr, YogaLogLevel level, string message);
}
