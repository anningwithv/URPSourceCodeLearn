using System;

namespace UnityEngine.VFX
{
	[Flags]
	internal enum VFXCullingFlags
	{
		CullNone = 0,
		CullSimulation = 1,
		CullBoundsUpdate = 2,
		CullDefault = 3
	}
}
