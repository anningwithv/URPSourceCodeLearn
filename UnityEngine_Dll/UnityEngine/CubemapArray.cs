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
	[NativeHeader("Runtime/Graphics/CubemapArrayTexture.h"), ExcludeFromPreset]
	public sealed class CubemapArray : Texture
	{
		public extern int cubemapCount
		{
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

		[FreeFunction("CubemapArrayScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl([Writable] CubemapArray mono, int ext, int count, int mipCount, GraphicsFormat format, TextureCreationFlags flags);

		private static void Internal_Create([Writable] CubemapArray mono, int ext, int count, int mipCount, GraphicsFormat format, TextureCreationFlags flags)
		{
			bool flag = !CubemapArray.Internal_CreateImpl(mono, ext, count, mipCount, format, flags);
			if (flag)
			{
				throw new UnityException("Failed to create cubemap array texture because of invalid parameters.");
			}
		}

		[FreeFunction(Name = "CubemapArrayScripting::Apply", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		[FreeFunction(Name = "CubemapArrayScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(CubemapFace face, int arrayElement, int miplevel);

		public Color[] GetPixels(CubemapFace face, int arrayElement)
		{
			return this.GetPixels(face, arrayElement, 0);
		}

		[FreeFunction(Name = "CubemapArrayScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color32[] GetPixels32(CubemapFace face, int arrayElement, int miplevel);

		public Color32[] GetPixels32(CubemapFace face, int arrayElement)
		{
			return this.GetPixels32(face, arrayElement, 0);
		}

		[FreeFunction(Name = "CubemapArrayScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels(Color[] colors, CubemapFace face, int arrayElement, int miplevel);

		public void SetPixels(Color[] colors, CubemapFace face, int arrayElement)
		{
			this.SetPixels(colors, face, arrayElement, 0);
		}

		[FreeFunction(Name = "CubemapArrayScripting::SetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels32(Color32[] colors, CubemapFace face, int arrayElement, int miplevel);

		public void SetPixels32(Color32[] colors, CubemapFace face, int arrayElement)
		{
			this.SetPixels32(colors, face, arrayElement, 0);
		}

		[FreeFunction(Name = "CubemapArrayScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImplArray(Array data, int mipLevel, int face, int element, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		[FreeFunction(Name = "CubemapArrayScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImpl(IntPtr data, int mipLevel, int face, int element, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		private AtomicSafetyHandle GetSafetyHandleForSlice(int mipLevel, int face, int element)
		{
			AtomicSafetyHandle result;
			this.GetSafetyHandleForSlice_Injected(mipLevel, face, element, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetImageDataPointer();

		public CubemapArray(int width, int cubemapCount, DefaultFormat format, TextureCreationFlags flags) : this(width, cubemapCount, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		[RequiredByNativeCode]
		public CubemapArray(int width, int cubemapCount, GraphicsFormat format, TextureCreationFlags flags) : this(width, cubemapCount, format, flags, Texture.GenerateAllMips)
		{
		}

		public CubemapArray(int width, int cubemapCount, GraphicsFormat format, TextureCreationFlags flags, int mipCount)
		{
			bool flag = !base.ValidateFormat(format, FormatUsage.Sample);
			if (!flag)
			{
				CubemapArray.ValidateIsNotCrunched(flags);
				CubemapArray.Internal_Create(this, width, cubemapCount, mipCount, format, flags);
			}
		}

		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, int mipCount, [DefaultValue("true")] bool linear)
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
				CubemapArray.ValidateIsNotCrunched(textureCreationFlags);
				CubemapArray.Internal_Create(this, width, cubemapCount, mipCount, graphicsFormat, textureCreationFlags);
			}
		}

		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, bool mipChain, [DefaultValue("true")] bool linear) : this(width, cubemapCount, textureFormat, mipChain ? -1 : 1, linear)
		{
		}

		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, bool mipChain) : this(width, cubemapCount, textureFormat, mipChain ? -1 : 1, false)
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

		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		public void Apply()
		{
			this.Apply(true, false);
		}

		public void SetPixelData<T>(T[] data, int mipLevel, CubemapFace face, int element, int sourceDataStartIndex = 0)
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
			this.SetPixelDataImplArray(data, mipLevel, (int)face, element, Marshal.SizeOf(data[0]), data.Length, sourceDataStartIndex);
		}

		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, CubemapFace face, int element, int sourceDataStartIndex = 0) where T : struct
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
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, (int)face, element, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel, CubemapFace face, int element) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			int num = (int)(element * 6 + face);
			int pixelDataOffset = base.GetPixelDataOffset(base.mipmapCount, num);
			int pixelDataOffset2 = base.GetPixelDataOffset(mipLevel, num);
			int pixelDataSize = base.GetPixelDataSize(mipLevel, num);
			int num2 = UnsafeUtility.SizeOf<T>();
			IntPtr value = new IntPtr(this.GetImageDataPointer().ToInt64() + (long)(pixelDataOffset * num + pixelDataOffset2));
			NativeArray<T> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)value, pixelDataSize / num2, Allocator.None);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, this.GetSafetyHandleForSlice(mipLevel, (int)face, element));
			return result;
		}

		private static void ValidateIsNotCrunched(TextureCreationFlags flags)
		{
			bool flag = (flags &= TextureCreationFlags.Crunch) > TextureCreationFlags.None;
			if (flag)
			{
				throw new ArgumentException("Crunched TextureCubeArray is not supported.");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSafetyHandleForSlice_Injected(int mipLevel, int face, int element, out AtomicSafetyHandle ret);
	}
}
