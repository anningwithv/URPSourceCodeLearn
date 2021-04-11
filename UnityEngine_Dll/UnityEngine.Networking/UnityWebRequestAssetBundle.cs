using System;

namespace UnityEngine.Networking
{
	public static class UnityWebRequestAssetBundle
	{
		public static UnityWebRequest GetAssetBundle(string uri)
		{
			return UnityWebRequestAssetBundle.GetAssetBundle(uri, 0u);
		}

		public static UnityWebRequest GetAssetBundle(Uri uri)
		{
			return UnityWebRequestAssetBundle.GetAssetBundle(uri, 0u);
		}

		public static UnityWebRequest GetAssetBundle(string uri, uint crc)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAssetBundle(uri, crc), null);
		}

		public static UnityWebRequest GetAssetBundle(Uri uri, uint crc)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAssetBundle(uri.AbsoluteUri, crc), null);
		}

		public static UnityWebRequest GetAssetBundle(string uri, uint version, uint crc)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAssetBundle(uri, version, crc), null);
		}

		public static UnityWebRequest GetAssetBundle(Uri uri, uint version, uint crc)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAssetBundle(uri.AbsoluteUri, version, crc), null);
		}

		public static UnityWebRequest GetAssetBundle(string uri, Hash128 hash, uint crc = 0u)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAssetBundle(uri, hash, crc), null);
		}

		public static UnityWebRequest GetAssetBundle(Uri uri, Hash128 hash, uint crc = 0u)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAssetBundle(uri.AbsoluteUri, hash, crc), null);
		}

		public static UnityWebRequest GetAssetBundle(string uri, CachedAssetBundle cachedAssetBundle, uint crc = 0u)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAssetBundle(uri, cachedAssetBundle, crc), null);
		}

		public static UnityWebRequest GetAssetBundle(Uri uri, CachedAssetBundle cachedAssetBundle, uint crc = 0u)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAssetBundle(uri.AbsoluteUri, cachedAssetBundle, crc), null);
		}
	}
}
