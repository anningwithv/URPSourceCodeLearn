using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/BatchCommands/CapsulecastCommand.h"), NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	public struct CapsulecastCommand
	{
		public Vector3 point1
		{
			[IsReadOnly]
			get;
			set;
		}

		public Vector3 point2
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

		public CapsulecastCommand(Vector3 p1, Vector3 p2, float radius, Vector3 direction, float distance = 3.40282347E+38f, int layerMask = -5)
		{
			this.point1 = p1;
			this.point2 = p2;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = 1;
		}

		public static JobHandle ScheduleBatch(NativeArray<CapsulecastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			BatchQueryJob<CapsulecastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<CapsulecastCommand, RaycastHit>(commands, results);
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<CapsulecastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<CapsulecastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
			return CapsulecastCommand.ScheduleCapsulecastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<CapsulecastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob);
		}

		[FreeFunction("ScheduleCapsulecastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleCapsulecastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob)
		{
			JobHandle result2;
			CapsulecastCommand.ScheduleCapsulecastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, out result2);
			return result2;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleCapsulecastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, out JobHandle ret);
	}
}
