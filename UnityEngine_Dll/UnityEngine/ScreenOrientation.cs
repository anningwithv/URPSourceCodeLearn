using System;

namespace UnityEngine
{
	public enum ScreenOrientation
	{
		[Obsolete("Enum member Unknown has been deprecated.", false)]
		Unknown,
		Portrait,
		PortraitUpsideDown,
		LandscapeLeft,
		LandscapeRight,
		AutoRotation,
		Landscape = 3
	}
}
