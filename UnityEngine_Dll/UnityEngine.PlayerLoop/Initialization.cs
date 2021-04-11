using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.PlayerLoop
{
	[MovedFrom("UnityEngine.Experimental.PlayerLoop"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct Initialization
	{
		[Obsolete("PlayerUpdateTime player loop component has been moved to its own category called TimeUpdate. (UnityUpgradable) -> UnityEngine.PlayerLoop.TimeUpdate/WaitForLastPresentationAndUpdateTime", true)]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PlayerUpdateTime
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateCameraMotionVectors
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct DirectorSampleTime
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct AsyncUploadTimeSlicedUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct SynchronizeState
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct SynchronizeInputs
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct XREarlyUpdate
		{
		}
	}
}
