using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	[NativeHeader("Runtime/Camera/Light.h"), NativeHeader("Runtime/Export/Graphics/Light.bindings.h"), RequireComponent(typeof(Transform)), RequireComponent(typeof(Transform))]
	public sealed class Light : Behaviour
	{
		private int m_BakedIndex;

		[NativeProperty("LightType")]
		public extern LightType type
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("LightShape")]
		public extern LightShape shape
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float spotAngle
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float innerSpotAngle
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Color color
		{
			get
			{
				Color result;
				this.get_color_Injected(out result);
				return result;
			}
			set
			{
				this.set_color_Injected(ref value);
			}
		}

		public extern float colorTemperature
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool useColorTemperature
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float intensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float bounceIntensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool useBoundingSphereOverride
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector4 boundingSphereOverride
		{
			get
			{
				Vector4 result;
				this.get_boundingSphereOverride_Injected(out result);
				return result;
			}
			set
			{
				this.set_boundingSphereOverride_Injected(ref value);
			}
		}

		public extern bool useViewFrustumForShadowCasterCull
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int shadowCustomResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float shadowBias
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float shadowNormalBias
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float shadowNearPlane
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool useShadowMatrixOverride
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Matrix4x4 shadowMatrixOverride
		{
			get
			{
				Matrix4x4 result;
				this.get_shadowMatrixOverride_Injected(out result);
				return result;
			}
			set
			{
				this.set_shadowMatrixOverride_Injected(ref value);
			}
		}

		public extern float range
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Flare flare
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public LightBakingOutput bakingOutput
		{
			get
			{
				LightBakingOutput result;
				this.get_bakingOutput_Injected(out result);
				return result;
			}
			set
			{
				this.set_bakingOutput_Injected(ref value);
			}
		}

		public extern int cullingMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int renderingLayerMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LightShadowCasterMode lightShadowCasterMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float shadowRadius
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float shadowAngle
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LightShadows shadows
		{
			[NativeMethod("GetShadowType")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("Light_Bindings::SetShadowType", HasExplicitThis = true, ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float shadowStrength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("Light_Bindings::SetShadowStrength", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LightShadowResolution shadowResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("Light_Bindings::SetShadowResolution", HasExplicitThis = true, ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Shadow softness is removed in Unity 5.0+", true)]
		public float shadowSoftness
		{
			get
			{
				return 4f;
			}
			set
			{
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Shadow softness is removed in Unity 5.0+", true)]
		public float shadowSoftnessFade
		{
			get
			{
				return 1f;
			}
			set
			{
			}
		}

		public extern float[] layerShadowCullDistances
		{
			[FreeFunction("Light_Bindings::GetLayerShadowCullDistances", HasExplicitThis = true, ThrowsException = false)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("Light_Bindings::SetLayerShadowCullDistances", HasExplicitThis = true, ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float cookieSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Texture cookie
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LightRenderMode renderMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("Light_Bindings::SetRenderMode", HasExplicitThis = true, ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("warning bakedIndex has been removed please use bakingOutput.isBaked instead.", true)]
		public int bakedIndex
		{
			get
			{
				return this.m_BakedIndex;
			}
			set
			{
				this.m_BakedIndex = value;
			}
		}

		public Vector2 areaSize
		{
			get
			{
				Vector2 result;
				this.get_areaSize_Injected(out result);
				return result;
			}
			set
			{
				this.set_areaSize_Injected(ref value);
			}
		}

		public extern LightmapBakeType lightmapBakeType
		{
			[NativeMethod("GetBakeType")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetBakeType")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int commandBufferCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[Obsolete("Use QualitySettings.pixelLightCount instead.")]
		public static int pixelLightCount
		{
			get
			{
				return QualitySettings.pixelLightCount;
			}
			set
			{
				QualitySettings.pixelLightCount = value;
			}
		}

		[Obsolete("light.shadowConstantBias was removed, use light.shadowBias", true)]
		public float shadowConstantBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete("light.shadowObjectSizeBias was removed, use light.shadowBias", true)]
		public float shadowObjectSizeBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete("light.attenuate was removed; all lights always attenuate now", true)]
		public bool attenuate
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		[Obsolete("Light.lightmappingMode has been deprecated. Use Light.lightmapBakeType instead (UnityUpgradable) -> lightmapBakeType", true)]
		public LightmappingMode lightmappingMode
		{
			get
			{
				return LightmappingMode.Realtime;
			}
			set
			{
			}
		}

		[Obsolete("Light.isBaked is no longer supported. Use Light.bakingOutput.isBaked (and other members of Light.bakingOutput) instead.", false)]
		public bool isBaked
		{
			get
			{
				return this.bakingOutput.isBaked;
			}
		}

		[Obsolete("Light.alreadyLightmapped is no longer supported. Use Light.bakingOutput instead. Allowing to describe mixed light on top of realtime and baked ones.", false)]
		public bool alreadyLightmapped
		{
			get
			{
				return this.bakingOutput.isBaked;
			}
			set
			{
				LightBakingOutput bakingOutput = new LightBakingOutput
				{
					probeOcclusionLightIndex = -1,
					occlusionMaskChannel = -1,
					lightmapBakeType = (value ? LightmapBakeType.Baked : LightmapBakeType.Realtime),
					isBaked = value
				};
				this.bakingOutput = bakingOutput;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Reset();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLightDirty();

		public void AddCommandBuffer(LightEvent evt, CommandBuffer buffer)
		{
			this.AddCommandBuffer(evt, buffer, ShadowMapPass.All);
		}

		[FreeFunction("Light_Bindings::AddCommandBuffer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddCommandBuffer(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask);

		public void AddCommandBufferAsync(LightEvent evt, CommandBuffer buffer, ComputeQueueType queueType)
		{
			this.AddCommandBufferAsync(evt, buffer, ShadowMapPass.All, queueType);
		}

		[FreeFunction("Light_Bindings::AddCommandBufferAsync", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddCommandBufferAsync(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask, ComputeQueueType queueType);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveCommandBuffer(LightEvent evt, CommandBuffer buffer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveCommandBuffers(LightEvent evt);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveAllCommandBuffers();

		[FreeFunction("Light_Bindings::GetCommandBuffers", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern CommandBuffer[] GetCommandBuffers(LightEvent evt);

		[FreeFunction("Light_Bindings::GetLights")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Light[] GetLights(LightType type, int layer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_color_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_color_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_boundingSphereOverride_Injected(out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_boundingSphereOverride_Injected(ref Vector4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_shadowMatrixOverride_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_shadowMatrixOverride_Injected(ref Matrix4x4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bakingOutput_Injected(out LightBakingOutput ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_bakingOutput_Injected(ref LightBakingOutput value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_areaSize_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_areaSize_Injected(ref Vector2 value);
	}
}
