using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.Jobs
{
	public static class IJobParallelForTransformExtensions
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		internal struct TransformParallelForLoopStruct<T> where T : struct, IJobParallelForTransform
		{
			private struct TransformJobData
			{
				public IntPtr TransformAccessArray;

				public int IsReadOnly;
			}

			public delegate void ExecuteJobFunction(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);

			public static IntPtr jobReflectionData;

			public static IntPtr Initialize()
			{
				bool flag = IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData == IntPtr.Zero;
				if (flag)
				{
					IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.ExecuteJobFunction(IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Execute), null, null);
				}
				return IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData;
			}

			public unsafe static void Execute(ref T jobData, IntPtr jobData2, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.TransformJobData transformJobData;
				UnsafeUtility.CopyPtrToStructure<IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.TransformJobData>((void*)jobData2, out transformJobData);
				int* ptr = (int*)((void*)TransformAccessArray.GetSortedToUserIndex(transformJobData.TransformAccessArray));
				TransformAccess* ptr2 = (TransformAccess*)((void*)TransformAccessArray.GetSortedTransformAccess(transformJobData.TransformAccessArray));
				bool flag = transformJobData.IsReadOnly == 1;
				if (flag)
				{
					while (true)
					{
						int num;
						int num2;
						bool flag2 = !JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2);
						if (flag2)
						{
							break;
						}
						int num3 = num2;
						for (int i = num; i < num3; i++)
						{
							int num4 = i;
							int num5 = ptr[num4];
							JobsUtility.PatchBufferMinMaxRanges(bufferRangePatchData, UnsafeUtility.AddressOf<T>(ref jobData), num5, 1);
							TransformAccess transform = ptr2[num4];
							transform.MarkReadOnly();
							jobData.Execute(num5, transform);
						}
					}
				}
				else
				{
					int num6;
					int num7;
					JobsUtility.GetJobRange(ref ranges, jobIndex, out num6, out num7);
					for (int j = num6; j < num7; j++)
					{
						int num8 = j;
						int num9 = ptr[num8];
						JobsUtility.PatchBufferMinMaxRanges(bufferRangePatchData, UnsafeUtility.AddressOf<T>(ref jobData), num9, 1);
						TransformAccess transform2 = ptr2[num8];
						transform2.MarkReadWrite();
						jobData.Execute(num9, transform2);
					}
				}
			}
		}

		public static JobHandle Schedule<T>(this T jobData, TransformAccessArray transforms, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Initialize(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelForTransform(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule());
		}

		public static JobHandle ScheduleReadOnly<T>(this T jobData, TransformAccessArray transforms, int batchSize, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Initialize(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelForTransformReadOnly(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule(), batchSize);
		}

		public static void RunReadOnly<T>(this T jobData, TransformAccessArray transforms) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Initialize(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelForTransformReadOnly(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule(), transforms.length);
		}
	}
}
