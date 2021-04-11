using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace UnityEngine
{
	public sealed class SystemInfo
	{
		public const string unsupportedIdentifier = "n/a";

		public static float batteryLevel
		{
			get
			{
				return ShimManager.systemInfoShim.batteryLevel;
			}
		}

		public static BatteryStatus batteryStatus
		{
			get
			{
				return ShimManager.systemInfoShim.batteryStatus;
			}
		}

		public static string operatingSystem
		{
			get
			{
				return ShimManager.systemInfoShim.operatingSystem;
			}
		}

		public static OperatingSystemFamily operatingSystemFamily
		{
			get
			{
				return ShimManager.systemInfoShim.operatingSystemFamily;
			}
		}

		public static string processorType
		{
			get
			{
				return ShimManager.systemInfoShim.processorType;
			}
		}

		public static int processorFrequency
		{
			get
			{
				return ShimManager.systemInfoShim.processorFrequency;
			}
		}

		public static int processorCount
		{
			get
			{
				return ShimManager.systemInfoShim.processorCount;
			}
		}

		public static int systemMemorySize
		{
			get
			{
				return ShimManager.systemInfoShim.systemMemorySize;
			}
		}

		public static string deviceUniqueIdentifier
		{
			get
			{
				return ShimManager.systemInfoShim.deviceUniqueIdentifier;
			}
		}

		public static string deviceName
		{
			get
			{
				return ShimManager.systemInfoShim.deviceName;
			}
		}

		public static string deviceModel
		{
			get
			{
				return ShimManager.systemInfoShim.deviceModel;
			}
		}

		public static bool supportsAccelerometer
		{
			get
			{
				return ShimManager.systemInfoShim.supportsAccelerometer;
			}
		}

		public static bool supportsGyroscope
		{
			get
			{
				return ShimManager.systemInfoShim.supportsGyroscope;
			}
		}

		public static bool supportsLocationService
		{
			get
			{
				return ShimManager.systemInfoShim.supportsLocationService;
			}
		}

		public static bool supportsVibration
		{
			get
			{
				return ShimManager.systemInfoShim.supportsVibration;
			}
		}

		public static bool supportsAudio
		{
			get
			{
				return ShimManager.systemInfoShim.supportsAudio;
			}
		}

		public static DeviceType deviceType
		{
			get
			{
				return ShimManager.systemInfoShim.deviceType;
			}
		}

		public static int graphicsMemorySize
		{
			get
			{
				return ShimManager.systemInfoShim.graphicsMemorySize;
			}
		}

		public static string graphicsDeviceName
		{
			get
			{
				return ShimManager.systemInfoShim.graphicsDeviceName;
			}
		}

		public static string graphicsDeviceVendor
		{
			get
			{
				return ShimManager.systemInfoShim.graphicsDeviceVendor;
			}
		}

		public static int graphicsDeviceID
		{
			get
			{
				return ShimManager.systemInfoShim.graphicsDeviceID;
			}
		}

		public static int graphicsDeviceVendorID
		{
			get
			{
				return ShimManager.systemInfoShim.graphicsDeviceVendorID;
			}
		}

		public static GraphicsDeviceType graphicsDeviceType
		{
			get
			{
				return ShimManager.systemInfoShim.graphicsDeviceType;
			}
		}

		public static bool graphicsUVStartsAtTop
		{
			get
			{
				return ShimManager.systemInfoShim.graphicsUVStartsAtTop;
			}
		}

		public static string graphicsDeviceVersion
		{
			get
			{
				return ShimManager.systemInfoShim.graphicsDeviceVersion;
			}
		}

		public static int graphicsShaderLevel
		{
			get
			{
				return ShimManager.systemInfoShim.graphicsShaderLevel;
			}
		}

		public static bool graphicsMultiThreaded
		{
			get
			{
				return ShimManager.systemInfoShim.graphicsMultiThreaded;
			}
		}

		public static RenderingThreadingMode renderingThreadingMode
		{
			get
			{
				return ShimManager.systemInfoShim.renderingThreadingMode;
			}
		}

		public static bool hasHiddenSurfaceRemovalOnGPU
		{
			get
			{
				return ShimManager.systemInfoShim.hasHiddenSurfaceRemovalOnGPU;
			}
		}

		public static bool hasDynamicUniformArrayIndexingInFragmentShaders
		{
			get
			{
				return ShimManager.systemInfoShim.hasDynamicUniformArrayIndexingInFragmentShaders;
			}
		}

		public static bool supportsShadows
		{
			get
			{
				return ShimManager.systemInfoShim.supportsShadows;
			}
		}

		public static bool supportsRawShadowDepthSampling
		{
			get
			{
				return ShimManager.systemInfoShim.supportsRawShadowDepthSampling;
			}
		}

		public static bool supportsMotionVectors
		{
			get
			{
				return ShimManager.systemInfoShim.supportsMotionVectors;
			}
		}

		public static bool supports3DTextures
		{
			get
			{
				return ShimManager.systemInfoShim.supports3DTextures;
			}
		}

		public static bool supportsCompressed3DTextures
		{
			get
			{
				return ShimManager.systemInfoShim.supportsCompressed3DTextures;
			}
		}

		public static bool supports2DArrayTextures
		{
			get
			{
				return ShimManager.systemInfoShim.supports2DArrayTextures;
			}
		}

		public static bool supports3DRenderTextures
		{
			get
			{
				return ShimManager.systemInfoShim.supports3DRenderTextures;
			}
		}

		public static bool supportsCubemapArrayTextures
		{
			get
			{
				return ShimManager.systemInfoShim.supportsCubemapArrayTextures;
			}
		}

		public static CopyTextureSupport copyTextureSupport
		{
			get
			{
				return ShimManager.systemInfoShim.copyTextureSupport;
			}
		}

		public static bool supportsComputeShaders
		{
			get
			{
				return ShimManager.systemInfoShim.supportsComputeShaders;
			}
		}

		public static bool supportsConservativeRaster
		{
			get
			{
				return ShimManager.systemInfoShim.supportsConservativeRaster;
			}
		}

		public static bool supportsMultiview
		{
			get
			{
				return ShimManager.systemInfoShim.supportsMultiview;
			}
		}

		public static bool supportsGeometryShaders
		{
			get
			{
				return ShimManager.systemInfoShim.supportsGeometryShaders;
			}
		}

		public static bool supportsTessellationShaders
		{
			get
			{
				return ShimManager.systemInfoShim.supportsTessellationShaders;
			}
		}

		public static bool supportsRenderTargetArrayIndexFromVertexShader
		{
			get
			{
				return ShimManager.systemInfoShim.supportsRenderTargetArrayIndexFromVertexShader;
			}
		}

		public static bool supportsInstancing
		{
			get
			{
				return ShimManager.systemInfoShim.supportsInstancing;
			}
		}

		public static bool supportsHardwareQuadTopology
		{
			get
			{
				return ShimManager.systemInfoShim.supportsHardwareQuadTopology;
			}
		}

		public static bool supports32bitsIndexBuffer
		{
			get
			{
				return ShimManager.systemInfoShim.supports32bitsIndexBuffer;
			}
		}

		public static bool supportsSparseTextures
		{
			get
			{
				return ShimManager.systemInfoShim.supportsSparseTextures;
			}
		}

		public static int supportedRenderTargetCount
		{
			get
			{
				return ShimManager.systemInfoShim.supportedRenderTargetCount;
			}
		}

		public static bool supportsSeparatedRenderTargetsBlend
		{
			get
			{
				return ShimManager.systemInfoShim.supportsSeparatedRenderTargetsBlend;
			}
		}

		public static int supportedRandomWriteTargetCount
		{
			get
			{
				return ShimManager.systemInfoShim.supportedRandomWriteTargetCount;
			}
		}

		public static int supportsMultisampledTextures
		{
			get
			{
				return ShimManager.systemInfoShim.supportsMultisampledTextures;
			}
		}

		public static bool supportsMultisampled2DArrayTextures
		{
			get
			{
				return ShimManager.systemInfoShim.supportsMultisampled2DArrayTextures;
			}
		}

		public static bool supportsMultisampleAutoResolve
		{
			get
			{
				return ShimManager.systemInfoShim.supportsMultisampleAutoResolve;
			}
		}

		public static int supportsTextureWrapMirrorOnce
		{
			get
			{
				return ShimManager.systemInfoShim.supportsTextureWrapMirrorOnce;
			}
		}

		public static bool usesReversedZBuffer
		{
			get
			{
				return ShimManager.systemInfoShim.usesReversedZBuffer;
			}
		}

		public static NPOTSupport npotSupport
		{
			get
			{
				return ShimManager.systemInfoShim.npotSupport;
			}
		}

		public static int maxTextureSize
		{
			get
			{
				return ShimManager.systemInfoShim.maxTextureSize;
			}
		}

		public static int maxCubemapSize
		{
			get
			{
				return ShimManager.systemInfoShim.maxCubemapSize;
			}
		}

		internal static int maxRenderTextureSize
		{
			get
			{
				return EditorSystemInfo.maxRenderTextureSize;
			}
		}

		public static int maxComputeBufferInputsVertex
		{
			get
			{
				return ShimManager.systemInfoShim.maxComputeBufferInputsVertex;
			}
		}

		public static int maxComputeBufferInputsFragment
		{
			get
			{
				return ShimManager.systemInfoShim.maxComputeBufferInputsFragment;
			}
		}

		public static int maxComputeBufferInputsGeometry
		{
			get
			{
				return ShimManager.systemInfoShim.maxComputeBufferInputsGeometry;
			}
		}

		public static int maxComputeBufferInputsDomain
		{
			get
			{
				return ShimManager.systemInfoShim.maxComputeBufferInputsDomain;
			}
		}

		public static int maxComputeBufferInputsHull
		{
			get
			{
				return ShimManager.systemInfoShim.maxComputeBufferInputsHull;
			}
		}

		public static int maxComputeBufferInputsCompute
		{
			get
			{
				return ShimManager.systemInfoShim.maxComputeBufferInputsCompute;
			}
		}

		public static int maxComputeWorkGroupSize
		{
			get
			{
				return ShimManager.systemInfoShim.maxComputeWorkGroupSize;
			}
		}

		public static int maxComputeWorkGroupSizeX
		{
			get
			{
				return ShimManager.systemInfoShim.maxComputeWorkGroupSizeX;
			}
		}

		public static int maxComputeWorkGroupSizeY
		{
			get
			{
				return ShimManager.systemInfoShim.maxComputeWorkGroupSizeY;
			}
		}

		public static int maxComputeWorkGroupSizeZ
		{
			get
			{
				return ShimManager.systemInfoShim.maxComputeWorkGroupSizeZ;
			}
		}

		public static bool supportsAsyncCompute
		{
			get
			{
				return ShimManager.systemInfoShim.supportsAsyncCompute;
			}
		}

		public static bool supportsGpuRecorder
		{
			get
			{
				return ShimManager.systemInfoShim.supportsGpuRecorder;
			}
		}

		public static bool supportsGraphicsFence
		{
			get
			{
				return ShimManager.systemInfoShim.supportsGraphicsFence;
			}
		}

		public static bool supportsAsyncGPUReadback
		{
			get
			{
				return ShimManager.systemInfoShim.supportsAsyncGPUReadback;
			}
		}

		public static bool supportsRayTracing
		{
			get
			{
				return ShimManager.systemInfoShim.supportsRayTracing;
			}
		}

		public static bool supportsSetConstantBuffer
		{
			get
			{
				return ShimManager.systemInfoShim.supportsSetConstantBuffer;
			}
		}

		public static int constantBufferOffsetAlignment
		{
			get
			{
				return ShimManager.systemInfoShim.constantBufferOffsetAlignment;
			}
		}

		[Obsolete("Use SystemInfo.constantBufferOffsetAlignment instead.")]
		public static bool minConstantBufferOffsetAlignment
		{
			get
			{
				return ShimManager.systemInfoShim.minConstantBufferOffsetAlignment;
			}
		}

		public static bool hasMipMaxLevel
		{
			get
			{
				return ShimManager.systemInfoShim.hasMipMaxLevel;
			}
		}

		public static bool supportsMipStreaming
		{
			get
			{
				return ShimManager.systemInfoShim.supportsMipStreaming;
			}
		}

		public static bool usesLoadStoreActions
		{
			get
			{
				return ShimManager.systemInfoShim.usesLoadStoreActions;
			}
		}

		public static HDRDisplaySupportFlags hdrDisplaySupportFlags
		{
			get
			{
				return ShimManager.systemInfoShim.hdrDisplaySupportFlags;
			}
		}

		[Obsolete("supportsRenderTextures always returns true, no need to call it")]
		public static bool supportsRenderTextures
		{
			get
			{
				return EditorSystemInfo.supportsRenderTextures;
			}
		}

		[Obsolete("supportsRenderToCubemap always returns true, no need to call it")]
		public static bool supportsRenderToCubemap
		{
			get
			{
				return EditorSystemInfo.supportsRenderToCubemap;
			}
		}

		[Obsolete("supportsImageEffects always returns true, no need to call it")]
		public static bool supportsImageEffects
		{
			get
			{
				return EditorSystemInfo.supportsImageEffects;
			}
		}

		[Obsolete("supportsStencil always returns true, no need to call it")]
		public static int supportsStencil
		{
			get
			{
				return EditorSystemInfo.supportsStencil;
			}
		}

		[Obsolete("graphicsPixelFillrate is no longer supported in Unity 5.0+.")]
		public static int graphicsPixelFillrate
		{
			get
			{
				return EditorSystemInfo.graphicsPixelFillrate;
			}
		}

		[Obsolete("Vertex program support is required in Unity 5.0+")]
		public static bool supportsVertexPrograms
		{
			get
			{
				return EditorSystemInfo.supportsVertexPrograms;
			}
		}

		[Obsolete("SystemInfo.supportsGPUFence has been deprecated, use SystemInfo.supportsGraphicsFence instead (UnityUpgradable) ->  supportsGraphicsFence", true)]
		public static bool supportsGPUFence
		{
			get
			{
				return false;
			}
		}

		public static bool SupportsRenderTextureFormat(RenderTextureFormat format)
		{
			return ShimManager.systemInfoShim.SupportsRenderTextureFormat(format);
		}

		public static bool SupportsBlendingOnRenderTextureFormat(RenderTextureFormat format)
		{
			return ShimManager.systemInfoShim.SupportsBlendingOnRenderTextureFormat(format);
		}

		public static bool SupportsTextureFormat(TextureFormat format)
		{
			return ShimManager.systemInfoShim.SupportsTextureFormat(format);
		}

		public static bool SupportsVertexAttributeFormat(VertexAttributeFormat format, int dimension)
		{
			return ShimManager.systemInfoShim.SupportsVertexAttributeFormat(format, dimension);
		}

		public static bool IsFormatSupported(GraphicsFormat format, FormatUsage usage)
		{
			return ShimManager.systemInfoShim.IsFormatSupported(format, usage);
		}

		public static GraphicsFormat GetCompatibleFormat(GraphicsFormat format, FormatUsage usage)
		{
			return ShimManager.systemInfoShim.GetCompatibleFormat(format, usage);
		}

		public static GraphicsFormat GetGraphicsFormat(DefaultFormat format)
		{
			return ShimManager.systemInfoShim.GetGraphicsFormat(format);
		}

		public static int GetRenderTextureSupportedMSAASampleCount(RenderTextureDescriptor desc)
		{
			return ShimManager.systemInfoShim.GetRenderTextureSupportedMSAASampleCount(desc);
		}
	}
}
