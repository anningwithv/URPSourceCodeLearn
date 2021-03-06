using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("iPhoneOrientation enumeration is deprecated. Please use DeviceOrientation instead (UnityUpgradable) -> DeviceOrientation", true)]
	public enum iPhoneOrientation
	{
		Unknown,
		Portrait,
		PortraitUpsideDown,
		LandscapeLeft,
		LandscapeRight,
		FaceUp,
		FaceDown
	}
}
