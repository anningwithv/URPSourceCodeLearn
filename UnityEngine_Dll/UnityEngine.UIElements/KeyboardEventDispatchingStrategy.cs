using System;

namespace UnityEngine.UIElements
{
	internal class KeyboardEventDispatchingStrategy : IEventDispatchingStrategy
	{
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is IKeyboardEvent;
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
					}
					else
					{
						evt.target = leafFocusedElement;
						EventDispatchUtilities.PropagateEvent(evt);
					}
				}
				else
				{
					evt.target = panel.visualTree;
					EventDispatchUtilities.PropagateEvent(evt);
					bool flag4 = !evt.isPropagationStopped;
					if (flag4)
					{
						EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
					}
				}
			}
			evt.propagateToIMGUI = false;
			evt.stopDispatch = true;
		}
	}
}
