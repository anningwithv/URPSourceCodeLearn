using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;

namespace Unity.Collections.LowLevel.Unsafe
{
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DisposeSentinel
	{
		private static readonly IntPtr s_CreateProfilerMarkerPtr = ProfilerUnsafeUtility.CreateMarker("DisposeSentinel.Create", 1, MarkerFlags.Script | MarkerFlags.AvailabilityEditor, 0);

		private static readonly IntPtr s_LogErrorProfilerMarkerPtr = ProfilerUnsafeUtility.CreateMarker("DisposeSentinel.LogError", 1, MarkerFlags.Script | MarkerFlags.AvailabilityEditor, 0);

		private int m_IsCreated;

		private StackTrace m_StackTrace;

		private DisposeSentinel()
		{
		}

		public static void Dispose(ref AtomicSafetyHandle safety, ref DisposeSentinel sentinel)
		{
			AtomicSafetyHandle.CheckDeallocateAndThrow(safety);
			bool flag = AtomicSafetyHandle.IsTempMemoryHandle(safety);
			if (flag)
			{
				int staticSafetyId = safety.staticSafetyId;
				safety = AtomicSafetyHandle.Create();
				safety.staticSafetyId = staticSafetyId;
			}
			AtomicSafetyHandle.Release(safety);
			DisposeSentinel.Clear(ref sentinel);
		}

		public static void Create(out AtomicSafetyHandle safety, out DisposeSentinel sentinel, int callSiteStackDepth, Allocator allocator)
		{
			safety = ((allocator == Allocator.Temp) ? AtomicSafetyHandle.GetTempMemoryHandle() : AtomicSafetyHandle.Create());
			sentinel = null;
			bool flag = allocator == Allocator.Temp || allocator == Allocator.AudioKernel;
			if (!flag)
			{
				bool isExecutingJob = JobsUtility.IsExecutingJob;
				if (isExecutingJob)
				{
					throw new InvalidOperationException("Jobs can only create Temp memory");
				}
				DisposeSentinel.CreateInternal(ref sentinel, callSiteStackDepth);
			}
		}

		[BurstDiscard]
		private static void CreateInternal(ref DisposeSentinel sentinel, int callSiteStackDepth)
		{
			NativeLeakDetectionMode mode = NativeLeakDetection.Mode;
			bool flag = mode == NativeLeakDetectionMode.Disabled;
			if (!flag)
			{
				ProfilerUnsafeUtility.BeginSample(DisposeSentinel.s_CreateProfilerMarkerPtr);
				StackTrace stackTrace = null;
				bool flag2 = mode == NativeLeakDetectionMode.EnabledWithStackTrace;
				if (flag2)
				{
					stackTrace = new StackTrace(callSiteStackDepth + 2, true);
				}
				sentinel = new DisposeSentinel
				{
					m_StackTrace = stackTrace,
					m_IsCreated = 1
				};
				ProfilerUnsafeUtility.EndSample(DisposeSentinel.s_CreateProfilerMarkerPtr);
			}
		}

		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_IsCreated != 0;
				if (flag)
				{
					string filename = "";
					int linenumber = 0;
					ProfilerUnsafeUtility.BeginSample(DisposeSentinel.s_LogErrorProfilerMarkerPtr);
					bool flag2 = this.m_StackTrace != null;
					if (flag2)
					{
						string str = StackTraceUtility.ExtractFormattedStackTrace(this.m_StackTrace);
						string msg = "A Native Collection has not been disposed, resulting in a memory leak. Allocated from:\n" + str;
						bool flag3 = this.m_StackTrace.FrameCount != 0;
						if (flag3)
						{
							filename = this.m_StackTrace.GetFrame(0).GetFileName();
							linenumber = this.m_StackTrace.GetFrame(0).GetFileLineNumber();
						}
						UnsafeUtility.LogError(msg, filename, linenumber);
					}
					else
					{
						string msg2 = "A Native Collection has not been disposed, resulting in a memory leak. Enable Full StackTraces to get more details.";
						UnsafeUtility.LogError(msg2, filename, linenumber);
					}
					ProfilerUnsafeUtility.EndSample(DisposeSentinel.s_LogErrorProfilerMarkerPtr);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		[BurstDiscard]
		public static void Clear(ref DisposeSentinel sentinel)
		{
			bool flag = sentinel != null;
			if (flag)
			{
				sentinel.m_IsCreated = 0;
				sentinel = null;
			}
		}
	}
}
