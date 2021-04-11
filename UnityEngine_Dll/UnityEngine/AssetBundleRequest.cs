using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadAssetOperation.h"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AssetBundleRequest : ResourceRequest
	{
		public new Object asset
		{
			get
			{
				return this.GetResult();
			}
		}

		public extern Object[] allAssets
		{
			[NativeMethod("GetAllLoadedAssets")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeMethod("GetLoadedAsset")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected override extern Object GetResult();
	}
}
