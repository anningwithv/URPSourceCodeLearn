using System;

namespace UnityEngine.UIElements
{
	[Flags]
	internal enum VisualElementFlags
	{
		WorldTransformDirty = 1,
		WorldTransformInverseDirty = 2,
		WorldClipDirty = 4,
		BoundingBoxDirty = 8,
		WorldBoundingBoxDirty = 16,
		LayoutManual = 32,
		CompositeRoot = 64,
		RequireMeasureFunction = 128,
		EnableViewDataPersistence = 256,
		NeedsAttachToPanelEvent = 1024,
		Init = 31
	}
}
