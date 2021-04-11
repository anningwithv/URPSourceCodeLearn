using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.UIR
{
	internal struct BitmapAllocator32
	{
		private struct Page
		{
			public ushort x;

			public ushort y;

			public int freeSlots;
		}

		public const int kPageWidth = 32;

		private int m_PageHeight;

		private List<BitmapAllocator32.Page> m_Pages;

		private List<uint> m_AllocMap;

		private int m_EntryWidth;

		private int m_EntryHeight;

		public int entryWidth
		{
			get
			{
				return this.m_EntryWidth;
			}
		}

		public int entryHeight
		{
			get
			{
				return this.m_EntryHeight;
			}
		}

		public void Construct(int pageHeight, int entryWidth = 1, int entryHeight = 1)
		{
			this.m_PageHeight = pageHeight;
			this.m_Pages = new List<BitmapAllocator32.Page>(1);
			this.m_AllocMap = new List<uint>(this.m_PageHeight * this.m_Pages.Capacity);
			this.m_EntryWidth = entryWidth;
			this.m_EntryHeight = entryHeight;
		}

		public void ForceFirstAlloc(ushort firstPageX, ushort firstPageY)
		{
			this.m_AllocMap.Add(4294967294u);
			for (int i = 1; i < this.m_PageHeight; i++)
			{
				this.m_AllocMap.Add(4294967295u);
			}
			this.m_Pages.Add(new BitmapAllocator32.Page
			{
				x = firstPageX,
				y = firstPageY,
				freeSlots = 32 * this.m_PageHeight - 1
			});
		}

		public BMPAlloc Allocate(UIRAtlasManager atlasManager)
		{
			int count = this.m_Pages.Count;
			BMPAlloc result;
			for (int i = 0; i < count; i++)
			{
				BitmapAllocator32.Page page = this.m_Pages[i];
				bool flag = page.freeSlots == 0;
				if (!flag)
				{
					int j = i * this.m_PageHeight;
					int num = j + this.m_PageHeight;
					while (j < num)
					{
						uint num2 = this.m_AllocMap[j];
						bool flag2 = num2 == 0u;
						if (!flag2)
						{
							byte b = BitmapAllocator32.CountTrailingZeroes(num2);
							this.m_AllocMap[j] = (num2 & ~(1u << (int)b));
							page.freeSlots--;
							this.m_Pages[i] = page;
							result = new BMPAlloc
							{
								page = i,
								pageLine = (ushort)(j - i * this.m_PageHeight),
								bitIndex = b,
								ownedState = OwnedState.Owned
							};
							return result;
						}
						j++;
					}
				}
			}
			RectInt rectInt;
			bool flag3 = atlasManager == null || !atlasManager.AllocateRect(32 * this.m_EntryWidth, this.m_PageHeight * this.m_EntryHeight, out rectInt);
			if (flag3)
			{
				result = BMPAlloc.Invalid;
				return result;
			}
			this.m_AllocMap.Capacity += this.m_PageHeight;
			this.m_AllocMap.Add(4294967294u);
			for (int k = 1; k < this.m_PageHeight; k++)
			{
				this.m_AllocMap.Add(4294967295u);
			}
			this.m_Pages.Add(new BitmapAllocator32.Page
			{
				x = (ushort)rectInt.xMin,
				y = (ushort)rectInt.yMin,
				freeSlots = 32 * this.m_PageHeight - 1
			});
			result = new BMPAlloc
			{
				page = this.m_Pages.Count - 1,
				ownedState = OwnedState.Owned
			};
			return result;
		}

		public void Free(BMPAlloc alloc)
		{
			Debug.Assert(alloc.ownedState == OwnedState.Owned);
			int index = alloc.page * this.m_PageHeight + (int)alloc.pageLine;
			this.m_AllocMap[index] = (this.m_AllocMap[index] | 1u << (int)alloc.bitIndex);
			BitmapAllocator32.Page value = this.m_Pages[alloc.page];
			value.freeSlots++;
			this.m_Pages[alloc.page] = value;
		}

		internal void GetAllocPageAtlasLocation(int page, out ushort x, out ushort y)
		{
			BitmapAllocator32.Page page2 = this.m_Pages[page];
			x = page2.x;
			y = page2.y;
		}

		private static byte CountTrailingZeroes(uint val)
		{
			byte b = 0;
			bool flag = (val & 65535u) == 0u;
			if (flag)
			{
				val >>= 16;
				b = 16;
			}
			bool flag2 = (val & 255u) == 0u;
			if (flag2)
			{
				val >>= 8;
				b += 8;
			}
			bool flag3 = (val & 15u) == 0u;
			if (flag3)
			{
				val >>= 4;
				b += 4;
			}
			bool flag4 = (val & 3u) == 0u;
			if (flag4)
			{
				val >>= 2;
				b += 2;
			}
			bool flag5 = (val & 1u) == 0u;
			if (flag5)
			{
				b += 1;
			}
			return b;
		}
	}
}
