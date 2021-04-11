using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandlerScript.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class DownloadHandlerScript : DownloadHandler
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(DownloadHandlerScript obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreatePreallocated(DownloadHandlerScript obj, byte[] preallocatedBuffer);

		private void InternalCreateScript()
		{
			this.m_Ptr = DownloadHandlerScript.Create(this);
		}

		private void InternalCreateScript(byte[] preallocatedBuffer)
		{
			this.m_Ptr = DownloadHandlerScript.CreatePreallocated(this, preallocatedBuffer);
		}

		public DownloadHandlerScript()
		{
			this.InternalCreateScript();
		}

		public DownloadHandlerScript(byte[] preallocatedBuffer)
		{
			bool flag = preallocatedBuffer == null || preallocatedBuffer.Length < 1;
			if (flag)
			{
				throw new ArgumentException("Cannot create a preallocated-buffer DownloadHandlerScript backed by a null or zero-length array");
			}
			this.InternalCreateScript(preallocatedBuffer);
		}
	}
}
