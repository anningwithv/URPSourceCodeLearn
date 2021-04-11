using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	public struct JobRanges
	{
		internal int BatchSize;

		internal int NumJobs;

		public int TotalIterationCount;

		internal int NumPhases;

		internal IntPtr StartEndIndex;

		internal IntPtr PhaseData;
	}
}
