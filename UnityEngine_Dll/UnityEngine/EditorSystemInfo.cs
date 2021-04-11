using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace UnityEngine
{
	[NativeHeader("Runtime/Shaders/GraphicsCapsScriptBindings.h"), NativeHeader("Runtime/Misc/SystemInfo.h"), NativeHeader("Runtime/Input/GetInput.h"), NativeHeader("Runtime/Graphics/GraphicsFormatUtility.bindings.h"), NativeHeader("Runtime/Camera/RenderLoops/MotionVectorRenderLoop.h"), NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
	internal sealed class EditorSystemInfo
	{
		public const string unsupportedIdentifier = "n/a";

		[NativeProperty]
		public static float batteryLevel
		{
			get
			{
				return EditorSystemInfo.GetBatteryLevel();
			}
		}

		public static BatteryStatus batteryStatus
		{
			get
			{
				return EditorSystemInfo.GetBatteryStatus();
			}
		}

		public static string operatingSystem
		{
			get
			{
				return EditorSystemInfo.GetOperatingSystem();
			}
		}

		public static OperatingSystemFamily operatingSystemFamily
		{
			get
			{
				return EditorSystemInfo.GetOperatingSystemFamily();
			}
		}

		public static string processorType
		{
			get
			{
				return EditorSystemInfo.GetProcessorType();
			}
		}

		public static int processorFrequency
		{
			get
			{
				return EditorSystemInfo.GetProcessorFrequencyMHz();
			}
		}

		public static int processorCount
		{
			get
			{
				return EditorSystemInfo.GetProcessorCount();
			}
		}

		public static int systemMemorySize
		{
			get
			{
				return EditorSystemInfo.GetPhysicalMemoryMB();
			}
		}

		public static string deviceUniqueIdentifier
		{
			get
			{
				return EditorSystemInfo.GetDeviceUniqueIdentifier();
			}
		}

		public static string deviceName
		{
			get
			{
				return EditorSystemInfo.GetDeviceName();
			}
		}

		public static string deviceModel
		{
			get
			{
				return EditorSystemInfo.GetDeviceModel();
			}
		}

		public static bool supportsAccelerometer
		{
			get
			{
				return EditorSystemInfo.SupportsAccelerometer();
			}
		}

		public static bool supportsGyroscope
		{
			get
			{
				return EditorSystemInfo.IsGyroAvailable();
			}
		}

		public static bool supportsLocationService
		{
			get
			{
				return EditorSystemInfo.SupportsLocationService();
			}
		}

		public static bool supportsVibration
		{
			get
			{
				return EditorSystemInfo.SupportsVibration();
			}
		}

		public static bool supportsAudio
		{
			get
			{
				return EditorSystemInfo.SupportsAudio();
			}
		}

		public static DeviceType deviceType
		{
			get
			{
				return EditorSystemInfo.GetDeviceType();
			}
		}

		public static int graphicsMemorySize
		{
			get
			{
				return EditorSystemInfo.GetGraphicsMemorySize();
			}
		}

		public static string graphicsDeviceName
		{
			get
			{
				return EditorSystemInfo.GetGraphicsDeviceName();
			}
		}

		public static string graphicsDeviceVendor
		{
			get
			{
				return EditorSystemInfo.GetGraphicsDeviceVendor();
			}
		}

		public static int graphicsDeviceID
		{
			get
			{
				return EditorSystemInfo.GetGraphicsDeviceID();
			}
		}

		public static int graphicsDeviceVendorID
		{
			get
			{
				return EditorSystemInfo.GetGraphicsDeviceVendorID();
			}
		}

		public static GraphicsDeviceType graphicsDeviceType
		{
			get
			{
				return EditorSystemInfo.GetGraphicsDeviceType();
			}
		}

		public static bool graphicsUVStartsAtTop
		{
			get
			{
				return EditorSystemInfo.GetGraphicsUVStartsAtTop();
			}
		}

		public static string graphicsDeviceVersion
		{
			get
			{
				return EditorSystemInfo.GetGraphicsDeviceVersion();
			}
		}

		public static int graphicsShaderLevel
		{
			get
			{
				return EditorSystemInfo.GetGraphicsShaderLevel();
			}
		}

		public static bool graphicsMultiThreaded
		{
			get
			{
				return EditorSystemInfo.GetGraphicsMultiThreaded();
			}
		}

		public static RenderingThreadingMode renderingThreadingMode
		{
			get
			{
				return EditorSystemInfo.GetRenderingThreadingMode();
			}
		}

		public static bool hasHiddenSurfaceRemovalOnGPU
		{
			get
			{
				return EditorSystemInfo.HasHiddenSurfaceRemovalOnGPU();
			}
		}

		public static bool hasDynamicUniformArrayIndexingInFragmentShaders
		{
			get
			{
				return EditorSystemInfo.HasDynamicUniformArrayIndexingInFragmentShaders();
			}
		}

		public static bool supportsShadows
		{
			get
			{
				return EditorSystemInfo.SupportsShadows();
			}
		}

		public static bool supportsRawShadowDepthSampling
		{
			get
			{
				return EditorSystemInfo.SupportsRawShadowDepthSampling();
			}
		}

		[Obsolete("supportsRenderTextures always returns true, no need to call it")]
		public static bool supportsRenderTextures
		{
			get
			{
				return true;
			}
		}

		public static bool supportsMotionVectors
		{
			get
			{
				return EditorSystemInfo.SupportsMotionVectors();
			}
		}

		[Obsolete("supportsRenderToCubemap always returns true, no need to call it")]
		public static bool supportsRenderToCubemap
		{
			get
			{
				return true;
			}
		}

		[Obsolete("supportsImageEffects always returns true, no need to call it")]
		public static bool supportsImageEffects
		{
			get
			{
				return true;
			}
		}

		public static bool supports3DTextures
		{
			get
			{
				return EditorSystemInfo.Supports3DTextures();
			}
		}

		public static bool supportsCompressed3DTextures
		{
			get
			{
				return EditorSystemInfo.SupportsCompressed3DTextures();
			}
		}

		public static bool supports2DArrayTextures
		{
			get
			{
				return EditorSystemInfo.Supports2DArrayTextures();
			}
		}

		public static bool supports3DRenderTextures
		{
			get
			{
				return EditorSystemInfo.Supports3DRenderTextures();
			}
		}

		public static bool supportsCubemapArrayTextures
		{
			get
			{
				return EditorSystemInfo.SupportsCubemapArrayTextures();
			}
		}

		public static CopyTextureSupport copyTextureSupport
		{
			get
			{
				return EditorSystemInfo.GetCopyTextureSupport();
			}
		}

		public static bool supportsComputeShaders
		{
			get
			{
				return EditorSystemInfo.SupportsComputeShaders();
			}
		}

		public static bool supportsGeometryShaders
		{
			get
			{
				return EditorSystemInfo.SupportsGeometryShaders();
			}
		}

		public static bool supportsTessellationShaders
		{
			get
			{
				return EditorSystemInfo.SupportsTessellationShaders();
			}
		}

		public static bool supportsRenderTargetArrayIndexFromVertexShader
		{
			get
			{
				return EditorSystemInfo.SupportsRenderTargetArrayIndexFromVertexShader();
			}
		}

		public static bool supportsInstancing
		{
			get
			{
				return EditorSystemInfo.SupportsInstancing();
			}
		}

		public static bool supportsHardwareQuadTopology
		{
			get
			{
				return EditorSystemInfo.SupportsHardwareQuadTopology();
			}
		}

		public static bool supports32bitsIndexBuffer
		{
			get
			{
				return EditorSystemInfo.Supports32bitsIndexBuffer();
			}
		}

		public static bool supportsSparseTextures
		{
			get
			{
				return EditorSystemInfo.SupportsSparseTextures();
			}
		}

		public static int supportedRenderTargetCount
		{
			get
			{
				return EditorSystemInfo.SupportedRenderTargetCount();
			}
		}

		public static bool supportsSeparatedRenderTargetsBlend
		{
			get
			{
				return EditorSystemInfo.SupportsSeparatedRenderTargetsBlend();
			}
		}

		public static int supportedRandomWriteTargetCount
		{
			get
			{
				return EditorSystemInfo.SupportedRandomWriteTargetCount();
			}
		}

		public static int supportsMultisampledTextures
		{
			get
			{
				return EditorSystemInfo.SupportsMultisampledTextures();
			}
		}

		public static bool supportsMultisampled2DArrayTextures
		{
			get
			{
				return EditorSystemInfo.SupportsMultisampled2DArrayTextures();
			}
		}

		public static bool supportsMultisampleAutoResolve
		{
			get
			{
				return EditorSystemInfo.SupportsMultisampleAutoResolve();
			}
		}

		public static int supportsTextureWrapMirrorOnce
		{
			get
			{
				return EditorSystemInfo.SupportsTextureWrapMirrorOnce();
			}
		}

		public static bool usesReversedZBuffer
		{
			get
			{
				return EditorSystemInfo.UsesReversedZBuffer();
			}
		}

		[Obsolete("supportsStencil always returns true, no need to call it")]
		public static int supportsStencil
		{
			get
			{
				return 1;
			}
		}

		public static NPOTSupport npotSupport
		{
			get
			{
				return EditorSystemInfo.GetNPOTSupport();
			}
		}

		public static int maxTextureSize
		{
			get
			{
				return EditorSystemInfo.GetMaxTextureSize();
			}
		}

		public static int maxCubemapSize
		{
			get
			{
				return EditorSystemInfo.GetMaxCubemapSize();
			}
		}

		internal static int maxRenderTextureSize
		{
			get
			{
				return EditorSystemInfo.GetMaxRenderTextureSize();
			}
		}

		public static int maxComputeBufferInputsVertex
		{
			get
			{
				return EditorSystemInfo.MaxComputeBufferInputsVertex();
			}
		}

		public static int maxComputeBufferInputsFragment
		{
			get
			{
				return EditorSystemInfo.MaxComputeBufferInputsFragment();
			}
		}

		public static int maxComputeBufferInputsGeometry
		{
			get
			{
				return EditorSystemInfo.MaxComputeBufferInputsGeometry();
			}
		}

		public static int maxComputeBufferInputsDomain
		{
			get
			{
				return EditorSystemInfo.MaxComputeBufferInputsDomain();
			}
		}

		public static int maxComputeBufferInputsHull
		{
			get
			{
				return EditorSystemInfo.MaxComputeBufferInputsHull();
			}
		}

		public static int maxComputeBufferInputsCompute
		{
			get
			{
				return EditorSystemInfo.MaxComputeBufferInputsCompute();
			}
		}

		public static int maxComputeWorkGroupSize
		{
			get
			{
				return EditorSystemInfo.GetMaxComputeWorkGroupSize();
			}
		}

		public static int maxComputeWorkGroupSizeX
		{
			get
			{
				return EditorSystemInfo.GetMaxComputeWorkGroupSizeX();
			}
		}

		public static int maxComputeWorkGroupSizeY
		{
			get
			{
				return EditorSystemInfo.GetMaxComputeWorkGroupSizeY();
			}
		}

		public static int maxComputeWorkGroupSizeZ
		{
			get
			{
				return EditorSystemInfo.GetMaxComputeWorkGroupSizeZ();
			}
		}

		public static bool supportsAsyncCompute
		{
			get
			{
				return EditorSystemInfo.SupportsAsyncCompute();
			}
		}

		public static bool supportsGpuRecorder
		{
			get
			{
				return EditorSystemInfo.SupportsGpuRecorder();
			}
		}

		public static bool supportsGraphicsFence
		{
			get
			{
				return EditorSystemInfo.SupportsGPUFence();
			}
		}

		public static bool supportsAsyncGPUReadback
		{
			get
			{
				return EditorSystemInfo.SupportsAsyncGPUReadback();
			}
		}

		public static bool supportsRayTracing
		{
			get
			{
				return EditorSystemInfo.SupportsRayTracing();
			}
		}

		public static bool supportsSetConstantBuffer
		{
			get
			{
				return EditorSystemInfo.SupportsSetConstantBuffer();
			}
		}

		public static int constantBufferOffsetAlignment
		{
			get
			{
				return EditorSystemInfo.MinConstantBufferOffsetAlignment();
			}
		}

		[Obsolete("Use SystemInfo.constantBufferOffsetAlignment instead.")]
		public static bool minConstantBufferOffsetAlignment
		{
			get
			{
				return false;
			}
		}

		public static bool hasMipMaxLevel
		{
			get
			{
				return EditorSystemInfo.HasMipMaxLevel();
			}
		}

		public static bool supportsMipStreaming
		{
			get
			{
				return EditorSystemInfo.SupportsMipStreaming();
			}
		}

		[Obsolete("graphicsPixelFillrate is no longer supported in Unity 5.0+.")]
		public static int graphicsPixelFillrate
		{
			get
			{
				return -1;
			}
		}

		public static bool usesLoadStoreActions
		{
			get
			{
				return EditorSystemInfo.UsesLoadStoreActions();
			}
		}

		public static HDRDisplaySupportFlags hdrDisplaySupportFlags
		{
			get
			{
				return EditorSystemInfo.GetHDRDisplaySupportFlags();
			}
		}

		public static bool supportsConservativeRaster
		{
			get
			{
				return EditorSystemInfo.SupportsConservativeRaster();
			}
		}

		public static bool supportsMultiview
		{
			get
			{
				return EditorSystemInfo.SupportsMultiview();
			}
		}

		[Obsolete("Vertex program support is required in Unity 5.0+")]
		public static bool supportsVertexPrograms
		{
			get
			{
				return true;
			}
		}

		internal static bool IsEnumValueObsolete(Enum value)
		{
			FieldInfo[] fields = value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public);
			bool result;
			for (int i = 0; i < fields.Length; i++)
			{
				FieldInfo fieldInfo = fields[i];
				bool flag = !object.Equals(fieldInfo.GetValue(null), value);
				if (!flag)
				{
					bool flag2 = fieldInfo.GetCustomAttributes(typeof(ObsoleteAttribute), false).Length != 0;
					bool flag3 = !flag2;
					if (flag3)
					{
						result = false;
						return result;
					}
				}
			}
			result = true;
			return result;
		}

		private static bool IsValidEnumValue(Enum value)
		{
			bool flag = !Enum.IsDefined(value.GetType(), value);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = EditorSystemInfo.IsEnumValueObsolete(value);
				result = !flag2;
			}
			return result;
		}

		public static bool SupportsRenderTextureFormat(RenderTextureFormat format)
		{
			bool flag = !EditorSystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsRenderTextureFormat; format is not a valid RenderTextureFormat");
			}
			return EditorSystemInfo.HasRenderTextureNative(format);
		}

		public static bool SupportsBlendingOnRenderTextureFormat(RenderTextureFormat format)
		{
			bool flag = !EditorSystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsBlendingOnRenderTextureFormat; format is not a valid RenderTextureFormat");
			}
			return EditorSystemInfo.SupportsBlendingOnRenderTextureFormatNative(format);
		}

		public static bool SupportsTextureFormat(TextureFormat format)
		{
			bool flag = !EditorSystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsTextureFormat; format is not a valid TextureFormat");
			}
			return EditorSystemInfo.SupportsTextureFormatNative(format);
		}

		public static bool SupportsVertexAttributeFormat(VertexAttributeFormat format, int dimension)
		{
			bool flag = !EditorSystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsVertexAttributeFormat; format is not a valid VertexAttributeFormat");
			}
			bool flag2 = dimension < 1 || dimension > 4;
			if (flag2)
			{
				throw new ArgumentException("Failed SupportsVertexAttributeFormat; dimension must be in 1..4 range");
			}
			return EditorSystemInfo.SupportsVertexAttributeFormatNative(format, dimension);
		}

		[FreeFunction("systeminfo::GetBatteryLevel")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetBatteryLevel();

		[FreeFunction("systeminfo::GetBatteryStatus")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern BatteryStatus GetBatteryStatus();

		[FreeFunction("systeminfo::GetOperatingSystem")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetOperatingSystem();

		[FreeFunction("systeminfo::GetOperatingSystemFamily")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern OperatingSystemFamily GetOperatingSystemFamily();

		[FreeFunction("systeminfo::GetProcessorType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetProcessorType();

		[FreeFunction("systeminfo::GetProcessorFrequencyMHz")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetProcessorFrequencyMHz();

		[FreeFunction("systeminfo::GetProcessorCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetProcessorCount();

		[FreeFunction("systeminfo::GetPhysicalMemoryMB")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPhysicalMemoryMB();

		[FreeFunction("systeminfo::GetDeviceUniqueIdentifier")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetDeviceUniqueIdentifier();

		[FreeFunction("systeminfo::GetDeviceName")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetDeviceName();

		[FreeFunction("systeminfo::GetDeviceModel")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetDeviceModel();

		[FreeFunction("systeminfo::SupportsAccelerometer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsAccelerometer();

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsGyroAvailable();

		[FreeFunction("systeminfo::SupportsLocationService")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsLocationService();

		[FreeFunction("systeminfo::SupportsVibration")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsVibration();

		[FreeFunction("systeminfo::SupportsAudio")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsAudio();

		[FreeFunction("systeminfo::GetDeviceType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DeviceType GetDeviceType();

		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsMemorySize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGraphicsMemorySize();

		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceName")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetGraphicsDeviceName();

		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVendor")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetGraphicsDeviceVendor();

		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGraphicsDeviceID();

		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVendorID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGraphicsDeviceVendorID();

		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsDeviceType GetGraphicsDeviceType();

		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsUVStartsAtTop")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetGraphicsUVStartsAtTop();

		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVersion")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetGraphicsDeviceVersion();

		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsShaderLevel")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGraphicsShaderLevel();

		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsMultiThreaded")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetGraphicsMultiThreaded();

		[FreeFunction("ScriptingGraphicsCaps::GetRenderingThreadingMode")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderingThreadingMode GetRenderingThreadingMode();

		[FreeFunction("ScriptingGraphicsCaps::HasHiddenSurfaceRemovalOnGPU")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasHiddenSurfaceRemovalOnGPU();

		[FreeFunction("ScriptingGraphicsCaps::HasDynamicUniformArrayIndexingInFragmentShaders")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasDynamicUniformArrayIndexingInFragmentShaders();

		[FreeFunction("ScriptingGraphicsCaps::SupportsShadows")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsShadows();

		[FreeFunction("ScriptingGraphicsCaps::SupportsRawShadowDepthSampling")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsRawShadowDepthSampling();

		[FreeFunction("SupportsMotionVectors")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMotionVectors();

		[FreeFunction("ScriptingGraphicsCaps::Supports3DTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Supports3DTextures();

		[FreeFunction("ScriptingGraphicsCaps::SupportsCompressed3DTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsCompressed3DTextures();

		[FreeFunction("ScriptingGraphicsCaps::Supports2DArrayTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Supports2DArrayTextures();

		[FreeFunction("ScriptingGraphicsCaps::Supports3DRenderTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Supports3DRenderTextures();

		[FreeFunction("ScriptingGraphicsCaps::SupportsCubemapArrayTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsCubemapArrayTextures();

		[FreeFunction("ScriptingGraphicsCaps::GetCopyTextureSupport")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CopyTextureSupport GetCopyTextureSupport();

		[FreeFunction("ScriptingGraphicsCaps::SupportsComputeShaders")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsComputeShaders();

		[FreeFunction("ScriptingGraphicsCaps::SupportsGeometryShaders")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsGeometryShaders();

		[FreeFunction("ScriptingGraphicsCaps::SupportsTessellationShaders")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsTessellationShaders();

		[FreeFunction("ScriptingGraphicsCaps::SupportsRenderTargetArrayIndexFromVertexShader")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsRenderTargetArrayIndexFromVertexShader();

		[FreeFunction("ScriptingGraphicsCaps::SupportsInstancing")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsInstancing();

		[FreeFunction("ScriptingGraphicsCaps::SupportsHardwareQuadTopology")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsHardwareQuadTopology();

		[FreeFunction("ScriptingGraphicsCaps::Supports32bitsIndexBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Supports32bitsIndexBuffer();

		[FreeFunction("ScriptingGraphicsCaps::SupportsSparseTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsSparseTextures();

		[FreeFunction("ScriptingGraphicsCaps::SupportedRenderTargetCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SupportedRenderTargetCount();

		[FreeFunction("ScriptingGraphicsCaps::SupportsSeparatedRenderTargetsBlend")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsSeparatedRenderTargetsBlend();

		[FreeFunction("ScriptingGraphicsCaps::SupportedRandomWriteTargetCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SupportedRandomWriteTargetCount();

		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsVertex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MaxComputeBufferInputsVertex();

		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsFragment")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MaxComputeBufferInputsFragment();

		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsGeometry")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MaxComputeBufferInputsGeometry();

		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsDomain")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MaxComputeBufferInputsDomain();

		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsHull")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MaxComputeBufferInputsHull();

		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsCompute")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MaxComputeBufferInputsCompute();

		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampledTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SupportsMultisampledTextures();

		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampled2DArrayTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMultisampled2DArrayTextures();

		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampleAutoResolve")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMultisampleAutoResolve();

		[FreeFunction("ScriptingGraphicsCaps::SupportsTextureWrapMirrorOnce")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SupportsTextureWrapMirrorOnce();

		[FreeFunction("ScriptingGraphicsCaps::UsesReversedZBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UsesReversedZBuffer();

		[FreeFunction("ScriptingGraphicsCaps::HasRenderTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasRenderTextureNative(RenderTextureFormat format);

		[FreeFunction("ScriptingGraphicsCaps::SupportsBlendingOnRenderTextureFormat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsBlendingOnRenderTextureFormatNative(RenderTextureFormat format);

		[FreeFunction("ScriptingGraphicsCaps::SupportsTextureFormat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsTextureFormatNative(TextureFormat format);

		[FreeFunction("ScriptingGraphicsCaps::SupportsVertexAttributeFormat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsVertexAttributeFormatNative(VertexAttributeFormat format, int dimension);

		[FreeFunction("ScriptingGraphicsCaps::GetNPOTSupport")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NPOTSupport GetNPOTSupport();

		[FreeFunction("ScriptingGraphicsCaps::GetMaxTextureSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxTextureSize();

		[FreeFunction("ScriptingGraphicsCaps::GetMaxCubemapSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxCubemapSize();

		[FreeFunction("ScriptingGraphicsCaps::GetMaxRenderTextureSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxRenderTextureSize();

		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxComputeWorkGroupSize();

		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSizeX")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxComputeWorkGroupSizeX();

		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSizeY")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxComputeWorkGroupSizeY();

		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSizeZ")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxComputeWorkGroupSizeZ();

		[FreeFunction("ScriptingGraphicsCaps::SupportsAsyncCompute")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsAsyncCompute();

		[FreeFunction("ScriptingGraphicsCaps::SupportsGpuRecorder")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsGpuRecorder();

		[FreeFunction("ScriptingGraphicsCaps::SupportsGPUFence")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsGPUFence();

		[FreeFunction("ScriptingGraphicsCaps::SupportsAsyncGPUReadback")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsAsyncGPUReadback();

		[FreeFunction("ScriptingGraphicsCaps::SupportsRayTracing")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsRayTracing();

		[FreeFunction("ScriptingGraphicsCaps::SupportsSetConstantBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsSetConstantBuffer();

		[FreeFunction("ScriptingGraphicsCaps::MinConstantBufferOffsetAlignment")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MinConstantBufferOffsetAlignment();

		[FreeFunction("ScriptingGraphicsCaps::HasMipMaxLevel")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasMipMaxLevel();

		[FreeFunction("ScriptingGraphicsCaps::SupportsMipStreaming")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMipStreaming();

		[FreeFunction("ScriptingGraphicsCaps::IsFormatSupported")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsFormatSupported(GraphicsFormat format, FormatUsage usage);

		[FreeFunction("ScriptingGraphicsCaps::GetCompatibleFormat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GraphicsFormat GetCompatibleFormat(GraphicsFormat format, FormatUsage usage);

		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsFormat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GraphicsFormat GetGraphicsFormat(DefaultFormat format);

		[FreeFunction("ScriptingGraphicsCaps::GetRenderTextureSupportedMSAASampleCount")]
		public static int GetRenderTextureSupportedMSAASampleCount(RenderTextureDescriptor desc)
		{
			return EditorSystemInfo.GetRenderTextureSupportedMSAASampleCount_Injected(ref desc);
		}

		[FreeFunction("ScriptingGraphicsCaps::UsesLoadStoreActions")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UsesLoadStoreActions();

		[FreeFunction("ScriptingGraphicsCaps::GetHDRDisplaySupportFlags")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern HDRDisplaySupportFlags GetHDRDisplaySupportFlags();

		[FreeFunction("ScriptingGraphicsCaps::SupportsConservativeRaster")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsConservativeRaster();

		[FreeFunction("ScriptingGraphicsCaps::SupportsMultiview")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMultiview();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRenderTextureSupportedMSAASampleCount_Injected(ref RenderTextureDescriptor desc);
	}
}
