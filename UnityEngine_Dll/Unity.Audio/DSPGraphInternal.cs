using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Jobs;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	[NativeType(Header = "Modules/DSPGraph/Public/DSPGraph.bindings.h")]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct DSPGraphInternal
	{
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_CreateDSPGraph(out Handle graph, int outputFormat, uint outputChannels, uint dspBufferSize, uint sampleRate);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_DisposeDSPGraph(ref Handle graph);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_CreateDSPCommandBlock(ref Handle graph, ref Handle block);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint Internal_AddNodeEventHandler(ref Handle graph, long eventTypeHashCode, object handler);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Internal_RemoveNodeEventHandler(ref Handle graph, uint handlerId);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_GetRootDSP(ref Handle graph, ref Handle root);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong Internal_GetDSPClock(ref Handle graph);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_BeginMix(ref Handle graph, int frameCount, int executionMode);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_ReadMix(ref Handle graph, void* buffer, int frameCount);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_Update(ref Handle graph);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Internal_AssertMixerThread(ref Handle graph);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Internal_AssertMainThread(ref Handle graph);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		public static Handle Internal_AllocateHandle(ref Handle graph)
		{
			Handle result;
			DSPGraphInternal.Internal_AllocateHandle_Injected(ref graph, out result);
			return result;
		}

		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_InitializeJob(void* jobStructData, void* jobReflectionData, void* resourceContext);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_ExecuteJob(void* jobStructData, void* jobReflectionData, void* jobData, void* resourceContext);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_ExecuteUpdateJob(void* updateStructMemory, void* updateReflectionData, void* jobStructMemory, void* jobReflectionData, void* resourceContext, ref Handle requestHandle, ref JobHandle fence);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_DisposeJob(void* jobStructData, void* jobReflectionData, void* resourceContext);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		public unsafe static void Internal_ScheduleGraph(JobHandle inputDeps, void* nodes, int nodeCount, int* childTable, void* dependencies)
		{
			DSPGraphInternal.Internal_ScheduleGraph_Injected(ref inputDeps, nodes, nodeCount, childTable, dependencies);
		}

		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		public static void Internal_SyncFenceNoWorkSteal(JobHandle handle)
		{
			DSPGraphInternal.Internal_SyncFenceNoWorkSteal_Injected(ref handle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_AllocateHandle_Injected(ref Handle graph, out Handle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_ScheduleGraph_Injected(ref JobHandle inputDeps, void* nodes, int nodeCount, int* childTable, void* dependencies);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SyncFenceNoWorkSteal_Injected(ref JobHandle handle);
	}
}
