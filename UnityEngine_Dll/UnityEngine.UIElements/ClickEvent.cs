using System;

namespace UnityEngine.UIElements
{
	public sealed class ClickEvent : PointerEventBase<ClickEvent>
	{
		internal static ClickEvent GetPooled(PointerUpEvent pointerEvent, int clickCount)
		{
			ClickEvent pooled = PointerEventBase<ClickEvent>.GetPooled(pointerEvent);
			pooled.clickCount = clickCount;
			return pooled;
		}
	}
}
