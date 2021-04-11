using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("NotificationServices is deprecated. Please use iOS.NotificationServices instead (UnityUpgradable) -> UnityEngine.iOS.NotificationServices", true)]
	public sealed class NotificationServices
	{
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("RegisterForRemoteNotificationTypes is deprecated. Please use RegisterForNotifications instead (UnityUpgradable) -> UnityEngine.iOS.NotificationServices.RegisterForNotifications(*)", true)]
		public static void RegisterForRemoteNotificationTypes(RemoteNotificationType notificationTypes)
		{
		}
	}
}
