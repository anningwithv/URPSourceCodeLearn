using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Yoga
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate YogaSize YogaMeasureFunc(IntPtr unmanagedNodePtr, float width, YogaMeasureMode widthMode, float height, YogaMeasureMode heightMode);
}
