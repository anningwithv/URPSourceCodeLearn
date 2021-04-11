using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	[NativeHeader("UnityAnalyticsScriptingClasses.h"), NativeHeader("Modules/UnityAnalytics/Public/UnityAnalytics.h"), RequiredByNativeCode]
	public static class AnalyticsSessionInfo
	{
		public delegate void SessionStateChanged(AnalyticsSessionState sessionState, long sessionId, long sessionElapsedTime, bool sessionChanged);

		public delegate void IdentityTokenChanged(string token);

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event AnalyticsSessionInfo.SessionStateChanged sessionStateChanged;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event AnalyticsSessionInfo.IdentityTokenChanged identityTokenChanged;

		public static extern AnalyticsSessionState sessionState
		{
			[NativeMethod("GetPlayerSessionState")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern long sessionId
		{
			[NativeMethod("GetPlayerSessionId")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern long sessionCount
		{
			[NativeMethod("GetPlayerSessionCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern long sessionElapsedTime
		{
			[NativeMethod("GetPlayerSessionElapsedTime")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool sessionFirstRun
		{
			[NativeMethod("GetPlayerSessionFirstRun", false, true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string userId
		{
			[NativeMethod("GetUserId")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static string customUserId
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				string result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = AnalyticsSessionInfo.customUserIdInternal;
				}
				return result;
			}
			set
			{
				bool flag = Analytics.IsInitialized();
				if (flag)
				{
					AnalyticsSessionInfo.customUserIdInternal = value;
				}
			}
		}

		public static string customDeviceId
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				string result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = AnalyticsSessionInfo.customDeviceIdInternal;
				}
				return result;
			}
			set
			{
				bool flag = Analytics.IsInitialized();
				if (flag)
				{
					AnalyticsSessionInfo.customDeviceIdInternal = value;
				}
			}
		}

		public static string identityToken
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				string result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = AnalyticsSessionInfo.identityTokenInternal;
				}
				return result;
			}
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern string identityTokenInternal
		{
			[NativeMethod("GetIdentityToken")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern string customUserIdInternal
		{
			[NativeMethod("GetCustomUserId")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetCustomUserId")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern string customDeviceIdInternal
		{
			[NativeMethod("GetCustomDeviceId")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetCustomDeviceId")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[RequiredByNativeCode]
		internal static void CallSessionStateChanged(AnalyticsSessionState sessionState, long sessionId, long sessionElapsedTime, bool sessionChanged)
		{
			AnalyticsSessionInfo.SessionStateChanged sessionStateChanged = AnalyticsSessionInfo.sessionStateChanged;
			bool flag = sessionStateChanged != null;
			if (flag)
			{
				sessionStateChanged(sessionState, sessionId, sessionElapsedTime, sessionChanged);
			}
		}

		[RequiredByNativeCode]
		internal static void CallIdentityTokenChanged(string token)
		{
			AnalyticsSessionInfo.IdentityTokenChanged identityTokenChanged = AnalyticsSessionInfo.identityTokenChanged;
			bool flag = identityTokenChanged != null;
			if (flag)
			{
				identityTokenChanged(token);
			}
		}
	}
}
