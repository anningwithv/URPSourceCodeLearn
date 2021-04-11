using System;
using System.ComponentModel;

namespace UnityEngine
{
	public enum TextureFormat
	{
		Alpha8 = 1,
		ARGB4444,
		RGB24,
		RGBA32,
		ARGB32,
		RGB565 = 7,
		R16 = 9,
		DXT1,
		DXT5 = 12,
		RGBA4444,
		BGRA32,
		RHalf,
		RGHalf,
		RGBAHalf,
		RFloat,
		RGFloat,
		RGBAFloat,
		YUY2,
		RGB9e5Float,
		BC4 = 26,
		BC5,
		BC6H = 24,
		BC7,
		DXT1Crunched = 28,
		DXT5Crunched,
		PVRTC_RGB2,
		PVRTC_RGBA2,
		PVRTC_RGB4,
		PVRTC_RGBA4,
		ETC_RGB4,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.ATC_RGB4 has been deprecated. Use ETC_RGB4 instead (UnityUpgradable) -> ETC_RGB4", true)]
		ATC_RGB4 = -127,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.ATC_RGBA8 has been deprecated. Use ETC2_RGBA8 instead (UnityUpgradable) -> ETC2_RGBA8", true)]
		ATC_RGBA8 = -127,
		EAC_R = 41,
		EAC_R_SIGNED,
		EAC_RG,
		EAC_RG_SIGNED,
		ETC2_RGB,
		ETC2_RGBA1,
		ETC2_RGBA8,
		ASTC_4x4,
		ASTC_5x5,
		ASTC_6x6,
		ASTC_8x8,
		ASTC_10x10,
		ASTC_12x12,
		[Obsolete("Nintendo 3DS is no longer supported.")]
		ETC_RGB4_3DS = 60,
		[Obsolete("Nintendo 3DS is no longer supported.")]
		ETC_RGBA8_3DS,
		RG16,
		R8,
		ETC_RGB4Crunched,
		ETC2_RGBA8Crunched,
		ASTC_HDR_4x4,
		ASTC_HDR_5x5,
		ASTC_HDR_6x6,
		ASTC_HDR_8x8,
		ASTC_HDR_10x10,
		ASTC_HDR_12x12,
		RG32,
		RGB48,
		RGBA64,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.ASTC_RGB_4x4 has been deprecated. Use ASTC_4x4 instead (UnityUpgradable) -> ASTC_4x4")]
		ASTC_RGB_4x4 = 48,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.ASTC_RGB_5x5 has been deprecated. Use ASTC_5x5 instead (UnityUpgradable) -> ASTC_5x5")]
		ASTC_RGB_5x5,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.ASTC_RGB_6x6 has been deprecated. Use ASTC_6x6 instead (UnityUpgradable) -> ASTC_6x6")]
		ASTC_RGB_6x6,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.ASTC_RGB_8x8 has been deprecated. Use ASTC_8x8 instead (UnityUpgradable) -> ASTC_8x8")]
		ASTC_RGB_8x8,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.ASTC_RGB_10x10 has been deprecated. Use ASTC_10x10 instead (UnityUpgradable) -> ASTC_10x10")]
		ASTC_RGB_10x10,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.ASTC_RGB_12x12 has been deprecated. Use ASTC_12x12 instead (UnityUpgradable) -> ASTC_12x12")]
		ASTC_RGB_12x12,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.ASTC_RGBA_4x4 has been deprecated. Use ASTC_4x4 instead (UnityUpgradable) -> ASTC_4x4")]
		ASTC_RGBA_4x4,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.ASTC_RGBA_5x5 has been deprecated. Use ASTC_5x5 instead (UnityUpgradable) -> ASTC_5x5")]
		ASTC_RGBA_5x5,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.ASTC_RGBA_6x6 has been deprecated. Use ASTC_6x6 instead (UnityUpgradable) -> ASTC_6x6")]
		ASTC_RGBA_6x6,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.ASTC_RGBA_8x8 has been deprecated. Use ASTC_8x8 instead (UnityUpgradable) -> ASTC_8x8")]
		ASTC_RGBA_8x8,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.ASTC_RGBA_10x10 has been deprecated. Use ASTC_10x10 instead (UnityUpgradable) -> ASTC_10x10")]
		ASTC_RGBA_10x10,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.ASTC_RGBA_12x12 has been deprecated. Use ASTC_12x12 instead (UnityUpgradable) -> ASTC_12x12")]
		ASTC_RGBA_12x12,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.PVRTC_2BPP_RGB has been deprecated. Use PVRTC_RGB2 instead (UnityUpgradable) -> PVRTC_RGB2", true)]
		PVRTC_2BPP_RGB = -127,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.PVRTC_2BPP_RGBA has been deprecated. Use PVRTC_RGBA2 instead (UnityUpgradable) -> PVRTC_RGBA2", true)]
		PVRTC_2BPP_RGBA = -127,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.PVRTC_4BPP_RGB has been deprecated. Use PVRTC_RGB4 instead (UnityUpgradable) -> PVRTC_RGB4", true)]
		PVRTC_4BPP_RGB = -127,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member TextureFormat.PVRTC_4BPP_RGBA has been deprecated. Use PVRTC_RGBA4 instead (UnityUpgradable) -> PVRTC_RGBA4", true)]
		PVRTC_4BPP_RGBA = -127
	}
}
