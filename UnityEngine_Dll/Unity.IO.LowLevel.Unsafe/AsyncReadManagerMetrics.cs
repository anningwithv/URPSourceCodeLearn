using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	[NativeConditional("ENABLE_PROFILER")]
	public static class AsyncReadManagerMetrics
	{
		[Flags]
		public enum Flags
		{
			None = 0,
			ClearOnRead = 1
		}

		[FreeFunction("AreMetricsEnabled_Internal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsEnabled();

		[FreeFunction("GetAsyncReadManagerMetrics()->ClearMetrics"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearMetrics_Internal();

		public static void ClearCompletedMetrics()
		{
			AsyncReadManagerMetrics.ClearMetrics_Internal();
		}

		[FreeFunction("GetAsyncReadManagerMetrics()->GetMarshalledMetrics"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerRequestMetric[] GetMetrics_Internal(bool clear);

		[FreeFunction("GetAsyncReadManagerMetrics()->GetMetrics_NoAlloc"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetMetrics_NoAlloc_Internal([NotNull("ArgumentNullException")] List<AsyncReadManagerRequestMetric> metrics, bool clear);

		[FreeFunction("GetAsyncReadManagerMetrics()->GetMarshalledMetrics_Filtered_Managed"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerRequestMetric[] GetMetrics_Filtered_Internal(AsyncReadManagerMetricsFilters filters, bool clear);

		[FreeFunction("GetAsyncReadManagerMetrics()->GetMetrics_NoAlloc_Filtered_Managed"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetMetrics_NoAlloc_Filtered_Internal([NotNull("ArgumentNullException")] List<AsyncReadManagerRequestMetric> metrics, AsyncReadManagerMetricsFilters filters, bool clear);

		public static AsyncReadManagerRequestMetric[] GetMetrics(AsyncReadManagerMetricsFilters filters, AsyncReadManagerMetrics.Flags flags)
		{
			bool clear = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetMetrics_Filtered_Internal(filters, clear);
		}

		public static void GetMetrics(List<AsyncReadManagerRequestMetric> outMetrics, AsyncReadManagerMetricsFilters filters, AsyncReadManagerMetrics.Flags flags)
		{
			bool clear = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			AsyncReadManagerMetrics.GetMetrics_NoAlloc_Filtered_Internal(outMetrics, filters, clear);
		}

		public static AsyncReadManagerRequestMetric[] GetMetrics(AsyncReadManagerMetrics.Flags flags)
		{
			bool clear = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetMetrics_Internal(clear);
		}

		public static void GetMetrics(List<AsyncReadManagerRequestMetric> outMetrics, AsyncReadManagerMetrics.Flags flags)
		{
			bool clear = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			AsyncReadManagerMetrics.GetMetrics_NoAlloc_Internal(outMetrics, clear);
		}

		[FreeFunction("GetAsyncReadManagerMetrics()->StartCollecting")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StartCollectingMetrics();

		[FreeFunction("GetAsyncReadManagerMetrics()->StopCollecting")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StopCollectingMetrics();

		[FreeFunction("GetAsyncReadManagerMetrics()->GetCurrentSummaryMetrics")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryMetrics_Internal(bool clear);

		public static AsyncReadManagerSummaryMetrics GetCurrentSummaryMetrics(AsyncReadManagerMetrics.Flags flags)
		{
			bool clear = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetSummaryMetrics_Internal(clear);
		}

		[FreeFunction("GetAsyncReadManagerMetrics()->GetCurrentSummaryMetricsWithFilters")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryMetricsWithFilters_Internal(AsyncReadManagerMetricsFilters metricsFilters, bool clear);

		public static AsyncReadManagerSummaryMetrics GetCurrentSummaryMetrics(AsyncReadManagerMetricsFilters metricsFilters, AsyncReadManagerMetrics.Flags flags)
		{
			bool clear = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetSummaryMetricsWithFilters_Internal(metricsFilters, clear);
		}

		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetrics_Managed"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetrics_Internal(AsyncReadManagerRequestMetric[] metrics);

		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(AsyncReadManagerRequestMetric[] metrics)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetrics_Internal(metrics);
		}

		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetrics_FromContainer_Managed", ThrowsException = true), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetrics_FromContainer_Internal(List<AsyncReadManagerRequestMetric> metrics);

		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(List<AsyncReadManagerRequestMetric> metrics)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetrics_FromContainer_Internal(metrics);
		}

		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetricsWithFilters_Managed"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetricsWithFilters_Internal(AsyncReadManagerRequestMetric[] metrics, AsyncReadManagerMetricsFilters metricsFilters);

		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(AsyncReadManagerRequestMetric[] metrics, AsyncReadManagerMetricsFilters metricsFilters)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetricsWithFilters_Internal(metrics, metricsFilters);
		}

		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetricsWithFilters_FromContainer_Managed", ThrowsException = true), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetricsWithFilters_FromContainer_Internal(List<AsyncReadManagerRequestMetric> metrics, AsyncReadManagerMetricsFilters metricsFilters);

		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(List<AsyncReadManagerRequestMetric> metrics, AsyncReadManagerMetricsFilters metricsFilters)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetricsWithFilters_FromContainer_Internal(metrics, metricsFilters);
		}

		[FreeFunction("GetAsyncReadManagerMetrics()->GetTotalSizeNonASRMReadsBytes"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong GetTotalSizeOfNonASRMReadsBytes(bool emptyAfterRead);
	}
}
