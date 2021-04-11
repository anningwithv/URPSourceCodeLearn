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
	[NativeHeader("Runtime/Graphics/Texture3D.h"), ExcludeFromPreset]
	public sealed class Texture3D : Texture
	{
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

		[NativeName("SetPixel")]
		private void SetPixelImpl(int image, int x, int y, int z, Color color)
		{
			this.SetPixelImpl_Injected(image, x, y, z, ref color);
		}

		[NativeName("GetPixel")]
		private Color GetPixelImpl(int image, int x, int y, int z)
		{
			Color result;
			this.GetPixelImpl_Injected(image, x, y, z, out result);
			return result;
		}

		[NativeName("GetPixelBilinear")]
		private Color GetPixelBilinearImpl(int image, float u, float v, float w)
		{
			Color result;
			this.GetPixelBilinearImpl_Injected(image, u, v, w, out result);
			return result;
		}

		[FreeFunction("Texture3DScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl([Writable] Texture3D mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex);

		private static void Internal_Create([Writable] Texture3D mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex)
		{
			bool flag = !Texture3D.Internal_CreateImpl(mono, w, h, d, mipCount, format, flags, nativeTex);
			if (flag)
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		[FreeFunction("Texture3DScripting::UpdateExternalTexture", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateExternalTexture(IntPtr nativeTex);

		[FreeFunction(Name = "Texture3DScripting::Apply", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		[FreeFunction(Name = "Texture3DScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(int miplevel);

		public Color[] GetPixels()
		{
			return this.GetPixels(0);
		}

		[FreeFunction(Name = "Texture3DScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color32[] GetPixels32(int miplevel);

		public Color32[] GetPixels32()
		{
			return this.GetPixels32(0);
		}

		[FreeFunction(Name = "Texture3DScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels(Color[] colors, int miplevel);

		public void SetPixels(Color[] colors)
		{
			this.SetPixels(colors, 0);
		}

		[FreeFunction(Name = "Texture3DScripting::SetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels32(Color32[] colors, int miplevel);

		public void SetPixels32(Color32[] colors)
		{
			this.SetPixels32(colors, 0);
		}

		[FreeFunction(Name = "Texture3DScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImplArray(Array data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		[FreeFunction(Name = "Texture3DScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImpl(IntPtr data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		private AtomicSafetyHandle GetSafetyHandleForSlice(int mipLevel)
		{
			AtomicSafetyHandle result;
			this.GetSafetyHandleForSlice_Injected(mipLevel, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetImageDataPointer();

		public Texture3D(int width, int height, int depth, DefaultFormat format, TextureCreationFlags flags) : this(width, height, depth, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		[RequiredByNativeCode]
		public Texture3D(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags) : this(width, height, depth, format, flags, Texture.GenerateAllMips)
		{
		}

		public Texture3D(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags, int mipCount)
		{
			bool flag = !base.ValidateFormat(format, FormatUsage.Sample);
			if (!flag)
			{
				Texture3D.ValidateIsNotCrunched(flags);
				Texture3D.Internal_Create(this, width, height, depth, mipCount, format, flags, IntPtr.Zero);
			}
		}

		public Texture3D(int width, int height, int depth, TextureFormat textureFormat, int mipCount)
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
				Texture3D.ValidateIsNotCrunched(textureCreationFlags);
				Texture3D.Internal_Create(this, width, height, depth, mipCount, graphicsFormat, textureCreationFlags, IntPtr.Zero);
			}
		}

		public Texture3D(int width, int height, int depth, TextureFormat textureFormat, int mipCount, IntPtr nativeTex)
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
				Texture3D.ValidateIsNotCrunched(textureCreationFlags);
				Texture3D.Internal_Create(this, width, height, depth, mipCount, graphicsFormat, textureCreationFlags, nativeTex);
			}
		}

		public Texture3D(int width, int height, int depth, TextureFormat textureFormat, bool mipChain) : this(width, height, depth, textureFormat, mipChain ? -1 : 1)
		{
		}

		public Texture3D(int width, int height, int depth, TextureFormat textureFormat, bool mipChain, IntPtr nativeTex) : this(width, height, depth, textureFormat, mipChain ? -1 : 1, nativeTex)
		{
		}

		public static Texture3D CreateExternalTexture(int width, int height, int depth, TextureFormat format, bool mipChain, IntPtr nativeTex)
		{
			bool flag = nativeTex == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("nativeTex may not be zero");
			}
			return new Texture3D(width, height, depth, format, mipChain ? -1 : 1, nativeTex);
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

		public void SetPixel(int x, int y, int z, Color color)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(0, x, y, z, color);
		}

		public void SetPixel(int x, int y, int z, Color color, int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(mipLevel, x, y, z, color);
		}

		public Color GetPixel(int x, int y, int z)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(0, x, y, z);
		}

		public Color GetPixel(int x, int y, int z, int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(mipLevel, x, y, z);
		}

		public Color GetPixelBilinear(float u, float v, float w)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(0, u, v, w);
		}

		public Color GetPixelBilinear(float u, float v, float w, int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(mipLevel, u, v, w);
		}

		public void SetPixelData<T>(T[] data, int mipLevel, int sourceDataStartIndex = 0)
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
			this.SetPixelDataImplArray(data, mipLevel, Marshal.SizeOf(data[0]), data.Length, sourceDataStartIndex);
		}

		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, int sourceDataStartIndex = 0) where T : struct
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
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			int pixelDataOffset = base.GetPixelDataOffset(mipLevel, 0);
			int pixelDataSize = base.GetPixelDataSize(mipLevel, 0);
			int num = UnsafeUtility.SizeOf<T>();
			IntPtr value = new IntPtr(this.GetImageDataPointer().ToInt64() + (long)pixelDataOffset);
			NativeArray<T> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)value, pixelDataSize / num, Allocator.None);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, this.GetSafetyHandleForSlice(mipLevel));
			return result;
		}

		private static void ValidateIsNotCrunched(TextureCreationFlags flags)
		{
			bool flag = (flags &= TextureCreationFlags.Crunch) > TextureCreationFlags.None;
			if (flag)
			{
				throw new ArgumentException("Crunched Texture3D is not supported.");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPixelImpl_Injected(int image, int x, int y, int z, ref Color color);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPixelImpl_Injected(int image, int x, int y, int z, out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPixelBilinearImpl_Injected(int image, float u, float v, float w, out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSafetyHandleForSlice_Injected(int mipLevel, out AtomicSafetyHandle ret);
	}
}
