using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering
{
	[Obsolete("GPUFence has been deprecated. Use GraphicsFence instead (UnityUpgradable) -> GraphicsFence", false)]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct GPUFence
	{
		public bool passed
		{
			get
			{
				return true;
			}
		}
	}
}
