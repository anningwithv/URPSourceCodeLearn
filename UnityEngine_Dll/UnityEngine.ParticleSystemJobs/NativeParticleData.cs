using System;

namespace UnityEngine.ParticleSystemJobs
{
	internal struct NativeParticleData
	{
		internal struct Array3
		{
			internal unsafe float* x;

			internal unsafe float* y;

			internal unsafe float* z;
		}

		internal struct Array4
		{
			internal unsafe float* x;

			internal unsafe float* y;

			internal unsafe float* z;

			internal unsafe float* w;
		}

		internal int count;

		internal NativeParticleData.Array3 positions;

		internal NativeParticleData.Array3 velocities;

		internal NativeParticleData.Array3 rotations;

		internal NativeParticleData.Array3 rotationalSpeeds;

		internal NativeParticleData.Array3 sizes;

		internal unsafe void* startColors;

		internal unsafe void* aliveTimePercent;

		internal unsafe void* inverseStartLifetimes;

		internal unsafe void* randomSeeds;

		internal NativeParticleData.Array4 customData1;

		internal NativeParticleData.Array4 customData2;
	}
}
