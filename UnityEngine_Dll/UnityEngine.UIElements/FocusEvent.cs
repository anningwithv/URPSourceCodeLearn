using System;

namespace UnityEngine.UIElements
{
	public class FocusEvent : FocusEventBase<FocusEvent>
	{
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.focusController.DoFocusChange(base.target as Focusable);
		}
	}
}
