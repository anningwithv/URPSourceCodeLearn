using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/CapsuleCollider.h"), RequiredByNativeCode]
	public class CapsuleCollider : Collider
	{
		public Vector3 center
		{
			get
			{
				Vector3 result;
				this.get_center_Injected(out result);
				return result;
			}
			set
			{
				this.set_center_Injected(ref value);
			}
		}

		public extern float radius
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float height
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int direction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal Vector2 GetGlobalExtents()
		{
			Vector2 result;
			this.GetGlobalExtents_Injected(out result);
			return result;
		}

		internal Matrix4x4 CalculateTransform()
		{
			Matrix4x4 result;
			this.CalculateTransform_Injected(out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_center_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_center_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetGlobalExtents_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CalculateTransform_Injected(out Matrix4x4 ret);
	}
}
