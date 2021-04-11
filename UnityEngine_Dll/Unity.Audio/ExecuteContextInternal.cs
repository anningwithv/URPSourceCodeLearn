using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	[NativeType(Header = "Modules/DSPGraph/Public/ExecuteContext.bindings.h")]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct ExecuteContextInternal
	{
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_PostEvent(void* dspNodePtr, long eventTypeHashCode, void* eventPtr, int eventSize);
	}
}
