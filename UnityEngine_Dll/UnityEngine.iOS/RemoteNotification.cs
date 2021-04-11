using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.iOS
{
	[Obsolete("iOS.Notification Services is deprecated. Consider using the Mobile Notifications package (available in the package manager) which implements the UserNotifications framework."), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS"), NativeHeader("PlatformDependent/iPhonePlayer/Notifications.h"), RequiredByNativeCode]
	public sealed class RemoteNotification
	{
		private IntPtr m_Ptr;

		public extern string alertBody
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern string alertTitle
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern string soundName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int applicationIconBadgeNumber
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern IDictionary userInfo
		{
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool hasAction
		{
			[NativeName("HasAction")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		private RemoteNotification()
		{
		}

		~RemoteNotification()
		{
			NotificationHelper.DestroyRemote(this.m_Ptr);
		}
	}
}
