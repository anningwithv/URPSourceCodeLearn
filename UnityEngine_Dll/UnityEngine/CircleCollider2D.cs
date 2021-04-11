using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics2D/Public/CircleCollider2D.h")]
	public sealed class CircleCollider2D : Collider2D
	{
		public extern float radius
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("CircleCollider2D.center has been deprecated. Use CircleCollider2D.offset instead (UnityUpgradable) -> offset", true)]
		public Vector2 center
		{
			get
			{
				return Vector2.zero;
			}
			set
			{
			}
		}
	}
}
