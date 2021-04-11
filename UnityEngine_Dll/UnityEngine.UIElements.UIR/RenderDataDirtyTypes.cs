using System;

namespace UnityEngine.UIElements.UIR
{
	[Flags]
	internal enum RenderDataDirtyTypes
	{
		None = 0,
		Transform = 1,
		ClipRectSize = 2,
		Clipping = 4,
		ClippingHierarchy = 8,
		Visuals = 16,
		VisualsHierarchy = 32,
		Opacity = 64
	}
}
