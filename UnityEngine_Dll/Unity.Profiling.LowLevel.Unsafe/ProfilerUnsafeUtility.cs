using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling.LowLevel.Unsafe
{
	[NativeHeader("Runtime/Profiler/ScriptBindings/ProfilerMarker.bindings.h"), UsedByNativeCode]
	public static class ProfilerUnsafeUtility
	{
		public struct TimestampConversionRatio
		{
			public long Numerator;

			public long Denominator;
		}

		public const ushort CategoryRender = 0;

		public const ushort CategoryScripts = 1;

		public const ushort CategoryGUI = 4;

		public const ushort CategoryPhysics = 5;

		public const ushort CategoryAnimation = 6;

		public const ushort CategoryAi = 7;

		public const ushort CategoryAudio = 8;

		public const ushort CategoryVideo = 11;

		public const ushort CategoryParticles = 12;

		public const ushort CategoryLighting = 13;

		[Obsolete("CategoryLightning has been renamed. Use CategoryLighting instead (UnityUpgradable) -> CategoryLighting", false)]
		public const ushort CategoryLightning = 13;

		public const ushort CategoryNetwork = 14;

		public const ushort CategoryLoading = 15;

		public const ushort CategoryOther = 16;

		public const ushort CategoryVr = 22;

		public const ushort CategoryAllocation = 23;

		public const ushort CategoryInternal = 24;

		public const ushort CategoryInput = 30;

		public const ushort CategoryVirtualTexturing = 31;

		internal const ushort CategoryAny = 65535;

		public static extern long Timestamp
		{
			[ThreadSafe]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static ProfilerUnsafeUtility.TimestampConversionRatio TimestampToNanosecondsConversionRatio
		{
			[ThreadSafe]
			get
			{
				ProfilerUnsafeUtility.TimestampConversionRatio result;
				ProfilerUnsafeUtility.get_TimestampToNanosecondsConversionRatio_Injected(out result);
				return result;
			}
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ushort GetCategoryByName(string name);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern ushort GetCategoryByName__Unmanaged(byte* name, int nameLen);

		[MethodImpl((MethodImplOptions)256)]
		public unsafe static ushort GetCategoryByName(char* name, int nameLen)
		{
			return ProfilerUnsafeUtility.GetCategoryByName_Unsafe(name, nameLen);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern ushort GetCategoryByName_Unsafe(char* name, int nameLen);

		[ThreadSafe]
		public static ProfilerCategoryDescription GetCategoryDescription(ushort categoryId)
		{
			ProfilerCategoryDescription result;
			ProfilerUnsafeUtility.GetCategoryDescription_Injected(categoryId, out result);
			return result;
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CreateMarker(string name, ushort categoryId, MarkerFlags flags, int metadataCount);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern IntPtr CreateMarker__Unmanaged(byte* name, int nameLen, ushort categoryId, MarkerFlags flags, int metadataCount);

		[MethodImpl((MethodImplOptions)256)]
		public unsafe static IntPtr CreateMarker(char* name, int nameLen, ushort categoryId, MarkerFlags flags, int metadataCount)
		{
			return ProfilerUnsafeUtility.CreateMarker_Unsafe(name, nameLen, categoryId, flags, metadataCount);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr CreateMarker_Unsafe(char* name, int nameLen, ushort categoryId, MarkerFlags flags, int metadataCount);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetMarkerMetadata(IntPtr markerPtr, int index, string name, byte type, byte unit);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void SetMarkerMetadata__Unmanaged(IntPtr markerPtr, int index, byte* name, int nameLen, byte type, byte unit);

		[MethodImpl((MethodImplOptions)256)]
		public unsafe static void SetMarkerMetadata(IntPtr markerPtr, int index, char* name, int nameLen, byte type, byte unit)
		{
			ProfilerUnsafeUtility.SetMarkerMetadata_Unsafe(markerPtr, index, name, nameLen, type, unit);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetMarkerMetadata_Unsafe(IntPtr markerPtr, int index, char* name, int nameLen, byte type, byte unit);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginSample(IntPtr markerPtr);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void BeginSampleWithMetadata(IntPtr markerPtr, int metadataCount, void* metadata);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndSample(IntPtr markerPtr);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void SingleSampleWithMetadata(IntPtr markerPtr, int metadataCount, void* metadata);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* CreateCounterValue(out IntPtr counterPtr, string name, ushort categoryId, MarkerFlags flags, byte dataType, byte dataUnit, int dataSize, ProfilerCounterOptions counterOptions);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void* CreateCounterValue__Unmanaged(out IntPtr counterPtr, byte* name, int nameLen, ushort categoryId, MarkerFlags flags, byte dataType, byte dataUnit, int dataSize, ProfilerCounterOptions counterOptions);

		[MethodImpl((MethodImplOptions)256)]
		public unsafe static void* CreateCounterValue(out IntPtr counterPtr, char* name, int nameLen, ushort categoryId, MarkerFlags flags, byte dataType, byte dataUnit, int dataSize, ProfilerCounterOptions counterOptions)
		{
			return ProfilerUnsafeUtility.CreateCounterValue_Unsafe(out counterPtr, name, nameLen, categoryId, flags, dataType, dataUnit, dataSize, counterOptions);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* CreateCounterValue_Unsafe(out IntPtr counterPtr, char* name, int nameLen, ushort categoryId, MarkerFlags flags, byte dataType, byte dataUnit, int dataSize, ProfilerCounterOptions counterOptions);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void FlushCounterValue(void* counterValuePtr);

		internal unsafe static string Utf8ToString(byte* chars, int charsLen)
		{
			bool flag = chars == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				byte[] array = new byte[charsLen];
				Marshal.Copy((IntPtr)((void*)chars), array, 0, charsLen);
				result = Encoding.UTF8.GetString(array, 0, charsLen);
			}
			return result;
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint CreateFlow(ushort categoryId);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FlowEvent(uint flowId, ProfilerFlowEventType flowEventType);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_BeginWithObject(IntPtr markerPtr, UnityEngine.Object contextUnityObject);

		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string Internal_GetName(IntPtr markerPtr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCategoryDescription_Injected(ushort categoryId, out ProfilerCategoryDescription ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_TimestampToNanosecondsConversionRatio_Injected(out ProfilerUnsafeUtility.TimestampConversionRatio ret);
	}
}
