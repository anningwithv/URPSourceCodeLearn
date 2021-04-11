using System;
using System.Collections.Generic;
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
	[NativeHeader("Runtime/Graphics/GeneratedTextures.h"), NativeHeader("Runtime/Graphics/Texture2D.h"), UsedByNativeCode]
	public sealed class Texture2D : Texture
	{
		[Flags]
		public enum EXRFlags
		{
			None = 0,
			OutputAsFloat = 1,
			CompressZIP = 2,
			CompressRLE = 4,
			CompressPIZ = 8
		}

		public extern TextureFormat format
		{
			[NativeName("GetTextureFormat")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D whiteTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D blackTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D redTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D grayTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D linearGrayTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D normalTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public override extern bool isReadable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeConditional("ENABLE_VIRTUALTEXTURING && UNITY_EDITOR"), NativeName("VTOnly")]
		public extern bool vtOnly
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

		public extern int minimumMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetMinimumMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetMinimumMipmapLevel", HasExplicitThis = true)]
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

		public extern int calculatedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetCalculatedMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
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

		public extern bool alphaIsTransparency
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[VisibleToOtherModules(new string[]
		{
			"UnityEngine.UIElementsModule",
			"Unity.UIElements"
		})]
		internal extern float pixelsPerPoint
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Compress(bool highQuality);

		[FreeFunction("Texture2DScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl([Writable] Texture2D mono, int w, int h, int mipCount, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex);

		private static void Internal_Create([Writable] Texture2D mono, int w, int h, int mipCount, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex)
		{
			bool flag = !Texture2D.Internal_CreateImpl(mono, w, h, mipCount, format, flags, nativeTex);
			if (flag)
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		[NativeName("Apply")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		[NativeName("Resize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool ResizeImpl(int width, int height);

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

		[NativeName("GetPixelBilinear")]
		private Color GetPixelBilinearImpl(int image, float u, float v)
		{
			Color result;
			this.GetPixelBilinearImpl_Injected(image, u, v, out result);
			return result;
		}

		[FreeFunction(Name = "Texture2DScripting::ResizeWithFormat", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool ResizeWithFormatImpl(int width, int height, GraphicsFormat format, bool hasMipMap);

		[FreeFunction(Name = "Texture2DScripting::ReadPixels", HasExplicitThis = true)]
		private void ReadPixelsImpl(Rect source, int destX, int destY, bool recalculateMipMaps)
		{
			this.ReadPixelsImpl_Injected(ref source, destX, destY, recalculateMipMaps);
		}

		[FreeFunction(Name = "Texture2DScripting::SetPixels", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPixelsImpl(int x, int y, int w, int h, Color[] pixel, int miplevel, int frame);

		[FreeFunction(Name = "Texture2DScripting::LoadRawData", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool LoadRawTextureDataImpl(IntPtr data, int size);

		[FreeFunction(Name = "Texture2DScripting::LoadRawData", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool LoadRawTextureDataImplArray(byte[] data);

		[FreeFunction(Name = "Texture2DScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImplArray(Array data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		[FreeFunction(Name = "Texture2DScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImpl(IntPtr data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetWritableImageData(int frame);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern long GetRawImageDataSize();

		private static AtomicSafetyHandle GetSafetyHandle(Texture2D tex)
		{
			AtomicSafetyHandle result;
			Texture2D.GetSafetyHandle_Injected(tex, out result);
			return result;
		}

		private AtomicSafetyHandle GetSafetyHandleForSlice(int mipLevel)
		{
			AtomicSafetyHandle result;
			this.GetSafetyHandleForSlice_Injected(mipLevel, out result);
			return result;
		}

		[FreeFunction("Texture2DScripting::GenerateAtlas")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GenerateAtlasImpl(Vector2[] sizes, int padding, int atlasSize, [Out] Rect[] rect);

		[FreeFunction(Name = "GetTextureStreamingManager().ClearRequestedMipmapLevel", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearRequestedMipmapLevel();

		[FreeFunction(Name = "GetTextureStreamingManager().IsRequestedMipmapLevelLoaded", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsRequestedMipmapLevelLoaded();

		[FreeFunction(Name = "GetTextureStreamingManager().ClearMinimumMipmapLevel", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearMinimumMipmapLevel();

		[FreeFunction("Texture2DScripting::UpdateExternalTexture", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateExternalTexture(IntPtr nativeTex);

		[FreeFunction("Texture2DScripting::SetAllPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetAllPixels32(Color32[] colors, int miplevel);

		[FreeFunction("Texture2DScripting::SetBlockOfPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBlockOfPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors, int miplevel);

		[FreeFunction("Texture2DScripting::GetRawTextureData", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern byte[] GetRawTextureData();

		[FreeFunction("Texture2DScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(int x, int y, int blockWidth, int blockHeight, int miplevel);

		public Color[] GetPixels(int x, int y, int blockWidth, int blockHeight)
		{
			return this.GetPixels(x, y, blockWidth, blockHeight, 0);
		}

		[FreeFunction("Texture2DScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color32[] GetPixels32(int miplevel);

		public Color32[] GetPixels32()
		{
			return this.GetPixels32(0);
		}

		[FreeFunction("Texture2DScripting::PackTextures", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize, bool makeNoLongerReadable);

		public Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize)
		{
			return this.PackTextures(textures, padding, maximumAtlasSize, false);
		}

		public Rect[] PackTextures(Texture2D[] textures, int padding)
		{
			return this.PackTextures(textures, padding, 2048);
		}

		internal Texture2D(int width, int height, GraphicsFormat format, TextureCreationFlags flags, int mipCount, IntPtr nativeTex)
		{
			bool flag = base.ValidateFormat(format, FormatUsage.Sample);
			if (flag)
			{
				Texture2D.Internal_Create(this, width, height, mipCount, format, flags, nativeTex);
			}
		}

		public Texture2D(int width, int height, DefaultFormat format, TextureCreationFlags flags) : this(width, height, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		public Texture2D(int width, int height, GraphicsFormat format, TextureCreationFlags flags) : this(width, height, format, flags, Texture.GenerateAllMips, IntPtr.Zero)
		{
		}

		public Texture2D(int width, int height, GraphicsFormat format, int mipCount, TextureCreationFlags flags) : this(width, height, format, flags, mipCount, IntPtr.Zero)
		{
		}

		internal Texture2D(int width, int height, TextureFormat textureFormat, int mipCount, bool linear, IntPtr nativeTex)
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
				Texture2D.Internal_Create(this, width, height, mipCount, graphicsFormat, textureCreationFlags, nativeTex);
			}
		}

		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("-1")] int mipCount, [DefaultValue("false")] bool linear) : this(width, height, textureFormat, mipCount, linear, IntPtr.Zero)
		{
		}

		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("true")] bool mipChain, [DefaultValue("false")] bool linear) : this(width, height, textureFormat, mipChain ? -1 : 1, linear, IntPtr.Zero)
		{
		}

		public Texture2D(int width, int height, TextureFormat textureFormat, bool mipChain) : this(width, height, textureFormat, mipChain ? -1 : 1, false, IntPtr.Zero)
		{
		}

		public Texture2D(int width, int height) : this(width, height, TextureFormat.RGBA32, Texture.GenerateAllMips, false, IntPtr.Zero)
		{
		}

		public static Texture2D CreateExternalTexture(int width, int height, TextureFormat format, bool mipChain, bool linear, IntPtr nativeTex)
		{
			bool flag = nativeTex == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("nativeTex can not be null");
			}
			return new Texture2D(width, height, format, mipChain ? -1 : 1, linear, nativeTex);
		}

		public void SetPixel(int x, int y, Color color)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(0, x, y, color);
		}

		public void SetPixel(int x, int y, Color color, int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(mipLevel, x, y, color);
		}

		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors, [DefaultValue("0")] int miplevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelsImpl(x, y, blockWidth, blockHeight, colors, miplevel, 0);
		}

		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors)
		{
			this.SetPixels(x, y, blockWidth, blockHeight, colors, 0);
		}

		public void SetPixels(Color[] colors, [DefaultValue("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			bool flag = num < 1;
			if (flag)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			bool flag2 = num2 < 1;
			if (flag2)
			{
				num2 = 1;
			}
			this.SetPixels(0, 0, num, num2, colors, miplevel);
		}

		public void SetPixels(Color[] colors)
		{
			this.SetPixels(0, 0, this.width, this.height, colors, 0);
		}

		public Color GetPixel(int x, int y)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(0, x, y);
		}

		public Color GetPixel(int x, int y, int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(mipLevel, x, y);
		}

		public Color GetPixelBilinear(float u, float v)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(0, u, v);
		}

		public Color GetPixelBilinear(float u, float v, int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(mipLevel, u, v);
		}

		public void LoadRawTextureData(IntPtr data, int size)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = data == IntPtr.Zero || size == 0;
			if (flag2)
			{
				Debug.LogError("No texture data provided to LoadRawTextureData", this);
			}
			else
			{
				bool flag3 = !this.LoadRawTextureDataImpl(data, size);
				if (flag3)
				{
					throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
				}
			}
		}

		public void LoadRawTextureData(byte[] data)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = data == null || data.Length == 0;
			if (flag2)
			{
				Debug.LogError("No texture data provided to LoadRawTextureData", this);
			}
			else
			{
				bool flag3 = !this.LoadRawTextureDataImplArray(data);
				if (flag3)
				{
					throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
				}
			}
		}

		public void LoadRawTextureData<T>(NativeArray<T> data) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = !data.IsCreated || data.Length == 0;
			if (flag2)
			{
				throw new UnityException("No texture data provided to LoadRawTextureData");
			}
			bool flag3 = !this.LoadRawTextureDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), data.Length * UnsafeUtility.SizeOf<T>());
			if (flag3)
			{
				throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
			}
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
			IntPtr value = new IntPtr(this.GetWritableImageData(0).ToInt64() + (long)pixelDataOffset);
			NativeArray<T> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)value, pixelDataSize / num, Allocator.None);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, this.GetSafetyHandleForSlice(mipLevel));
			return result;
		}

		public unsafe NativeArray<T> GetRawTextureData<T>() where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			int num = UnsafeUtility.SizeOf<T>();
			NativeArray<T> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.GetWritableImageData(0), (int)(this.GetRawImageDataSize() / (long)num), Allocator.None);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, Texture2D.GetSafetyHandle(this));
			return result;
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

		public bool Resize(int width, int height)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.ResizeImpl(width, height);
		}

		public bool Resize(int width, int height, TextureFormat format, bool hasMipMap)
		{
			return this.ResizeWithFormatImpl(width, height, GraphicsFormatUtility.GetGraphicsFormat(format, base.activeTextureColorSpace == ColorSpace.Linear), hasMipMap);
		}

		public bool Resize(int width, int height, GraphicsFormat format, bool hasMipMap)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.ResizeWithFormatImpl(width, height, format, hasMipMap);
		}

		public void ReadPixels(Rect source, int destX, int destY, [DefaultValue("true")] bool recalculateMipMaps)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ReadPixelsImpl(source, destX, destY, recalculateMipMaps);
		}

		[ExcludeFromDocs]
		public void ReadPixels(Rect source, int destX, int destY)
		{
			this.ReadPixels(source, destX, destY, true);
		}

		public static bool GenerateAtlas(Vector2[] sizes, int padding, int atlasSize, List<Rect> results)
		{
			bool flag = sizes == null;
			if (flag)
			{
				throw new ArgumentException("sizes array can not be null");
			}
			bool flag2 = results == null;
			if (flag2)
			{
				throw new ArgumentException("results list cannot be null");
			}
			bool flag3 = padding < 0;
			if (flag3)
			{
				throw new ArgumentException("padding can not be negative");
			}
			bool flag4 = atlasSize <= 0;
			if (flag4)
			{
				throw new ArgumentException("atlas size must be positive");
			}
			results.Clear();
			bool flag5 = sizes.Length == 0;
			bool result;
			if (flag5)
			{
				result = true;
			}
			else
			{
				NoAllocHelpers.EnsureListElemCount<Rect>(results, sizes.Length);
				Texture2D.GenerateAtlasImpl(sizes, padding, atlasSize, NoAllocHelpers.ExtractArrayFromListT<Rect>(results));
				result = (results.Count != 0);
			}
			return result;
		}

		public void SetPixels32(Color32[] colors, int miplevel)
		{
			this.SetAllPixels32(colors, miplevel);
		}

		public void SetPixels32(Color32[] colors)
		{
			this.SetPixels32(colors, 0);
		}

		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors, int miplevel)
		{
			this.SetBlockOfPixels32(x, y, blockWidth, blockHeight, colors, miplevel);
		}

		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors)
		{
			this.SetPixels32(x, y, blockWidth, blockHeight, colors, 0);
		}

		public Color[] GetPixels(int miplevel)
		{
			int num = this.width >> miplevel;
			bool flag = num < 1;
			if (flag)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			bool flag2 = num2 < 1;
			if (flag2)
			{
				num2 = 1;
			}
			return this.GetPixels(0, 0, num, num2, miplevel);
		}

		public Color[] GetPixels()
		{
			return this.GetPixels(0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPixelImpl_Injected(int image, int x, int y, ref Color color);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPixelImpl_Injected(int image, int x, int y, out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPixelBilinearImpl_Injected(int image, float u, float v, out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ReadPixelsImpl_Injected(ref Rect source, int destX, int destY, bool recalculateMipMaps);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSafetyHandle_Injected(Texture2D tex, out AtomicSafetyHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSafetyHandleForSlice_Injected(int mipLevel, out AtomicSafetyHandle ret);
	}
}
