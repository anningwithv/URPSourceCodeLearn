using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/Joint.h"), NativeClass("Unity::Joint"), RequireComponent(typeof(Rigidbody))]
	public class Joint : Component
	{
		public extern Rigidbody connectedBody
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ArticulationBody connectedArticulationBody
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 axis
		{
			get
			{
				Vector3 result;
				this.get_axis_Injected(out result);
				return result;
			}
			set
			{
				this.set_axis_Injected(ref value);
			}
		}

		public Vector3 anchor
		{
			get
			{
				Vector3 result;
				this.get_anchor_Injected(out result);
				return result;
			}
			set
			{
				this.set_anchor_Injected(ref value);
			}
		}

		public Vector3 connectedAnchor
		{
			get
			{
				Vector3 result;
				this.get_connectedAnchor_Injected(out result);
				return result;
			}
			set
			{
				this.set_connectedAnchor_Injected(ref value);
			}
		}

		public extern bool autoConfigureConnectedAnchor
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

		public extern bool enableCollision
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool enablePreprocessing
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float massScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float connectedMassScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 currentForce
		{
			get
			{
				Vector3 zero = Vector3.zero;
				Vector3 zero2 = Vector3.zero;
				this.GetCurrentForces(ref zero, ref zero2);
				return zero;
			}
		}

		public Vector3 currentTorque
		{
			get
			{
				Vector3 zero = Vector3.zero;
				Vector3 zero2 = Vector3.zero;
				this.GetCurrentForces(ref zero, ref zero2);
				return zero2;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCurrentForces(ref Vector3 linearForce, ref Vector3 angularForce);

		internal Matrix4x4 GetLocalPoseMatrix(int bodyIndex)
		{
			Matrix4x4 result;
			this.GetLocalPoseMatrix_Injected(bodyIndex, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_axis_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_axis_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_anchor_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_anchor_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_connectedAnchor_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_connectedAnchor_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLocalPoseMatrix_Injected(int bodyIndex, out Matrix4x4 ret);
	}
}
