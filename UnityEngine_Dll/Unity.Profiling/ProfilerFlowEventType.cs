using System;

namespace Unity.Profiling
{
	public enum ProfilerFlowEventType : byte
	{
		Begin,
		ParallelNext,
		End,
		Next
	}
}
