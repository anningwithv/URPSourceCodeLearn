using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace UnityEngine
{
	[NativeHeader("Runtime/Misc/PlayerSettings.h"), NativeHeader("Runtime/Graphics/QualitySettings.h"), StaticAccessor("GetQualitySettings()", StaticAccessorType.Dot)]
	public sealed class QualitySettings : Object
	{
		[Obsolete("Use GetQualityLevel and SetQualityLevel", false)]
		public static QualityLevel currentLevel
		{
			get
			{
				return (QualityLevel)QualitySettings.GetQualityLevel();
			}
			set
			{
				QualitySettings.SetQualityLevel((int)value, true);
			}
		}

		public static extern int pixelLightCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("ShadowQuality")]
		public static extern ShadowQuality shadows
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern ShadowProjection shadowProjection
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int shadowCascades
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float shadowDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("ShadowResolution")]
		public static extern ShadowResolution shadowResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("ShadowmaskMode")]
		public static extern ShadowmaskMode shadowmaskMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float shadowNearPlaneOffset
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float shadowCascade2Split
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static Vector3 shadowCascade4Split
		{
			get
			{
				Vector3 result;
				QualitySettings.get_shadowCascade4Split_Injected(out result);
				return result;
			}
			set
			{
				QualitySettings.set_shadowCascade4Split_Injected(ref value);
			}
		}

		[NativeProperty("LODBias")]
		public static extern float lodBias
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("AnisotropicTextures")]
		public static extern AnisotropicFiltering anisotropicFiltering
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int masterTextureLimit
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int maximumLODLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int particleRaycastBudget
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool softParticles
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool softVegetation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int vSyncCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int antiAliasing
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int asyncUploadTimeSlice
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int asyncUploadBufferSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool asyncUploadPersistentBuffer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool realtimeReflectionProbes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool billboardsFaceCameraPosition
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float resolutionScalingFixedDPIFactor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("RenderPipeline")]
		private static extern ScriptableObject INTERNAL_renderPipeline
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static RenderPipelineAsset renderPipeline
		{
			get
			{
				return QualitySettings.INTERNAL_renderPipeline as RenderPipelineAsset;
			}
			set
			{
				QualitySettings.INTERNAL_renderPipeline = value;
			}
		}

		[Obsolete("blendWeights is obsolete. Use skinWeights instead (UnityUpgradable) -> skinWeights", true)]
		public static extern BlendWeights blendWeights
		{
			[NativeName("GetSkinWeights")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetSkinWeights")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern SkinWeights skinWeights
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool streamingMipmapsActive
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float streamingMipmapsMemoryBudget
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int streamingMipmapsRenderersPerFrame
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int streamingMipmapsMaxLevelReduction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool streamingMipmapsAddAllCameras
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int streamingMipmapsMaxFileIORequests
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("QualitySettingsScripting", StaticAccessorType.DoubleColon)]
		public static extern int maxQueuedFrames
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("QualitySettingsNames")]
		public static extern string[] names
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ColorSpace desiredColorSpace
		{
			[NativeName("GetColorSpace"), StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ColorSpace activeColorSpace
		{
			[NativeName("GetColorSpace"), StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static void IncreaseLevel([DefaultValue("false")] bool applyExpensiveChanges)
		{
			QualitySettings.SetQualityLevel(QualitySettings.GetQualityLevel() + 1, applyExpensiveChanges);
		}

		public static void DecreaseLevel([DefaultValue("false")] bool applyExpensiveChanges)
		{
			QualitySettings.SetQualityLevel(QualitySettings.GetQualityLevel() - 1, applyExpensiveChanges);
		}

		public static void SetQualityLevel(int index)
		{
			QualitySettings.SetQualityLevel(index, true);
		}

		public static void IncreaseLevel()
		{
			QualitySettings.IncreaseLevel(false);
		}

		public static void DecreaseLevel()
		{
			QualitySettings.DecreaseLevel(false);
		}

		private QualitySettings()
		{
		}

		[NativeName("GetRenderPipelineAssetAt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ScriptableObject InternalGetRenderPipelineAssetAt(int index);

		public static RenderPipelineAsset GetRenderPipelineAssetAt(int index)
		{
			bool flag = index < 0 || index >= QualitySettings.names.Length;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("{0} is out of range [0..{1}[", "index", QualitySettings.names.Length));
			}
			return QualitySettings.InternalGetRenderPipelineAssetAt(index) as RenderPipelineAsset;
		}

		[NativeName("GetCurrentIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetQualityLevel();

		[NativeName("SetCurrentIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetQualityLevel(int index, [DefaultValue("true")] bool applyExpensiveChanges);

		[NativeName("IsTextureResReducedOnAnyPlatform")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsTextureResReducedOnAnyPlatform();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_shadowCascade4Split_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowCascade4Split_Injected(ref Vector3 value);
	}
}
