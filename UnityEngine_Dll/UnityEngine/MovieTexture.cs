using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true), ExcludeFromObjectFactory, ExcludeFromPreset]
	public sealed class MovieTexture : Texture
	{
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public AudioClip audioClip
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return null;
			}
		}

		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public bool loop
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return false;
			}
			set
			{
				MovieTexture.FeatureRemoved();
			}
		}

		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public bool isPlaying
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return false;
			}
		}

		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public bool isReadyToPlay
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return false;
			}
		}

		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public float duration
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return 1f;
			}
		}

		private static void FeatureRemoved()
		{
			throw new Exception("MovieTexture has been removed from Unity. Use VideoPlayer instead.");
		}

		private MovieTexture()
		{
		}

		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public void Play()
		{
			MovieTexture.FeatureRemoved();
		}

		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public void Stop()
		{
			MovieTexture.FeatureRemoved();
		}

		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public void Pause()
		{
			MovieTexture.FeatureRemoved();
		}
	}
}
