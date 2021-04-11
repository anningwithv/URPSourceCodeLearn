using System;

namespace UnityEngine.UIElements
{
	public interface IFocusEvent
	{
		Focusable relatedTarget
		{
			get;
		}

		FocusChangeDirection direction
		{
			get;
		}
	}
}
