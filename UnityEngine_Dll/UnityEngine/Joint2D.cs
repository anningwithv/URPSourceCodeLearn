using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics2D/Joint2D.h"), RequireComponent(typeof(Transform), typeof(Rigidbody2D))]
	public class Joint2D : Behaviour
	{
		public extern Rigidbody2D attachedRigidbody
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Rigidbody2D connectedBody
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool enableCollision
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float breakForce
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float breakTorque
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector2 reactionForce
		{
			[NativeMethod("GetReactionForceFixedTime")]
			get
			{
				Vector2 result;
				this.get_reactionForce_Injected(out result);
				return result;
			}
		}

		public extern float reactionTorque
		{
			[NativeMethod("GetReactionTorqueFixedTime")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Joint2D.collideConnected has been deprecated. Use Joint2D.enableCollision instead (UnityUpgradable) -> enableCollision", true)]
		public bool collideConnected
		{
			get
			{
				return this.enableCollision;
			}
			set
			{
				this.enableCollision = value;
			}
		}

		public Vector2 GetReactionForce(float timeStep)
		{
			Vector2 result;
			this.GetReactionForce_Injected(timeStep, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetReactionTorque(float timeStep);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_reactionForce_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetReactionForce_Injected(float timeStep, out Vector2 ret);
	}
}
