using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	internal static class SubsystemDescriptorBindings
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Create(IntPtr descriptorPtr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetId(IntPtr descriptorPtr);
	}
}
