using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	[NativeHeader("Modules/UI/Canvas.h"), NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderContext.bindings.h"), NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableDrawRenderersUtility.h"), NativeHeader("Modules/UI/CanvasManager.h"), NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderPipeline.bindings.h"), NativeType("Runtime/Graphics/ScriptableRenderLoop/ScriptableRenderContext.h")]
	public struct ScriptableRenderContext : IEquatable<ScriptableRenderContext>
	{
		private static readonly ShaderTagId kRenderTypeTag = new ShaderTagId("RenderType");

		private IntPtr m_Ptr;

		private AtomicSafetyHandle m_Safety;

		[FreeFunction("ScriptableRenderContext::BeginRenderPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginRenderPass_Internal(IntPtr self, int width, int height, int samples, IntPtr colors, int colorCount, int depthAttachmentIndex);

		[FreeFunction("ScriptableRenderContext::BeginSubPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSubPass_Internal(IntPtr self, IntPtr colors, int colorCount, IntPtr inputs, int inputCount, bool isDepthReadOnly, bool isStencilReadOnly);

		[FreeFunction("ScriptableRenderContext::EndSubPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndSubPass_Internal(IntPtr self);

		[FreeFunction("ScriptableRenderContext::EndRenderPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndRenderPass_Internal(IntPtr self);

		[FreeFunction("ScriptableRenderPipeline_Bindings::Internal_Cull")]
		private static void Internal_Cull(ref ScriptableCullingParameters parameters, ScriptableRenderContext renderLoop, IntPtr results)
		{
			ScriptableRenderContext.Internal_Cull_Injected(ref parameters, ref renderLoop, results);
		}

		[FreeFunction("InitializeSortSettings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InitializeSortSettings(Camera camera, out SortingSettings sortingSettings);

		private void Submit_Internal()
		{
			ScriptableRenderContext.Submit_Internal_Injected(ref this);
		}

		private int GetNumberOfCameras_Internal()
		{
			return ScriptableRenderContext.GetNumberOfCameras_Internal_Injected(ref this);
		}

		private Camera GetCamera_Internal(int index)
		{
			return ScriptableRenderContext.GetCamera_Internal_Injected(ref this, index);
		}

		private void DrawRenderers_Internal(IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount)
		{
			ScriptableRenderContext.DrawRenderers_Internal_Injected(ref this, cullResults, ref drawingSettings, ref filteringSettings, ref tagName, isPassTagName, tagValues, stateBlocks, stateCount);
		}

		private void DrawShadows_Internal(IntPtr shadowDrawingSettings)
		{
			ScriptableRenderContext.DrawShadows_Internal_Injected(ref this, shadowDrawingSettings);
		}

		[FreeFunction("UI::GetCanvasManager().EmitWorldGeometryForSceneView")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EmitWorldGeometryForSceneView(Camera cullingCamera);

		[FreeFunction("UI::GetCanvasManager().EmitGeometryForCamera")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EmitGeometryForCamera(Camera camera);

		[NativeThrows]
		private void ExecuteCommandBuffer_Internal(CommandBuffer commandBuffer)
		{
			ScriptableRenderContext.ExecuteCommandBuffer_Internal_Injected(ref this, commandBuffer);
		}

		[NativeThrows]
		private void ExecuteCommandBufferAsync_Internal(CommandBuffer commandBuffer, ComputeQueueType queueType)
		{
			ScriptableRenderContext.ExecuteCommandBufferAsync_Internal_Injected(ref this, commandBuffer, queueType);
		}

		private void SetupCameraProperties_Internal([NotNull("NullExceptionObject")] Camera camera, bool stereoSetup, int eye)
		{
			ScriptableRenderContext.SetupCameraProperties_Internal_Injected(ref this, camera, stereoSetup, eye);
		}

		private void StereoEndRender_Internal([NotNull("NullExceptionObject")] Camera camera, int eye, bool isFinalPass)
		{
			ScriptableRenderContext.StereoEndRender_Internal_Injected(ref this, camera, eye, isFinalPass);
		}

		private void StartMultiEye_Internal([NotNull("NullExceptionObject")] Camera camera, int eye)
		{
			ScriptableRenderContext.StartMultiEye_Internal_Injected(ref this, camera, eye);
		}

		private void StopMultiEye_Internal([NotNull("NullExceptionObject")] Camera camera)
		{
			ScriptableRenderContext.StopMultiEye_Internal_Injected(ref this, camera);
		}

		private void DrawSkybox_Internal([NotNull("NullExceptionObject")] Camera camera)
		{
			ScriptableRenderContext.DrawSkybox_Internal_Injected(ref this, camera);
		}

		private void InvokeOnRenderObjectCallback_Internal()
		{
			ScriptableRenderContext.InvokeOnRenderObjectCallback_Internal_Injected(ref this);
		}

		private void DrawGizmos_Internal([NotNull("NullExceptionObject")] Camera camera, GizmoSubset gizmoSubset)
		{
			ScriptableRenderContext.DrawGizmos_Internal_Injected(ref this, camera, gizmoSubset);
		}

		private void DrawWireOverlay_Impl([NotNull("NullExceptionObject")] Camera camera)
		{
			ScriptableRenderContext.DrawWireOverlay_Impl_Injected(ref this, camera);
		}

		private void DrawUIOverlay_Internal([NotNull("NullExceptionObject")] Camera camera)
		{
			ScriptableRenderContext.DrawUIOverlay_Internal_Injected(ref this, camera);
		}

		internal IntPtr Internal_GetPtr()
		{
			return this.m_Ptr;
		}

		internal ScriptableRenderContext(IntPtr ptr, AtomicSafetyHandle safety)
		{
			this.m_Ptr = ptr;
			this.m_Safety = safety;
		}

		public void BeginRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex = -1)
		{
			this.Validate();
			ScriptableRenderContext.BeginRenderPass_Internal(this.m_Ptr, width, height, samples, (IntPtr)attachments.GetUnsafeReadOnlyPtr<AttachmentDescriptor>(), attachments.Length, depthAttachmentIndex);
		}

		public ScopedRenderPass BeginScopedRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex = -1)
		{
			this.BeginRenderPass(width, height, samples, attachments, depthAttachmentIndex);
			return new ScopedRenderPass(this);
		}

		public void BeginSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			this.Validate();
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, (IntPtr)inputs.GetUnsafeReadOnlyPtr<int>(), inputs.Length, isDepthReadOnly, isStencilReadOnly);
		}

		public void BeginSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthStencilReadOnly = false)
		{
			this.Validate();
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, (IntPtr)inputs.GetUnsafeReadOnlyPtr<int>(), inputs.Length, isDepthStencilReadOnly, isDepthStencilReadOnly);
		}

		public void BeginSubPass(NativeArray<int> colors, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			this.Validate();
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, IntPtr.Zero, 0, isDepthReadOnly, isStencilReadOnly);
		}

		public void BeginSubPass(NativeArray<int> colors, bool isDepthStencilReadOnly = false)
		{
			this.Validate();
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, IntPtr.Zero, 0, isDepthStencilReadOnly, isDepthStencilReadOnly);
		}

		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			this.BeginSubPass(colors, inputs, isDepthReadOnly, isStencilReadOnly);
			return new ScopedSubPass(this);
		}

		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthStencilReadOnly = false)
		{
			this.BeginSubPass(colors, inputs, isDepthStencilReadOnly);
			return new ScopedSubPass(this);
		}

		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			this.BeginSubPass(colors, isDepthReadOnly, isStencilReadOnly);
			return new ScopedSubPass(this);
		}

		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, bool isDepthStencilReadOnly = false)
		{
			this.BeginSubPass(colors, isDepthStencilReadOnly);
			return new ScopedSubPass(this);
		}

		public void EndSubPass()
		{
			this.Validate();
			ScriptableRenderContext.EndSubPass_Internal(this.m_Ptr);
		}

		public void EndRenderPass()
		{
			this.Validate();
			ScriptableRenderContext.EndRenderPass_Internal(this.m_Ptr);
		}

		public void Submit()
		{
			this.Validate();
			this.Submit_Internal();
		}

		internal int GetNumberOfCameras()
		{
			this.Validate();
			return this.GetNumberOfCameras_Internal();
		}

		internal Camera GetCamera(int index)
		{
			this.Validate();
			return this.GetCamera_Internal(index);
		}

		public void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings)
		{
			this.Validate();
			cullingResults.Validate();
			this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, ShaderTagId.none, false, IntPtr.Zero, IntPtr.Zero, 0);
		}

		public unsafe void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref RenderStateBlock stateBlock)
		{
			this.Validate();
			cullingResults.Validate();
			ShaderTagId shaderTagId = default(ShaderTagId);
			fixed (RenderStateBlock* ptr = &stateBlock)
			{
				RenderStateBlock* value = ptr;
				this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, ShaderTagId.none, false, (IntPtr)((void*)(&shaderTagId)), (IntPtr)((void*)value), 1);
			}
		}

		public void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, NativeArray<ShaderTagId> renderTypes, NativeArray<RenderStateBlock> stateBlocks)
		{
			this.Validate();
			cullingResults.Validate();
			bool flag = renderTypes.Length != stateBlocks.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("Arrays {0} and {1} should have same length, but {2} had length {3} while {4} had length {5}.", new object[]
				{
					"renderTypes",
					"stateBlocks",
					"renderTypes",
					renderTypes.Length,
					"stateBlocks",
					stateBlocks.Length
				}));
			}
			this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, ScriptableRenderContext.kRenderTypeTag, false, (IntPtr)renderTypes.GetUnsafeReadOnlyPtr<ShaderTagId>(), (IntPtr)stateBlocks.GetUnsafeReadOnlyPtr<RenderStateBlock>(), renderTypes.Length);
		}

		public void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ShaderTagId tagName, bool isPassTagName, NativeArray<ShaderTagId> tagValues, NativeArray<RenderStateBlock> stateBlocks)
		{
			this.Validate();
			cullingResults.Validate();
			bool flag = tagValues.Length != stateBlocks.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("Arrays {0} and {1} should have same length, but {2} had length {3} while {4} had length {5}.", new object[]
				{
					"tagValues",
					"stateBlocks",
					"tagValues",
					tagValues.Length,
					"stateBlocks",
					stateBlocks.Length
				}));
			}
			this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, tagName, isPassTagName, (IntPtr)tagValues.GetUnsafeReadOnlyPtr<ShaderTagId>(), (IntPtr)stateBlocks.GetUnsafeReadOnlyPtr<RenderStateBlock>(), tagValues.Length);
		}

		public unsafe void DrawShadows(ref ShadowDrawingSettings settings)
		{
			this.Validate();
			settings.cullingResults.Validate();
			fixed (ShadowDrawingSettings* ptr = &settings)
			{
				ShadowDrawingSettings* value = ptr;
				this.DrawShadows_Internal((IntPtr)((void*)value));
			}
		}

		public void ExecuteCommandBuffer(CommandBuffer commandBuffer)
		{
			bool flag = commandBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("commandBuffer");
			}
			this.Validate();
			this.ExecuteCommandBuffer_Internal(commandBuffer);
		}

		public void ExecuteCommandBufferAsync(CommandBuffer commandBuffer, ComputeQueueType queueType)
		{
			bool flag = commandBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("commandBuffer");
			}
			this.Validate();
			this.ExecuteCommandBufferAsync_Internal(commandBuffer, queueType);
		}

		public void SetupCameraProperties(Camera camera, bool stereoSetup = false)
		{
			this.SetupCameraProperties(camera, stereoSetup, 0);
		}

		public void SetupCameraProperties(Camera camera, bool stereoSetup, int eye)
		{
			this.Validate();
			this.SetupCameraProperties_Internal(camera, stereoSetup, eye);
		}

		public void StereoEndRender(Camera camera)
		{
			this.StereoEndRender(camera, 0, true);
		}

		public void StereoEndRender(Camera camera, int eye)
		{
			this.StereoEndRender(camera, eye, true);
		}

		public void StereoEndRender(Camera camera, int eye, bool isFinalPass)
		{
			this.Validate();
			this.StereoEndRender_Internal(camera, eye, isFinalPass);
		}

		public void StartMultiEye(Camera camera)
		{
			this.StartMultiEye(camera, 0);
		}

		public void StartMultiEye(Camera camera, int eye)
		{
			this.Validate();
			this.StartMultiEye_Internal(camera, eye);
		}

		public void StopMultiEye(Camera camera)
		{
			this.Validate();
			this.StopMultiEye_Internal(camera);
		}

		public void DrawSkybox(Camera camera)
		{
			this.Validate();
			this.DrawSkybox_Internal(camera);
		}

		public void InvokeOnRenderObjectCallback()
		{
			this.Validate();
			this.InvokeOnRenderObjectCallback_Internal();
		}

		public void DrawGizmos(Camera camera, GizmoSubset gizmoSubset)
		{
			this.Validate();
			this.DrawGizmos_Internal(camera, gizmoSubset);
		}

		public void DrawWireOverlay(Camera camera)
		{
			this.Validate();
			this.DrawWireOverlay_Impl(camera);
		}

		public void DrawUIOverlay(Camera camera)
		{
			this.Validate();
			this.DrawUIOverlay_Internal(camera);
		}

		public unsafe CullingResults Cull(ref ScriptableCullingParameters parameters)
		{
			CullingResults result = default(CullingResults);
			ScriptableRenderContext.Internal_Cull(ref parameters, this, (IntPtr)((void*)(&result)));
			return result;
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal void Validate()
		{
			bool flag = this.m_Ptr.ToInt64() == 0L;
			if (flag)
			{
				throw new InvalidOperationException("The ScriptableRenderContext instance is invalid. This can happen if you construct an instance using the default constructor.");
			}
			try
			{
				AtomicSafetyHandle.CheckExistsAndThrow(this.m_Safety);
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException("The ScriptableRenderContext instance is no longer valid. This can happen if you re-use it across multiple frames.", innerException);
			}
		}

		public bool Equals(ScriptableRenderContext other)
		{
			return this.m_Ptr.Equals(other.m_Ptr);
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ScriptableRenderContext && this.Equals((ScriptableRenderContext)obj);
		}

		public override int GetHashCode()
		{
			return this.m_Ptr.GetHashCode();
		}

		public static bool operator ==(ScriptableRenderContext left, ScriptableRenderContext right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ScriptableRenderContext left, ScriptableRenderContext right)
		{
			return !left.Equals(right);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Cull_Injected(ref ScriptableCullingParameters parameters, ref ScriptableRenderContext renderLoop, IntPtr results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Submit_Internal_Injected(ref ScriptableRenderContext _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNumberOfCameras_Internal_Injected(ref ScriptableRenderContext _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Camera GetCamera_Internal_Injected(ref ScriptableRenderContext _unity_self, int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawRenderers_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawShadows_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr shadowDrawingSettings);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExecuteCommandBuffer_Internal_Injected(ref ScriptableRenderContext _unity_self, CommandBuffer commandBuffer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExecuteCommandBufferAsync_Internal_Injected(ref ScriptableRenderContext _unity_self, CommandBuffer commandBuffer, ComputeQueueType queueType);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetupCameraProperties_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, bool stereoSetup, int eye);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StereoEndRender_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, int eye, bool isFinalPass);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StartMultiEye_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, int eye);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopMultiEye_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawSkybox_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InvokeOnRenderObjectCallback_Internal_Injected(ref ScriptableRenderContext _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawGizmos_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, GizmoSubset gizmoSubset);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawWireOverlay_Impl_Injected(ref ScriptableRenderContext _unity_self, Camera camera);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawUIOverlay_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera);
	}
}
