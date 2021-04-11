using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	[NativeHeader("Modules/AI/NavMesh/NavMesh.bindings.h")]
	public sealed class NavMeshData : UnityEngine.Object
	{
		public Bounds sourceBounds
		{
			get
			{
				Bounds result;
				this.get_sourceBounds_Injected(out result);
				return result;
			}
		}

		public Vector3 position
		{
			get
			{
				Vector3 result;
				this.get_position_Injected(out result);
				return result;
			}
			set
			{
				this.set_position_Injected(ref value);
			}
		}

		public Quaternion rotation
		{
			get
			{
				Quaternion result;
				this.get_rotation_Injected(out result);
				return result;
			}
			set
			{
				this.set_rotation_Injected(ref value);
			}
		}

		public NavMeshData()
		{
			NavMeshData.Internal_Create(this, 0);
		}

		public NavMeshData(int agentTypeID)
		{
			NavMeshData.Internal_Create(this, agentTypeID);
		}

		[StaticAccessor("NavMeshDataBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] NavMeshData mono, int agentTypeID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_sourceBounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_position_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_position_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotation_Injected(out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotation_Injected(ref Quaternion value);
	}
}
