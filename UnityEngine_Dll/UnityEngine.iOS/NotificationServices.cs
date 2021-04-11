using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.iOS
{
	[Obsolete("iOS.Notification Services is deprecated. Consider using the Mobile Notifications package (available in the package manager) which implements the UserNotifications framework."), NativeConditional("PLATFORM_IOS || PLATFORM_TVOS"), NativeHeader("PlatformDependent/iPhonePlayer/Notifications.h")]
	public sealed class NotificationServices
	{
		public static extern int localNotificationCount
		{
			[FreeFunction("NotificationScripting::GetLocalCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int remoteNotificationCount
		{
			[FreeFunction("NotificationScripting::GetRemoteCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern NotificationType enabledNotificationTypes
		{
			[FreeFunction("GetEnabledNotificationTypes")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string registrationError
		{
			[FreeFunction("iPhoneRemoteNotification::GetError")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern byte[] deviceToken
		{
			[FreeFunction("NotificationScripting::GetDeviceToken")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static LocalNotification[] localNotifications
		{
			get
			{
				int localNotificationCount = NotificationServices.localNotificationCount;
				LocalNotification[] array = new LocalNotification[localNotificationCount];
				for (int i = 0; i < localNotificationCount; i++)
				{
					array[i] = NotificationServices.GetLocalNotificationImpl(i);
				}
				return array;
			}
		}

		public static RemoteNotification[] remoteNotifications
		{
			get
			{
				int remoteNotificationCount = NotificationServices.remoteNotificationCount;
				RemoteNotification[] array = new RemoteNotification[remoteNotificationCount];
				for (int i = 0; i < remoteNotificationCount; i++)
				{
					array[i] = NotificationServices.GetRemoteNotificationImpl(i);
				}
				return array;
			}
		}

		public static extern LocalNotification[] scheduledLocalNotifications
		{
			[FreeFunction("NotificationScripting::GetScheduledLocal")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[FreeFunction("NotificationScripting::ClearLocal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearLocalNotifications();

		[FreeFunction("NotificationScripting::ClearRemote")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearRemoteNotifications();

		[FreeFunction("RegisterForNotifications")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_RegisterImpl(NotificationType notificationTypes, bool registerForRemote);

		public static void RegisterForNotifications(NotificationType notificationTypes)
		{
			NotificationServices.Internal_RegisterImpl(notificationTypes, true);
		}

		public static void RegisterForNotifications(NotificationType notificationTypes, bool registerForRemote)
		{
			NotificationServices.Internal_RegisterImpl(notificationTypes, registerForRemote);
		}

		public static void ScheduleLocalNotification(LocalNotification notification)
		{
			notification.Schedule();
		}

		public static void PresentLocalNotificationNow(LocalNotification notification)
		{
			notification.PresentNow();
		}

		public static void CancelLocalNotification(LocalNotification notification)
		{
			notification.Cancel();
		}

		[FreeFunction("iPhoneLocalNotification::CancelAll")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CancelAllLocalNotifications();

		[FreeFunction("iPhoneRemoteNotification::Unregister")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnregisterForRemoteNotifications();

		[FreeFunction("NotificationScripting::GetLocal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern LocalNotification GetLocalNotificationImpl(int index);

		public static LocalNotification GetLocalNotification(int index)
		{
			bool flag = index < 0 || index >= NotificationServices.localNotificationCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", "Index out of bounds.");
			}
			return NotificationServices.GetLocalNotificationImpl(index);
		}

		[FreeFunction("NotificationScripting::GetRemote")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RemoteNotification GetRemoteNotificationImpl(int index);

		public static RemoteNotification GetRemoteNotification(int index)
		{
			bool flag = index < 0 || index >= NotificationServices.remoteNotificationCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", "Index out of bounds.");
			}
			return NotificationServices.GetRemoteNotificationImpl(index);
		}
	}
}
