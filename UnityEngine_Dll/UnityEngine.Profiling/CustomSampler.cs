using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	[NativeHeader("Runtime/Profiler/Marker.h"), NativeHeader("Runtime/Profiler/ScriptBindings/Sampler.bindings.h"), UsedByNativeCode]
	public sealed class CustomSampler : Sampler
	{
		internal static CustomSampler s_InvalidCustomSampler = new CustomSampler();

		internal CustomSampler()
		{
		}

		internal CustomSampler(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		public static CustomSampler Create(string name, bool collectGpuData = false)
		{
			IntPtr intPtr = CustomSampler.CreateInternal(name, collectGpuData);
			bool flag = intPtr == IntPtr.Zero;
			CustomSampler result;
			if (flag)
			{
				result = CustomSampler.s_InvalidCustomSampler;
			}
			else
			{
				result = new CustomSampler(intPtr);
			}
			return result;
		}

		[NativeMethod(Name = "ProfilerBindings::CreateCustomSamplerInternal", IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateInternal([NotNull("ArgumentNullException")] string name, bool collectGpuData);

		[Conditional("ENABLE_PROFILER")]
		public void Begin()
		{
			CustomSampler.Begin_Internal(this.m_Ptr);
		}

		[Conditional("ENABLE_PROFILER")]
		public void Begin(UnityEngine.Object targetObject)
		{
			CustomSampler.BeginWithObject_Internal(this.m_Ptr, targetObject);
		}

		[Conditional("ENABLE_PROFILER")]
		public void End()
		{
			CustomSampler.End_Internal(this.m_Ptr);
		}

		[NativeMethod(Name = "ProfilerBindings::CustomSampler_Begin", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Begin_Internal(IntPtr ptr);

		[NativeMethod(Name = "ProfilerBindings::CustomSampler_BeginWithObject", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginWithObject_Internal(IntPtr ptr, UnityEngine.Object targetObject);

		[NativeMethod(Name = "ProfilerBindings::CustomSampler_End", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void End_Internal(IntPtr ptr);
	}
}
