using System;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	internal class UIRAtlasAllocator : IDisposable
	{
		private class Row
		{
			private static ObjectPool<UIRAtlasAllocator.Row> s_Pool = new ObjectPool<UIRAtlasAllocator.Row>(100);

			public int Cursor;

			public int offsetX
			{
				get;
				private set;
			}

			public int offsetY
			{
				get;
				private set;
			}

			public int width
			{
				get;
				private set;
			}

			public int height
			{
				get;
				private set;
			}

			public static UIRAtlasAllocator.Row Acquire(int offsetX, int offsetY, int width, int height)
			{
				UIRAtlasAllocator.Row row = UIRAtlasAllocator.Row.s_Pool.Get();
				row.offsetX = offsetX;
				row.offsetY = offsetY;
				row.width = width;
				row.height = height;
				row.Cursor = 0;
				return row;
			}

			public void Release()
			{
				UIRAtlasAllocator.Row.s_Pool.Release(this);
				this.offsetX = -1;
				this.offsetY = -1;
				this.width = -1;
				this.height = -1;
				this.Cursor = -1;
			}
		}

		private class AreaNode
		{
			private static ObjectPool<UIRAtlasAllocator.AreaNode> s_Pool = new ObjectPool<UIRAtlasAllocator.AreaNode>(100);

			public RectInt rect;

			public UIRAtlasAllocator.AreaNode previous;

			public UIRAtlasAllocator.AreaNode next;

			public static UIRAtlasAllocator.AreaNode Acquire(RectInt rect)
			{
				UIRAtlasAllocator.AreaNode areaNode = UIRAtlasAllocator.AreaNode.s_Pool.Get();
				areaNode.rect = rect;
				areaNode.previous = null;
				areaNode.next = null;
				return areaNode;
			}

			public void Release()
			{
				UIRAtlasAllocator.AreaNode.s_Pool.Release(this);
			}

			public void RemoveFromChain()
			{
				bool flag = this.previous != null;
				if (flag)
				{
					this.previous.next = this.next;
				}
				bool flag2 = this.next != null;
				if (flag2)
				{
					this.next.previous = this.previous;
				}
				this.previous = null;
				this.next = null;
			}

			public void AddAfter(UIRAtlasAllocator.AreaNode previous)
			{
				Assert.IsNull<UIRAtlasAllocator.AreaNode>(this.previous);
				Assert.IsNull<UIRAtlasAllocator.AreaNode>(this.next);
				this.previous = previous;
				bool flag = previous != null;
				if (flag)
				{
					this.next = previous.next;
					previous.next = this;
				}
				bool flag2 = this.next != null;
				if (flag2)
				{
					this.next.previous = this;
				}
			}
		}

		private UIRAtlasAllocator.AreaNode m_FirstUnpartitionedArea;

		private UIRAtlasAllocator.Row[] m_OpenRows;

		private int m_1SidePadding;

		private int m_2SidePadding;

		private static ProfilerMarker s_MarkerTryAllocate = new ProfilerMarker("UIRAtlasAllocator.TryAllocate");

		public int maxAtlasSize
		{
			[CompilerGenerated]
			get
			{
				return this.<maxAtlasSize>k__BackingField;
			}
		}

		public int maxImageWidth
		{
			[CompilerGenerated]
			get
			{
				return this.<maxImageWidth>k__BackingField;
			}
		}

		public int maxImageHeight
		{
			[CompilerGenerated]
			get
			{
				return this.<maxImageHeight>k__BackingField;
			}
		}

		public int virtualWidth
		{
			get;
			private set;
		}

		public int virtualHeight
		{
			get;
			private set;
		}

		public int physicalWidth
		{
			get;
			private set;
		}

		public int physicalHeight
		{
			get;
			private set;
		}

		protected bool disposed
		{
			get;
			private set;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					for (int i = 0; i < this.m_OpenRows.Length; i++)
					{
						UIRAtlasAllocator.Row row = this.m_OpenRows[i];
						bool flag = row != null;
						if (flag)
						{
							row.Release();
						}
					}
					this.m_OpenRows = null;
					UIRAtlasAllocator.AreaNode next;
					for (UIRAtlasAllocator.AreaNode areaNode = this.m_FirstUnpartitionedArea; areaNode != null; areaNode = next)
					{
						next = areaNode.next;
						areaNode.Release();
					}
					this.m_FirstUnpartitionedArea = null;
				}
				this.disposed = true;
			}
		}

		private static int GetLog2OfNextPower(int n)
		{
			float f = (float)Mathf.NextPowerOfTwo(n);
			float f2 = Mathf.Log(f, 2f);
			return Mathf.RoundToInt(f2);
		}

		public UIRAtlasAllocator(int initialAtlasSize, int maxAtlasSize, int sidePadding = 1)
		{
			Assert.IsTrue(initialAtlasSize > 0 && initialAtlasSize <= maxAtlasSize);
			Assert.IsTrue(initialAtlasSize == Mathf.NextPowerOfTwo(initialAtlasSize));
			Assert.IsTrue(maxAtlasSize == Mathf.NextPowerOfTwo(maxAtlasSize));
			this.m_1SidePadding = sidePadding;
			this.m_2SidePadding = sidePadding << 1;
			this.<maxAtlasSize>k__BackingField = maxAtlasSize;
			this.<maxImageWidth>k__BackingField = maxAtlasSize;
			this.<maxImageHeight>k__BackingField = ((initialAtlasSize == maxAtlasSize) ? (maxAtlasSize / 2 + this.m_2SidePadding) : (maxAtlasSize / 4 + this.m_2SidePadding));
			this.virtualWidth = initialAtlasSize;
			this.virtualHeight = initialAtlasSize;
			int num = UIRAtlasAllocator.GetLog2OfNextPower(maxAtlasSize) + 1;
			this.m_OpenRows = new UIRAtlasAllocator.Row[num];
			RectInt rect = new RectInt(0, 0, initialAtlasSize, initialAtlasSize);
			this.m_FirstUnpartitionedArea = UIRAtlasAllocator.AreaNode.Acquire(rect);
			this.BuildAreas();
		}

		public bool TryAllocate(int width, int height, out RectInt location)
		{
			bool result;
			using (UIRAtlasAllocator.s_MarkerTryAllocate.Auto())
			{
				location = default(RectInt);
				bool disposed = this.disposed;
				if (disposed)
				{
					result = false;
				}
				else
				{
					bool flag = width < 1 || height < 1;
					if (flag)
					{
						result = false;
					}
					else
					{
						bool flag2 = width > this.maxImageWidth || height > this.maxImageHeight;
						if (flag2)
						{
							result = false;
						}
						else
						{
							int log2OfNextPower = UIRAtlasAllocator.GetLog2OfNextPower(Mathf.Max(height - this.m_2SidePadding, 1));
							int rowHeight = (1 << log2OfNextPower) + this.m_2SidePadding;
							UIRAtlasAllocator.Row row = this.m_OpenRows[log2OfNextPower];
							bool flag3 = row != null && row.width - row.Cursor < width;
							if (flag3)
							{
								row = null;
							}
							bool flag4 = row == null;
							if (flag4)
							{
								for (UIRAtlasAllocator.AreaNode areaNode = this.m_FirstUnpartitionedArea; areaNode != null; areaNode = areaNode.next)
								{
									bool flag5 = this.TryPartitionArea(areaNode, log2OfNextPower, rowHeight, width);
									if (flag5)
									{
										row = this.m_OpenRows[log2OfNextPower];
										break;
									}
								}
								bool flag6 = row == null;
								if (flag6)
								{
									result = false;
									return result;
								}
							}
							location = new RectInt(row.offsetX + row.Cursor, row.offsetY, width, height);
							row.Cursor += width;
							Assert.IsTrue(row.Cursor <= row.width);
							this.physicalWidth = Mathf.NextPowerOfTwo(Mathf.Max(this.physicalWidth, location.xMax));
							this.physicalHeight = Mathf.NextPowerOfTwo(Mathf.Max(this.physicalHeight, location.yMax));
							result = true;
						}
					}
				}
			}
			return result;
		}

		private bool TryPartitionArea(UIRAtlasAllocator.AreaNode areaNode, int rowIndex, int rowHeight, int minWidth)
		{
			RectInt rect = areaNode.rect;
			bool flag = rect.height < rowHeight || rect.width < minWidth;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				UIRAtlasAllocator.Row row = this.m_OpenRows[rowIndex];
				bool flag2 = row != null;
				if (flag2)
				{
					row.Release();
				}
				row = UIRAtlasAllocator.Row.Acquire(rect.x, rect.y, rect.width, rowHeight);
				this.m_OpenRows[rowIndex] = row;
				rect.y += rowHeight;
				rect.height -= rowHeight;
				bool flag3 = rect.height == 0;
				if (flag3)
				{
					bool flag4 = areaNode == this.m_FirstUnpartitionedArea;
					if (flag4)
					{
						this.m_FirstUnpartitionedArea = areaNode.next;
					}
					areaNode.RemoveFromChain();
					areaNode.Release();
				}
				else
				{
					areaNode.rect = rect;
				}
				result = true;
			}
			return result;
		}

		private void BuildAreas()
		{
			UIRAtlasAllocator.AreaNode previous = this.m_FirstUnpartitionedArea;
			while (this.virtualWidth < this.maxAtlasSize || this.virtualHeight < this.maxAtlasSize)
			{
				bool flag = this.virtualWidth > this.virtualHeight;
				RectInt rect;
				if (flag)
				{
					rect = new RectInt(0, this.virtualHeight, this.virtualWidth, this.virtualHeight);
					this.virtualHeight *= 2;
				}
				else
				{
					rect = new RectInt(this.virtualWidth, 0, this.virtualWidth, this.virtualHeight);
					this.virtualWidth *= 2;
				}
				UIRAtlasAllocator.AreaNode areaNode = UIRAtlasAllocator.AreaNode.Acquire(rect);
				areaNode.AddAfter(previous);
				previous = areaNode;
			}
		}
	}
}
