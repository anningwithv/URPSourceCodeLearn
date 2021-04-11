using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Analytics
{
	[NativeHeader("Modules/UnityConnect/UnityConnectSettings.h"), NativeHeader("Modules/UnityAnalytics/Public/Events/UserCustomEvent.h"), NativeHeader("Modules/UnityAnalytics/Public/UnityAnalytics.h")]
	[StructLayout(LayoutKind.Sequential)]
	public static class Analytics
	{
		[Serializable]
		private struct UserInfo
		{
			public string custom_userid;

			public string sex;
		}

		[Serializable]
		private struct UserInfoBirthYear
		{
			public int birth_year;
		}

		public static bool initializeOnStartup
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				return !flag && Analytics.initializeOnStartupInternal;
			}
			set
			{
				bool flag = Analytics.IsInitialized();
				if (flag)
				{
					Analytics.initializeOnStartupInternal = value;
				}
			}
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool initializeOnStartupInternal
		{
			[NativeMethod("GetInitializeOnStartup")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetInitializeOnStartup")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool enabledInternal
		{
			[NativeMethod("GetEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool playerOptedOutInternal
		{
			[NativeMethod("GetPlayerOptedOut")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		private static extern string eventUrlInternal
		{
			[NativeMethod("GetEventUrl")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		private static extern string configUrlInternal
		{
			[NativeMethod("GetConfigUrl")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool limitUserTrackingInternal
		{
			[NativeMethod("GetLimitUserTracking")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetLimitUserTracking")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool deviceStatsEnabledInternal
		{
			[NativeMethod("GetDeviceStatsEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetDeviceStatsEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static bool playerOptedOut
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				return !flag && Analytics.playerOptedOutInternal;
			}
		}

		public static string eventUrl
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				string result;
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					result = Analytics.eventUrlInternal;
				}
				return result;
			}
		}

		public static string configUrl
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				string result;
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					result = Analytics.configUrlInternal;
				}
				return result;
			}
		}

		public static bool limitUserTracking
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				return !flag && Analytics.limitUserTrackingInternal;
			}
			set
			{
				bool flag = Analytics.IsInitialized();
				if (flag)
				{
					Analytics.limitUserTrackingInternal = value;
				}
			}
		}

		public static bool deviceStatsEnabled
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				return !flag && Analytics.deviceStatsEnabledInternal;
			}
			set
			{
				bool flag = Analytics.IsInitialized();
				if (flag)
				{
					Analytics.deviceStatsEnabledInternal = value;
				}
			}
		}

		public static bool enabled
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				return !flag && Analytics.enabledInternal;
			}
			set
			{
				bool flag = Analytics.IsInitialized();
				if (flag)
				{
					Analytics.enabledInternal = value;
				}
			}
		}

		public static AnalyticsResult ResumeInitialization()
		{
			bool flag = !Analytics.IsInitialized();
			AnalyticsResult result;
			if (flag)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = Analytics.ResumeInitializationInternal();
			}
			return result;
		}

		[NativeMethod("ResumeInitialization"), StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult ResumeInitializationInternal();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInitialized();

		[NativeMethod("FlushEvents"), StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FlushArchivedEvents();

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult Transaction(string productId, double amount, string currency, string receiptPurchaseData, string signature, bool usingIAPService);

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult SendCustomEventName(string customEventName);

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult SendCustomEvent(CustomEventData eventData);

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AnalyticsResult IsCustomEventWithLimitEnabled(string customEventName);

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AnalyticsResult EnableCustomEventWithLimit(string customEventName, bool enable);

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AnalyticsResult IsEventWithLimitEnabled(string eventName, int ver, string prefix);

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AnalyticsResult EnableEventWithLimit(string eventName, bool enable, int ver, string prefix);

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AnalyticsResult RegisterEventWithLimit(string eventName, int maxEventPerHour, int maxItems, string vendorKey, int ver, string prefix, string assemblyInfo, bool notifyServer);

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AnalyticsResult RegisterEventsWithLimit(string[] eventName, int maxEventPerHour, int maxItems, string vendorKey, int ver, string prefix, string assemblyInfo, bool notifyServer);

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AnalyticsResult SendEventWithLimit(string eventName, object parameters, int ver, string prefix);

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AnalyticsResult SetEventWithLimitEndPoint(string eventName, string endPoint, int ver, string prefix);

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AnalyticsResult SetEventWithLimitPriority(string eventName, AnalyticsEventPriority eventPriority, int ver, string prefix);

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool QueueEvent(string eventName, object parameters, int ver, string prefix);

		public static AnalyticsResult FlushEvents()
		{
			bool flag = !Analytics.IsInitialized();
			AnalyticsResult result;
			if (flag)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = (Analytics.FlushArchivedEvents() ? AnalyticsResult.Ok : AnalyticsResult.NotInitialized);
			}
			return result;
		}

		public static AnalyticsResult SetUserId(string userId)
		{
			bool flag = string.IsNullOrEmpty(userId);
			if (flag)
			{
				throw new ArgumentException("Cannot set userId to an empty or null string");
			}
			return Analytics.SendUserInfoEvent(new Analytics.UserInfo
			{
				custom_userid = userId
			});
		}

		public static AnalyticsResult SetUserGender(Gender gender)
		{
			return Analytics.SendUserInfoEvent(new Analytics.UserInfo
			{
				sex = ((gender == Gender.Male) ? "M" : ((gender == Gender.Female) ? "F" : "U"))
			});
		}

		public static AnalyticsResult SetUserBirthYear(int birthYear)
		{
			return Analytics.SendUserInfoEvent(new Analytics.UserInfoBirthYear
			{
				birth_year = birthYear
			});
		}

		private static AnalyticsResult SendUserInfoEvent(object param)
		{
			bool flag = !Analytics.IsInitialized();
			AnalyticsResult result;
			if (flag)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				Analytics.QueueEvent("userInfo", param, 1, string.Empty);
				result = AnalyticsResult.Ok;
			}
			return result;
		}

		public static AnalyticsResult Transaction(string productId, decimal amount, string currency)
		{
			return Analytics.Transaction(productId, amount, currency, null, null, false);
		}

		public static AnalyticsResult Transaction(string productId, decimal amount, string currency, string receiptPurchaseData, string signature)
		{
			return Analytics.Transaction(productId, amount, currency, receiptPurchaseData, signature, false);
		}

		public static AnalyticsResult Transaction(string productId, decimal amount, string currency, string receiptPurchaseData, string signature, bool usingIAPService)
		{
			bool flag = string.IsNullOrEmpty(productId);
			if (flag)
			{
				throw new ArgumentException("Cannot set productId to an empty or null string");
			}
			bool flag2 = string.IsNullOrEmpty(currency);
			if (flag2)
			{
				throw new ArgumentException("Cannot set currency to an empty or null string");
			}
			bool flag3 = !Analytics.IsInitialized();
			AnalyticsResult result;
			if (flag3)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				bool flag4 = receiptPurchaseData == null;
				if (flag4)
				{
					receiptPurchaseData = string.Empty;
				}
				bool flag5 = signature == null;
				if (flag5)
				{
					signature = string.Empty;
				}
				result = Analytics.Transaction(productId, Convert.ToDouble(amount), currency, receiptPurchaseData, signature, usingIAPService);
			}
			return result;
		}

		public static AnalyticsResult CustomEvent(string customEventName)
		{
			bool flag = string.IsNullOrEmpty(customEventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set custom event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = Analytics.SendCustomEventName(customEventName);
			}
			return result;
		}

		public static AnalyticsResult CustomEvent(string customEventName, Vector3 position)
		{
			bool flag = string.IsNullOrEmpty(customEventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set custom event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				CustomEventData customEventData = new CustomEventData(customEventName);
				customEventData.AddDouble("x", (double)Convert.ToDecimal(position.x));
				customEventData.AddDouble("y", (double)Convert.ToDecimal(position.y));
				customEventData.AddDouble("z", (double)Convert.ToDecimal(position.z));
				AnalyticsResult analyticsResult = Analytics.SendCustomEvent(customEventData);
				customEventData.Dispose();
				result = analyticsResult;
			}
			return result;
		}

		public static AnalyticsResult CustomEvent(string customEventName, IDictionary<string, object> eventData)
		{
			bool flag = string.IsNullOrEmpty(customEventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set custom event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				bool flag3 = eventData == null;
				if (flag3)
				{
					result = Analytics.SendCustomEventName(customEventName);
				}
				else
				{
					CustomEventData customEventData = new CustomEventData(customEventName);
					AnalyticsResult analyticsResult = AnalyticsResult.InvalidData;
					try
					{
						customEventData.AddDictionary(eventData);
						analyticsResult = Analytics.SendCustomEvent(customEventData);
					}
					finally
					{
						customEventData.Dispose();
					}
					result = analyticsResult;
				}
			}
			return result;
		}

		public static AnalyticsResult EnableCustomEvent(string customEventName, bool enabled)
		{
			bool flag = string.IsNullOrEmpty(customEventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = Analytics.EnableCustomEventWithLimit(customEventName, enabled);
			}
			return result;
		}

		public static AnalyticsResult IsCustomEventEnabled(string customEventName)
		{
			bool flag = string.IsNullOrEmpty(customEventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = Analytics.IsCustomEventWithLimitEnabled(customEventName);
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static AnalyticsResult RegisterEvent(string eventName, int maxEventPerHour, int maxItems, string vendorKey = "", string prefix = "")
		{
			string assemblyInfo = string.Empty;
			assemblyInfo = Assembly.GetCallingAssembly().FullName;
			return Analytics.RegisterEvent(eventName, maxEventPerHour, maxItems, vendorKey, 1, prefix, assemblyInfo);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static AnalyticsResult RegisterEvent(string eventName, int maxEventPerHour, int maxItems, string vendorKey, int ver, string prefix = "")
		{
			string assemblyInfo = string.Empty;
			assemblyInfo = Assembly.GetCallingAssembly().FullName;
			return Analytics.RegisterEvent(eventName, maxEventPerHour, maxItems, vendorKey, ver, prefix, assemblyInfo);
		}

		private static AnalyticsResult RegisterEvent(string eventName, int maxEventPerHour, int maxItems, string vendorKey, int ver, string prefix, string assemblyInfo)
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = Analytics.RegisterEventWithLimit(eventName, maxEventPerHour, maxItems, vendorKey, ver, prefix, assemblyInfo, true);
			}
			return result;
		}

		public static AnalyticsResult SendEvent(string eventName, object parameters, int ver = 1, string prefix = "")
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = parameters == null;
			if (flag2)
			{
				throw new ArgumentException("Cannot set parameters to null");
			}
			bool flag3 = !Analytics.IsInitialized();
			AnalyticsResult result;
			if (flag3)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = Analytics.SendEventWithLimit(eventName, parameters, ver, prefix);
			}
			return result;
		}

		public static AnalyticsResult SetEventEndPoint(string eventName, string endPoint, int ver = 1, string prefix = "")
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = endPoint == null;
			if (flag2)
			{
				throw new ArgumentException("Cannot set parameters to null");
			}
			bool flag3 = !Analytics.IsInitialized();
			AnalyticsResult result;
			if (flag3)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = Analytics.SetEventWithLimitEndPoint(eventName, endPoint, ver, prefix);
			}
			return result;
		}

		public static AnalyticsResult SetEventPriority(string eventName, AnalyticsEventPriority eventPriority, int ver = 1, string prefix = "")
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = Analytics.SetEventWithLimitPriority(eventName, eventPriority, ver, prefix);
			}
			return result;
		}

		public static AnalyticsResult EnableEvent(string eventName, bool enabled, int ver = 1, string prefix = "")
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = Analytics.EnableEventWithLimit(eventName, enabled, ver, prefix);
			}
			return result;
		}

		public static AnalyticsResult IsEventEnabled(string eventName, int ver = 1, string prefix = "")
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = Analytics.IsEventWithLimitEnabled(eventName, ver, prefix);
			}
			return result;
		}
	}
}
