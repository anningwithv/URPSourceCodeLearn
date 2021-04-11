using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleManifest.h")]
	public class AssetBundleManifest : Object
	{
		private AssetBundleManifest()
		{
		}

		[NativeMethod("GetAllAssetBundles")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetAllAssetBundles();

		[NativeMethod("GetAllAssetBundlesWithVariant")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetAllAssetBundlesWithVariant();

		[NativeMethod("GetAssetBundleHash")]
		public Hash128 GetAssetBundleHash(string assetBundleName)
		{
			Hash128 result;
			this.GetAssetBundleHash_Injected(assetBundleName, out result);
			return result;
		}

		[NativeMethod("GetDirectDependencies")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetDirectDependencies(string assetBundleName);

		[NativeMethod("GetAllDependencies")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetAllDependencies(string assetBundleName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetAssetBundleHash_Injected(string assetBundleName, out Hash128 ret);
	}
}
