using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.tvOS
{
	[NativeHeader("PlatformDependent/iPhonePlayer/IOSScriptBindings.h")]
	public sealed class Device
	{
		private static extern string tvOSsystemVersion
		{
			[FreeFunction("systeminfo::GetDeviceSystemVersion")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static string systemVersion
		{
			get
			{
				return Device.tvOSsystemVersion;
			}
		}

		private static extern DeviceGeneration tvOSGeneration
		{
			[FreeFunction("UnityDeviceGeneration"), NativeConditional("PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static DeviceGeneration generation
		{
			get
			{
				return Device.tvOSGeneration;
			}
		}

		private static extern string tvOSVendorIdentifier
		{
			[FreeFunction("UnityVendorIdentifier"), NativeConditional("PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static string vendorIdentifier
		{
			get
			{
				return Device.tvOSVendorIdentifier;
			}
		}

		public static string advertisingIdentifier
		{
			get
			{
				string tVOSAdIdentifier = Device.GetTVOSAdIdentifier();
				Application.InvokeOnAdvertisingIdentifierCallback(tVOSAdIdentifier, Device.IsTVOSAdTrackingEnabled());
				return tVOSAdIdentifier;
			}
		}

		public static bool advertisingTrackingEnabled
		{
			get
			{
				return Device.IsTVOSAdTrackingEnabled();
			}
		}

		[FreeFunction("UnityAdIdentifier"), NativeConditional("PLATFORM_TVOS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetTVOSAdIdentifier();

		[FreeFunction("IOSScripting::IsAdTrackingEnabled"), NativeConditional("PLATFORM_TVOS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTVOSAdTrackingEnabled();

		[NativeConditional("PLATFORM_TVOS"), NativeMethod(Name = "IOSScripting::SetNoBackupFlag", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SettvOSNoBackupFlag(string path);

		public static void SetNoBackupFlag(string path)
		{
			Device.SettvOSNoBackupFlag(path);
		}

		[NativeConditional("PLATFORM_TVOS"), NativeMethod(Name = "IOSScripting::ResetNoBackupFlag", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void tvOSResetNoBackupFlag(string path);

		public static void ResetNoBackupFlag(string path)
		{
			Device.tvOSResetNoBackupFlag(path);
		}
	}
}
