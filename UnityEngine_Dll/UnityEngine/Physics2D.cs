using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Physics2DScriptingClasses.h"), NativeHeader("Physics2DScriptingClasses.h"), NativeHeader("Modules/Physics2D/PhysicsManager2D.h"), StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
	public class Physics2D
	{
		public const int IgnoreRaycastLayer = 4;

		public const int DefaultRaycastLayers = -5;

		public const int AllLayers = -1;

		private static List<Rigidbody2D> m_LastDisabledRigidbody2D = new List<Rigidbody2D>();

		public static PhysicsScene2D defaultPhysicsScene
		{
			get
			{
				return default(PhysicsScene2D);
			}
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern int velocityIterations
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern int positionIterations
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static Vector2 gravity
		{
			get
			{
				Vector2 result;
				Physics2D.get_gravity_Injected(out result);
				return result;
			}
			set
			{
				Physics2D.set_gravity_Injected(ref value);
			}
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool queriesHitTriggers
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool queriesStartInColliders
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool callbacksOnDisable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool reuseCollisionCallbacks
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool autoSyncTransforms
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern SimulationMode2D simulationMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static PhysicsJobOptions2D jobOptions
		{
			get
			{
				PhysicsJobOptions2D result;
				Physics2D.get_jobOptions_Injected(out result);
				return result;
			}
			set
			{
				Physics2D.set_jobOptions_Injected(ref value);
			}
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float velocityThreshold
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float maxLinearCorrection
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float maxAngularCorrection
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float maxTranslationSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float maxRotationSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float defaultContactOffset
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float baumgarteScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float baumgarteTOIScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float timeToSleep
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float linearSleepTolerance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float angularSleepTolerance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool alwaysShowColliders
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool showColliderSleep
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool showColliderContacts
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool showColliderAABB
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float contactArrowScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static Color colliderAwakeColor
		{
			get
			{
				Color result;
				Physics2D.get_colliderAwakeColor_Injected(out result);
				return result;
			}
			set
			{
				Physics2D.set_colliderAwakeColor_Injected(ref value);
			}
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static Color colliderAsleepColor
		{
			get
			{
				Color result;
				Physics2D.get_colliderAsleepColor_Injected(out result);
				return result;
			}
			set
			{
				Physics2D.set_colliderAsleepColor_Injected(ref value);
			}
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static Color colliderContactColor
		{
			get
			{
				Color result;
				Physics2D.get_colliderContactColor_Injected(out result);
				return result;
			}
			set
			{
				Physics2D.set_colliderContactColor_Injected(ref value);
			}
		}

		[StaticAccessor("GetPhysics2DSettings()")]
		public static Color colliderAABBColor
		{
			get
			{
				Color result;
				Physics2D.get_colliderAABBColor_Injected(out result);
				return result;
			}
			set
			{
				Physics2D.set_colliderAABBColor_Injected(ref value);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Physics2D.raycastsHitTriggers is deprecated. Use Physics2D.queriesHitTriggers instead. (UnityUpgradable) -> queriesHitTriggers", true)]
		public static bool raycastsHitTriggers
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Physics2D.raycastsStartInColliders is deprecated. Use Physics2D.queriesStartInColliders instead. (UnityUpgradable) -> queriesStartInColliders", true)]
		public static bool raycastsStartInColliders
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Physics2D.deleteStopsCallbacks is deprecated.(UnityUpgradable) -> changeStopsCallbacks", true)]
		public static bool deleteStopsCallbacks
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[Obsolete("Physics2D.changeStopsCallbacks is deprecated and will always return false.", false)]
		public static bool changeStopsCallbacks
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[Obsolete("Physics2D.minPenetrationForPenalty is deprecated. Use Physics2D.defaultContactOffset instead. (UnityUpgradable) -> defaultContactOffset", false)]
		public static float minPenetrationForPenalty
		{
			get
			{
				return Physics2D.defaultContactOffset;
			}
			set
			{
				Physics2D.defaultContactOffset = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Physics2D.autoSimulation is deprecated. Use Physics2D.simulationMode instead.", false)]
		public static bool autoSimulation
		{
			get
			{
				return Physics2D.simulationMode != SimulationMode2D.Script;
			}
			set
			{
				Physics2D.simulationMode = (value ? SimulationMode2D.FixedUpdate : SimulationMode2D.Script);
			}
		}

		public static bool Simulate(float step)
		{
			return Physics2D.Simulate_Internal(Physics2D.defaultPhysicsScene, step);
		}

		[NativeMethod("Simulate_Binding")]
		internal static bool Simulate_Internal(PhysicsScene2D physicsScene, float step)
		{
			return Physics2D.Simulate_Internal_Injected(ref physicsScene, step);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SyncTransforms();

		[ExcludeFromDocs]
		public static void IgnoreCollision([Writable] Collider2D collider1, [Writable] Collider2D collider2)
		{
			Physics2D.IgnoreCollision(collider1, collider2, true);
		}

		[NativeMethod("IgnoreCollision_Binding"), StaticAccessor("PhysicsScene2D", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IgnoreCollision([NotNull("ArgumentNullException"), Writable] Collider2D collider1, [NotNull("ArgumentNullException"), Writable] Collider2D collider2, [UnityEngine.Internal.DefaultValue("true")] bool ignore);

		[NativeMethod("GetIgnoreCollision_Binding"), StaticAccessor("PhysicsScene2D", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetIgnoreCollision([NotNull("ArgumentNullException"), Writable] Collider2D collider1, [NotNull("ArgumentNullException"), Writable] Collider2D collider2);

		[ExcludeFromDocs]
		public static void IgnoreLayerCollision(int layer1, int layer2)
		{
			Physics2D.IgnoreLayerCollision(layer1, layer2, true);
		}

		public static void IgnoreLayerCollision(int layer1, int layer2, bool ignore)
		{
			bool flag = layer1 < 0 || layer1 > 31;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
			}
			bool flag2 = layer2 < 0 || layer2 > 31;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("layer2 is out of range. Layer numbers must be in the range 0 to 31.");
			}
			Physics2D.IgnoreLayerCollision_Internal(layer1, layer2, ignore);
		}

		[NativeMethod("IgnoreLayerCollision"), StaticAccessor("GetPhysics2DSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IgnoreLayerCollision_Internal(int layer1, int layer2, bool ignore);

		public static bool GetIgnoreLayerCollision(int layer1, int layer2)
		{
			bool flag = layer1 < 0 || layer1 > 31;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
			}
			bool flag2 = layer2 < 0 || layer2 > 31;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("layer2 is out of range. Layer numbers must be in the range 0 to 31.");
			}
			return Physics2D.GetIgnoreLayerCollision_Internal(layer1, layer2);
		}

		[NativeMethod("GetIgnoreLayerCollision"), StaticAccessor("GetPhysics2DSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIgnoreLayerCollision_Internal(int layer1, int layer2);

		public static void SetLayerCollisionMask(int layer, int layerMask)
		{
			bool flag = layer < 0 || layer > 31;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
			}
			Physics2D.SetLayerCollisionMask_Internal(layer, layerMask);
		}

		[NativeMethod("SetLayerCollisionMask"), StaticAccessor("GetPhysics2DSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLayerCollisionMask_Internal(int layer, int layerMask);

		public static int GetLayerCollisionMask(int layer)
		{
			bool flag = layer < 0 || layer > 31;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
			}
			return Physics2D.GetLayerCollisionMask_Internal(layer);
		}

		[NativeMethod("GetLayerCollisionMask"), StaticAccessor("GetPhysics2DSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLayerCollisionMask_Internal(int layer);

		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsTouching([NotNull("ArgumentNullException"), Writable] Collider2D collider1, [NotNull("ArgumentNullException"), Writable] Collider2D collider2);

		public static bool IsTouching([Writable] Collider2D collider1, [Writable] Collider2D collider2, ContactFilter2D contactFilter)
		{
			return Physics2D.IsTouching_TwoCollidersWithFilter(collider1, collider2, contactFilter);
		}

		[NativeMethod("IsTouching"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static bool IsTouching_TwoCollidersWithFilter([NotNull("ArgumentNullException"), Writable] Collider2D collider1, [NotNull("ArgumentNullException"), Writable] Collider2D collider2, ContactFilter2D contactFilter)
		{
			return Physics2D.IsTouching_TwoCollidersWithFilter_Injected(collider1, collider2, ref contactFilter);
		}

		public static bool IsTouching([Writable] Collider2D collider, ContactFilter2D contactFilter)
		{
			return Physics2D.IsTouching_SingleColliderWithFilter(collider, contactFilter);
		}

		[NativeMethod("IsTouching"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static bool IsTouching_SingleColliderWithFilter([NotNull("ArgumentNullException"), Writable] Collider2D collider, ContactFilter2D contactFilter)
		{
			return Physics2D.IsTouching_SingleColliderWithFilter_Injected(collider, ref contactFilter);
		}

		[ExcludeFromDocs]
		public static bool IsTouchingLayers([Writable] Collider2D collider)
		{
			return Physics2D.IsTouchingLayers(collider, -1);
		}

		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsTouchingLayers([NotNull("ArgumentNullException"), Writable] Collider2D collider, [UnityEngine.Internal.DefaultValue("Physics2D.AllLayers")] int layerMask);

		public static ColliderDistance2D Distance([Writable] Collider2D colliderA, [Writable] Collider2D colliderB)
		{
			bool flag = colliderA == null;
			if (flag)
			{
				throw new ArgumentNullException("ColliderA cannot be NULL.");
			}
			bool flag2 = colliderB == null;
			if (flag2)
			{
				throw new ArgumentNullException("ColliderB cannot be NULL.");
			}
			bool flag3 = colliderA == colliderB;
			if (flag3)
			{
				throw new ArgumentException("Cannot calculate the distance between the same collider.");
			}
			return Physics2D.Distance_Internal(colliderA, colliderB);
		}

		[NativeMethod("Distance"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static ColliderDistance2D Distance_Internal([NotNull("ArgumentNullException"), Writable] Collider2D colliderA, [NotNull("ArgumentNullException"), Writable] Collider2D colliderB)
		{
			ColliderDistance2D result;
			Physics2D.Distance_Internal_Injected(colliderA, colliderB, out result);
			return result;
		}

		public static Vector2 ClosestPoint(Vector2 position, Collider2D collider)
		{
			bool flag = collider == null;
			if (flag)
			{
				throw new ArgumentNullException("Collider cannot be NULL.");
			}
			return Physics2D.ClosestPoint_Collider(position, collider);
		}

		public static Vector2 ClosestPoint(Vector2 position, Rigidbody2D rigidbody)
		{
			bool flag = rigidbody == null;
			if (flag)
			{
				throw new ArgumentNullException("Rigidbody cannot be NULL.");
			}
			return Physics2D.ClosestPoint_Rigidbody(position, rigidbody);
		}

		[NativeMethod("ClosestPoint"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static Vector2 ClosestPoint_Collider(Vector2 position, [NotNull("ArgumentNullException")] Collider2D collider)
		{
			Vector2 result;
			Physics2D.ClosestPoint_Collider_Injected(ref position, collider, out result);
			return result;
		}

		[NativeMethod("ClosestPoint"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static Vector2 ClosestPoint_Rigidbody(Vector2 position, [NotNull("ArgumentNullException")] Rigidbody2D rigidbody)
		{
			Vector2 result;
			Physics2D.ClosestPoint_Rigidbody_Injected(ref position, rigidbody, out result);
			return result;
		}

		[ExcludeFromDocs]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end)
		{
			return Physics2D.defaultPhysicsScene.Linecast(start, end, -5);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter);
		}

		public static RaycastHit2D Linecast(Vector2 start, Vector2 end, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter);
		}

		public static int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter, results);
		}

		public static int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, List<RaycastHit2D> results)
		{
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.LinecastAll_Internal(Physics2D.defaultPhysicsScene, start, end, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.LinecastAll_Internal(Physics2D.defaultPhysicsScene, start, end, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.LinecastAll_Internal(Physics2D.defaultPhysicsScene, start, end, contactFilter);
		}

		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.LinecastAll_Internal(Physics2D.defaultPhysicsScene, start, end, contactFilter);
		}

		[NativeMethod("LinecastAll_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D[] LinecastAll_Internal(PhysicsScene2D physicsScene, Vector2 start, Vector2 end, ContactFilter2D contactFilter)
		{
			return Physics2D.LinecastAll_Internal_Injected(ref physicsScene, ref start, ref end, ref contactFilter);
		}

		[ExcludeFromDocs]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.Linecast(start, end, results, -5);
		}

		[ExcludeFromDocs]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter, results);
		}

		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity, -5);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, -5);
		}

		[ExcludeFromDocs, RequiredByNativeCode]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter);
		}

		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity, contactFilter, results);
		}

		public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
		}

		public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity, results, -5);
		}

		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, results, -5);
		}

		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
		}

		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.RaycastAll_Internal(Physics2D.defaultPhysicsScene, origin, direction, float.PositiveInfinity, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.RaycastAll_Internal(Physics2D.defaultPhysicsScene, origin, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.RaycastAll_Internal(Physics2D.defaultPhysicsScene, origin, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.RaycastAll_Internal(Physics2D.defaultPhysicsScene, origin, direction, distance, contactFilter);
		}

		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.RaycastAll_Internal(Physics2D.defaultPhysicsScene, origin, direction, distance, contactFilter);
		}

		[NativeMethod("RaycastAll_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D[] RaycastAll_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			return Physics2D.RaycastAll_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, ref contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction)
		{
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, float.PositiveInfinity, -5);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance)
		{
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, -5);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter);
		}

		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, float.PositiveInfinity, contactFilter, results);
		}

		public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance)
		{
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter, results);
		}

		public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.CircleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, radius, direction, float.PositiveInfinity, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.CircleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, radius, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.CircleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, radius, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.CircleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, radius, direction, distance, contactFilter);
		}

		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.CircleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, radius, direction, distance, contactFilter);
		}

		[NativeMethod("CircleCastAll_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D[] CircleCastAll_Internal(PhysicsScene2D physicsScene, Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			return Physics2D.CircleCastAll_Internal_Injected(ref physicsScene, ref origin, radius, ref direction, distance, ref contactFilter);
		}

		[ExcludeFromDocs]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, float.PositiveInfinity, results, -5);
		}

		[ExcludeFromDocs]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, results, -5);
		}

		[ExcludeFromDocs]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter, results);
		}

		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction)
		{
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, float.PositiveInfinity, -5);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
		{
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, -5);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter);
		}

		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("Physics2D.AllLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, float.PositiveInfinity, contactFilter, results);
		}

		public static int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance)
		{
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter, results);
		}

		public static int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.BoxCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, angle, direction, float.PositiveInfinity, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.BoxCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, angle, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.BoxCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, angle, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.BoxCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, angle, direction, distance, contactFilter);
		}

		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.BoxCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, angle, direction, distance, contactFilter);
		}

		[NativeMethod("BoxCastAll_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D[] BoxCastAll_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			return Physics2D.BoxCastAll_Internal_Injected(ref physicsScene, ref origin, ref size, angle, ref direction, distance, ref contactFilter);
		}

		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, float.PositiveInfinity, results, -5);
		}

		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, results, -5);
		}

		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter, results);
		}

		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction)
		{
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, float.PositiveInfinity, -5);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance)
		{
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, -5);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter);
		}

		public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, float.PositiveInfinity, contactFilter, results);
		}

		public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance)
		{
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
		}

		public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.CapsuleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, capsuleDirection, angle, direction, float.PositiveInfinity, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.CapsuleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, capsuleDirection, angle, direction, distance, contactFilter);
		}

		[NativeMethod("CapsuleCastAll_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D[] CapsuleCastAll_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			return Physics2D.CapsuleCastAll_Internal_Injected(ref physicsScene, ref origin, ref size, capsuleDirection, angle, ref direction, distance, ref contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.CapsuleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, capsuleDirection, angle, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.CapsuleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, capsuleDirection, angle, direction, distance, contactFilter);
		}

		public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.CapsuleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, capsuleDirection, angle, direction, distance, contactFilter);
		}

		[ExcludeFromDocs]
		public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, float.PositiveInfinity, results, -5);
		}

		[ExcludeFromDocs]
		public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, results, -5);
		}

		[ExcludeFromDocs]
		public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
		}

		public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D GetRayIntersection(Ray ray)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, float.PositiveInfinity, -5);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D GetRayIntersection(Ray ray, float distance)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, distance, -5);
		}

		public static RaycastHit2D GetRayIntersection(Ray ray, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, distance, layerMask);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray)
		{
			return Physics2D.GetRayIntersectionAll_Internal(Physics2D.defaultPhysicsScene, ray.origin, ray.direction, float.PositiveInfinity, -5);
		}

		[ExcludeFromDocs]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, float distance)
		{
			return Physics2D.GetRayIntersectionAll_Internal(Physics2D.defaultPhysicsScene, ray.origin, ray.direction, distance, -5);
		}

		[RequiredByNativeCode]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics2D.GetRayIntersectionAll_Internal(Physics2D.defaultPhysicsScene, ray.origin, ray.direction, distance, layerMask);
		}

		[NativeMethod("GetRayIntersectionAll_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D[] GetRayIntersectionAll_Internal(PhysicsScene2D physicsScene, Vector3 origin, Vector3 direction, float distance, int layerMask)
		{
			return Physics2D.GetRayIntersectionAll_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, layerMask);
		}

		[ExcludeFromDocs]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, float.PositiveInfinity, results, -5);
		}

		[ExcludeFromDocs]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, float distance)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, distance, results, -5);
		}

		[RequiredByNativeCode]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, distance, results, layerMask);
		}

		[ExcludeFromDocs]
		public static Collider2D OverlapPoint(Vector2 point)
		{
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, -5);
		}

		[ExcludeFromDocs]
		public static Collider2D OverlapPoint(Vector2 point, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter);
		}

		[ExcludeFromDocs]
		public static Collider2D OverlapPoint(Vector2 point, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter);
		}

		public static Collider2D OverlapPoint(Vector2 point, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter);
		}

		public static int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter, results);
		}

		public static int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static Collider2D[] OverlapPointAll(Vector2 point)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapPointAll_Internal(Physics2D.defaultPhysicsScene, point, contactFilter);
		}

		[ExcludeFromDocs]
		public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapPointAll_Internal(Physics2D.defaultPhysicsScene, point, contactFilter);
		}

		[ExcludeFromDocs]
		public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.OverlapPointAll_Internal(Physics2D.defaultPhysicsScene, point, contactFilter);
		}

		public static Collider2D[] OverlapPointAll(Vector2 point, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.OverlapPointAll_Internal(Physics2D.defaultPhysicsScene, point, contactFilter);
		}

		[NativeMethod("OverlapPointAll_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static Collider2D[] OverlapPointAll_Internal(PhysicsScene2D physicsScene, Vector2 point, ContactFilter2D contactFilter)
		{
			return Physics2D.OverlapPointAll_Internal_Injected(ref physicsScene, ref point, ref contactFilter);
		}

		[ExcludeFromDocs]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, results, -5);
		}

		[ExcludeFromDocs]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter, results);
		}

		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static Collider2D OverlapCircle(Vector2 point, float radius)
		{
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, -5);
		}

		[ExcludeFromDocs]
		public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter);
		}

		[ExcludeFromDocs]
		public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter);
		}

		public static Collider2D OverlapCircle(Vector2 point, float radius, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter);
		}

		public static int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter, results);
		}

		public static int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapCircleAll_Internal(Physics2D.defaultPhysicsScene, point, radius, contactFilter);
		}

		[ExcludeFromDocs]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapCircleAll_Internal(Physics2D.defaultPhysicsScene, point, radius, contactFilter);
		}

		[ExcludeFromDocs]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.OverlapCircleAll_Internal(Physics2D.defaultPhysicsScene, point, radius, contactFilter);
		}

		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.OverlapCircleAll_Internal(Physics2D.defaultPhysicsScene, point, radius, contactFilter);
		}

		[NativeMethod("OverlapCircleAll_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static Collider2D[] OverlapCircleAll_Internal(PhysicsScene2D physicsScene, Vector2 point, float radius, ContactFilter2D contactFilter)
		{
			return Physics2D.OverlapCircleAll_Internal_Injected(ref physicsScene, ref point, radius, ref contactFilter);
		}

		[ExcludeFromDocs]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, results, -5);
		}

		[ExcludeFromDocs]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter, results);
		}

		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle)
		{
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, -5);
		}

		[ExcludeFromDocs]
		public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter);
		}

		[ExcludeFromDocs]
		public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter);
		}

		public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter);
		}

		public static int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter, results);
		}

		public static int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapBoxAll_Internal(Physics2D.defaultPhysicsScene, point, size, angle, contactFilter);
		}

		[ExcludeFromDocs]
		public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapBoxAll_Internal(Physics2D.defaultPhysicsScene, point, size, angle, contactFilter);
		}

		[ExcludeFromDocs]
		public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.OverlapBoxAll_Internal(Physics2D.defaultPhysicsScene, point, size, angle, contactFilter);
		}

		public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.OverlapBoxAll_Internal(Physics2D.defaultPhysicsScene, point, size, angle, contactFilter);
		}

		[NativeMethod("OverlapBoxAll_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static Collider2D[] OverlapBoxAll_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter)
		{
			return Physics2D.OverlapBoxAll_Internal_Injected(ref physicsScene, ref point, ref size, angle, ref contactFilter);
		}

		[ExcludeFromDocs]
		public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, results, -5);
		}

		[ExcludeFromDocs]
		public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter, results);
		}

		public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB)
		{
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, -5);
		}

		[ExcludeFromDocs]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter);
		}

		[ExcludeFromDocs]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter);
		}

		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter);
		}

		public static int OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter, results);
		}

		public static int OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB)
		{
			return Physics2D.OverlapAreaAllToBox_Internal(pointA, pointB, -5, float.NegativeInfinity, float.PositiveInfinity);
		}

		[ExcludeFromDocs]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask)
		{
			return Physics2D.OverlapAreaAllToBox_Internal(pointA, pointB, layerMask, float.NegativeInfinity, float.PositiveInfinity);
		}

		[ExcludeFromDocs]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth)
		{
			return Physics2D.OverlapAreaAllToBox_Internal(pointA, pointB, layerMask, minDepth, float.PositiveInfinity);
		}

		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.OverlapAreaAllToBox_Internal(pointA, pointB, layerMask, minDepth, maxDepth);
		}

		private static Collider2D[] OverlapAreaAllToBox_Internal(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth, float maxDepth)
		{
			Vector2 point = (pointA + pointB) * 0.5f;
			Vector2 size = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
			return Physics2D.OverlapBoxAll(point, size, 0f, layerMask, minDepth, maxDepth);
		}

		[ExcludeFromDocs]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, results, -5);
		}

		[ExcludeFromDocs]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter, results);
		}

		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle)
		{
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, -5);
		}

		[ExcludeFromDocs]
		public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter);
		}

		[ExcludeFromDocs]
		public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter);
		}

		public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter);
		}

		public static int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter, results);
		}

		public static int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapCapsuleAll_Internal(Physics2D.defaultPhysicsScene, point, size, direction, angle, contactFilter);
		}

		[ExcludeFromDocs]
		public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapCapsuleAll_Internal(Physics2D.defaultPhysicsScene, point, size, direction, angle, contactFilter);
		}

		[ExcludeFromDocs]
		public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.OverlapCapsuleAll_Internal(Physics2D.defaultPhysicsScene, point, size, direction, angle, contactFilter);
		}

		public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.OverlapCapsuleAll_Internal(Physics2D.defaultPhysicsScene, point, size, direction, angle, contactFilter);
		}

		[NativeMethod("OverlapCapsuleAll_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static Collider2D[] OverlapCapsuleAll_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter)
		{
			return Physics2D.OverlapCapsuleAll_Internal_Injected(ref physicsScene, ref point, ref size, direction, angle, ref contactFilter);
		}

		[ExcludeFromDocs]
		public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, results, -5);
		}

		[ExcludeFromDocs]
		public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter, results);
		}

		[ExcludeFromDocs]
		public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter, results);
		}

		public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter, results);
		}

		public static int OverlapCollider(Collider2D collider, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapCollider(collider, contactFilter, results);
		}

		public static int OverlapCollider(Collider2D collider, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapCollider(collider, contactFilter, results);
		}

		public static int GetContacts(Collider2D collider1, Collider2D collider2, ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			return Physics2D.GetColliderColliderContactsArray(collider1, collider2, contactFilter, contacts);
		}

		public static int GetContacts(Collider2D collider, ContactPoint2D[] contacts)
		{
			return Physics2D.GetColliderContactsArray(collider, default(ContactFilter2D).NoFilter(), contacts);
		}

		public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			return Physics2D.GetColliderContactsArray(collider, contactFilter, contacts);
		}

		public static int GetContacts(Collider2D collider, Collider2D[] colliders)
		{
			return Physics2D.GetColliderContactsCollidersOnlyArray(collider, default(ContactFilter2D).NoFilter(), colliders);
		}

		public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, Collider2D[] colliders)
		{
			return Physics2D.GetColliderContactsCollidersOnlyArray(collider, contactFilter, colliders);
		}

		public static int GetContacts(Rigidbody2D rigidbody, ContactPoint2D[] contacts)
		{
			return Physics2D.GetRigidbodyContactsArray(rigidbody, default(ContactFilter2D).NoFilter(), contacts);
		}

		public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			return Physics2D.GetRigidbodyContactsArray(rigidbody, contactFilter, contacts);
		}

		public static int GetContacts(Rigidbody2D rigidbody, Collider2D[] colliders)
		{
			return Physics2D.GetRigidbodyContactsCollidersOnlyArray(rigidbody, default(ContactFilter2D).NoFilter(), colliders);
		}

		public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, Collider2D[] colliders)
		{
			return Physics2D.GetRigidbodyContactsCollidersOnlyArray(rigidbody, contactFilter, colliders);
		}

		[NativeMethod("GetColliderContactsArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int GetColliderContactsArray([NotNull("ArgumentNullException")] Collider2D collider, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] ContactPoint2D[] results)
		{
			return Physics2D.GetColliderContactsArray_Injected(collider, ref contactFilter, results);
		}

		[NativeMethod("GetColliderColliderContactsArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int GetColliderColliderContactsArray([NotNull("ArgumentNullException")] Collider2D collider1, [NotNull("ArgumentNullException")] Collider2D collider2, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] ContactPoint2D[] results)
		{
			return Physics2D.GetColliderColliderContactsArray_Injected(collider1, collider2, ref contactFilter, results);
		}

		[NativeMethod("GetRigidbodyContactsArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int GetRigidbodyContactsArray([NotNull("ArgumentNullException")] Rigidbody2D rigidbody, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] ContactPoint2D[] results)
		{
			return Physics2D.GetRigidbodyContactsArray_Injected(rigidbody, ref contactFilter, results);
		}

		[NativeMethod("GetColliderContactsCollidersOnlyArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int GetColliderContactsCollidersOnlyArray([NotNull("ArgumentNullException")] Collider2D collider, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] Collider2D[] results)
		{
			return Physics2D.GetColliderContactsCollidersOnlyArray_Injected(collider, ref contactFilter, results);
		}

		[NativeMethod("GetRigidbodyContactsCollidersOnlyArray_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int GetRigidbodyContactsCollidersOnlyArray([NotNull("ArgumentNullException")] Rigidbody2D rigidbody, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] Collider2D[] results)
		{
			return Physics2D.GetRigidbodyContactsCollidersOnlyArray_Injected(rigidbody, ref contactFilter, results);
		}

		public static int GetContacts(Collider2D collider1, Collider2D collider2, ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetColliderColliderContactsList(collider1, collider2, contactFilter, contacts);
		}

		public static int GetContacts(Collider2D collider, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetColliderContactsList(collider, default(ContactFilter2D).NoFilter(), contacts);
		}

		public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetColliderContactsList(collider, contactFilter, contacts);
		}

		public static int GetContacts(Collider2D collider, List<Collider2D> colliders)
		{
			return Physics2D.GetColliderContactsCollidersOnlyList(collider, default(ContactFilter2D).NoFilter(), colliders);
		}

		public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, List<Collider2D> colliders)
		{
			return Physics2D.GetColliderContactsCollidersOnlyList(collider, contactFilter, colliders);
		}

		public static int GetContacts(Rigidbody2D rigidbody, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetRigidbodyContactsList(rigidbody, default(ContactFilter2D).NoFilter(), contacts);
		}

		public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetRigidbodyContactsList(rigidbody, contactFilter, contacts);
		}

		public static int GetContacts(Rigidbody2D rigidbody, List<Collider2D> colliders)
		{
			return Physics2D.GetRigidbodyContactsCollidersOnlyList(rigidbody, default(ContactFilter2D).NoFilter(), colliders);
		}

		public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, List<Collider2D> colliders)
		{
			return Physics2D.GetRigidbodyContactsCollidersOnlyList(rigidbody, contactFilter, colliders);
		}

		[NativeMethod("GetColliderContactsList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int GetColliderContactsList([NotNull("ArgumentNullException")] Collider2D collider, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<ContactPoint2D> results)
		{
			return Physics2D.GetColliderContactsList_Injected(collider, ref contactFilter, results);
		}

		[NativeMethod("GetColliderColliderContactsList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int GetColliderColliderContactsList([NotNull("ArgumentNullException")] Collider2D collider1, [NotNull("ArgumentNullException")] Collider2D collider2, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<ContactPoint2D> results)
		{
			return Physics2D.GetColliderColliderContactsList_Injected(collider1, collider2, ref contactFilter, results);
		}

		[NativeMethod("GetRigidbodyContactsList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int GetRigidbodyContactsList([NotNull("ArgumentNullException")] Rigidbody2D rigidbody, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<ContactPoint2D> results)
		{
			return Physics2D.GetRigidbodyContactsList_Injected(rigidbody, ref contactFilter, results);
		}

		[NativeMethod("GetColliderContactsCollidersOnlyList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int GetColliderContactsCollidersOnlyList([NotNull("ArgumentNullException")] Collider2D collider, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
		{
			return Physics2D.GetColliderContactsCollidersOnlyList_Injected(collider, ref contactFilter, results);
		}

		[NativeMethod("GetRigidbodyContactsCollidersOnlyList_Binding"), StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int GetRigidbodyContactsCollidersOnlyList([NotNull("ArgumentNullException")] Rigidbody2D rigidbody, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
		{
			return Physics2D.GetRigidbodyContactsCollidersOnlyList_Injected(rigidbody, ref contactFilter, results);
		}

		internal static void SetEditorDragMovement(bool dragging, GameObject[] objs)
		{
			foreach (Rigidbody2D current in Physics2D.m_LastDisabledRigidbody2D)
			{
				bool flag = current != null;
				if (flag)
				{
					current.SetDragBehaviour(false);
				}
			}
			Physics2D.m_LastDisabledRigidbody2D.Clear();
			bool flag2 = !dragging;
			if (!flag2)
			{
				for (int i = 0; i < objs.Length; i++)
				{
					GameObject gameObject = objs[i];
					Rigidbody2D[] componentsInChildren = gameObject.GetComponentsInChildren<Rigidbody2D>(false);
					Rigidbody2D[] array = componentsInChildren;
					for (int j = 0; j < array.Length; j++)
					{
						Rigidbody2D rigidbody2D = array[j];
						Physics2D.m_LastDisabledRigidbody2D.Add(rigidbody2D);
						rigidbody2D.SetDragBehaviour(true);
					}
				}
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_gravity_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_gravity_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_jobOptions_Injected(out PhysicsJobOptions2D ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_jobOptions_Injected(ref PhysicsJobOptions2D value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_colliderAwakeColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_colliderAwakeColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_colliderAsleepColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_colliderAsleepColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_colliderContactColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_colliderContactColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_colliderAABBColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_colliderAABBColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Simulate_Internal_Injected(ref PhysicsScene2D physicsScene, float step);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTouching_TwoCollidersWithFilter_Injected([Writable] Collider2D collider1, [Writable] Collider2D collider2, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTouching_SingleColliderWithFilter_Injected([Writable] Collider2D collider, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Distance_Internal_Injected([Writable] Collider2D colliderA, [Writable] Collider2D colliderB, out ColliderDistance2D ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClosestPoint_Collider_Injected(ref Vector2 position, Collider2D collider, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClosestPoint_Rigidbody_Injected(ref Vector2 position, Rigidbody2D rigidbody, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] LinecastAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 start, ref Vector2 end, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] RaycastAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] CircleCastAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, float radius, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] BoxCastAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] CapsuleCastAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, CapsuleDirection2D capsuleDirection, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] GetRayIntersectionAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector3 origin, ref Vector3 direction, float distance, int layerMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D[] OverlapPointAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D[] OverlapCircleAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, float radius, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D[] OverlapBoxAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, float angle, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D[] OverlapCapsuleAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, CapsuleDirection2D direction, float angle, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetColliderContactsArray_Injected(Collider2D collider, ref ContactFilter2D contactFilter, ContactPoint2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetColliderColliderContactsArray_Injected(Collider2D collider1, Collider2D collider2, ref ContactFilter2D contactFilter, ContactPoint2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRigidbodyContactsArray_Injected(Rigidbody2D rigidbody, ref ContactFilter2D contactFilter, ContactPoint2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetColliderContactsCollidersOnlyArray_Injected(Collider2D collider, ref ContactFilter2D contactFilter, Collider2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRigidbodyContactsCollidersOnlyArray_Injected(Rigidbody2D rigidbody, ref ContactFilter2D contactFilter, Collider2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetColliderContactsList_Injected(Collider2D collider, ref ContactFilter2D contactFilter, List<ContactPoint2D> results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetColliderColliderContactsList_Injected(Collider2D collider1, Collider2D collider2, ref ContactFilter2D contactFilter, List<ContactPoint2D> results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRigidbodyContactsList_Injected(Rigidbody2D rigidbody, ref ContactFilter2D contactFilter, List<ContactPoint2D> results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetColliderContactsCollidersOnlyList_Injected(Collider2D collider, ref ContactFilter2D contactFilter, List<Collider2D> results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRigidbodyContactsCollidersOnlyList_Injected(Rigidbody2D rigidbody, ref ContactFilter2D contactFilter, List<Collider2D> results);
	}
}
