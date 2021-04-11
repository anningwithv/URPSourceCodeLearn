using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngineInternal
{
	[NativeHeader("Runtime/Export/Graphics/GraphicsDeviceDebug.bindings.h"), StaticAccessor("GraphicsDeviceDebug", StaticAccessorType.DoubleColon)]
	internal static class GraphicsDeviceDebug
	{
		internal static GraphicsDeviceDebugSettings settings
		{
			get
			{
				GraphicsDeviceDebugSettings result;
				GraphicsDeviceDebug.get_settings_Injected(out result);
				return result;
			}
			set
			{
				GraphicsDeviceDebug.set_settings_Injected(ref value);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_settings_Injected(out GraphicsDeviceDebugSettings ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_settings_Injected(ref GraphicsDeviceDebugSettings value);
	}
}
