using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Streaming/TextureStreamingManager.h"), NativeHeader("Runtime/Graphics/Texture.h"), UsedByNativeCode]
	public class Texture : Object
	{
		public static readonly int GenerateAllMips = -1;

		[NativeProperty("GlobalMasterTextureLimit")]
		public static extern int masterTextureLimit
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int mipmapCount
		{
			[NativeName("GetMipmapCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("AnisoLimit")]
		public static extern AnisotropicFiltering anisotropicFiltering
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public virtual GraphicsFormat graphicsFormat
		{
			get
			{
				return GraphicsFormatUtility.GetFormat(this);
			}
		}

		public virtual int width
		{
			get
			{
				return this.GetDataWidth();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public virtual int height
		{
			get
			{
				return this.GetDataHeight();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public virtual TextureDimension dimension
		{
			get
			{
				return this.GetDimension();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public virtual extern bool isReadable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern TextureWrapMode wrapMode
		{
			[NativeName("GetWrapModeU")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TextureWrapMode wrapModeU
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TextureWrapMode wrapModeV
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TextureWrapMode wrapModeW
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern FilterMode filterMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int anisoLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float mipMapBias
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector2 texelSize
		{
			[NativeName("GetNpotTexelSize")]
			get
			{
				Vector2 result;
				this.get_texelSize_Injected(out result);
				return result;
			}
		}

		public extern uint updateCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal ColorSpace activeTextureColorSpace
		{
			[VisibleToOtherModules(new string[]
			{
				"UnityEngine.UIElementsModule",
				"Unity.UIElements"
			})]
			get
			{
				return (this.Internal_GetActiveTextureColorSpace() == 0) ? ColorSpace.Linear : ColorSpace.Gamma;
			}
		}

		public Hash128 imageContentsHash
		{
			get
			{
				Hash128 result;
				this.get_imageContentsHash_Injected(out result);
				return result;
			}
			set
			{
				this.set_imageContentsHash_Injected(ref value);
			}
		}

		public static extern ulong totalTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetTotalTextureMemory")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ulong desiredTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetDesiredTextureMemory")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ulong targetTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetTargetTextureMemory")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ulong currentTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetCurrentTextureMemory")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ulong nonStreamingTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetNonStreamingTextureMemory")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ulong streamingMipmapUploadCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingMipmapUploadCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ulong streamingRendererCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingRendererCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ulong streamingTextureCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingTextureCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ulong nonStreamingTextureCount
		{
			[FreeFunction("GetTextureStreamingManager().GetNonStreamingTextureCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ulong streamingTexturePendingLoadCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingTexturePendingLoadCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ulong streamingTextureLoadingCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingTextureLoadingCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool streamingTextureForceLoadAll
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetForceLoadAll")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetForceLoadAll")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool streamingTextureDiscardUnusedMips
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetDiscardUnusedMips")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetDiscardUnusedMips")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool allowThreadedTextureCreation
		{
			[FreeFunction(Name = "Texture2DScripting::IsCreateTextureThreadedEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(Name = "Texture2DScripting::EnableCreateTextureThreaded")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		protected Texture()
		{
		}

		[NativeName("SetGlobalAnisoLimits")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalAnisotropicFilteringLimits(int forcedMin, int globalMax);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetDataWidth();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetDataHeight();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern TextureDimension GetDimension();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetNativeTexturePtr();

		[Obsolete("Use GetNativeTexturePtr instead.", false)]
		public int GetNativeTextureID()
		{
			return (int)this.GetNativeTexturePtr();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void IncrementUpdateCount();

		[NativeMethod("GetActiveTextureColorSpace")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int Internal_GetActiveTextureColorSpace();

		[FreeFunction("GetTextureStreamingManager().SetStreamingTextureMaterialDebugProperties")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStreamingTextureMaterialDebugProperties();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetPixelDataSize(int mipLevel, int element = 0);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetPixelDataOffset(int mipLevel, int element = 0);

		internal bool ValidateFormat(RenderTextureFormat format)
		{
			bool flag = SystemInfo.SupportsRenderTextureFormat(format);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				Debug.LogError(string.Format("RenderTexture creation failed. '{0}' is not supported on this platform. Use 'SystemInfo.SupportsRenderTextureFormat' C# API to check format support.", format.ToString()), this);
				result = false;
			}
			return result;
		}

		internal bool ValidateFormat(TextureFormat format)
		{
			bool flag = SystemInfo.SupportsTextureFormat(format);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = GraphicsFormatUtility.IsCompressedTextureFormat(format);
				if (flag2)
				{
					Debug.LogWarning(string.Format("'{0}' is not supported on this platform. Decompressing texture. Use 'SystemInfo.SupportsTextureFormat' C# API to check format support.", format.ToString()), this);
					result = true;
				}
				else
				{
					Debug.LogError(string.Format("Texture creation failed. '{0}' is not supported on this platform. Use 'SystemInfo.SupportsTextureFormat' C# API to check format support.", format.ToString()), this);
					result = false;
				}
			}
			return result;
		}

		internal bool ValidateFormat(GraphicsFormat format, FormatUsage usage)
		{
			bool flag = SystemInfo.IsFormatSupported(format, usage);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				Debug.LogError(string.Format("Texture creation failed. '{0}' is not supported for {1} usage on this platform. Use 'SystemInfo.IsFormatSupported' C# API to check format support.", format.ToString(), usage.ToString()), this);
				result = false;
			}
			return result;
		}

		internal UnityException CreateNonReadableException(Texture t)
		{
			return new UnityException(string.Format("Texture '{0}' is not readable, the texture memory can not be accessed from scripts. You can make the texture readable in the Texture Import Settings.", t.name));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_texelSize_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_imageContentsHash_Injected(out Hash128 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_imageContentsHash_Injected(ref Hash128 value);
	}
}
