using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/ArticulationBody.h"), NativeClass("Unity::ArticulationBody")]
	public class ArticulationBody : Behaviour
	{
		public extern ArticulationJointType jointType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 anchorPosition
		{
			get
			{
				Vector3 result;
				this.get_anchorPosition_Injected(out result);
				return result;
			}
			set
			{
				this.set_anchorPosition_Injected(ref value);
			}
		}

		public Vector3 parentAnchorPosition
		{
			get
			{
				Vector3 result;
				this.get_parentAnchorPosition_Injected(out result);
				return result;
			}
			set
			{
				this.set_parentAnchorPosition_Injected(ref value);
			}
		}

		public Quaternion anchorRotation
		{
			get
			{
				Quaternion result;
				this.get_anchorRotation_Injected(out result);
				return result;
			}
			set
			{
				this.set_anchorRotation_Injected(ref value);
			}
		}

		public Quaternion parentAnchorRotation
		{
			get
			{
				Quaternion result;
				this.get_parentAnchorRotation_Injected(out result);
				return result;
			}
			set
			{
				this.set_parentAnchorRotation_Injected(ref value);
			}
		}

		public extern bool isRoot
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ArticulationDofLock linearLockX
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ArticulationDofLock linearLockY
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ArticulationDofLock linearLockZ
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ArticulationDofLock swingYLock
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ArticulationDofLock swingZLock
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ArticulationDofLock twistLock
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public ArticulationDrive xDrive
		{
			get
			{
				ArticulationDrive result;
				this.get_xDrive_Injected(out result);
				return result;
			}
			set
			{
				this.set_xDrive_Injected(ref value);
			}
		}

		public ArticulationDrive yDrive
		{
			get
			{
				ArticulationDrive result;
				this.get_yDrive_Injected(out result);
				return result;
			}
			set
			{
				this.set_yDrive_Injected(ref value);
			}
		}

		public ArticulationDrive zDrive
		{
			get
			{
				ArticulationDrive result;
				this.get_zDrive_Injected(out result);
				return result;
			}
			set
			{
				this.set_zDrive_Injected(ref value);
			}
		}

		public extern bool immovable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool useGravity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float linearDamping
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float angularDamping
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float jointFriction
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

		public Vector3 angularVelocity
		{
			get
			{
				Vector3 result;
				this.get_angularVelocity_Injected(out result);
				return result;
			}
			set
			{
				this.set_angularVelocity_Injected(ref value);
			}
		}

		public extern float mass
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 centerOfMass
		{
			get
			{
				Vector3 result;
				this.get_centerOfMass_Injected(out result);
				return result;
			}
			set
			{
				this.set_centerOfMass_Injected(ref value);
			}
		}

		public Vector3 worldCenterOfMass
		{
			get
			{
				Vector3 result;
				this.get_worldCenterOfMass_Injected(out result);
				return result;
			}
		}

		public Vector3 inertiaTensor
		{
			get
			{
				Vector3 result;
				this.get_inertiaTensor_Injected(out result);
				return result;
			}
			set
			{
				this.set_inertiaTensor_Injected(ref value);
			}
		}

		public Quaternion inertiaTensorRotation
		{
			get
			{
				Quaternion result;
				this.get_inertiaTensorRotation_Injected(out result);
				return result;
			}
			set
			{
				this.set_inertiaTensorRotation_Injected(ref value);
			}
		}

		public extern float sleepThreshold
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int solverIterations
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int solverVelocityIterations
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float maxAngularVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float maxLinearVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float maxJointVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float maxDepenetrationVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public ArticulationReducedSpace jointPosition
		{
			get
			{
				ArticulationReducedSpace result;
				this.get_jointPosition_Injected(out result);
				return result;
			}
			set
			{
				this.set_jointPosition_Injected(ref value);
			}
		}

		public ArticulationReducedSpace jointVelocity
		{
			get
			{
				ArticulationReducedSpace result;
				this.get_jointVelocity_Injected(out result);
				return result;
			}
			set
			{
				this.set_jointVelocity_Injected(ref value);
			}
		}

		public ArticulationReducedSpace jointAcceleration
		{
			get
			{
				ArticulationReducedSpace result;
				this.get_jointAcceleration_Injected(out result);
				return result;
			}
			set
			{
				this.set_jointAcceleration_Injected(ref value);
			}
		}

		public ArticulationReducedSpace jointForce
		{
			get
			{
				ArticulationReducedSpace result;
				this.get_jointForce_Injected(out result);
				return result;
			}
			set
			{
				this.set_jointForce_Injected(ref value);
			}
		}

		public extern int dofCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int index
		{
			[NativeMethod("GetBodyIndex")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public void AddForce(Vector3 force)
		{
			this.AddForce_Injected(ref force);
		}

		public void AddRelativeForce(Vector3 force)
		{
			this.AddRelativeForce_Injected(ref force);
		}

		public void AddTorque(Vector3 torque)
		{
			this.AddTorque_Injected(ref torque);
		}

		public void AddRelativeTorque(Vector3 torque)
		{
			this.AddRelativeTorque_Injected(ref torque);
		}

		public void AddForceAtPosition(Vector3 force, Vector3 position)
		{
			this.AddForceAtPosition_Injected(ref force, ref position);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetCenterOfMass();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetInertiaTensor();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Sleep();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsSleeping();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void WakeUp();

		public void TeleportRoot(Vector3 position, Quaternion rotation)
		{
			this.TeleportRoot_Injected(ref position, ref rotation);
		}

		public Vector3 GetClosestPoint(Vector3 point)
		{
			Vector3 result;
			this.GetClosestPoint_Injected(ref point, out result);
			return result;
		}

		public Vector3 GetRelativePointVelocity(Vector3 relativePoint)
		{
			Vector3 result;
			this.GetRelativePointVelocity_Injected(ref relativePoint, out result);
			return result;
		}

		public Vector3 GetPointVelocity(Vector3 worldPoint)
		{
			Vector3 result;
			this.GetPointVelocity_Injected(ref worldPoint, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetDenseJacobian(ref ArticulationJacobian jacobian);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetJointPositions(List<float> positions);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetJointPositions(List<float> positions);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetJointVelocities(List<float> velocities);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetJointVelocities(List<float> velocities);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetJointAccelerations(List<float> accelerations);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetJointAccelerations(List<float> accelerations);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetJointForces(List<float> forces);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetJointForces(List<float> forces);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetDriveTargets(List<float> targets);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDriveTargets(List<float> targets);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetDriveTargetVelocities(List<float> targetVelocities);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDriveTargetVelocities(List<float> targetVelocities);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetDofStartIndices(List<int> dofStartIndices);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_anchorPosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_anchorPosition_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_parentAnchorPosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_parentAnchorPosition_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_anchorRotation_Injected(out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_anchorRotation_Injected(ref Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_parentAnchorRotation_Injected(out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_parentAnchorRotation_Injected(ref Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_xDrive_Injected(out ArticulationDrive ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_xDrive_Injected(ref ArticulationDrive value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_yDrive_Injected(out ArticulationDrive ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_yDrive_Injected(ref ArticulationDrive value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_zDrive_Injected(out ArticulationDrive ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_zDrive_Injected(ref ArticulationDrive value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddForce_Injected(ref Vector3 force);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddRelativeForce_Injected(ref Vector3 force);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddTorque_Injected(ref Vector3 torque);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddRelativeTorque_Injected(ref Vector3 torque);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddForceAtPosition_Injected(ref Vector3 force, ref Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_velocity_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_velocity_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_angularVelocity_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_angularVelocity_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_centerOfMass_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_centerOfMass_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_worldCenterOfMass_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_inertiaTensor_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_inertiaTensor_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_inertiaTensorRotation_Injected(out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_inertiaTensorRotation_Injected(ref Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_jointPosition_Injected(out ArticulationReducedSpace ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_jointPosition_Injected(ref ArticulationReducedSpace value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_jointVelocity_Injected(out ArticulationReducedSpace ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_jointVelocity_Injected(ref ArticulationReducedSpace value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_jointAcceleration_Injected(out ArticulationReducedSpace ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_jointAcceleration_Injected(ref ArticulationReducedSpace value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_jointForce_Injected(out ArticulationReducedSpace ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_jointForce_Injected(ref ArticulationReducedSpace value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TeleportRoot_Injected(ref Vector3 position, ref Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetClosestPoint_Injected(ref Vector3 point, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetRelativePointVelocity_Injected(ref Vector3 relativePoint, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPointVelocity_Injected(ref Vector3 worldPoint, out Vector3 ret);
	}
}
