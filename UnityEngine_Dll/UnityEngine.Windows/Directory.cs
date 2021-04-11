using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Windows
{
	public static class Directory
	{
		[NativeHeader("Runtime/Export/Windows/WindowsDirectoryBindings.h")]
		public static extern string temporaryFolder
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeHeader("Runtime/Export/Windows/WindowsDirectoryBindings.h")]
		public static extern string localFolder
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeHeader("Runtime/Export/Windows/WindowsDirectoryBindings.h")]
		public static extern string roamingFolder
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeHeader("Runtime/Export/Windows/WindowsDirectoryBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CreateDirectory(string path);

		[NativeHeader("Runtime/Export/Windows/WindowsDirectoryBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Exists(string path);

		[NativeHeader("Runtime/Export/Windows/WindowsDirectoryBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Delete(string path);
	}
}
