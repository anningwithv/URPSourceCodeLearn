using System;

namespace UnityEngine.UIElements
{
	internal class PointerEventDispatchingStrategy : IEventDispatchingStrategy
	{
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is IPointerEvent;
		}

		public virtual void DispatchEvent(EventBase evt, IPanel panel)
		{
			PointerEventDispatchingStrategy.SetBestTargetForEvent(evt, panel);
			PointerEventDispatchingStrategy.SendEventToTarget(evt);
			evt.stopDispatch = true;
		}

		private static void SendEventToTarget(EventBase evt)
		{
			bool flag = evt.target != null;
			if (flag)
			{
				EventDispatchUtilities.PropagateEvent(evt);
			}
		}

		private static void SetBestTargetForEvent(EventBase evt, IPanel panel)
		{
			VisualElement visualElement;
			PointerEventDispatchingStrategy.UpdateElementUnderPointer(evt, panel, out visualElement);
			bool flag = evt.target == null && visualElement != null;
			if (flag)
			{
				evt.propagateToIMGUI = false;
				evt.target = visualElement;
			}
			else
			{
				bool flag2 = evt.target == null && visualElement == null;
				if (flag2)
				{
					evt.target = ((panel != null) ? panel.visualTree : null);
				}
				else
				{
					bool flag3 = evt.target != null;
					if (flag3)
					{
						evt.propagateToIMGUI = false;
					}
				}
			}
		}

		private static void UpdateElementUnderPointer(EventBase evt, IPanel panel, out VisualElement elementUnderPointer)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
			IPointerEventInternal expr_15 = evt as IPointerEventInternal;
			elementUnderPointer = ((expr_15 == null || expr_15.recomputeTopElementUnderPointer) ? ((baseVisualElementPanel != null) ? baseVisualElementPanel.RecomputeTopElementUnderPointer(pointerEvent.position, evt) : null) : ((baseVisualElementPanel != null) ? baseVisualElementPanel.GetTopElementUnderPointer(pointerEvent.pointerId) : null));
		}
	}
}
