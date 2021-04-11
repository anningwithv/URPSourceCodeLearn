using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Advertisements
{
	[NativeHeader("Modules/UnityConnect/UnityAds/UnityAdsSettings.h")]
	internal static class UnityAdsSettings
	{
		[StaticAccessor("GetUnityAdsSettings()", StaticAccessorType.Dot)]
		public static extern bool enabled
		{
			[ThreadSafe]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[ThreadSafe]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetUnityAdsSettings()", StaticAccessorType.Dot)]
		public static extern bool initializeOnStartup
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetUnityAdsSettings()", StaticAccessorType.Dot)]
		public static extern bool testMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("warning No longer supported and will always return true")]
		public static bool IsPlatformEnabled(RuntimePlatform platform)
		{
			return true;
		}

		[Obsolete("warning No longer supported and will do nothing")]
		public static void SetPlatformEnabled(RuntimePlatform platform, bool value)
		{
		}

		[StaticAccessor("GetUnityAdsSettings()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetGameId(RuntimePlatform platform);

		[StaticAccessor("GetUnityAdsSettings()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGameId(RuntimePlatform platform, string gameId);
	}
}
