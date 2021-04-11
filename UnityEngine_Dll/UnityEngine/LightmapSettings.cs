using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/LightmapSettings.h"), StaticAccessor("GetLightmapSettings()")]
	public sealed class LightmapSettings : Object
	{
		public static extern LightmapData[] lightmaps
		{
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern LightmapsMode lightmapsMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern LightProbes lightProbes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction, NativeName("SetLightProbes")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Use lightmapsMode instead.", false)]
		public static LightmapsModeLegacy lightmapsModeLegacy
		{
			get
			{
				return LightmapsModeLegacy.Single;
			}
			set
			{
			}
		}

		[Obsolete("Use QualitySettings.desiredColorSpace instead.", false)]
		public static ColorSpace bakedColorSpace
		{
			get
			{
				return QualitySettings.desiredColorSpace;
			}
			set
			{
			}
		}

		private LightmapSettings()
		{
		}

		[NativeName("ResetAndAwakeFromLoad")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Reset();
	}
}
