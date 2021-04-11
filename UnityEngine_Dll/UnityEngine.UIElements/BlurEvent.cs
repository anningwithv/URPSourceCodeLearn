using System;

namespace UnityEngine.UIElements
{
	public class BlurEvent : FocusEventBase<BlurEvent>
	{
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool flag = base.relatedTarget == null;
			if (flag)
			{
				base.focusController.DoFocusChange(null);
			}
		}
	}
}
