using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.Animations
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct ProcessAnimationJobStruct<T> where T : struct, IAnimationJob
	{
		public delegate void ExecuteJobFunction(ref T data, IntPtr animationStreamPtr, IntPtr unusedPtr, ref JobRanges ranges, int jobIndex);

		private static IntPtr jobReflectionData;

		public static IntPtr GetJobReflectionData()
		{
			bool flag = ProcessAnimationJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				ProcessAnimationJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new ProcessAnimationJobStruct<T>.ExecuteJobFunction(ProcessAnimationJobStruct<T>.Execute), null, null);
			}
			return ProcessAnimationJobStruct<T>.jobReflectionData;
		}

		public unsafe static void Execute(ref T data, IntPtr animationStreamPtr, IntPtr methodIndex, ref JobRanges ranges, int jobIndex)
		{
			AnimationStream stream;
			UnsafeUtility.CopyPtrToStructure<AnimationStream>((void*)animationStreamPtr, out stream);
			JobMethodIndex jobMethodIndex = (JobMethodIndex)methodIndex.ToInt32();
			JobMethodIndex jobMethodIndex2 = jobMethodIndex;
			JobMethodIndex jobMethodIndex3 = jobMethodIndex2;
			if (jobMethodIndex3 != JobMethodIndex.ProcessRootMotionMethodIndex)
			{
				if (jobMethodIndex3 != JobMethodIndex.ProcessAnimationMethodIndex)
				{
					throw new NotImplementedException("Invalid Animation jobs method index.");
				}
				data.ProcessAnimation(stream);
			}
			else
			{
				data.ProcessRootMotion(stream);
			}
		}
	}
}
