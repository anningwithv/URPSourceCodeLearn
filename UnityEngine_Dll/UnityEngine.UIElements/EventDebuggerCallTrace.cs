using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	internal class EventDebuggerCallTrace : EventDebuggerTrace
	{
		public int callbackHashCode
		{
			[CompilerGenerated]
			get
			{
				return this.<callbackHashCode>k__BackingField;
			}
		}

		public string callbackName
		{
			[CompilerGenerated]
			get
			{
				return this.<callbackName>k__BackingField;
			}
		}

		public bool propagationHasStopped
		{
			[CompilerGenerated]
			get
			{
				return this.<propagationHasStopped>k__BackingField;
			}
		}

		public bool immediatePropagationHasStopped
		{
			[CompilerGenerated]
			get
			{
				return this.<immediatePropagationHasStopped>k__BackingField;
			}
		}

		public bool defaultHasBeenPrevented
		{
			[CompilerGenerated]
			get
			{
				return this.<defaultHasBeenPrevented>k__BackingField;
			}
		}

		public EventDebuggerCallTrace(IPanel panel, EventBase evt, int cbHashCode, string cbName, bool propagationHasStopped, bool immediatePropagationHasStopped, bool defaultHasBeenPrevented, long duration, IEventHandler mouseCapture) : base(panel, evt, duration, mouseCapture)
		{
			this.<callbackHashCode>k__BackingField = cbHashCode;
			this.<callbackName>k__BackingField = cbName;
			this.<propagationHasStopped>k__BackingField = propagationHasStopped;
			this.<immediatePropagationHasStopped>k__BackingField = immediatePropagationHasStopped;
			this.<defaultHasBeenPrevented>k__BackingField = defaultHasBeenPrevented;
		}
	}
}
