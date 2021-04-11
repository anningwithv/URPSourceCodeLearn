using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h"), NativeHeader("Runtime/Graphics/Renderer.h"), RequireComponent(typeof(Transform)), UsedByNativeCode]
	public class Renderer : Component
	{
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property lightmapTilingOffset has been deprecated. Use lightmapScaleOffset (UnityUpgradable) -> lightmapScaleOffset", true)]
		public Vector4 lightmapTilingOffset
		{
			get
			{
				return Vector4.zero;
			}
			set
			{
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use probeAnchor instead (UnityUpgradable) -> probeAnchor", true)]
		public Transform lightProbeAnchor
		{
			get
			{
				return this.probeAnchor;
			}
			set
			{
				this.probeAnchor = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use shadowCastingMode instead.", false)]
		public bool castShadows
		{
			get
			{
				return this.shadowCastingMode > ShadowCastingMode.Off;
			}
			set
			{
				this.shadowCastingMode = (value ? ShadowCastingMode.On : ShadowCastingMode.Off);
			}
		}

		[Obsolete("Use motionVectorGenerationMode instead.", false)]
		public bool motionVectors
		{
			get
			{
				return this.motionVectorGenerationMode == MotionVectorGenerationMode.Object;
			}
			set
			{
				this.motionVectorGenerationMode = (value ? MotionVectorGenerationMode.Object : MotionVectorGenerationMode.Camera);
			}
		}

		[Obsolete("Use lightProbeUsage instead.", false)]
		public bool useLightProbes
		{
			get
			{
				return this.lightProbeUsage > LightProbeUsage.Off;
			}
			set
			{
				this.lightProbeUsage = (value ? LightProbeUsage.BlendProbes : LightProbeUsage.Off);
			}
		}

		public Bounds bounds
		{
			[FreeFunction(Name = "RendererScripting::GetBounds", HasExplicitThis = true)]
			get
			{
				Bounds result;
				this.get_bounds_Injected(out result);
				return result;
			}
		}

		public extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isVisible
		{
			[NativeName("IsVisibleInScene")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ShadowCastingMode shadowCastingMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool receiveShadows
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool forceRenderingOff
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern MotionVectorGenerationMode motionVectorGenerationMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LightProbeUsage lightProbeUsage
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ReflectionProbeUsage reflectionProbeUsage
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern uint renderingLayerMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int rendererPriority
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern RayTracingMode rayTracingMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern string sortingLayerName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int sortingLayerID
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int sortingOrder
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal extern int sortingGroupID
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal extern int sortingGroupOrder
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("IsDynamicOccludee")]
		public extern bool allowOcclusionWhenDynamic
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("StaticBatchRoot")]
		internal extern Transform staticBatchRootTransform
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal extern int staticBatchIndex
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isPartOfStaticBatch
		{
			[NativeName("IsPartOfStaticBatch")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 result;
				this.get_worldToLocalMatrix_Injected(out result);
				return result;
			}
		}

		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 result;
				this.get_localToWorldMatrix_Injected(out result);
				return result;
			}
		}

		public extern GameObject lightProbeProxyVolumeOverride
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Transform probeAnchor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public int lightmapIndex
		{
			get
			{
				return this.GetLightmapIndex(LightmapType.StaticLightmap);
			}
			set
			{
				this.SetLightmapIndex(value, LightmapType.StaticLightmap);
			}
		}

		public int realtimeLightmapIndex
		{
			get
			{
				return this.GetLightmapIndex(LightmapType.DynamicLightmap);
			}
			set
			{
				this.SetLightmapIndex(value, LightmapType.DynamicLightmap);
			}
		}

		public Vector4 lightmapScaleOffset
		{
			get
			{
				return this.GetLightmapST(LightmapType.StaticLightmap);
			}
			set
			{
				this.SetStaticLightmapST(value);
			}
		}

		public Vector4 realtimeLightmapScaleOffset
		{
			get
			{
				return this.GetLightmapST(LightmapType.DynamicLightmap);
			}
			set
			{
				this.SetLightmapST(value, LightmapType.DynamicLightmap);
			}
		}

		public Material[] materials
		{
			get
			{
				bool flag = this.IsPersistent();
				Material[] result;
				if (flag)
				{
					Debug.LogError("Not allowed to access Renderer.materials on prefab object. Use Renderer.sharedMaterials instead", this);
					result = null;
				}
				else
				{
					result = this.GetMaterialArray();
				}
				return result;
			}
			set
			{
				this.SetMaterialArray(value);
			}
		}

		public Material material
		{
			get
			{
				bool flag = this.IsPersistent();
				Material result;
				if (flag)
				{
					Debug.LogError("Not allowed to access Renderer.material on prefab object. Use Renderer.sharedMaterial instead", this);
					result = null;
				}
				else
				{
					result = this.GetMaterial();
				}
				return result;
			}
			set
			{
				this.SetMaterial(value);
			}
		}

		public Material sharedMaterial
		{
			get
			{
				return this.GetSharedMaterial();
			}
			set
			{
				this.SetMaterial(value);
			}
		}

		public Material[] sharedMaterials
		{
			get
			{
				return this.GetSharedMaterialArray();
			}
			set
			{
				this.SetMaterialArray(value);
			}
		}

		[FreeFunction(Name = "RendererScripting::SetStaticLightmapST", HasExplicitThis = true)]
		private void SetStaticLightmapST(Vector4 st)
		{
			this.SetStaticLightmapST_Injected(ref st);
		}

		[FreeFunction(Name = "RendererScripting::GetMaterial", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Material GetMaterial();

		[FreeFunction(Name = "RendererScripting::GetSharedMaterial", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Material GetSharedMaterial();

		[FreeFunction(Name = "RendererScripting::SetMaterial", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMaterial(Material m);

		[FreeFunction(Name = "RendererScripting::GetMaterialArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Material[] GetMaterialArray();

		[FreeFunction(Name = "RendererScripting::GetMaterialArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CopyMaterialArray([Out] Material[] m);

		[FreeFunction(Name = "RendererScripting::GetSharedMaterialArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CopySharedMaterialArray([Out] Material[] m);

		[FreeFunction(Name = "RendererScripting::SetMaterialArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMaterialArray([NotNull("ArgumentNullException")] Material[] m);

		[FreeFunction(Name = "RendererScripting::SetPropertyBlock", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_SetPropertyBlock(MaterialPropertyBlock properties);

		[FreeFunction(Name = "RendererScripting::GetPropertyBlock", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_GetPropertyBlock([NotNull("ArgumentNullException")] MaterialPropertyBlock dest);

		[FreeFunction(Name = "RendererScripting::SetPropertyBlockMaterialIndex", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_SetPropertyBlockMaterialIndex(MaterialPropertyBlock properties, int materialIndex);

		[FreeFunction(Name = "RendererScripting::GetPropertyBlockMaterialIndex", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_GetPropertyBlockMaterialIndex([NotNull("ArgumentNullException")] MaterialPropertyBlock dest, int materialIndex);

		[FreeFunction(Name = "RendererScripting::HasPropertyBlock", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasPropertyBlock();

		public void SetPropertyBlock(MaterialPropertyBlock properties)
		{
			this.Internal_SetPropertyBlock(properties);
		}

		public void SetPropertyBlock(MaterialPropertyBlock properties, int materialIndex)
		{
			this.Internal_SetPropertyBlockMaterialIndex(properties, materialIndex);
		}

		public void GetPropertyBlock(MaterialPropertyBlock properties)
		{
			this.Internal_GetPropertyBlock(properties);
		}

		public void GetPropertyBlock(MaterialPropertyBlock properties, int materialIndex)
		{
			this.Internal_GetPropertyBlockMaterialIndex(properties, materialIndex);
		}

		[FreeFunction(Name = "RendererScripting::GetClosestReflectionProbes", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetClosestReflectionProbesInternal(object result);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetStaticBatchInfo(int firstSubMesh, int subMeshCount);

		[NativeName("GetLightmapIndexInt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetLightmapIndex(LightmapType lt);

		[NativeName("SetLightmapIndexInt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLightmapIndex(int index, LightmapType lt);

		[NativeName("GetLightmapST")]
		private Vector4 GetLightmapST(LightmapType lt)
		{
			Vector4 result;
			this.GetLightmapST_Injected(lt, out result);
			return result;
		}

		[NativeName("SetLightmapST")]
		private void SetLightmapST(Vector4 st, LightmapType lt)
		{
			this.SetLightmapST_Injected(ref st, lt);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetMaterialCount();

		[NativeName("GetMaterialArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Material[] GetSharedMaterialArray();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsPersistent();

		public void GetMaterials(List<Material> m)
		{
			bool flag = m == null;
			if (flag)
			{
				throw new ArgumentNullException("The result material list cannot be null.", "m");
			}
			bool flag2 = this.IsPersistent();
			if (flag2)
			{
				throw new InvalidOperationException("Not allowed to access Renderer.materials on prefab object. Use Renderer.sharedMaterials instead");
			}
			NoAllocHelpers.EnsureListElemCount<Material>(m, this.GetMaterialCount());
			this.CopyMaterialArray(NoAllocHelpers.ExtractArrayFromListT<Material>(m));
		}

		public void GetSharedMaterials(List<Material> m)
		{
			bool flag = m == null;
			if (flag)
			{
				throw new ArgumentNullException("The result material list cannot be null.", "m");
			}
			NoAllocHelpers.EnsureListElemCount<Material>(m, this.GetMaterialCount());
			this.CopySharedMaterialArray(NoAllocHelpers.ExtractArrayFromListT<Material>(m));
		}

		public void GetClosestReflectionProbes(List<ReflectionProbeBlendInfo> result)
		{
			this.GetClosestReflectionProbesInternal(result);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetStaticLightmapST_Injected(ref Vector4 st);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_worldToLocalMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localToWorldMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLightmapST_Injected(LightmapType lt, out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLightmapST_Injected(ref Vector4 st, LightmapType lt);
	}
}
