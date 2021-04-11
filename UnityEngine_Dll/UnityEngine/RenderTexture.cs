using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/RenderBufferManager.h"), NativeHeader("Runtime/Graphics/RenderTexture.h"), NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h"), NativeHeader("Runtime/Camera/Camera.h"), UsedByNativeCode]
	public class RenderTexture : Texture
	{
		public override extern int width
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public override extern int height
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public override extern TextureDimension dimension
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("ColorFormat")]
		public new extern GraphicsFormat graphicsFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("MipMap")]
		public extern bool useMipMap
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("SRGBReadWrite")]
		public extern bool sRGB
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("VRUsage")]
		public extern VRTextureUsage vrUsage
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("Memoryless")]
		public extern RenderTextureMemoryless memorylessMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public RenderTextureFormat format
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(this.graphicsFormat);
			}
			set
			{
				this.graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(value, this.sRGB);
			}
		}

		public extern GraphicsFormat stencilFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool autoGenerateMips
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int volumeDepth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int antiAliasing
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool bindTextureMS
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool enableRandomWrite
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool useDynamicScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public bool isPowerOfTwo
		{
			get
			{
				return this.GetIsPowerOfTwo();
			}
			set
			{
			}
		}

		public static RenderTexture active
		{
			get
			{
				return RenderTexture.GetActive();
			}
			set
			{
				RenderTexture.SetActive(value);
			}
		}

		public RenderBuffer colorBuffer
		{
			get
			{
				return this.GetColorBuffer();
			}
		}

		public RenderBuffer depthBuffer
		{
			get
			{
				return this.GetDepthBuffer();
			}
		}

		public extern int depth
		{
			[FreeFunction("RenderTextureScripting::GetDepth", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("RenderTextureScripting::SetDepth", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public RenderTextureDescriptor descriptor
		{
			get
			{
				return this.GetDescriptor();
			}
			set
			{
				RenderTexture.ValidateRenderTextureDesc(value);
				this.SetRenderTextureDescriptor(value);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use RenderTexture.autoGenerateMips instead (UnityUpgradable) -> autoGenerateMips", false)]
		public bool generateMips
		{
			get
			{
				return this.autoGenerateMips;
			}
			set
			{
				this.autoGenerateMips = value;
			}
		}

		[Obsolete("Use RenderTexture.dimension instead.", false)]
		public bool isCubemap
		{
			get
			{
				return this.dimension == TextureDimension.Cube;
			}
			set
			{
				this.dimension = (value ? TextureDimension.Cube : TextureDimension.Tex2D);
			}
		}

		[Obsolete("Use RenderTexture.dimension instead.", false)]
		public bool isVolume
		{
			get
			{
				return this.dimension == TextureDimension.Tex3D;
			}
			set
			{
				this.dimension = (value ? TextureDimension.Tex3D : TextureDimension.Tex2D);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("RenderTexture.enabled is always now, no need to use it.", false)]
		public static bool enabled
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetIsPowerOfTwo();

		[FreeFunction("RenderTexture::GetActive")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderTexture GetActive();

		[FreeFunction("RenderTextureScripting::SetActive")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetActive(RenderTexture rt);

		[FreeFunction(Name = "RenderTextureScripting::GetColorBuffer", HasExplicitThis = true)]
		private RenderBuffer GetColorBuffer()
		{
			RenderBuffer result;
			this.GetColorBuffer_Injected(out result);
			return result;
		}

		[FreeFunction(Name = "RenderTextureScripting::GetDepthBuffer", HasExplicitThis = true)]
		private RenderBuffer GetDepthBuffer()
		{
			RenderBuffer result;
			this.GetDepthBuffer_Injected(out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetNativeDepthBufferPtr();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DiscardContents(bool discardColor, bool discardDepth);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void MarkRestoreExpected();

		public void DiscardContents()
		{
			this.DiscardContents(true, true);
		}

		[NativeName("ResolveAntiAliasedSurface")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResolveAA();

		[NativeName("ResolveAntiAliasedSurface")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResolveAATo(RenderTexture rt);

		public void ResolveAntiAliasedSurface()
		{
			this.ResolveAA();
		}

		public void ResolveAntiAliasedSurface(RenderTexture target)
		{
			this.ResolveAATo(target);
		}

		[FreeFunction(Name = "RenderTextureScripting::SetGlobalShaderProperty", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetGlobalShaderProperty(string propertyName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Create();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Release();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsCreated();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GenerateMips();

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ConvertToEquirect(RenderTexture equirect, Camera.MonoOrStereoscopicEye eye = Camera.MonoOrStereoscopicEye.Mono);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetSRGBReadWrite(bool srgb);

		[FreeFunction("RenderTextureScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] RenderTexture rt);

		[FreeFunction("RenderTextureSupportsStencil")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SupportsStencil(RenderTexture rt);

		[NativeName("SetRenderTextureDescFromScript")]
		private void SetRenderTextureDescriptor(RenderTextureDescriptor desc)
		{
			this.SetRenderTextureDescriptor_Injected(ref desc);
		}

		[NativeName("GetRenderTextureDesc")]
		private RenderTextureDescriptor GetDescriptor()
		{
			RenderTextureDescriptor result;
			this.GetDescriptor_Injected(out result);
			return result;
		}

		[FreeFunction("GetRenderBufferManager().GetTextures().GetTempBuffer")]
		private static RenderTexture GetTemporary_Internal(RenderTextureDescriptor desc)
		{
			return RenderTexture.GetTemporary_Internal_Injected(ref desc);
		}

		[FreeFunction("GetRenderBufferManager().GetTextures().ReleaseTempBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ReleaseTemporary(RenderTexture temp);

		[RequiredByNativeCode]
		protected internal RenderTexture()
		{
		}

		public RenderTexture(RenderTextureDescriptor desc)
		{
			RenderTexture.ValidateRenderTextureDesc(desc);
			RenderTexture.Internal_Create(this);
			this.SetRenderTextureDescriptor(desc);
		}

		public RenderTexture(RenderTexture textureToCopy)
		{
			bool flag = textureToCopy == null;
			if (flag)
			{
				throw new ArgumentNullException("textureToCopy");
			}
			RenderTexture.ValidateRenderTextureDesc(textureToCopy.descriptor);
			RenderTexture.Internal_Create(this);
			this.SetRenderTextureDescriptor(textureToCopy.descriptor);
		}

		public RenderTexture(int width, int height, int depth, DefaultFormat format) : this(width, height, depth, SystemInfo.GetGraphicsFormat(format))
		{
		}

		public RenderTexture(int width, int height, int depth, GraphicsFormat format)
		{
			bool flag = !base.ValidateFormat(format, FormatUsage.Render);
			if (!flag)
			{
				RenderTexture.Internal_Create(this);
				this.width = width;
				this.height = height;
				this.depth = depth;
				this.graphicsFormat = format;
				this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(format));
			}
		}

		public RenderTexture(int width, int height, int depth, GraphicsFormat format, int mipCount)
		{
			bool flag = !base.ValidateFormat(format, FormatUsage.Render);
			if (!flag)
			{
				RenderTexture.Internal_Create(this);
				this.width = width;
				this.height = height;
				this.depth = depth;
				this.graphicsFormat = format;
				this.descriptor = new RenderTextureDescriptor(width, height, format, depth, mipCount);
				this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(format));
			}
		}

		public RenderTexture(int width, int height, int depth, [UnityEngine.Internal.DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [UnityEngine.Internal.DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite) : this(width, height, depth, RenderTexture.GetCompatibleFormat(format, readWrite))
		{
		}

		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format) : this(width, height, depth, RenderTexture.GetCompatibleFormat(format, RenderTextureReadWrite.Default))
		{
		}

		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth) : this(width, height, depth, RenderTexture.GetCompatibleFormat(RenderTextureFormat.Default, RenderTextureReadWrite.Default))
		{
		}

		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format, int mipCount) : this(width, height, depth, RenderTexture.GetCompatibleFormat(format, RenderTextureReadWrite.Default), mipCount)
		{
		}

		private static void ValidateRenderTextureDesc(RenderTextureDescriptor desc)
		{
			bool flag = !SystemInfo.IsFormatSupported(desc.graphicsFormat, FormatUsage.Render);
			if (flag)
			{
				throw new ArgumentException("RenderTextureDesc graphicsFormat must be a supported GraphicsFormat. " + desc.graphicsFormat.ToString() + " is not supported.", "desc.graphicsFormat");
			}
			bool flag2 = desc.width <= 0;
			if (flag2)
			{
				throw new ArgumentException("RenderTextureDesc width must be greater than zero.", "desc.width");
			}
			bool flag3 = desc.height <= 0;
			if (flag3)
			{
				throw new ArgumentException("RenderTextureDesc height must be greater than zero.", "desc.height");
			}
			bool flag4 = desc.volumeDepth <= 0;
			if (flag4)
			{
				throw new ArgumentException("RenderTextureDesc volumeDepth must be greater than zero.", "desc.volumeDepth");
			}
			bool flag5 = desc.msaaSamples != 1 && desc.msaaSamples != 2 && desc.msaaSamples != 4 && desc.msaaSamples != 8;
			if (flag5)
			{
				throw new ArgumentException("RenderTextureDesc msaaSamples must be 1, 2, 4, or 8.", "desc.msaaSamples");
			}
			bool flag6 = desc.depthBufferBits != 0 && desc.depthBufferBits != 16 && desc.depthBufferBits != 24;
			if (flag6)
			{
				throw new ArgumentException("RenderTextureDesc depthBufferBits must be 0, 16, or 24.", "desc.depthBufferBits");
			}
		}

		internal static GraphicsFormat GetCompatibleFormat(RenderTextureFormat renderTextureFormat, RenderTextureReadWrite readWrite)
		{
			GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(renderTextureFormat, readWrite);
			GraphicsFormat compatibleFormat = SystemInfo.GetCompatibleFormat(graphicsFormat, FormatUsage.Render);
			bool flag = graphicsFormat == compatibleFormat;
			GraphicsFormat result;
			if (flag)
			{
				result = graphicsFormat;
			}
			else
			{
				Debug.LogWarning(string.Format("'{0}' is not supported. RenderTexture::GetTemporary fallbacks to {1} format on this platform. Use 'SystemInfo.IsFormatSupported' C# API to check format support.", graphicsFormat.ToString(), compatibleFormat.ToString()));
				result = compatibleFormat;
			}
			return result;
		}

		public static RenderTexture GetTemporary(RenderTextureDescriptor desc)
		{
			RenderTexture.ValidateRenderTextureDesc(desc);
			desc.createdFromScript = true;
			return RenderTexture.GetTemporary_Internal(desc);
		}

		private static RenderTexture GetTemporaryImpl(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing = 1, RenderTextureMemoryless memorylessMode = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, bool useDynamicScale = false)
		{
			return RenderTexture.GetTemporary(new RenderTextureDescriptor(width, height, format, depthBuffer)
			{
				msaaSamples = antiAliasing,
				memoryless = memorylessMode,
				vrUsage = vrUsage,
				useDynamicScale = useDynamicScale
			});
		}

		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, [UnityEngine.Internal.DefaultValue("1")] int antiAliasing, [UnityEngine.Internal.DefaultValue("RenderTextureMemoryless.None")] RenderTextureMemoryless memorylessMode, [UnityEngine.Internal.DefaultValue("VRTextureUsage.None")] VRTextureUsage vrUsage, [UnityEngine.Internal.DefaultValue("false")] bool useDynamicScale)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, format, antiAliasing, memorylessMode, vrUsage, useDynamicScale);
		}

		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing, RenderTextureMemoryless memorylessMode, VRTextureUsage vrUsage)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, format, antiAliasing, memorylessMode, vrUsage, false);
		}

		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing, RenderTextureMemoryless memorylessMode)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, format, antiAliasing, memorylessMode, VRTextureUsage.None, false);
		}

		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, format, antiAliasing, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, format, 1, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		public static RenderTexture GetTemporary(int width, int height, [UnityEngine.Internal.DefaultValue("0")] int depthBuffer, [UnityEngine.Internal.DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [UnityEngine.Internal.DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite, [UnityEngine.Internal.DefaultValue("1")] int antiAliasing, [UnityEngine.Internal.DefaultValue("RenderTextureMemoryless.None")] RenderTextureMemoryless memorylessMode, [UnityEngine.Internal.DefaultValue("VRTextureUsage.None")] VRTextureUsage vrUsage, [UnityEngine.Internal.DefaultValue("false")] bool useDynamicScale)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, GraphicsFormatUtility.GetGraphicsFormat(format, readWrite), antiAliasing, memorylessMode, vrUsage, useDynamicScale);
		}

		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, RenderTextureMemoryless memorylessMode, VRTextureUsage vrUsage)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, readWrite), antiAliasing, memorylessMode, vrUsage, false);
		}

		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, RenderTextureMemoryless memorylessMode)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, readWrite), antiAliasing, memorylessMode, VRTextureUsage.None, false);
		}

		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, readWrite), antiAliasing, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, readWrite), 1, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, RenderTextureReadWrite.Default), 1, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(RenderTextureFormat.Default, RenderTextureReadWrite.Default), 1, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height)
		{
			return RenderTexture.GetTemporaryImpl(width, height, 0, RenderTexture.GetCompatibleFormat(RenderTextureFormat.Default, RenderTextureReadWrite.Default), 1, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("UsSetBorderColor is no longer supported.", true)]
		public void SetBorderColor(Color color)
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("GetTexelOffset always returns zero now, no point in using it.", false)]
		public Vector2 GetTexelOffset()
		{
			return Vector2.zero;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetColorBuffer_Injected(out RenderBuffer ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetDepthBuffer_Injected(out RenderBuffer ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetRenderTextureDescriptor_Injected(ref RenderTextureDescriptor desc);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetDescriptor_Injected(out RenderTextureDescriptor ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderTexture GetTemporary_Internal_Injected(ref RenderTextureDescriptor desc);
	}
}
