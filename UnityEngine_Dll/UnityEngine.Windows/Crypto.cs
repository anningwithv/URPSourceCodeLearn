using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Windows
{
	public static class Crypto
	{
		[NativeHeader("PlatformDependent/MetroPlayer/Bindings/WindowsCryptoBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte[] ComputeMD5Hash(byte[] buffer);

		[NativeHeader("PlatformDependent/MetroPlayer/Bindings/WindowsCryptoBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte[] ComputeSHA1Hash(byte[] buffer);
	}
}
