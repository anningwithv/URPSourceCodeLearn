using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal static class PointerEventsHelper
	{
		internal static void SendEnterLeave<TLeaveEvent, TEnterEvent>(VisualElement previousTopElementUnderPointer, VisualElement currentTopElementUnderPointer, IPointerEvent triggerEvent, Vector2 position, int pointerId) where TLeaveEvent : PointerEventBase<TLeaveEvent>, new() where TEnterEvent : PointerEventBase<TEnterEvent>, new()
		{
			bool flag = previousTopElementUnderPointer != null && previousTopElementUnderPointer.panel == null;
			if (flag)
			{
				previousTopElementUnderPointer = null;
			}
			int i = 0;
			VisualElement visualElement;
			for (visualElement = previousTopElementUnderPointer; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				i++;
			}
			int j = 0;
			VisualElement visualElement2;
			for (visualElement2 = currentTopElementUnderPointer; visualElement2 != null; visualElement2 = visualElement2.hierarchy.parent)
			{
				j++;
			}
			visualElement = previousTopElementUnderPointer;
			visualElement2 = currentTopElementUnderPointer;
			while (i > j)
			{
				using (TLeaveEvent pooled = PointerEventBase<TLeaveEvent>.GetPooled(triggerEvent, position, pointerId))
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
				using (TLeaveEvent pooled2 = PointerEventBase<TLeaveEvent>.GetPooled(triggerEvent, position, pointerId))
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
				using (TEnterEvent pooled3 = PointerEventBase<TEnterEvent>.GetPooled(triggerEvent, position, pointerId))
				{
					pooled3.target = list[k];
					list[k].SendEvent(pooled3);
				}
			}
			VisualElementListPool.Release(list);
		}

		internal static void SendOverOut(VisualElement previousTopElementUnderPointer, VisualElement currentTopElementUnderPointer, IPointerEvent triggerEvent, Vector2 position, int pointerId)
		{
			bool flag = previousTopElementUnderPointer != null && previousTopElementUnderPointer.panel != null;
			if (flag)
			{
				using (PointerOutEvent pooled = PointerEventBase<PointerOutEvent>.GetPooled(triggerEvent, position, pointerId))
				{
					pooled.target = previousTopElementUnderPointer;
					previousTopElementUnderPointer.SendEvent(pooled);
				}
			}
			bool flag2 = currentTopElementUnderPointer != null;
			if (flag2)
			{
				using (PointerOverEvent pooled2 = PointerEventBase<PointerOverEvent>.GetPooled(triggerEvent, position, pointerId))
				{
					pooled2.target = currentTopElementUnderPointer;
					currentTopElementUnderPointer.SendEvent(pooled2);
				}
			}
		}
	}
}
