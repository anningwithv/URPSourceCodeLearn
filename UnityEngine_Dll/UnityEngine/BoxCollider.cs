using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/BoxCollider.h"), RequiredByNativeCode]
	public class BoxCollider : Collider
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

		public Vector3 size
		{
			get
			{
				Vector3 result;
				this.get_size_Injected(out result);
				return result;
			}
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		[Obsolete("Use BoxCollider.size instead.")]
		public Vector3 extents
		{
			get
			{
				return this.size * 0.5f;
			}
			set
			{
				this.size = value * 2f;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_center_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_center_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_size_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_size_Injected(ref Vector3 value);
	}
}
