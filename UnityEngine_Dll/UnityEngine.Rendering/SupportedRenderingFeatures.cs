using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	public class SupportedRenderingFeatures
	{
		[Flags]
		public enum ReflectionProbeModes
		{
			None = 0,
			Rotation = 1
		}

		[Flags]
		public enum LightmapMixedBakeModes
		{
			None = 0,
			IndirectOnly = 1,
			Subtractive = 2,
			Shadowmask = 4
		}

		private static SupportedRenderingFeatures s_Active = new SupportedRenderingFeatures();

		public static SupportedRenderingFeatures active
		{
			get
			{
				bool flag = SupportedRenderingFeatures.s_Active == null;
				if (flag)
				{
					SupportedRenderingFeatures.s_Active = new SupportedRenderingFeatures();
				}
				return SupportedRenderingFeatures.s_Active;
			}
			set
			{
				SupportedRenderingFeatures.s_Active = value;
			}
		}

		public SupportedRenderingFeatures.ReflectionProbeModes reflectionProbeModes
		{
			get;
			set;
		}

		public SupportedRenderingFeatures.LightmapMixedBakeModes defaultMixedLightingModes
		{
			get;
			set;
		}

		public SupportedRenderingFeatures.LightmapMixedBakeModes mixedLightingModes
		{
			get;
			set;
		}

		public LightmapBakeType lightmapBakeTypes
		{
			get;
			set;
		}

		public LightmapsMode lightmapsModes
		{
			get;
			set;
		}

		public bool enlighten
		{
			get;
			set;
		}

		public bool lightProbeProxyVolumes
		{
			get;
			set;
		}

		public bool motionVectors
		{
			get;
			set;
		}

		public bool receiveShadows
		{
			get;
			set;
		}

		public bool reflectionProbes
		{
			get;
			set;
		}

		public bool rendererPriority
		{
			get;
			set;
		}

		public bool terrainDetailUnsupported
		{
			get;
			set;
		}

		public bool rendersUIOverlay
		{
			get;
			set;
		}

		public bool overridesEnvironmentLighting
		{
			get;
			set;
		}

		public bool overridesFog
		{
			get;
			set;
		}

		public bool overridesRealtimeReflectionProbes
		{
			get;
			set;
		}

		public bool overridesOtherLightingSettings
		{
			get;
			set;
		}

		public bool editableMaterialRenderQueue
		{
			get;
			set;
		}

		public bool overridesLODBias
		{
			get;
			set;
		}

		public bool overridesMaximumLODLevel
		{
			get;
			set;
		}

		public bool rendererProbes
		{
			get;
			set;
		}

		public bool particleSystemInstancing
		{
			get;
			set;
		}

		public bool overridesShadowmask
		{
			get;
			set;
		}

		public string overrideShadowmaskMessage
		{
			get;
			set;
		}

		public string shadowmaskMessage
		{
			get
			{
				bool flag = !this.overridesShadowmask;
				string result;
				if (flag)
				{
					result = "The Shadowmask Mode used at run time can be set in the Quality Settings panel.";
				}
				else
				{
					result = this.overrideShadowmaskMessage;
				}
				return result;
			}
		}

		internal unsafe static MixedLightingMode FallbackMixedLightingMode()
		{
			MixedLightingMode result;
			SupportedRenderingFeatures.FallbackMixedLightingModeByRef(new IntPtr((void*)(&result)));
			return result;
		}

		[RequiredByNativeCode]
		internal unsafe static void FallbackMixedLightingModeByRef(IntPtr fallbackModePtr)
		{
			MixedLightingMode* ptr = (MixedLightingMode*)((void*)fallbackModePtr);
			bool flag = SupportedRenderingFeatures.active.defaultMixedLightingModes != SupportedRenderingFeatures.LightmapMixedBakeModes.None && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.active.defaultMixedLightingModes) == SupportedRenderingFeatures.active.defaultMixedLightingModes;
			if (flag)
			{
				SupportedRenderingFeatures.LightmapMixedBakeModes defaultMixedLightingModes = SupportedRenderingFeatures.active.defaultMixedLightingModes;
				SupportedRenderingFeatures.LightmapMixedBakeModes lightmapMixedBakeModes = defaultMixedLightingModes;
				if (lightmapMixedBakeModes != SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive)
				{
					if (lightmapMixedBakeModes != SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask)
					{
						*ptr = MixedLightingMode.IndirectOnly;
					}
					else
					{
						*ptr = MixedLightingMode.Shadowmask;
					}
				}
				else
				{
					*ptr = MixedLightingMode.Subtractive;
				}
			}
			else
			{
				bool flag2 = SupportedRenderingFeatures.IsMixedLightingModeSupported(MixedLightingMode.Shadowmask);
				if (flag2)
				{
					*ptr = MixedLightingMode.Shadowmask;
				}
				else
				{
					bool flag3 = SupportedRenderingFeatures.IsMixedLightingModeSupported(MixedLightingMode.Subtractive);
					if (flag3)
					{
						*ptr = MixedLightingMode.Subtractive;
					}
					else
					{
						*ptr = MixedLightingMode.IndirectOnly;
					}
				}
			}
		}

		internal unsafe static bool IsMixedLightingModeSupported(MixedLightingMode mixedMode)
		{
			bool result;
			SupportedRenderingFeatures.IsMixedLightingModeSupportedByRef(mixedMode, new IntPtr((void*)(&result)));
			return result;
		}

		[RequiredByNativeCode]
		internal unsafe static void IsMixedLightingModeSupportedByRef(MixedLightingMode mixedMode, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)((void*)isSupportedPtr);
			bool flag = !SupportedRenderingFeatures.IsLightmapBakeTypeSupported(LightmapBakeType.Mixed);
			if (flag)
			{
				*ptr = false;
			}
			else
			{
				*ptr = ((mixedMode == MixedLightingMode.IndirectOnly && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly) == SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly) || (mixedMode == MixedLightingMode.Subtractive && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive) == SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive) || (mixedMode == MixedLightingMode.Shadowmask && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask) == SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask));
			}
		}

		internal unsafe static bool IsLightmapBakeTypeSupported(LightmapBakeType bakeType)
		{
			bool result;
			SupportedRenderingFeatures.IsLightmapBakeTypeSupportedByRef(bakeType, new IntPtr((void*)(&result)));
			return result;
		}

		[RequiredByNativeCode]
		internal unsafe static void IsLightmapBakeTypeSupportedByRef(LightmapBakeType bakeType, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)((void*)isSupportedPtr);
			bool flag = bakeType == LightmapBakeType.Mixed;
			if (flag)
			{
				bool flag2 = SupportedRenderingFeatures.IsLightmapBakeTypeSupported(LightmapBakeType.Baked);
				bool flag3 = !flag2 || SupportedRenderingFeatures.active.mixedLightingModes == SupportedRenderingFeatures.LightmapMixedBakeModes.None;
				if (flag3)
				{
					*ptr = false;
					return;
				}
			}
			*ptr = ((SupportedRenderingFeatures.active.lightmapBakeTypes & bakeType) == bakeType);
			bool flag4 = bakeType == LightmapBakeType.Realtime && !SupportedRenderingFeatures.active.enlighten;
			if (flag4)
			{
				*ptr = false;
			}
		}

		internal unsafe static bool IsLightmapsModeSupported(LightmapsMode mode)
		{
			bool result;
			SupportedRenderingFeatures.IsLightmapsModeSupportedByRef(mode, new IntPtr((void*)(&result)));
			return result;
		}

		[RequiredByNativeCode]
		internal unsafe static void IsLightmapsModeSupportedByRef(LightmapsMode mode, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)((void*)isSupportedPtr);
			*ptr = ((SupportedRenderingFeatures.active.lightmapsModes & mode) == mode);
		}

		internal unsafe static bool IsLightmapperSupported(int lightmapper)
		{
			bool result;
			SupportedRenderingFeatures.IsLightmapperSupportedByRef(lightmapper, new IntPtr((void*)(&result)));
			return result;
		}

		[RequiredByNativeCode]
		internal unsafe static void IsLightmapperSupportedByRef(int lightmapper, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)((void*)isSupportedPtr);
			*ptr = ((lightmapper == 0 && !SupportedRenderingFeatures.active.enlighten) ? false : true);
		}

		[RequiredByNativeCode]
		internal unsafe static void IsUIOverlayRenderedBySRP(IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)((void*)isSupportedPtr);
			*ptr = SupportedRenderingFeatures.active.rendersUIOverlay;
		}

		internal unsafe static int FallbackLightmapper()
		{
			int result;
			SupportedRenderingFeatures.FallbackLightmapperByRef(new IntPtr((void*)(&result)));
			return result;
		}

		[RequiredByNativeCode]
		internal unsafe static void FallbackLightmapperByRef(IntPtr lightmapperPtr)
		{
			int* ptr = (int*)((void*)lightmapperPtr);
			*ptr = 1;
		}

		public SupportedRenderingFeatures()
		{
			this.<reflectionProbeModes>k__BackingField = SupportedRenderingFeatures.ReflectionProbeModes.None;
			this.<defaultMixedLightingModes>k__BackingField = SupportedRenderingFeatures.LightmapMixedBakeModes.None;
			this.<mixedLightingModes>k__BackingField = (SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly | SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive | SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask);
			this.<lightmapBakeTypes>k__BackingField = (LightmapBakeType.Realtime | LightmapBakeType.Baked | LightmapBakeType.Mixed);
			this.<lightmapsModes>k__BackingField = LightmapsMode.CombinedDirectional;
			this.<enlighten>k__BackingField = true;
			this.<lightProbeProxyVolumes>k__BackingField = true;
			this.<motionVectors>k__BackingField = true;
			this.<receiveShadows>k__BackingField = true;
			this.<reflectionProbes>k__BackingField = true;
			this.<rendererPriority>k__BackingField = false;
			this.<terrainDetailUnsupported>k__BackingField = false;
			this.<overridesEnvironmentLighting>k__BackingField = false;
			this.<overridesFog>k__BackingField = false;
			this.<overridesRealtimeReflectionProbes>k__BackingField = false;
			this.<overridesOtherLightingSettings>k__BackingField = false;
			this.<editableMaterialRenderQueue>k__BackingField = true;
			this.<overridesLODBias>k__BackingField = false;
			this.<overridesMaximumLODLevel>k__BackingField = false;
			this.<rendererProbes>k__BackingField = true;
			this.<particleSystemInstancing>k__BackingField = true;
			this.<overridesShadowmask>k__BackingField = false;
			this.<overrideShadowmaskMessage>k__BackingField = "";
			base..ctor();
		}
	}
}
