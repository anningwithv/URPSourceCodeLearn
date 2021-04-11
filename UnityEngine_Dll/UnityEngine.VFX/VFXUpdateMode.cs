using System;

namespace UnityEngine.VFX
{
	[Flags]
	internal enum VFXUpdateMode
	{
		FixedDeltaTime = 0,
		DeltaTime = 1,
		IgnoreTimeScale = 2,
		ExactFixedTimeStep = 4,
		DeltaTimeAndIgnoreTimeScale = 3,
		FixedDeltaAndExactTime = 4,
		FixedDeltaAndExactTimeAndIgnoreTimeScale = 6
	}
}
