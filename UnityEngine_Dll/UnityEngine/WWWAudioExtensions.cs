using System;

namespace UnityEngine
{
	public static class WWWAudioExtensions
	{
		[Obsolete("WWWAudioExtensions.GetAudioClip extension method has been replaced by WWW.GetAudioClip instance method. (UnityUpgradable) -> WWW.GetAudioClip()", true)]
		public static AudioClip GetAudioClip(this WWW www)
		{
			return www.GetAudioClip();
		}

		[Obsolete("WWWAudioExtensions.GetAudioClip extension method has been replaced by WWW.GetAudioClip instance method. (UnityUpgradable) -> WWW.GetAudioClip([mscorlib] System.Boolean)", true)]
		public static AudioClip GetAudioClip(this WWW www, bool threeD)
		{
			return www.GetAudioClip(threeD);
		}

		[Obsolete("WWWAudioExtensions.GetAudioClip extension method has been replaced by WWW.GetAudioClip instance method. (UnityUpgradable) -> WWW.GetAudioClip([mscorlib] System.Boolean, [mscorlib] System.Boolean)", true)]
		public static AudioClip GetAudioClip(this WWW www, bool threeD, bool stream)
		{
			return www.GetAudioClip(threeD, stream);
		}

		[Obsolete("WWWAudioExtensions.GetAudioClip extension method has been replaced by WWW.GetAudioClip instance method. (UnityUpgradable) -> WWW.GetAudioClip([mscorlib] System.Boolean, [mscorlib] System.Boolean, UnityEngine.AudioType)", true)]
		public static AudioClip GetAudioClip(this WWW www, bool threeD, bool stream, AudioType audioType)
		{
			return www.GetAudioClip(threeD, stream, audioType);
		}

		[Obsolete("WWWAudioExtensions.GetAudioClipCompressed extension method has been replaced by WWW.GetAudioClipCompressed instance method. (UnityUpgradable) -> WWW.GetAudioClipCompressed()", true)]
		public static AudioClip GetAudioClipCompressed(this WWW www)
		{
			return www.GetAudioClipCompressed();
		}

		[Obsolete("WWWAudioExtensions.GetAudioClipCompressed extension method has been replaced by WWW.GetAudioClipCompressed instance method. (UnityUpgradable) -> WWW.GetAudioClipCompressed([mscorlib] System.Boolean)", true)]
		public static AudioClip GetAudioClipCompressed(this WWW www, bool threeD)
		{
			return www.GetAudioClipCompressed(threeD);
		}

		[Obsolete("WWWAudioExtensions.GetAudioClipCompressed extension method has been replaced by WWW.GetAudioClipCompressed instance method. (UnityUpgradable) -> WWW.GetAudioClipCompressed([mscorlib] System.Boolean, UnityEngine.AudioType)", true)]
		public static AudioClip GetAudioClipCompressed(this WWW www, bool threeD, AudioType audioType)
		{
			return www.GetAudioClipCompressed(threeD, audioType);
		}

		[Obsolete("WWWAudioExtensions.GetMovieTexture extension method has been replaced by WWW.GetMovieTexture instance method. (UnityUpgradable) -> WWW.GetMovieTexture()", true)]
		public static MovieTexture GetMovieTexture(this WWW www)
		{
			return www.GetMovieTexture();
		}
	}
}
