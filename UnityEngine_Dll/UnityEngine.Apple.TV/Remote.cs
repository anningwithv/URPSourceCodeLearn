using System;
using System.ComponentModel;

namespace UnityEngine.Apple.TV
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("UnityEngine.Apple.TV.Remote has been deprecated. Use UnityEngine.tvOS.Remote instead (UnityUpgradable) -> UnityEngine.tvOS.Remote", true)]
	public sealed class Remote
	{
		public static bool allowExitToHome
		{
			get;
			set;
		}

		public static bool allowRemoteRotation
		{
			get;
			set;
		}

		public static bool reportAbsoluteDpadValues
		{
			get;
			set;
		}

		public static bool touchesEnabled
		{
			get;
			set;
		}
	}
}
