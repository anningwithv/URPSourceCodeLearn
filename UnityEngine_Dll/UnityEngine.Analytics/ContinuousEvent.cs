using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	[NativeHeader("Modules/UnityAnalytics/Public/UnityAnalytics.h"), NativeHeader("Modules/UnityAnalytics/ContinuousEvent/Manager.h"), ExcludeFromDocs, RequiredByNativeCode]
	public class ContinuousEvent
	{
		public static AnalyticsResult RegisterCollector<T>(string metricName, Func<T> del) where T : struct, IComparable<T>, IEquatable<T>
		{
			bool flag = string.IsNullOrEmpty(metricName);
			if (flag)
			{
				throw new ArgumentException("Cannot set metric name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = ContinuousEvent.InternalRegisterCollector(typeof(T).ToString(), metricName, del);
			}
			return result;
		}

		public static AnalyticsResult SetEventHistogramThresholds<T>(string eventName, int count, T[] data, int ver = 1, string prefix = "") where T : struct, IComparable<T>, IEquatable<T>
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = ContinuousEvent.InternalSetEventHistogramThresholds(typeof(T).ToString(), eventName, count, data, ver, prefix);
			}
			return result;
		}

		public static AnalyticsResult SetCustomEventHistogramThresholds<T>(string eventName, int count, T[] data) where T : struct, IComparable<T>, IEquatable<T>
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = ContinuousEvent.InternalSetCustomEventHistogramThresholds(typeof(T).ToString(), eventName, count, data);
			}
			return result;
		}

		public static AnalyticsResult ConfigureCustomEvent(string customEventName, string metricName, float interval, float period, bool enabled = true)
		{
			bool flag = string.IsNullOrEmpty(customEventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = ContinuousEvent.InternalConfigureCustomEvent(customEventName, metricName, interval, period, enabled);
			}
			return result;
		}

		public static AnalyticsResult ConfigureEvent(string eventName, string metricName, float interval, float period, bool enabled = true, int ver = 1, string prefix = "")
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult result;
			if (flag2)
			{
				result = AnalyticsResult.NotInitialized;
			}
			else
			{
				result = ContinuousEvent.InternalConfigureEvent(eventName, metricName, interval, period, enabled, ver, prefix);
			}
			return result;
		}

		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult InternalRegisterCollector(string type, string metricName, object collector);

		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult InternalSetEventHistogramThresholds(string type, string eventName, int count, object data, int ver, string prefix);

		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult InternalSetCustomEventHistogramThresholds(string type, string eventName, int count, object data);

		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult InternalConfigureCustomEvent(string customEventName, string metricName, float interval, float period, bool enabled);

		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult InternalConfigureEvent(string eventName, string metricName, float interval, float period, bool enabled, int ver, string prefix);

		internal static bool IsInitialized()
		{
			return Analytics.IsInitialized();
		}
	}
}
