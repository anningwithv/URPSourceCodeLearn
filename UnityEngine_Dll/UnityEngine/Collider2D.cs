using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics2D/Public/Collider2D.h"), RequireComponent(typeof(Transform)), RequiredByNativeCode(Optional = true)]
	public class Collider2D : Behaviour
	{
		public extern float density
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isTrigger
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool usedByEffector
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool usedByComposite
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern CompositeCollider2D composite
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Vector2 offset
		{
			get
			{
				Vector2 result;
				this.get_offset_Injected(out result);
				return result;
			}
			set
			{
				this.set_offset_Injected(ref value);
			}
		}

		public extern Rigidbody2D attachedRigidbody
		{
			[NativeMethod("GetAttachedRigidbody_Binding")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int shapeCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.get_bounds_Injected(out result);
				return result;
			}
		}

		internal extern ColliderErrorState2D errorState
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal extern bool compositeCapable
		{
			[NativeMethod("GetCompositeCapable_Binding")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern PhysicsMaterial2D sharedMaterial
		{
			[NativeMethod("GetMaterial")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetMaterial")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float friction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float bounciness
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeMethod("CreateMesh_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Mesh CreateMesh(bool useBodyPosition, bool useBodyRotation);

		[NativeMethod("GetShapeHash_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetShapeHash();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsTouching([NotNull("ArgumentNullException"), Writable] Collider2D collider);

		public bool IsTouching([Writable] Collider2D collider, ContactFilter2D contactFilter)
		{
			return this.IsTouching_OtherColliderWithFilter(collider, contactFilter);
		}

		[NativeMethod("IsTouching")]
		private bool IsTouching_OtherColliderWithFilter([NotNull("ArgumentNullException"), Writable] Collider2D collider, ContactFilter2D contactFilter)
		{
			return this.IsTouching_OtherColliderWithFilter_Injected(collider, ref contactFilter);
		}

		public bool IsTouching(ContactFilter2D contactFilter)
		{
			return this.IsTouching_AnyColliderWithFilter(contactFilter);
		}

		[NativeMethod("IsTouching")]
		private bool IsTouching_AnyColliderWithFilter(ContactFilter2D contactFilter)
		{
			return this.IsTouching_AnyColliderWithFilter_Injected(ref contactFilter);
		}

		[ExcludeFromDocs]
		public bool IsTouchingLayers()
		{
			return this.IsTouchingLayers(-1);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsTouchingLayers([DefaultValue("Physics2D.AllLayers")] int layerMask);

		public bool OverlapPoint(Vector2 point)
		{
			return this.OverlapPoint_Injected(ref point);
		}

		public ColliderDistance2D Distance([Writable] Collider2D collider)
		{
			return Physics2D.Distance(this, collider);
		}

		public int OverlapCollider(ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapCollider(this, contactFilter, results);
		}

		public int OverlapCollider(ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapCollider(this, contactFilter, results);
		}

		public int GetContacts(ContactPoint2D[] contacts)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
		}

		public int GetContacts(List<ContactPoint2D> contacts)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
		}

		public int GetContacts(ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			return Physics2D.GetContacts(this, contactFilter, contacts);
		}

		public int GetContacts(ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetContacts(this, contactFilter, contacts);
		}

		public int GetContacts(Collider2D[] colliders)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
		}

		public int GetContacts(List<Collider2D> colliders)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
		}

		public int GetContacts(ContactFilter2D contactFilter, Collider2D[] colliders)
		{
			return Physics2D.GetContacts(this, contactFilter, colliders);
		}

		public int GetContacts(ContactFilter2D contactFilter, List<Collider2D> colliders)
		{
			return Physics2D.GetContacts(this, contactFilter, colliders);
		}

		[ExcludeFromDocs]
		public int Cast(Vector2 direction, RaycastHit2D[] results)
		{
			ContactFilter2D contactFilter = default(ContactFilter2D);
			contactFilter.useTriggers = Physics2D.queriesHitTriggers;
			contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(base.gameObject.layer));
			return this.CastArray_Internal(direction, float.PositiveInfinity, contactFilter, true, results);
		}

		[ExcludeFromDocs]
		public int Cast(Vector2 direction, RaycastHit2D[] results, float distance)
		{
			ContactFilter2D contactFilter = default(ContactFilter2D);
			contactFilter.useTriggers = Physics2D.queriesHitTriggers;
			contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(base.gameObject.layer));
			return this.CastArray_Internal(direction, distance, contactFilter, true, results);
		}

		public int Cast(Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("true")] bool ignoreSiblingColliders)
		{
			ContactFilter2D contactFilter = default(ContactFilter2D);
			contactFilter.useTriggers = Physics2D.queriesHitTriggers;
			contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(base.gameObject.layer));
			return this.CastArray_Internal(direction, distance, contactFilter, ignoreSiblingColliders, results);
		}

		[ExcludeFromDocs]
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return this.CastArray_Internal(direction, float.PositiveInfinity, contactFilter, true, results);
		}

		[ExcludeFromDocs]
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance)
		{
			return this.CastArray_Internal(direction, distance, contactFilter, true, results);
		}

		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("true")] bool ignoreSiblingColliders)
		{
			return this.CastArray_Internal(direction, distance, contactFilter, ignoreSiblingColliders, results);
		}

		[NativeMethod("CastArray_Binding")]
		private int CastArray_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, bool ignoreSiblingColliders, [NotNull("ArgumentNullException")] RaycastHit2D[] results)
		{
			return this.CastArray_Internal_Injected(ref direction, distance, ref contactFilter, ignoreSiblingColliders, results);
		}

		public int Cast(Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity, [DefaultValue("true")] bool ignoreSiblingColliders = true)
		{
			return this.CastList_Internal(direction, distance, contactFilter, ignoreSiblingColliders, results);
		}

		[NativeMethod("CastList_Binding")]
		private int CastList_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, bool ignoreSiblingColliders, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
		{
			return this.CastList_Internal_Injected(ref direction, distance, ref contactFilter, ignoreSiblingColliders, results);
		}

		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-1, float.NegativeInfinity, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, float.PositiveInfinity, contactFilter, results);
		}

		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-1, float.NegativeInfinity, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, distance, contactFilter, results);
		}

		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, distance, contactFilter, results);
		}

		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, distance, contactFilter, results);
		}

		public int Raycast(Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("Physics2D.AllLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return this.RaycastArray_Internal(direction, distance, contactFilter, results);
		}

		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return this.RaycastArray_Internal(direction, float.PositiveInfinity, contactFilter, results);
		}

		public int Raycast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return this.RaycastArray_Internal(direction, distance, contactFilter, results);
		}

		[NativeMethod("RaycastArray_Binding")]
		private int RaycastArray_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] RaycastHit2D[] results)
		{
			return this.RaycastArray_Internal_Injected(ref direction, distance, ref contactFilter, results);
		}

		public int Raycast(Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return this.RaycastList_Internal(direction, distance, contactFilter, results);
		}

		[NativeMethod("RaycastList_Binding")]
		private int RaycastList_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
		{
			return this.RaycastList_Internal_Injected(ref direction, distance, ref contactFilter, results);
		}

		public Vector2 ClosestPoint(Vector2 position)
		{
			return Physics2D.ClosestPoint(position, this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_offset_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_offset_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsTouching_OtherColliderWithFilter_Injected([Writable] Collider2D collider, ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsTouching_AnyColliderWithFilter_Injected(ref ContactFilter2D contactFilter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool OverlapPoint_Injected(ref Vector2 point);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int CastArray_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, bool ignoreSiblingColliders, RaycastHit2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int CastList_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, bool ignoreSiblingColliders, List<RaycastHit2D> results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int RaycastArray_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int RaycastList_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);
	}
}
