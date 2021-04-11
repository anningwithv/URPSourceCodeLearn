using System;

namespace UnityEngine.VFX
{
	internal enum VFXSystemFlag
	{
		SystemDefault,
		SystemHasKill,
		SystemHasIndirectBuffer,
		SystemReceivedEventGPU = 4,
		SystemHasStrips = 8
	}
}
