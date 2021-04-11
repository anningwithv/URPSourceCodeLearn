using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h"), NativeHeader("Modules/Physics/BatchCommands/RaycastCommand.h")]
	public struct RaycastCommand
	{
		public Vector3 from
		{
			[IsReadOnly]
			get;
			set;
		}

		public Vector3 direction
		{
			[IsReadOnly]
			get;
			set;
		}

		public float distance
		{
			[IsReadOnly]
			get;
			set;
		}

		public int layerMask
		{
			[IsReadOnly]
			get;
			set;
		}

		public int maxHits
		{
			[IsReadOnly]
			get;
			set;
		}

		public RaycastCommand(Vector3 from, Vector3 direction, float distance = 3.40282347E+38f, int layerMask = -5, int maxHits = 1)
		{
			this.from = from;
			this.direction = direction;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = maxHits;
		}

		public static JobHandle ScheduleBatch(NativeArray<RaycastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			BatchQueryJob<RaycastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<RaycastCommand, RaycastHit>(commands, results);
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<RaycastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<RaycastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
			return RaycastCommand.ScheduleRaycastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob);
		}

		[FreeFunction("ScheduleRaycastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleRaycastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob)
		{
			JobHandle result2;
			RaycastCommand.ScheduleRaycastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, out result2);
			return result2;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleRaycastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, out JobHandle ret);
	}
}
