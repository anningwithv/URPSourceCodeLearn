using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.VirtualTexturing
{
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h"), StaticAccessor("VirtualTexturing::System", StaticAccessorType.DoubleColon)]
	public static class System
	{
		public const int AllMips = 2147483647;

		internal static extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Update();
	}
}
