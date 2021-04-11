using System;

namespace UnityEngine.UIElements
{
	public sealed class PointerUpEvent : PointerEventBase<PointerUpEvent>
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

		public PointerUpEvent()
		{
			this.LocalInit();
		}

		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = PointerType.IsDirectManipulationDevice(base.pointerType);
			if (flag)
			{
				panel.ReleasePointer(base.pointerId);
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.ClearCachedElementUnderPointer(this);
				}
			}
			bool flag2 = panel.ShouldSendCompatibilityMouseEvents(this);
			if (flag2)
			{
				using (MouseUpEvent pooled = MouseUpEvent.GetPooled(this))
				{
					pooled.target = base.target;
					pooled.target.SendEvent(pooled);
				}
			}
			panel.ActivateCompatibilityMouseEvents(base.pointerId);
			base.PostDispatch(panel);
		}
	}
}
