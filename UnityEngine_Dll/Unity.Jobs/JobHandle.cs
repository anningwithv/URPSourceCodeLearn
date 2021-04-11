using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace Unity.Jobs
{
	[NativeType(Header = "Runtime/Jobs/ScriptBindings/JobsBindings.h")]
	public struct JobHandle
	{
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr jobGroup;

		internal int version;

		public bool IsCompleted
		{
			get
			{
				return JobHandle.ScheduleBatchedJobsAndIsCompleted(ref this);
			}
		}

		public void Complete()
		{
			bool flag = this.jobGroup == IntPtr.Zero;
			if (!flag)
			{
				JobHandle.ScheduleBatchedJobsAndComplete(ref this);
			}
		}

		public unsafe static void CompleteAll(ref JobHandle job0, ref JobHandle job1)
		{
			JobHandle* ptr = stackalloc JobHandle[2];
			*ptr = job0;
			ptr[1] = job1;
			JobHandle.ScheduleBatchedJobsAndCompleteAll((void*)ptr, 2);
			job0 = default(JobHandle);
			job1 = default(JobHandle);
		}

		public unsafe static void CompleteAll(ref JobHandle job0, ref JobHandle job1, ref JobHandle job2)
		{
			JobHandle* ptr = stackalloc JobHandle[3];
			*ptr = job0;
			ptr[1] = job1;
			ptr[2] = job2;
			JobHandle.ScheduleBatchedJobsAndCompleteAll((void*)ptr, 3);
			job0 = default(JobHandle);
			job1 = default(JobHandle);
			job2 = default(JobHandle);
		}

		public static void CompleteAll(NativeArray<JobHandle> jobs)
		{
			JobHandle.ScheduleBatchedJobsAndCompleteAll(jobs.GetUnsafeReadOnlyPtr<JobHandle>(), jobs.Length);
		}

		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ScheduleBatchedJobs();

		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleBatchedJobsAndComplete(ref JobHandle job);

		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ScheduleBatchedJobsAndIsCompleted(ref JobHandle job);

		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleBatchedJobsAndCompleteAll(void* jobs, int count);

		public static JobHandle CombineDependencies(JobHandle job0, JobHandle job1)
		{
			return JobHandle.CombineDependenciesInternal2(ref job0, ref job1);
		}

		public static JobHandle CombineDependencies(JobHandle job0, JobHandle job1, JobHandle job2)
		{
			return JobHandle.CombineDependenciesInternal3(ref job0, ref job1, ref job2);
		}

		public static JobHandle CombineDependencies(NativeArray<JobHandle> jobs)
		{
			return JobHandle.CombineDependenciesInternalPtr(jobs.GetUnsafeReadOnlyPtr<JobHandle>(), jobs.Length);
		}

		public static JobHandle CombineDependencies(NativeSlice<JobHandle> jobs)
		{
			return JobHandle.CombineDependenciesInternalPtr(jobs.GetUnsafeReadOnlyPtr<JobHandle>(), jobs.Length);
		}

		[NativeMethod(IsFreeFunction = true)]
		private static JobHandle CombineDependenciesInternal2(ref JobHandle job0, ref JobHandle job1)
		{
			JobHandle result;
			JobHandle.CombineDependenciesInternal2_Injected(ref job0, ref job1, out result);
			return result;
		}

		[NativeMethod(IsFreeFunction = true)]
		private static JobHandle CombineDependenciesInternal3(ref JobHandle job0, ref JobHandle job1, ref JobHandle job2)
		{
			JobHandle result;
			JobHandle.CombineDependenciesInternal3_Injected(ref job0, ref job1, ref job2, out result);
			return result;
		}

		[NativeMethod(IsFreeFunction = true)]
		internal unsafe static JobHandle CombineDependenciesInternalPtr(void* jobs, int count)
		{
			JobHandle result;
			JobHandle.CombineDependenciesInternalPtr_Injected(jobs, count, out result);
			return result;
		}

		[NativeMethod(IsFreeFunction = true)]
		public static bool CheckFenceIsDependencyOrDidSyncFence(JobHandle jobHandle, JobHandle dependsOn)
		{
			return JobHandle.CheckFenceIsDependencyOrDidSyncFence_Injected(ref jobHandle, ref dependsOn);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CombineDependenciesInternal2_Injected(ref JobHandle job0, ref JobHandle job1, out JobHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CombineDependenciesInternal3_Injected(ref JobHandle job0, ref JobHandle job1, ref JobHandle job2, out JobHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void CombineDependenciesInternalPtr_Injected(void* jobs, int count, out JobHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CheckFenceIsDependencyOrDidSyncFence_Injected(ref JobHandle jobHandle, ref JobHandle dependsOn);
	}
}
