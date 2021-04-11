using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	[NativeType(Header = "Modules/DSPGraph/Public/AudioOutputHookManager.bindings.h")]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct AudioOutputHookManager
	{
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_CreateAudioOutputHook(out Handle outputHook, void* jobReflectionData, void* jobData);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_DisposeAudioOutputHook(ref Handle outputHook);
	}
}
