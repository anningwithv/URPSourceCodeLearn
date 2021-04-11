using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/BatchCommands/BoxcastCommand.h"), NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	public struct BoxcastCommand
	{
		public Vector3 center
		{
			[IsReadOnly]
			get;
			set;
		}

		public Vector3 halfExtents
		{
			[IsReadOnly]
			get;
			set;
		}

		public Quaternion orientation
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

		public BoxcastCommand(Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float distance = 3.40282347E+38f, int layerMask = -5)
		{
			this.center = center;
			this.halfExtents = halfExtents;
			this.orientation = orientation;
			this.direction = direction;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = 1;
		}

		public static JobHandle ScheduleBatch(NativeArray<BoxcastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			BatchQueryJob<BoxcastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<BoxcastCommand, RaycastHit>(commands, results);
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<BoxcastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<BoxcastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
			return BoxcastCommand.ScheduleBoxcastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<BoxcastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob);
		}

		[FreeFunction("ScheduleBoxcastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleBoxcastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob)
		{
			JobHandle result2;
			BoxcastCommand.ScheduleBoxcastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, out result2);
			return result2;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleBoxcastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, out JobHandle ret);
	}
}
