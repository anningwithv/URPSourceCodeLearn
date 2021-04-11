using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h"), NativeHeader("Runtime/Math/Matrix4x4.h"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class BatchRendererGroup : IDisposable
	{
		public delegate JobHandle OnPerformCulling(BatchRendererGroup rendererGroup, BatchCullingContext cullingContext);

		private IntPtr m_GroupHandle = IntPtr.Zero;

		private BatchRendererGroup.OnPerformCulling m_PerformCulling;

		public BatchRendererGroup(BatchRendererGroup.OnPerformCulling cullingCallback)
		{
			this.m_PerformCulling = cullingCallback;
			this.m_GroupHandle = BatchRendererGroup.Create(this);
		}

		public void Dispose()
		{
			BatchRendererGroup.Destroy(this.m_GroupHandle);
			this.m_GroupHandle = IntPtr.Zero;
		}

		public int AddBatch(Mesh mesh, int subMeshIndex, Material material, int layer, ShadowCastingMode castShadows, bool receiveShadows, bool invertCulling, Bounds bounds, int instanceCount, MaterialPropertyBlock customProps, GameObject associatedSceneObject)
		{
			return this.AddBatch(mesh, subMeshIndex, material, layer, castShadows, receiveShadows, invertCulling, bounds, instanceCount, customProps, associatedSceneObject, 9223372036854775808uL, 4294967295u);
		}

		public int AddBatch(Mesh mesh, int subMeshIndex, Material material, int layer, ShadowCastingMode castShadows, bool receiveShadows, bool invertCulling, Bounds bounds, int instanceCount, MaterialPropertyBlock customProps, GameObject associatedSceneObject, ulong sceneCullingMask)
		{
			return this.AddBatch(mesh, subMeshIndex, material, layer, castShadows, receiveShadows, invertCulling, bounds, instanceCount, customProps, associatedSceneObject, sceneCullingMask, 4294967295u);
		}

		public int AddBatch(Mesh mesh, int subMeshIndex, Material material, int layer, ShadowCastingMode castShadows, bool receiveShadows, bool invertCulling, Bounds bounds, int instanceCount, MaterialPropertyBlock customProps, GameObject associatedSceneObject, ulong sceneCullingMask, uint renderingLayerMask)
		{
			return this.AddBatch_Injected(mesh, subMeshIndex, material, layer, castShadows, receiveShadows, invertCulling, ref bounds, instanceCount, customProps, associatedSceneObject, sceneCullingMask, renderingLayerMask);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBatchFlags(int batchIndex, ulong flags);

		public void SetBatchPropertyMetadata(int batchIndex, NativeArray<int> cbufferLengths, NativeArray<int> cbufferMetadata)
		{
			this.InternalSetBatchPropertyMetadata(batchIndex, (IntPtr)cbufferLengths.GetUnsafeReadOnlyPtr<int>(), cbufferLengths.Length, (IntPtr)cbufferMetadata.GetUnsafeReadOnlyPtr<int>(), cbufferMetadata.Length);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetBatchPropertyMetadata(int batchIndex, IntPtr cbufferLengths, int cbufferLengthsCount, IntPtr cbufferMetadata, int cbufferMetadataCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetInstancingData(int batchIndex, int instanceCount, MaterialPropertyBlock customProps);

		public unsafe NativeArray<Matrix4x4> GetBatchMatrices(int batchIndex)
		{
			int length = 0;
			void* batchMatrices = this.GetBatchMatrices(batchIndex, out length);
			NativeArray<Matrix4x4> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(batchMatrices, length, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<Matrix4x4>(ref result, this.GetMatricesSafetyHandle(batchIndex));
			return result;
		}

		public unsafe NativeArray<int> GetBatchScalarArrayInt(int batchIndex, string propertyName)
		{
			int length = 0;
			void* batchScalarArray = this.GetBatchScalarArray(batchIndex, propertyName, out length);
			NativeArray<int> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(batchScalarArray, length, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<int>(ref result, this.GetBatchArraySafetyHandle(batchIndex, propertyName));
			return result;
		}

		public unsafe NativeArray<float> GetBatchScalarArray(int batchIndex, string propertyName)
		{
			int length = 0;
			void* batchScalarArray = this.GetBatchScalarArray(batchIndex, propertyName, out length);
			NativeArray<float> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>(batchScalarArray, length, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<float>(ref result, this.GetBatchArraySafetyHandle(batchIndex, propertyName));
			return result;
		}

		public unsafe NativeArray<int> GetBatchVectorArrayInt(int batchIndex, string propertyName)
		{
			int length = 0;
			void* batchVectorArray = this.GetBatchVectorArray(batchIndex, propertyName, out length);
			NativeArray<int> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(batchVectorArray, length, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<int>(ref result, this.GetBatchArraySafetyHandle(batchIndex, propertyName));
			return result;
		}

		public unsafe NativeArray<Vector4> GetBatchVectorArray(int batchIndex, string propertyName)
		{
			int length = 0;
			void* batchVectorArray = this.GetBatchVectorArray(batchIndex, propertyName, out length);
			NativeArray<Vector4> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector4>(batchVectorArray, length, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<Vector4>(ref result, this.GetBatchArraySafetyHandle(batchIndex, propertyName));
			return result;
		}

		public unsafe NativeArray<Matrix4x4> GetBatchMatrixArray(int batchIndex, string propertyName)
		{
			int length = 0;
			void* batchMatrixArray = this.GetBatchMatrixArray(batchIndex, propertyName, out length);
			NativeArray<Matrix4x4> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(batchMatrixArray, length, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<Matrix4x4>(ref result, this.GetBatchArraySafetyHandle(batchIndex, propertyName));
			return result;
		}

		public unsafe NativeArray<int> GetBatchScalarArrayInt(int batchIndex, int propertyName)
		{
			int length = 0;
			void* batchScalarArray_Internal = this.GetBatchScalarArray_Internal(batchIndex, propertyName, out length);
			NativeArray<int> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(batchScalarArray_Internal, length, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<int>(ref result, this.GetBatchArraySafetyHandle_Int(batchIndex, propertyName));
			return result;
		}

		public unsafe NativeArray<float> GetBatchScalarArray(int batchIndex, int propertyName)
		{
			int length = 0;
			void* batchScalarArray_Internal = this.GetBatchScalarArray_Internal(batchIndex, propertyName, out length);
			NativeArray<float> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>(batchScalarArray_Internal, length, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<float>(ref result, this.GetBatchArraySafetyHandle_Int(batchIndex, propertyName));
			return result;
		}

		public unsafe NativeArray<int> GetBatchVectorArrayInt(int batchIndex, int propertyName)
		{
			int length = 0;
			void* batchVectorArray_Internal = this.GetBatchVectorArray_Internal(batchIndex, propertyName, out length);
			NativeArray<int> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(batchVectorArray_Internal, length, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<int>(ref result, this.GetBatchArraySafetyHandle_Int(batchIndex, propertyName));
			return result;
		}

		public unsafe NativeArray<Vector4> GetBatchVectorArray(int batchIndex, int propertyName)
		{
			int length = 0;
			void* batchVectorArray_Internal = this.GetBatchVectorArray_Internal(batchIndex, propertyName, out length);
			NativeArray<Vector4> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector4>(batchVectorArray_Internal, length, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<Vector4>(ref result, this.GetBatchArraySafetyHandle_Int(batchIndex, propertyName));
			return result;
		}

		public unsafe NativeArray<Matrix4x4> GetBatchMatrixArray(int batchIndex, int propertyName)
		{
			int length = 0;
			void* batchMatrixArray_Internal = this.GetBatchMatrixArray_Internal(batchIndex, propertyName, out length);
			NativeArray<Matrix4x4> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(batchMatrixArray_Internal, length, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<Matrix4x4>(ref result, this.GetBatchArraySafetyHandle_Int(batchIndex, propertyName));
			return result;
		}

		public void SetBatchBounds(int batchIndex, Bounds bounds)
		{
			this.SetBatchBounds_Injected(batchIndex, ref bounds);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetNumBatches();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveBatch(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* GetBatchMatrices(int batchIndex, out int matrixCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* GetBatchScalarArray(int batchIndex, string propertyName, out int elementCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* GetBatchVectorArray(int batchIndex, string propertyName, out int elementCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* GetBatchMatrixArray(int batchIndex, string propertyName, out int elementCount);

		[NativeName("GetBatchScalarArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* GetBatchScalarArray_Internal(int batchIndex, int propertyName, out int elementCount);

		[NativeName("GetBatchVectorArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* GetBatchVectorArray_Internal(int batchIndex, int propertyName, out int elementCount);

		[NativeName("GetBatchMatrixArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* GetBatchMatrixArray_Internal(int batchIndex, int propertyName, out int elementCount);

		private AtomicSafetyHandle GetMatricesSafetyHandle(int batchIndex)
		{
			AtomicSafetyHandle result;
			this.GetMatricesSafetyHandle_Injected(batchIndex, out result);
			return result;
		}

		private AtomicSafetyHandle GetBatchArraySafetyHandle(int batchIndex, string propertyName)
		{
			AtomicSafetyHandle result;
			this.GetBatchArraySafetyHandle_Injected(batchIndex, propertyName, out result);
			return result;
		}

		[NativeName("GetBatchArraySafetyHandle")]
		private AtomicSafetyHandle GetBatchArraySafetyHandle_Int(int batchIndex, int propertyName)
		{
			AtomicSafetyHandle result;
			this.GetBatchArraySafetyHandle_Int_Injected(batchIndex, propertyName, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnableVisibleIndicesYArray(bool enabled);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(BatchRendererGroup group);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr groupHandle);

		[RequiredByNativeCode]
		private unsafe static void InvokeOnPerformCulling(BatchRendererGroup group, ref BatchRendererCullingOutput context, ref LODParameters lodParameters)
		{
			NativeArray<Plane> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Plane>((void*)context.cullingPlanes, context.cullingPlanesCount, Allocator.Invalid);
			NativeArray<BatchVisibility> nativeArray2 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<BatchVisibility>((void*)context.batchVisibility, context.batchVisibilityCount, Allocator.Invalid);
			NativeArray<int> nativeArray3 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)context.visibleIndices, context.visibleIndicesCount, Allocator.Invalid);
			NativeArray<int> nativeArray4 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)context.visibleIndicesY, context.visibleIndicesCount, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<Plane>(ref nativeArray, AtomicSafetyHandle.Create());
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<BatchVisibility>(ref nativeArray2, AtomicSafetyHandle.Create());
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<int>(ref nativeArray3, AtomicSafetyHandle.Create());
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<int>(ref nativeArray4, AtomicSafetyHandle.Create());
			try
			{
				context.cullingJobsFence = group.m_PerformCulling(group, new BatchCullingContext(nativeArray, nativeArray2, nativeArray3, nativeArray4, lodParameters, context.cullingMatrix, context.nearPlane));
			}
			finally
			{
				JobHandle.ScheduleBatchedJobs();
				AtomicSafetyHandle.Release(NativeArrayUnsafeUtility.GetAtomicSafetyHandle<Plane>(nativeArray));
				AtomicSafetyHandle.Release(NativeArrayUnsafeUtility.GetAtomicSafetyHandle<BatchVisibility>(nativeArray2));
				AtomicSafetyHandle.Release(NativeArrayUnsafeUtility.GetAtomicSafetyHandle<int>(nativeArray3));
				AtomicSafetyHandle.Release(NativeArrayUnsafeUtility.GetAtomicSafetyHandle<int>(nativeArray4));
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int AddBatch_Injected(Mesh mesh, int subMeshIndex, Material material, int layer, ShadowCastingMode castShadows, bool receiveShadows, bool invertCulling, ref Bounds bounds, int instanceCount, MaterialPropertyBlock customProps, GameObject associatedSceneObject, ulong sceneCullingMask, uint renderingLayerMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBatchBounds_Injected(int batchIndex, ref Bounds bounds);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetMatricesSafetyHandle_Injected(int batchIndex, out AtomicSafetyHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetBatchArraySafetyHandle_Injected(int batchIndex, string propertyName, out AtomicSafetyHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetBatchArraySafetyHandle_Int_Injected(int batchIndex, int propertyName, out AtomicSafetyHandle ret);
	}
}
