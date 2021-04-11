using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	[DebuggerDisplay("Count = {Count}"), DebuggerTypeProxy(typeof(ProfilerRecorderDebugView)), NativeHeader("Runtime/Profiler/ScriptBindings/ProfilerRecorder.bindings.h"), UsedByNativeCode]
	public struct ProfilerRecorder : IDisposable
	{
		internal enum ControlOptions
		{
			Start,
			Stop,
			Reset,
			Release = 4
		}

		internal enum CountOptions
		{
			Count,
			MaxCount
		}

		internal ulong handle;

		public bool Valid
		{
			get
			{
				return this.handle != 0uL && ProfilerRecorder.GetValid(this);
			}
		}

		public ProfilerMarkerDataType DataType
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetValueDataType(this);
			}
		}

		public ProfilerMarkerDataUnit UnitType
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetValueUnitType(this);
			}
		}

		public long CurrentValue
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCurrentValue(this);
			}
		}

		public double CurrentValueAsDouble
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCurrentValueAsDouble(this);
			}
		}

		public long LastValue
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetLastValue(this);
			}
		}

		public double LastValueAsDouble
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetLastValueAsDouble(this);
			}
		}

		public int Capacity
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCount(this, ProfilerRecorder.CountOptions.MaxCount);
			}
		}

		public int Count
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCount(this, ProfilerRecorder.CountOptions.Count);
			}
		}

		public bool IsRunning
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetRunning(this);
			}
		}

		public bool WrappedAround
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetWrapped(this);
			}
		}

		internal ProfilerRecorder(ProfilerRecorderOptions options)
		{
			this = ProfilerRecorder.Create(default(ProfilerRecorderHandle), 0, options);
		}

		public ProfilerRecorder(string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = new ProfilerRecorder(ProfilerCategory.Any, statName, capacity, options);
		}

		public ProfilerRecorder(string categoryName, string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = new ProfilerRecorder(new ProfilerCategory(categoryName), statName, capacity, options);
		}

		public ProfilerRecorder(ProfilerCategory category, string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			ProfilerRecorderHandle byName = ProfilerRecorderHandle.GetByName(category, statName);
			this = ProfilerRecorder.Create(byName, capacity, options);
		}

		public unsafe ProfilerRecorder(ProfilerCategory category, char* statName, int statNameLen, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			ProfilerRecorderHandle byName = ProfilerRecorderHandle.GetByName(category, statName, statNameLen);
			this = ProfilerRecorder.Create(byName, capacity, options);
		}

		public ProfilerRecorder(ProfilerMarker marker, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = ProfilerRecorder.Create(ProfilerRecorderHandle.Get(marker), capacity, options);
		}

		public ProfilerRecorder(ProfilerRecorderHandle statHandle, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = ProfilerRecorder.Create(statHandle, capacity, options);
		}

		public unsafe static ProfilerRecorder StartNew(ProfilerCategory category, string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			char* ptr = statName;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return new ProfilerRecorder(category, ptr, statName.Length, capacity, options | ProfilerRecorderOptions.StartImmediately);
		}

		public static ProfilerRecorder StartNew(ProfilerMarker marker, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			return new ProfilerRecorder(marker, capacity, options | ProfilerRecorderOptions.StartImmediately);
		}

		internal static ProfilerRecorder StartNew()
		{
			return ProfilerRecorder.Create(default(ProfilerRecorderHandle), 0, ProfilerRecorderOptions.StartImmediately);
		}

		public void Start()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Start);
		}

		public void Stop()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Stop);
		}

		public void Reset()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Reset);
		}

		public ProfilerRecorderSample GetSample(int index)
		{
			this.CheckInitializedAndThrow();
			return ProfilerRecorder.GetSampleInternal(this, index);
		}

		public void CopyTo(List<ProfilerRecorderSample> outSamples, bool reset = false)
		{
			bool flag = outSamples == null;
			if (flag)
			{
				throw new ArgumentNullException("outSamples");
			}
			this.CheckInitializedAndThrow();
			ProfilerRecorder.CopyTo_List(this, outSamples, reset);
		}

		public unsafe int CopyTo(ProfilerRecorderSample* dest, int destSize, bool reset = false)
		{
			this.CheckInitializedWithParamsAndThrow(dest);
			return ProfilerRecorder.CopyTo_Pointer(this, dest, destSize, reset);
		}

		public unsafe ProfilerRecorderSample[] ToArray()
		{
			this.CheckInitializedAndThrow();
			int count = this.Count;
			ProfilerRecorderSample[] array = new ProfilerRecorderSample[count];
			ProfilerRecorderSample[] array2;
			ProfilerRecorderSample* outSamples;
			if ((array2 = array) == null || array2.Length == 0)
			{
				outSamples = null;
			}
			else
			{
				outSamples = &array2[0];
			}
			ProfilerRecorder.CopyTo_Pointer(this, outSamples, count, false);
			array2 = null;
			return array;
		}

		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		private static ProfilerRecorder Create(ProfilerRecorderHandle statHandle, int maxSampleCount, ProfilerRecorderOptions options)
		{
			ProfilerRecorder result;
			ProfilerRecorder.Create_Injected(ref statHandle, maxSampleCount, options, out result);
			return result;
		}

		[NativeMethod(IsThreadSafe = true)]
		private static void Control(ProfilerRecorder handle, ProfilerRecorder.ControlOptions options)
		{
			ProfilerRecorder.Control_Injected(ref handle, options);
		}

		[NativeMethod(IsThreadSafe = true)]
		private static ProfilerMarkerDataUnit GetValueUnitType(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetValueUnitType_Injected(ref handle);
		}

		[NativeMethod(IsThreadSafe = true)]
		private static ProfilerMarkerDataType GetValueDataType(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetValueDataType_Injected(ref handle);
		}

		[NativeMethod(IsThreadSafe = true)]
		private static long GetCurrentValue(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetCurrentValue_Injected(ref handle);
		}

		[NativeMethod(IsThreadSafe = true)]
		private static double GetCurrentValueAsDouble(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetCurrentValueAsDouble_Injected(ref handle);
		}

		[NativeMethod(IsThreadSafe = true)]
		private static long GetLastValue(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetLastValue_Injected(ref handle);
		}

		[NativeMethod(IsThreadSafe = true)]
		private static double GetLastValueAsDouble(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetLastValueAsDouble_Injected(ref handle);
		}

		[NativeMethod(IsThreadSafe = true)]
		private static int GetCount(ProfilerRecorder handle, ProfilerRecorder.CountOptions countOptions)
		{
			return ProfilerRecorder.GetCount_Injected(ref handle, countOptions);
		}

		[NativeMethod(IsThreadSafe = true)]
		private static bool GetValid(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetValid_Injected(ref handle);
		}

		[NativeMethod(IsThreadSafe = true)]
		private static bool GetWrapped(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetWrapped_Injected(ref handle);
		}

		[NativeMethod(IsThreadSafe = true)]
		private static bool GetRunning(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetRunning_Injected(ref handle);
		}

		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		private static ProfilerRecorderSample GetSampleInternal(ProfilerRecorder handle, int index)
		{
			ProfilerRecorderSample result;
			ProfilerRecorder.GetSampleInternal_Injected(ref handle, index, out result);
			return result;
		}

		[NativeMethod(IsThreadSafe = true)]
		private static void CopyTo_List(ProfilerRecorder handle, List<ProfilerRecorderSample> outSamples, bool reset)
		{
			ProfilerRecorder.CopyTo_List_Injected(ref handle, outSamples, reset);
		}

		[NativeMethod(IsThreadSafe = true)]
		private unsafe static int CopyTo_Pointer(ProfilerRecorder handle, ProfilerRecorderSample* outSamples, int outSamplesSize, bool reset)
		{
			return ProfilerRecorder.CopyTo_Pointer_Injected(ref handle, outSamples, outSamplesSize, reset);
		}

		public void Dispose()
		{
			bool flag = this.handle == 0uL;
			if (!flag)
			{
				ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Release);
				this.handle = 0uL;
			}
		}

		[BurstDiscard]
		private unsafe void CheckInitializedWithParamsAndThrow(ProfilerRecorderSample* dest)
		{
			bool flag = this.handle == 0uL;
			if (flag)
			{
				throw new InvalidOperationException("ProfilerRecorder object is not initialized or has been disposed.");
			}
			bool flag2 = dest == null;
			if (flag2)
			{
				throw new ArgumentNullException("dest");
			}
		}

		[BurstDiscard]
		private void CheckInitializedAndThrow()
		{
			bool flag = this.handle == 0uL;
			if (flag)
			{
				throw new InvalidOperationException("ProfilerRecorder object is not initialized or has been disposed.");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Create_Injected(ref ProfilerRecorderHandle statHandle, int maxSampleCount, ProfilerRecorderOptions options, out ProfilerRecorder ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Control_Injected(ref ProfilerRecorder handle, ProfilerRecorder.ControlOptions options);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ProfilerMarkerDataUnit GetValueUnitType_Injected(ref ProfilerRecorder handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ProfilerMarkerDataType GetValueDataType_Injected(ref ProfilerRecorder handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetCurrentValue_Injected(ref ProfilerRecorder handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetCurrentValueAsDouble_Injected(ref ProfilerRecorder handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetLastValue_Injected(ref ProfilerRecorder handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetLastValueAsDouble_Injected(ref ProfilerRecorder handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCount_Injected(ref ProfilerRecorder handle, ProfilerRecorder.CountOptions countOptions);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetValid_Injected(ref ProfilerRecorder handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetWrapped_Injected(ref ProfilerRecorder handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetRunning_Injected(ref ProfilerRecorder handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSampleInternal_Injected(ref ProfilerRecorder handle, int index, out ProfilerRecorderSample ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTo_List_Injected(ref ProfilerRecorder handle, List<ProfilerRecorderSample> outSamples, bool reset);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int CopyTo_Pointer_Injected(ref ProfilerRecorder handle, ProfilerRecorderSample* outSamples, int outSamplesSize, bool reset);
	}
}
