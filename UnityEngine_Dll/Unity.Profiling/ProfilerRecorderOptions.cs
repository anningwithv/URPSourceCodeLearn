using System;

namespace Unity.Profiling
{
	[Flags]
	public enum ProfilerRecorderOptions
	{
		None = 0,
		StartImmediately = 1,
		KeepAliveDuringDomainReload = 2,
		CollectOnlyOnCurrentThread = 4,
		WrapAroundWhenCapacityReached = 8,
		SumAllSamplesInFrame = 16,
		Default = 24
	}
}
