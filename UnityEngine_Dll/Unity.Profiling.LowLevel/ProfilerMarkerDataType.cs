using System;

namespace Unity.Profiling.LowLevel
{
	public enum ProfilerMarkerDataType : byte
	{
		Int32 = 2,
		UInt32,
		Int64,
		UInt64,
		Float,
		Double,
		String16 = 9,
		Blob8 = 11
	}
}
