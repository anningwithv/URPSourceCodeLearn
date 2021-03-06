using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class iPhoneSettings
	{
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("screenOrientation property is deprecated. Please use Screen.orientation instead (UnityUpgradable) -> Screen.orientation", true)]
		public static iPhoneScreenOrientation screenOrientation
		{
			get
			{
				return iPhoneScreenOrientation.Unknown;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("uniqueIdentifier property is deprecated. Please use SystemInfo.deviceUniqueIdentifier instead (UnityUpgradable) -> SystemInfo.deviceUniqueIdentifier", true)]
		public static string uniqueIdentifier
		{
			get
			{
				return string.Empty;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("name property is deprecated (UnityUpgradable). Please use SystemInfo.deviceName instead (UnityUpgradable) -> SystemInfo.deviceName", true)]
		public static string name
		{
			get
			{
				return string.Empty;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("model property is deprecated. Please use SystemInfo.deviceModel instead (UnityUpgradable) -> SystemInfo.deviceModel", true)]
		public static string model
		{
			get
			{
				return string.Empty;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("systemName property is deprecated. Please use SystemInfo.operatingSystem instead (UnityUpgradable) -> SystemInfo.operatingSystem", true)]
		public static string systemName
		{
			get
			{
				return string.Empty;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("internetReachability property is deprecated. Please use Application.internetReachability instead (UnityUpgradable) -> Application.internetReachability", true)]
		public static iPhoneNetworkReachability internetReachability
		{
			get
			{
				return iPhoneNetworkReachability.NotReachable;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("systemVersion property is deprecated. Please use iOS.Device.systemVersion instead (UnityUpgradable) -> UnityEngine.iOS.Device.systemVersion", true)]
		public static string systemVersion
		{
			get
			{
				return string.Empty;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("generation property is deprecated. Please use iOS.Device.generation instead (UnityUpgradable) -> UnityEngine.iOS.Device.generation", true)]
		public static iPhoneGeneration generation
		{
			get
			{
				return iPhoneGeneration.Unknown;
			}
		}

		[Obsolete("verticalOrientation property is deprecated. Please use Screen.orientation == ScreenOrientation.Portrait instead.", false)]
		public static bool verticalOrientation
		{
			get
			{
				return false;
			}
		}

		[Obsolete("screenCanDarken property is deprecated. Please use (Screen.sleepTimeout != SleepTimeout.NeverSleep) instead.", false)]
		public static bool screenCanDarken
		{
			get
			{
				return false;
			}
		}

		[Obsolete("locationServiceEnabledByUser property is deprecated. Please use Input.location.isEnabledByUser instead.", true)]
		public static bool locationServiceEnabledByUser
		{
			get
			{
				return false;
			}
		}

		[Obsolete("StartLocationServiceUpdates method is deprecated. Please use Input.location.Start instead.", true)]
		public static void StartLocationServiceUpdates(float desiredAccuracyInMeters, float updateDistanceInMeters)
		{
		}

		[Obsolete("StartLocationServiceUpdates method is deprecated. Please use Input.location.Start instead.", true)]
		public static void StartLocationServiceUpdates(float desiredAccuracyInMeters)
		{
		}

		[Obsolete("StartLocationServiceUpdates method is deprecated. Please use Input.location.Start instead.", true)]
		public static void StartLocationServiceUpdates()
		{
		}

		[Obsolete("StopLocationServiceUpdates method is deprecated. Please use Input.location.Stop instead.", true)]
		public static void StopLocationServiceUpdates()
		{
		}
	}
}
