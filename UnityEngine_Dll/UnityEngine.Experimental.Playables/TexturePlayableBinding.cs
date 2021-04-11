using System;
using UnityEngine.Playables;

namespace UnityEngine.Experimental.Playables
{
	public static class TexturePlayableBinding
	{
		public static PlayableBinding Create(string name, UnityEngine.Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(RenderTexture), new PlayableBinding.CreateOutputMethod(TexturePlayableBinding.CreateTextureOutput));
		}

		private static PlayableOutput CreateTextureOutput(PlayableGraph graph, string name)
		{
			return TexturePlayableOutput.Create(graph, name, null);
		}
	}
}
