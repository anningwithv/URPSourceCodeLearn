using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.Jobs.LowLevel.Unsafe
{
	[NativeHeader("Runtime/Jobs/JobSystem.h"), NativeType(Header = "Runtime/Jobs/ScriptBindings/JobsBindings.h")]
	public static class JobsUtility
	{
		public struct JobScheduleParameters
		{
			public JobHandle Dependency;

			public int ScheduleMode;

			public IntPtr ReflectionData;

			public IntPtr JobDataPtr;

			public unsafe JobScheduleParameters(void* i_jobData, IntPtr i_reflectionData, JobHandle i_dependency, ScheduleMode i_scheduleMode)
			{
				this.Dependency = i_dependency;
				this.JobDataPtr = (IntPtr)i_jobData;
				this.ReflectionData = i_reflectionData;
				this.ScheduleMode = (int)i_scheduleMode;
			}
		}

		public const int MaxJobThreadCount = 128;

		public const int CacheLineSize = 64;

		public static extern bool IsExecutingJob
		{
			[NativeMethod(IsFreeFunction = true, IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool JobDebuggerEnabled
		{
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool JobCompilerEnabled
		{
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int JobWorkerMaximumCount
		{
			[FreeFunction("JobSystem::GetJobQueueMaximumThreadCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static int JobWorkerCount
		{
			get
			{
				return JobsUtility.GetJobQueueWorkerThreadCount();
			}
			set
			{
				bool flag = value < 0 || value > JobsUtility.JobWorkerMaximumCount;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("JobWorkerCount", string.Format("Invalid JobWorkerCount {0} must be in the range 0 -> {1}", value, JobsUtility.JobWorkerMaximumCount));
				}
				JobsUtility.SetJobQueueMaximumActiveThreadCount(value);
			}
		}

		public unsafe static void GetJobRange(ref JobRanges ranges, int jobIndex, out int beginIndex, out int endIndex)
		{
			int* ptr = (int*)((void*)ranges.StartEndIndex);
			beginIndex = ptr[jobIndex * 2];
			endIndex = ptr[jobIndex * 2 + 1];
		}

		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetWorkStealingRange(ref JobRanges ranges, int jobIndex, out int beginIndex, out int endIndex);

		[FreeFunction("ScheduleManagedJob", ThrowsException = true)]
		public static JobHandle Schedule(ref JobsUtility.JobScheduleParameters parameters)
		{
			JobHandle result;
			JobsUtility.Schedule_Injected(ref parameters, out result);
			return result;
		}

		[FreeFunction("ScheduleManagedJobParallelFor", ThrowsException = true)]
		public static JobHandle ScheduleParallelFor(ref JobsUtility.JobScheduleParameters parameters, int arrayLength, int innerloopBatchCount)
		{
			JobHandle result;
			JobsUtility.ScheduleParallelFor_Injected(ref parameters, arrayLength, innerloopBatchCount, out result);
			return result;
		}

		[FreeFunction("ScheduleManagedJobParallelForDeferArraySize", ThrowsException = true)]
		public unsafe static JobHandle ScheduleParallelForDeferArraySize(ref JobsUtility.JobScheduleParameters parameters, int innerloopBatchCount, void* listData, void* listDataAtomicSafetyHandle)
		{
			JobHandle result;
			JobsUtility.ScheduleParallelForDeferArraySize_Injected(ref parameters, innerloopBatchCount, listData, listDataAtomicSafetyHandle, out result);
			return result;
		}

		[FreeFunction("ScheduleManagedJobParallelForTransform", ThrowsException = true)]
		public static JobHandle ScheduleParallelForTransform(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray)
		{
			JobHandle result;
			JobsUtility.ScheduleParallelForTransform_Injected(ref parameters, transfromAccesssArray, out result);
			return result;
		}

		[FreeFunction("ScheduleManagedJobParallelForTransformReadOnly", ThrowsException = true)]
		public static JobHandle ScheduleParallelForTransformReadOnly(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray, int innerloopBatchCount)
		{
			JobHandle result;
			JobsUtility.ScheduleParallelForTransformReadOnly_Injected(ref parameters, transfromAccesssArray, innerloopBatchCount, out result);
			return result;
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS"), NativeMethod(IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void PatchBufferMinMaxRanges(IntPtr bufferRangePatchData, void* jobdata, int startIndex, int rangeSize);

		[FreeFunction(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, object managedJobFunction0, object managedJobFunction1, object managedJobFunction2);

		[Obsolete("JobType is obsolete. The parameter should be removed. (UnityUpgradable) -> !1")]
		public static IntPtr CreateJobReflectionData(Type type, JobType jobType, object managedJobFunction0, object managedJobFunction1 = null, object managedJobFunction2 = null)
		{
			return JobsUtility.CreateJobReflectionData(type, type, managedJobFunction0, managedJobFunction1, managedJobFunction2);
		}

		public static IntPtr CreateJobReflectionData(Type type, object managedJobFunction0, object managedJobFunction1 = null, object managedJobFunction2 = null)
		{
			return JobsUtility.CreateJobReflectionData(type, type, managedJobFunction0, managedJobFunction1, managedJobFunction2);
		}

		[Obsolete("JobType is obsolete. The parameter should be removed. (UnityUpgradable) -> !2")]
		public static IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, JobType jobType, object managedJobFunction0)
		{
			return JobsUtility.CreateJobReflectionData(wrapperJobType, userJobType, managedJobFunction0, null, null);
		}

		public static IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, object managedJobFunction0)
		{
			return JobsUtility.CreateJobReflectionData(wrapperJobType, userJobType, managedJobFunction0, null, null);
		}

		[FreeFunction("JobSystem::GetJobQueueWorkerThreadCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetJobQueueWorkerThreadCount();

		[FreeFunction("JobSystem::ForceSetJobQueueWorkerThreadCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetJobQueueMaximumActiveThreadCount(int count);

		[FreeFunction("JobSystem::ResetJobQueueWorkerThreadCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetJobWorkerCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Schedule_Injected(ref JobsUtility.JobScheduleParameters parameters, out JobHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleParallelFor_Injected(ref JobsUtility.JobScheduleParameters parameters, int arrayLength, int innerloopBatchCount, out JobHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleParallelForDeferArraySize_Injected(ref JobsUtility.JobScheduleParameters parameters, int innerloopBatchCount, void* listData, void* listDataAtomicSafetyHandle, out JobHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleParallelForTransform_Injected(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray, out JobHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleParallelForTransformReadOnly_Injected(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray, int innerloopBatchCount, out JobHandle ret);
	}
}
