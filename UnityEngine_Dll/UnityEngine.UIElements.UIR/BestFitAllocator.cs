using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	internal class BestFitAllocator
	{
		private class Block : PoolItem
		{
			public uint start;

			public uint end;

			public BestFitAllocator.Block prev;

			public BestFitAllocator.Block next;

			public BestFitAllocator.Block prevAvailable;

			public BestFitAllocator.Block nextAvailable;

			public bool allocated;

			public uint size
			{
				get
				{
					return this.end - this.start;
				}
			}
		}

		private BestFitAllocator.Block m_FirstBlock;

		private BestFitAllocator.Block m_FirstAvailableBlock;

		private Pool<BestFitAllocator.Block> m_BlockPool = new Pool<BestFitAllocator.Block>();

		private uint m_HighWatermark;

		public uint totalSize
		{
			[CompilerGenerated]
			get
			{
				return this.<totalSize>k__BackingField;
			}
		}

		public uint highWatermark
		{
			get
			{
				return this.m_HighWatermark;
			}
		}

		public BestFitAllocator(uint size)
		{
			this.<totalSize>k__BackingField = size;
			this.m_FirstBlock = (this.m_FirstAvailableBlock = this.m_BlockPool.Get());
			this.m_FirstAvailableBlock.end = size;
		}

		public Alloc Allocate(uint size)
		{
			BestFitAllocator.Block block = this.BestFitFindAvailableBlock(size);
			bool flag = block == null;
			Alloc result;
			if (flag)
			{
				result = default(Alloc);
			}
			else
			{
				Debug.Assert(block.size >= size);
				Debug.Assert(!block.allocated);
				bool flag2 = size != block.size;
				if (flag2)
				{
					this.SplitBlock(block, size);
				}
				Debug.Assert(block.size == size);
				bool flag3 = block.end > this.m_HighWatermark;
				if (flag3)
				{
					this.m_HighWatermark = block.end;
				}
				bool flag4 = block == this.m_FirstAvailableBlock;
				if (flag4)
				{
					this.m_FirstAvailableBlock = this.m_FirstAvailableBlock.nextAvailable;
				}
				bool flag5 = block.prevAvailable != null;
				if (flag5)
				{
					block.prevAvailable.nextAvailable = block.nextAvailable;
				}
				bool flag6 = block.nextAvailable != null;
				if (flag6)
				{
					block.nextAvailable.prevAvailable = block.prevAvailable;
				}
				block.allocated = true;
				block.prevAvailable = (block.nextAvailable = null);
				result = new Alloc
				{
					start = block.start,
					size = block.size,
					handle = block
				};
			}
			return result;
		}

		public void Free(Alloc alloc)
		{
			BestFitAllocator.Block block = (BestFitAllocator.Block)alloc.handle;
			bool flag = !block.allocated;
			if (flag)
			{
				Debug.Assert(false, "Severe error: UIR allocation double-free");
			}
			else
			{
				Debug.Assert(block.allocated);
				Debug.Assert(block.start == alloc.start);
				Debug.Assert(block.size == alloc.size);
				bool flag2 = block.end == this.m_HighWatermark;
				if (flag2)
				{
					bool flag3 = block.prev != null;
					if (flag3)
					{
						this.m_HighWatermark = (block.prev.allocated ? block.prev.end : block.prev.start);
					}
					else
					{
						this.m_HighWatermark = 0u;
					}
				}
				block.allocated = false;
				BestFitAllocator.Block block2 = this.m_FirstAvailableBlock;
				BestFitAllocator.Block block3 = null;
				while (block2 != null && block2.start < block.start)
				{
					block3 = block2;
					block2 = block2.nextAvailable;
				}
				bool flag4 = block3 == null;
				if (flag4)
				{
					Debug.Assert(block.prevAvailable == null);
					block.nextAvailable = this.m_FirstAvailableBlock;
					this.m_FirstAvailableBlock = block;
				}
				else
				{
					block.prevAvailable = block3;
					block.nextAvailable = block3.nextAvailable;
					block3.nextAvailable = block;
				}
				bool flag5 = block.nextAvailable != null;
				if (flag5)
				{
					block.nextAvailable.prevAvailable = block;
				}
				bool flag6 = block.prevAvailable == block.prev && block.prev != null;
				if (flag6)
				{
					block = this.CoalesceBlockWithPrevious(block);
				}
				bool flag7 = block.nextAvailable == block.next && block.next != null;
				if (flag7)
				{
					block = this.CoalesceBlockWithPrevious(block.next);
				}
			}
		}

		private BestFitAllocator.Block CoalesceBlockWithPrevious(BestFitAllocator.Block block)
		{
			Debug.Assert(block.prevAvailable.end == block.start);
			Debug.Assert(block.prev.nextAvailable == block);
			BestFitAllocator.Block prev = block.prev;
			prev.next = block.next;
			bool flag = block.next != null;
			if (flag)
			{
				block.next.prev = prev;
			}
			prev.nextAvailable = block.nextAvailable;
			bool flag2 = block.nextAvailable != null;
			if (flag2)
			{
				block.nextAvailable.prevAvailable = block.prevAvailable;
			}
			prev.end = block.end;
			this.m_BlockPool.Return(block);
			return prev;
		}

		internal HeapStatistics GatherStatistics()
		{
			HeapStatistics heapStatistics = default(HeapStatistics);
			for (BestFitAllocator.Block block = this.m_FirstBlock; block != null; block = block.next)
			{
				bool allocated = block.allocated;
				if (allocated)
				{
					heapStatistics.numAllocs += 1u;
					heapStatistics.allocatedSize += block.size;
				}
				else
				{
					heapStatistics.freeSize += block.size;
					heapStatistics.availableBlocksCount += 1u;
					heapStatistics.largestAvailableBlock = Math.Max(heapStatistics.largestAvailableBlock, block.size);
				}
				heapStatistics.blockCount += 1u;
			}
			heapStatistics.totalSize = this.totalSize;
			heapStatistics.highWatermark = this.m_HighWatermark;
			bool flag = heapStatistics.freeSize > 0u;
			if (flag)
			{
				heapStatistics.fragmentation = (float)((heapStatistics.freeSize - heapStatistics.largestAvailableBlock) / heapStatistics.freeSize) * 100f;
			}
			return heapStatistics;
		}

		private BestFitAllocator.Block BestFitFindAvailableBlock(uint size)
		{
			BestFitAllocator.Block block = this.m_FirstAvailableBlock;
			BestFitAllocator.Block result = null;
			uint num = 4294967295u;
			while (block != null)
			{
				bool flag = block.size >= size && num > block.size;
				if (flag)
				{
					result = block;
					num = block.size;
				}
				block = block.nextAvailable;
			}
			return result;
		}

		private void SplitBlock(BestFitAllocator.Block block, uint size)
		{
			Debug.Assert(block.size > size);
			BestFitAllocator.Block block2 = this.m_BlockPool.Get();
			block2.next = block.next;
			block2.nextAvailable = block.nextAvailable;
			block2.prev = block;
			block2.prevAvailable = block;
			block2.start = block.start + size;
			block2.end = block.end;
			bool flag = block2.next != null;
			if (flag)
			{
				block2.next.prev = block2;
			}
			bool flag2 = block2.nextAvailable != null;
			if (flag2)
			{
				block2.nextAvailable.prevAvailable = block2;
			}
			block.next = block2;
			block.nextAvailable = block2;
			block.end = block2.start;
		}
	}
}
