using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandler.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class DownloadHandler : IDisposable
	{
		[VisibleToOtherModules]
		[NonSerialized]
		internal IntPtr m_Ptr;

		public bool isDone
		{
			get
			{
				return this.IsDone();
			}
		}

		public string error
		{
			get
			{
				return this.GetErrorMsg();
			}
		}

		public byte[] data
		{
			get
			{
				return this.GetData();
			}
		}

		public string text
		{
			get
			{
				return this.GetText();
			}
		}

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Release();

		[VisibleToOtherModules]
		internal DownloadHandler()
		{
		}

		~DownloadHandler()
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

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsDone();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetErrorMsg();

		protected virtual byte[] GetData()
		{
			return null;
		}

		protected virtual string GetText()
		{
			byte[] data = this.GetData();
			bool flag = data != null && data.Length != 0;
			string result;
			if (flag)
			{
				result = this.GetTextEncoder().GetString(data, 0, data.Length);
			}
			else
			{
				result = "";
			}
			return result;
		}

		private Encoding GetTextEncoder()
		{
			string contentType = this.GetContentType();
			bool flag = !string.IsNullOrEmpty(contentType);
			Encoding result;
			if (flag)
			{
				int num = contentType.IndexOf("charset", StringComparison.OrdinalIgnoreCase);
				bool flag2 = num > -1;
				if (flag2)
				{
					int num2 = contentType.IndexOf('=', num);
					bool flag3 = num2 > -1;
					if (flag3)
					{
						string text = contentType.Substring(num2 + 1).Trim().Trim(new char[]
						{
							'\'',
							'"'
						}).Trim();
						int num3 = text.IndexOf(';');
						bool flag4 = num3 > -1;
						if (flag4)
						{
							text = text.Substring(0, num3);
						}
						try
						{
							result = Encoding.GetEncoding(text);
							return result;
						}
						catch (ArgumentException ex)
						{
							Debug.LogWarning(string.Format("Unsupported encoding '{0}': {1}", text, ex.Message));
						}
						catch (NotSupportedException ex2)
						{
							Debug.LogWarning(string.Format("Unsupported encoding '{0}': {1}", text, ex2.Message));
						}
					}
				}
			}
			result = Encoding.UTF8;
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetContentType();

		[UsedByNativeCode]
		protected virtual bool ReceiveData(byte[] data, int dataLength)
		{
			return true;
		}

		[UsedByNativeCode]
		protected virtual void ReceiveContentLengthHeader(ulong contentLength)
		{
			this.ReceiveContentLength((int)contentLength);
		}

		[Obsolete("Use ReceiveContentLengthHeader")]
		protected virtual void ReceiveContentLength(int contentLength)
		{
		}

		[UsedByNativeCode]
		protected virtual void CompleteContent()
		{
		}

		[UsedByNativeCode]
		protected virtual float GetProgress()
		{
			return 0f;
		}

		protected static T GetCheckedDownloader<T>(UnityWebRequest www) where T : DownloadHandler
		{
			bool flag = www == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get content from a null UnityWebRequest object");
			}
			bool flag2 = !www.isDone;
			if (flag2)
			{
				throw new InvalidOperationException("Cannot get content from an unfinished UnityWebRequest object");
			}
			bool flag3 = www.result == UnityWebRequest.Result.ProtocolError;
			if (flag3)
			{
				throw new InvalidOperationException(www.error);
			}
			return (T)((object)www.downloadHandler);
		}

		[NativeThrows, VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern byte[] InternalGetByteArray(DownloadHandler dh);
	}
}
