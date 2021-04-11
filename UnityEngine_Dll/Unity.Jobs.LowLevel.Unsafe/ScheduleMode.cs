using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	public enum ScheduleMode
	{
		Run,
		[Obsolete("Batched is obsolete, use Parallel or Single depending on job type. (UnityUpgradable) -> Parallel", false)]
		Batched,
		Parallel = 1,
		Single
	}
}
