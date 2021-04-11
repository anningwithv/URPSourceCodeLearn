using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.VirtualTexturing
{
	[NativeHeader("Modules/VirtualTexturing/Public/VirtualTexturingFilterMode.h")]
	public enum FilterMode
	{
		Bilinear = 1,
		Trilinear
	}
}
