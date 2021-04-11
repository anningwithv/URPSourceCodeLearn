using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements.UIR
{
	[NativeHeader("Modules/UIElementsNative/UIRendererUtility.h"), VisibleToOtherModules(new string[]
	{
		"Unity.UIElements"
	})]
	internal class Utility
	{
		[Flags]
		internal enum RendererCallbacks
		{
			RendererCallback_Init = 1,
			RendererCallback_Exec = 2,
			RendererCallback_Cleanup = 4
		}

		internal enum GPUBufferType
		{
			Vertex,
			Index
		}

		public class GPUBuffer<T> : IDisposable where T : struct
		{
			private IntPtr buffer;

			private int elemCount;

			private int elemStride;

			public int ElementStride
			{
				get
				{
					return this.elemStride;
				}
			}

			public int Count
			{
				get
				{
					return this.elemCount;
				}
			}

			internal IntPtr BufferPointer
			{
				get
				{
					return this.buffer;
				}
			}

			public GPUBuffer(int elementCount, Utility.GPUBufferType type)
			{
				this.elemCount = elementCount;
				this.elemStride = UnsafeUtility.SizeOf<T>();
				this.buffer = Utility.AllocateBuffer(elementCount, this.elemStride, type == Utility.GPUBufferType.Vertex);
			}

			public void Dispose()
			{
				Utility.FreeBuffer(this.buffer);
			}

			public void UpdateRanges(NativeSlice<GfxUpdateBufferRange> ranges, int rangesMin, int rangesMax)
			{
				Utility.UpdateBufferRanges(this.buffer, new IntPtr(ranges.GetUnsafePtr<GfxUpdateBufferRange>()), ranges.Length, rangesMin, rangesMax);
			}
		}

		private static ProfilerMarker s_MarkerRaiseEngineUpdate = new ProfilerMarker("UIR.RaiseEngineUpdate");

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<bool> GraphicsResourcesRecreate;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action EngineUpdate;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action FlushPendingResources;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<Camera> RegisterIntermediateRenderers;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<IntPtr> RenderNodeAdd;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<IntPtr> RenderNodeExecute;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<IntPtr> RenderNodeCleanup;

		public static void SetVectorArray<T>(MaterialPropertyBlock props, int name, NativeSlice<T> vector4s) where T : struct
		{
			int count = vector4s.Length * vector4s.Stride / 16;
			Utility.SetVectorArray(props, name, new IntPtr(vector4s.GetUnsafePtr<T>()), count);
		}

		[RequiredByNativeCode]
		internal static void RaiseGraphicsResourcesRecreate(bool recreate)
		{
			Action<bool> expr_06 = Utility.GraphicsResourcesRecreate;
			if (expr_06 != null)
			{
				expr_06(recreate);
			}
		}

		[RequiredByNativeCode]
		internal static void RaiseEngineUpdate()
		{
			bool flag = Utility.EngineUpdate != null;
			if (flag)
			{
				Utility.s_MarkerRaiseEngineUpdate.Begin();
				Utility.EngineUpdate();
				Utility.s_MarkerRaiseEngineUpdate.End();
			}
		}

		[RequiredByNativeCode]
		internal static void RaiseFlushPendingResources()
		{
			Action expr_06 = Utility.FlushPendingResources;
			if (expr_06 != null)
			{
				expr_06();
			}
		}

		[RequiredByNativeCode]
		internal static void RaiseRegisterIntermediateRenderers(Camera camera)
		{
			Action<Camera> expr_06 = Utility.RegisterIntermediateRenderers;
			if (expr_06 != null)
			{
				expr_06(camera);
			}
		}

		[RequiredByNativeCode]
		internal static void RaiseRenderNodeAdd(IntPtr userData)
		{
			Action<IntPtr> expr_06 = Utility.RenderNodeAdd;
			if (expr_06 != null)
			{
				expr_06(userData);
			}
		}

		[RequiredByNativeCode]
		internal static void RaiseRenderNodeExecute(IntPtr userData)
		{
			Action<IntPtr> expr_06 = Utility.RenderNodeExecute;
			if (expr_06 != null)
			{
				expr_06(userData);
			}
		}

		[RequiredByNativeCode]
		internal static void RaiseRenderNodeCleanup(IntPtr userData)
		{
			Action<IntPtr> expr_06 = Utility.RenderNodeCleanup;
			if (expr_06 != null)
			{
				expr_06(userData);
			}
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr AllocateBuffer(int elementCount, int elementStride, bool vertexBuffer);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FreeBuffer(IntPtr buffer);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateBufferRanges(IntPtr buffer, IntPtr ranges, int rangeCount, int writeRangeStart, int writeRangeEnd);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVectorArray(MaterialPropertyBlock props, int name, IntPtr vector4s, int count);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetVertexDeclaration(VertexAttributeDescriptor[] vertexAttributes);

		public static void RegisterIntermediateRenderer(Camera camera, Material material, Matrix4x4 transform, Bounds aabb, int renderLayer, int shadowCasting, bool receiveShadows, int sameDistanceSortPriority, ulong sceneCullingMask, int rendererCallbackFlags, IntPtr userData, int userDataSize)
		{
			Utility.RegisterIntermediateRenderer_Injected(camera, material, ref transform, ref aabb, renderLayer, shadowCasting, receiveShadows, sameDistanceSortPriority, sceneCullingMask, rendererCallbackFlags, userData, userDataSize);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void DrawRanges(IntPtr ib, IntPtr* vertexStreams, int streamCount, IntPtr ranges, int rangeCount, IntPtr vertexDecl);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetPropertyBlock(MaterialPropertyBlock props);

		[ThreadSafe]
		public static void SetScissorRect(RectInt scissorRect)
		{
			Utility.SetScissorRect_Injected(ref scissorRect);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DisableScissor();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsScissorEnabled();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint InsertCPUFence();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CPUFencePassed(uint fence);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WaitForCPUFencePassed(uint fence);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SyncRenderThread();

		[ThreadSafe]
		public static RectInt GetActiveViewport()
		{
			RectInt result;
			Utility.GetActiveViewport_Injected(out result);
			return result;
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ProfileDrawChainBegin();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ProfileDrawChainEnd();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ProfileImmediateRendererBegin();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ProfileImmediateRendererEnd();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void NotifyOfUIREvents(bool subscribe);

		[ThreadSafe]
		public static Matrix4x4 GetUnityProjectionMatrix()
		{
			Matrix4x4 result;
			Utility.GetUnityProjectionMatrix_Injected(out result);
			return result;
		}

		[ThreadSafe]
		public static Matrix4x4 GetDeviceProjectionMatrix()
		{
			Matrix4x4 result;
			Utility.GetDeviceProjectionMatrix_Injected(out result);
			return result;
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool DebugIsMainThread();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterIntermediateRenderer_Injected(Camera camera, Material material, ref Matrix4x4 transform, ref Bounds aabb, int renderLayer, int shadowCasting, bool receiveShadows, int sameDistanceSortPriority, ulong sceneCullingMask, int rendererCallbackFlags, IntPtr userData, int userDataSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetScissorRect_Injected(ref RectInt scissorRect);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveViewport_Injected(out RectInt ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetUnityProjectionMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDeviceProjectionMatrix_Injected(out Matrix4x4 ret);
	}
}
