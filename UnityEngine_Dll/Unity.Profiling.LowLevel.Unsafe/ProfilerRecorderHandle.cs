using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling.LowLevel.Unsafe
{
	[IsReadOnly, UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct ProfilerRecorderHandle
	{
		private const ulong k_InvalidHandle = 18446744073709551615uL;

		[FieldOffset(0)]
		internal readonly ulong handle;

		public bool Valid
		{
			get
			{
				return this.handle != 0uL && this.handle != 18446744073709551615uL;
			}
		}

		internal ProfilerRecorderHandle(ulong handle)
		{
			this.handle = handle;
		}

		internal static ProfilerRecorderHandle Get(ProfilerMarker marker)
		{
			return new ProfilerRecorderHandle((ulong)marker.Handle.ToInt64());
		}

		internal static ProfilerRecorderHandle Get(ProfilerCategory category, string statName)
		{
			bool flag = string.IsNullOrEmpty(statName);
			if (flag)
			{
				throw new ArgumentException("String must be not null or empty", "statName");
			}
			return ProfilerRecorderHandle.GetByName(category, statName);
		}

		public static ProfilerRecorderDescription GetDescription(ProfilerRecorderHandle handle)
		{
			bool flag = !handle.Valid;
			if (flag)
			{
				throw new ArgumentException("ProfilerRecorderHandle is not initialized or is not available", "handle");
			}
			return ProfilerRecorderHandle.GetDescriptionInternal(handle);
		}

		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetAvailable(List<ProfilerRecorderHandle> outRecorderHandleList);

		[NativeMethod(IsThreadSafe = true)]
		internal static ProfilerRecorderHandle GetByName(ProfilerCategory category, string name)
		{
			ProfilerRecorderHandle result;
			ProfilerRecorderHandle.GetByName_Injected(ref category, name, out result);
			return result;
		}

		[NativeMethod(IsThreadSafe = true)]
		internal unsafe static ProfilerRecorderHandle GetByName__Unmanaged(ProfilerCategory category, byte* name, int nameLen)
		{
			ProfilerRecorderHandle result;
			ProfilerRecorderHandle.GetByName__Unmanaged_Injected(ref category, name, nameLen, out result);
			return result;
		}

		[MethodImpl((MethodImplOptions)256)]
		internal unsafe static ProfilerRecorderHandle GetByName(ProfilerCategory category, char* name, int nameLen)
		{
			return ProfilerRecorderHandle.GetByName_Unsafe(category, name, nameLen);
		}

		[NativeMethod(IsThreadSafe = true)]
		private unsafe static ProfilerRecorderHandle GetByName_Unsafe(ProfilerCategory category, char* name, int nameLen)
		{
			ProfilerRecorderHandle result;
			ProfilerRecorderHandle.GetByName_Unsafe_Injected(ref category, name, nameLen, out result);
			return result;
		}

		[NativeMethod(IsThreadSafe = true)]
		private static ProfilerRecorderDescription GetDescriptionInternal(ProfilerRecorderHandle handle)
		{
			ProfilerRecorderDescription result;
			ProfilerRecorderHandle.GetDescriptionInternal_Injected(ref handle, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetByName_Injected(ref ProfilerCategory category, string name, out ProfilerRecorderHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GetByName__Unmanaged_Injected(ref ProfilerCategory category, byte* name, int nameLen, out ProfilerRecorderHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GetByName_Unsafe_Injected(ref ProfilerCategory category, char* name, int nameLen, out ProfilerRecorderHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDescriptionInternal_Injected(ref ProfilerRecorderHandle handle, out ProfilerRecorderDescription ret);
	}
}
