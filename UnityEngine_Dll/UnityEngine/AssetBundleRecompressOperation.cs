using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleRecompressOperation.h"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AssetBundleRecompressOperation : AsyncOperation
	{
		public extern string humanReadableResult
		{
			[NativeMethod("GetResultStr")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern string inputPath
		{
			[NativeMethod("GetInputPath")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern string outputPath
		{
			[NativeMethod("GetOutputPath")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern AssetBundleLoadResult result
		{
			[NativeMethod("GetResult")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool success
		{
			[NativeMethod("GetSuccess")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
