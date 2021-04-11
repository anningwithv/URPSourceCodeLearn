using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEngine.Networking;

namespace UnityEngine
{
	[Obsolete("Use UnityWebRequest, a fully featured replacement which is more efficient and has additional features")]
	public class WWW : CustomYieldInstruction, IDisposable
	{
		private UnityWebRequest _uwr;

		private AssetBundle _assetBundle;

		private Dictionary<string, string> _responseHeaders;

		public AssetBundle assetBundle
		{
			get
			{
				bool flag = this._assetBundle == null;
				AssetBundle result;
				if (flag)
				{
					bool flag2 = !this.WaitUntilDoneIfPossible();
					if (flag2)
					{
						result = null;
						return result;
					}
					bool flag3 = this._uwr.result == UnityWebRequest.Result.ConnectionError;
					if (flag3)
					{
						result = null;
						return result;
					}
					DownloadHandlerAssetBundle downloadHandlerAssetBundle = this._uwr.downloadHandler as DownloadHandlerAssetBundle;
					bool flag4 = downloadHandlerAssetBundle != null;
					if (flag4)
					{
						this._assetBundle = downloadHandlerAssetBundle.assetBundle;
					}
					else
					{
						byte[] bytes = this.bytes;
						bool flag5 = bytes == null;
						if (flag5)
						{
							result = null;
							return result;
						}
						this._assetBundle = AssetBundle.LoadFromMemory(bytes);
					}
				}
				result = this._assetBundle;
				return result;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Obsolete msg (UnityUpgradable) -> * UnityEngine.WWW.GetAudioClip()", true)]
		public Object audioClip
		{
			get
			{
				return null;
			}
		}

		public byte[] bytes
		{
			get
			{
				bool flag = !this.WaitUntilDoneIfPossible();
				byte[] result;
				if (flag)
				{
					result = new byte[0];
				}
				else
				{
					bool flag2 = this._uwr.result == UnityWebRequest.Result.ConnectionError;
					if (flag2)
					{
						result = new byte[0];
					}
					else
					{
						DownloadHandler downloadHandler = this._uwr.downloadHandler;
						bool flag3 = downloadHandler == null;
						if (flag3)
						{
							result = new byte[0];
						}
						else
						{
							result = downloadHandler.data;
						}
					}
				}
				return result;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Obsolete msg (UnityUpgradable) -> * UnityEngine.WWW.GetMovieTexture()", true)]
		public Object movie
		{
			get
			{
				return null;
			}
		}

		[Obsolete("WWW.size is obsolete. Please use WWW.bytesDownloaded instead")]
		public int size
		{
			get
			{
				return this.bytesDownloaded;
			}
		}

		public int bytesDownloaded
		{
			get
			{
				return (int)this._uwr.downloadedBytes;
			}
		}

		public string error
		{
			get
			{
				bool flag = !this._uwr.isDone;
				string result;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = this._uwr.result == UnityWebRequest.Result.ConnectionError;
					if (flag2)
					{
						result = this._uwr.error;
					}
					else
					{
						bool flag3 = this._uwr.responseCode >= 400L;
						if (flag3)
						{
							string hTTPStatusString = UnityWebRequest.GetHTTPStatusString(this._uwr.responseCode);
							result = string.Format("{0} {1}", this._uwr.responseCode, hTTPStatusString);
						}
						else
						{
							result = null;
						}
					}
				}
				return result;
			}
		}

		public bool isDone
		{
			get
			{
				return this._uwr.isDone;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Obsolete msg (UnityUpgradable) -> * UnityEngine.WWW.GetAudioClip()", true)]
		public Object oggVorbis
		{
			get
			{
				return null;
			}
		}

		public float progress
		{
			get
			{
				float num = this._uwr.downloadProgress;
				bool flag = num < 0f;
				if (flag)
				{
					num = 0f;
				}
				return num;
			}
		}

		public Dictionary<string, string> responseHeaders
		{
			get
			{
				bool flag = !this.isDone;
				Dictionary<string, string> result;
				if (flag)
				{
					result = new Dictionary<string, string>();
				}
				else
				{
					bool flag2 = this._responseHeaders == null;
					if (flag2)
					{
						this._responseHeaders = this._uwr.GetResponseHeaders();
						bool flag3 = this._responseHeaders != null;
						if (flag3)
						{
							string hTTPStatusString = UnityWebRequest.GetHTTPStatusString(this._uwr.responseCode);
							this._responseHeaders["STATUS"] = string.Format("HTTP/1.1 {0} {1}", this._uwr.responseCode, hTTPStatusString);
						}
						else
						{
							this._responseHeaders = new Dictionary<string, string>();
						}
					}
					result = this._responseHeaders;
				}
				return result;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Please use WWW.text instead. (UnityUpgradable) -> text", true)]
		public string data
		{
			get
			{
				return this.text;
			}
		}

		public string text
		{
			get
			{
				bool flag = !this.WaitUntilDoneIfPossible();
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					bool flag2 = this._uwr.result == UnityWebRequest.Result.ConnectionError;
					if (flag2)
					{
						result = "";
					}
					else
					{
						DownloadHandler downloadHandler = this._uwr.downloadHandler;
						bool flag3 = downloadHandler == null;
						if (flag3)
						{
							result = "";
						}
						else
						{
							result = downloadHandler.text;
						}
					}
				}
				return result;
			}
		}

		public Texture2D texture
		{
			get
			{
				return this.CreateTextureFromDownloadedData(false);
			}
		}

		public Texture2D textureNonReadable
		{
			get
			{
				return this.CreateTextureFromDownloadedData(true);
			}
		}

		public ThreadPriority threadPriority
		{
			get;
			set;
		}

		public float uploadProgress
		{
			get
			{
				float num = this._uwr.uploadProgress;
				bool flag = num < 0f;
				if (flag)
				{
					num = 0f;
				}
				return num;
			}
		}

		public string url
		{
			get
			{
				return this._uwr.url;
			}
		}

		public override bool keepWaiting
		{
			get
			{
				return this._uwr != null && !this._uwr.isDone;
			}
		}

		public static string EscapeURL(string s)
		{
			return WWW.EscapeURL(s, Encoding.UTF8);
		}

		public static string EscapeURL(string s, Encoding e)
		{
			return UnityWebRequest.EscapeURL(s, e);
		}

		public static string UnEscapeURL(string s)
		{
			return WWW.UnEscapeURL(s, Encoding.UTF8);
		}

		public static string UnEscapeURL(string s, Encoding e)
		{
			return UnityWebRequest.UnEscapeURL(s, e);
		}

		public static WWW LoadFromCacheOrDownload(string url, int version)
		{
			return WWW.LoadFromCacheOrDownload(url, version, 0u);
		}

		public static WWW LoadFromCacheOrDownload(string url, int version, uint crc)
		{
			Hash128 hash = new Hash128(0u, 0u, 0u, (uint)version);
			return WWW.LoadFromCacheOrDownload(url, hash, crc);
		}

		public static WWW LoadFromCacheOrDownload(string url, Hash128 hash)
		{
			return WWW.LoadFromCacheOrDownload(url, hash, 0u);
		}

		public static WWW LoadFromCacheOrDownload(string url, Hash128 hash, uint crc)
		{
			return new WWW(url, "", hash, crc);
		}

		public static WWW LoadFromCacheOrDownload(string url, CachedAssetBundle cachedBundle, uint crc = 0u)
		{
			return new WWW(url, cachedBundle.name, cachedBundle.hash, crc);
		}

		public WWW(string url)
		{
			this._uwr = UnityWebRequest.Get(url);
			this._uwr.SendWebRequest();
		}

		public WWW(string url, WWWForm form)
		{
			this._uwr = UnityWebRequest.Post(url, form);
			this._uwr.chunkedTransfer = false;
			this._uwr.SendWebRequest();
		}

		public WWW(string url, byte[] postData)
		{
			this._uwr = new UnityWebRequest(url, "POST");
			this._uwr.chunkedTransfer = false;
			UploadHandler uploadHandler = new UploadHandlerRaw(postData);
			uploadHandler.contentType = "application/x-www-form-urlencoded";
			this._uwr.uploadHandler = uploadHandler;
			this._uwr.downloadHandler = new DownloadHandlerBuffer();
			this._uwr.SendWebRequest();
		}

		[Obsolete("This overload is deprecated. Use UnityEngine.WWW.WWW(string, byte[], System.Collections.Generic.Dictionary<string, string>) instead.")]
		public WWW(string url, byte[] postData, Hashtable headers)
		{
			string method = (postData == null) ? "GET" : "POST";
			this._uwr = new UnityWebRequest(url, method);
			this._uwr.chunkedTransfer = false;
			UploadHandler uploadHandler = new UploadHandlerRaw(postData);
			uploadHandler.contentType = "application/x-www-form-urlencoded";
			this._uwr.uploadHandler = uploadHandler;
			this._uwr.downloadHandler = new DownloadHandlerBuffer();
			foreach (object current in headers.Keys)
			{
				this._uwr.SetRequestHeader((string)current, (string)headers[current]);
			}
			this._uwr.SendWebRequest();
		}

		public WWW(string url, byte[] postData, Dictionary<string, string> headers)
		{
			string method = (postData == null) ? "GET" : "POST";
			this._uwr = new UnityWebRequest(url, method);
			this._uwr.chunkedTransfer = false;
			UploadHandler uploadHandler = new UploadHandlerRaw(postData);
			uploadHandler.contentType = "application/x-www-form-urlencoded";
			this._uwr.uploadHandler = uploadHandler;
			this._uwr.downloadHandler = new DownloadHandlerBuffer();
			foreach (KeyValuePair<string, string> current in headers)
			{
				this._uwr.SetRequestHeader(current.Key, current.Value);
			}
			this._uwr.SendWebRequest();
		}

		internal WWW(string url, string name, Hash128 hash, uint crc)
		{
			this._uwr = UnityWebRequestAssetBundle.GetAssetBundle(url, new CachedAssetBundle(name, hash), crc);
			this._uwr.SendWebRequest();
		}

		private Texture2D CreateTextureFromDownloadedData(bool markNonReadable)
		{
			bool flag = !this.WaitUntilDoneIfPossible();
			Texture2D result;
			if (flag)
			{
				result = new Texture2D(2, 2);
			}
			else
			{
				bool flag2 = this._uwr.result == UnityWebRequest.Result.ConnectionError;
				if (flag2)
				{
					result = null;
				}
				else
				{
					DownloadHandler downloadHandler = this._uwr.downloadHandler;
					bool flag3 = downloadHandler == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						Texture2D texture2D = new Texture2D(2, 2);
						texture2D.LoadImage(downloadHandler.data, markNonReadable);
						result = texture2D;
					}
				}
			}
			return result;
		}

		public void LoadImageIntoTexture(Texture2D texture)
		{
			bool flag = !this.WaitUntilDoneIfPossible();
			if (!flag)
			{
				bool flag2 = this._uwr.result == UnityWebRequest.Result.ConnectionError;
				if (flag2)
				{
					Debug.LogError("Cannot load image: download failed");
				}
				else
				{
					DownloadHandler downloadHandler = this._uwr.downloadHandler;
					bool flag3 = downloadHandler == null;
					if (flag3)
					{
						Debug.LogError("Cannot load image: internal error");
					}
					else
					{
						texture.LoadImage(downloadHandler.data, false);
					}
				}
			}
		}

		public void Dispose()
		{
			bool flag = this._uwr != null;
			if (flag)
			{
				this._uwr.Dispose();
				this._uwr = null;
			}
		}

		internal Object GetAudioClipInternal(bool threeD, bool stream, bool compressed, AudioType audioType)
		{
			return WebRequestWWW.InternalCreateAudioClipUsingDH(this._uwr.downloadHandler, this._uwr.url, stream, compressed, audioType);
		}

		public AudioClip GetAudioClip()
		{
			return this.GetAudioClip(true, false, AudioType.UNKNOWN);
		}

		public AudioClip GetAudioClip(bool threeD)
		{
			return this.GetAudioClip(threeD, false, AudioType.UNKNOWN);
		}

		public AudioClip GetAudioClip(bool threeD, bool stream)
		{
			return this.GetAudioClip(threeD, stream, AudioType.UNKNOWN);
		}

		public AudioClip GetAudioClip(bool threeD, bool stream, AudioType audioType)
		{
			return (AudioClip)this.GetAudioClipInternal(threeD, stream, false, audioType);
		}

		public AudioClip GetAudioClipCompressed()
		{
			return this.GetAudioClipCompressed(false, AudioType.UNKNOWN);
		}

		public AudioClip GetAudioClipCompressed(bool threeD)
		{
			return this.GetAudioClipCompressed(threeD, AudioType.UNKNOWN);
		}

		public AudioClip GetAudioClipCompressed(bool threeD, AudioType audioType)
		{
			return (AudioClip)this.GetAudioClipInternal(threeD, false, true, audioType);
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("MovieTexture is deprecated. Use VideoPlayer instead.", false)]
		public MovieTexture GetMovieTexture()
		{
			throw new Exception("MovieTexture has been removed from Unity. Use VideoPlayer instead.");
		}

		private bool WaitUntilDoneIfPossible()
		{
			bool isDone = this._uwr.isDone;
			bool result;
			if (isDone)
			{
				result = true;
			}
			else
			{
				bool flag = this.url.StartsWith("file://", StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					while (!this._uwr.isDone)
					{
					}
					result = true;
				}
				else
				{
					Debug.LogError("You are trying to load data from a www stream which has not completed the download yet.\nYou need to yield the download or wait until isDone returns true.");
					result = false;
				}
			}
			return result;
		}
	}
}
