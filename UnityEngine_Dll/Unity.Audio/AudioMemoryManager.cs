using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	[NativeType(Header = "Modules/DSPGraph/Public/AudioMemoryManager.bindings.h")]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct AudioMemoryManager
	{
		[NativeMethod(IsFreeFunction = true, ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* Internal_AllocateAudioMemory(int size, int alignment);

		[NativeMethod(IsFreeFunction = true, ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_FreeAudioMemory(void* memory);
	}
}
