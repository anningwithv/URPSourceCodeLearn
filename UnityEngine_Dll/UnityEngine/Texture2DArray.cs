using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/Texture2DArray.h")]
	public sealed class Texture2DArray : Texture
	{
		public static extern int allSlices
		{
			[NativeName("GetAllTextureLayersIdentifier")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int depth
		{
			[NativeName("GetTextureLayerCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern TextureFormat format
		{
			[NativeName("GetTextureFormat")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public override extern bool isReadable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[FreeFunction("Texture2DArrayScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl([Writable] Texture2DArray mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureCreationFlags flags);

		private static void Internal_Create([Writable] Texture2DArray mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureCreationFlags flags)
		{
			bool flag = !Texture2DArray.Internal_CreateImpl(mono, w, h, d, mipCount, format, flags);
			if (flag)
			{
				throw new UnityException("Failed to create 2D array texture because of invalid parameters.");
			}
		}

		[FreeFunction(Name = "Texture2DArrayScripting::Apply", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		[FreeFunction(Name = "Texture2DArrayScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(int arrayElement, int miplevel);

		public Color[] GetPixels(int arrayElement)
		{
			return this.GetPixels(arrayElement, 0);
		}

		[FreeFunction(Name = "Texture2DArrayScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImplArray(Array data, int mipLevel, int element, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		[FreeFunction(Name = "Texture2DArrayScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImpl(IntPtr data, int mipLevel, int element, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		[FreeFunction(Name = "Texture2DArrayScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color32[] GetPixels32(int arrayElement, int miplevel);

		public Color32[] GetPixels32(int arrayElement)
		{
			return this.GetPixels32(arrayElement, 0);
		}

		[FreeFunction(Name = "Texture2DArrayScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels(Color[] colors, int arrayElement, int miplevel);

		public void SetPixels(Color[] colors, int arrayElement)
		{
			this.SetPixels(colors, arrayElement, 0);
		}

		[FreeFunction(Name = "Texture2DArrayScripting::SetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels32(Color32[] colors, int arrayElement, int miplevel);

		public void SetPixels32(Color32[] colors, int arrayElement)
		{
			this.SetPixels32(colors, arrayElement, 0);
		}

		private AtomicSafetyHandle GetSafetyHandleForSlice(int mipLevel, int element)
		{
			AtomicSafetyHandle result;
			this.GetSafetyHandleForSlice_Injected(mipLevel, element, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetImageDataPointer();

		public Texture2DArray(int width, int height, int depth, DefaultFormat format, TextureCreationFlags flags) : this(width, height, depth, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		[RequiredByNativeCode]
		public Texture2DArray(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags) : this(width, height, depth, format, flags, Texture.GenerateAllMips)
		{
		}

		public Texture2DArray(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags, int mipCount)
		{
			bool flag = !base.ValidateFormat(format, FormatUsage.Sample);
			if (!flag)
			{
				Texture2DArray.ValidateIsNotCrunched(flags);
				Texture2DArray.Internal_Create(this, width, height, depth, mipCount, format, flags);
			}
		}

		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, int mipCount, [DefaultValue("true")] bool linear)
		{
			bool flag = !base.ValidateFormat(textureFormat);
			if (!flag)
			{
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, !linear);
				TextureCreationFlags textureCreationFlags = (mipCount != 1) ? TextureCreationFlags.MipChain : TextureCreationFlags.None;
				bool flag2 = GraphicsFormatUtility.IsCrunchFormat(textureFormat);
				if (flag2)
				{
					textureCreationFlags |= TextureCreationFlags.Crunch;
				}
				Texture2DArray.ValidateIsNotCrunched(textureCreationFlags);
				Texture2DArray.Internal_Create(this, width, height, depth, mipCount, graphicsFormat, textureCreationFlags);
			}
		}

		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, bool mipChain, [DefaultValue("true")] bool linear) : this(width, height, depth, textureFormat, mipChain ? -1 : 1, linear)
		{
		}

		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, bool mipChain) : this(width, height, depth, textureFormat, mipChain ? -1 : 1, false)
		{
		}

		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		public void SetPixelData<T>(T[] data, int mipLevel, int element, int sourceDataStartIndex = 0)
		{
			bool flag = sourceDataStartIndex < 0;
			if (flag)
			{
				throw new UnityException("SetPixelData: sourceDataStartIndex cannot be less than 0.");
			}
			bool flag2 = !this.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag3 = data == null || data.Length == 0;
			if (flag3)
			{
				throw new UnityException("No texture data provided to SetPixelData.");
			}
			this.SetPixelDataImplArray(data, mipLevel, element, Marshal.SizeOf(data[0]), data.Length, sourceDataStartIndex);
		}

		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, int element, int sourceDataStartIndex = 0) where T : struct
		{
			bool flag = sourceDataStartIndex < 0;
			if (flag)
			{
				throw new UnityException("SetPixelData: sourceDataStartIndex cannot be less than 0.");
			}
			bool flag2 = !this.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag3 = !data.IsCreated || data.Length == 0;
			if (flag3)
			{
				throw new UnityException("No texture data provided to SetPixelData.");
			}
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, element, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel, int element) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			int pixelDataOffset = base.GetPixelDataOffset(base.mipmapCount, element);
			int pixelDataOffset2 = base.GetPixelDataOffset(mipLevel, element);
			int pixelDataSize = base.GetPixelDataSize(mipLevel, element);
			int num = UnsafeUtility.SizeOf<T>();
			IntPtr value = new IntPtr(this.GetImageDataPointer().ToInt64() + (long)(pixelDataOffset * element + pixelDataOffset2));
			NativeArray<T> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)value, pixelDataSize / num, Allocator.None);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, this.GetSafetyHandleForSlice(mipLevel, element));
			return result;
		}

		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		public void Apply()
		{
			this.Apply(true, false);
		}

		private static void ValidateIsNotCrunched(TextureCreationFlags flags)
		{
			bool flag = (flags &= TextureCreationFlags.Crunch) > TextureCreationFlags.None;
			if (flag)
			{
				throw new ArgumentException("Crunched Texture2DArray is not supported.");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSafetyHandleForSlice_Injected(int mipLevel, int element, out AtomicSafetyHandle ret);
	}
}
