using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting
{
	[NativeHeader("Runtime/Scripting/GarbageCollector.h")]
	public static class GarbageCollector
	{
		public enum Mode
		{
			Disabled,
			Enabled,
			Manual
		}

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<GarbageCollector.Mode> GCModeChanged;

		public static GarbageCollector.Mode GCMode
		{
			get
			{
				return GarbageCollector.GetMode();
			}
			set
			{
				bool flag = value == GarbageCollector.GetMode();
				if (!flag)
				{
					GarbageCollector.SetMode(value);
					bool flag2 = GarbageCollector.GCModeChanged != null;
					if (flag2)
					{
						GarbageCollector.GCModeChanged(value);
					}
				}
			}
		}

		public static extern bool isIncremental
		{
			[NativeMethod("GetIncrementalEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ulong incrementalTimeSliceNanoseconds
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMode(GarbageCollector.Mode mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GarbageCollector.Mode GetMode();

		[NativeMethod("CollectIncrementalWrapper"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CollectIncremental(ulong nanoseconds = 0uL);
	}
}
