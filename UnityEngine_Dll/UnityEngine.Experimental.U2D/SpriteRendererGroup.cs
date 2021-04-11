using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.U2D
{
	[NativeHeader("Runtime/2D/Renderer/SpriteRendererGroup.h"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	internal class SpriteRendererGroup
	{
		public static void AddRenderers(NativeArray<SpriteIntermediateRendererInfo> renderers)
		{
			SpriteRendererGroup.AddRenderers(renderers.GetUnsafeReadOnlyPtr<SpriteIntermediateRendererInfo>(), renderers.Length);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void AddRenderers(void* renderers, int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Clear();
	}
}
