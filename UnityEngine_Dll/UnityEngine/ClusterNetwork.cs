using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/ClusterRenderer/ClusterNetwork.h")]
	public class ClusterNetwork
	{
		public static extern bool isMasterOfCluster
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool isDisconnected
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int nodeIndex
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
