using System;

namespace UnityEngine.UIElements
{
	internal class PointerCaptureDispatchingStrategy : IEventDispatchingStrategy
	{
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is IPointerEvent;
		}

		public void DispatchEvent(EventBase evt, IPanel panel)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				IEventHandler capturingElement = panel.GetCapturingElement(pointerEvent.pointerId);
				bool flag2 = capturingElement == null;
				if (!flag2)
				{
					VisualElement visualElement = capturingElement as VisualElement;
					bool flag3 = evt.eventTypeId != EventBase<PointerCaptureOutEvent>.TypeId() && visualElement != null && visualElement.panel == null;
					if (flag3)
					{
						panel.ReleasePointer(pointerEvent.pointerId);
					}
					else
					{
						bool flag4 = evt.target != null && evt.target != capturingElement;
						if (!flag4)
						{
							bool flag5 = evt.eventTypeId != EventBase<PointerCaptureEvent>.TypeId() && evt.eventTypeId != EventBase<PointerCaptureOutEvent>.TypeId();
							if (flag5)
							{
								panel.ProcessPointerCapture(pointerEvent.pointerId);
							}
							evt.dispatch = true;
							evt.target = capturingElement;
							evt.currentTarget = capturingElement;
							evt.propagationPhase = PropagationPhase.AtTarget;
							capturingElement.HandleEvent(evt);
							evt.currentTarget = null;
							evt.propagationPhase = PropagationPhase.None;
							evt.dispatch = false;
							evt.stopDispatch = true;
							evt.propagateToIMGUI = false;
						}
					}
				}
			}
		}
	}
}
