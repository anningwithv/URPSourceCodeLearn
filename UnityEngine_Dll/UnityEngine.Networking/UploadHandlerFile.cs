using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandlerFile.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class UploadHandlerFile : UploadHandler
	{
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(UploadHandlerFile self, string filePath);

		public UploadHandlerFile(string filePath)
		{
			this.m_Ptr = UploadHandlerFile.Create(this, filePath);
		}
	}
}
