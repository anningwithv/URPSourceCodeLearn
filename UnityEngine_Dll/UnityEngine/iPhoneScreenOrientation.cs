using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("iPhoneScreenOrientation enumeration is deprecated. Please use ScreenOrientation instead (UnityUpgradable) -> ScreenOrientation", true)]
	public enum iPhoneScreenOrientation
	{
		Unknown,
		Portrait,
		PortraitUpsideDown,
		LandscapeLeft,
		LandscapeRight,
		AutoRotation,
		Landscape
	}
}
