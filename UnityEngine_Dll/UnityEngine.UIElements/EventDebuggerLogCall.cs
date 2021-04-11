using System;

namespace UnityEngine.UIElements
{
	internal struct EventDebuggerLogCall : IDisposable
	{
		private readonly Delegate m_Callback;

		private readonly EventBase m_Event;

		private readonly long m_Start;

		private readonly bool m_IsPropagationStopped;

		private readonly bool m_IsImmediatePropagationStopped;

		private readonly bool m_IsDefaultPrevented;

		public EventDebuggerLogCall(Delegate callback, EventBase evt)
		{
			this.m_Callback = callback;
			this.m_Event = evt;
			this.m_Start = (long)(Time.realtimeSinceStartup * 1000f);
			this.m_IsPropagationStopped = evt.isPropagationStopped;
			this.m_IsImmediatePropagationStopped = evt.isImmediatePropagationStopped;
			this.m_IsDefaultPrevented = evt.isDefaultPrevented;
		}

		public void Dispose()
		{
			bool flag = this.m_Event != null && this.m_Event.log;
			if (flag)
			{
				VisualElement expr_2F = this.m_Event.target as VisualElement;
				IPanel panel = (expr_2F != null) ? expr_2F.panel : null;
				IEventHandler mouseCapture = (panel != null) ? panel.GetCapturingElement(PointerId.mousePointerId) : null;
				this.m_Event.eventLogger.LogCall(this.GetCallbackHashCode(), this.GetCallbackName(), this.m_Event, this.m_IsPropagationStopped != this.m_Event.isPropagationStopped, this.m_IsImmediatePropagationStopped != this.m_Event.isImmediatePropagationStopped, this.m_IsDefaultPrevented != this.m_Event.isDefaultPrevented, (long)(Time.realtimeSinceStartup * 1000f) - this.m_Start, mouseCapture);
			}
		}

		private string GetCallbackName()
		{
			bool flag = this.m_Callback == null;
			string result;
			if (flag)
			{
				result = "No callback";
			}
			else
			{
				bool flag2 = this.m_Callback.Target != null;
				if (flag2)
				{
					result = this.m_Callback.Target.GetType().FullName + "." + this.m_Callback.Method.Name;
				}
				else
				{
					bool flag3 = this.m_Callback.Method.DeclaringType != null;
					if (flag3)
					{
						result = this.m_Callback.Method.DeclaringType.FullName + "." + this.m_Callback.Method.Name;
					}
					else
					{
						result = this.m_Callback.Method.Name;
					}
				}
			}
			return result;
		}

		private int GetCallbackHashCode()
		{
			Delegate expr_07 = this.m_Callback;
			return (expr_07 != null) ? expr_07.GetHashCode() : 0;
		}
	}
}
