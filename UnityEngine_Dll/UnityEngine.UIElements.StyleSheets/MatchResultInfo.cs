using System;

namespace UnityEngine.UIElements.StyleSheets
{
	internal struct MatchResultInfo
	{
		public readonly bool success;

		public readonly PseudoStates triggerPseudoMask;

		public readonly PseudoStates dependencyPseudoMask;

		public MatchResultInfo(bool success, PseudoStates triggerPseudoMask, PseudoStates dependencyPseudoMask)
		{
			this.success = success;
			this.triggerPseudoMask = triggerPseudoMask;
			this.dependencyPseudoMask = dependencyPseudoMask;
		}
	}
}
