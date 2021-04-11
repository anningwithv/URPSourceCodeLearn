using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public static class ShaderWarmup
	{
		[FreeFunction(Name = "ShaderWarmupScripting::WarmupShader")]
		public static void WarmupShader(Shader shader, ShaderWarmupSetup setup)
		{
			ShaderWarmup.WarmupShader_Injected(shader, ref setup);
		}

		[FreeFunction(Name = "ShaderWarmupScripting::WarmupShaderFromCollection")]
		public static void WarmupShaderFromCollection(ShaderVariantCollection collection, Shader shader, ShaderWarmupSetup setup)
		{
			ShaderWarmup.WarmupShaderFromCollection_Injected(collection, shader, ref setup);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WarmupShader_Injected(Shader shader, ref ShaderWarmupSetup setup);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WarmupShaderFromCollection_Injected(ShaderVariantCollection collection, Shader shader, ref ShaderWarmupSetup setup);
	}
}
