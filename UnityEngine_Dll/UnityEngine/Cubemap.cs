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
	[NativeHeader("Runtime/Graphics/CubemapTexture.h"), ExcludeFromPreset]
	public sealed class Cubemap : Texture
	{
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

		internal extern bool isPreProcessed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool streamingMipmaps
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int streamingMipmapsPriority
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int requestedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetRequestedMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetRequestedMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal extern bool loadAllMips
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadAllMips", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetLoadAllMips", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int desiredMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetDesiredMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int loadingMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadingMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int loadedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadedMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[FreeFunction("CubemapScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl([Writable] Cubemap mono, int ext, int mipCount, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex);

		private static void Internal_Create([Writable] Cubemap mono, int ext, int mipCount, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex)
		{
			bool flag = !Cubemap.Internal_CreateImpl(mono, ext, mipCount, format, flags, nativeTex);
			if (flag)
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		[FreeFunction(Name = "CubemapScripting::Apply", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		[FreeFunction("CubemapScripting::UpdateExternalTexture", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateExternalTexture(IntPtr nativeTexture);

		[NativeName("SetPixel")]
		private void SetPixelImpl(int image, int x, int y, Color color)
		{
			this.SetPixelImpl_Injected(image, x, y, ref color);
		}

		[NativeName("GetPixel")]
		private Color GetPixelImpl(int image, int x, int y)
		{
			Color result;
			this.GetPixelImpl_Injected(image, x, y, out result);
			return result;
		}

		[NativeName("FixupEdges")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SmoothEdges([DefaultValue("1")] int smoothRegionWidthInPixels);

		public void SmoothEdges()
		{
			this.SmoothEdges(1);
		}

		[FreeFunction(Name = "CubemapScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(CubemapFace face, int miplevel);

		public Color[] GetPixels(CubemapFace face)
		{
			return this.GetPixels(face, 0);
		}

		[FreeFunction(Name = "CubemapScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels(Color[] colors, CubemapFace face, int miplevel);

		[FreeFunction(Name = "CubemapScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImplArray(Array data, int mipLevel, int face, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		[FreeFunction(Name = "CubemapScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImpl(IntPtr data, int mipLevel, int face, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		public void SetPixels(Color[] colors, CubemapFace face)
		{
			this.SetPixels(colors, face, 0);
		}

		private AtomicSafetyHandle GetSafetyHandleForSlice(int mipLevel, int face)
		{
			AtomicSafetyHandle result;
			this.GetSafetyHandleForSlice_Injected(mipLevel, face, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetWritableImageData(int frame);

		[FreeFunction(Name = "GetTextureStreamingManager().ClearRequestedMipmapLevel", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearRequestedMipmapLevel();

		[FreeFunction(Name = "GetTextureStreamingManager().IsRequestedMipmapLevelLoaded", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsRequestedMipmapLevelLoaded();

		public Cubemap(int width, DefaultFormat format, TextureCreationFlags flags) : this(width, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		[RequiredByNativeCode]
		public Cubemap(int width, GraphicsFormat format, TextureCreationFlags flags)
		{
			bool flag = base.ValidateFormat(format, FormatUsage.Sample);
			if (flag)
			{
				Cubemap.Internal_Create(this, width, Texture.GenerateAllMips, format, flags, IntPtr.Zero);
			}
		}

		public Cubemap(int width, TextureFormat format, int mipCount) : this(width, format, mipCount, IntPtr.Zero)
		{
		}

		public Cubemap(int width, GraphicsFormat format, TextureCreationFlags flags, int mipCount)
		{
			bool flag = !base.ValidateFormat(format, FormatUsage.Sample);
			if (!flag)
			{
				Cubemap.ValidateIsNotCrunched(flags);
				Cubemap.Internal_Create(this, width, mipCount, format, flags, IntPtr.Zero);
			}
		}

		internal Cubemap(int width, TextureFormat textureFormat, int mipCount, IntPtr nativeTex)
		{
			bool flag = !base.ValidateFormat(textureFormat);
			if (!flag)
			{
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, false);
				TextureCreationFlags textureCreationFlags = (mipCount != 1) ? TextureCreationFlags.MipChain : TextureCreationFlags.None;
				bool flag2 = GraphicsFormatUtility.IsCrunchFormat(textureFormat);
				if (flag2)
				{
					textureCreationFlags |= TextureCreationFlags.Crunch;
				}
				Cubemap.ValidateIsNotCrunched(textureCreationFlags);
				Cubemap.Internal_Create(this, width, mipCount, graphicsFormat, textureCreationFlags, nativeTex);
			}
		}

		internal Cubemap(int width, TextureFormat textureFormat, bool mipChain, IntPtr nativeTex) : this(width, textureFormat, mipChain ? -1 : 1, nativeTex)
		{
		}

		public Cubemap(int width, TextureFormat textureFormat, bool mipChain) : this(width, textureFormat, mipChain ? -1 : 1, IntPtr.Zero)
		{
		}

		public static Cubemap CreateExternalTexture(int width, TextureFormat format, bool mipmap, IntPtr nativeTex)
		{
			bool flag = nativeTex == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("nativeTex can not be null");
			}
			return new Cubemap(width, format, mipmap, nativeTex);
		}

		public void SetPixelData<T>(T[] data, int mipLevel, CubemapFace face, int sourceDataStartIndex = 0)
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
			this.SetPixelDataImplArray(data, mipLevel, (int)face, Marshal.SizeOf(data[0]), data.Length, sourceDataStartIndex);
		}

		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, CubemapFace face, int sourceDataStartIndex = 0) where T : struct
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
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, (int)face, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel, CubemapFace face) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			int pixelDataOffset = base.GetPixelDataOffset(base.mipmapCount, (int)face);
			int pixelDataOffset2 = base.GetPixelDataOffset(mipLevel, (int)face);
			int pixelDataSize = base.GetPixelDataSize(mipLevel, (int)face);
			int num = UnsafeUtility.SizeOf<T>();
			IntPtr value = new IntPtr(this.GetWritableImageData(0).ToInt64() + (long)(pixelDataOffset * (int)face + pixelDataOffset2));
			NativeArray<T> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)value, pixelDataSize / num, Allocator.None);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, this.GetSafetyHandleForSlice(mipLevel, (int)face));
			return result;
		}

		public void SetPixel(CubemapFace face, int x, int y, Color color)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl((int)face, x, y, color);
		}

		public Color GetPixel(CubemapFace face, int x, int y)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl((int)face, x, y);
		}

		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
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

		private static void ValidateIsNotCrunched(TextureCreationFlags flags)
		{
			bool flag = (flags &= TextureCreationFlags.Crunch) > TextureCreationFlags.None;
			if (flag)
			{
				throw new ArgumentException("Crunched Cubemap is not supported for textures created from script.");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPixelImpl_Injected(int image, int x, int y, ref Color color);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPixelImpl_Injected(int image, int x, int y, out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSafetyHandleForSlice_Injected(int mipLevel, int face, out AtomicSafetyHandle ret);
	}
}
