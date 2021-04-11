using System;
using System.Runtime.CompilerServices;
using Unity.Jobs;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.AI
{
	[StaticAccessor("NavMeshWorldBindings", StaticAccessorType.DoubleColon)]
	public struct NavMeshWorld
	{
		internal IntPtr world;

		public bool IsValid()
		{
			return this.world != IntPtr.Zero;
		}

		public static NavMeshWorld GetDefaultWorld()
		{
			NavMeshWorld result;
			NavMeshWorld.GetDefaultWorld_Injected(out result);
			return result;
		}

		private static void AddDependencyInternal(IntPtr navmesh, JobHandle handle)
		{
			NavMeshWorld.AddDependencyInternal_Injected(navmesh, ref handle);
		}

		public void AddDependency(JobHandle job)
		{
			bool flag = !this.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("The NavMesh world is invalid.");
			}
			NavMeshWorld.AddDependencyInternal(this.world, job);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDefaultWorld_Injected(out NavMeshWorld ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddDependencyInternal_Injected(IntPtr navmesh, ref JobHandle handle);
	}
}
