using System;

namespace UnityEngine.Playables
{
	public static class ScriptPlayableBinding
	{
		public static PlayableBinding Create(string name, UnityEngine.Object key, Type type)
		{
			return PlayableBinding.CreateInternal(name, key, type, new PlayableBinding.CreateOutputMethod(ScriptPlayableBinding.CreateScriptOutput));
		}

		private static PlayableOutput CreateScriptOutput(PlayableGraph graph, string name)
		{
			return ScriptPlayableOutput.Create(graph, name);
		}
	}
}
