using System;
using UnityEngine.Playables;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public interface IAnimationWindowPreview
	{
		void StartPreview();

		void StopPreview();

		void UpdatePreviewGraph(PlayableGraph graph);

		Playable BuildPreviewGraph(PlayableGraph graph, Playable inputPlayable);
	}
}
