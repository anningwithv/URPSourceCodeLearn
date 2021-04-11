using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.WSA
{
	[NativeConditional("PLATFORM_METRO"), NativeHeader("PlatformDependent/MetroPlayer/MetroLauncher.h"), NativeHeader("Runtime/Export/WSA/WSALauncher.bindings.h"), StaticAccessor("metro::Launcher", StaticAccessorType.DoubleColon)]
	public sealed class Launcher
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LaunchFile(Folder folder, string relativeFilePath, bool showWarning);

		public static void LaunchFileWithPicker(string fileExtension)
		{
			Process.Start("explorer.exe");
		}

		public static void LaunchUri(string uri, bool showWarning)
		{
			Process.Start(uri);
		}

		[NativeMethod("LaunchFileWithPicker")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalLaunchFileWithPicker(string fileExtension);

		[NativeMethod("LaunchUri")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalLaunchUri(string uri, bool showWarning);
	}
}
