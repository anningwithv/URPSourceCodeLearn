using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/Public/PhysicsSceneHandle.h")]
	public struct PhysicsScene : IEquatable<PhysicsScene>
	{
		private int m_Handle;

		public override string ToString()
		{
			return UnityString.Format("({0})", new object[]
			{
				this.m_Handle
			});
		}

		public static bool operator ==(PhysicsScene lhs, PhysicsScene rhs)
		{
			return lhs.m_Handle == rhs.m_Handle;
		}

		public static bool operator !=(PhysicsScene lhs, PhysicsScene rhs)
		{
			return lhs.m_Handle != rhs.m_Handle;
		}

		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		public override bool Equals(object other)
		{
			bool flag = !(other is PhysicsScene);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				PhysicsScene physicsScene = (PhysicsScene)other;
				result = (this.m_Handle == physicsScene.m_Handle);
			}
			return result;
		}

		public bool Equals(PhysicsScene other)
		{
			return this.m_Handle == other.m_Handle;
		}

		public bool IsValid()
		{
			return PhysicsScene.IsValid_Internal(this);
		}

		[NativeMethod("IsPhysicsSceneValid"), StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
		private static bool IsValid_Internal(PhysicsScene physicsScene)
		{
			return PhysicsScene.IsValid_Internal_Injected(ref physicsScene);
		}

		public bool IsEmpty()
		{
			bool flag = this.IsValid();
			if (flag)
			{
				return PhysicsScene.IsEmpty_Internal(this);
			}
			throw new InvalidOperationException("Cannot check if physics scene is empty as it is invalid.");
		}

		[NativeMethod("IsPhysicsWorldEmpty"), StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
		private static bool IsEmpty_Internal(PhysicsScene physicsScene)
		{
			return PhysicsScene.IsEmpty_Internal_Injected(ref physicsScene);
		}

		public void Simulate(float step)
		{
			bool flag = this.IsValid();
			if (flag)
			{
				bool flag2 = this == Physics.defaultPhysicsScene && Physics.autoSimulation;
				if (flag2)
				{
					Debug.LogWarning("PhysicsScene.Simulate(...) was called but auto simulation is active. You should disable auto simulation first before calling this function therefore the simulation was not run.");
				}
				else
				{
					Physics.Simulate_Internal(this, step);
				}
				return;
			}
			throw new InvalidOperationException("Cannot simulate the physics scene as it is invalid.");
		}

		public bool Raycast(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > 1.401298E-45f;
			bool result;
			if (flag)
			{
				Vector3 direction2 = direction / magnitude;
				Ray ray = new Ray(origin, direction2);
				result = PhysicsScene.Internal_RaycastTest(this, ray, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				result = false;
			}
			return result;
		}

		[NativeName("RaycastTest"), StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
		private static bool Internal_RaycastTest(PhysicsScene physicsScene, Ray ray, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_RaycastTest_Injected(ref physicsScene, ref ray, maxDistance, layerMask, queryTriggerInteraction);
		}

		public bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			hitInfo = default(RaycastHit);
			float magnitude = direction.magnitude;
			bool flag = magnitude > 1.401298E-45f;
			bool result;
			if (flag)
			{
				Vector3 direction2 = direction / magnitude;
				Ray ray = new Ray(origin, direction2);
				result = PhysicsScene.Internal_Raycast(this, ray, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
			}
			else
			{
				result = false;
			}
			return result;
		}

		[NativeName("Raycast"), StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
		private static bool Internal_Raycast(PhysicsScene physicsScene, Ray ray, float maxDistance, ref RaycastHit hit, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_Raycast_Injected(ref physicsScene, ref ray, maxDistance, ref hit, layerMask, queryTriggerInteraction);
		}

		public int Raycast(Vector3 origin, Vector3 direction, RaycastHit[] raycastHits, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > 1.401298E-45f;
			int result;
			if (flag)
			{
				Ray ray = new Ray(origin, direction.normalized);
				result = PhysicsScene.Internal_RaycastNonAlloc(this, ray, raycastHits, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		[NativeName("RaycastNonAlloc"), StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		private static int Internal_RaycastNonAlloc(PhysicsScene physicsScene, Ray ray, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_RaycastNonAlloc_Injected(ref physicsScene, ref ray, raycastHits, maxDistance, mask, queryTriggerInteraction);
		}

		[NativeName("CapsuleCast"), StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
		private static bool Query_CapsuleCast(PhysicsScene physicsScene, Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Query_CapsuleCast_Injected(ref physicsScene, ref point1, ref point2, radius, ref direction, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
		}

		private static bool Internal_CapsuleCast(PhysicsScene physicsScene, Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			hitInfo = default(RaycastHit);
			bool flag = magnitude > 1.401298E-45f;
			bool result;
			if (flag)
			{
				Vector3 direction2 = direction / magnitude;
				result = PhysicsScene.Query_CapsuleCast(physicsScene, point1, point2, radius, direction2, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
			}
			else
			{
				result = false;
			}
			return result;
		}

		public bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return PhysicsScene.Internal_CapsuleCast(this, point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		[NativeName("CapsuleCastNonAlloc"), StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		private static int Internal_CapsuleCastNonAlloc(PhysicsScene physicsScene, Vector3 p0, Vector3 p1, float radius, Vector3 direction, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_CapsuleCastNonAlloc_Injected(ref physicsScene, ref p0, ref p1, radius, ref direction, raycastHits, maxDistance, mask, queryTriggerInteraction);
		}

		public int CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > 1.401298E-45f;
			int result;
			if (flag)
			{
				result = PhysicsScene.Internal_CapsuleCastNonAlloc(this, point1, point2, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		[NativeName("OverlapCapsuleNonAlloc"), StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		private static int OverlapCapsuleNonAlloc_Internal(PhysicsScene physicsScene, Vector3 point0, Vector3 point1, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.OverlapCapsuleNonAlloc_Internal_Injected(ref physicsScene, ref point0, ref point1, radius, results, layerMask, queryTriggerInteraction);
		}

		public int OverlapCapsule(Vector3 point0, Vector3 point1, float radius, Collider[] results, [DefaultValue("AllLayers")] int layerMask = -1, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return PhysicsScene.OverlapCapsuleNonAlloc_Internal(this, point0, point1, radius, results, layerMask, queryTriggerInteraction);
		}

		[NativeName("SphereCast"), StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
		private static bool Query_SphereCast(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Query_SphereCast_Injected(ref physicsScene, ref origin, radius, ref direction, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
		}

		private static bool Internal_SphereCast(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			hitInfo = default(RaycastHit);
			bool flag = magnitude > 1.401298E-45f;
			bool result;
			if (flag)
			{
				Vector3 direction2 = direction / magnitude;
				result = PhysicsScene.Query_SphereCast(physicsScene, origin, radius, direction2, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
			}
			else
			{
				result = false;
			}
			return result;
		}

		public bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return PhysicsScene.Internal_SphereCast(this, origin, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		[NativeName("SphereCastNonAlloc"), StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		private static int Internal_SphereCastNonAlloc(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_SphereCastNonAlloc_Injected(ref physicsScene, ref origin, radius, ref direction, raycastHits, maxDistance, mask, queryTriggerInteraction);
		}

		public int SphereCast(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > 1.401298E-45f;
			int result;
			if (flag)
			{
				result = PhysicsScene.Internal_SphereCastNonAlloc(this, origin, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		[NativeName("OverlapSphereNonAlloc"), StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		private static int OverlapSphereNonAlloc_Internal(PhysicsScene physicsScene, Vector3 position, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.OverlapSphereNonAlloc_Internal_Injected(ref physicsScene, ref position, radius, results, layerMask, queryTriggerInteraction);
		}

		public int OverlapSphere(Vector3 position, float radius, Collider[] results, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.OverlapSphereNonAlloc_Internal(this, position, radius, results, layerMask, queryTriggerInteraction);
		}

		[NativeName("BoxCast"), StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
		private static bool Query_BoxCast(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, ref RaycastHit outHit, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Query_BoxCast_Injected(ref physicsScene, ref center, ref halfExtents, ref direction, ref orientation, maxDistance, ref outHit, layerMask, queryTriggerInteraction);
		}

		private static bool Internal_BoxCast(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			hitInfo = default(RaycastHit);
			bool flag = magnitude > 1.401298E-45f;
			bool result;
			if (flag)
			{
				Vector3 direction2 = direction / magnitude;
				result = PhysicsScene.Query_BoxCast(physicsScene, center, halfExtents, direction2, orientation, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
			}
			else
			{
				result = false;
			}
			return result;
		}

		public bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return PhysicsScene.Internal_BoxCast(this, center, halfExtents, orientation, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		[ExcludeFromDocs]
		public bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo)
		{
			return PhysicsScene.Internal_BoxCast(this, center, halfExtents, Quaternion.identity, direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		[NativeName("OverlapBoxNonAlloc"), StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		private static int OverlapBoxNonAlloc_Internal(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.OverlapBoxNonAlloc_Internal_Injected(ref physicsScene, ref center, ref halfExtents, results, ref orientation, mask, queryTriggerInteraction);
		}

		public int OverlapBox(Vector3 center, Vector3 halfExtents, Collider[] results, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return PhysicsScene.OverlapBoxNonAlloc_Internal(this, center, halfExtents, results, orientation, layerMask, queryTriggerInteraction);
		}

		[ExcludeFromDocs]
		public int OverlapBox(Vector3 center, Vector3 halfExtents, Collider[] results)
		{
			return PhysicsScene.OverlapBoxNonAlloc_Internal(this, center, halfExtents, results, Quaternion.identity, -5, QueryTriggerInteraction.UseGlobal);
		}

		[NativeName("BoxCastNonAlloc"), StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		private static int Internal_BoxCastNonAlloc(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] raycastHits, Quaternion orientation, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_BoxCastNonAlloc_Injected(ref physicsScene, ref center, ref halfExtents, ref direction, raycastHits, ref orientation, maxDistance, mask, queryTriggerInteraction);
		}

		public int BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > 1.401298E-45f;
			int result;
			if (flag)
			{
				result = PhysicsScene.Internal_BoxCastNonAlloc(this, center, halfExtents, direction, results, orientation, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		[ExcludeFromDocs]
		public int BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results)
		{
			return this.BoxCast(center, halfExtents, direction, results, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValid_Internal_Injected(ref PhysicsScene physicsScene);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsEmpty_Internal_Injected(ref PhysicsScene physicsScene);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_RaycastTest_Injected(ref PhysicsScene physicsScene, ref Ray ray, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_Raycast_Injected(ref PhysicsScene physicsScene, ref Ray ray, float maxDistance, ref RaycastHit hit, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_RaycastNonAlloc_Injected(ref PhysicsScene physicsScene, ref Ray ray, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Query_CapsuleCast_Injected(ref PhysicsScene physicsScene, ref Vector3 point1, ref Vector3 point2, float radius, ref Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_CapsuleCastNonAlloc_Injected(ref PhysicsScene physicsScene, ref Vector3 p0, ref Vector3 p1, float radius, ref Vector3 direction, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapCapsuleNonAlloc_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 point0, ref Vector3 point1, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Query_SphereCast_Injected(ref PhysicsScene physicsScene, ref Vector3 origin, float radius, ref Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_SphereCastNonAlloc_Injected(ref PhysicsScene physicsScene, ref Vector3 origin, float radius, ref Vector3 direction, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapSphereNonAlloc_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 position, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Query_BoxCast_Injected(ref PhysicsScene physicsScene, ref Vector3 center, ref Vector3 halfExtents, ref Vector3 direction, ref Quaternion orientation, float maxDistance, ref RaycastHit outHit, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapBoxNonAlloc_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 center, ref Vector3 halfExtents, Collider[] results, ref Quaternion orientation, int mask, QueryTriggerInteraction queryTriggerInteraction);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_BoxCastNonAlloc_Injected(ref PhysicsScene physicsScene, ref Vector3 center, ref Vector3 halfExtents, ref Vector3 direction, RaycastHit[] raycastHits, ref Quaternion orientation, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);
	}
}
