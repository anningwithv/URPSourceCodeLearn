using System;

namespace UnityEngine.Rendering
{
	[Flags]
	public enum ShaderPropertyFlags
	{
		None = 0,
		HideInInspector = 1,
		PerRendererData = 2,
		NoScaleOffset = 4,
		Normal = 8,
		HDR = 16,
		Gamma = 32,
		NonModifiableTextureData = 64,
		MainTexture = 128,
		MainColor = 256
	}
}
