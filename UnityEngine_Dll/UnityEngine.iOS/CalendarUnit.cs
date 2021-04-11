using System;

namespace UnityEngine.iOS
{
	[Obsolete("iOS.Notification Services is deprecated. Consider using the Mobile Notifications package (available in the package manager) which implements the UserNotifications framework.")]
	public enum CalendarUnit
	{
		Era = 2,
		Year = 4,
		Month = 8,
		Day = 16,
		Hour = 32,
		Minute = 64,
		Second = 128,
		Week = 256,
		Weekday = 512,
		WeekdayOrdinal = 1024,
		Quarter = 2048
	}
}
