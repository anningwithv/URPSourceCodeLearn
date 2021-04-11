using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine
{
	[NativeHeader("Modules/ImageConversion/ScriptBindings/ImageConversion.bindings.h")]
	public static class ImageConversion
	{
		public static bool EnableLegacyPngGammaRuntimeLoadBehavior
		{
			get
			{
				return ImageConversion.GetEnableLegacyPngGammaRuntimeLoadBehavior();
			}
			set
			{
				ImageConversion.SetEnableLegacyPngGammaRuntimeLoadBehavior(value);
			}
		}

		[NativeMethod(Name = "ImageConversionBindings::GetEnableLegacyPngGammaRuntimeLoadBehavior", IsFreeFunction = true, ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetEnableLegacyPngGammaRuntimeLoadBehavior();

		[NativeMethod(Name = "ImageConversionBindings::SetEnableLegacyPngGammaRuntimeLoadBehavior", IsFreeFunction = true, ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetEnableLegacyPngGammaRuntimeLoadBehavior(bool enable);

		[NativeMethod(Name = "ImageConversionBindings::EncodeToTGA", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte[] EncodeToTGA(this Texture2D tex);

		[NativeMethod(Name = "ImageConversionBindings::EncodeToPNG", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte[] EncodeToPNG(this Texture2D tex);

		[NativeMethod(Name = "ImageConversionBindings::EncodeToJPG", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte[] EncodeToJPG(this Texture2D tex, int quality);

		public static byte[] EncodeToJPG(this Texture2D tex)
		{
			return tex.EncodeToJPG(75);
		}

		[NativeMethod(Name = "ImageConversionBindings::EncodeToEXR", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte[] EncodeToEXR(this Texture2D tex, Texture2D.EXRFlags flags);

		public static byte[] EncodeToEXR(this Texture2D tex)
		{
			return tex.EncodeToEXR(Texture2D.EXRFlags.None);
		}

		[NativeMethod(Name = "ImageConversionBindings::LoadImage", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool LoadImage([NotNull("ArgumentNullException")] this Texture2D tex, byte[] data, bool markNonReadable);

		public static bool LoadImage(this Texture2D tex, byte[] data)
		{
			return tex.LoadImage(data, false);
		}

		[FreeFunction("ImageConversionBindings::EncodeArrayToTGA", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte[] EncodeArrayToTGA(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u);

		[FreeFunction("ImageConversionBindings::EncodeArrayToPNG", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte[] EncodeArrayToPNG(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u);

		[FreeFunction("ImageConversionBindings::EncodeArrayToJPG", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte[] EncodeArrayToJPG(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u, int quality = 75);

		[FreeFunction("ImageConversionBindings::EncodeArrayToEXR", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte[] EncodeArrayToEXR(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u, Texture2D.EXRFlags flags = Texture2D.EXRFlags.None);

		public unsafe static NativeArray<byte> EncodeNativeArrayToTGA<T>(NativeArray<T> input, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u) where T : struct
		{
			int length = input.Length * UnsafeUtility.SizeOf<T>();
			void* dataPointer = ImageConversion.UnsafeEncodeNativeArrayToTGA(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(input), ref length, format, width, height, rowBytes);
			NativeArray<byte> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(dataPointer, length, Allocator.Persistent);
			AtomicSafetyHandle atomicSafetyHandle = AtomicSafetyHandle.Create();
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<byte>(ref result, atomicSafetyHandle);
			AtomicSafetyHandle.SetAllowReadOrWriteAccess(atomicSafetyHandle, true);
			return result;
		}

		public unsafe static NativeArray<byte> EncodeNativeArrayToPNG<T>(NativeArray<T> input, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u) where T : struct
		{
			int length = input.Length * UnsafeUtility.SizeOf<T>();
			void* dataPointer = ImageConversion.UnsafeEncodeNativeArrayToPNG(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(input), ref length, format, width, height, rowBytes);
			NativeArray<byte> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(dataPointer, length, Allocator.Persistent);
			AtomicSafetyHandle atomicSafetyHandle = AtomicSafetyHandle.Create();
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<byte>(ref result, atomicSafetyHandle);
			AtomicSafetyHandle.SetAllowReadOrWriteAccess(atomicSafetyHandle, true);
			return result;
		}

		public unsafe static NativeArray<byte> EncodeNativeArrayToJPG<T>(NativeArray<T> input, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u, int quality = 75) where T : struct
		{
			int length = input.Length * UnsafeUtility.SizeOf<T>();
			void* dataPointer = ImageConversion.UnsafeEncodeNativeArrayToJPG(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(input), ref length, format, width, height, rowBytes, quality);
			NativeArray<byte> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(dataPointer, length, Allocator.Persistent);
			AtomicSafetyHandle atomicSafetyHandle = AtomicSafetyHandle.Create();
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<byte>(ref result, atomicSafetyHandle);
			AtomicSafetyHandle.SetAllowReadOrWriteAccess(atomicSafetyHandle, true);
			return result;
		}

		public unsafe static NativeArray<byte> EncodeNativeArrayToEXR<T>(NativeArray<T> input, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u, Texture2D.EXRFlags flags = Texture2D.EXRFlags.None) where T : struct
		{
			int length = input.Length * UnsafeUtility.SizeOf<T>();
			void* dataPointer = ImageConversion.UnsafeEncodeNativeArrayToEXR(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(input), ref length, format, width, height, rowBytes, flags);
			NativeArray<byte> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(dataPointer, length, Allocator.Persistent);
			AtomicSafetyHandle atomicSafetyHandle = AtomicSafetyHandle.Create();
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<byte>(ref result, atomicSafetyHandle);
			AtomicSafetyHandle.SetAllowReadOrWriteAccess(atomicSafetyHandle, true);
			return result;
		}

		[FreeFunction("ImageConversionBindings::UnsafeEncodeNativeArrayToTGA", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* UnsafeEncodeNativeArrayToTGA(void* array, ref int sizeInBytes, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u);

		[FreeFunction("ImageConversionBindings::UnsafeEncodeNativeArrayToPNG", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* UnsafeEncodeNativeArrayToPNG(void* array, ref int sizeInBytes, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u);

		[FreeFunction("ImageConversionBindings::UnsafeEncodeNativeArrayToJPG", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* UnsafeEncodeNativeArrayToJPG(void* array, ref int sizeInBytes, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u, int quality = 75);

		[FreeFunction("ImageConversionBindings::UnsafeEncodeNativeArrayToEXR", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* UnsafeEncodeNativeArrayToEXR(void* array, ref int sizeInBytes, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u, Texture2D.EXRFlags flags = Texture2D.EXRFlags.None);
	}
}
