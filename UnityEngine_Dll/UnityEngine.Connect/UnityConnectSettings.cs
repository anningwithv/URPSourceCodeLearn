using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Connect
{
	[NativeHeader("Modules/UnityConnect/UnityConnectSettings.h")]
	internal class UnityConnectSettings : UnityEngine.Object
	{
		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		public static extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		public static extern bool testMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		public static extern string eventUrl
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		public static extern string eventOldUrl
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		public static extern string configUrl
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		public static extern int testInitMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
