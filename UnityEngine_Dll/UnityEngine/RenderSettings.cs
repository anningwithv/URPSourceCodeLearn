using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	[NativeHeader("Runtime/Camera/RenderSettings.h"), NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h"), NativeHeader("Runtime/Graphics/QualitySettingsTypes.h"), StaticAccessor("GetRenderSettings()", StaticAccessorType.Dot)]
	public sealed class RenderSettings : Object
	{
		[Obsolete("Use RenderSettings.ambientIntensity instead (UnityUpgradable) -> ambientIntensity", false)]
		public static float ambientSkyboxAmount
		{
			get
			{
				return RenderSettings.ambientIntensity;
			}
			set
			{
				RenderSettings.ambientIntensity = value;
			}
		}

		[NativeProperty("UseFog")]
		public static extern bool fog
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("LinearFogStart")]
		public static extern float fogStartDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("LinearFogEnd")]
		public static extern float fogEndDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern FogMode fogMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static Color fogColor
		{
			get
			{
				Color result;
				RenderSettings.get_fogColor_Injected(out result);
				return result;
			}
			set
			{
				RenderSettings.set_fogColor_Injected(ref value);
			}
		}

		public static extern float fogDensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern AmbientMode ambientMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static Color ambientSkyColor
		{
			get
			{
				Color result;
				RenderSettings.get_ambientSkyColor_Injected(out result);
				return result;
			}
			set
			{
				RenderSettings.set_ambientSkyColor_Injected(ref value);
			}
		}

		public static Color ambientEquatorColor
		{
			get
			{
				Color result;
				RenderSettings.get_ambientEquatorColor_Injected(out result);
				return result;
			}
			set
			{
				RenderSettings.set_ambientEquatorColor_Injected(ref value);
			}
		}

		public static Color ambientGroundColor
		{
			get
			{
				Color result;
				RenderSettings.get_ambientGroundColor_Injected(out result);
				return result;
			}
			set
			{
				RenderSettings.set_ambientGroundColor_Injected(ref value);
			}
		}

		public static extern float ambientIntensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("AmbientSkyColor")]
		public static Color ambientLight
		{
			get
			{
				Color result;
				RenderSettings.get_ambientLight_Injected(out result);
				return result;
			}
			set
			{
				RenderSettings.set_ambientLight_Injected(ref value);
			}
		}

		public static Color subtractiveShadowColor
		{
			get
			{
				Color result;
				RenderSettings.get_subtractiveShadowColor_Injected(out result);
				return result;
			}
			set
			{
				RenderSettings.set_subtractiveShadowColor_Injected(ref value);
			}
		}

		[NativeProperty("SkyboxMaterial")]
		public static extern Material skybox
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern Light sun
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static SphericalHarmonicsL2 ambientProbe
		{
			[NativeMethod("GetFinalAmbientProbe")]
			get
			{
				SphericalHarmonicsL2 result;
				RenderSettings.get_ambientProbe_Injected(out result);
				return result;
			}
			set
			{
				RenderSettings.set_ambientProbe_Injected(ref value);
			}
		}

		public static extern Cubemap customReflection
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float reflectionIntensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int reflectionBounces
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern DefaultReflectionMode defaultReflectionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int defaultReflectionResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float haloStrength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float flareStrength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float flareFadeSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		private RenderSettings()
		{
		}

		[FreeFunction("GetRenderSettings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object GetRenderSettings();

		[StaticAccessor("RenderSettingsScripting", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Reset();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_fogColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fogColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientSkyColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientSkyColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientEquatorColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientEquatorColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientGroundColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientGroundColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientLight_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientLight_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_subtractiveShadowColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_subtractiveShadowColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientProbe_Injected(out SphericalHarmonicsL2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientProbe_Injected(ref SphericalHarmonicsL2 value);
	}
}
