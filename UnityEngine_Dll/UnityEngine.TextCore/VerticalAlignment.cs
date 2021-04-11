using System;

namespace UnityEngine.TextCore
{
	[Flags]
	internal enum VerticalAlignment
	{
		Top = 256,
		Middle = 512,
		Bottom = 1024,
		Baseline = 2048,
		Midline = 4096,
		Capline = 8192
	}
}
