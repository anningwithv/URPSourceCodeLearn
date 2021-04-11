using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystemDescriptor.h"), UsedByNativeCode]
	public class XRDisplaySubsystemDescriptor : IntegratedSubsystemDescriptor<XRDisplaySubsystem>
	{
		[NativeConditional("ENABLE_XR")]
		public extern bool disablesLegacyVr
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeConditional("ENABLE_XR")]
		public extern bool enableBackBufferMSAA
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeConditional("ENABLE_XR"), NativeMethod("TryGetAvailableMirrorModeCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetAvailableMirrorBlitModeCount();

		[NativeConditional("ENABLE_XR"), NativeMethod("TryGetMirrorModeByIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetMirrorBlitModeByIndex(int index, out XRMirrorViewBlitModeDesc mode);
	}
}
