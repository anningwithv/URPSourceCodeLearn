using System;
using UnityEngine.Scripting;

namespace UnityEngine.Video
{
	[RequiredByNativeCode]
	public enum VideoRenderMode
	{
		CameraFarPlane,
		CameraNearPlane,
		RenderTexture,
		MaterialOverride,
		APIOnly
	}
}
