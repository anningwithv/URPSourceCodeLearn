using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Interfaces/ITerrainManager.h"), NativeHeader("TerrainScriptingClasses.h"), NativeHeader("Modules/Terrain/Public/Terrain.h"), StaticAccessor("GetITerrainManager()", StaticAccessorType.Arrow), UsedByNativeCode]
	public sealed class Terrain : Behaviour
	{
		[Obsolete("Enum type MaterialType is not used any more.", false)]
		public enum MaterialType
		{
			BuiltInStandard,
			BuiltInLegacyDiffuse,
			BuiltInLegacySpecular,
			Custom
		}

		public extern TerrainData terrainData
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float treeDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float treeBillboardDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float treeCrossFadeLength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int treeMaximumFullLODCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float detailObjectDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float detailObjectDensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float heightmapPixelError
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int heightmapMaximumLOD
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float basemapDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("StaticLightmapIndexInt")]
		public extern int lightmapIndex
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("DynamicLightmapIndexInt")]
		public extern int realtimeLightmapIndex
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("StaticLightmapST")]
		public Vector4 lightmapScaleOffset
		{
			get
			{
				Vector4 result;
				this.get_lightmapScaleOffset_Injected(out result);
				return result;
			}
			set
			{
				this.set_lightmapScaleOffset_Injected(ref value);
			}
		}

		[NativeProperty("DynamicLightmapST")]
		public Vector4 realtimeLightmapScaleOffset
		{
			get
			{
				Vector4 result;
				this.get_realtimeLightmapScaleOffset_Injected(out result);
				return result;
			}
			set
			{
				this.set_realtimeLightmapScaleOffset_Injected(ref value);
			}
		}

		[NativeProperty("GarbageCollectCameraData")]
		public extern bool freeUnusedRenderingResources
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ShadowCastingMode shadowCastingMode
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

		public extern Material materialTemplate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool drawHeightmap
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool allowAutoConnect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int groupingID
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool drawInstanced
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern RenderTexture normalmapTexture
		{
			[NativeMethod("TryGetNormalMapTexture")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool drawTreesAndFoliage
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 patchBoundsMultiplier
		{
			get
			{
				Vector3 result;
				this.get_patchBoundsMultiplier_Injected(out result);
				return result;
			}
			set
			{
				this.set_patchBoundsMultiplier_Injected(ref value);
			}
		}

		public extern float treeLODBiasMultiplier
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool collectDetailPatches
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TerrainRenderFlags editorRenderFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool bakeLightProbesForTrees
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool deringLightProbesForTrees
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool preserveTreePrototypeLayers
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat heightmapFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static TextureFormat heightmapTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetTextureFormat(Terrain.heightmapFormat);
			}
		}

		public static RenderTextureFormat heightmapRenderTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(Terrain.heightmapFormat);
			}
		}

		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat normalmapFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static TextureFormat normalmapTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetTextureFormat(Terrain.normalmapFormat);
			}
		}

		public static RenderTextureFormat normalmapRenderTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(Terrain.normalmapFormat);
			}
		}

		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat holesFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static RenderTextureFormat holesRenderTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(Terrain.holesFormat);
			}
		}

		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat compressedHolesFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static TextureFormat compressedHolesTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetTextureFormat(Terrain.compressedHolesFormat);
			}
		}

		public static extern Terrain activeTerrain
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("ActiveTerrainsScriptingArray")]
		public static extern Terrain[] activeTerrains
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Terrain leftNeighbor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Terrain rightNeighbor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Terrain topNeighbor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Terrain bottomNeighbor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint renderingLayerMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("splatmapDistance is deprecated, please use basemapDistance instead. (UnityUpgradable) -> basemapDistance", true)]
		public float splatmapDistance
		{
			get
			{
				return this.basemapDistance;
			}
			set
			{
				this.basemapDistance = value;
			}
		}

		[Obsolete("castShadows is deprecated, please use shadowCastingMode instead.")]
		public bool castShadows
		{
			get
			{
				return this.shadowCastingMode > ShadowCastingMode.Off;
			}
			set
			{
				this.shadowCastingMode = (value ? ShadowCastingMode.TwoSided : ShadowCastingMode.Off);
			}
		}

		[Obsolete("Property materialType is not used any more. Set materialTemplate directly.", false)]
		public Terrain.MaterialType materialType
		{
			get
			{
				return Terrain.MaterialType.Custom;
			}
			set
			{
			}
		}

		[Obsolete("Property legacySpecular is not used any more. Set materialTemplate directly.", false)]
		public Color legacySpecular
		{
			get
			{
				return Color.gray;
			}
			set
			{
			}
		}

		[Obsolete("Property legacyShininess is not used any more. Set materialTemplate directly.", false)]
		public float legacyShininess
		{
			get
			{
				return 0.078125f;
			}
			set
			{
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetClosestReflectionProbes(List<ReflectionProbeBlendInfo> result);

		public float SampleHeight(Vector3 worldPosition)
		{
			return this.SampleHeight_Injected(ref worldPosition);
		}

		public void AddTreeInstance(TreeInstance instance)
		{
			this.AddTreeInstance_Injected(ref instance);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetNeighbors(Terrain left, Terrain top, Terrain right, Terrain bottom);

		public Vector3 GetPosition()
		{
			Vector3 result;
			this.GetPosition_Injected(out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Flush();

		internal void RemoveTrees(Vector2 position, float radius, int prototypeIndex)
		{
			this.RemoveTrees_Injected(ref position, radius, prototypeIndex);
		}

		[NativeMethod("CopySplatMaterialCustomProps")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetSplatMaterialPropertyBlock(MaterialPropertyBlock properties);

		public void GetSplatMaterialPropertyBlock(MaterialPropertyBlock dest)
		{
			bool flag = dest == null;
			if (flag)
			{
				throw new ArgumentNullException("dest");
			}
			this.Internal_GetSplatMaterialPropertyBlock(dest);
		}

		[NativeMethod("GetSplatMaterialCustomProps")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetSplatMaterialPropertyBlock(MaterialPropertyBlock dest);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetConnectivityDirty();

		[UsedByNativeCode]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject CreateTerrainGameObject(TerrainData assignTerrain);

		[Obsolete("Use TerrainData.SyncHeightmap to notify all Terrain instances using the TerrainData.", false)]
		public void ApplyDelayedHeightmapModification()
		{
			TerrainData expr_07 = this.terrainData;
			if (expr_07 != null)
			{
				expr_07.SyncHeightmap();
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_lightmapScaleOffset_Injected(out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_lightmapScaleOffset_Injected(ref Vector4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_realtimeLightmapScaleOffset_Injected(out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_realtimeLightmapScaleOffset_Injected(ref Vector4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_patchBoundsMultiplier_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_patchBoundsMultiplier_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float SampleHeight_Injected(ref Vector3 worldPosition);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddTreeInstance_Injected(ref TreeInstance instance);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RemoveTrees_Injected(ref Vector2 position, float radius, int prototypeIndex);
	}
}
