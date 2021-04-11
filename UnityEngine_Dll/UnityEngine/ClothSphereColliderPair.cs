using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Cloth/Cloth.h"), UsedByNativeCode]
	public struct ClothSphereColliderPair
	{
		public SphereCollider first
		{
			[IsReadOnly]
			get;
			set;
		}

		public SphereCollider second
		{
			[IsReadOnly]
			get;
			set;
		}

		public ClothSphereColliderPair(SphereCollider a)
		{
			this.first = a;
			this.second = null;
		}

		public ClothSphereColliderPair(SphereCollider a, SphereCollider b)
		{
			this.first = a;
			this.second = b;
		}
	}
}
