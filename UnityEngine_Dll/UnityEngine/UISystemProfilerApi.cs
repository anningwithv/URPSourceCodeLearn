using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/UI/Canvas.h"), StaticAccessor("UI::SystemProfilerApi", StaticAccessorType.DoubleColon)]
	public static class UISystemProfilerApi
	{
		public enum SampleType
		{
			Layout,
			Render
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginSample(UISystemProfilerApi.SampleType type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndSample(UISystemProfilerApi.SampleType type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void AddMarker(string name, Object obj);
	}
}
