using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.iOS
{
	[NativeHeader("PlatformDependent/iPhonePlayer/IOSScriptBindings.h")]
	public sealed class Device
	{
		public static extern string systemVersion
		{
			[FreeFunction("systeminfo::GetDeviceSystemVersion")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern DeviceGeneration generation
		{
			[FreeFunction("UnityDeviceGeneration"), NativeConditional("PLATFORM_IOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string vendorIdentifier
		{
			[FreeFunction("UnityVendorIdentifier"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static string advertisingIdentifier
		{
			get
			{
				string adIdentifier = Device.GetAdIdentifier();
				Application.InvokeOnAdvertisingIdentifierCallback(adIdentifier, Device.IsAdTrackingEnabled());
				return adIdentifier;
			}
		}

		public static bool advertisingTrackingEnabled
		{
			get
			{
				return Device.IsAdTrackingEnabled();
			}
		}

		public static extern bool hideHomeButton
		{
			[FreeFunction("IOSScripting::GetHideHomeButton"), NativeConditional("PLATFORM_IOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("IOSScripting::SetHideHomeButton"), NativeConditional("PLATFORM_IOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool lowPowerModeEnabled
		{
			[FreeFunction("IOSScripting::GetLowPowerModeEnabled"), NativeConditional("PLATFORM_IOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool wantsSoftwareDimming
		{
			[FreeFunction("IOSScripting::GetWantsSoftwareDimming"), NativeConditional("PLATFORM_IOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("IOSScripting::SetWantsSoftwareDimming"), NativeConditional("PLATFORM_IOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		private static extern int deferSystemGesturesModeInternal
		{
			[FreeFunction("IOSScripting::GetDeferSystemGesturesMode"), NativeConditional("PLATFORM_IOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("IOSScripting::SetDeferSystemGesturesMode"), NativeConditional("PLATFORM_IOS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static SystemGestureDeferMode deferSystemGesturesMode
		{
			get
			{
				return (SystemGestureDeferMode)Device.deferSystemGesturesModeInternal;
			}
			set
			{
				Device.deferSystemGesturesModeInternal = (int)value;
			}
		}

		[FreeFunction("UnityAdIdentifier"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetAdIdentifier();

		[FreeFunction("IOSScripting::IsAdTrackingEnabled"), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAdTrackingEnabled();

		[NativeConditional("PLATFORM_IOS || PLATFORM_TVOS"), NativeMethod(Name = "IOSScripting::SetNoBackupFlag", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetNoBackupFlag(string path);

		[NativeConditional("PLATFORM_IOS || PLATFORM_TVOS"), NativeMethod(Name = "IOSScripting::ResetNoBackupFlag", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetNoBackupFlag(string path);

		[NativeConditional("PLATFORM_IOS"), NativeMethod(Name = "IOSScripting::RequestStoreReview", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RequestStoreReview();
	}
}
