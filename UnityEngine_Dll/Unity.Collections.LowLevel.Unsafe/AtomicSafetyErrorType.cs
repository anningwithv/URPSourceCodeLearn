using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	public enum AtomicSafetyErrorType
	{
		Deallocated,
		DeallocatedFromJob,
		NotAllocatedFromJob
	}
}
