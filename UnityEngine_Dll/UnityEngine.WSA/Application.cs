using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.WSA
{
	[NativeHeader("Runtime/Export/WSA/WSAApplication.bindings.h"), StaticAccessor("WSAApplicationBindings", StaticAccessorType.DoubleColon)]
	public sealed class Application
	{
		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event WindowSizeChanged windowSizeChanged;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event WindowActivated windowActivated;

		public static string arguments
		{
			get
			{
				return Application.GetAppArguments();
			}
		}

		public static string advertisingIdentifier
		{
			get
			{
				string advertisingIdentifier = Application.GetAdvertisingIdentifier();
				UnityEngine.Application.InvokeOnAdvertisingIdentifierCallback(advertisingIdentifier, true);
				return advertisingIdentifier;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetAdvertisingIdentifier();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetAppArguments();

		[RequiredByNativeCode]
		internal static void InvokeWindowSizeChangedEvent(int width, int height)
		{
			bool flag = Application.windowSizeChanged != null;
			if (flag)
			{
				Application.windowSizeChanged(width, height);
			}
		}

		[RequiredByNativeCode]
		internal static void InvokeWindowActivatedEvent(WindowActivationState state)
		{
			bool flag = Application.windowActivated != null;
			if (flag)
			{
				Application.windowActivated(state);
			}
		}

		public static void InvokeOnAppThread(AppCallbackItem item, bool waitUntilDone)
		{
			item();
		}

		public static void InvokeOnUIThread(AppCallbackItem item, bool waitUntilDone)
		{
			item();
		}

		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalInvokeOnAppThread(object item, bool waitUntilDone);

		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalInvokeOnUIThread(object item, bool waitUntilDone);

		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RunningOnAppThread();

		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RunningOnUIThread();
	}
}
