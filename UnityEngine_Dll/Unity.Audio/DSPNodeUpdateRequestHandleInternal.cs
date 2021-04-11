using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Jobs;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	[NativeType(Header = "Modules/DSPGraph/Public/DSPNodeUpdateRequest.bindings.h")]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct DSPNodeUpdateRequestHandleInternal
	{
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* Internal_GetUpdateJobData(ref Handle graph, ref Handle requestHandle);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Internal_HasError(ref Handle graph, ref Handle requestHandle);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_GetDSPNode(ref Handle graph, ref Handle requestHandle, ref Handle node);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_GetFence(ref Handle graph, ref Handle requestHandle, ref JobHandle fence);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_Dispose(ref Handle graph, ref Handle requestHandle);
	}
}
