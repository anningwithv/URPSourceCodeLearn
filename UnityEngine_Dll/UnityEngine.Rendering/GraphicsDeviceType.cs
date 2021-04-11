using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[UsedByNativeCode]
	public enum GraphicsDeviceType
	{
		[Obsolete("OpenGL2 is no longer supported in Unity 5.5+")]
		OpenGL2,
		[Obsolete("Direct3D 9 is no longer supported in Unity 2017.2+")]
		Direct3D9,
		Direct3D11,
		[Obsolete("PS3 is no longer supported in Unity 5.5+")]
		PlayStation3,
		Null,
		[Obsolete("Xbox360 is no longer supported in Unity 5.5+")]
		Xbox360 = 6,
		OpenGLES2 = 8,
		OpenGLES3 = 11,
		[Obsolete("PVita is no longer supported as of Unity 2018")]
		PlayStationVita,
		PlayStation4,
		XboxOne,
		[Obsolete("PlayStationMobile is no longer supported in Unity 5.3+")]
		PlayStationMobile,
		Metal,
		OpenGLCore,
		Direct3D12,
		[Obsolete("Nintendo 3DS support is unavailable since 2018.1")]
		N3DS,
		Vulkan = 21,
		Switch,
		XboxOneD3D12,
		GameCoreXboxOne,
		GameCoreScarlett,
		PlayStation5,
		PlayStation5NGGC
	}
}
