using System;

namespace UnityEngine.iOS
{
	[Obsolete("iOS.Notification Services is deprecated. Consider using the Mobile Notifications package (available in the package manager) which implements the UserNotifications framework.")]
	public enum NotificationType
	{
		None,
		Badge,
		Sound,
		Alert = 4
	}
}
