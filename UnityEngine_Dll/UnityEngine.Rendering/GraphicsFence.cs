using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[NativeHeader("Runtime/Graphics/GPUFence.h"), UsedByNativeCode]
	public struct GraphicsFence
	{
		internal IntPtr m_Ptr;

		internal int m_Version;

		internal GraphicsFenceType m_FenceType;

		public bool passed
		{
			get
			{
				this.Validate();
				bool flag = !SystemInfo.supportsGraphicsFence || (this.m_FenceType == GraphicsFenceType.AsyncQueueSynchronisation && !SystemInfo.supportsAsyncCompute);
				if (flag)
				{
					throw new NotSupportedException("Cannot determine if this GraphicsFence has passed as this platform has not implemented GraphicsFences.");
				}
				bool flag2 = !this.IsFencePending();
				return flag2 || GraphicsFence.HasFencePassed_Internal(this.m_Ptr);
			}
		}

		internal static SynchronisationStageFlags TranslateSynchronizationStageToFlags(SynchronisationStage s)
		{
			return (s == SynchronisationStage.VertexProcessing) ? SynchronisationStageFlags.VertexProcessing : SynchronisationStageFlags.PixelProcessing;
		}

		[FreeFunction("GPUFenceInternals::HasFencePassed_Internal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasFencePassed_Internal(IntPtr fencePtr);

		internal void InitPostAllocation()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				bool supportsGraphicsFence = SystemInfo.supportsGraphicsFence;
				if (supportsGraphicsFence)
				{
					throw new NullReferenceException("The internal fence ptr is null, this should not be possible for fences that have been correctly constructed using Graphics.CreateGraphicsFence() or CommandBuffer.CreateGraphicsFence()");
				}
				this.m_Version = this.GetPlatformNotSupportedVersion();
			}
			else
			{
				this.m_Version = GraphicsFence.GetVersionNumber(this.m_Ptr);
			}
		}

		internal bool IsFencePending()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			return !flag && this.m_Version == GraphicsFence.GetVersionNumber(this.m_Ptr);
		}

		internal void Validate()
		{
			bool flag = this.m_Version == 0 || (SystemInfo.supportsGraphicsFence && this.m_Version == this.GetPlatformNotSupportedVersion());
			if (flag)
			{
				throw new InvalidOperationException("This GraphicsFence object has not been correctly constructed see Graphics.CreateGraphicsFence() or CommandBuffer.CreateGraphicsFence()");
			}
		}

		private int GetPlatformNotSupportedVersion()
		{
			return -1;
		}

		[FreeFunction("GPUFenceInternals::GetVersionNumber"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVersionNumber(IntPtr fencePtr);
	}
}
