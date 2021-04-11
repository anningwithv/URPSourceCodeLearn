using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	public static class IJobForExtensions
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		internal struct ForJobStruct<T> where T : struct, IJobFor
		{
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);

			public static IntPtr jobReflectionData;

			public static IntPtr Initialize()
			{
				bool flag = IJobForExtensions.ForJobStruct<T>.jobReflectionData == IntPtr.Zero;
				if (flag)
				{
					IJobForExtensions.ForJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new IJobForExtensions.ForJobStruct<T>.ExecuteJobFunction(IJobForExtensions.ForJobStruct<T>.Execute), null, null);
				}
				return IJobForExtensions.ForJobStruct<T>.jobReflectionData;
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

		public static JobHandle Schedule<T>(this T jobData, int arrayLength, JobHandle dependency) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.ForJobStruct<T>.Initialize(), dependency, ScheduleMode.Single);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		public static JobHandle ScheduleParallel<T>(this T jobData, int arrayLength, int innerloopBatchCount, JobHandle dependency) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.ForJobStruct<T>.Initialize(), dependency, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, innerloopBatchCount);
		}

		public static void Run<T>(this T jobData, int arrayLength) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.ForJobStruct<T>.Initialize(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}
	}
}
