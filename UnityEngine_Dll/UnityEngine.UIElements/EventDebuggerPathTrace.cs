using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	internal class EventDebuggerPathTrace : EventDebuggerTrace
	{
		public PropagationPaths paths
		{
			[CompilerGenerated]
			get
			{
				return this.<paths>k__BackingField;
			}
		}

		public EventDebuggerPathTrace(IPanel panel, EventBase evt, PropagationPaths paths) : base(panel, evt, -1L, null)
		{
			this.<paths>k__BackingField = paths;
		}
	}
}
