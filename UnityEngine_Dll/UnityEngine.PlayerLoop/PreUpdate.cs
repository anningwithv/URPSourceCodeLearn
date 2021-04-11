using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.PlayerLoop
{
	[MovedFrom("UnityEngine.Experimental.PlayerLoop"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct PreUpdate
	{
		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PhysicsUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct Physics2DUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct CheckTexFieldInput
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct IMGUISendQueuedEvents
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct SendMouseEvents
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct AIUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct WindUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct UpdateVideo
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct NewInputUpdate
		{
		}
	}
}
