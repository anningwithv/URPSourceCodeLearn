using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Android
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct Permission
	{
		public const string Camera = "android.permission.CAMERA";

		public const string Microphone = "android.permission.RECORD_AUDIO";

		public const string FineLocation = "android.permission.ACCESS_FINE_LOCATION";

		public const string CoarseLocation = "android.permission.ACCESS_COARSE_LOCATION";

		public const string ExternalStorageRead = "android.permission.READ_EXTERNAL_STORAGE";

		public const string ExternalStorageWrite = "android.permission.WRITE_EXTERNAL_STORAGE";

		private static AndroidJavaObject m_UnityPermissions;

		private static AndroidJavaObject m_Activity;

		private static AndroidJavaObject GetActivity()
		{
			bool flag = Permission.m_Activity != null;
			AndroidJavaObject activity;
			if (flag)
			{
				activity = Permission.m_Activity;
			}
			else
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					Permission.m_Activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				}
				activity = Permission.m_Activity;
			}
			return activity;
		}

		private static AndroidJavaObject GetUnityPermissions()
		{
			bool flag = Permission.m_UnityPermissions != null;
			AndroidJavaObject unityPermissions;
			if (flag)
			{
				unityPermissions = Permission.m_UnityPermissions;
			}
			else
			{
				Permission.m_UnityPermissions = new AndroidJavaClass("com.unity3d.player.UnityPermissions");
				unityPermissions = Permission.m_UnityPermissions;
			}
			return unityPermissions;
		}

		public static bool HasUserAuthorizedPermission(string permission)
		{
			bool flag = permission == null;
			return !flag;
		}

		public static void RequestUserPermission(string permission)
		{
			bool flag = permission == null;
			if (!flag)
			{
				Permission.RequestUserPermissions(new string[]
				{
					permission
				}, null);
			}
		}

		public static void RequestUserPermissions(string[] permissions)
		{
			bool flag = permissions == null || permissions.Length == 0;
			if (!flag)
			{
				Permission.RequestUserPermissions(permissions, null);
			}
		}

		public static void RequestUserPermission(string permission, PermissionCallbacks callbacks)
		{
			bool flag = permission == null;
			if (!flag)
			{
				Permission.RequestUserPermissions(new string[]
				{
					permission
				}, callbacks);
			}
		}

		public static void RequestUserPermissions(string[] permissions, PermissionCallbacks callbacks)
		{
			bool flag = permissions == null || permissions.Length == 0;
			if (flag)
			{
			}
		}
	}
}
