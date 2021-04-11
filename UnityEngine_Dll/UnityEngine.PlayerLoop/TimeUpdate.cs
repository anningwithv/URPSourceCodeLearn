using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.PlayerLoop
{
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct TimeUpdate
	{
		[RequiredByNativeCode]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct WaitForLastPresentationAndUpdateTime
		{
		}
	}
}
