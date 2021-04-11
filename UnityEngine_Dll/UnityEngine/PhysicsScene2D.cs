using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics2D/Public/PhysicsSceneHandle2D.h")]
	public struct PhysicsScene2D : IEquatable<PhysicsScene2D>
	{
		private int m_Handle;

		public override string ToString()
		{
			return UnityString.Format("({0})", new object[]
			{
				this.m_Handle
			});
		}

		public static bool operator ==(PhysicsScene2D lhs, PhysicsScene2D rhs)
		{
			return lhs.m_Handle == rhs.m_Handle;
		}

		public static bool operator !=(PhysicsScene2D lhs, PhysicsScene2D rhs)
		{
			return lhs.m_Handle != rhs.m_Handle;
		}

		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		public override bool Equals(object other)
		{
			bool flag = !(other is PhysicsScene2D);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				PhysicsScene2D physicsScene2D = (PhysicsScene2D)other;
				result = (this.m_Handle == physicsScene2D.m_Handle);
			}
			return result;
		}

		public bool Equals(PhysicsScene2D other)
		{
			return this.m_Handle == other.m_Handle;
		}

		public bool IsValid()
		{
			return PhysicsScene2D.IsValid_Internal(this);
		}

		[NativeMethod("IsPhysicsSceneValid"), StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
		private static bool IsValid_Internal(PhysicsScene2D physicsScene)
		{
			return PhysicsScene2D.IsValid_Internal_Injected(ref physicsScene);
		}

		public bool IsEmpty()
		{
			bool flag = this.IsValid();
			if (flag)
			{
				return PhysicsScene2D.IsEmpty_Internal(this);
			}
			throw new InvalidOperationException("Cannot check if physics scene is empty as it is invalid.");
		}

		[NativeMethod("IsPhysicsWorldEmpty"), StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
		private static bool IsEmpty_Internal(PhysicsScene2D physicsScene)
		{
			return PhysicsScene2D.IsEmpty_Internal_Injected(ref physicsScene);
		}

		public bool Simulate(float step)
		{
			bool flag = this.IsValid();
			if (flag)
			{
				return Physics2D.Simulate_Internal(this, step);
			}
			throw new InvalidOperationException("Cannot simulate the physics scene as it is invalid.");
		}

		public RaycastHit2D Linecast(Vector2 start, Vector2 end, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.Linecast_Internal(this, start, end, contactFilter);
		}

		public RaycastHit2D Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.Linecast_Internal(this, start, end, contactFilter);
		}

		[NativeMethod("Linecast_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D Linecast_Internal(PhysicsScene2D physicsScene, Vector2 start, Vector2 end, ContactFilter2D contactFilter)
		{
			RaycastHit2D result;
			PhysicsScene2D.Linecast_Internal_Injected(ref physicsScene, ref start, ref end, ref contactFilter, out result);
			return result;
		}

		public int Linecast(Vector2 start, Vector2 end, RaycastHit2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.LinecastArray_Internal(this, start, end, contactFilter, results);
		}

		public int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return PhysicsScene2D.LinecastArray_Internal(this, start, end, contactFilter, results);
		}

		[NativeMethod("LinecastArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int LinecastArray_Internal(PhysicsScene2D physicsScene, Vector2 start, Vector2 end, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] RaycastHit2D[] results)
		{
			return PhysicsScene2D.LinecastArray_Internal_Injected(ref physicsScene, ref start, ref end, ref contactFilter, results);
		}

		public int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, List<RaycastHit2D> results)
		{
			return PhysicsScene2D.LinecastNonAllocList_Internal(this, start, end, contactFilter, results);
		}

		[NativeMethod("LinecastList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int LinecastNonAllocList_Internal(PhysicsScene2D physicsScene, Vector2 start, Vector2 end, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
		{
			return PhysicsScene2D.LinecastNonAllocList_Internal_Injected(ref physicsScene, ref start, ref end, ref contactFilter, results);
		}

		public RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.Raycast_Internal(this, origin, direction, distance, contactFilter);
		}

		public RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.Raycast_Internal(this, origin, direction, distance, contactFilter);
		}

		[NativeMethod("Raycast_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D Raycast_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			RaycastHit2D result;
			PhysicsScene2D.Raycast_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, ref contactFilter, out result);
			return result;
		}

		public int Raycast(Vector2 origin, Vector2 direction, float distance, RaycastHit2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.RaycastArray_Internal(this, origin, direction, distance, contactFilter, results);
		}

		public int Raycast(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return PhysicsScene2D.RaycastArray_Internal(this, origin, direction, distance, contactFilter, results);
		}

		[NativeMethod("RaycastArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int RaycastArray_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] RaycastHit2D[] results)
		{
			return PhysicsScene2D.RaycastArray_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, ref contactFilter, results);
		}

		public int Raycast(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
		{
			return PhysicsScene2D.RaycastList_Internal(this, origin, direction, distance, contactFilter, results);
		}

		[NativeMethod("RaycastList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int RaycastList_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
		{
			return PhysicsScene2D.RaycastList_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, ref contactFilter, results);
		}

		public RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.CircleCast_Internal(this, origin, radius, direction, distance, contactFilter);
		}

		public RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.CircleCast_Internal(this, origin, radius, direction, distance, contactFilter);
		}

		[NativeMethod("CircleCast_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D CircleCast_Internal(PhysicsScene2D physicsScene, Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			RaycastHit2D result;
			PhysicsScene2D.CircleCast_Internal_Injected(ref physicsScene, ref origin, radius, ref direction, distance, ref contactFilter, out result);
			return result;
		}

		public int CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, RaycastHit2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.CircleCastArray_Internal(this, origin, radius, direction, distance, contactFilter, results);
		}

		public int CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return PhysicsScene2D.CircleCastArray_Internal(this, origin, radius, direction, distance, contactFilter, results);
		}

		[NativeMethod("CircleCastArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int CircleCastArray_Internal(PhysicsScene2D physicsScene, Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] RaycastHit2D[] results)
		{
			return PhysicsScene2D.CircleCastArray_Internal_Injected(ref physicsScene, ref origin, radius, ref direction, distance, ref contactFilter, results);
		}

		public int CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
		{
			return PhysicsScene2D.CircleCastList_Internal(this, origin, radius, direction, distance, contactFilter, results);
		}

		[NativeMethod("CircleCastList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int CircleCastList_Internal(PhysicsScene2D physicsScene, Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
		{
			return PhysicsScene2D.CircleCastList_Internal_Injected(ref physicsScene, ref origin, radius, ref direction, distance, ref contactFilter, results);
		}

		public RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.BoxCast_Internal(this, origin, size, angle, direction, distance, contactFilter);
		}

		public RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.BoxCast_Internal(this, origin, size, angle, direction, distance, contactFilter);
		}

		[NativeMethod("BoxCast_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D BoxCast_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			RaycastHit2D result;
			PhysicsScene2D.BoxCast_Internal_Injected(ref physicsScene, ref origin, ref size, angle, ref direction, distance, ref contactFilter, out result);
			return result;
		}

		public int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, RaycastHit2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.BoxCastArray_Internal(this, origin, size, angle, direction, distance, contactFilter, results);
		}

		public int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return PhysicsScene2D.BoxCastArray_Internal(this, origin, size, angle, direction, distance, contactFilter, results);
		}

		[NativeMethod("BoxCastArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int BoxCastArray_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] RaycastHit2D[] results)
		{
			return PhysicsScene2D.BoxCastArray_Internal_Injected(ref physicsScene, ref origin, ref size, angle, ref direction, distance, ref contactFilter, results);
		}

		public int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
		{
			return PhysicsScene2D.BoxCastList_Internal(this, origin, size, angle, direction, distance, contactFilter, results);
		}

		[NativeMethod("BoxCastList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int BoxCastList_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
		{
			return PhysicsScene2D.BoxCastList_Internal_Injected(ref physicsScene, ref origin, ref size, angle, ref direction, distance, ref contactFilter, results);
		}

		public RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.CapsuleCast_Internal(this, origin, size, capsuleDirection, angle, direction, distance, contactFilter);
		}

		public RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.CapsuleCast_Internal(this, origin, size, capsuleDirection, angle, direction, distance, contactFilter);
		}

		[NativeMethod("CapsuleCast_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D CapsuleCast_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			RaycastHit2D result;
			PhysicsScene2D.CapsuleCast_Internal_Injected(ref physicsScene, ref origin, ref size, capsuleDirection, angle, ref direction, distance, ref contactFilter, out result);
			return result;
		}

		public int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, RaycastHit2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.CapsuleCastArray_Internal(this, origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
		}

		public int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return PhysicsScene2D.CapsuleCastArray_Internal(this, origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
		}

		[NativeMethod("CapsuleCastArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int CapsuleCastArray_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] RaycastHit2D[] results)
		{
			return PhysicsScene2D.CapsuleCastArray_Internal_Injected(ref physicsScene, ref origin, ref size, capsuleDirection, angle, ref direction, distance, ref contactFilter, results);
		}

		public int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
		{
			return PhysicsScene2D.CapsuleCastList_Internal(this, origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
		}

		[NativeMethod("CapsuleCastList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int CapsuleCastList_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
		{
			return PhysicsScene2D.CapsuleCastList_Internal_Injected(ref physicsScene, ref origin, ref size, capsuleDirection, angle, ref direction, distance, ref contactFilter, results);
		}

		public RaycastHit2D GetRayIntersection(Ray ray, float distance, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			return PhysicsScene2D.GetRayIntersection_Internal(this, ray.origin, ray.direction, distance, layerMask);
		}

		[NativeMethod("GetRayIntersection_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D GetRayIntersection_Internal(PhysicsScene2D physicsScene, Vector3 origin, Vector3 direction, float distance, int layerMask)
		{
			RaycastHit2D result;
			PhysicsScene2D.GetRayIntersection_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, layerMask, out result);
			return result;
		}

		public int GetRayIntersection(Ray ray, float distance, RaycastHit2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			return PhysicsScene2D.GetRayIntersectionArray_Internal(this, ray.origin, ray.direction, distance, layerMask, results);
		}

		[NativeMethod("GetRayIntersectionArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int GetRayIntersectionArray_Internal(PhysicsScene2D physicsScene, Vector3 origin, Vector3 direction, float distance, int layerMask, [NotNull("ArgumentNullException")] RaycastHit2D[] results)
		{
			return PhysicsScene2D.GetRayIntersectionArray_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, layerMask, results);
		}

		[NativeMethod("GetRayIntersectionList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int GetRayIntersectionList_Internal(PhysicsScene2D physicsScene, Vector3 origin, Vector3 direction, float distance, int layerMask, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
		{
			return PhysicsScene2D.GetRayIntersectionList_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, layerMask, results);
		}

		public Collider2D OverlapPoint(Vector2 point, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapPoint_Internal(this, point, contactFilter);
		}

		public Collider2D OverlapPoint(Vector2 point, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.OverlapPoint_Internal(this, point, contactFilter);
		}

		[NativeMethod("OverlapPoint_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static Collider2D OverlapPoint_Internal(PhysicsScene2D physicsScene, Vector2 point, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.OverlapPoint_Internal_Injected(ref physicsScene, ref point, ref contactFilter);
		}

		public int OverlapPoint(Vector2 point, Collider2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapPointArray_Internal(this, point, contactFilter, results);
		}

		public int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapPointArray_Internal(this, point, contactFilter, results);
		}

		[NativeMethod("OverlapPointArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int OverlapPointArray_Internal(PhysicsScene2D physicsScene, Vector2 point, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] Collider2D[] results)
		{
			return PhysicsScene2D.OverlapPointArray_Internal_Injected(ref physicsScene, ref point, ref contactFilter, results);
		}

		public int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapPointList_Internal(this, point, contactFilter, results);
		}

		[NativeMethod("OverlapPointList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int OverlapPointList_Internal(PhysicsScene2D physicsScene, Vector2 point, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapPointList_Internal_Injected(ref physicsScene, ref point, ref contactFilter, results);
		}

		public Collider2D OverlapCircle(Vector2 point, float radius, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapCircle_Internal(this, point, radius, contactFilter);
		}

		public Collider2D OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.OverlapCircle_Internal(this, point, radius, contactFilter);
		}

		[NativeMethod("OverlapCircle_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static Collider2D OverlapCircle_Internal(PhysicsScene2D physicsScene, Vector2 point, float radius, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.OverlapCircle_Internal_Injected(ref physicsScene, ref point, radius, ref contactFilter);
		}

		public int OverlapCircle(Vector2 point, float radius, Collider2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapCircleArray_Internal(this, point, radius, contactFilter, results);
		}

		public int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapCircleArray_Internal(this, point, radius, contactFilter, results);
		}

		[NativeMethod("OverlapCircleArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int OverlapCircleArray_Internal(PhysicsScene2D physicsScene, Vector2 point, float radius, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] Collider2D[] results)
		{
			return PhysicsScene2D.OverlapCircleArray_Internal_Injected(ref physicsScene, ref point, radius, ref contactFilter, results);
		}

		public int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapCircleList_Internal(this, point, radius, contactFilter, results);
		}

		[NativeMethod("OverlapCircleList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int OverlapCircleList_Internal(PhysicsScene2D physicsScene, Vector2 point, float radius, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapCircleList_Internal_Injected(ref physicsScene, ref point, radius, ref contactFilter, results);
		}

		public Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapBox_Internal(this, point, size, angle, contactFilter);
		}

		public Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.OverlapBox_Internal(this, point, size, angle, contactFilter);
		}

		[NativeMethod("OverlapBox_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static Collider2D OverlapBox_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.OverlapBox_Internal_Injected(ref physicsScene, ref point, ref size, angle, ref contactFilter);
		}

		public int OverlapBox(Vector2 point, Vector2 size, float angle, Collider2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapBoxArray_Internal(this, point, size, angle, contactFilter, results);
		}

		public int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapBoxArray_Internal(this, point, size, angle, contactFilter, results);
		}

		[NativeMethod("OverlapBoxArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int OverlapBoxArray_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] Collider2D[] results)
		{
			return PhysicsScene2D.OverlapBoxArray_Internal_Injected(ref physicsScene, ref point, ref size, angle, ref contactFilter, results);
		}

		public int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapBoxList_Internal(this, point, size, angle, contactFilter, results);
		}

		[NativeMethod("OverlapBoxList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int OverlapBoxList_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapBoxList_Internal_Injected(ref physicsScene, ref point, ref size, angle, ref contactFilter, results);
		}

		public Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return this.OverlapAreaToBoxArray_Internal(pointA, pointB, contactFilter);
		}

		public Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter)
		{
			return this.OverlapAreaToBoxArray_Internal(pointA, pointB, contactFilter);
		}

		private Collider2D OverlapAreaToBoxArray_Internal(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter)
		{
			Vector2 point = (pointA + pointB) * 0.5f;
			Vector2 size = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
			return this.OverlapBox(point, size, 0f, contactFilter);
		}

		public int OverlapArea(Vector2 pointA, Vector2 pointB, Collider2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return this.OverlapAreaToBoxArray_Internal(pointA, pointB, contactFilter, results);
		}

		public int OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return this.OverlapAreaToBoxArray_Internal(pointA, pointB, contactFilter, results);
		}

		private int OverlapAreaToBoxArray_Internal(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, Collider2D[] results)
		{
			Vector2 point = (pointA + pointB) * 0.5f;
			Vector2 size = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
			return this.OverlapBox(point, size, 0f, contactFilter, results);
		}

		public int OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return this.OverlapAreaToBoxList_Internal(pointA, pointB, contactFilter, results);
		}

		private int OverlapAreaToBoxList_Internal(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			Vector2 point = (pointA + pointB) * 0.5f;
			Vector2 size = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
			return this.OverlapBox(point, size, 0f, contactFilter, results);
		}

		public Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapCapsule_Internal(this, point, size, direction, angle, contactFilter);
		}

		public Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.OverlapCapsule_Internal(this, point, size, direction, angle, contactFilter);
		}

		[NativeMethod("OverlapCapsule_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static Collider2D OverlapCapsule_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.OverlapCapsule_Internal_Injected(ref physicsScene, ref point, ref size, direction, angle, ref contactFilter);
		}

		public int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapCapsuleArray_Internal(this, point, size, direction, angle, contactFilter, results);
		}

		public int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapCapsuleArray_Internal(this, point, size, direction, angle, contactFilter, results);
		}

		[NativeMethod("OverlapCapsuleArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int OverlapCapsuleArray_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] Collider2D[] results)
		{
			return PhysicsScene2D.OverlapCapsuleArray_Internal_Injected(ref physicsScene, ref point, ref size, direction, angle, ref contactFilter, results);
		}

		public int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapCapsuleList_Internal(this, point, size, direction, angle, contactFilter, results);
		}

		[NativeMethod("OverlapCapsuleList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int OverlapCapsuleList_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapCapsuleList_Internal_Injected(ref physicsScene, ref point, ref size, direction, angle, ref contactFilter, results);
		}

		public static int OverlapCollider(Collider2D collider, Collider2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapColliderArray_Internal(collider, contactFilter, results);
		}

		public static int OverlapCollider(Collider2D collider, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapColliderArray_Internal(collider, contactFilter, results);
		}

		[NativeMethod("OverlapColliderArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int OverlapColliderArray_Internal([NotNull("ArgumentNullException")] Collider2D collider, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] Collider2D[] results)
		{
			return PhysicsScene2D.OverlapColliderArray_Internal_Injected(collider, ref contactFilter, results);
		}

		public static int OverlapCollider(Collider2D collider, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapColliderList_Internal(collider, contactFilter, results);
		}

		[NativeMethod("OverlapColliderList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int OverlapColliderList_Internal([NotNull("ArgumentNullException")] Collider2D collider, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapColliderList_Internal_Injected(collider, ref contactFilter, results);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValid_Internal_Injected(ref PhysicsScene2D physicsScene);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsEmpty_Internal_Injected(ref PhysicsScene2D physicsScene);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Linecast_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 start, ref Vector2 end, ref ContactFilter2D contactFilter, out RaycastHit2D ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LinecastArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 start, ref Vector2 end, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LinecastNonAllocList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 start, ref Vector2 end, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Raycast_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, out RaycastHit2D ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RaycastArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RaycastList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CircleCast_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, float radius, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, out RaycastHit2D ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CircleCastArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, float radius, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CircleCastList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, float radius, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BoxCast_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, out RaycastHit2D ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int BoxCastArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int BoxCastList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CapsuleCast_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, CapsuleDirection2D capsuleDirection, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, out RaycastHit2D ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CapsuleCastArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, CapsuleDirection2D capsuleDirection, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CapsuleCastList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, CapsuleDirection2D capsuleDirection, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRayIntersection_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector3 origin, ref Vector3 direction, float distance, int layerMask, out RaycastHit2D ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRayIntersectionArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector3 origin, ref Vector3 direction, float distance, int layerMask, RaycastHit2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRayIntersectionList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector3 origin, ref Vector3 direction, float distance, int layerMask, List<RaycastHit2D> results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D OverlapPoint_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapPointArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref ContactFilter2D contactFilter, Collider2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapPointList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref ContactFilter2D contactFilter, List<Collider2D> results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D OverlapCircle_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, float radius, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapCircleArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, float radius, ref ContactFilter2D contactFilter, Collider2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapCircleList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, float radius, ref ContactFilter2D contactFilter, List<Collider2D> results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D OverlapBox_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, float angle, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapBoxArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, float angle, ref ContactFilter2D contactFilter, Collider2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapBoxList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, float angle, ref ContactFilter2D contactFilter, List<Collider2D> results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D OverlapCapsule_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, CapsuleDirection2D direction, float angle, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapCapsuleArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, CapsuleDirection2D direction, float angle, ref ContactFilter2D contactFilter, Collider2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapCapsuleList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, CapsuleDirection2D direction, float angle, ref ContactFilter2D contactFilter, List<Collider2D> results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapColliderArray_Internal_Injected(Collider2D collider, ref ContactFilter2D contactFilter, Collider2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapColliderList_Internal_Injected(Collider2D collider, ref ContactFilter2D contactFilter, List<Collider2D> results);
	}
}
