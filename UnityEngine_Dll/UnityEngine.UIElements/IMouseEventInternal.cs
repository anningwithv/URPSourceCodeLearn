using System;

namespace UnityEngine.UIElements
{
	internal interface IMouseEventInternal
	{
		bool triggeredByOS
		{
			get;
			set;
		}

		bool recomputeTopElementUnderMouse
		{
			get;
			set;
		}

		IPointerEvent sourcePointerEvent
		{
			get;
			set;
		}
	}
}
