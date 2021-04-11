using System;
using Unity.Collections;

namespace UnityEngine.ParticleSystemJobs
{
	public struct ParticleSystemNativeArray4
	{
		public NativeArray<float> x;

		public NativeArray<float> y;

		public NativeArray<float> z;

		public NativeArray<float> w;

		public Vector4 this[int index]
		{
			get
			{
				return new Vector4(this.x[index], this.y[index], this.z[index], this.w[index]);
			}
			set
			{
				this.x[index] = value.x;
				this.y[index] = value.y;
				this.z[index] = value.z;
				this.w[index] = value.w;
			}
		}
	}
}
