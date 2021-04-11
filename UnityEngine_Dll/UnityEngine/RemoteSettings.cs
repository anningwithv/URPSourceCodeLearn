using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/UnityAnalytics/RemoteSettings/RemoteSettings.h"), NativeHeader("UnityAnalyticsScriptingClasses.h")]
	public static class RemoteSettings
	{
		public delegate void UpdatedEventHandler();

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event RemoteSettings.UpdatedEventHandler Updated;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action BeforeFetchFromServer;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<bool, bool, int> Completed;

		[RequiredByNativeCode]
		internal static void RemoteSettingsUpdated(bool wasLastUpdatedFromServer)
		{
			RemoteSettings.UpdatedEventHandler updated = RemoteSettings.Updated;
			bool flag = updated != null;
			if (flag)
			{
				updated();
			}
		}

		[RequiredByNativeCode]
		internal static void RemoteSettingsBeforeFetchFromServer()
		{
			Action beforeFetchFromServer = RemoteSettings.BeforeFetchFromServer;
			bool flag = beforeFetchFromServer != null;
			if (flag)
			{
				beforeFetchFromServer();
			}
		}

		[RequiredByNativeCode]
		internal static void RemoteSettingsUpdateCompleted(bool wasLastUpdatedFromServer, bool settingsChanged, int response)
		{
			Action<bool, bool, int> completed = RemoteSettings.Completed;
			bool flag = completed != null;
			if (flag)
			{
				completed(wasLastUpdatedFromServer, settingsChanged, response);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Calling CallOnUpdate() is not necessary any more and should be removed. Use RemoteSettingsUpdated instead", true)]
		public static void CallOnUpdate()
		{
			throw new NotSupportedException("Calling CallOnUpdate() is not necessary any more and should be removed.");
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ForceUpdate();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool WasLastUpdatedFromServer();

		[ExcludeFromDocs]
		public static int GetInt(string key)
		{
			return RemoteSettings.GetInt(key, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetInt(string key, [UnityEngine.Internal.DefaultValue("0")] int defaultValue);

		[ExcludeFromDocs]
		public static long GetLong(string key)
		{
			return RemoteSettings.GetLong(key, 0L);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetLong(string key, [UnityEngine.Internal.DefaultValue("0")] long defaultValue);

		[ExcludeFromDocs]
		public static float GetFloat(string key)
		{
			return RemoteSettings.GetFloat(key, 0f);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetFloat(string key, [UnityEngine.Internal.DefaultValue("0.0F")] float defaultValue);

		[ExcludeFromDocs]
		public static string GetString(string key)
		{
			return RemoteSettings.GetString(key, "");
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetString(string key, [UnityEngine.Internal.DefaultValue("\"\"")] string defaultValue);

		[ExcludeFromDocs]
		public static bool GetBool(string key)
		{
			return RemoteSettings.GetBool(key, false);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetBool(string key, [UnityEngine.Internal.DefaultValue("false")] bool defaultValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasKey(string key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetKeys();

		public static T GetObject<T>(string key = "")
		{
			return (T)((object)RemoteSettings.GetObject(typeof(T), key));
		}

		public static object GetObject(Type type, string key = "")
		{
			bool flag = type == null;
			if (flag)
			{
				throw new ArgumentNullException("type");
			}
			bool flag2 = type.IsAbstract || type.IsSubclassOf(typeof(Object));
			if (flag2)
			{
				throw new ArgumentException("Cannot deserialize to new instances of type '" + type.Name + ".'");
			}
			return RemoteSettings.GetAsScriptingObject(type, null, key);
		}

		public static object GetObject(string key, object defaultValue)
		{
			bool flag = defaultValue == null;
			if (flag)
			{
				throw new ArgumentNullException("defaultValue");
			}
			Type type = defaultValue.GetType();
			bool flag2 = type.IsAbstract || type.IsSubclassOf(typeof(Object));
			if (flag2)
			{
				throw new ArgumentException("Cannot deserialize to new instances of type '" + type.Name + ".'");
			}
			return RemoteSettings.GetAsScriptingObject(type, defaultValue, key);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object GetAsScriptingObject(Type t, object defaultValue, string key);

		public static IDictionary<string, object> GetDictionary(string key = "")
		{
			RemoteSettings.UseSafeLock();
			IDictionary<string, object> dictionary = RemoteConfigSettingsHelper.GetDictionary(RemoteSettings.GetSafeTopMap(), key);
			RemoteSettings.ReleaseSafeLock();
			return dictionary;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void UseSafeLock();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReleaseSafeLock();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeTopMap();
	}
}
