using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	[NativeHeader("Runtime/Scripting/ScriptingCommonStructDefinitions.h"), NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableCulling.h"), NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderPipeline.bindings.h")]
	public struct CullingResults : IEquatable<CullingResults>
	{
		internal IntPtr ptr;

		private unsafe CullingAllocationInfo* m_AllocationInfo;

		private AtomicSafetyHandle m_Safety;

		public unsafe NativeArray<VisibleLight> visibleLights
		{
			get
			{
				return this.GetNativeArray<VisibleLight>((void*)this.m_AllocationInfo->visibleLightsPtr, this.m_AllocationInfo->visibleLightCount);
			}
		}

		public unsafe NativeArray<VisibleLight> visibleOffscreenVertexLights
		{
			get
			{
				return this.GetNativeArray<VisibleLight>((void*)this.m_AllocationInfo->visibleOffscreenVertexLightsPtr, this.m_AllocationInfo->visibleOffscreenVertexLightCount);
			}
		}

		public unsafe NativeArray<VisibleReflectionProbe> visibleReflectionProbes
		{
			get
			{
				return this.GetNativeArray<VisibleReflectionProbe>((void*)this.m_AllocationInfo->visibleReflectionProbesPtr, this.m_AllocationInfo->visibleReflectionProbeCount);
			}
		}

		public int lightIndexCount
		{
			get
			{
				this.Validate();
				return CullingResults.GetLightIndexCount(this.ptr);
			}
		}

		public int reflectionProbeIndexCount
		{
			get
			{
				this.Validate();
				return CullingResults.GetReflectionProbeIndexCount(this.ptr);
			}
		}

		public int lightAndReflectionProbeIndexCount
		{
			get
			{
				this.Validate();
				return CullingResults.GetLightIndexCount(this.ptr) + CullingResults.GetReflectionProbeIndexCount(this.ptr);
			}
		}

		[FreeFunction("ScriptableRenderPipeline_Bindings::GetLightIndexCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLightIndexCount(IntPtr cullingResultsPtr);

		[FreeFunction("ScriptableRenderPipeline_Bindings::GetReflectionProbeIndexCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetReflectionProbeIndexCount(IntPtr cullingResultsPtr);

		[FreeFunction("FillLightAndReflectionProbeIndices")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FillLightAndReflectionProbeIndices(IntPtr cullingResultsPtr, ComputeBuffer computeBuffer);

		[FreeFunction("FillLightAndReflectionProbeIndices")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FillLightAndReflectionProbeIndicesGraphicsBuffer(IntPtr cullingResultsPtr, GraphicsBuffer buffer);

		[FreeFunction("GetLightIndexMapSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLightIndexMapSize(IntPtr cullingResultsPtr);

		[FreeFunction("GetReflectionProbeIndexMapSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetReflectionProbeIndexMapSize(IntPtr cullingResultsPtr);

		[FreeFunction("FillLightIndexMapScriptable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FillLightIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		[FreeFunction("FillReflectionProbeIndexMapScriptable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FillReflectionProbeIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		[FreeFunction("SetLightIndexMapScriptable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLightIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		[FreeFunction("SetReflectionProbeIndexMapScriptable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetReflectionProbeIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		[FreeFunction("ScriptableRenderPipeline_Bindings::GetShadowCasterBounds")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetShadowCasterBounds(IntPtr cullingResultsPtr, int lightIndex, out Bounds bounds);

		[FreeFunction("ScriptableRenderPipeline_Bindings::ComputeSpotShadowMatricesAndCullingPrimitives")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ComputeSpotShadowMatricesAndCullingPrimitives(IntPtr cullingResultsPtr, int activeLightIndex, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData);

		[FreeFunction("ScriptableRenderPipeline_Bindings::ComputePointShadowMatricesAndCullingPrimitives")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ComputePointShadowMatricesAndCullingPrimitives(IntPtr cullingResultsPtr, int activeLightIndex, CubemapFace cubemapFace, float fovBias, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData);

		[FreeFunction("ScriptableRenderPipeline_Bindings::ComputeDirectionalShadowMatricesAndCullingPrimitives")]
		private static bool ComputeDirectionalShadowMatricesAndCullingPrimitives(IntPtr cullingResultsPtr, int activeLightIndex, int splitIndex, int splitCount, Vector3 splitRatio, int shadowResolution, float shadowNearPlaneOffset, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return CullingResults.ComputeDirectionalShadowMatricesAndCullingPrimitives_Injected(cullingResultsPtr, activeLightIndex, splitIndex, splitCount, ref splitRatio, shadowResolution, shadowNearPlaneOffset, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		private unsafe NativeArray<T> GetNativeArray<T>(void* dataPointer, int length) where T : struct
		{
			NativeArray<T> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(dataPointer, length, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, this.m_Safety);
			return result;
		}

		public void FillLightAndReflectionProbeIndices(ComputeBuffer computeBuffer)
		{
			this.Validate();
			CullingResults.FillLightAndReflectionProbeIndices(this.ptr, computeBuffer);
		}

		public void FillLightAndReflectionProbeIndices(GraphicsBuffer buffer)
		{
			this.Validate();
			CullingResults.FillLightAndReflectionProbeIndicesGraphicsBuffer(this.ptr, buffer);
		}

		public NativeArray<int> GetLightIndexMap(Allocator allocator)
		{
			this.Validate();
			int lightIndexMapSize = CullingResults.GetLightIndexMapSize(this.ptr);
			NativeArray<int> nativeArray = new NativeArray<int>(lightIndexMapSize, allocator, NativeArrayOptions.UninitializedMemory);
			CullingResults.FillLightIndexMap(this.ptr, (IntPtr)nativeArray.GetUnsafePtr<int>(), lightIndexMapSize);
			return nativeArray;
		}

		public void SetLightIndexMap(NativeArray<int> lightIndexMap)
		{
			this.Validate();
			CullingResults.SetLightIndexMap(this.ptr, (IntPtr)lightIndexMap.GetUnsafeReadOnlyPtr<int>(), lightIndexMap.Length);
		}

		public NativeArray<int> GetReflectionProbeIndexMap(Allocator allocator)
		{
			this.Validate();
			int reflectionProbeIndexMapSize = CullingResults.GetReflectionProbeIndexMapSize(this.ptr);
			NativeArray<int> nativeArray = new NativeArray<int>(reflectionProbeIndexMapSize, allocator, NativeArrayOptions.UninitializedMemory);
			CullingResults.FillReflectionProbeIndexMap(this.ptr, (IntPtr)nativeArray.GetUnsafePtr<int>(), reflectionProbeIndexMapSize);
			return nativeArray;
		}

		public void SetReflectionProbeIndexMap(NativeArray<int> lightIndexMap)
		{
			this.Validate();
			CullingResults.SetReflectionProbeIndexMap(this.ptr, (IntPtr)lightIndexMap.GetUnsafeReadOnlyPtr<int>(), lightIndexMap.Length);
		}

		public bool GetShadowCasterBounds(int lightIndex, out Bounds outBounds)
		{
			this.Validate();
			return CullingResults.GetShadowCasterBounds(this.ptr, lightIndex, out outBounds);
		}

		public bool ComputeSpotShadowMatricesAndCullingPrimitives(int activeLightIndex, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			this.Validate();
			return CullingResults.ComputeSpotShadowMatricesAndCullingPrimitives(this.ptr, activeLightIndex, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		public bool ComputePointShadowMatricesAndCullingPrimitives(int activeLightIndex, CubemapFace cubemapFace, float fovBias, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			this.Validate();
			return CullingResults.ComputePointShadowMatricesAndCullingPrimitives(this.ptr, activeLightIndex, cubemapFace, fovBias, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		public bool ComputeDirectionalShadowMatricesAndCullingPrimitives(int activeLightIndex, int splitIndex, int splitCount, Vector3 splitRatio, int shadowResolution, float shadowNearPlaneOffset, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			this.Validate();
			return CullingResults.ComputeDirectionalShadowMatricesAndCullingPrimitives(this.ptr, activeLightIndex, splitIndex, splitCount, splitRatio, shadowResolution, shadowNearPlaneOffset, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal void Validate()
		{
			bool flag = this.ptr == IntPtr.Zero;
			if (flag)
			{
				throw new InvalidOperationException("The CullingResults instance is invalid. This can happen if you construct an instance using the default constructor.");
			}
			try
			{
				AtomicSafetyHandle.CheckExistsAndThrow(this.m_Safety);
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException("The CullingResults instance is no longer valid. This can happen if you re-use it across multiple frames.", innerException);
			}
		}

		public bool Equals(CullingResults other)
		{
			return this.ptr.Equals(other.ptr) && this.m_AllocationInfo == other.m_AllocationInfo;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is CullingResults && this.Equals((CullingResults)obj);
		}

		public override int GetHashCode()
		{
			int hashCode = this.ptr.GetHashCode();
			return hashCode * 397 ^ this.m_AllocationInfo;
		}

		public static bool operator ==(CullingResults left, CullingResults right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(CullingResults left, CullingResults right)
		{
			return !left.Equals(right);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ComputeDirectionalShadowMatricesAndCullingPrimitives_Injected(IntPtr cullingResultsPtr, int activeLightIndex, int splitIndex, int splitCount, ref Vector3 splitRatio, int shadowResolution, float shadowNearPlaneOffset, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData);
	}
}
