using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	internal class EventDebuggerDefaultActionTrace : EventDebuggerTrace
	{
		public PropagationPhase phase
		{
			[CompilerGenerated]
			get
			{
				return this.<phase>k__BackingField;
			}
		}

		public string targetName
		{
			get
			{
				return base.eventBase.target.GetType().FullName;
			}
		}

		public EventDebuggerDefaultActionTrace(IPanel panel, EventBase evt, PropagationPhase phase, long duration, IEventHandler mouseCapture) : base(panel, evt, duration, mouseCapture)
		{
			this.<phase>k__BackingField = phase;
		}
	}
}
