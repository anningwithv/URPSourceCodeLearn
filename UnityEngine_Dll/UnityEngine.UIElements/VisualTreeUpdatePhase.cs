using System;

namespace UnityEngine.UIElements
{
	internal enum VisualTreeUpdatePhase
	{
		ViewData,
		Bindings,
		Animation,
		Styles,
		Layout,
		TransformClip,
		Repaint,
		Count
	}
}
