using System;

namespace UnityEngine.UIElements.UIR
{
	internal struct HeapStatistics
	{
		public uint numAllocs;

		public uint totalSize;

		public uint allocatedSize;

		public uint freeSize;

		public uint largestAvailableBlock;

		public uint availableBlocksCount;

		public uint blockCount;

		public uint highWatermark;

		public float fragmentation;

		public HeapStatistics[] subAllocators;
	}
}
