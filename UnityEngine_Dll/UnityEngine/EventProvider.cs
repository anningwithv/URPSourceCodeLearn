using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/ETW/EventProvider.bindings.h")]
	public sealed class EventProvider
	{
		[FreeFunction("EventProvider_Bindings::WriteCustomEvent")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WriteCustomEvent(int value, string text);
	}
}
