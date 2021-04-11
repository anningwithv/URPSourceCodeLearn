using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	internal class EventDebuggerTrace
	{
		public EventDebuggerEventRecord eventBase
		{
			[CompilerGenerated]
			get
			{
				return this.<eventBase>k__BackingField;
			}
		}

		public IEventHandler focusedElement
		{
			[CompilerGenerated]
			get
			{
				return this.<focusedElement>k__BackingField;
			}
		}

		public IEventHandler mouseCapture
		{
			[CompilerGenerated]
			get
			{
				return this.<mouseCapture>k__BackingField;
			}
		}

		public long duration
		{
			get;
			set;
		}

		public EventDebuggerTrace(IPanel panel, EventBase evt, long duration, IEventHandler mouseCapture)
		{
			this.<eventBase>k__BackingField = new EventDebuggerEventRecord(evt);
			IEventHandler arg_2D_1;
			if (panel == null)
			{
				arg_2D_1 = null;
			}
			else
			{
				FocusController expr_21 = panel.focusController;
				arg_2D_1 = ((expr_21 != null) ? expr_21.focusedElement : null);
			}
			this.<focusedElement>k__BackingField = arg_2D_1;
			this.<mouseCapture>k__BackingField = mouseCapture;
			this.duration = duration;
		}
	}
}
