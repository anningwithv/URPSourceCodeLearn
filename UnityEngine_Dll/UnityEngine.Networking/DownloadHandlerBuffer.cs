using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandlerBuffer.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerBuffer : DownloadHandler
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(DownloadHandlerBuffer obj);

		private void InternalCreateBuffer()
		{
			this.m_Ptr = DownloadHandlerBuffer.Create(this);
		}

		public DownloadHandlerBuffer()
		{
			this.InternalCreateBuffer();
		}

		protected override byte[] GetData()
		{
			return this.InternalGetData();
		}

		private byte[] InternalGetData()
		{
			return DownloadHandler.InternalGetByteArray(this);
		}

		public static string GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerBuffer>(www).text;
		}
	}
}
