using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	[NativeHeader("Runtime/VirtualFileSystem/VirtualFileSystem.h")]
	public static class VirtualFileSystem
	{
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetLocalFileSystemName(string vfsFileName, out string localFileName, out ulong localFileOffset, out ulong localFileSize);
	}
}
