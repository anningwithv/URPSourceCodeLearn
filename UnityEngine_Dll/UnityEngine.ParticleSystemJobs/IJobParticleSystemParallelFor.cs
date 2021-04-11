using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	[JobProducerType(typeof(ParticleSystemParallelForJobStruct<>))]
	public interface IJobParticleSystemParallelFor
	{
		void Execute(ParticleSystemJobData jobData, int index);
	}
}
