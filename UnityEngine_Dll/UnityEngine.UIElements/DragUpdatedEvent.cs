using System;

namespace UnityEngine.UIElements
{
	public class DragUpdatedEvent : DragAndDropEventBase<DragUpdatedEvent>
	{
		public new static DragUpdatedEvent GetPooled(Event systemEvent)
		{
			bool flag = systemEvent != null;
			if (flag)
			{
				PointerDeviceState.PressButton(PointerId.mousePointerId, systemEvent.button);
			}
			DragUpdatedEvent pooled = MouseEventBase<DragUpdatedEvent>.GetPooled(systemEvent);
			pooled.button = 0;
			return pooled;
		}

		internal static DragUpdatedEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseEventBase<DragUpdatedEvent>.GetPooled(pointerEvent);
		}

		protected internal override void PostDispatch(IPanel panel)
		{
			EventBase eventBase = ((IMouseEventInternal)this).sourcePointerEvent as EventBase;
			bool flag = eventBase == null;
			if (flag)
			{
				BaseVisualElementPanel expr_1C = panel as BaseVisualElementPanel;
				if (expr_1C != null)
				{
					expr_1C.CommitElementUnderPointers();
				}
			}
			base.PostDispatch(panel);
		}
	}
}
