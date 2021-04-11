using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	public static class RenderPipelineManager
	{
		internal static RenderPipelineAsset s_CurrentPipelineAsset;

		private static Camera[] s_Cameras = new Camera[0];

		private static int s_CameraCapacity = 0;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<ScriptableRenderContext, Camera[]> beginFrameRendering;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<ScriptableRenderContext, Camera> beginCameraRendering;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<ScriptableRenderContext, Camera[]> endFrameRendering;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<ScriptableRenderContext, Camera> endCameraRendering;

		public static RenderPipeline currentPipeline
		{
			get;
			private set;
		}

		internal static void BeginFrameRendering(ScriptableRenderContext context, Camera[] cameras)
		{
			Action<ScriptableRenderContext, Camera[]> expr_06 = RenderPipelineManager.beginFrameRendering;
			if (expr_06 != null)
			{
				expr_06(context, cameras);
			}
		}

		internal static void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			Action<ScriptableRenderContext, Camera> expr_06 = RenderPipelineManager.beginCameraRendering;
			if (expr_06 != null)
			{
				expr_06(context, camera);
			}
		}

		internal static void EndFrameRendering(ScriptableRenderContext context, Camera[] cameras)
		{
			Action<ScriptableRenderContext, Camera[]> expr_06 = RenderPipelineManager.endFrameRendering;
			if (expr_06 != null)
			{
				expr_06(context, cameras);
			}
		}

		internal static void EndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			Action<ScriptableRenderContext, Camera> expr_06 = RenderPipelineManager.endCameraRendering;
			if (expr_06 != null)
			{
				expr_06(context, camera);
			}
		}

		[RequiredByNativeCode]
		internal static void CleanupRenderPipeline()
		{
			bool flag = RenderPipelineManager.currentPipeline != null && !RenderPipelineManager.currentPipeline.disposed;
			if (flag)
			{
				RenderPipelineManager.currentPipeline.Dispose();
				RenderPipelineManager.s_CurrentPipelineAsset = null;
				RenderPipelineManager.currentPipeline = null;
				SupportedRenderingFeatures.active = new SupportedRenderingFeatures();
			}
		}

		private static void GetCameras(ScriptableRenderContext context)
		{
			int numberOfCameras = context.GetNumberOfCameras();
			bool flag = numberOfCameras != RenderPipelineManager.s_CameraCapacity;
			if (flag)
			{
				Array.Resize<Camera>(ref RenderPipelineManager.s_Cameras, numberOfCameras);
				RenderPipelineManager.s_CameraCapacity = numberOfCameras;
			}
			for (int i = 0; i < numberOfCameras; i++)
			{
				RenderPipelineManager.s_Cameras[i] = context.GetCamera(i);
			}
		}

		[RequiredByNativeCode]
		private static void DoRenderLoop_Internal(RenderPipelineAsset pipe, IntPtr loopPtr, List<Camera.RenderRequest> renderRequests, AtomicSafetyHandle safety)
		{
			RenderPipelineManager.PrepareRenderPipeline(pipe);
			bool flag = RenderPipelineManager.currentPipeline == null;
			if (!flag)
			{
				ScriptableRenderContext context = new ScriptableRenderContext(loopPtr, safety);
				Array.Clear(RenderPipelineManager.s_Cameras, 0, RenderPipelineManager.s_Cameras.Length);
				RenderPipelineManager.GetCameras(context);
				bool flag2 = renderRequests == null;
				if (flag2)
				{
					RenderPipelineManager.currentPipeline.InternalRender(context, RenderPipelineManager.s_Cameras);
				}
				else
				{
					RenderPipelineManager.currentPipeline.InternalRenderWithRequests(context, RenderPipelineManager.s_Cameras, renderRequests);
				}
				Array.Clear(RenderPipelineManager.s_Cameras, 0, RenderPipelineManager.s_Cameras.Length);
			}
		}

		internal static void PrepareRenderPipeline(RenderPipelineAsset pipelineAsset)
		{
			bool flag = RenderPipelineManager.s_CurrentPipelineAsset != pipelineAsset;
			if (flag)
			{
				RenderPipelineManager.CleanupRenderPipeline();
				RenderPipelineManager.s_CurrentPipelineAsset = pipelineAsset;
			}
			bool flag2 = RenderPipelineManager.s_CurrentPipelineAsset != null && (RenderPipelineManager.currentPipeline == null || RenderPipelineManager.currentPipeline.disposed);
			if (flag2)
			{
				RenderPipelineManager.currentPipeline = RenderPipelineManager.s_CurrentPipelineAsset.InternalCreatePipeline();
			}
		}
	}
}
