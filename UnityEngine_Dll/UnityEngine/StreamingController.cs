using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Streaming/StreamingController.h"), RequireComponent(typeof(Camera))]
	public class StreamingController : Behaviour
	{
		public extern float streamingMipmapBias
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPreloading(float timeoutSeconds = 0f, bool activateCameraOnTimeout = false, Camera disableCameraCuttingFrom = null);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CancelPreloading();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsPreloading();
	}
}
