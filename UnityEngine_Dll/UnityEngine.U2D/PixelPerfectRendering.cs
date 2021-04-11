using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	[NativeHeader("Runtime/2D/Common/PixelSnapping.h"), MovedFrom("UnityEngine.Experimental.U2D")]
	public static class PixelPerfectRendering
	{
		public static extern float pixelSnapSpacing
		{
			[FreeFunction("GetPixelSnapSpacing")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("SetPixelSnapSpacing")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
