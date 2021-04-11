using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h"), UsedByNativeCode]
	public struct BatchCullingContext
	{
		public readonly NativeArray<Plane> cullingPlanes;

		public NativeArray<BatchVisibility> batchVisibility;

		public NativeArray<int> visibleIndices;

		public NativeArray<int> visibleIndicesY;

		public readonly LODParameters lodParameters;

		public readonly Matrix4x4 cullingMatrix;

		public readonly float nearPlane;

		[Obsolete("For internal BatchRendererGroup use only")]
		public BatchCullingContext(NativeArray<Plane> inCullingPlanes, NativeArray<BatchVisibility> inOutBatchVisibility, NativeArray<int> outVisibleIndices, LODParameters inLodParameters)
		{
			this.cullingPlanes = inCullingPlanes;
			this.batchVisibility = inOutBatchVisibility;
			this.visibleIndices = outVisibleIndices;
			this.visibleIndicesY = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(null, 0, Allocator.Invalid);
			this.lodParameters = inLodParameters;
			this.cullingMatrix = Matrix4x4.identity;
			this.nearPlane = 0f;
		}

		[Obsolete("For internal BatchRendererGroup use only")]
		public BatchCullingContext(NativeArray<Plane> inCullingPlanes, NativeArray<BatchVisibility> inOutBatchVisibility, NativeArray<int> outVisibleIndices, LODParameters inLodParameters, Matrix4x4 inCullingMatrix, float inNearPlane)
		{
			this.cullingPlanes = inCullingPlanes;
			this.batchVisibility = inOutBatchVisibility;
			this.visibleIndices = outVisibleIndices;
			this.visibleIndicesY = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(null, 0, Allocator.Invalid);
			this.lodParameters = inLodParameters;
			this.cullingMatrix = inCullingMatrix;
			this.nearPlane = inNearPlane;
		}

		internal BatchCullingContext(NativeArray<Plane> inCullingPlanes, NativeArray<BatchVisibility> inOutBatchVisibility, NativeArray<int> outVisibleIndices, NativeArray<int> outVisibleIndicesY, LODParameters inLodParameters, Matrix4x4 inCullingMatrix, float inNearPlane)
		{
			this.cullingPlanes = inCullingPlanes;
			this.batchVisibility = inOutBatchVisibility;
			this.visibleIndices = outVisibleIndices;
			this.visibleIndicesY = outVisibleIndicesY;
			this.lodParameters = inLodParameters;
			this.cullingMatrix = inCullingMatrix;
			this.nearPlane = inNearPlane;
		}
	}
}
