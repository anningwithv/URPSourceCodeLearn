using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.iOS
{
	[Obsolete("iOS.Notification Services is deprecated. Consider using the Mobile Notifications package (available in the package manager) which implements the UserNotifications framework."), NativeConditional("PLATFORM_IOS"), NativeHeader("PlatformDependent/iPhonePlayer/Notifications.h"), RequiredByNativeCode]
	public sealed class LocalNotification
	{
		private IntPtr m_Ptr;

		private static long m_NSReferenceDateTicks = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

		public extern string timeZone
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern CalendarIdentifier repeatCalendar
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod(Name = "NotificationScripting::SetRepeatCalendar", IsFreeFunction = true, HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern CalendarUnit repeatInterval
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("FireDate")]
		private extern double fireDateImpl
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public DateTime fireDate
		{
			get
			{
				return new DateTime((long)(this.fireDateImpl * 10000000.0) + LocalNotification.m_NSReferenceDateTicks);
			}
			set
			{
				this.fireDateImpl = (double)(value.ToUniversalTime().Ticks - LocalNotification.m_NSReferenceDateTicks) / 10000000.0;
			}
		}

		public extern string alertBody
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern string alertTitle
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern string alertAction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern string alertLaunchImage
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern string soundName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int applicationIconBadgeNumber
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern string defaultSoundName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeThrows]
		public extern IDictionary userInfo
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool hasAction
		{
			[NativeName("HasAction")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("HasAction")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public LocalNotification()
		{
			this.m_Ptr = NotificationHelper.CreateLocal();
		}

		~LocalNotification()
		{
			NotificationHelper.DestroyLocal(this.m_Ptr);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Schedule();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void PresentNow();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Cancel();
	}
}
