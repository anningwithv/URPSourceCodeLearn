using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Animation/OptimizeTransformHierarchy.h")]
	public class AnimatorUtility
	{
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void OptimizeTransformHierarchy([NotNull("NullExceptionObject")] GameObject go, string[] exposedTransforms);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeoptimizeTransformHierarchy([NotNull("NullExceptionObject")] GameObject go);
	}
}
