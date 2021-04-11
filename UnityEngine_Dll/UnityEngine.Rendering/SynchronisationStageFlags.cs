using System;

namespace UnityEngine.Rendering
{
	public enum SynchronisationStageFlags
	{
		VertexProcessing = 1,
		PixelProcessing,
		ComputeProcessing = 4,
		AllGPUOperations = 7
	}
}
