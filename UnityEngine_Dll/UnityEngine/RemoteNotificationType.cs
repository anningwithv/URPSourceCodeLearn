using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("RemoteNotificationType is deprecated. Please use iOS.NotificationType instead (UnityUpgradable) -> UnityEngine.iOS.NotificationType", true)]
	public enum RemoteNotificationType
	{
		None,
		Badge,
		Sound,
		Alert
	}
}
