using System;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	internal class MouseEventDispatchingStrategy : IEventDispatchingStrategy
	{
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is IMouseEvent;
		}

		public void DispatchEvent(EventBase evt, IPanel iPanel)
		{
			bool flag = iPanel != null;
			if (flag)
			{
				Assert.IsTrue(iPanel is BaseVisualElementPanel);
				BaseVisualElementPanel panel = (BaseVisualElementPanel)iPanel;
				MouseEventDispatchingStrategy.SetBestTargetForEvent(evt, panel);
				MouseEventDispatchingStrategy.SendEventToTarget(evt, panel);
			}
			evt.stopDispatch = true;
		}

		private static bool SendEventToTarget(EventBase evt, BaseVisualElementPanel panel)
		{
			return MouseEventDispatchingStrategy.SendEventToRegularTarget(evt, panel) || MouseEventDispatchingStrategy.SendEventToIMGUIContainer(evt, panel);
		}

		private static bool SendEventToRegularTarget(EventBase evt, BaseVisualElementPanel panel)
		{
			bool flag = evt.target == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				EventDispatchUtilities.PropagateEvent(evt);
				result = MouseEventDispatchingStrategy.IsDone(evt);
			}
			return result;
		}

		private static bool SendEventToIMGUIContainer(EventBase evt, BaseVisualElementPanel panel)
		{
			bool flag = evt.imguiEvent == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IMGUIContainer rootIMGUIContainer = panel.rootIMGUIContainer;
				bool flag2 = rootIMGUIContainer == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = evt.propagateToIMGUI || evt.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() || evt.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId();
					if (flag3)
					{
						evt.skipElements.Add(evt.target);
						EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
					}
					result = MouseEventDispatchingStrategy.IsDone(evt);
				}
			}
			return result;
		}

		private static void SetBestTargetForEvent(EventBase evt, BaseVisualElementPanel panel)
		{
			VisualElement visualElement;
			MouseEventDispatchingStrategy.UpdateElementUnderMouse(evt, panel, out visualElement);
			bool flag = evt.target != null;
			if (flag)
			{
				evt.propagateToIMGUI = false;
			}
			else
			{
				bool flag2 = visualElement != null;
				if (flag2)
				{
					evt.propagateToIMGUI = false;
					evt.target = visualElement;
				}
				else
				{
					evt.target = ((panel != null) ? panel.visualTree : null);
				}
			}
		}

		private static void UpdateElementUnderMouse(EventBase evt, BaseVisualElementPanel panel, out VisualElement elementUnderMouse)
		{
			IMouseEventInternal expr_07 = evt as IMouseEventInternal;
			elementUnderMouse = ((expr_07 == null || expr_07.recomputeTopElementUnderMouse) ? panel.RecomputeTopElementUnderPointer(((IMouseEvent)evt).mousePosition, evt) : panel.GetTopElementUnderPointer(PointerId.mousePointerId));
			bool flag = evt.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId() && (evt as MouseLeaveWindowEvent).pressedButtons == 0;
			if (flag)
			{
				panel.ClearCachedElementUnderPointer(evt);
			}
		}

		private static bool IsDone(EventBase evt)
		{
			Event expr_07 = evt.imguiEvent;
			bool flag = expr_07 != null && expr_07.rawType == EventType.Used;
			if (flag)
			{
				evt.StopPropagation();
			}
			return evt.isPropagationStopped;
		}
	}
}
