using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	public static class IJobParallelForExtensions
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		internal struct ParallelForJobStruct<T> where T : struct, IJobParallelFor
		{
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);

			public static IntPtr jobReflectionData;

			public static IntPtr Initialize()
			{
				bool flag = IJobParallelForExtensions.ParallelForJobStruct<T>.jobReflectionData == IntPtr.Zero;
				if (flag)
				{
					IJobParallelForExtensions.ParallelForJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new IJobParallelForExtensions.ParallelForJobStruct<T>.ExecuteJobFunction(IJobParallelForExtensions.ParallelForJobStruct<T>.Execute), null, null);
				}
				return IJobParallelForExtensions.ParallelForJobStruct<T>.jobReflectionData;
			}

			public static void Execute(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				while (true)
				{
					int num;
					int num2;
					bool flag = !JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2);
					if (flag)
					{
						break;
					}
					JobsUtility.PatchBufferMinMaxRanges(bufferRangePatchData, UnsafeUtility.AddressOf<T>(ref jobData), num, num2 - num);
					int num3 = num2;
					for (int i = num; i < num3; i++)
					{
						jobData.Execute(i);
					}
				}
			}
		}

		public static JobHandle Schedule<T>(this T jobData, int arrayLength, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForExtensions.ParallelForJobStruct<T>.Initialize(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, innerloopBatchCount);
		}

		public static void Run<T>(this T jobData, int arrayLength) where T : struct, IJobParallelFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForExtensions.ParallelForJobStruct<T>.Initialize(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}
	}
}
