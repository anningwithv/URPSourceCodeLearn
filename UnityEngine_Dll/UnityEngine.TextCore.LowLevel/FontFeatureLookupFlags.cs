using System;

namespace UnityEngine.TextCore.LowLevel
{
	[Flags]
	public enum FontFeatureLookupFlags
	{
		None = 0,
		IgnoreLigatures = 4,
		IgnoreSpacingAdjustments = 256
	}
}
