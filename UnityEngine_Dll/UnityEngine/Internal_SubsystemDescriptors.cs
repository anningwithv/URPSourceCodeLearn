using System;
using UnityEngine.Scripting;
using UnityEngine.SubsystemsImplementation;

namespace UnityEngine
{
	internal static class Internal_SubsystemDescriptors
	{
		[RequiredByNativeCode]
		internal static void Internal_AddDescriptor(SubsystemDescriptor descriptor)
		{
			SubsystemDescriptorStore.RegisterDeprecatedDescriptor(descriptor);
		}
	}
}
