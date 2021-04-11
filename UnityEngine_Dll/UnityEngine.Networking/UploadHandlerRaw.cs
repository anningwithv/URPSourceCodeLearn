using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandlerRaw.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class UploadHandlerRaw : UploadHandler
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(UploadHandlerRaw self, byte[] data);

		public UploadHandlerRaw(byte[] data)
		{
			bool flag = data != null && data.Length == 0;
			if (flag)
			{
				throw new ArgumentException("Cannot create a data handler without payload data");
			}
			this.m_Ptr = UploadHandlerRaw.Create(this, data);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern byte[] InternalGetData();

		internal override byte[] GetData()
		{
			return this.InternalGetData();
		}
	}
}
