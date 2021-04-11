using System;

namespace UnityEngine.UIElements
{
	internal class IMGUIEventDispatchingStrategy : IEventDispatchingStrategy
	{
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is IMGUIEvent;
		}

		public void DispatchEvent(EventBase evt, IPanel panel)
		{
			bool flag = panel != null;
			if (flag)
			{
				EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
			}
			evt.propagateToIMGUI = false;
			evt.stopDispatch = true;
		}
	}
}
