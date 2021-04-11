using System;

namespace UnityEngine.UIElements
{
	public enum PropagationPhase
	{
		None,
		TrickleDown,
		AtTarget,
		DefaultActionAtTarget = 5,
		BubbleUp = 3,
		DefaultAction
	}
}
