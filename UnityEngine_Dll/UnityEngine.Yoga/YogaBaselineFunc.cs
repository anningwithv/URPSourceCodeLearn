using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Yoga
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate float YogaBaselineFunc(IntPtr unmanagedNodePtr, float width, float height);
}
