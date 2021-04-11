using System;

namespace UnityEngine.UIElements
{
	internal struct EventDebuggerLogIMGUICall : IDisposable
	{
		private readonly EventBase m_Event;

		private readonly long m_Start;

		public EventDebuggerLogIMGUICall(EventBase evt)
		{
			this.m_Event = evt;
			this.m_Start = (long)(Time.realtimeSinceStartup * 1000f);
		}

		public void Dispose()
		{
			bool flag = this.m_Event != null && this.m_Event.log;
			if (flag)
			{
				VisualElement expr_2C = this.m_Event.target as VisualElement;
				IPanel panel = (expr_2C != null) ? expr_2C.panel : null;
				IEventHandler mouseCapture = (panel != null) ? panel.GetCapturingElement(PointerId.mousePointerId) : null;
				this.m_Event.eventLogger.LogIMGUICall(this.m_Event, (long)(Time.realtimeSinceStartup * 1000f) - this.m_Start, mouseCapture);
			}
		}
	}
}
