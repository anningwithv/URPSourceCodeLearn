using System;

namespace UnityEngine.UIElements.UIR
{
	internal class GPUBufferAllocator
	{
		private BestFitAllocator m_Low;

		private BestFitAllocator m_High;

		public bool isEmpty
		{
			get
			{
				return this.m_Low.highWatermark == 0u && this.m_High.highWatermark == 0u;
			}
		}

		public GPUBufferAllocator(uint maxSize)
		{
			this.m_Low = new BestFitAllocator(maxSize);
			this.m_High = new BestFitAllocator(maxSize);
		}

		public Alloc Allocate(uint size, bool shortLived)
		{
			bool flag = !shortLived;
			Alloc alloc;
			if (flag)
			{
				alloc = this.m_Low.Allocate(size);
			}
			else
			{
				alloc = this.m_High.Allocate(size);
				alloc.start = this.m_High.totalSize - alloc.start - alloc.size;
			}
			alloc.shortLived = shortLived;
			bool flag2 = this.HighLowCollide() && alloc.size > 0u;
			Alloc result;
			if (flag2)
			{
				this.Free(alloc);
				result = default(Alloc);
			}
			else
			{
				result = alloc;
			}
			return result;
		}

		public void Free(Alloc alloc)
		{
			bool flag = !alloc.shortLived;
			if (flag)
			{
				this.m_Low.Free(alloc);
			}
			else
			{
				alloc.start = this.m_High.totalSize - alloc.start - alloc.size;
				this.m_High.Free(alloc);
			}
		}

		public HeapStatistics GatherStatistics()
		{
			HeapStatistics heapStatistics = default(HeapStatistics);
			heapStatistics.subAllocators = new HeapStatistics[]
			{
				this.m_Low.GatherStatistics(),
				this.m_High.GatherStatistics()
			};
			heapStatistics.largestAvailableBlock = 4294967295u;
			for (int i = 0; i < 2; i++)
			{
				heapStatistics.numAllocs += heapStatistics.subAllocators[i].numAllocs;
				heapStatistics.totalSize = Math.Max(heapStatistics.totalSize, heapStatistics.subAllocators[i].totalSize);
				heapStatistics.allocatedSize += heapStatistics.subAllocators[i].allocatedSize;
				heapStatistics.largestAvailableBlock = Math.Min(heapStatistics.largestAvailableBlock, heapStatistics.subAllocators[i].largestAvailableBlock);
				heapStatistics.availableBlocksCount += heapStatistics.subAllocators[i].availableBlocksCount;
				heapStatistics.blockCount += heapStatistics.subAllocators[i].blockCount;
				heapStatistics.highWatermark = Math.Max(heapStatistics.highWatermark, heapStatistics.subAllocators[i].highWatermark);
				heapStatistics.fragmentation = Math.Max(heapStatistics.fragmentation, heapStatistics.subAllocators[i].fragmentation);
			}
			heapStatistics.freeSize = heapStatistics.totalSize - heapStatistics.allocatedSize;
			return heapStatistics;
		}

		private bool HighLowCollide()
		{
			return this.m_Low.highWatermark + this.m_High.highWatermark > this.m_Low.totalSize;
		}
	}
}
