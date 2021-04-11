using System;

namespace UnityEngine.UIElements
{
	internal class DebuggerEventDispatchingStrategy : IEventDispatchingStrategy
	{
		internal static IGlobalPanelDebugger s_GlobalPanelDebug;

		public bool CanDispatchEvent(EventBase evt)
		{
			return true;
		}

		public void DispatchEvent(EventBase evt, IPanel panel)
		{
			IMouseEvent mouseEvent = evt as IMouseEvent;
			bool flag = DebuggerEventDispatchingStrategy.s_GlobalPanelDebug != null && mouseEvent != null;
			if (flag)
			{
				bool flag2 = DebuggerEventDispatchingStrategy.s_GlobalPanelDebug.InterceptMouseEvent(panel, mouseEvent);
				if (flag2)
				{
					evt.StopPropagation();
					evt.PreventDefault();
					evt.stopDispatch = true;
					return;
				}
			}
			BaseVisualElementPanel expr_4B = panel as BaseVisualElementPanel;
			IPanelDebug panelDebug = (expr_4B != null) ? expr_4B.panelDebug : null;
			bool flag3 = panelDebug != null;
			if (flag3)
			{
				bool flag4 = panelDebug.InterceptEvent(evt);
				if (flag4)
				{
					evt.StopPropagation();
					evt.PreventDefault();
					evt.stopDispatch = true;
				}
			}
		}

		public void PostDispatch(EventBase evt, IPanel panel)
		{
			IMouseEvent mouseEvent = evt as IMouseEvent;
			BaseVisualElementPanel expr_0E = panel as BaseVisualElementPanel;
			IPanelDebug panelDebug = (expr_0E != null) ? expr_0E.panelDebug : null;
			bool flag = panelDebug != null;
			if (flag)
			{
				panelDebug.PostProcessEvent(evt);
			}
			bool flag2 = DebuggerEventDispatchingStrategy.s_GlobalPanelDebug != null && mouseEvent != null && evt.target != null && !evt.isDefaultPrevented && !evt.isPropagationStopped;
			if (flag2)
			{
				DebuggerEventDispatchingStrategy.s_GlobalPanelDebug.OnPostMouseEvent(panel, mouseEvent);
			}
		}
	}
}
