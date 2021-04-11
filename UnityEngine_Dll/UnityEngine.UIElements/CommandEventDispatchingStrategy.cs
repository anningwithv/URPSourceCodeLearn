using System;

namespace UnityEngine.UIElements
{
	internal class CommandEventDispatchingStrategy : IEventDispatchingStrategy
	{
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is ICommandEvent;
		}

		public void DispatchEvent(EventBase evt, IPanel panel)
		{
			bool flag = panel != null;
			if (flag)
			{
				Focusable leafFocusedElement = panel.focusController.GetLeafFocusedElement();
				bool flag2 = leafFocusedElement != null;
				if (flag2)
				{
					bool isIMGUIContainer = leafFocusedElement.isIMGUIContainer;
					if (isIMGUIContainer)
					{
						IMGUIContainer iMGUIContainer = (IMGUIContainer)leafFocusedElement;
						bool flag3 = !evt.Skip(iMGUIContainer) && iMGUIContainer.SendEventToIMGUI(evt, true, true);
						if (flag3)
						{
							evt.StopPropagation();
							evt.PreventDefault();
						}
						bool flag4 = !evt.isPropagationStopped && evt.propagateToIMGUI;
						if (flag4)
						{
							evt.skipElements.Add(iMGUIContainer);
							EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
						}
					}
					else
					{
						evt.target = panel.focusController.GetLeafFocusedElement();
						EventDispatchUtilities.PropagateEvent(evt);
						bool flag5 = !evt.isPropagationStopped && evt.propagateToIMGUI;
						if (flag5)
						{
							EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
						}
					}
				}
				else
				{
					EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
				}
			}
			evt.propagateToIMGUI = false;
			evt.stopDispatch = true;
		}
	}
}
