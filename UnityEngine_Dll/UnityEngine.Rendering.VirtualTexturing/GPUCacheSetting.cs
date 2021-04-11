using System;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing
{
	[NativeHeader("Modules/VirtualTexturing/Public/VirtualTexturingSettings.h"), UsedByNativeCode]
	[Serializable]
	public struct GPUCacheSetting
	{
		public GraphicsFormat format;

		public uint sizeInMegaBytes;
	}
}
