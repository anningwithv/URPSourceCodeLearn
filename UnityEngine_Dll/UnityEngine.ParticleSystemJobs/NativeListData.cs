using System;

namespace UnityEngine.ParticleSystemJobs
{
	internal struct NativeListData
	{
		public unsafe void* system;

		public int length;

		public int capacity;
	}
}
