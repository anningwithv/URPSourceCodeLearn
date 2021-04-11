using System;

namespace UnityEngine.iOS
{
	[Obsolete("iOS.Notification Services is deprecated. Consider using the Mobile Notifications package (available in the package manager) which implements the UserNotifications framework.")]
	public enum CalendarIdentifier
	{
		GregorianCalendar,
		BuddhistCalendar,
		ChineseCalendar,
		HebrewCalendar,
		IslamicCalendar,
		IslamicCivilCalendar,
		JapaneseCalendar,
		RepublicOfChinaCalendar,
		PersianCalendar,
		IndianCalendar,
		ISO8601Calendar
	}
}
