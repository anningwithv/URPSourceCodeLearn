using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Utilities/PlayerPrefs.h")]
	public class PlayerPrefs
	{
		[NativeMethod("SetInt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetInt(string key, int value);

		[NativeMethod("SetFloat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetFloat(string key, float value);

		[NativeMethod("SetString")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetSetString(string key, string value);

		public static void SetInt(string key, int value)
		{
			bool flag = !PlayerPrefs.TrySetInt(key, value);
			if (flag)
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetInt(string key, int defaultValue);

		public static int GetInt(string key)
		{
			return PlayerPrefs.GetInt(key, 0);
		}

		public static void SetFloat(string key, float value)
		{
			bool flag = !PlayerPrefs.TrySetFloat(key, value);
			if (flag)
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetFloat(string key, float defaultValue);

		public static float GetFloat(string key)
		{
			return PlayerPrefs.GetFloat(key, 0f);
		}

		public static void SetString(string key, string value)
		{
			bool flag = !PlayerPrefs.TrySetSetString(key, value);
			if (flag)
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetString(string key, string defaultValue);

		public static string GetString(string key)
		{
			return PlayerPrefs.GetString(key, "");
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasKey(string key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteKey(string key);

		[NativeMethod("DeleteAllWithCallback")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteAll();

		[NativeMethod("Sync")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Save();

		[NativeMethod("SetInt"), StaticAccessor("EditorPrefs", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void EditorPrefsSetInt(string key, int value);

		[NativeMethod("GetInt"), StaticAccessor("EditorPrefs", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int EditorPrefsGetInt(string key, int defaultValue);
	}
}
