using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.PlayerLoop
{
	[MovedFrom("UnityEngine.Experimental.PlayerLoop"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct Update
	{
		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ScriptRunBehaviourUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct DirectorUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ScriptRunDelayedDynamicFrameRate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ScriptRunDelayedTasks
		{
		}
	}
}
