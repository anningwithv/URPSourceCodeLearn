using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.AssetBundlePatching
{
	[NativeHeader("Modules/AssetBundle/Public/AssetBundlePatching.h")]
	public static class AssetBundleUtility
	{
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PatchAssetBundles(AssetBundle[] bundles, string[] filenames);
	}
}
