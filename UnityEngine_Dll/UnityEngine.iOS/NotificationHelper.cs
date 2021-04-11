using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.iOS
{
	[NativeHeader("PlatformDependent/iPhonePlayer/Notifications.h")]
	internal sealed class NotificationHelper
	{
		[FreeFunction("NotificationScripting::CreateLocal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr CreateLocal();

		[NativeMethod(Name = "NotificationScripting::DestroyLocal", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DestroyLocal(IntPtr target);

		[NativeMethod(Name = "NotificationScripting::DestroyRemote", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DestroyRemote(IntPtr target);
	}
}
