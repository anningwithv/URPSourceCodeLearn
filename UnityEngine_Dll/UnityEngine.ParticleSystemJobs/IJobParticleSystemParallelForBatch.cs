using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	[JobProducerType(typeof(ParticleSystemParallelForBatchJobStruct<>))]
	public interface IJobParticleSystemParallelForBatch
	{
		void Execute(ParticleSystemJobData jobData, int startIndex, int count);
	}
}
