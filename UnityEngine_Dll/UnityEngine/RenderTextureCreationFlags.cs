using System;

namespace UnityEngine
{
	[Flags]
	public enum RenderTextureCreationFlags
	{
		MipMap = 1,
		AutoGenerateMips = 2,
		SRGB = 4,
		EyeTexture = 8,
		EnableRandomWrite = 16,
		CreatedFromScript = 32,
		AllowVerticalFlip = 128,
		NoResolvedColorSurface = 256,
		DynamicallyScalable = 1024,
		BindMS = 2048
	}
}
