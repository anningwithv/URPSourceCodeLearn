using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandler.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class UploadHandler : IDisposable
	{
		[NonSerialized]
		internal IntPtr m_Ptr;

		public byte[] data
		{
			get
			{
				return this.GetData();
			}
		}

		public string contentType
		{
			get
			{
				return this.GetContentType();
			}
			set
			{
				this.SetContentType(value);
			}
		}

		public float progress
		{
			get
			{
				return this.GetProgress();
			}
		}

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Release();

		internal UploadHandler()
		{
		}

		~UploadHandler()
		{
			this.Dispose();
		}

		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Release();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		internal virtual byte[] GetData()
		{
			return null;
		}

		internal virtual string GetContentType()
		{
			return this.InternalGetContentType();
		}

		internal virtual void SetContentType(string newContentType)
		{
			this.InternalSetContentType(newContentType);
		}

		internal virtual float GetProgress()
		{
			return this.InternalGetProgress();
		}

		[NativeMethod("GetContentType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string InternalGetContentType();

		[NativeMethod("SetContentType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetContentType(string newContentType);

		[NativeMethod("GetProgress")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float InternalGetProgress();
	}
}
