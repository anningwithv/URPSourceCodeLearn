using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	[NativeHeader("Modules/AI/Components/NavMeshObstacle.bindings.h"), MovedFrom("UnityEngine")]
	public sealed class NavMeshObstacle : Behaviour
	{
		public extern float height
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float radius
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.get_velocity_Injected(out result);
				return result;
			}
			set
			{
				this.set_velocity_Injected(ref value);
			}
		}

		public extern bool carving
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool carveOnlyStationary
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("MoveThreshold")]
		public extern float carvingMoveThreshold
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("TimeToStationary")]
		public extern float carvingTimeToStationary
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern NavMeshObstacleShape shape
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 center
		{
			get
			{
				Vector3 result;
				this.get_center_Injected(out result);
				return result;
			}
			set
			{
				this.set_center_Injected(ref value);
			}
		}

		public Vector3 size
		{
			[FreeFunction("NavMeshObstacleScriptBindings::GetSize", HasExplicitThis = true)]
			get
			{
				Vector3 result;
				this.get_size_Injected(out result);
				return result;
			}
			[FreeFunction("NavMeshObstacleScriptBindings::SetSize", HasExplicitThis = true)]
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		[FreeFunction("NavMeshObstacleScriptBindings::FitExtents", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void FitExtents();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_velocity_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_velocity_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_center_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_center_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_size_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_size_Injected(ref Vector3 value);
	}
}
