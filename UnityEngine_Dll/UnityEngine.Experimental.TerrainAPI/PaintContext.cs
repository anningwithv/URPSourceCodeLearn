using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Experimental.TerrainAPI
{
	public class PaintContext
	{
		public interface ITerrainInfo
		{
			Terrain terrain
			{
				get;
			}

			RectInt clippedTerrainPixels
			{
				get;
			}

			RectInt clippedPCPixels
			{
				get;
			}

			bool gatherEnable
			{
				get;
				set;
			}

			bool scatterEnable
			{
				get;
				set;
			}

			object userData
			{
				get;
				set;
			}
		}

		private class TerrainTile : PaintContext.ITerrainInfo
		{
			public Terrain terrain;

			public Vector2Int tileOriginPixels;

			public RectInt clippedTerrainPixels;

			public RectInt clippedPCPixels;

			public object userData;

			public bool gatherEnable;

			public bool scatterEnable;

			Terrain PaintContext.ITerrainInfo.terrain
			{
				get
				{
					return this.terrain;
				}
			}

			RectInt PaintContext.ITerrainInfo.clippedTerrainPixels
			{
				get
				{
					return this.clippedTerrainPixels;
				}
			}

			RectInt PaintContext.ITerrainInfo.clippedPCPixels
			{
				get
				{
					return this.clippedPCPixels;
				}
			}

			bool PaintContext.ITerrainInfo.gatherEnable
			{
				get
				{
					return this.gatherEnable;
				}
				set
				{
					this.gatherEnable = value;
				}
			}

			bool PaintContext.ITerrainInfo.scatterEnable
			{
				get
				{
					return this.scatterEnable;
				}
				set
				{
					this.scatterEnable = value;
				}
			}

			object PaintContext.ITerrainInfo.userData
			{
				get
				{
					return this.userData;
				}
				set
				{
					this.userData = value;
				}
			}

			public static PaintContext.TerrainTile Make(Terrain terrain, int tileOriginPixelsX, int tileOriginPixelsY, RectInt pixelRect, int targetTextureWidth, int targetTextureHeight)
			{
				PaintContext.TerrainTile terrainTile = new PaintContext.TerrainTile
				{
					terrain = terrain,
					gatherEnable = true,
					scatterEnable = true,
					tileOriginPixels = new Vector2Int(tileOriginPixelsX, tileOriginPixelsY),
					clippedTerrainPixels = new RectInt
					{
						x = Mathf.Max(0, pixelRect.x - tileOriginPixelsX),
						y = Mathf.Max(0, pixelRect.y - tileOriginPixelsY),
						xMax = Mathf.Min(targetTextureWidth, pixelRect.xMax - tileOriginPixelsX),
						yMax = Mathf.Min(targetTextureHeight, pixelRect.yMax - tileOriginPixelsY)
					}
				};
				terrainTile.clippedPCPixels = new RectInt(terrainTile.clippedTerrainPixels.x + terrainTile.tileOriginPixels.x - pixelRect.x, terrainTile.clippedTerrainPixels.y + terrainTile.tileOriginPixels.y - pixelRect.y, terrainTile.clippedTerrainPixels.width, terrainTile.clippedTerrainPixels.height);
				bool flag = terrainTile.clippedTerrainPixels.width == 0 || terrainTile.clippedTerrainPixels.height == 0;
				if (flag)
				{
					terrainTile.gatherEnable = false;
					terrainTile.scatterEnable = false;
					UnityEngine.Debug.LogError("PaintContext.ClipTerrainTiles found 0 content rect");
				}
				return terrainTile;
			}
		}

		private class SplatmapUserData
		{
			public TerrainLayer terrainLayer;

			public int terrainLayerIndex;

			public int mapIndex;

			public int channelIndex;
		}

		[Flags]
		internal enum ToolAction
		{
			None = 0,
			PaintHeightmap = 1,
			PaintTexture = 2,
			PaintHoles = 4,
			AddTerrainLayer = 8
		}

		private struct PaintedTerrain
		{
			public Terrain terrain;

			public PaintContext.ToolAction action;
		}

		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly PaintContext.<>c <>9 = new PaintContext.<>c();

			public static Func<PaintContext.ITerrainInfo, Texture> <>9__57_0;

			public static Func<PaintContext.ITerrainInfo, RenderTexture> <>9__58_0;

			public static Action<PaintContext.ITerrainInfo> <>9__58_2;

			public static Func<PaintContext.ITerrainInfo, Texture> <>9__59_0;

			public static Func<PaintContext.ITerrainInfo, Texture> <>9__61_0;

			internal Texture <GatherHeightmap>b__57_0(PaintContext.ITerrainInfo t)
			{
				return t.terrain.terrainData.heightmapTexture;
			}

			internal RenderTexture <ScatterHeightmap>b__58_0(PaintContext.ITerrainInfo t)
			{
				return t.terrain.terrainData.heightmapTexture;
			}

			internal void <ScatterHeightmap>b__58_2(PaintContext.ITerrainInfo t)
			{
				t.terrain.terrainData.DirtyHeightmapRegion(t.clippedTerrainPixels, t.terrain.drawInstanced ? TerrainHeightmapSyncControl.None : TerrainHeightmapSyncControl.HeightOnly);
				PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintHeightmap);
			}

			internal Texture <GatherHoles>b__59_0(PaintContext.ITerrainInfo t)
			{
				return t.terrain.terrainData.holesTexture;
			}

			internal Texture <GatherNormals>b__61_0(PaintContext.ITerrainInfo t)
			{
				return t.terrain.normalmapTexture;
			}
		}

		private List<PaintContext.TerrainTile> m_TerrainTiles;

		private float m_HeightWorldSpaceMin;

		private float m_HeightWorldSpaceMax;

		private static List<PaintContext.PaintedTerrain> s_PaintedTerrain = new List<PaintContext.PaintedTerrain>();

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		internal static event Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> onTerrainTileBeforePaint;

		public Terrain originTerrain
		{
			[CompilerGenerated]
			get
			{
				return this.<originTerrain>k__BackingField;
			}
		}

		public RectInt pixelRect
		{
			[CompilerGenerated]
			get
			{
				return this.<pixelRect>k__BackingField;
			}
		}

		public int targetTextureWidth
		{
			[CompilerGenerated]
			get
			{
				return this.<targetTextureWidth>k__BackingField;
			}
		}

		public int targetTextureHeight
		{
			[CompilerGenerated]
			get
			{
				return this.<targetTextureHeight>k__BackingField;
			}
		}

		public Vector2 pixelSize
		{
			[CompilerGenerated]
			get
			{
				return this.<pixelSize>k__BackingField;
			}
		}

		public RenderTexture sourceRenderTexture
		{
			get;
			private set;
		}

		public RenderTexture destinationRenderTexture
		{
			get;
			private set;
		}

		public RenderTexture oldRenderTexture
		{
			get;
			private set;
		}

		public int terrainCount
		{
			get
			{
				return this.m_TerrainTiles.Count;
			}
		}

		public float heightWorldSpaceMin
		{
			get
			{
				return this.m_HeightWorldSpaceMin;
			}
		}

		public float heightWorldSpaceSize
		{
			get
			{
				return this.m_HeightWorldSpaceMax - this.m_HeightWorldSpaceMin;
			}
		}

		public static float kNormalizedHeightScale
		{
			get
			{
				return 0.4999771f;
			}
		}

		public Terrain GetTerrain(int terrainIndex)
		{
			return this.m_TerrainTiles[terrainIndex].terrain;
		}

		public RectInt GetClippedPixelRectInTerrainPixels(int terrainIndex)
		{
			return this.m_TerrainTiles[terrainIndex].clippedTerrainPixels;
		}

		public RectInt GetClippedPixelRectInRenderTexturePixels(int terrainIndex)
		{
			return this.m_TerrainTiles[terrainIndex].clippedPCPixels;
		}

		public PaintContext(Terrain terrain, RectInt pixelRect, int targetTextureWidth, int targetTextureHeight, bool texelPadding = true)
		{
			this.<originTerrain>k__BackingField = terrain;
			this.<pixelRect>k__BackingField = pixelRect;
			this.<targetTextureWidth>k__BackingField = targetTextureWidth;
			this.<targetTextureHeight>k__BackingField = targetTextureHeight;
			TerrainData terrainData = terrain.terrainData;
			this.<pixelSize>k__BackingField = new Vector2(terrainData.size.x / ((float)targetTextureWidth - (texelPadding ? 1f : 0f)), terrainData.size.z / ((float)targetTextureHeight - (texelPadding ? 1f : 0f)));
			this.FindTerrainTilesUnlimited(texelPadding);
		}

		public static PaintContext CreateFromBounds(Terrain terrain, Rect boundsInTerrainSpace, int inputTextureWidth, int inputTextureHeight, int extraBorderPixels = 0, bool texelPadding = true)
		{
			return new PaintContext(terrain, TerrainPaintUtility.CalcPixelRectFromBounds(terrain, boundsInTerrainSpace, inputTextureWidth, inputTextureHeight, extraBorderPixels, texelPadding), inputTextureWidth, inputTextureHeight, texelPadding);
		}

		private void FindTerrainTilesUnlimited(bool texelPadding)
		{
			float minX = this.originTerrain.transform.position.x + this.pixelSize.x * (float)this.pixelRect.xMin;
			float minZ = this.originTerrain.transform.position.z + this.pixelSize.y * (float)this.pixelRect.yMin;
			float maxX = this.originTerrain.transform.position.x + this.pixelSize.x * (float)(this.pixelRect.xMax - 1);
			float maxZ = this.originTerrain.transform.position.z + this.pixelSize.y * (float)(this.pixelRect.yMax - 1);
			this.m_HeightWorldSpaceMin = this.originTerrain.GetPosition().y;
			this.m_HeightWorldSpaceMax = this.m_HeightWorldSpaceMin + this.originTerrain.terrainData.size.y;
			TerrainUtility.TerrainMap.TerrainFilter filter = delegate(Terrain t)
			{
				float x = t.transform.position.x;
				float z = t.transform.position.z;
				float num3 = t.transform.position.x + t.terrainData.size.x;
				float num4 = t.transform.position.z + t.terrainData.size.z;
				return x <= maxX && num3 >= minX && z <= maxZ && num4 >= minZ;
			};
			TerrainUtility.TerrainMap terrainMap = TerrainUtility.TerrainMap.CreateFromConnectedNeighbors(this.originTerrain, filter, false);
			this.m_TerrainTiles = new List<PaintContext.TerrainTile>();
			bool flag = terrainMap != null;
			if (flag)
			{
				foreach (KeyValuePair<TerrainUtility.TerrainMap.TileCoord, Terrain> current in terrainMap.m_terrainTiles)
				{
					TerrainUtility.TerrainMap.TileCoord key = current.Key;
					Terrain value = current.Value;
					int num = key.tileX * (this.targetTextureWidth - (texelPadding ? 1 : 0));
					int num2 = key.tileZ * (this.targetTextureHeight - (texelPadding ? 1 : 0));
					RectInt other = new RectInt(num, num2, this.targetTextureWidth, this.targetTextureHeight);
					bool flag2 = this.pixelRect.Overlaps(other);
					if (flag2)
					{
						this.m_TerrainTiles.Add(PaintContext.TerrainTile.Make(value, num, num2, this.pixelRect, this.targetTextureWidth, this.targetTextureHeight));
						this.m_HeightWorldSpaceMin = Mathf.Min(this.m_HeightWorldSpaceMin, value.GetPosition().y);
						this.m_HeightWorldSpaceMax = Mathf.Max(this.m_HeightWorldSpaceMax, value.GetPosition().y + value.terrainData.size.y);
					}
				}
			}
		}

		public void CreateRenderTargets(RenderTextureFormat colorFormat)
		{
			this.sourceRenderTexture = RenderTexture.GetTemporary(this.pixelRect.width, this.pixelRect.height, 0, colorFormat, RenderTextureReadWrite.Linear);
			this.destinationRenderTexture = RenderTexture.GetTemporary(this.pixelRect.width, this.pixelRect.height, 0, colorFormat, RenderTextureReadWrite.Linear);
			this.sourceRenderTexture.wrapMode = TextureWrapMode.Clamp;
			this.sourceRenderTexture.filterMode = FilterMode.Point;
			this.oldRenderTexture = RenderTexture.active;
		}

		public void Cleanup(bool restoreRenderTexture = true)
		{
			if (restoreRenderTexture)
			{
				RenderTexture.active = this.oldRenderTexture;
			}
			RenderTexture.ReleaseTemporary(this.sourceRenderTexture);
			RenderTexture.ReleaseTemporary(this.destinationRenderTexture);
			this.sourceRenderTexture = null;
			this.destinationRenderTexture = null;
			this.oldRenderTexture = null;
		}

		private void GatherInternal(Func<PaintContext.ITerrainInfo, Texture> terrainToTexture, Color defaultColor, string operationName, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			bool flag = blitMaterial == null;
			if (flag)
			{
				blitMaterial = TerrainPaintUtility.GetBlitMaterial();
			}
			RenderTexture.active = this.sourceRenderTexture;
			GL.Clear(false, true, defaultColor);
			GL.PushMatrix();
			GL.LoadPixelMatrix(0f, (float)this.pixelRect.width, 0f, (float)this.pixelRect.height);
			for (int i = 0; i < this.m_TerrainTiles.Count; i++)
			{
				PaintContext.TerrainTile terrainTile = this.m_TerrainTiles[i];
				bool flag2 = !terrainTile.gatherEnable;
				if (!flag2)
				{
					Texture texture = terrainToTexture(terrainTile);
					bool flag3 = texture == null || !terrainTile.gatherEnable;
					if (!flag3)
					{
						bool flag4 = texture.width != this.targetTextureWidth || texture.height != this.targetTextureHeight;
						if (flag4)
						{
							UnityEngine.Debug.LogWarning(operationName + " requires the same resolution texture for all Terrains - mismatched Terrains are ignored.", terrainTile.terrain);
						}
						else
						{
							if (beforeBlit != null)
							{
								beforeBlit(terrainTile);
							}
							bool flag5 = !terrainTile.gatherEnable;
							if (!flag5)
							{
								FilterMode filterMode = texture.filterMode;
								texture.filterMode = FilterMode.Point;
								blitMaterial.SetTexture("_MainTex", texture);
								blitMaterial.SetPass(blitPass);
								TerrainPaintUtility.DrawQuad(terrainTile.clippedPCPixels, terrainTile.clippedTerrainPixels, texture);
								texture.filterMode = filterMode;
								if (afterBlit != null)
								{
									afterBlit(terrainTile);
								}
							}
						}
					}
				}
			}
			GL.PopMatrix();
			RenderTexture.active = this.oldRenderTexture;
		}

		private void ScatterInternal(Func<PaintContext.ITerrainInfo, RenderTexture> terrainToRT, string operationName, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			RenderTexture active = RenderTexture.active;
			bool flag = blitMaterial == null;
			if (flag)
			{
				blitMaterial = TerrainPaintUtility.GetBlitMaterial();
			}
			for (int i = 0; i < this.m_TerrainTiles.Count; i++)
			{
				PaintContext.TerrainTile terrainTile = this.m_TerrainTiles[i];
				bool flag2 = !terrainTile.scatterEnable;
				if (!flag2)
				{
					RenderTexture renderTexture = terrainToRT(terrainTile);
					bool flag3 = renderTexture == null || !terrainTile.scatterEnable;
					if (!flag3)
					{
						bool flag4 = renderTexture.width != this.targetTextureWidth || renderTexture.height != this.targetTextureHeight;
						if (flag4)
						{
							UnityEngine.Debug.LogWarning(operationName + " requires the same resolution for all Terrains - mismatched Terrains are ignored.", terrainTile.terrain);
						}
						else
						{
							if (beforeBlit != null)
							{
								beforeBlit(terrainTile);
							}
							bool flag5 = !terrainTile.scatterEnable;
							if (!flag5)
							{
								RenderTexture.active = renderTexture;
								GL.PushMatrix();
								GL.LoadPixelMatrix(0f, (float)renderTexture.width, 0f, (float)renderTexture.height);
								FilterMode filterMode = this.destinationRenderTexture.filterMode;
								this.destinationRenderTexture.filterMode = FilterMode.Point;
								blitMaterial.SetTexture("_MainTex", this.destinationRenderTexture);
								blitMaterial.SetPass(blitPass);
								TerrainPaintUtility.DrawQuad(terrainTile.clippedTerrainPixels, terrainTile.clippedPCPixels, this.destinationRenderTexture);
								this.destinationRenderTexture.filterMode = filterMode;
								GL.PopMatrix();
								if (afterBlit != null)
								{
									afterBlit(terrainTile);
								}
							}
						}
					}
				}
			}
			RenderTexture.active = active;
		}

		public void Gather(Func<PaintContext.ITerrainInfo, Texture> terrainSource, Color defaultColor, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			bool flag = terrainSource != null;
			if (flag)
			{
				this.GatherInternal(terrainSource, defaultColor, "PaintContext.Gather", blitMaterial, blitPass, beforeBlit, afterBlit);
			}
		}

		public void Scatter(Func<PaintContext.ITerrainInfo, RenderTexture> terrainDest, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			bool flag = terrainDest != null;
			if (flag)
			{
				this.ScatterInternal(terrainDest, "PaintContext.Scatter", blitMaterial, blitPass, beforeBlit, afterBlit);
			}
		}

		public void GatherHeightmap()
		{
			Material blitMaterial = TerrainPaintUtility.GetHeightBlitMaterial();
			blitMaterial.SetFloat("_Height_Offset", 0f);
			blitMaterial.SetFloat("_Height_Scale", 1f);
			Func<PaintContext.ITerrainInfo, Texture> arg_97_1;
			if ((arg_97_1 = PaintContext.<>c.<>9__57_0) == null)
			{
				arg_97_1 = (PaintContext.<>c.<>9__57_0 = new Func<PaintContext.ITerrainInfo, Texture>(PaintContext.<>c.<>9.<GatherHeightmap>b__57_0));
			}
			this.GatherInternal(arg_97_1, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherHeightmap", blitMaterial, 0, delegate(PaintContext.ITerrainInfo t)
			{
				blitMaterial.SetFloat("_Height_Offset", (t.terrain.GetPosition().y - this.heightWorldSpaceMin) / this.heightWorldSpaceSize * PaintContext.kNormalizedHeightScale);
				blitMaterial.SetFloat("_Height_Scale", t.terrain.terrainData.size.y / this.heightWorldSpaceSize);
			}, null);
		}

		public void ScatterHeightmap(string editorUndoName)
		{
			Material blitMaterial = TerrainPaintUtility.GetHeightBlitMaterial();
			blitMaterial.SetFloat("_Height_Offset", 0f);
			blitMaterial.SetFloat("_Height_Scale", 1f);
			Func<PaintContext.ITerrainInfo, RenderTexture> arg_A3_1;
			if ((arg_A3_1 = PaintContext.<>c.<>9__58_0) == null)
			{
				arg_A3_1 = (PaintContext.<>c.<>9__58_0 = new Func<PaintContext.ITerrainInfo, RenderTexture>(PaintContext.<>c.<>9.<ScatterHeightmap>b__58_0));
			}
			string arg_A3_2 = "PaintContext.ScatterHeightmap";
			Material arg_A3_3 = blitMaterial;
			int arg_A3_4 = 0;
			Action<PaintContext.ITerrainInfo> arg_A3_5 = delegate(PaintContext.ITerrainInfo t)
			{
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> expr_06 = PaintContext.onTerrainTileBeforePaint;
				if (expr_06 != null)
				{
					expr_06(t, PaintContext.ToolAction.PaintHeightmap, editorUndoName);
				}
				blitMaterial.SetFloat("_Height_Offset", (this.heightWorldSpaceMin - t.terrain.GetPosition().y) / t.terrain.terrainData.size.y * PaintContext.kNormalizedHeightScale);
				blitMaterial.SetFloat("_Height_Scale", this.heightWorldSpaceSize / t.terrain.terrainData.size.y);
			};
			Action<PaintContext.ITerrainInfo> arg_A3_6;
			if ((arg_A3_6 = PaintContext.<>c.<>9__58_2) == null)
			{
				arg_A3_6 = (PaintContext.<>c.<>9__58_2 = new Action<PaintContext.ITerrainInfo>(PaintContext.<>c.<>9.<ScatterHeightmap>b__58_2));
			}
			this.ScatterInternal(arg_A3_1, arg_A3_2, arg_A3_3, arg_A3_4, arg_A3_5, arg_A3_6);
		}

		public void GatherHoles()
		{
			Func<PaintContext.ITerrainInfo, Texture> arg_43_1;
			if ((arg_43_1 = PaintContext.<>c.<>9__59_0) == null)
			{
				arg_43_1 = (PaintContext.<>c.<>9__59_0 = new Func<PaintContext.ITerrainInfo, Texture>(PaintContext.<>c.<>9.<GatherHoles>b__59_0));
			}
			this.GatherInternal(arg_43_1, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherHoles", null, 0, null, null);
		}

		public void ScatterHoles(string editorUndoName)
		{
			this.ScatterInternal(delegate(PaintContext.ITerrainInfo t)
			{
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> expr_06 = PaintContext.onTerrainTileBeforePaint;
				if (expr_06 != null)
				{
					expr_06(t, PaintContext.ToolAction.PaintHoles, editorUndoName);
				}
				t.terrain.terrainData.CopyActiveRenderTextureToTexture(TerrainData.HolesTextureName, 0, t.clippedPCPixels, t.clippedTerrainPixels.min, true);
				PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintHoles);
				return null;
			}, "PaintContext.ScatterHoles", null, 0, null, null);
		}

		public void GatherNormals()
		{
			Func<PaintContext.ITerrainInfo, Texture> arg_43_1;
			if ((arg_43_1 = PaintContext.<>c.<>9__61_0) == null)
			{
				arg_43_1 = (PaintContext.<>c.<>9__61_0 = new Func<PaintContext.ITerrainInfo, Texture>(PaintContext.<>c.<>9.<GatherNormals>b__61_0));
			}
			this.GatherInternal(arg_43_1, new Color(0.5f, 0.5f, 0.5f, 0.5f), "PaintContext.GatherNormals", null, 0, null, null);
		}

		private PaintContext.SplatmapUserData GetTerrainLayerUserData(PaintContext.ITerrainInfo context, TerrainLayer terrainLayer = null, bool addLayerIfDoesntExist = false)
		{
			PaintContext.SplatmapUserData splatmapUserData = context.userData as PaintContext.SplatmapUserData;
			bool flag = splatmapUserData != null;
			PaintContext.SplatmapUserData result;
			if (flag)
			{
				bool flag2 = terrainLayer == null || terrainLayer == splatmapUserData.terrainLayer;
				if (flag2)
				{
					result = splatmapUserData;
					return result;
				}
				splatmapUserData = null;
			}
			bool flag3 = splatmapUserData == null;
			if (flag3)
			{
				int num = -1;
				bool flag4 = terrainLayer != null;
				if (flag4)
				{
					num = TerrainPaintUtility.FindTerrainLayerIndex(context.terrain, terrainLayer);
					bool flag5 = num == -1 & addLayerIfDoesntExist;
					if (flag5)
					{
						Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> expr_7C = PaintContext.onTerrainTileBeforePaint;
						if (expr_7C != null)
						{
							expr_7C(context, PaintContext.ToolAction.AddTerrainLayer, "Adding Terrain Layer");
						}
						num = TerrainPaintUtility.AddTerrainLayer(context.terrain, terrainLayer);
					}
				}
				bool flag6 = num != -1;
				if (flag6)
				{
					splatmapUserData = new PaintContext.SplatmapUserData();
					splatmapUserData.terrainLayer = terrainLayer;
					splatmapUserData.terrainLayerIndex = num;
					splatmapUserData.mapIndex = num >> 2;
					splatmapUserData.channelIndex = (num & 3);
				}
				context.userData = splatmapUserData;
			}
			result = splatmapUserData;
			return result;
		}

		public void GatherAlphamap(TerrainLayer inputLayer, bool addLayerIfDoesntExist = true)
		{
			bool flag = inputLayer == null;
			if (!flag)
			{
				Material copyTerrainLayerMaterial = TerrainPaintUtility.GetCopyTerrainLayerMaterial();
				Vector4[] layerMasks = new Vector4[]
				{
					new Vector4(1f, 0f, 0f, 0f),
					new Vector4(0f, 1f, 0f, 0f),
					new Vector4(0f, 0f, 1f, 0f),
					new Vector4(0f, 0f, 0f, 1f)
				};
				this.GatherInternal(delegate(PaintContext.ITerrainInfo t)
				{
					PaintContext.SplatmapUserData terrainLayerUserData = this.GetTerrainLayerUserData(t, inputLayer, addLayerIfDoesntExist);
					bool flag2 = terrainLayerUserData != null;
					Texture result;
					if (flag2)
					{
						result = TerrainPaintUtility.GetTerrainAlphaMapChecked(t.terrain, terrainLayerUserData.mapIndex);
					}
					else
					{
						result = null;
					}
					return result;
				}, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherAlphamap", copyTerrainLayerMaterial, 0, delegate(PaintContext.ITerrainInfo t)
				{
					PaintContext.SplatmapUserData terrainLayerUserData = this.GetTerrainLayerUserData(t, null, false);
					copyTerrainLayerMaterial.SetVector("_LayerMask", layerMasks[terrainLayerUserData.channelIndex]);
				}, null);
			}
		}

		public void ScatterAlphamap(string editorUndoName)
		{
			Vector4[] layerMasks = new Vector4[]
			{
				new Vector4(1f, 0f, 0f, 0f),
				new Vector4(0f, 1f, 0f, 0f),
				new Vector4(0f, 0f, 1f, 0f),
				new Vector4(0f, 0f, 0f, 1f)
			};
			Material copyTerrainLayerMaterial = TerrainPaintUtility.GetCopyTerrainLayerMaterial();
			RenderTexture tempTarget = RenderTexture.GetTemporary(new RenderTextureDescriptor(this.destinationRenderTexture.width, this.destinationRenderTexture.height, RenderTextureFormat.ARGB32)
			{
				sRGB = false,
				useMipMap = false,
				autoGenerateMips = false
			});
			this.ScatterInternal(delegate(PaintContext.ITerrainInfo t)
			{
				PaintContext.SplatmapUserData terrainLayerUserData = this.GetTerrainLayerUserData(t, null, false);
				bool flag = terrainLayerUserData != null;
				if (flag)
				{
					Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> expr_21 = PaintContext.onTerrainTileBeforePaint;
					if (expr_21 != null)
					{
						expr_21(t, PaintContext.ToolAction.PaintTexture, editorUndoName);
					}
					int mapIndex = terrainLayerUserData.mapIndex;
					int channelIndex = terrainLayerUserData.channelIndex;
					Texture2D value = t.terrain.terrainData.alphamapTextures[mapIndex];
					this.destinationRenderTexture.filterMode = FilterMode.Point;
					this.sourceRenderTexture.filterMode = FilterMode.Point;
					for (int i = 0; i <= t.terrain.terrainData.alphamapTextureCount; i++)
					{
						bool flag2 = i == mapIndex;
						if (!flag2)
						{
							int num = (i == t.terrain.terrainData.alphamapTextureCount) ? mapIndex : i;
							Texture2D texture2D = t.terrain.terrainData.alphamapTextures[num];
							bool flag3 = texture2D.width != this.targetTextureWidth || texture2D.height != this.targetTextureHeight;
							if (flag3)
							{
								UnityEngine.Debug.LogWarning("PaintContext alphamap operations must use the same resolution for all Terrains - mismatched Terrains are ignored.", t.terrain);
							}
							else
							{
								RenderTexture.active = tempTarget;
								GL.PushMatrix();
								GL.LoadPixelMatrix(0f, (float)tempTarget.width, 0f, (float)tempTarget.height);
								copyTerrainLayerMaterial.SetTexture("_MainTex", this.destinationRenderTexture);
								copyTerrainLayerMaterial.SetTexture("_OldAlphaMapTexture", this.sourceRenderTexture);
								copyTerrainLayerMaterial.SetTexture("_OriginalTargetAlphaMap", value);
								copyTerrainLayerMaterial.SetTexture("_AlphaMapTexture", texture2D);
								copyTerrainLayerMaterial.SetVector("_LayerMask", (num == mapIndex) ? layerMasks[channelIndex] : Vector4.zero);
								copyTerrainLayerMaterial.SetVector("_OriginalTargetAlphaMask", layerMasks[channelIndex]);
								copyTerrainLayerMaterial.SetPass(1);
								TerrainPaintUtility.DrawQuad2(t.clippedPCPixels, t.clippedPCPixels, this.destinationRenderTexture, t.clippedTerrainPixels, texture2D);
								GL.PopMatrix();
								t.terrain.terrainData.CopyActiveRenderTextureToTexture(TerrainData.AlphamapTextureName, num, t.clippedPCPixels, t.clippedTerrainPixels.min, true);
							}
						}
					}
					RenderTexture.active = null;
					PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintTexture);
				}
				return null;
			}, "PaintContext.ScatterAlphamap", copyTerrainLayerMaterial, 0, null, null);
			RenderTexture.ReleaseTemporary(tempTarget);
		}

		private static void OnTerrainPainted(PaintContext.ITerrainInfo tile, PaintContext.ToolAction action)
		{
			for (int i = 0; i < PaintContext.s_PaintedTerrain.Count; i++)
			{
				bool flag = tile.terrain == PaintContext.s_PaintedTerrain[i].terrain;
				if (flag)
				{
					PaintContext.PaintedTerrain value = PaintContext.s_PaintedTerrain[i];
					value.action |= action;
					PaintContext.s_PaintedTerrain[i] = value;
					return;
				}
			}
			PaintContext.s_PaintedTerrain.Add(new PaintContext.PaintedTerrain
			{
				terrain = tile.terrain,
				action = action
			});
		}

		public static void ApplyDelayedActions()
		{
			for (int i = 0; i < PaintContext.s_PaintedTerrain.Count; i++)
			{
				PaintContext.PaintedTerrain paintedTerrain = PaintContext.s_PaintedTerrain[i];
				TerrainData terrainData = paintedTerrain.terrain.terrainData;
				bool flag = terrainData == null;
				if (!flag)
				{
					bool flag2 = (paintedTerrain.action & PaintContext.ToolAction.PaintHeightmap) > PaintContext.ToolAction.None;
					if (flag2)
					{
						terrainData.SyncHeightmap();
					}
					bool flag3 = (paintedTerrain.action & PaintContext.ToolAction.PaintHoles) > PaintContext.ToolAction.None;
					if (flag3)
					{
						terrainData.SyncTexture(TerrainData.HolesTextureName);
					}
					bool flag4 = (paintedTerrain.action & PaintContext.ToolAction.PaintTexture) > PaintContext.ToolAction.None;
					if (flag4)
					{
						terrainData.SetBaseMapDirty();
						terrainData.SyncTexture(TerrainData.AlphamapTextureName);
					}
					paintedTerrain.terrain.editorRenderFlags = TerrainRenderFlags.all;
				}
			}
			PaintContext.s_PaintedTerrain.Clear();
		}
	}
}
