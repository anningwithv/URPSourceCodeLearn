using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngineInternal
{
	[NativeHeader("Runtime/Export/GI/GIDebugVisualisation.bindings.h")]
	public static class GIDebugVisualisation
	{
		public static extern bool cycleMode
		{
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool pauseCycleMode
		{
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern GITextureType texType
		{
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetRuntimeInputTextures();

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PlayCycleMode();

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PauseCycleMode();

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StopCycleMode();

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CycleSkipSystems(int skip);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CycleSkipInstances(int skip);
	}
}
