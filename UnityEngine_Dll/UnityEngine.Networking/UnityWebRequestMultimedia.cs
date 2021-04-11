using System;

namespace UnityEngine.Networking
{
	public static class UnityWebRequestMultimedia
	{
		public static UnityWebRequest GetAudioClip(string uri, AudioType audioType)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAudioClip(uri, audioType), null);
		}

		public static UnityWebRequest GetAudioClip(Uri uri, AudioType audioType)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAudioClip(uri, audioType), null);
		}

		[Obsolete("MovieTexture is deprecated. Use VideoPlayer instead.", true)]
		public static UnityWebRequest GetMovieTexture(string uri)
		{
			return null;
		}

		[Obsolete("MovieTexture is deprecated. Use VideoPlayer instead.", true)]
		public static UnityWebRequest GetMovieTexture(Uri uri)
		{
			return null;
		}
	}
}
