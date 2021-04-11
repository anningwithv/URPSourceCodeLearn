using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/HingeJoint.h"), NativeClass("Unity::HingeJoint")]
	public class HingeJoint : Joint
	{
		public JointMotor motor
		{
			get
			{
				JointMotor result;
				this.get_motor_Injected(out result);
				return result;
			}
			set
			{
				this.set_motor_Injected(ref value);
			}
		}

		public JointLimits limits
		{
			get
			{
				JointLimits result;
				this.get_limits_Injected(out result);
				return result;
			}
			set
			{
				this.set_limits_Injected(ref value);
			}
		}

		public JointSpring spring
		{
			get
			{
				JointSpring result;
				this.get_spring_Injected(out result);
				return result;
			}
			set
			{
				this.set_spring_Injected(ref value);
			}
		}

		public extern bool useMotor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool useLimits
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool useSpring
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float velocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float angle
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_motor_Injected(out JointMotor ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_motor_Injected(ref JointMotor value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_limits_Injected(out JointLimits ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_limits_Injected(ref JointLimits value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_spring_Injected(out JointSpring ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_spring_Injected(ref JointSpring value);
	}
}
