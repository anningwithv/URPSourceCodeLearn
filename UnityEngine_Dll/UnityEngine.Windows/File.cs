using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Windows
{
	public static class File
	{
		[NativeHeader("PlatformDependent/MetroPlayer/Bindings/WindowsFileBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte[] ReadAllBytes(string path);

		[NativeHeader("PlatformDependent/MetroPlayer/Bindings/WindowsFileBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WriteAllBytes(string path, byte[] bytes);

		[NativeHeader("PlatformDependent/MetroPlayer/Bindings/WindowsFileBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Exists(string path);

		[NativeHeader("PlatformDependent/MetroPlayer/Bindings/WindowsFileBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Delete(string path);
	}
}
