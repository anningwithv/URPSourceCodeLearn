using System;
using UnityEngine.Bindings;

namespace UnityEngine.XR
{
	[NativeHeader("Modules/XR/XRPrefix.h"), NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystemDescriptor.h")]
	public struct XRMirrorViewBlitModeDesc
	{
		public int blitMode;

		public string blitModeDesc;
	}
}
