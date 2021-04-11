using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct ParticleSystemParallelForBatchJobStruct<T> where T : struct, IJobParticleSystemParallelForBatch
	{
		public delegate void ExecuteJobFunction(ref T data, IntPtr listDataPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);

		public static IntPtr jobReflectionData;

		public static IntPtr Initialize()
		{
			bool flag = ParticleSystemParallelForBatchJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				ParticleSystemParallelForBatchJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new ParticleSystemParallelForBatchJobStruct<T>.ExecuteJobFunction(ParticleSystemParallelForBatchJobStruct<T>.Execute), null, null);
			}
			return ParticleSystemParallelForBatchJobStruct<T>.jobReflectionData;
		}

		public unsafe static void Execute(ref T data, IntPtr listDataPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
		{
			NativeListData* ptr = (NativeListData*)((void*)listDataPtr);
			NativeParticleData nativeParticleData;
			ParticleSystem.CopyManagedJobData(ptr->system, out nativeParticleData);
			ParticleSystemJobData particleSystemJobData = new ParticleSystemJobData(ref nativeParticleData);
			while (true)
			{
				int num;
				int num2;
				bool flag = !JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2);
				if (flag)
				{
					break;
				}
				JobsUtility.PatchBufferMinMaxRanges(bufferRangePatchData, UnsafeUtility.AddressOf<T>(ref data), num, num2 - num);
				data.Execute(particleSystemJobData, num, num2 - num);
			}
			AtomicSafetyHandle.CheckDeallocateAndThrow(particleSystemJobData.m_Safety);
			AtomicSafetyHandle.Release(particleSystemJobData.m_Safety);
		}
	}
}
