using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/CharacterController.h")]
	public class CharacterController : Collider
	{
		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.get_velocity_Injected(out result);
				return result;
			}
		}

		public extern bool isGrounded
		{
			[NativeName("IsGrounded")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern CollisionFlags collisionFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float radius
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float height
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

		public extern float slopeLimit
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float stepOffset
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float skinWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float minMoveDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool detectCollisions
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool enableOverlapRecovery
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public bool SimpleMove(Vector3 speed)
		{
			return this.SimpleMove_Injected(ref speed);
		}

		public CollisionFlags Move(Vector3 motion)
		{
			return this.Move_Injected(ref motion);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SimpleMove_Injected(ref Vector3 speed);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern CollisionFlags Move_Injected(ref Vector3 motion);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_velocity_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_center_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_center_Injected(ref Vector3 value);
	}
}
