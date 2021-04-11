using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.PlayerLoop
{
	[MovedFrom("UnityEngine.Experimental.PlayerLoop"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct PostLateUpdate
	{
		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PlayerSendFrameStarted
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateRectTransform
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateCanvasRectTransform
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PlayerUpdateCanvases
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateAudio
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateVideo
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct DirectorLateUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ScriptRunDelayedDynamicFrameRate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct VFXUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ParticleSystemEndUpdateAll
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct EndGraphicsJobsAfterScriptLateUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateSubstance
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateCustomRenderTextures
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateAllRenderers
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateLightProbeProxyVolumes
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct EnlightenRuntimeUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateAllSkinnedMeshes
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ProcessWebSendMessages
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct SortingGroupsUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateVideoTextures
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct DirectorRenderImage
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PlayerEmitCanvasGeometry
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct FinishFrameRendering
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct BatchModeUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PlayerSendFrameComplete
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateCaptureScreenshot
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PresentAfterDraw
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ClearImmediateRenderers
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct XRPostPresent
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateResolution
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct InputEndFrame
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct GUIClearEvents
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ShaderHandleErrors
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ResetInputAxis
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ThreadedLoadingDebug
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ProfilerSynchronizeStats
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct MemoryFrameMaintenance
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ExecuteGameCenterCallbacks
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ProfilerEndFrame
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PlayerSendFramePostPresent
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PhysicsSkinnedClothBeginUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PhysicsSkinnedClothFinishUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct TriggerEndOfFrameCallbacks
		{
		}
	}
}
