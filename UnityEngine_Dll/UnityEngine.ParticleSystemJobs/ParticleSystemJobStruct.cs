using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct ParticleSystemJobStruct<T> where T : struct, IJobParticleSystem
	{
		public delegate void ExecuteJobFunction(ref T data, IntPtr listDataPtr, IntPtr unusedPtr, ref JobRanges ranges, int jobIndex);

		public static IntPtr jobReflectionData;

		public static IntPtr Initialize()
		{
			bool flag = ParticleSystemJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				ParticleSystemJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new ParticleSystemJobStruct<T>.ExecuteJobFunction(ParticleSystemJobStruct<T>.Execute), null, null);
			}
			return ParticleSystemJobStruct<T>.jobReflectionData;
		}

		public unsafe static void Execute(ref T data, IntPtr listDataPtr, IntPtr unusedPtr, ref JobRanges ranges, int jobIndex)
		{
			NativeListData* ptr = (NativeListData*)((void*)listDataPtr);
			NativeParticleData nativeParticleData;
			ParticleSystem.CopyManagedJobData(ptr->system, out nativeParticleData);
			ParticleSystemJobData particleSystemJobData = new ParticleSystemJobData(ref nativeParticleData);
			data.Execute(particleSystemJobData);
			AtomicSafetyHandle.CheckDeallocateAndThrow(particleSystemJobData.m_Safety);
			AtomicSafetyHandle.Release(particleSystemJobData.m_Safety);
		}
	}
}
