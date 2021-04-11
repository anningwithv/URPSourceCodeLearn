using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Windows
{
	[NativeHeader("PlatformDependent/MetroPlayer/Bindings/ApplicationTrialBindings.h")]
	public static class LicenseInformation
	{
		public static extern bool isOnAppTrial
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PurchaseApp();
	}
}
