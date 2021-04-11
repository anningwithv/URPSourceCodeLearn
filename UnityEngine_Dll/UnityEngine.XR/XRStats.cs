using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.XR
{
	[NativeConditional("ENABLE_VR")]
	public static class XRStats
	{
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool TryGetGPUTimeLastFrame(out float gpuTimeLastFrame);

		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool TryGetDroppedFrameCount(out int droppedFrameCount);

		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool TryGetFramePresentCount(out int framePresentCount);
	}
}
