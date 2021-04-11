using System;

namespace UnityEngine.UIElements
{
	internal class DefaultDispatchingStrategy : IEventDispatchingStrategy
	{
		public bool CanDispatchEvent(EventBase evt)
		{
			return !(evt is IMGUIEvent);
		}

		public void DispatchEvent(EventBase evt, IPanel panel)
		{
			bool flag = evt.target != null;
			if (flag)
			{
				evt.propagateToIMGUI = (evt.target is IMGUIContainer);
				EventDispatchUtilities.PropagateEvent(evt);
			}
			else
			{
				bool flag2 = !evt.isPropagationStopped && panel != null;
				if (flag2)
				{
					bool flag3 = evt.propagateToIMGUI || evt.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() || evt.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId();
					if (flag3)
					{
						EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
					}
				}
			}
			evt.stopDispatch = true;
		}
	}
}
