using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.GlobalIllumination
{
	[NativeHeader("Runtime/Camera/RenderSettings.h"), StaticAccessor("GetRenderSettings()", StaticAccessorType.Dot)]
	public class RenderSettings
	{
		public static extern bool useRadianceAmbientProbe
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
