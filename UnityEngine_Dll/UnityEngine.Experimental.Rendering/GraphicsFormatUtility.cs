using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	[NativeHeader("Runtime/Graphics/Format.h"), NativeHeader("Runtime/Graphics/GraphicsFormatUtility.bindings.h"), NativeHeader("Runtime/Graphics/TextureFormat.h")]
	public class GraphicsFormatUtility
	{
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern GraphicsFormat GetFormat([NotNull("NullExceptionObject")] Texture texture);

		public static GraphicsFormat GetGraphicsFormat(TextureFormat format, bool isSRGB)
		{
			return GraphicsFormatUtility.GetGraphicsFormat_Native_TextureFormat(format, isSRGB);
		}

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat GetGraphicsFormat_Native_TextureFormat(TextureFormat format, bool isSRGB);

		public static TextureFormat GetTextureFormat(GraphicsFormat format)
		{
			return GraphicsFormatUtility.GetTextureFormat_Native_GraphicsFormat(format);
		}

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureFormat GetTextureFormat_Native_GraphicsFormat(GraphicsFormat format);

		public static GraphicsFormat GetGraphicsFormat(RenderTextureFormat format, bool isSRGB)
		{
			return GraphicsFormatUtility.GetGraphicsFormat_Native_RenderTextureFormat(format, isSRGB);
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat GetGraphicsFormat_Native_RenderTextureFormat(RenderTextureFormat format, bool isSRGB);

		public static GraphicsFormat GetGraphicsFormat(RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			bool flag = QualitySettings.activeColorSpace == ColorSpace.Linear;
			bool isSRGB = (readWrite == RenderTextureReadWrite.Default) ? flag : (readWrite == RenderTextureReadWrite.sRGB);
			return GraphicsFormatUtility.GetGraphicsFormat(format, isSRGB);
		}

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSRGBFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSwizzleFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GraphicsFormat GetSRGBFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GraphicsFormat GetLinearFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern RenderTextureFormat GetRenderTextureFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetColorComponentCount(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetAlphaComponentCount(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetComponentCount(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetFormatString(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsCompressedFormat(GraphicsFormat format);

		[FreeFunction("IsAnyCompressedTextureFormat", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsCompressedTextureFormat(TextureFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsPackedFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Is16BitPackedFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GraphicsFormat ConvertToAlphaFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsAlphaOnlyFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsAlphaTestFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasAlphaChannel(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsDepthFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsStencilFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsIEEE754Format(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsFloatFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsHalfFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsUnsignedFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSignedFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsNormFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsUNormFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSNormFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsIntegerFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsUIntFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSIntFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsXRFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsDXTCFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsRGTCFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsBPTCFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsBCFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsPVRTCFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsETCFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsEACFormat(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsASTCFormat(GraphicsFormat format);

		public static bool IsCrunchFormat(TextureFormat format)
		{
			return format == TextureFormat.DXT1Crunched || format == TextureFormat.DXT5Crunched || format == TextureFormat.ETC_RGB4Crunched || format == TextureFormat.ETC2_RGBA8Crunched;
		}

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FormatSwizzle GetSwizzleR(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FormatSwizzle GetSwizzleG(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FormatSwizzle GetSwizzleB(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FormatSwizzle GetSwizzleA(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetBlockSize(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetBlockWidth(GraphicsFormat format);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetBlockHeight(GraphicsFormat format);

		public static uint ComputeMipmapSize(int width, int height, GraphicsFormat format)
		{
			return GraphicsFormatUtility.ComputeMipmapSize_Native_2D(width, height, format);
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint ComputeMipmapSize_Native_2D(int width, int height, GraphicsFormat format);

		public static uint ComputeMipmapSize(int width, int height, int depth, GraphicsFormat format)
		{
			return GraphicsFormatUtility.ComputeMipmapSize_Native_3D(width, height, depth, format);
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint ComputeMipmapSize_Native_3D(int width, int height, int depth, GraphicsFormat format);
	}
}
