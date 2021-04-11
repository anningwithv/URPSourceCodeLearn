using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/LightingSettings.h"), PreventReadOnlyInstanceModification]
	public sealed class LightingSettings : Object
	{
		public enum Lightmapper
		{
			Enlighten,
			ProgressiveCPU,
			ProgressiveGPU
		}

		public enum Sampling
		{
			Auto,
			Fixed
		}

		public enum FilterMode
		{
			None,
			Auto,
			Advanced
		}

		public enum DenoiserType
		{
			None,
			Optix,
			OpenImage,
			RadeonPro
		}

		public enum FilterType
		{
			Gaussian,
			ATrous,
			None
		}

		[NativeName("EnableBakedLightmaps")]
		public extern bool bakedGI
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("EnableRealtimeLightmaps")]
		public extern bool realtimeGI
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("RealtimeEnvironmentLighting")]
		public extern bool realtimeEnvironmentLighting
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("AutoGenerate")]
		public extern bool autoGenerate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("MixedBakeMode")]
		public extern MixedLightingMode mixedBakeMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("AlbedoBoost")]
		public extern float albedoBoost
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("IndirectOutputScale")]
		public extern float indirectScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("BakeBackend")]
		public extern LightingSettings.Lightmapper lightmapper
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("LightmapMaxSize")]
		public extern int lightmapMaxSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("BakeResolution")]
		public extern float lightmapResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("Padding")]
		public extern int lightmapPadding
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("TextureCompression")]
		public extern bool compressLightmaps
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("AO")]
		public extern bool ao
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("AOMaxDistance")]
		public extern float aoMaxDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("CompAOExponent")]
		public extern float aoExponentIndirect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("CompAOExponentDirect")]
		public extern float aoExponentDirect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("ExtractAO")]
		public extern bool extractAO
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("LightmapsBakeMode")]
		public extern LightmapsMode directionalityMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("FilterMode")]
		internal extern UnityEngine.FilterMode lightmapFilterMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool exportTrainingData
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern string trainingDataDestination
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("RealtimeResolution")]
		public extern float indirectResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("ForceWhiteAlbedo")]
		internal extern bool realtimeForceWhiteAlbedo
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("ForceUpdates")]
		internal extern bool realtimeForceUpdates
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool finalGather
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float finalGatherRayCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool finalGatherFiltering
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRSampling")]
		public extern LightingSettings.Sampling sampling
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRDirectSampleCount")]
		public extern int directSampleCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRSampleCount")]
		public extern int indirectSampleCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Use LightingSettings.maxBounces instead. (UnityUpgradable) -> UnityEngine.LightingSettings.maxBounces", false), NativeName("PVRBounces")]
		public extern int bounces
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRBounces")]
		public extern int maxBounces
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Use LightingSettings.minBounces instead. (UnityUpgradable) -> UnityEngine.LightingSettings.minBounces", false), NativeName("PVRMinBounces")]
		public extern int russianRouletteStartBounce
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRMinBounces")]
		public extern int minBounces
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRCulling")]
		public extern bool prioritizeView
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRFilteringMode")]
		public extern LightingSettings.FilterMode filteringMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRDenoiserTypeDirect")]
		public extern LightingSettings.DenoiserType denoiserTypeDirect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRDenoiserTypeIndirect")]
		public extern LightingSettings.DenoiserType denoiserTypeIndirect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRDenoiserTypeAO")]
		public extern LightingSettings.DenoiserType denoiserTypeAO
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRFilterTypeDirect")]
		public extern LightingSettings.FilterType filterTypeDirect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRFilterTypeIndirect")]
		public extern LightingSettings.FilterType filterTypeIndirect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRFilterTypeAO")]
		public extern LightingSettings.FilterType filterTypeAO
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRFilteringGaussRadiusDirect")]
		public extern int filteringGaussRadiusDirect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRFilteringGaussRadiusIndirect")]
		public extern int filteringGaussRadiusIndirect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRFilteringGaussRadiusAO")]
		public extern int filteringGaussRadiusAO
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRFilteringAtrousPositionSigmaDirect")]
		public extern float filteringAtrousPositionSigmaDirect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRFilteringAtrousPositionSigmaIndirect")]
		public extern float filteringAtrousPositionSigmaIndirect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVRFilteringAtrousPositionSigmaAO")]
		public extern float filteringAtrousPositionSigmaAO
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVREnvironmentMIS")]
		internal extern int environmentMIS
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVREnvironmentSampleCount")]
		public extern int environmentSampleCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("PVREnvironmentReferencePointCount")]
		internal extern int environmentReferencePointCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("LightProbeSampleCountMultiplier")]
		public extern float lightProbeSampleCountMultiplier
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[RequiredByNativeCode]
		internal void LightingSettingsDontStripMe()
		{
		}

		public LightingSettings()
		{
			LightingSettings.Internal_Create(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] LightingSettings self);
	}
}
