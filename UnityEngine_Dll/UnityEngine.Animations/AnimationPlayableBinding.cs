using System;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	public static class AnimationPlayableBinding
	{
		public static PlayableBinding Create(string name, UnityEngine.Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(Animator), new PlayableBinding.CreateOutputMethod(AnimationPlayableBinding.CreateAnimationOutput));
		}

		private static PlayableOutput CreateAnimationOutput(PlayableGraph graph, string name)
		{
			return AnimationPlayableOutput.Create(graph, name, null);
		}
	}
}
