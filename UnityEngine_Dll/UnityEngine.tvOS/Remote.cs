using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.tvOS
{
	[NativeHeader("PlatformDependent/iPhonePlayer/IOSScriptBindings.h")]
	public sealed class Remote
	{
		public static extern bool allowExitToHome
		{
			[FreeFunction("UnityGetAppleTVRemoteAllowExitToMenu"), NativeConditional("PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("UnitySetAppleTVRemoteAllowExitToMenu"), NativeConditional("PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool allowRemoteRotation
		{
			[FreeFunction("UnityGetAppleTVRemoteAllowRotation"), NativeConditional("PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("UnitySetAppleTVRemoteAllowRotation"), NativeConditional("PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool reportAbsoluteDpadValues
		{
			[FreeFunction("UnityGetAppleTVRemoteReportAbsoluteDpadValues"), NativeConditional("PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("UnitySetAppleTVRemoteReportAbsoluteDpadValues"), NativeConditional("PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool touchesEnabled
		{
			[FreeFunction("TVOSScripting::GetRemoteTouchesEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("UnitySetAppleTVRemoteTouchesEnabled"), NativeConditional("PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
