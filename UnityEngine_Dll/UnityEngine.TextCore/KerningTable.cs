using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore
{
	[Serializable]
	internal class KerningTable
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly KerningTable.<>c <>9 = new KerningTable.<>c();

			public static Func<KerningPair, uint> <>9__5_0;

			public static Func<KerningPair, uint> <>9__5_1;

			internal uint <SortKerningPairs>b__5_0(KerningPair s)
			{
				return s.firstGlyph;
			}

			internal uint <SortKerningPairs>b__5_1(KerningPair s)
			{
				return s.secondGlyph;
			}
		}

		public List<KerningPair> kerningPairs;

		public KerningTable()
		{
			this.kerningPairs = new List<KerningPair>();
		}

		public int AddGlyphPairAdjustmentRecord(uint first, GlyphValueRecord firstAdjustments, uint second, GlyphValueRecord secondAdjustments)
		{
			int num = this.kerningPairs.FindIndex((KerningPair item) => item.firstGlyph == first && item.secondGlyph == second);
			bool flag = num == -1;
			int result;
			if (flag)
			{
				this.kerningPairs.Add(new KerningPair(first, firstAdjustments, second, secondAdjustments));
				result = 0;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		public void RemoveKerningPair(int left, int right)
		{
			int num = this.kerningPairs.FindIndex((KerningPair item) => (ulong)item.firstGlyph == (ulong)((long)left) && (ulong)item.secondGlyph == (ulong)((long)right));
			bool flag = num != -1;
			if (flag)
			{
				this.kerningPairs.RemoveAt(num);
			}
		}

		public void RemoveKerningPair(int index)
		{
			this.kerningPairs.RemoveAt(index);
		}

		public void SortKerningPairs()
		{
			bool flag = this.kerningPairs.Count > 0;
			if (flag)
			{
				IEnumerable<KerningPair> arg_39_0 = this.kerningPairs;
				Func<KerningPair, uint> arg_39_1;
				if ((arg_39_1 = KerningTable.<>c.<>9__5_0) == null)
				{
					arg_39_1 = (KerningTable.<>c.<>9__5_0 = new Func<KerningPair, uint>(KerningTable.<>c.<>9.<SortKerningPairs>b__5_0));
				}
				IOrderedEnumerable<KerningPair> arg_5D_0 = arg_39_0.OrderBy(arg_39_1);
				Func<KerningPair, uint> arg_5D_1;
				if ((arg_5D_1 = KerningTable.<>c.<>9__5_1) == null)
				{
					arg_5D_1 = (KerningTable.<>c.<>9__5_1 = new Func<KerningPair, uint>(KerningTable.<>c.<>9.<SortKerningPairs>b__5_1));
				}
				this.kerningPairs = arg_5D_0.ThenBy(arg_5D_1).ToList<KerningPair>();
			}
		}
	}
}
