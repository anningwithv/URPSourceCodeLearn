using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[NativeConditional("ENABLE_XR"), NativeHeader("Modules/XR/XRPrefix.h"), NativeType(Header = "Modules/XR/Subsystems/Input/XRInputSubsystemDescriptor.h"), UsedByNativeCode]
	public class XRInputSubsystemDescriptor : IntegratedSubsystemDescriptor<XRInputSubsystem>
	{
		[NativeConditional("ENABLE_XR")]
		public extern bool disablesLegacyInput
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
