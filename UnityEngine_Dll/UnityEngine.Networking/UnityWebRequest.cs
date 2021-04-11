using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine.Networking
{
	[NativeHeader("Modules/UnityWebRequest/Public/UnityWebRequest.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class UnityWebRequest : IDisposable
	{
		internal enum UnityWebRequestMethod
		{
			Get,
			Post,
			Put,
			Head,
			Custom
		}

		internal enum UnityWebRequestError
		{
			OK,
			Unknown,
			SDKError,
			UnsupportedProtocol,
			MalformattedUrl,
			CannotResolveProxy,
			CannotResolveHost,
			CannotConnectToHost,
			AccessDenied,
			GenericHttpError,
			WriteError,
			ReadError,
			OutOfMemory,
			Timeout,
			HTTPPostError,
			SSLCannotConnect,
			Aborted,
			TooManyRedirects,
			ReceivedNoData,
			SSLNotSupported,
			FailedToSendData,
			FailedToReceiveData,
			SSLCertificateError,
			SSLCipherNotAvailable,
			SSLCACertError,
			UnrecognizedContentEncoding,
			LoginFailed,
			SSLShutdownFailed,
			NoInternetConnection
		}

		public enum Result
		{
			InProgress,
			Success,
			ConnectionError,
			ProtocolError,
			DataProcessingError
		}

		[NonSerialized]
		internal IntPtr m_Ptr;

		[NonSerialized]
		internal DownloadHandler m_DownloadHandler;

		[NonSerialized]
		internal UploadHandler m_UploadHandler;

		[NonSerialized]
		internal CertificateHandler m_CertificateHandler;

		[NonSerialized]
		internal Uri m_Uri;

		public const string kHttpVerbGET = "GET";

		public const string kHttpVerbHEAD = "HEAD";

		public const string kHttpVerbPOST = "POST";

		public const string kHttpVerbPUT = "PUT";

		public const string kHttpVerbCREATE = "CREATE";

		public const string kHttpVerbDELETE = "DELETE";

		public bool disposeCertificateHandlerOnDispose
		{
			get;
			set;
		}

		public bool disposeDownloadHandlerOnDispose
		{
			get;
			set;
		}

		public bool disposeUploadHandlerOnDispose
		{
			get;
			set;
		}

		public string method
		{
			get
			{
				string result;
				switch (this.GetMethod())
				{
				case UnityWebRequest.UnityWebRequestMethod.Get:
					result = "GET";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Post:
					result = "POST";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Put:
					result = "PUT";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Head:
					result = "HEAD";
					break;
				default:
					result = this.GetCustomMethod();
					break;
				}
				return result;
			}
			set
			{
				bool flag = string.IsNullOrEmpty(value);
				if (flag)
				{
					throw new ArgumentException("Cannot set a UnityWebRequest's method to an empty or null string");
				}
				string text = value.ToUpper();
				string text2 = text;
				if (text2 != null)
				{
					if (text2 == "GET")
					{
						this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Get);
						return;
					}
					if (text2 == "POST")
					{
						this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Post);
						return;
					}
					if (text2 == "PUT")
					{
						this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Put);
						return;
					}
					if (text2 == "HEAD")
					{
						this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Head);
						return;
					}
				}
				this.InternalSetCustomMethod(value.ToUpper());
			}
		}

		public string error
		{
			get
			{
				UnityWebRequest.Result result = this.result;
				UnityWebRequest.Result result2 = result;
				string result3;
				if (result2 > UnityWebRequest.Result.Success)
				{
					if (result2 != UnityWebRequest.Result.ProtocolError)
					{
						result3 = UnityWebRequest.GetWebErrorString(this.GetError());
					}
					else
					{
						result3 = string.Format("HTTP/1.1 {0} {1}", this.responseCode, UnityWebRequest.GetHTTPStatusString(this.responseCode));
					}
				}
				else
				{
					result3 = null;
				}
				return result3;
			}
		}

		private extern bool use100Continue
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public bool useHttpContinue
		{
			get
			{
				return this.use100Continue;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent and its 100-Continue setting cannot be altered");
				}
				this.use100Continue = value;
			}
		}

		public string url
		{
			get
			{
				return this.GetUrl();
			}
			set
			{
				string localUrl = "http://localhost/";
				this.InternalSetUrl(WebRequestUtils.MakeInitialUrl(value, localUrl));
			}
		}

		public Uri uri
		{
			get
			{
				return new Uri(this.GetUrl());
			}
			set
			{
				bool flag = !value.IsAbsoluteUri;
				if (flag)
				{
					throw new ArgumentException("URI must be absolute");
				}
				this.InternalSetUrl(WebRequestUtils.MakeUriString(value, value.OriginalString, false));
				this.m_Uri = value;
			}
		}

		public extern long responseCode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public float uploadProgress
		{
			get
			{
				bool flag = !this.IsExecuting() && !this.isDone;
				float result;
				if (flag)
				{
					result = -1f;
				}
				else
				{
					result = this.GetUploadProgress();
				}
				return result;
			}
		}

		public extern bool isModifiable
		{
			[NativeMethod("IsModifiable")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public bool isDone
		{
			get
			{
				return this.result > UnityWebRequest.Result.InProgress;
			}
		}

		[Obsolete("UnityWebRequest.isNetworkError is deprecated. Use (UnityWebRequest.result == UnityWebRequest.Result.ConnectionError) instead.", false)]
		public bool isNetworkError
		{
			get
			{
				return this.result == UnityWebRequest.Result.ConnectionError;
			}
		}

		[Obsolete("UnityWebRequest.isHttpError is deprecated. Use (UnityWebRequest.result == UnityWebRequest.Result.ProtocolError) instead.", false)]
		public bool isHttpError
		{
			get
			{
				return this.result == UnityWebRequest.Result.ProtocolError;
			}
		}

		public extern UnityWebRequest.Result result
		{
			[NativeMethod("GetResult")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public float downloadProgress
		{
			get
			{
				bool flag = !this.IsExecuting() && !this.isDone;
				float result;
				if (flag)
				{
					result = -1f;
				}
				else
				{
					result = this.GetDownloadProgress();
				}
				return result;
			}
		}

		public extern ulong uploadedBytes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ulong downloadedBytes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public int redirectLimit
		{
			get
			{
				return this.GetRedirectLimit();
			}
			set
			{
				this.SetRedirectLimitFromScripting(value);
			}
		}

		[Obsolete("HTTP/2 and many HTTP/1.1 servers don't support this; we recommend leaving it set to false (default).", false)]
		public bool chunkedTransfer
		{
			get
			{
				return this.GetChunked();
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent and its chunked transfer encoding setting cannot be altered");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetChunked(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
			}
		}

		public UploadHandler uploadHandler
		{
			get
			{
				return this.m_UploadHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the upload handler");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetUploadHandler(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
				this.m_UploadHandler = value;
			}
		}

		public DownloadHandler downloadHandler
		{
			get
			{
				return this.m_DownloadHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the download handler");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetDownloadHandler(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
				this.m_DownloadHandler = value;
			}
		}

		public CertificateHandler certificateHandler
		{
			get
			{
				return this.m_CertificateHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the certificate handler");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetCertificateHandler(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
				this.m_CertificateHandler = value;
			}
		}

		public int timeout
		{
			get
			{
				return this.GetTimeoutMsec() / 1000;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the timeout");
				}
				value = Math.Max(value, 0);
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetTimeoutMsec(value * 1000);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
			}
		}

		internal bool suppressErrorsToConsole
		{
			get
			{
				return this.GetSuppressErrorsToConsole();
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the timeout");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetSuppressErrorsToConsole(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
			}
		}

		[Obsolete("UnityWebRequest.isError has been renamed to isNetworkError for clarity. (UnityUpgradable) -> isNetworkError", false)]
		public bool isError
		{
			get
			{
				return this.isNetworkError;
			}
		}

		[NativeConditional("ENABLE_UNITYWEBREQUEST"), NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetWebErrorString(UnityWebRequest.UnityWebRequestError err);

		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetHTTPStatusString(long responseCode);

		public static void ClearCookieCache()
		{
			UnityWebRequest.ClearCookieCache(null, null);
		}

		public static void ClearCookieCache(Uri uri)
		{
			bool flag = uri == null;
			if (flag)
			{
				UnityWebRequest.ClearCookieCache(null, null);
			}
			else
			{
				string host = uri.Host;
				string text = uri.AbsolutePath;
				bool flag2 = text == "/";
				if (flag2)
				{
					text = null;
				}
				UnityWebRequest.ClearCookieCache(host, text);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearCookieCache(string domain, string path);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Create();

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Release();

		internal void InternalDestroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Abort();
				this.Release();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		private void InternalSetDefaults()
		{
			this.disposeDownloadHandlerOnDispose = true;
			this.disposeUploadHandlerOnDispose = true;
			this.disposeCertificateHandlerOnDispose = true;
		}

		public UnityWebRequest()
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
		}

		public UnityWebRequest(string url)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
		}

		public UnityWebRequest(Uri uri)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
		}

		public UnityWebRequest(string url, string method)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
			this.method = method;
		}

		public UnityWebRequest(Uri uri, string method)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
			this.method = method;
		}

		public UnityWebRequest(string url, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
			this.method = method;
			this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;
		}

		public UnityWebRequest(Uri uri, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
			this.method = method;
			this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;
		}

		~UnityWebRequest()
		{
			this.DisposeHandlers();
			this.InternalDestroy();
		}

		public void Dispose()
		{
			this.DisposeHandlers();
			this.InternalDestroy();
			GC.SuppressFinalize(this);
		}

		private void DisposeHandlers()
		{
			bool disposeDownloadHandlerOnDispose = this.disposeDownloadHandlerOnDispose;
			if (disposeDownloadHandlerOnDispose)
			{
				DownloadHandler downloadHandler = this.downloadHandler;
				bool flag = downloadHandler != null;
				if (flag)
				{
					downloadHandler.Dispose();
				}
			}
			bool disposeUploadHandlerOnDispose = this.disposeUploadHandlerOnDispose;
			if (disposeUploadHandlerOnDispose)
			{
				UploadHandler uploadHandler = this.uploadHandler;
				bool flag2 = uploadHandler != null;
				if (flag2)
				{
					uploadHandler.Dispose();
				}
			}
			bool disposeCertificateHandlerOnDispose = this.disposeCertificateHandlerOnDispose;
			if (disposeCertificateHandlerOnDispose)
			{
				CertificateHandler certificateHandler = this.certificateHandler;
				bool flag3 = certificateHandler != null;
				if (flag3)
				{
					certificateHandler.Dispose();
				}
			}
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern UnityWebRequestAsyncOperation BeginWebRequest();

		[Obsolete("Use SendWebRequest.  It returns a UnityWebRequestAsyncOperation which contains a reference to the WebRequest object.", false)]
		public UnityEngine.AsyncOperation Send()
		{
			return this.SendWebRequest();
		}

		public UnityWebRequestAsyncOperation SendWebRequest()
		{
			UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = this.BeginWebRequest();
			bool flag = unityWebRequestAsyncOperation != null;
			if (flag)
			{
				unityWebRequestAsyncOperation.webRequest = this;
			}
			return unityWebRequestAsyncOperation;
		}

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Abort();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetMethod(UnityWebRequest.UnityWebRequestMethod methodType);

		internal void InternalSetMethod(UnityWebRequest.UnityWebRequestMethod methodType)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request method can no longer be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetMethod(methodType);
			bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetCustomMethod(string customMethodName);

		internal void InternalSetCustomMethod(string customMethodName)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request method can no longer be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetCustomMethod(customMethodName);
			bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern UnityWebRequest.UnityWebRequestMethod GetMethod();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string GetCustomMethod();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError GetError();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetUrl();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetUrl(string url);

		private void InternalSetUrl(string url)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its URL cannot be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetUrl(url);
			bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetUploadProgress();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsExecuting();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetDownloadProgress();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetRedirectLimit();

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetRedirectLimitFromScripting(int limit);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetChunked();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetChunked(bool chunked);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetRequestHeader(string name);

		[NativeMethod("SetRequestHeader")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern UnityWebRequest.UnityWebRequestError InternalSetRequestHeader(string name, string value);

		public void SetRequestHeader(string name, string value)
		{
			bool flag = string.IsNullOrEmpty(name);
			if (flag)
			{
				throw new ArgumentException("Cannot set a Request Header with a null or empty name");
			}
			bool flag2 = value == null;
			if (flag2)
			{
				throw new ArgumentException("Cannot set a Request header with a null");
			}
			bool flag3 = !this.isModifiable;
			if (flag3)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request headers cannot be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.InternalSetRequestHeader(name, value);
			bool flag4 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag4)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetResponseHeader(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string[] GetResponseHeaderKeys();

		public Dictionary<string, string> GetResponseHeaders()
		{
			string[] responseHeaderKeys = this.GetResponseHeaderKeys();
			bool flag = responseHeaderKeys == null || responseHeaderKeys.Length == 0;
			Dictionary<string, string> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(responseHeaderKeys.Length, StringComparer.OrdinalIgnoreCase);
				for (int i = 0; i < responseHeaderKeys.Length; i++)
				{
					string responseHeader = this.GetResponseHeader(responseHeaderKeys[i]);
					dictionary.Add(responseHeaderKeys[i], responseHeader);
				}
				result = dictionary;
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetUploadHandler(UploadHandler uh);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetDownloadHandler(DownloadHandler dh);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetCertificateHandler(CertificateHandler ch);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetTimeoutMsec();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetTimeoutMsec(int timeout);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetSuppressErrorsToConsole();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetSuppressErrorsToConsole(bool suppress);

		public static UnityWebRequest Get(string uri)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerBuffer(), null);
		}

		public static UnityWebRequest Get(Uri uri)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerBuffer(), null);
		}

		public static UnityWebRequest Delete(string uri)
		{
			return new UnityWebRequest(uri, "DELETE");
		}

		public static UnityWebRequest Delete(Uri uri)
		{
			return new UnityWebRequest(uri, "DELETE");
		}

		public static UnityWebRequest Head(string uri)
		{
			return new UnityWebRequest(uri, "HEAD");
		}

		public static UnityWebRequest Head(Uri uri)
		{
			return new UnityWebRequest(uri, "HEAD");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestTexture.GetTexture(*)", true)]
		public static UnityWebRequest GetTexture(string uri)
		{
			throw new NotSupportedException("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestTexture.GetTexture(*)", true)]
		public static UnityWebRequest GetTexture(string uri, bool nonReadable)
		{
			throw new NotSupportedException("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("UnityWebRequest.GetAudioClip is obsolete. Use UnityWebRequestMultimedia.GetAudioClip instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestMultimedia.GetAudioClip(*)", true)]
		public static UnityWebRequest GetAudioClip(string uri, AudioType audioType)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, uint crc)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, uint version, uint crc)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, Hash128 hash, uint crc)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, CachedAssetBundle cachedAssetBundle, uint crc)
		{
			return null;
		}

		public static UnityWebRequest Put(string uri, byte[] bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyData));
		}

		public static UnityWebRequest Put(Uri uri, byte[] bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyData));
		}

		public static UnityWebRequest Put(string uri, string bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyData)));
		}

		public static UnityWebRequest Put(Uri uri, string bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyData)));
		}

		public static UnityWebRequest Post(string uri, string postData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, postData);
			return unityWebRequest;
		}

		public static UnityWebRequest Post(Uri uri, string postData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, postData);
			return unityWebRequest;
		}

		private static void SetupPost(UnityWebRequest request, string postData)
		{
			byte[] data = null;
			bool flag = !string.IsNullOrEmpty(postData);
			if (flag)
			{
				string s = WWWTranscoder.DataEncode(postData, Encoding.UTF8);
				data = Encoding.UTF8.GetBytes(s);
			}
			request.uploadHandler = new UploadHandlerRaw(data);
			request.uploadHandler.contentType = "application/x-www-form-urlencoded";
			request.downloadHandler = new DownloadHandlerBuffer();
		}

		public static UnityWebRequest Post(string uri, WWWForm formData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formData);
			return unityWebRequest;
		}

		public static UnityWebRequest Post(Uri uri, WWWForm formData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formData);
			return unityWebRequest;
		}

		private static void SetupPost(UnityWebRequest request, WWWForm formData)
		{
			byte[] array = null;
			bool flag = formData != null;
			if (flag)
			{
				array = formData.data;
				bool flag2 = array.Length == 0;
				if (flag2)
				{
					array = null;
				}
			}
			request.uploadHandler = new UploadHandlerRaw(array);
			request.downloadHandler = new DownloadHandlerBuffer();
			bool flag3 = formData != null;
			if (flag3)
			{
				Dictionary<string, string> headers = formData.headers;
				foreach (KeyValuePair<string, string> current in headers)
				{
					request.SetRequestHeader(current.Key, current.Value);
				}
			}
		}

		public static UnityWebRequest Post(string uri, List<IMultipartFormSection> multipartFormSections)
		{
			byte[] boundary = UnityWebRequest.GenerateBoundary();
			return UnityWebRequest.Post(uri, multipartFormSections, boundary);
		}

		public static UnityWebRequest Post(Uri uri, List<IMultipartFormSection> multipartFormSections)
		{
			byte[] boundary = UnityWebRequest.GenerateBoundary();
			return UnityWebRequest.Post(uri, multipartFormSections, boundary);
		}

		public static UnityWebRequest Post(string uri, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, multipartFormSections, boundary);
			return unityWebRequest;
		}

		public static UnityWebRequest Post(Uri uri, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, multipartFormSections, boundary);
			return unityWebRequest;
		}

		private static void SetupPost(UnityWebRequest request, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			byte[] data = null;
			bool flag = multipartFormSections != null && multipartFormSections.Count != 0;
			if (flag)
			{
				data = UnityWebRequest.SerializeFormSections(multipartFormSections, boundary);
			}
			request.uploadHandler = new UploadHandlerRaw(data)
			{
				contentType = "multipart/form-data; boundary=" + Encoding.UTF8.GetString(boundary, 0, boundary.Length)
			};
			request.downloadHandler = new DownloadHandlerBuffer();
		}

		public static UnityWebRequest Post(string uri, Dictionary<string, string> formFields)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formFields);
			return unityWebRequest;
		}

		public static UnityWebRequest Post(Uri uri, Dictionary<string, string> formFields)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formFields);
			return unityWebRequest;
		}

		private static void SetupPost(UnityWebRequest request, Dictionary<string, string> formFields)
		{
			byte[] data = null;
			bool flag = formFields != null && formFields.Count != 0;
			if (flag)
			{
				data = UnityWebRequest.SerializeSimpleForm(formFields);
			}
			request.uploadHandler = new UploadHandlerRaw(data)
			{
				contentType = "application/x-www-form-urlencoded"
			};
			request.downloadHandler = new DownloadHandlerBuffer();
		}

		public static string EscapeURL(string s)
		{
			return UnityWebRequest.EscapeURL(s, Encoding.UTF8);
		}

		public static string EscapeURL(string s, Encoding e)
		{
			bool flag = s == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = s == "";
				if (flag2)
				{
					result = "";
				}
				else
				{
					bool flag3 = e == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						byte[] bytes = e.GetBytes(s);
						byte[] bytes2 = WWWTranscoder.URLEncode(bytes);
						result = e.GetString(bytes2);
					}
				}
			}
			return result;
		}

		public static string UnEscapeURL(string s)
		{
			return UnityWebRequest.UnEscapeURL(s, Encoding.UTF8);
		}

		public static string UnEscapeURL(string s, Encoding e)
		{
			bool flag = s == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = s.IndexOf('%') == -1 && s.IndexOf('+') == -1;
				if (flag2)
				{
					result = s;
				}
				else
				{
					byte[] bytes = e.GetBytes(s);
					byte[] bytes2 = WWWTranscoder.URLDecode(bytes);
					result = e.GetString(bytes2);
				}
			}
			return result;
		}

		public static byte[] SerializeFormSections(List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			bool flag = multipartFormSections == null || multipartFormSections.Count == 0;
			byte[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				byte[] bytes = Encoding.UTF8.GetBytes("\r\n");
				byte[] bytes2 = WWWForm.DefaultEncoding.GetBytes("--");
				int num = 0;
				foreach (IMultipartFormSection current in multipartFormSections)
				{
					num += 64 + current.sectionData.Length;
				}
				List<byte> list = new List<byte>(num);
				foreach (IMultipartFormSection current2 in multipartFormSections)
				{
					string str = "form-data";
					string sectionName = current2.sectionName;
					string fileName = current2.fileName;
					string text = "Content-Disposition: " + str;
					bool flag2 = !string.IsNullOrEmpty(sectionName);
					if (flag2)
					{
						text = text + "; name=\"" + sectionName + "\"";
					}
					bool flag3 = !string.IsNullOrEmpty(fileName);
					if (flag3)
					{
						text = text + "; filename=\"" + fileName + "\"";
					}
					text += "\r\n";
					string contentType = current2.contentType;
					bool flag4 = !string.IsNullOrEmpty(contentType);
					if (flag4)
					{
						text = text + "Content-Type: " + contentType + "\r\n";
					}
					list.AddRange(bytes);
					list.AddRange(bytes2);
					list.AddRange(boundary);
					list.AddRange(bytes);
					list.AddRange(Encoding.UTF8.GetBytes(text));
					list.AddRange(bytes);
					list.AddRange(current2.sectionData);
				}
				list.AddRange(bytes);
				list.AddRange(bytes2);
				list.AddRange(boundary);
				list.AddRange(bytes2);
				list.AddRange(bytes);
				result = list.ToArray();
			}
			return result;
		}

		public static byte[] GenerateBoundary()
		{
			byte[] array = new byte[40];
			for (int i = 0; i < 40; i++)
			{
				int num = UnityEngine.Random.Range(48, 110);
				bool flag = num > 57;
				if (flag)
				{
					num += 7;
				}
				bool flag2 = num > 90;
				if (flag2)
				{
					num += 6;
				}
				array[i] = (byte)num;
			}
			return array;
		}

		public static byte[] SerializeSimpleForm(Dictionary<string, string> formFields)
		{
			string text = "";
			foreach (KeyValuePair<string, string> current in formFields)
			{
				bool flag = text.Length > 0;
				if (flag)
				{
					text += "&";
				}
				text = text + WWWTranscoder.DataEncode(current.Key) + "=" + WWWTranscoder.DataEncode(current.Value);
			}
			return Encoding.UTF8.GetBytes(text);
		}
	}
}
