using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace UnityEngine
{
	internal class SystemInfoShimBase
	{
		public virtual string unsupportedIdentifier
		{
			get
			{
				return "n/a";
			}
		}

		public virtual float batteryLevel
		{
			get
			{
				return EditorSystemInfo.batteryLevel;
			}
		}

		public virtual BatteryStatus batteryStatus
		{
			get
			{
				return EditorSystemInfo.batteryStatus;
			}
		}

		public virtual string operatingSystem
		{
			get
			{
				return EditorSystemInfo.operatingSystem;
			}
		}

		public virtual OperatingSystemFamily operatingSystemFamily
		{
			get
			{
				return EditorSystemInfo.operatingSystemFamily;
			}
		}

		public virtual string processorType
		{
			get
			{
				return EditorSystemInfo.processorType;
			}
		}

		public virtual int processorFrequency
		{
			get
			{
				return EditorSystemInfo.processorFrequency;
			}
		}

		public virtual int processorCount
		{
			get
			{
				return EditorSystemInfo.processorCount;
			}
		}

		public virtual int systemMemorySize
		{
			get
			{
				return EditorSystemInfo.systemMemorySize;
			}
		}

		public virtual string deviceUniqueIdentifier
		{
			get
			{
				return EditorSystemInfo.deviceUniqueIdentifier;
			}
		}

		public virtual string deviceName
		{
			get
			{
				return EditorSystemInfo.deviceName;
			}
		}

		public virtual string deviceModel
		{
			get
			{
				return EditorSystemInfo.deviceModel;
			}
		}

		public virtual bool supportsAccelerometer
		{
			get
			{
				return EditorSystemInfo.supportsAccelerometer;
			}
		}

		public virtual bool supportsGyroscope
		{
			get
			{
				return EditorSystemInfo.supportsGyroscope;
			}
		}

		public virtual bool supportsLocationService
		{
			get
			{
				return EditorSystemInfo.supportsLocationService;
			}
		}

		public virtual bool supportsVibration
		{
			get
			{
				return EditorSystemInfo.supportsVibration;
			}
		}

		public virtual bool supportsAudio
		{
			get
			{
				return EditorSystemInfo.supportsAudio;
			}
		}

		public virtual DeviceType deviceType
		{
			get
			{
				return EditorSystemInfo.deviceType;
			}
		}

		public virtual int graphicsMemorySize
		{
			get
			{
				return EditorSystemInfo.graphicsMemorySize;
			}
		}

		public virtual string graphicsDeviceName
		{
			get
			{
				return EditorSystemInfo.graphicsDeviceName;
			}
		}

		public virtual string graphicsDeviceVendor
		{
			get
			{
				return EditorSystemInfo.graphicsDeviceVendor;
			}
		}

		public virtual int graphicsDeviceID
		{
			get
			{
				return EditorSystemInfo.graphicsDeviceID;
			}
		}

		public virtual int graphicsDeviceVendorID
		{
			get
			{
				return EditorSystemInfo.graphicsDeviceVendorID;
			}
		}

		public virtual GraphicsDeviceType graphicsDeviceType
		{
			get
			{
				return EditorSystemInfo.graphicsDeviceType;
			}
		}

		public virtual bool graphicsUVStartsAtTop
		{
			get
			{
				return EditorSystemInfo.graphicsUVStartsAtTop;
			}
		}

		public virtual string graphicsDeviceVersion
		{
			get
			{
				return EditorSystemInfo.graphicsDeviceVersion;
			}
		}

		public virtual int graphicsShaderLevel
		{
			get
			{
				return EditorSystemInfo.graphicsShaderLevel;
			}
		}

		public virtual bool graphicsMultiThreaded
		{
			get
			{
				return EditorSystemInfo.graphicsMultiThreaded;
			}
		}

		public virtual RenderingThreadingMode renderingThreadingMode
		{
			get
			{
				return EditorSystemInfo.renderingThreadingMode;
			}
		}

		public virtual bool hasHiddenSurfaceRemovalOnGPU
		{
			get
			{
				return EditorSystemInfo.hasHiddenSurfaceRemovalOnGPU;
			}
		}

		public virtual bool hasDynamicUniformArrayIndexingInFragmentShaders
		{
			get
			{
				return EditorSystemInfo.hasDynamicUniformArrayIndexingInFragmentShaders;
			}
		}

		public virtual bool supportsShadows
		{
			get
			{
				return EditorSystemInfo.supportsShadows;
			}
		}

		public virtual bool supportsRawShadowDepthSampling
		{
			get
			{
				return EditorSystemInfo.supportsRawShadowDepthSampling;
			}
		}

		public virtual bool supportsMotionVectors
		{
			get
			{
				return EditorSystemInfo.supportsMotionVectors;
			}
		}

		public virtual bool supports3DTextures
		{
			get
			{
				return EditorSystemInfo.supports3DTextures;
			}
		}

		public virtual bool supportsCompressed3DTextures
		{
			get
			{
				return EditorSystemInfo.supportsCompressed3DTextures;
			}
		}

		public virtual bool supports2DArrayTextures
		{
			get
			{
				return EditorSystemInfo.supports2DArrayTextures;
			}
		}

		public virtual bool supports3DRenderTextures
		{
			get
			{
				return EditorSystemInfo.supports3DRenderTextures;
			}
		}

		public virtual bool supportsCubemapArrayTextures
		{
			get
			{
				return EditorSystemInfo.supportsCubemapArrayTextures;
			}
		}

		public virtual CopyTextureSupport copyTextureSupport
		{
			get
			{
				return EditorSystemInfo.copyTextureSupport;
			}
		}

		public virtual bool supportsComputeShaders
		{
			get
			{
				return EditorSystemInfo.supportsComputeShaders;
			}
		}

		public virtual bool supportsGeometryShaders
		{
			get
			{
				return EditorSystemInfo.supportsGeometryShaders;
			}
		}

		public virtual bool supportsTessellationShaders
		{
			get
			{
				return EditorSystemInfo.supportsTessellationShaders;
			}
		}

		public virtual bool supportsRenderTargetArrayIndexFromVertexShader
		{
			get
			{
				return EditorSystemInfo.supportsRenderTargetArrayIndexFromVertexShader;
			}
		}

		public virtual bool supportsInstancing
		{
			get
			{
				return EditorSystemInfo.supportsInstancing;
			}
		}

		public virtual bool supportsHardwareQuadTopology
		{
			get
			{
				return EditorSystemInfo.supportsHardwareQuadTopology;
			}
		}

		public virtual bool supports32bitsIndexBuffer
		{
			get
			{
				return EditorSystemInfo.supports32bitsIndexBuffer;
			}
		}

		public virtual bool supportsSparseTextures
		{
			get
			{
				return EditorSystemInfo.supportsSparseTextures;
			}
		}

		public virtual int supportedRenderTargetCount
		{
			get
			{
				return EditorSystemInfo.supportedRenderTargetCount;
			}
		}

		public virtual bool supportsSeparatedRenderTargetsBlend
		{
			get
			{
				return EditorSystemInfo.supportsSeparatedRenderTargetsBlend;
			}
		}

		public virtual int supportedRandomWriteTargetCount
		{
			get
			{
				return EditorSystemInfo.supportedRandomWriteTargetCount;
			}
		}

		public virtual int supportsMultisampledTextures
		{
			get
			{
				return EditorSystemInfo.supportsMultisampledTextures;
			}
		}

		public virtual bool supportsMultisampled2DArrayTextures
		{
			get
			{
				return EditorSystemInfo.supportsMultisampled2DArrayTextures;
			}
		}

		public virtual bool supportsMultisampleAutoResolve
		{
			get
			{
				return EditorSystemInfo.supportsMultisampleAutoResolve;
			}
		}

		public virtual int supportsTextureWrapMirrorOnce
		{
			get
			{
				return EditorSystemInfo.supportsTextureWrapMirrorOnce;
			}
		}

		public virtual bool usesReversedZBuffer
		{
			get
			{
				return EditorSystemInfo.usesReversedZBuffer;
			}
		}

		public virtual NPOTSupport npotSupport
		{
			get
			{
				return EditorSystemInfo.npotSupport;
			}
		}

		public virtual int maxTextureSize
		{
			get
			{
				return EditorSystemInfo.maxTextureSize;
			}
		}

		public virtual int maxCubemapSize
		{
			get
			{
				return EditorSystemInfo.maxCubemapSize;
			}
		}

		public virtual int maxComputeBufferInputsVertex
		{
			get
			{
				return EditorSystemInfo.maxComputeBufferInputsVertex;
			}
		}

		public virtual int maxComputeBufferInputsFragment
		{
			get
			{
				return EditorSystemInfo.maxComputeBufferInputsFragment;
			}
		}

		public virtual int maxComputeBufferInputsGeometry
		{
			get
			{
				return EditorSystemInfo.maxComputeBufferInputsGeometry;
			}
		}

		public virtual int maxComputeBufferInputsDomain
		{
			get
			{
				return EditorSystemInfo.maxComputeBufferInputsDomain;
			}
		}

		public virtual int maxComputeBufferInputsHull
		{
			get
			{
				return EditorSystemInfo.maxComputeBufferInputsHull;
			}
		}

		public virtual int maxComputeBufferInputsCompute
		{
			get
			{
				return EditorSystemInfo.maxComputeBufferInputsCompute;
			}
		}

		public virtual int maxComputeWorkGroupSize
		{
			get
			{
				return EditorSystemInfo.maxComputeWorkGroupSize;
			}
		}

		public virtual int maxComputeWorkGroupSizeX
		{
			get
			{
				return EditorSystemInfo.maxComputeWorkGroupSizeX;
			}
		}

		public virtual int maxComputeWorkGroupSizeY
		{
			get
			{
				return EditorSystemInfo.maxComputeWorkGroupSizeY;
			}
		}

		public virtual int maxComputeWorkGroupSizeZ
		{
			get
			{
				return EditorSystemInfo.maxComputeWorkGroupSizeZ;
			}
		}

		public virtual bool supportsAsyncCompute
		{
			get
			{
				return EditorSystemInfo.supportsAsyncCompute;
			}
		}

		public virtual bool supportsGpuRecorder
		{
			get
			{
				return EditorSystemInfo.supportsGpuRecorder;
			}
		}

		public virtual bool supportsGraphicsFence
		{
			get
			{
				return EditorSystemInfo.supportsGraphicsFence;
			}
		}

		public virtual bool supportsAsyncGPUReadback
		{
			get
			{
				return EditorSystemInfo.supportsAsyncGPUReadback;
			}
		}

		public virtual bool supportsRayTracing
		{
			get
			{
				return EditorSystemInfo.supportsRayTracing;
			}
		}

		public virtual bool supportsSetConstantBuffer
		{
			get
			{
				return EditorSystemInfo.supportsSetConstantBuffer;
			}
		}

		public virtual int constantBufferOffsetAlignment
		{
			get
			{
				return EditorSystemInfo.constantBufferOffsetAlignment;
			}
		}

		[Obsolete("Use SystemInfo.constantBufferOffsetAlignment instead.")]
		public virtual bool minConstantBufferOffsetAlignment
		{
			get
			{
				return EditorSystemInfo.minConstantBufferOffsetAlignment;
			}
		}

		public virtual bool hasMipMaxLevel
		{
			get
			{
				return EditorSystemInfo.hasMipMaxLevel;
			}
		}

		public virtual bool supportsMipStreaming
		{
			get
			{
				return EditorSystemInfo.supportsMipStreaming;
			}
		}

		public virtual bool usesLoadStoreActions
		{
			get
			{
				return EditorSystemInfo.usesLoadStoreActions;
			}
		}

		public virtual HDRDisplaySupportFlags hdrDisplaySupportFlags
		{
			get
			{
				return EditorSystemInfo.hdrDisplaySupportFlags;
			}
		}

		public virtual bool supportsConservativeRaster
		{
			get
			{
				return EditorSystemInfo.supportsConservativeRaster;
			}
		}

		public virtual bool supportsMultiview
		{
			get
			{
				return EditorSystemInfo.supportsMultiview;
			}
		}

		public virtual bool SupportsRenderTextureFormat(RenderTextureFormat format)
		{
			return EditorSystemInfo.SupportsRenderTextureFormat(format);
		}

		public virtual bool SupportsBlendingOnRenderTextureFormat(RenderTextureFormat format)
		{
			return EditorSystemInfo.SupportsBlendingOnRenderTextureFormat(format);
		}

		public virtual bool SupportsTextureFormat(TextureFormat format)
		{
			return EditorSystemInfo.SupportsTextureFormat(format);
		}

		public virtual bool SupportsVertexAttributeFormat(VertexAttributeFormat format, int dimension)
		{
			return EditorSystemInfo.SupportsVertexAttributeFormat(format, dimension);
		}

		public virtual bool IsFormatSupported(GraphicsFormat format, FormatUsage usage)
		{
			return EditorSystemInfo.IsFormatSupported(format, usage);
		}

		public virtual GraphicsFormat GetCompatibleFormat(GraphicsFormat format, FormatUsage usage)
		{
			return EditorSystemInfo.GetCompatibleFormat(format, usage);
		}

		public virtual GraphicsFormat GetGraphicsFormat(DefaultFormat format)
		{
			return EditorSystemInfo.GetGraphicsFormat(format);
		}

		public virtual int GetRenderTextureSupportedMSAASampleCount(RenderTextureDescriptor desc)
		{
			return EditorSystemInfo.GetRenderTextureSupportedMSAASampleCount(desc);
		}
	}
}
