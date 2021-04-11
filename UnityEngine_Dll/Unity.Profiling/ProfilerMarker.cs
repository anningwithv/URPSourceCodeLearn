using JetBrains.Annotations;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	[NativeHeader("Runtime/Profiler/ScriptBindings/ProfilerMarker.bindings.h"), UsedByNativeCode]
	public struct ProfilerMarker
	{
		[UsedByNativeCode]
		public struct AutoScope : IDisposable
		{
			[NativeDisableUnsafePtrRestriction]
			internal readonly IntPtr m_Ptr;

			[MethodImpl((MethodImplOptions)256)]
			internal AutoScope(IntPtr markerPtr)
			{
				this.m_Ptr = markerPtr;
				ProfilerUnsafeUtility.BeginSample(markerPtr);
			}

			[MethodImpl((MethodImplOptions)256)]
			public void Dispose()
			{
				ProfilerUnsafeUtility.EndSample(this.m_Ptr);
			}
		}

		[NativeDisableUnsafePtrRestriction]
		[NonSerialized]
		internal readonly IntPtr m_Ptr;

		public IntPtr Handle
		{
			get
			{
				return this.m_Ptr;
			}
		}

		[MethodImpl((MethodImplOptions)256)]
		public ProfilerMarker(string name)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, 1, MarkerFlags.Default, 0);
		}

		[MethodImpl((MethodImplOptions)256)]
		public unsafe ProfilerMarker(char* name, int nameLen)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, nameLen, 1, MarkerFlags.Default, 0);
		}

		[MethodImpl((MethodImplOptions)256)]
		public ProfilerMarker(ProfilerCategory category, string name)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, category, MarkerFlags.Default, 0);
		}

		[MethodImpl((MethodImplOptions)256)]
		public unsafe ProfilerMarker(ProfilerCategory category, char* name, int nameLen)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, nameLen, category, MarkerFlags.Default, 0);
		}

		[Pure, Conditional("ENABLE_PROFILER")]
		[MethodImpl((MethodImplOptions)256)]
		public void Begin()
		{
			ProfilerUnsafeUtility.BeginSample(this.m_Ptr);
		}

		[Conditional("ENABLE_PROFILER")]
		[MethodImpl((MethodImplOptions)256)]
		public void Begin(UnityEngine.Object contextUnityObject)
		{
			ProfilerUnsafeUtility.Internal_BeginWithObject(this.m_Ptr, contextUnityObject);
		}

		[Pure, Conditional("ENABLE_PROFILER")]
		[MethodImpl((MethodImplOptions)256)]
		public void End()
		{
			ProfilerUnsafeUtility.EndSample(this.m_Ptr);
		}

		[Conditional("ENABLE_PROFILER")]
		internal void GetName(ref string name)
		{
			name = ProfilerUnsafeUtility.Internal_GetName(this.m_Ptr);
		}

		[Pure]
		[MethodImpl((MethodImplOptions)256)]
		public ProfilerMarker.AutoScope Auto()
		{
			return new ProfilerMarker.AutoScope(this.m_Ptr);
		}
	}
}
