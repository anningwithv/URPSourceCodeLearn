using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	public static class IJobExtensions
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		internal struct JobStruct<T> where T : struct, IJob
		{
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);

			public static IntPtr jobReflectionData;

			public static IntPtr Initialize()
			{
				bool flag = IJobExtensions.JobStruct<T>.jobReflectionData == IntPtr.Zero;
				if (flag)
				{
					IJobExtensions.JobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new IJobExtensions.JobStruct<T>.ExecuteJobFunction(IJobExtensions.JobStruct<T>.Execute), null, null);
				}
				return IJobExtensions.JobStruct<T>.jobReflectionData;
			}

			public static void Execute(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				data.Execute();
			}
		}

		public static JobHandle Schedule<T>(this T jobData, JobHandle dependsOn = default(JobHandle)) where T : struct, IJob
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobExtensions.JobStruct<T>.Initialize(), dependsOn, ScheduleMode.Single);
			return JobsUtility.Schedule(ref jobScheduleParameters);
		}

		public static void Run<T>(this T jobData) where T : struct, IJob
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobExtensions.JobStruct<T>.Initialize(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.Schedule(ref jobScheduleParameters);
		}
	}
}
