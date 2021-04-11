using System;

namespace UnityEngine.UIElements
{
	public sealed class PointerDownEvent : PointerEventBase<PointerDownEvent>
	{
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		private void LocalInit()
		{
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = true;
		}

		public PointerDownEvent()
		{
			this.LocalInit();
		}

		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = !base.isDefaultPrevented;
			if (flag)
			{
				bool flag2 = panel.ShouldSendCompatibilityMouseEvents(this);
				if (flag2)
				{
					using (MouseDownEvent pooled = MouseDownEvent.GetPooled(this))
					{
						pooled.target = base.target;
						pooled.target.SendEvent(pooled);
					}
				}
			}
			else
			{
				panel.PreventCompatibilityMouseEvents(base.pointerId);
			}
			base.PostDispatch(panel);
		}
	}
}
