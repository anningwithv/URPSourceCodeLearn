using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.VirtualTexturing
{
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h"), StaticAccessor("VirtualTexturing::Streaming", StaticAccessorType.DoubleColon)]
	public static class Streaming
	{
		[NativeThrows]
		public static void RequestRegion([NotNull("ArgumentNullException")] Material mat, int stackNameId, Rect r, int mipMap, int numMips)
		{
			Streaming.RequestRegion_Injected(mat, stackNameId, ref r, mipMap, numMips);
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetTextureStackSize([NotNull("ArgumentNullException")] Material mat, int stackNameId, out int width, out int height);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCPUCacheSize(int sizeInMegabytes);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGPUCacheSettings(GPUCacheSetting[] cacheSettings);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RequestRegion_Injected(Material mat, int stackNameId, ref Rect r, int mipMap, int numMips);
	}
}
