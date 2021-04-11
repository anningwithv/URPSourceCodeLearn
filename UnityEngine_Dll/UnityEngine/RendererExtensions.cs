using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public static class RendererExtensions
	{
		public static void UpdateGIMaterials(this Renderer renderer)
		{
			RendererExtensions.UpdateGIMaterialsForRenderer(renderer);
		}

		[FreeFunction("RendererScripting::UpdateGIMaterialsForRenderer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void UpdateGIMaterialsForRenderer(Renderer renderer);
	}
}
