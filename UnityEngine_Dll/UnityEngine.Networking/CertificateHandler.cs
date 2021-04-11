using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	[NativeHeader("Modules/UnityWebRequest/Public/CertificateHandler/CertificateHandlerScript.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class CertificateHandler : IDisposable
	{
		[NonSerialized]
		internal IntPtr m_Ptr;

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(CertificateHandler obj);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Release();

		protected CertificateHandler()
		{
			this.m_Ptr = CertificateHandler.Create(this);
		}

		~CertificateHandler()
		{
			this.Dispose();
		}

		protected virtual bool ValidateCertificate(byte[] certificateData)
		{
			return false;
		}

		[RequiredByNativeCode]
		internal bool ValidateCertificateNative(byte[] certificateData)
		{
			return this.ValidateCertificate(certificateData);
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
	}
}
