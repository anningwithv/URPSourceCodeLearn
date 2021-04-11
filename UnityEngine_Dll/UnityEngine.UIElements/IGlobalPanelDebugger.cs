using System;

namespace UnityEngine.UIElements
{
	internal interface IGlobalPanelDebugger
	{
		bool InterceptMouseEvent(IPanel panel, IMouseEvent ev);

		void OnPostMouseEvent(IPanel panel, IMouseEvent ev);
	}
}
