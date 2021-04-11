using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("CalendarIdentifier is deprecated. Please use iOS.CalendarIdentifier instead (UnityUpgradable) -> UnityEngine.iOS.CalendarIdentifier", true)]
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
