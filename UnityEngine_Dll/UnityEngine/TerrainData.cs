using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.TerrainAPI;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("TerrainScriptingClasses.h"), NativeHeader("Modules/Terrain/Public/TerrainDataScriptingInterface.h"), UsedByNativeCode]
	public sealed class TerrainData : Object
	{
		private enum BoundaryValueType
		{
			MaxHeightmapRes,
			MinDetailResPerPatch,
			MaxDetailResPerPatch,
			MaxDetailPatchCount,
			MaxDetailsPerRes,
			MinAlphamapRes,
			MaxAlphamapRes,
			MinBaseMapRes,
			MaxBaseMapRes
		}

		private const string k_ScriptingInterfaceName = "TerrainDataScriptingInterface";

		private const string k_ScriptingInterfacePrefix = "TerrainDataScriptingInterface::";

		private const string k_HeightmapPrefix = "GetHeightmap().";

		private const string k_DetailDatabasePrefix = "GetDetailDatabase().";

		private const string k_TreeDatabasePrefix = "GetTreeDatabase().";

		private const string k_SplatDatabasePrefix = "GetSplatDatabase().";

		internal static readonly int k_MaximumResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxHeightmapRes);

		internal static readonly int k_MinimumDetailResolutionPerPatch = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MinDetailResPerPatch);

		internal static readonly int k_MaximumDetailResolutionPerPatch = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxDetailResPerPatch);

		internal static readonly int k_MaximumDetailPatchCount = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxDetailPatchCount);

		internal static readonly int k_MaximumDetailsPerRes = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxDetailsPerRes);

		internal static readonly int k_MinimumAlphamapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MinAlphamapRes);

		internal static readonly int k_MaximumAlphamapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxAlphamapRes);

		internal static readonly int k_MinimumBaseMapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MinBaseMapRes);

		internal static readonly int k_MaximumBaseMapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxBaseMapRes);

		[Obsolete("Please use heightmapResolution instead. (UnityUpgradable) -> heightmapResolution", false)]
		public int heightmapWidth
		{
			get
			{
				return this.heightmapResolution;
			}
		}

		[Obsolete("Please use heightmapResolution instead. (UnityUpgradable) -> heightmapResolution", false)]
		public int heightmapHeight
		{
			get
			{
				return this.heightmapResolution;
			}
		}

		public extern RenderTexture heightmapTexture
		{
			[NativeName("GetHeightmap().GetHeightmapTexture")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public int heightmapResolution
		{
			get
			{
				return this.internalHeightmapResolution;
			}
			set
			{
				int internalHeightmapResolution = value;
				bool flag = value < 0 || value > TerrainData.k_MaximumResolution;
				if (flag)
				{
					Debug.LogWarning("heightmapResolution is clamped to the range of [0, " + TerrainData.k_MaximumResolution.ToString() + "].");
					internalHeightmapResolution = Math.Min(TerrainData.k_MaximumResolution, Math.Max(value, 0));
				}
				this.internalHeightmapResolution = internalHeightmapResolution;
			}
		}

		private extern int internalHeightmapResolution
		{
			[NativeName("GetHeightmap().GetResolution")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("GetHeightmap().SetResolution")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 heightmapScale
		{
			[NativeName("GetHeightmap().GetScale")]
			get
			{
				Vector3 result;
				this.get_heightmapScale_Injected(out result);
				return result;
			}
		}

		public Texture holesTexture
		{
			get
			{
				bool flag = this.IsHolesTextureCompressed();
				Texture result;
				if (flag)
				{
					result = this.GetCompressedHolesTexture();
				}
				else
				{
					result = this.GetHolesTexture();
				}
				return result;
			}
		}

		public extern bool enableHolesTextureCompression
		{
			[NativeName("GetHeightmap().GetEnableHolesTextureCompression")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("GetHeightmap().SetEnableHolesTextureCompression")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal RenderTexture holesRenderTexture
		{
			get
			{
				return this.GetHolesTexture();
			}
		}

		public int holesResolution
		{
			get
			{
				return this.heightmapResolution - 1;
			}
		}

		public Vector3 size
		{
			[NativeName("GetHeightmap().GetSize")]
			get
			{
				Vector3 result;
				this.get_size_Injected(out result);
				return result;
			}
			[NativeName("GetHeightmap().SetSize")]
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		public Bounds bounds
		{
			[NativeName("GetHeightmap().CalculateBounds")]
			get
			{
				Bounds result;
				this.get_bounds_Injected(out result);
				return result;
			}
		}

		[Obsolete("Terrain thickness is no longer required by the physics engine. Set appropriate continuous collision detection modes to fast moving bodies.")]
		public float thickness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public extern float wavingGrassStrength
		{
			[NativeName("GetDetailDatabase().GetWavingGrassStrength")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassStrength", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float wavingGrassAmount
		{
			[NativeName("GetDetailDatabase().GetWavingGrassAmount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassAmount", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float wavingGrassSpeed
		{
			[NativeName("GetDetailDatabase().GetWavingGrassSpeed")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassSpeed", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Color wavingGrassTint
		{
			[NativeName("GetDetailDatabase().GetWavingGrassTint")]
			get
			{
				Color result;
				this.get_wavingGrassTint_Injected(out result);
				return result;
			}
			[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassTint", HasExplicitThis = true)]
			set
			{
				this.set_wavingGrassTint_Injected(ref value);
			}
		}

		public extern int detailWidth
		{
			[NativeName("GetDetailDatabase().GetWidth")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int detailHeight
		{
			[NativeName("GetDetailDatabase().GetHeight")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static int maxDetailsPerRes
		{
			get
			{
				return TerrainData.k_MaximumDetailsPerRes;
			}
		}

		public extern int detailPatchCount
		{
			[NativeName("GetDetailDatabase().GetPatchCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int detailResolution
		{
			[NativeName("GetDetailDatabase().GetResolution")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int detailResolutionPerPatch
		{
			[NativeName("GetDetailDatabase().GetResolutionPerPatch")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern DetailPrototype[] detailPrototypes
		{
			[FreeFunction("TerrainDataScriptingInterface::GetDetailPrototypes", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("TerrainDataScriptingInterface::SetDetailPrototypes", HasExplicitThis = true, ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public TreeInstance[] treeInstances
		{
			get
			{
				return this.Internal_GetTreeInstances();
			}
			set
			{
				this.SetTreeInstances(value, false);
			}
		}

		public extern int treeInstanceCount
		{
			[NativeName("GetTreeDatabase().GetInstances().size")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern TreePrototype[] treePrototypes
		{
			[FreeFunction("TerrainDataScriptingInterface::GetTreePrototypes", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("TerrainDataScriptingInterface::SetTreePrototypes", HasExplicitThis = true, ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int alphamapLayers
		{
			[NativeName("GetSplatDatabase().GetSplatCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public int alphamapResolution
		{
			get
			{
				return this.Internal_alphamapResolution;
			}
			set
			{
				int internal_alphamapResolution = value;
				bool flag = value < TerrainData.k_MinimumAlphamapResolution || value > TerrainData.k_MaximumAlphamapResolution;
				if (flag)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"alphamapResolution is clamped to the range of [",
						TerrainData.k_MinimumAlphamapResolution.ToString(),
						", ",
						TerrainData.k_MaximumAlphamapResolution.ToString(),
						"]."
					}));
					internal_alphamapResolution = Math.Min(TerrainData.k_MaximumAlphamapResolution, Math.Max(value, TerrainData.k_MinimumAlphamapResolution));
				}
				this.Internal_alphamapResolution = internal_alphamapResolution;
			}
		}

		private extern int Internal_alphamapResolution
		{
			[NativeName("GetSplatDatabase().GetAlphamapResolution")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("GetSplatDatabase().SetAlphamapResolution")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public int alphamapWidth
		{
			get
			{
				return this.alphamapResolution;
			}
		}

		public int alphamapHeight
		{
			get
			{
				return this.alphamapResolution;
			}
		}

		public int baseMapResolution
		{
			get
			{
				return this.Internal_baseMapResolution;
			}
			set
			{
				int internal_baseMapResolution = value;
				bool flag = value < TerrainData.k_MinimumBaseMapResolution || value > TerrainData.k_MaximumBaseMapResolution;
				if (flag)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"baseMapResolution is clamped to the range of [",
						TerrainData.k_MinimumBaseMapResolution.ToString(),
						", ",
						TerrainData.k_MaximumBaseMapResolution.ToString(),
						"]."
					}));
					internal_baseMapResolution = Math.Min(TerrainData.k_MaximumBaseMapResolution, Math.Max(value, TerrainData.k_MinimumBaseMapResolution));
				}
				this.Internal_baseMapResolution = internal_baseMapResolution;
			}
		}

		private extern int Internal_baseMapResolution
		{
			[NativeName("GetSplatDatabase().GetBaseMapResolution")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("GetSplatDatabase().SetBaseMapResolution")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int alphamapTextureCount
		{
			[NativeName("GetSplatDatabase().GetAlphaTextureCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Texture2D[] alphamapTextures
		{
			get
			{
				Texture2D[] array = new Texture2D[this.alphamapTextureCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.GetAlphamapTexture(i);
				}
				return array;
			}
		}

		[Obsolete("Please use the terrainLayers API instead.", false)]
		public extern SplatPrototype[] splatPrototypes
		{
			[FreeFunction("TerrainDataScriptingInterface::GetSplatPrototypes", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("TerrainDataScriptingInterface::SetSplatPrototypes", HasExplicitThis = true, ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TerrainLayer[] terrainLayers
		{
			[FreeFunction("TerrainDataScriptingInterface::GetTerrainLayers", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("TerrainDataScriptingInterface::SetTerrainLayers", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal extern Terrain[] users
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		private static bool SupportsCopyTextureBetweenRTAndTexture
		{
			get
			{
				return (SystemInfo.copyTextureSupport & (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture)) == (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture);
			}
		}

		public static string AlphamapTextureName
		{
			get
			{
				return "alphamap";
			}
		}

		public static string HolesTextureName
		{
			get
			{
				return "holes";
			}
		}

		[StaticAccessor("TerrainDataScriptingInterface", StaticAccessorType.DoubleColon), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetBoundaryValue(TerrainData.BoundaryValueType type);

		public TerrainData()
		{
			TerrainData.Internal_Create(this);
		}

		[FreeFunction("TerrainDataScriptingInterface::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] TerrainData terrainData);

		[Obsolete("Please use DirtyHeightmapRegion instead.", false)]
		public void UpdateDirtyRegion(int x, int y, int width, int height, bool syncHeightmapTextureImmediately)
		{
			this.DirtyHeightmapRegion(new RectInt(x, y, width, height), syncHeightmapTextureImmediately ? TerrainHeightmapSyncControl.HeightOnly : TerrainHeightmapSyncControl.None);
		}

		[NativeName("GetHeightmap().IsHolesTextureCompressed")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsHolesTextureCompressed();

		[NativeName("GetHeightmap().GetHolesTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern RenderTexture GetHolesTexture();

		[NativeName("GetHeightmap().GetCompressedHolesTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Texture2D GetCompressedHolesTexture();

		[NativeName("GetHeightmap().GetHeight")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetHeight(int x, int y);

		[NativeName("GetHeightmap().GetInterpolatedHeight")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetInterpolatedHeight(float x, float y);

		public float[,] GetInterpolatedHeights(float xBase, float yBase, int xCount, int yCount, float xInterval, float yInterval)
		{
			bool flag = xCount <= 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("xCount");
			}
			bool flag2 = yCount <= 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("yCount");
			}
			float[,] array = new float[yCount, xCount];
			this.Internal_GetInterpolatedHeights(array, xCount, 0, 0, xBase, yBase, xCount, yCount, xInterval, yInterval);
			return array;
		}

		public void GetInterpolatedHeights(float[,] results, int resultXOffset, int resultYOffset, float xBase, float yBase, int xCount, int yCount, float xInterval, float yInterval)
		{
			bool flag = results == null;
			if (flag)
			{
				throw new ArgumentNullException("results");
			}
			bool flag2 = xCount <= 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("xCount");
			}
			bool flag3 = yCount <= 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("yCount");
			}
			bool flag4 = resultXOffset < 0 || resultXOffset + xCount > results.GetLength(1);
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("resultXOffset");
			}
			bool flag5 = resultYOffset < 0 || resultYOffset + yCount > results.GetLength(0);
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("resultYOffset");
			}
			this.Internal_GetInterpolatedHeights(results, results.GetLength(1), resultXOffset, resultYOffset, xBase, yBase, xCount, yCount, xInterval, yInterval);
		}

		[FreeFunction("TerrainDataScriptingInterface::GetInterpolatedHeights", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetInterpolatedHeights(float[,] results, int resultXDimension, int resultXOffset, int resultYOffset, float xBase, float yBase, int xCount, int yCount, float xInterval, float yInterval);

		public float[,] GetHeights(int xBase, int yBase, int width, int height)
		{
			bool flag = xBase < 0 || yBase < 0 || xBase + width < 0 || yBase + height < 0 || xBase + width > this.heightmapResolution || yBase + height > this.heightmapResolution;
			if (flag)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain height information.");
			}
			return this.Internal_GetHeights(xBase, yBase, width, height);
		}

		[FreeFunction("TerrainDataScriptingInterface::GetHeights", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float[,] Internal_GetHeights(int xBase, int yBase, int width, int height);

		public void SetHeights(int xBase, int yBase, float[,] heights)
		{
			bool flag = heights == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			bool flag2 = xBase + heights.GetLength(1) > this.heightmapResolution || xBase + heights.GetLength(1) < 0 || yBase + heights.GetLength(0) < 0 || xBase < 0 || yBase < 0 || yBase + heights.GetLength(0) > this.heightmapResolution;
			if (flag2)
			{
				throw new ArgumentException(UnityString.Format("X or Y base out of bounds. Setting up to {0}x{1} while map size is {2}x{2}", new object[]
				{
					xBase + heights.GetLength(1),
					yBase + heights.GetLength(0),
					this.heightmapResolution
				}));
			}
			this.Internal_SetHeights(xBase, yBase, heights.GetLength(1), heights.GetLength(0), heights);
		}

		[FreeFunction("TerrainDataScriptingInterface::SetHeights", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHeights(int xBase, int yBase, int width, int height, float[,] heights);

		[FreeFunction("TerrainDataScriptingInterface::GetPatchMinMaxHeights", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern PatchExtents[] GetPatchMinMaxHeights();

		[FreeFunction("TerrainDataScriptingInterface::OverrideMinMaxPatchHeights", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void OverrideMinMaxPatchHeights(PatchExtents[] minMaxHeights);

		[FreeFunction("TerrainDataScriptingInterface::GetMaximumHeightError", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float[] GetMaximumHeightError();

		[FreeFunction("TerrainDataScriptingInterface::OverrideMaximumHeightError", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void OverrideMaximumHeightError(float[] maxError);

		public void SetHeightsDelayLOD(int xBase, int yBase, float[,] heights)
		{
			bool flag = heights == null;
			if (flag)
			{
				throw new ArgumentNullException("heights");
			}
			int length = heights.GetLength(0);
			int length2 = heights.GetLength(1);
			bool flag2 = xBase < 0 || xBase + length2 < 0 || xBase + length2 > this.heightmapResolution;
			if (flag2)
			{
				throw new ArgumentException(UnityString.Format("X out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					xBase,
					xBase + length2,
					this.heightmapResolution
				}));
			}
			bool flag3 = yBase < 0 || yBase + length < 0 || yBase + length > this.heightmapResolution;
			if (flag3)
			{
				throw new ArgumentException(UnityString.Format("Y out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					yBase,
					yBase + length,
					this.heightmapResolution
				}));
			}
			this.Internal_SetHeightsDelayLOD(xBase, yBase, length2, length, heights);
		}

		[FreeFunction("TerrainDataScriptingInterface::SetHeightsDelayLOD", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHeightsDelayLOD(int xBase, int yBase, int width, int height, float[,] heights);

		public bool IsHole(int x, int y)
		{
			bool flag = x < 0 || x >= this.holesResolution || y < 0 || y >= this.holesResolution;
			if (flag)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain holes information.");
			}
			return this.Internal_IsHole(x, y);
		}

		public bool[,] GetHoles(int xBase, int yBase, int width, int height)
		{
			bool flag = xBase < 0 || yBase < 0 || width <= 0 || height <= 0 || xBase + width > this.holesResolution || yBase + height > this.holesResolution;
			if (flag)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain holes information.");
			}
			return this.Internal_GetHoles(xBase, yBase, width, height);
		}

		public void SetHoles(int xBase, int yBase, bool[,] holes)
		{
			bool flag = holes == null;
			if (flag)
			{
				throw new ArgumentNullException("holes");
			}
			int length = holes.GetLength(0);
			int length2 = holes.GetLength(1);
			bool flag2 = xBase < 0 || xBase + length2 > this.holesResolution;
			if (flag2)
			{
				throw new ArgumentException(UnityString.Format("X out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					xBase,
					xBase + length2,
					this.holesResolution
				}));
			}
			bool flag3 = yBase < 0 || yBase + length > this.holesResolution;
			if (flag3)
			{
				throw new ArgumentException(UnityString.Format("Y out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					yBase,
					yBase + length,
					this.holesResolution
				}));
			}
			this.Internal_SetHoles(xBase, yBase, holes.GetLength(1), holes.GetLength(0), holes);
		}

		public void SetHolesDelayLOD(int xBase, int yBase, bool[,] holes)
		{
			bool flag = holes == null;
			if (flag)
			{
				throw new ArgumentNullException("holes");
			}
			int length = holes.GetLength(0);
			int length2 = holes.GetLength(1);
			bool flag2 = xBase < 0 || xBase + length2 > this.holesResolution;
			if (flag2)
			{
				throw new ArgumentException(UnityString.Format("X out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					xBase,
					xBase + length2,
					this.holesResolution
				}));
			}
			bool flag3 = yBase < 0 || yBase + length > this.holesResolution;
			if (flag3)
			{
				throw new ArgumentException(UnityString.Format("Y out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					yBase,
					yBase + length,
					this.holesResolution
				}));
			}
			this.Internal_SetHolesDelayLOD(xBase, yBase, length2, length, holes);
		}

		[FreeFunction("TerrainDataScriptingInterface::SetHoles", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHoles(int xBase, int yBase, int width, int height, bool[,] holes);

		[FreeFunction("TerrainDataScriptingInterface::GetHoles", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool[,] Internal_GetHoles(int xBase, int yBase, int width, int height);

		[FreeFunction("TerrainDataScriptingInterface::IsHole", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_IsHole(int x, int y);

		[FreeFunction("TerrainDataScriptingInterface::SetHolesDelayLOD", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHolesDelayLOD(int xBase, int yBase, int width, int height, bool[,] holes);

		[NativeName("GetHeightmap().GetSteepness")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetSteepness(float x, float y);

		[NativeName("GetHeightmap().GetInterpolatedNormal")]
		public Vector3 GetInterpolatedNormal(float x, float y)
		{
			Vector3 result;
			this.GetInterpolatedNormal_Injected(x, y, out result);
			return result;
		}

		[NativeName("GetHeightmap().GetAdjustedSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetAdjustedSize(int size);

		public void SetDetailResolution(int detailResolution, int resolutionPerPatch)
		{
			bool flag = detailResolution < 0;
			if (flag)
			{
				Debug.LogWarning("detailResolution must not be negative.");
				detailResolution = 0;
			}
			bool flag2 = resolutionPerPatch < TerrainData.k_MinimumDetailResolutionPerPatch || resolutionPerPatch > TerrainData.k_MaximumDetailResolutionPerPatch;
			if (flag2)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"resolutionPerPatch is clamped to the range of [",
					TerrainData.k_MinimumDetailResolutionPerPatch.ToString(),
					", ",
					TerrainData.k_MaximumDetailResolutionPerPatch.ToString(),
					"]."
				}));
				resolutionPerPatch = Math.Min(TerrainData.k_MaximumDetailResolutionPerPatch, Math.Max(resolutionPerPatch, TerrainData.k_MinimumDetailResolutionPerPatch));
			}
			int num = detailResolution / resolutionPerPatch;
			bool flag3 = num > TerrainData.k_MaximumDetailPatchCount;
			if (flag3)
			{
				Debug.LogWarning("Patch count (detailResolution / resolutionPerPatch) is clamped to the range of [0, " + TerrainData.k_MaximumDetailPatchCount.ToString() + "].");
				num = Math.Min(TerrainData.k_MaximumDetailPatchCount, Math.Max(num, 0));
			}
			this.Internal_SetDetailResolution(num, resolutionPerPatch);
		}

		[NativeName("GetDetailDatabase().SetDetailResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetDetailResolution(int patchCount, int resolutionPerPatch);

		[NativeName("GetDetailDatabase().ResetDirtyDetails")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void ResetDirtyDetails();

		[FreeFunction("TerrainDataScriptingInterface::RefreshPrototypes", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RefreshPrototypes();

		[FreeFunction("TerrainDataScriptingInterface::GetSupportedLayers", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[] GetSupportedLayers(int xBase, int yBase, int totalWidth, int totalHeight);

		[FreeFunction("TerrainDataScriptingInterface::GetDetailLayer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[,] GetDetailLayer(int xBase, int yBase, int width, int height, int layer);

		public void SetDetailLayer(int xBase, int yBase, int layer, int[,] details)
		{
			this.Internal_SetDetailLayer(xBase, yBase, details.GetLength(1), details.GetLength(0), layer, details);
		}

		[FreeFunction("TerrainDataScriptingInterface::SetDetailLayer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetDetailLayer(int xBase, int yBase, int totalWidth, int totalHeight, int detailIndex, int[,] data);

		[FreeFunction("TerrainDataScriptingInterface::GetClampedDetailPatches", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector2Int[] GetClampedDetailPatches(float density);

		[NativeName("GetTreeDatabase().GetInstances")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern TreeInstance[] Internal_GetTreeInstances();

		[FreeFunction("TerrainDataScriptingInterface::SetTreeInstances", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTreeInstances([NotNull("ArgumentNullException")] TreeInstance[] instances, bool snapToHeightmap);

		public TreeInstance GetTreeInstance(int index)
		{
			bool flag = index < 0 || index >= this.treeInstanceCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return this.Internal_GetTreeInstance(index);
		}

		[FreeFunction("TerrainDataScriptingInterface::GetTreeInstance", HasExplicitThis = true)]
		private TreeInstance Internal_GetTreeInstance(int index)
		{
			TreeInstance result;
			this.Internal_GetTreeInstance_Injected(index, out result);
			return result;
		}

		[FreeFunction("TerrainDataScriptingInterface::SetTreeInstance", HasExplicitThis = true), NativeThrows]
		public void SetTreeInstance(int index, TreeInstance instance)
		{
			this.SetTreeInstance_Injected(index, ref instance);
		}

		[NativeName("GetTreeDatabase().RemoveTreePrototype")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RemoveTreePrototype(int index);

		[NativeName("GetDetailDatabase().RemoveDetailPrototype")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RemoveDetailPrototype(int index);

		[NativeName("GetTreeDatabase().NeedUpgradeScaledPrototypes")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool NeedUpgradeScaledTreePrototypes();

		[FreeFunction("TerrainDataScriptingInterface::UpgradeScaledTreePrototype", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void UpgradeScaledTreePrototype();

		public float[,,] GetAlphamaps(int x, int y, int width, int height)
		{
			bool flag = x < 0 || y < 0 || width < 0 || height < 0;
			if (flag)
			{
				throw new ArgumentException("Invalid argument for GetAlphaMaps");
			}
			return this.Internal_GetAlphamaps(x, y, width, height);
		}

		[FreeFunction("TerrainDataScriptingInterface::GetAlphamaps", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float[,,] Internal_GetAlphamaps(int x, int y, int width, int height);

		[NativeName("GetSplatDatabase().GetAlphamapResolution"), RequiredByNativeCode]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern float GetAlphamapResolutionInternal();

		public void SetAlphamaps(int x, int y, float[,,] map)
		{
			bool flag = map.GetLength(2) != this.alphamapLayers;
			if (flag)
			{
				throw new Exception(UnityString.Format("Float array size wrong (layers should be {0})", new object[]
				{
					this.alphamapLayers
				}));
			}
			this.Internal_SetAlphamaps(x, y, map.GetLength(1), map.GetLength(0), map);
		}

		[FreeFunction("TerrainDataScriptingInterface::SetAlphamaps", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetAlphamaps(int x, int y, int width, int height, float[,,] map);

		[NativeName("GetSplatDatabase().SetBaseMapsDirty")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBaseMapDirty();

		[NativeName("GetSplatDatabase().GetAlphaTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture2D GetAlphamapTexture(int index);

		public void SetTerrainLayersRegisterUndo(TerrainLayer[] terrainLayers, string undoName)
		{
			bool flag = string.IsNullOrEmpty(undoName);
			if (flag)
			{
				throw new ArgumentNullException("undoName");
			}
			this.Internal_SetTerrainLayersRegisterUndo(terrainLayers, undoName);
		}

		[FreeFunction("TerrainDataScriptingInterface::SetTerrainLayersRegisterUndo", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetTerrainLayersRegisterUndo(TerrainLayer[] terrainLayers, string undoName);

		[NativeName("GetTreeDatabase().AddTree")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void AddTree(ref TreeInstance tree);

		[NativeName("GetTreeDatabase().RemoveTrees")]
		internal int RemoveTrees(Vector2 position, float radius, int prototypeIndex)
		{
			return this.RemoveTrees_Injected(ref position, radius, prototypeIndex);
		}

		[NativeName("GetHeightmap().CopyHeightmapFromActiveRenderTexture")]
		private void Internal_CopyActiveRenderTextureToHeightmap(RectInt rect, int destX, int destY, TerrainHeightmapSyncControl syncControl)
		{
			this.Internal_CopyActiveRenderTextureToHeightmap_Injected(ref rect, destX, destY, syncControl);
		}

		[NativeName("GetHeightmap().DirtyHeightmapRegion")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_DirtyHeightmapRegion(int x, int y, int width, int height, TerrainHeightmapSyncControl syncControl);

		[NativeName("GetHeightmap().SyncHeightmapGPUModifications")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SyncHeightmap();

		[NativeName("GetHeightmap().CopyHolesFromActiveRenderTexture")]
		private void Internal_CopyActiveRenderTextureToHoles(RectInt rect, int destX, int destY, bool allowDelayedCPUSync)
		{
			this.Internal_CopyActiveRenderTextureToHoles_Injected(ref rect, destX, destY, allowDelayedCPUSync);
		}

		[NativeName("GetHeightmap().DirtyHolesRegion")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_DirtyHolesRegion(int x, int y, int width, int height, bool allowDelayedCPUSync);

		[NativeName("GetHeightmap().SyncHolesGPUModifications")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SyncHoles();

		[NativeName("GetSplatDatabase().MarkDirtyRegion")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_MarkAlphamapDirtyRegion(int alphamapIndex, int x, int y, int width, int height);

		[NativeName("GetSplatDatabase().ClearDirtyRegion")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_ClearAlphamapDirtyRegion(int alphamapIndex);

		[NativeName("GetSplatDatabase().SyncGPUModifications")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SyncAlphamaps();

		public void CopyActiveRenderTextureToHeightmap(RectInt sourceRect, Vector2Int dest, TerrainHeightmapSyncControl syncControl)
		{
			RenderTexture active = RenderTexture.active;
			bool flag = active == null;
			if (flag)
			{
				throw new InvalidOperationException("Active RenderTexture is null.");
			}
			bool flag2 = sourceRect.x < 0 || sourceRect.y < 0 || sourceRect.xMax > active.width || sourceRect.yMax > active.height;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("sourceRect");
			}
			bool flag3 = dest.x < 0 || dest.x + sourceRect.width > this.heightmapResolution;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("dest.x");
			}
			bool flag4 = dest.y < 0 || dest.y + sourceRect.height > this.heightmapResolution;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("dest.y");
			}
			this.Internal_CopyActiveRenderTextureToHeightmap(sourceRect, dest.x, dest.y, syncControl);
			TerrainCallbacks.InvokeHeightmapChangedCallback(this, new RectInt(dest.x, dest.y, sourceRect.width, sourceRect.height), syncControl == TerrainHeightmapSyncControl.HeightAndLod);
		}

		public void DirtyHeightmapRegion(RectInt region, TerrainHeightmapSyncControl syncControl)
		{
			int heightmapResolution = this.heightmapResolution;
			bool flag = region.x < 0 || region.x >= heightmapResolution;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("region.x");
			}
			bool flag2 = region.width <= 0 || region.xMax > heightmapResolution;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("region.width");
			}
			bool flag3 = region.y < 0 || region.y >= heightmapResolution;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("region.y");
			}
			bool flag4 = region.height <= 0 || region.yMax > heightmapResolution;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("region.height");
			}
			this.Internal_DirtyHeightmapRegion(region.x, region.y, region.width, region.height, syncControl);
			TerrainCallbacks.InvokeHeightmapChangedCallback(this, region, syncControl == TerrainHeightmapSyncControl.HeightAndLod);
		}

		public void CopyActiveRenderTextureToTexture(string textureName, int textureIndex, RectInt sourceRect, Vector2Int dest, bool allowDelayedCPUSync)
		{
			bool flag = string.IsNullOrEmpty(textureName);
			if (flag)
			{
				throw new ArgumentNullException("textureName");
			}
			RenderTexture active = RenderTexture.active;
			bool flag2 = active == null;
			if (flag2)
			{
				throw new InvalidOperationException("Active RenderTexture is null.");
			}
			bool flag3 = textureName == TerrainData.HolesTextureName;
			int num2;
			int num;
			if (flag3)
			{
				bool flag4 = textureIndex != 0;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("textureIndex");
				}
				bool flag5 = active == this.holesTexture;
				if (flag5)
				{
					throw new ArgumentException("source", "Active RenderTexture cannot be holesTexture.");
				}
				num = (num2 = this.holesResolution);
			}
			else
			{
				bool flag6 = textureName == TerrainData.AlphamapTextureName;
				if (!flag6)
				{
					throw new ArgumentException("Unrecognized terrain texture name: \"" + textureName + "\"");
				}
				bool flag7 = textureIndex < 0 || textureIndex >= this.alphamapTextureCount;
				if (flag7)
				{
					throw new ArgumentOutOfRangeException("textureIndex");
				}
				num = (num2 = this.alphamapResolution);
			}
			bool flag8 = sourceRect.x < 0 || sourceRect.y < 0 || sourceRect.xMax > active.width || sourceRect.yMax > active.height;
			if (flag8)
			{
				throw new ArgumentOutOfRangeException("sourceRect");
			}
			bool flag9 = dest.x < 0 || dest.x + sourceRect.width > num2;
			if (flag9)
			{
				throw new ArgumentOutOfRangeException("dest.x");
			}
			bool flag10 = dest.y < 0 || dest.y + sourceRect.height > num;
			if (flag10)
			{
				throw new ArgumentOutOfRangeException("dest.y");
			}
			bool flag11 = textureName == TerrainData.HolesTextureName;
			if (flag11)
			{
				this.Internal_CopyActiveRenderTextureToHoles(sourceRect, dest.x, dest.y, allowDelayedCPUSync);
			}
			else
			{
				Texture2D alphamapTexture = this.GetAlphamapTexture(textureIndex);
				allowDelayedCPUSync = (allowDelayedCPUSync && TerrainData.SupportsCopyTextureBetweenRTAndTexture);
				bool flag12 = allowDelayedCPUSync;
				if (flag12)
				{
					bool flag13 = alphamapTexture.mipmapCount > 1;
					if (flag13)
					{
						RenderTexture temporary = RenderTexture.GetTemporary(new RenderTextureDescriptor(alphamapTexture.width, alphamapTexture.height, active.format)
						{
							sRGB = false,
							useMipMap = true,
							autoGenerateMips = false
						});
						bool flag14 = !temporary.IsCreated();
						if (flag14)
						{
							temporary.Create();
						}
						Graphics.CopyTexture(alphamapTexture, 0, 0, temporary, 0, 0);
						Graphics.CopyTexture(active, 0, 0, sourceRect.x, sourceRect.y, sourceRect.width, sourceRect.height, temporary, 0, 0, dest.x, dest.y);
						temporary.GenerateMips();
						Graphics.CopyTexture(temporary, alphamapTexture);
						RenderTexture.ReleaseTemporary(temporary);
					}
					else
					{
						Graphics.CopyTexture(active, 0, 0, sourceRect.x, sourceRect.y, sourceRect.width, sourceRect.height, alphamapTexture, 0, 0, dest.x, dest.y);
					}
					this.Internal_MarkAlphamapDirtyRegion(textureIndex, dest.x, dest.y, sourceRect.width, sourceRect.height);
				}
				else
				{
					bool flag15 = SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal || !SystemInfo.graphicsUVStartsAtTop;
					if (flag15)
					{
						alphamapTexture.ReadPixels(new Rect((float)sourceRect.x, (float)sourceRect.y, (float)sourceRect.width, (float)sourceRect.height), dest.x, dest.y);
					}
					else
					{
						alphamapTexture.ReadPixels(new Rect((float)sourceRect.x, (float)(active.height - sourceRect.yMax), (float)sourceRect.width, (float)sourceRect.height), dest.x, dest.y);
					}
					alphamapTexture.Apply(true);
					this.Internal_ClearAlphamapDirtyRegion(textureIndex);
				}
				TerrainCallbacks.InvokeTextureChangedCallback(this, textureName, new RectInt(dest.x, dest.y, sourceRect.width, sourceRect.height), !allowDelayedCPUSync);
			}
		}

		public void DirtyTextureRegion(string textureName, RectInt region, bool allowDelayedCPUSync)
		{
			bool flag = string.IsNullOrEmpty(textureName);
			if (flag)
			{
				throw new ArgumentNullException("textureName");
			}
			bool flag2 = textureName == TerrainData.AlphamapTextureName;
			int num;
			if (flag2)
			{
				num = this.alphamapResolution;
			}
			else
			{
				bool flag3 = textureName == TerrainData.HolesTextureName;
				if (!flag3)
				{
					throw new ArgumentException("Unrecognized terrain texture name: \"" + textureName + "\"");
				}
				num = this.holesResolution;
			}
			bool flag4 = region.x < 0 || region.x >= num;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("region.x");
			}
			bool flag5 = region.width <= 0 || region.xMax > num;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("region.width");
			}
			bool flag6 = region.y < 0 || region.y >= num;
			if (flag6)
			{
				throw new ArgumentOutOfRangeException("region.y");
			}
			bool flag7 = region.height <= 0 || region.yMax > num;
			if (flag7)
			{
				throw new ArgumentOutOfRangeException("region.height");
			}
			bool flag8 = textureName == TerrainData.HolesTextureName;
			if (flag8)
			{
				this.Internal_DirtyHolesRegion(region.x, region.y, region.width, region.height, allowDelayedCPUSync);
			}
			else
			{
				this.Internal_MarkAlphamapDirtyRegion(-1, region.x, region.y, region.width, region.height);
				bool flag9 = !allowDelayedCPUSync;
				if (flag9)
				{
					this.SyncTexture(textureName);
				}
				else
				{
					TerrainCallbacks.InvokeTextureChangedCallback(this, textureName, region, false);
				}
			}
		}

		public void SyncTexture(string textureName)
		{
			bool flag = string.IsNullOrEmpty(textureName);
			if (flag)
			{
				throw new ArgumentNullException("textureName");
			}
			bool flag2 = textureName == TerrainData.AlphamapTextureName;
			if (flag2)
			{
				this.Internal_SyncAlphamaps();
			}
			else
			{
				bool flag3 = textureName == TerrainData.HolesTextureName;
				if (!flag3)
				{
					throw new ArgumentException("Unrecognized terrain texture name: \"" + textureName + "\"");
				}
				bool flag4 = this.IsHolesTextureCompressed();
				if (flag4)
				{
					throw new InvalidOperationException("Holes texture is compressed. Compressed holes texture can not be read back from GPU. Use TerrainData.enableHolesTextureCompression to disable holes texture compression.");
				}
				this.Internal_SyncHoles();
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_heightmapScale_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_size_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_size_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetInterpolatedNormal_Injected(float x, float y, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_wavingGrassTint_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_wavingGrassTint_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetTreeInstance_Injected(int index, out TreeInstance ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTreeInstance_Injected(int index, ref TreeInstance instance);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int RemoveTrees_Injected(ref Vector2 position, float radius, int prototypeIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CopyActiveRenderTextureToHeightmap_Injected(ref RectInt rect, int destX, int destY, TerrainHeightmapSyncControl syncControl);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CopyActiveRenderTextureToHoles_Injected(ref RectInt rect, int destX, int destY, bool allowDelayedCPUSync);
	}
}
