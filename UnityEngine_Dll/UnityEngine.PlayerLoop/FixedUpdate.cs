using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.PlayerLoop
{
	[MovedFrom("UnityEngine.Experimental.PlayerLoop"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct FixedUpdate
	{
		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ClearLines
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct DirectorFixedSampleTime
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct AudioFixedUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ScriptRunBehaviourFixedUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct DirectorFixedUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct LegacyFixedAnimationUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct XRFixedUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct PhysicsFixedUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct Physics2DFixedUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		private struct PhysicsClothFixedUpdate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct DirectorFixedUpdatePostPhysics
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ScriptRunDelayedFixedFrameRate
		{
		}

		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct NewInputFixedUpdate
		{
		}
	}
}
