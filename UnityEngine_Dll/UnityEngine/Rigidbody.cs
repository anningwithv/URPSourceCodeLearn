using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/Rigidbody.h"), RequireComponent(typeof(Transform))]
	public class Rigidbody : Component
	{
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

		public extern float drag
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float angularDrag
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float mass
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

		public extern float maxDepenetrationVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isKinematic
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool freezeRotation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern RigidbodyConstraints constraints
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern CollisionDetectionMode collisionDetectionMode
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

		public extern bool detectCollisions
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
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

		public extern RigidbodyInterpolation interpolation
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

		public extern float sleepThreshold
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

		public extern int solverVelocityIterations
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("The sleepVelocity is no longer supported. Use sleepThreshold. Note that sleepThreshold is energy but not velocity.")]
		public extern float sleepVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("The sleepAngularVelocity is no longer supported. Set Use sleepThreshold to specify energy.")]
		public extern float sleepAngularVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Cone friction is no longer supported.")]
		public bool useConeFriction
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[Obsolete("Please use Rigidbody.solverIterations instead. (UnityUpgradable) -> solverIterations")]
		public int solverIterationCount
		{
			get
			{
				return this.solverIterations;
			}
			set
			{
				this.solverIterations = value;
			}
		}

		[Obsolete("Please use Rigidbody.solverVelocityIterations instead. (UnityUpgradable) -> solverVelocityIterations")]
		public int solverVelocityIterationCount
		{
			get
			{
				return this.solverVelocityIterations;
			}
			set
			{
				this.solverVelocityIterations = value;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDensity(float density);

		public void MovePosition(Vector3 position)
		{
			this.MovePosition_Injected(ref position);
		}

		public void MoveRotation(Quaternion rot)
		{
			this.MoveRotation_Injected(ref rot);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Sleep();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsSleeping();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void WakeUp();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetCenterOfMass();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetInertiaTensor();

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

		[Obsolete("Use Rigidbody.maxAngularVelocity instead.")]
		public void SetMaxAngularVelocity(float a)
		{
			this.maxAngularVelocity = a;
		}

		public void AddForce(Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddForce_Injected(ref force, mode);
		}

		[ExcludeFromDocs]
		public void AddForce(Vector3 force)
		{
			this.AddForce(force, ForceMode.Force);
		}

		public void AddForce(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddForce(new Vector3(x, y, z), mode);
		}

		[ExcludeFromDocs]
		public void AddForce(float x, float y, float z)
		{
			this.AddForce(new Vector3(x, y, z), ForceMode.Force);
		}

		public void AddRelativeForce(Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeForce_Injected(ref force, mode);
		}

		[ExcludeFromDocs]
		public void AddRelativeForce(Vector3 force)
		{
			this.AddRelativeForce(force, ForceMode.Force);
		}

		public void AddRelativeForce(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeForce(new Vector3(x, y, z), mode);
		}

		[ExcludeFromDocs]
		public void AddRelativeForce(float x, float y, float z)
		{
			this.AddRelativeForce(new Vector3(x, y, z), ForceMode.Force);
		}

		public void AddTorque(Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddTorque_Injected(ref torque, mode);
		}

		[ExcludeFromDocs]
		public void AddTorque(Vector3 torque)
		{
			this.AddTorque(torque, ForceMode.Force);
		}

		public void AddTorque(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddTorque(new Vector3(x, y, z), mode);
		}

		[ExcludeFromDocs]
		public void AddTorque(float x, float y, float z)
		{
			this.AddTorque(new Vector3(x, y, z), ForceMode.Force);
		}

		public void AddRelativeTorque(Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeTorque_Injected(ref torque, mode);
		}

		[ExcludeFromDocs]
		public void AddRelativeTorque(Vector3 torque)
		{
			this.AddRelativeTorque(torque, ForceMode.Force);
		}

		public void AddRelativeTorque(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeTorque(new Vector3(x, y, z), mode);
		}

		[ExcludeFromDocs]
		public void AddRelativeTorque(float x, float y, float z)
		{
			this.AddRelativeTorque(x, y, z, ForceMode.Force);
		}

		public void AddForceAtPosition(Vector3 force, Vector3 position, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddForceAtPosition_Injected(ref force, ref position, mode);
		}

		[ExcludeFromDocs]
		public void AddForceAtPosition(Vector3 force, Vector3 position)
		{
			this.AddForceAtPosition(force, position, ForceMode.Force);
		}

		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, [DefaultValue("0.0f")] float upwardsModifier, [DefaultValue("ForceMode.Force)")] ForceMode mode)
		{
			this.AddExplosionForce_Injected(explosionForce, ref explosionPosition, explosionRadius, upwardsModifier, mode);
		}

		[ExcludeFromDocs]
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier)
		{
			this.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode.Force);
		}

		[ExcludeFromDocs]
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius)
		{
			this.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, 0f, ForceMode.Force);
		}

		[NativeName("ClosestPointOnBounds")]
		private void Internal_ClosestPointOnBounds(Vector3 point, ref Vector3 outPos, ref float distance)
		{
			this.Internal_ClosestPointOnBounds_Injected(ref point, ref outPos, ref distance);
		}

		public Vector3 ClosestPointOnBounds(Vector3 position)
		{
			float num = 0f;
			Vector3 zero = Vector3.zero;
			this.Internal_ClosestPointOnBounds(position, ref zero, ref num);
			return zero;
		}

		private RaycastHit SweepTest(Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction, ref bool hasHit)
		{
			RaycastHit result;
			this.SweepTest_Injected(ref direction, maxDistance, queryTriggerInteraction, ref hasHit, out result);
			return result;
		}

		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > 1.401298E-45f;
			bool result;
			if (flag)
			{
				Vector3 direction2 = direction / magnitude;
				bool flag2 = false;
				hitInfo = this.SweepTest(direction2, maxDistance, queryTriggerInteraction, ref flag2);
				result = flag2;
			}
			else
			{
				hitInfo = default(RaycastHit);
				result = false;
			}
			return result;
		}

		[ExcludeFromDocs]
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			return this.SweepTest(direction, out hitInfo, maxDistance, QueryTriggerInteraction.UseGlobal);
		}

		[ExcludeFromDocs]
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo)
		{
			return this.SweepTest(direction, out hitInfo, float.PositiveInfinity, QueryTriggerInteraction.UseGlobal);
		}

		[NativeName("SweepTestAll")]
		private RaycastHit[] Internal_SweepTestAll(Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction)
		{
			return this.Internal_SweepTestAll_Injected(ref direction, maxDistance, queryTriggerInteraction);
		}

		public RaycastHit[] SweepTestAll(Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > 1.401298E-45f;
			RaycastHit[] result;
			if (flag)
			{
				Vector3 direction2 = direction / magnitude;
				result = this.Internal_SweepTestAll(direction2, maxDistance, queryTriggerInteraction);
			}
			else
			{
				result = new RaycastHit[0];
			}
			return result;
		}

		[ExcludeFromDocs]
		public RaycastHit[] SweepTestAll(Vector3 direction, float maxDistance)
		{
			return this.SweepTestAll(direction, maxDistance, QueryTriggerInteraction.UseGlobal);
		}

		[ExcludeFromDocs]
		public RaycastHit[] SweepTestAll(Vector3 direction)
		{
			return this.SweepTestAll(direction, float.PositiveInfinity, QueryTriggerInteraction.UseGlobal);
		}

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
		private extern void get_inertiaTensorRotation_Injected(out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_inertiaTensorRotation_Injected(ref Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_inertiaTensor_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_inertiaTensor_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_position_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_position_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotation_Injected(out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotation_Injected(ref Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void MovePosition_Injected(ref Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void MoveRotation_Injected(ref Quaternion rot);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetRelativePointVelocity_Injected(ref Vector3 relativePoint, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPointVelocity_Injected(ref Vector3 worldPoint, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddForce_Injected(ref Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddRelativeForce_Injected(ref Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddTorque_Injected(ref Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddRelativeTorque_Injected(ref Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddForceAtPosition_Injected(ref Vector3 force, ref Vector3 position, [DefaultValue("ForceMode.Force")] ForceMode mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddExplosionForce_Injected(float explosionForce, ref Vector3 explosionPosition, float explosionRadius, [DefaultValue("0.0f")] float upwardsModifier, [DefaultValue("ForceMode.Force)")] ForceMode mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_ClosestPointOnBounds_Injected(ref Vector3 point, ref Vector3 outPos, ref float distance);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SweepTest_Injected(ref Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction, ref bool hasHit, out RaycastHit ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern RaycastHit[] Internal_SweepTestAll_Injected(ref Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction);
	}
}
