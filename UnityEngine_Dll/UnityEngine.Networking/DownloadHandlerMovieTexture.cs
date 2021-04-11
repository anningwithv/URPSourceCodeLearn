using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Networking
{
	[Obsolete("MovieTexture is deprecated. Use VideoPlayer instead.", true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerMovieTexture : DownloadHandler
	{
		public MovieTexture movieTexture
		{
			get
			{
				DownloadHandlerMovieTexture.FeatureRemoved();
				return null;
			}
		}

		public DownloadHandlerMovieTexture()
		{
			DownloadHandlerMovieTexture.FeatureRemoved();
		}

		protected override byte[] GetData()
		{
			DownloadHandlerMovieTexture.FeatureRemoved();
			return null;
		}

		protected override string GetText()
		{
			throw new NotSupportedException("String access is not supported for movies");
		}

		public static MovieTexture GetContent(UnityWebRequest uwr)
		{
			DownloadHandlerMovieTexture.FeatureRemoved();
			return null;
		}

		private static void FeatureRemoved()
		{
			throw new Exception("Movie texture has been removed, use VideoPlayer instead");
		}
	}
}
