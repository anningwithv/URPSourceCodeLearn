using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	[NativeType(Header = "Modules/DSPGraph/Public/DSPSampleProvider.bindings.h")]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct DSPSampleProviderInternal
	{
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int Internal_ReadUInt8FromSampleProvider(void* provider, int format, void* buffer, int length);

		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int Internal_ReadSInt16FromSampleProvider(void* provider, int format, void* buffer, int length);

		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int Internal_ReadFloatFromSampleProvider(void* provider, void* buffer, int length);

		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern ushort Internal_GetChannelCount(void* provider);

		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern uint Internal_GetSampleRate(void* provider);

		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int Internal_ReadUInt8FromSampleProviderById(uint providerId, int format, void* buffer, int length);

		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int Internal_ReadSInt16FromSampleProviderById(uint providerId, int format, void* buffer, int length);

		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int Internal_ReadFloatFromSampleProviderById(uint providerId, void* buffer, int length);

		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ushort Internal_GetChannelCountById(uint providerId);

		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint Internal_GetSampleRateById(uint providerId);
	}
}
