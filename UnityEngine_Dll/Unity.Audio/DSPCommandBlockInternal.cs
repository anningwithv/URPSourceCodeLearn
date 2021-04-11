using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	[NativeHeader("Modules/DSPGraph/Public/DSPSampleProvider.bindings.h"), NativeType(Header = "Modules/DSPGraph/Public/DSPCommandBlock.bindings.h")]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct DSPCommandBlockInternal
	{
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_CreateDSPNode(ref Handle graph, ref Handle block, ref Handle node, void* jobReflectionData, void* jobMemory, void* parameterDescriptionArray, int parameterCount, void* sampleProviderDescriptionArray, int sampleProviderCount);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_SetFloat(ref Handle graph, ref Handle block, ref Handle node, void* jobReflectionData, uint pIndex, float value, uint interpolationLength);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_AddFloatKey(ref Handle graph, ref Handle block, ref Handle node, void* jobReflectionData, uint pIndex, ulong dspClock, float value);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_SustainFloat(ref Handle graph, ref Handle block, ref Handle node, void* jobReflectionData, uint pIndex, ulong dspClock);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_UpdateAudioJob(ref Handle graph, ref Handle block, ref Handle node, void* updateJobMem, void* updateJobReflectionData, void* nodeReflectionData);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_CreateUpdateRequest(ref Handle graph, ref Handle block, ref Handle node, ref Handle request, object callback, void* updateJobMem, void* updateJobReflectionData, void* nodeReflectionData);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_ReleaseDSPNode(ref Handle graph, ref Handle block, ref Handle node);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_Connect(ref Handle graph, ref Handle block, ref Handle output, int outputPort, ref Handle input, int inputPort, ref Handle connection);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_Disconnect(ref Handle graph, ref Handle block, ref Handle output, int outputPort, ref Handle input, int inputPort);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_DisconnectByHandle(ref Handle graph, ref Handle block, ref Handle connection);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_SetAttenuation(ref Handle graph, ref Handle block, ref Handle connection, void* value, byte dimension, uint interpolationLength);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_AddAttenuationKey(ref Handle graph, ref Handle block, ref Handle connection, ulong dspClock, void* value, byte dimension);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_SustainAttenuation(ref Handle graph, ref Handle block, ref Handle connection, ulong dspClock);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_AddInletPort(ref Handle graph, ref Handle block, ref Handle node, int channelCount, int format);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_AddOutletPort(ref Handle graph, ref Handle block, ref Handle node, int channelCount, int format);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_SetSampleProvider(ref Handle graph, ref Handle block, ref Handle node, int item, int index, uint audioSampleProviderId, bool destroyOnRemove);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_InsertSampleProvider(ref Handle graph, ref Handle block, ref Handle node, int item, int index, uint audioSampleProviderId, bool destroyOnRemove);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_RemoveSampleProvider(ref Handle graph, ref Handle block, ref Handle node, int item, int index);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_Complete(ref Handle graph, ref Handle block);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_Cancel(ref Handle graph, ref Handle block);
	}
}
