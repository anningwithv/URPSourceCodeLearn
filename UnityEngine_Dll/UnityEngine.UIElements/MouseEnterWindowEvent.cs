using System;

namespace UnityEngine.UIElements
{
	public class MouseEnterWindowEvent : MouseEventBase<MouseEnterWindowEvent>
	{
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Cancellable;
		}

		public MouseEnterWindowEvent()
		{
			this.LocalInit();
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
