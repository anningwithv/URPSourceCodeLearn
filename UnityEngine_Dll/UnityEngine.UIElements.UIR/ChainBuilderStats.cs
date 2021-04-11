using System;

namespace UnityEngine.UIElements.UIR
{
	internal struct ChainBuilderStats
	{
		public uint elementsAdded;

		public uint elementsRemoved;

		public uint recursiveClipUpdates;

		public uint recursiveClipUpdatesExpanded;

		public uint nonRecursiveClipUpdates;

		public uint recursiveTransformUpdates;

		public uint recursiveTransformUpdatesExpanded;

		public uint recursiveOpacityUpdates;

		public uint recursiveOpacityUpdatesExpanded;

		public uint recursiveVisualUpdates;

		public uint recursiveVisualUpdatesExpanded;

		public uint nonRecursiveVisualUpdates;

		public uint dirtyProcessed;

		public uint nudgeTransformed;

		public uint boneTransformed;

		public uint skipTransformed;

		public uint visualUpdateTransformed;

		public uint updatedMeshAllocations;

		public uint newMeshAllocations;

		public uint groupTransformElementsChanged;

		public uint immedateRenderersActive;

		public uint textUpdates;
	}
}
