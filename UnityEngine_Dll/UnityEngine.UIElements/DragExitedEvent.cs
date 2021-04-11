using System;

namespace UnityEngine.UIElements
{
	public class DragExitedEvent : DragAndDropEventBase<DragExitedEvent>
	{
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown);
		}

		public DragExitedEvent()
		{
			this.LocalInit();
		}

		public new static DragExitedEvent GetPooled(Event systemEvent)
		{
			bool flag = systemEvent != null;
			if (flag)
			{
				PointerDeviceState.ReleaseButton(PointerId.mousePointerId, systemEvent.button);
			}
			return MouseEventBase<DragExitedEvent>.GetPooled(systemEvent);
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
