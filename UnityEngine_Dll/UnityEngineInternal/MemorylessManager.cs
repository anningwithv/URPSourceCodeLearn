using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngineInternal
{
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	public class MemorylessManager
	{
		public static MemorylessMode depthMemorylessMode
		{
			get
			{
				return MemorylessManager.GetFramebufferDepthMemorylessMode();
			}
			set
			{
				MemorylessManager.SetFramebufferDepthMemorylessMode(value);
			}
		}

		[NativeMethod(Name = "GetFramebufferDepthMemorylessMode"), StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MemorylessMode GetFramebufferDepthMemorylessMode();

		[NativeMethod(Name = "SetFramebufferDepthMemorylessMode"), StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetFramebufferDepthMemorylessMode(MemorylessMode mode);
	}
}
