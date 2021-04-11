using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/BatchCommands/SpherecastCommand.h"), NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	public struct SpherecastCommand
	{
		public Vector3 origin
		{
			[IsReadOnly]
			get;
			set;
		}

		public float radius
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

		internal int maxHits
		{
			[IsReadOnly]
			get;
			set;
		}

		public SpherecastCommand(Vector3 origin, float radius, Vector3 direction, float distance = 3.40282347E+38f, int layerMask = -5)
		{
			this.origin = origin;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = 1;
		}

		public static JobHandle ScheduleBatch(NativeArray<SpherecastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			BatchQueryJob<SpherecastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<SpherecastCommand, RaycastHit>(commands, results);
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<SpherecastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<SpherecastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
			return SpherecastCommand.ScheduleSpherecastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<SpherecastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob);
		}

		[FreeFunction("ScheduleSpherecastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleSpherecastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob)
		{
			JobHandle result2;
			SpherecastCommand.ScheduleSpherecastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, out result2);
			return result2;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleSpherecastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, out JobHandle ret);
	}
}
