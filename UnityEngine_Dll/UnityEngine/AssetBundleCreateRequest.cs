using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromAsyncOperation.h"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AssetBundleCreateRequest : AsyncOperation
	{
		public extern AssetBundle assetBundle
		{
			[NativeMethod("GetAssetBundleBlocking")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeMethod("SetEnableCompatibilityChecks")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetEnableCompatibilityChecks(bool set);

		internal void DisableCompatibilityChecks()
		{
			this.SetEnableCompatibilityChecks(false);
		}
	}
}
