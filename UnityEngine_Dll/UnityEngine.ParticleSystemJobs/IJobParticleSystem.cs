using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	[JobProducerType(typeof(ParticleSystemJobStruct<>))]
	public interface IJobParticleSystem
	{
		void Execute(ParticleSystemJobData jobData);
	}
}
