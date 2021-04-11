using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal static class MouseEventsHelper
	{
		internal static void SendEnterLeave<TLeaveEvent, TEnterEvent>(VisualElement previousTopElementUnderMouse, VisualElement currentTopElementUnderMouse, IMouseEvent triggerEvent, Vector2 mousePosition) where TLeaveEvent : MouseEventBase<TLeaveEvent>, new() where TEnterEvent : MouseEventBase<TEnterEvent>, new()
		{
			bool flag = previousTopElementUnderMouse != null && previousTopElementUnderMouse.panel == null;
			if (flag)
			{
				previousTopElementUnderMouse = null;
			}
			int i = 0;
			VisualElement visualElement;
			for (visualElement = previousTopElementUnderMouse; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				i++;
			}
			int j = 0;
			VisualElement visualElement2;
			for (visualElement2 = currentTopElementUnderMouse; visualElement2 != null; visualElement2 = visualElement2.hierarchy.parent)
			{
				j++;
			}
			visualElement = previousTopElementUnderMouse;
			visualElement2 = currentTopElementUnderMouse;
			while (i > j)
			{
				using (TLeaveEvent pooled = MouseEventBase<TLeaveEvent>.GetPooled(triggerEvent, mousePosition, false))
				{
					pooled.target = visualElement;
					visualElement.SendEvent(pooled);
				}
				i--;
				visualElement = visualElement.hierarchy.parent;
			}
			List<VisualElement> list = VisualElementListPool.Get(j);
			while (j > i)
			{
				list.Add(visualElement2);
				j--;
				visualElement2 = visualElement2.hierarchy.parent;
			}
			while (visualElement != visualElement2)
			{
				using (TLeaveEvent pooled2 = MouseEventBase<TLeaveEvent>.GetPooled(triggerEvent, mousePosition, false))
				{
					pooled2.target = visualElement;
					visualElement.SendEvent(pooled2);
				}
				list.Add(visualElement2);
				visualElement = visualElement.hierarchy.parent;
				visualElement2 = visualElement2.hierarchy.parent;
			}
			for (int k = list.Count - 1; k >= 0; k--)
			{
				using (TEnterEvent pooled3 = MouseEventBase<TEnterEvent>.GetPooled(triggerEvent, mousePosition, false))
				{
					pooled3.target = list[k];
					list[k].SendEvent(pooled3);
				}
			}
			VisualElementListPool.Release(list);
		}

		internal static void SendMouseOverMouseOut(VisualElement previousTopElementUnderMouse, VisualElement currentTopElementUnderMouse, IMouseEvent triggerEvent, Vector2 mousePosition)
		{
			bool flag = previousTopElementUnderMouse != null && previousTopElementUnderMouse.panel != null;
			if (flag)
			{
				using (MouseOutEvent pooled = MouseEventBase<MouseOutEvent>.GetPooled(triggerEvent, mousePosition, false))
				{
					pooled.target = previousTopElementUnderMouse;
					previousTopElementUnderMouse.SendEvent(pooled);
				}
			}
			bool flag2 = currentTopElementUnderMouse != null;
			if (flag2)
			{
				using (MouseOverEvent pooled2 = MouseEventBase<MouseOverEvent>.GetPooled(triggerEvent, mousePosition, false))
				{
					pooled2.target = currentTopElementUnderMouse;
					currentTopElementUnderMouse.SendEvent(pooled2);
				}
			}
		}
	}
}
