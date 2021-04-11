using System;

namespace UnityEngine.UIElements
{
	public class MouseUpEvent : MouseEventBase<MouseUpEvent>
	{
		public new static MouseUpEvent GetPooled(Event systemEvent)
		{
			bool flag = systemEvent != null;
			if (flag)
			{
				PointerDeviceState.ReleaseButton(PointerId.mousePointerId, systemEvent.button);
			}
			return MouseEventBase<MouseUpEvent>.GetPooled(systemEvent);
		}

		private static MouseUpEvent MakeFromPointerEvent(IPointerEvent pointerEvent)
		{
			bool flag = pointerEvent != null && pointerEvent.button >= 0;
			if (flag)
			{
				PointerDeviceState.ReleaseButton(PointerId.mousePointerId, pointerEvent.button);
			}
			return MouseEventBase<MouseUpEvent>.GetPooled(pointerEvent);
		}

		internal static MouseUpEvent GetPooled(PointerUpEvent pointerEvent)
		{
			return MouseUpEvent.MakeFromPointerEvent(pointerEvent);
		}

		internal static MouseUpEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseUpEvent.MakeFromPointerEvent(pointerEvent);
		}

		internal static MouseUpEvent GetPooled(PointerCancelEvent pointerEvent)
		{
			return MouseUpEvent.MakeFromPointerEvent(pointerEvent);
		}
	}
}
