using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	[NativeType(Header = "Runtime/2D/Sorting/SortingGroup.h"), RequireComponent(typeof(Transform))]
	public sealed class SortingGroup : Behaviour
	{
		[StaticAccessor("SortingGroup", StaticAccessorType.DoubleColon)]
		internal static extern int invalidSortingGroupID
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern string sortingLayerName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int sortingLayerID
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int sortingOrder
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal extern int sortingGroupID
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal extern int sortingGroupOrder
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal extern int index
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("SortingGroup", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UpdateAllSortingGroups();
	}
}
