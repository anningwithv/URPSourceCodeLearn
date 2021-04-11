using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[NativeHeader("Runtime/Graphics/Texture.h"), NativeHeader("Runtime/Shaders/ComputeShader.h"), NativeHeader("Runtime/Graphics/AsyncGPUReadbackManaged.h"), UsedByNativeCode]
	public struct AsyncGPUReadbackRequest
	{
		internal IntPtr m_Ptr;

		internal int m_Version;

		public bool done
		{
			get
			{
				return this.IsDone();
			}
		}

		public bool hasError
		{
			get
			{
				return this.HasError();
			}
		}

		public int layerCount
		{
			get
			{
				return this.GetLayerCount();
			}
		}

		public int layerDataSize
		{
			get
			{
				return this.GetLayerDataSize();
			}
		}

		public int width
		{
			get
			{
				return this.GetWidth();
			}
		}

		public int height
		{
			get
			{
				return this.GetHeight();
			}
		}

		public int depth
		{
			get
			{
				return this.GetDepth();
			}
		}

		public void Update()
		{
			AsyncGPUReadbackRequest.Update_Injected(ref this);
		}

		public void WaitForCompletion()
		{
			AsyncGPUReadbackRequest.WaitForCompletion_Injected(ref this);
		}

		public unsafe NativeArray<T> GetData<T>(int layer = 0) where T : struct
		{
			bool flag = !this.done || this.hasError;
			if (flag)
			{
				throw new InvalidOperationException("Cannot access the data as it is not available");
			}
			bool flag2 = layer < 0 || layer >= this.layerCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Layer index is out of range {0} / {1}", layer, this.layerCount));
			}
			int num = UnsafeUtility.SizeOf<T>();
			NativeArray<T> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.GetDataRaw(layer), this.layerDataSize / num, Allocator.None);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, this.GetSafetyHandle());
			return result;
		}

		private bool IsDone()
		{
			return AsyncGPUReadbackRequest.IsDone_Injected(ref this);
		}

		private bool HasError()
		{
			return AsyncGPUReadbackRequest.HasError_Injected(ref this);
		}

		private int GetLayerCount()
		{
			return AsyncGPUReadbackRequest.GetLayerCount_Injected(ref this);
		}

		private int GetLayerDataSize()
		{
			return AsyncGPUReadbackRequest.GetLayerDataSize_Injected(ref this);
		}

		private int GetWidth()
		{
			return AsyncGPUReadbackRequest.GetWidth_Injected(ref this);
		}

		private int GetHeight()
		{
			return AsyncGPUReadbackRequest.GetHeight_Injected(ref this);
		}

		private int GetDepth()
		{
			return AsyncGPUReadbackRequest.GetDepth_Injected(ref this);
		}

		internal void CreateSafetyHandle()
		{
			AsyncGPUReadbackRequest.CreateSafetyHandle_Injected(ref this);
		}

		private AtomicSafetyHandle GetSafetyHandle()
		{
			AtomicSafetyHandle result;
			AsyncGPUReadbackRequest.GetSafetyHandle_Injected(ref this, out result);
			return result;
		}

		internal void SetScriptingCallback(Action<AsyncGPUReadbackRequest> callback)
		{
			AsyncGPUReadbackRequest.SetScriptingCallback_Injected(ref this, callback);
		}

		private IntPtr GetDataRaw(int layer)
		{
			return AsyncGPUReadbackRequest.GetDataRaw_Injected(ref this, layer);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Update_Injected(ref AsyncGPUReadbackRequest _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WaitForCompletion_Injected(ref AsyncGPUReadbackRequest _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsDone_Injected(ref AsyncGPUReadbackRequest _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasError_Injected(ref AsyncGPUReadbackRequest _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLayerCount_Injected(ref AsyncGPUReadbackRequest _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLayerDataSize_Injected(ref AsyncGPUReadbackRequest _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetWidth_Injected(ref AsyncGPUReadbackRequest _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetHeight_Injected(ref AsyncGPUReadbackRequest _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDepth_Injected(ref AsyncGPUReadbackRequest _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateSafetyHandle_Injected(ref AsyncGPUReadbackRequest _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSafetyHandle_Injected(ref AsyncGPUReadbackRequest _unity_self, out AtomicSafetyHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetScriptingCallback_Injected(ref AsyncGPUReadbackRequest _unity_self, Action<AsyncGPUReadbackRequest> callback);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDataRaw_Injected(ref AsyncGPUReadbackRequest _unity_self, int layer);
	}
}
