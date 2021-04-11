using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	public enum EnforceJobResult
	{
		AllJobsAlreadySynced,
		DidSyncRunningJobs,
		HandleWasAlreadyDeallocated
	}
}
