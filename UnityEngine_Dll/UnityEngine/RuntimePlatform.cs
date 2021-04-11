using System;
using System.ComponentModel;

namespace UnityEngine
{
	public enum RuntimePlatform
	{
		OSXEditor,
		OSXPlayer,
		WindowsPlayer,
		[Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)]
		OSXWebPlayer,
		[Obsolete("Dashboard widget on Mac OS X export is no longer supported in Unity 5.4+.", true)]
		OSXDashboardPlayer,
		[Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)]
		WindowsWebPlayer,
		WindowsEditor = 7,
		IPhonePlayer,
		[Obsolete("Xbox360 export is no longer supported in Unity 5.5+.")]
		XBOX360 = 10,
		[Obsolete("PS3 export is no longer supported in Unity >=5.5.")]
		PS3 = 9,
		Android = 11,
		[Obsolete("NaCl export is no longer supported in Unity 5.0+.")]
		NaCl,
		[Obsolete("FlashPlayer export is no longer supported in Unity 5.0+.")]
		FlashPlayer = 15,
		LinuxPlayer = 13,
		LinuxEditor = 16,
		WebGLPlayer,
		[Obsolete("Use WSAPlayerX86 instead")]
		MetroPlayerX86,
		WSAPlayerX86 = 18,
		[Obsolete("Use WSAPlayerX64 instead")]
		MetroPlayerX64,
		WSAPlayerX64 = 19,
		[Obsolete("Use WSAPlayerARM instead")]
		MetroPlayerARM,
		WSAPlayerARM = 20,
		[Obsolete("Windows Phone 8 was removed in 5.3")]
		WP8Player,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("BB10Player export is no longer supported in Unity 5.4+.")]
		BB10Player,
		[Obsolete("BlackBerryPlayer export is no longer supported in Unity 5.4+.")]
		BlackBerryPlayer = 22,
		[Obsolete("TizenPlayer export is no longer supported in Unity 2017.3+.")]
		TizenPlayer,
		[Obsolete("PSP2 is no longer supported as of Unity 2018.3")]
		PSP2,
		PS4,
		[Obsolete("PSM export is no longer supported in Unity >= 5.3")]
		PSM,
		XboxOne,
		[Obsolete("SamsungTVPlayer export is no longer supported in Unity 2017.3+.")]
		SamsungTVPlayer,
		[Obsolete("Wii U is no longer supported in Unity 2018.1+.")]
		WiiU = 30,
		tvOS,
		Switch,
		Lumin,
		Stadia,
		CloudRendering,
		GameCoreScarlett,
		GameCoreXboxOne,
		PS5
	}
}
