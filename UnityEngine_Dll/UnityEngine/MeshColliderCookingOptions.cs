using System;

namespace UnityEngine
{
	[Flags]
	public enum MeshColliderCookingOptions
	{
		None = 0,
		[Obsolete("No longer used because the problem this was trying to solve is gone since Unity 2018.3", true)]
		InflateConvexMesh = 1,
		CookForFasterSimulation = 2,
		EnableMeshCleaning = 4,
		WeldColocatedVertices = 8,
		UseFastMidphase = 16
	}
}
