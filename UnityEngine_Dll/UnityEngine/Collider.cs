using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/Collider.h"), RequireComponent(typeof(Transform)), RequiredByNativeCode]
	public class Collider : Component
	{
		public extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Rigidbody attachedRigidbody
		{
			[NativeMethod("GetRigidbody")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ArticulationBody attachedArticulationBody
		{
			[NativeMethod("GetArticulationBody")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isTrigger
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float contactOffset
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
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

		[NativeMethod("Material")]
		public extern PhysicMaterial sharedMaterial
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern PhysicMaterial material
		{
			[NativeMethod("GetClonedMaterial")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetMaterial")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 ClosestPoint(Vector3 position)
		{
			Vector3 result;
			this.ClosestPoint_Injected(ref position, out result);
			return result;
		}

		private RaycastHit Raycast(Ray ray, float maxDistance, ref bool hasHit)
		{
			RaycastHit result;
			this.Raycast_Injected(ref ray, maxDistance, ref hasHit, out result);
			return result;
		}

		public bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
		{
			bool result = false;
			hitInfo = this.Raycast(ray, maxDistance, ref result);
			return result;
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

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ClosestPoint_Injected(ref Vector3 position, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Raycast_Injected(ref Ray ray, float maxDistance, ref bool hasHit, out RaycastHit ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_ClosestPointOnBounds_Injected(ref Vector3 point, ref Vector3 outPos, ref float distance);
	}
}
