using System;

namespace UnityEngine.UIElements
{
	internal static class EventDispatchUtilities
	{
		public static void PropagateEvent(EventBase evt)
		{
			Debug.Assert(!evt.dispatch, "Event is being dispatched recursively.");
			evt.dispatch = true;
			bool flag = evt.path == null;
			if (flag)
			{
				CallbackEventHandler expr_36 = evt.target as CallbackEventHandler;
				if (expr_36 != null)
				{
					expr_36.HandleEventAtTargetPhase(evt);
				}
			}
			else
			{
				bool tricklesDown = evt.tricklesDown;
				if (tricklesDown)
				{
					evt.propagationPhase = PropagationPhase.TrickleDown;
					for (int i = evt.path.trickleDownPath.Count - 1; i >= 0; i--)
					{
						bool isPropagationStopped = evt.isPropagationStopped;
						if (isPropagationStopped)
						{
							break;
						}
						bool flag2 = evt.Skip(evt.path.trickleDownPath[i]);
						if (!flag2)
						{
							evt.currentTarget = evt.path.trickleDownPath[i];
							evt.currentTarget.HandleEvent(evt);
						}
					}
				}
				evt.propagationPhase = PropagationPhase.AtTarget;
				foreach (VisualElement current in evt.path.targetElements)
				{
					bool flag3 = evt.Skip(current);
					if (!flag3)
					{
						evt.target = current;
						evt.currentTarget = evt.target;
						evt.currentTarget.HandleEvent(evt);
					}
				}
				evt.propagationPhase = PropagationPhase.DefaultActionAtTarget;
				foreach (VisualElement current2 in evt.path.targetElements)
				{
					bool flag4 = evt.Skip(current2);
					if (!flag4)
					{
						evt.target = current2;
						evt.currentTarget = evt.target;
						evt.currentTarget.HandleEvent(evt);
					}
				}
				evt.target = evt.leafTarget;
				bool bubbles = evt.bubbles;
				if (bubbles)
				{
					evt.propagationPhase = PropagationPhase.BubbleUp;
					foreach (VisualElement current3 in evt.path.bubbleUpPath)
					{
						bool flag5 = evt.Skip(current3);
						if (!flag5)
						{
							evt.currentTarget = current3;
							evt.currentTarget.HandleEvent(evt);
						}
					}
				}
			}
			evt.dispatch = false;
			evt.propagationPhase = PropagationPhase.None;
			evt.currentTarget = null;
		}

		internal static void PropagateToIMGUIContainer(VisualElement root, EventBase evt)
		{
			bool flag = evt.imguiEvent == null || root.elementPanel.contextType == ContextType.Player;
			if (!flag)
			{
				bool isIMGUIContainer = root.isIMGUIContainer;
				if (isIMGUIContainer)
				{
					IMGUIContainer iMGUIContainer = root as IMGUIContainer;
					bool flag2 = evt.Skip(iMGUIContainer);
					if (flag2)
					{
						return;
					}
					Focusable expr_54 = evt.target as Focusable;
					bool flag3 = expr_54 != null && expr_54.focusable;
					bool flag4 = iMGUIContainer.SendEventToIMGUI(evt, !flag3, true);
					if (flag4)
					{
						evt.StopPropagation();
						evt.PreventDefault();
					}
					bool flag5 = evt.imguiEvent.rawType == EventType.Used;
					if (flag5)
					{
						Debug.Assert(evt.isPropagationStopped);
					}
				}
				bool flag6 = root.imguiContainerDescendantCount > 0;
				if (flag6)
				{
					int childCount = root.hierarchy.childCount;
					for (int i = 0; i < childCount; i++)
					{
						EventDispatchUtilities.PropagateToIMGUIContainer(root.hierarchy[i], evt);
						bool isPropagationStopped = evt.isPropagationStopped;
						if (isPropagationStopped)
						{
							break;
						}
					}
				}
			}
		}

		public static void ExecuteDefaultAction(EventBase evt, IPanel panel)
		{
			bool flag = evt.target == null && panel != null;
			if (flag)
			{
				evt.target = panel.visualTree;
			}
			bool flag2 = evt.target != null;
			if (flag2)
			{
				evt.dispatch = true;
				evt.currentTarget = evt.target;
				evt.propagationPhase = PropagationPhase.DefaultAction;
				evt.currentTarget.HandleEvent(evt);
				evt.propagationPhase = PropagationPhase.None;
				evt.currentTarget = null;
				evt.dispatch = false;
			}
		}
	}
}
