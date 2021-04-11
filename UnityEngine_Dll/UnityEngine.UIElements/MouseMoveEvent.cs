using System;

namespace UnityEngine.UIElements
{
	public class MouseMoveEvent : MouseEventBase<MouseMoveEvent>
	{
		public new static MouseMoveEvent GetPooled(Event systemEvent)
		{
			MouseMoveEvent pooled = MouseEventBase<MouseMoveEvent>.GetPooled(systemEvent);
			pooled.button = 0;
			return pooled;
		}

		internal static MouseMoveEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseEventBase<MouseMoveEvent>.GetPooled(pointerEvent);
		}
	}
}
