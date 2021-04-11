using System;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	[UsedByNativeCode]
	public enum GlyphPackingMode
	{
		BestShortSideFit,
		BestLongSideFit,
		BestAreaFit,
		BottomLeftRule,
		ContactPointRule
	}
}
