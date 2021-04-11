using System;
using UnityEngine.Playables;

namespace UnityEngine.Audio
{
	public static class AudioPlayableBinding
	{
		public static PlayableBinding Create(string name, UnityEngine.Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(AudioSource), new PlayableBinding.CreateOutputMethod(AudioPlayableBinding.CreateAudioOutput));
		}

		private static PlayableOutput CreateAudioOutput(PlayableGraph graph, string name)
		{
			return AudioPlayableOutput.Create(graph, name, null);
		}
	}
}
