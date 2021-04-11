using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.iOS
{
	[NativeHeader("Runtime/Export/iOS/OnDemandResources.h")]
	public static class OnDemandResources
	{
		public static extern bool enabled
		{
			[FreeFunction("OnDemandResourcesScripting::Enabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[FreeFunction("OnDemandResourcesScripting::PreloadAsync")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern OnDemandResourcesRequest PreloadAsyncImpl(string[] tags);

		public static OnDemandResourcesRequest PreloadAsync(string[] tags)
		{
			return OnDemandResources.PreloadAsyncImpl(tags);
		}
	}
}
