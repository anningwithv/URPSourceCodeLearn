using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/ConstantForce.h"), RequireComponent(typeof(Rigidbody))]
	public class ConstantForce : Behaviour
	{
		public Vector3 force
		{
			get
			{
				Vector3 result;
				this.get_force_Injected(out result);
				return result;
			}
			set
			{
				this.set_force_Injected(ref value);
			}
		}

		public Vector3 relativeForce
		{
			get
			{
				Vector3 result;
				this.get_relativeForce_Injected(out result);
				return result;
			}
			set
			{
				this.set_relativeForce_Injected(ref value);
			}
		}

		public Vector3 torque
		{
			get
			{
				Vector3 result;
				this.get_torque_Injected(out result);
				return result;
			}
			set
			{
				this.set_torque_Injected(ref value);
			}
		}

		public Vector3 relativeTorque
		{
			get
			{
				Vector3 result;
				this.get_relativeTorque_Injected(out result);
				return result;
			}
			set
			{
				this.set_relativeTorque_Injected(ref value);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_force_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_force_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_relativeForce_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_relativeForce_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_torque_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_torque_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_relativeTorque_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_relativeTorque_Injected(ref Vector3 value);
	}
}
