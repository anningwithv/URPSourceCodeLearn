using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Subsystems
{
	[NativeType(Header = "Modules/Subsystems/Example/ExampleSubsystem.h"), UsedByNativeCode]
	public class ExampleSubsystem : IntegratedSubsystem<ExampleSubsystemDescriptor>
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PrintExample();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetBool();
	}
}
