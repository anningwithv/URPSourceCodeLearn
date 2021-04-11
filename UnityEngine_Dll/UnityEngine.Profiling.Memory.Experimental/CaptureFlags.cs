using System;

namespace UnityEngine.Profiling.Memory.Experimental
{
	[Flags]
	public enum CaptureFlags : uint
	{
		ManagedObjects = 1u,
		NativeObjects = 2u,
		NativeAllocations = 4u,
		NativeAllocationSites = 8u,
		NativeStackTraces = 16u
	}
}
