using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.PlayerLoop
{
	[MovedFrom("UnityEngine.Experimental.PlayerLoop"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct EarlyUpdate
	{
		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PollPlayerConnection
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ProfilerStartFrame
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PollHtcsPlayerConnection
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct GpuTimestamp
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct AnalyticsCoreStatsUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UnityWebRequestUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateStreamingManager
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ExecuteMainThreadJobs
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ProcessMouseInWindow
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ClearIntermediateRenderers
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ClearLines
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PresentBeforeUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ResetFrameStatsAfterPresent
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateAsyncReadbackManager
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateTextureStreamingManager
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdatePreloading
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct RendererNotifyInvisible
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PlayerCleanupCachedData
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateMainGameViewRect
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateCanvasRectTransform
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateInputManager
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ProcessRemoteInput
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct XRUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ScriptRunDelayedStartupFrame
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateKinect
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct DeliverIosPlatformEvents
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct DispatchEventQueueEvents
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PhysicsResetInterpolatedTransformPosition
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct SpriteAtlasManagerUpdate
		{
		}

		[Obsolete("TangoUpdate has been deprecated. Use ARCoreUpdate instead (UnityUpgradable) -> UnityEngine.PlayerLoop.EarlyUpdate/ARCoreUpdate", false), RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct TangoUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ARCoreUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PerformanceAnalyticsUpdate
		{
		}
	}
}
