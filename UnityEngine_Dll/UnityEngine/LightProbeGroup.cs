using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/LightProbeGroup.h")]
	public sealed class LightProbeGroup : Behaviour
	{
		[NativeName("Positions")]
		public extern Vector3[] probePositions
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("Dering")]
		public extern bool dering
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
