using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.TerrainAPI
{
	public static class TerrainUtility
	{
		public class TerrainMap
		{
			public delegate bool TerrainFilter(Terrain terrain);

			private struct QueueElement
			{
				public readonly int tileX;

				public readonly int tileZ;

				public readonly Terrain terrain;

				public QueueElement(int tileX, int tileZ, Terrain terrain)
				{
					this.tileX = tileX;
					this.tileZ = tileZ;
					this.terrain = terrain;
				}
			}

			public struct TileCoord
			{
				public readonly int tileX;

				public readonly int tileZ;

				public TileCoord(int tileX, int tileZ)
				{
					this.tileX = tileX;
					this.tileZ = tileZ;
				}
			}

			public enum ErrorCode
			{
				OK,
				Overlapping,
				SizeMismatch = 4,
				EdgeAlignmentMismatch = 8
			}

			private Vector3 m_patchSize;

			public TerrainUtility.TerrainMap.ErrorCode m_errorCode;

			public Dictionary<TerrainUtility.TerrainMap.TileCoord, Terrain> m_terrainTiles;

			public Terrain GetTerrain(int tileX, int tileZ)
			{
				Terrain result = null;
				this.m_terrainTiles.TryGetValue(new TerrainUtility.TerrainMap.TileCoord(tileX, tileZ), out result);
				return result;
			}

			public static TerrainUtility.TerrainMap CreateFromConnectedNeighbors(Terrain originTerrain, TerrainUtility.TerrainMap.TerrainFilter filter = null, bool fullValidation = true)
			{
				bool flag = originTerrain == null;
				TerrainUtility.TerrainMap result;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = originTerrain.terrainData == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						TerrainUtility.TerrainMap terrainMap = new TerrainUtility.TerrainMap();
						Queue<TerrainUtility.TerrainMap.QueueElement> queue = new Queue<TerrainUtility.TerrainMap.QueueElement>();
						queue.Enqueue(new TerrainUtility.TerrainMap.QueueElement(0, 0, originTerrain));
						int num = Terrain.activeTerrains.Length;
						while (queue.Count > 0)
						{
							TerrainUtility.TerrainMap.QueueElement queueElement = queue.Dequeue();
							bool flag3 = filter == null || filter(queueElement.terrain);
							if (flag3)
							{
								bool flag4 = terrainMap.TryToAddTerrain(queueElement.tileX, queueElement.tileZ, queueElement.terrain);
								if (flag4)
								{
									bool flag5 = terrainMap.m_terrainTiles.Count > num;
									if (flag5)
									{
										break;
									}
									bool flag6 = queueElement.terrain.leftNeighbor != null;
									if (flag6)
									{
										queue.Enqueue(new TerrainUtility.TerrainMap.QueueElement(queueElement.tileX - 1, queueElement.tileZ, queueElement.terrain.leftNeighbor));
									}
									bool flag7 = queueElement.terrain.bottomNeighbor != null;
									if (flag7)
									{
										queue.Enqueue(new TerrainUtility.TerrainMap.QueueElement(queueElement.tileX, queueElement.tileZ - 1, queueElement.terrain.bottomNeighbor));
									}
									bool flag8 = queueElement.terrain.rightNeighbor != null;
									if (flag8)
									{
										queue.Enqueue(new TerrainUtility.TerrainMap.QueueElement(queueElement.tileX + 1, queueElement.tileZ, queueElement.terrain.rightNeighbor));
									}
									bool flag9 = queueElement.terrain.topNeighbor != null;
									if (flag9)
									{
										queue.Enqueue(new TerrainUtility.TerrainMap.QueueElement(queueElement.tileX, queueElement.tileZ + 1, queueElement.terrain.topNeighbor));
									}
								}
							}
						}
						if (fullValidation)
						{
							terrainMap.Validate();
						}
						result = terrainMap;
					}
				}
				return result;
			}

			public static TerrainUtility.TerrainMap CreateFromPlacement(Terrain originTerrain, TerrainUtility.TerrainMap.TerrainFilter filter = null, bool fullValidation = true)
			{
				bool flag = Terrain.activeTerrains == null || Terrain.activeTerrains.Length == 0 || originTerrain == null;
				TerrainUtility.TerrainMap result;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = originTerrain.terrainData == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						int groupID = originTerrain.groupingID;
						float x3 = originTerrain.transform.position.x;
						float z = originTerrain.transform.position.z;
						float x2 = originTerrain.terrainData.size.x;
						float z2 = originTerrain.terrainData.size.z;
						bool flag3 = filter == null;
						if (flag3)
						{
							filter = ((Terrain x) => x.groupingID == groupID);
						}
						result = TerrainUtility.TerrainMap.CreateFromPlacement(new Vector2(x3, z), new Vector2(x2, z2), filter, fullValidation);
					}
				}
				return result;
			}

			public static TerrainUtility.TerrainMap CreateFromPlacement(Vector2 gridOrigin, Vector2 gridSize, TerrainUtility.TerrainMap.TerrainFilter filter = null, bool fullValidation = true)
			{
				bool flag = Terrain.activeTerrains == null || Terrain.activeTerrains.Length == 0;
				TerrainUtility.TerrainMap result;
				if (flag)
				{
					result = null;
				}
				else
				{
					TerrainUtility.TerrainMap terrainMap = new TerrainUtility.TerrainMap();
					float num = 1f / gridSize.x;
					float num2 = 1f / gridSize.y;
					Terrain[] activeTerrains = Terrain.activeTerrains;
					for (int i = 0; i < activeTerrains.Length; i++)
					{
						Terrain terrain = activeTerrains[i];
						bool flag2 = terrain.terrainData == null;
						if (!flag2)
						{
							bool flag3 = filter == null || filter(terrain);
							if (flag3)
							{
								Vector3 position = terrain.transform.position;
								int tileX = Mathf.RoundToInt((position.x - gridOrigin.x) * num);
								int tileZ = Mathf.RoundToInt((position.z - gridOrigin.y) * num2);
								terrainMap.TryToAddTerrain(tileX, tileZ, terrain);
							}
						}
					}
					if (fullValidation)
					{
						terrainMap.Validate();
					}
					result = ((terrainMap.m_terrainTiles.Count > 0) ? terrainMap : null);
				}
				return result;
			}

			public TerrainMap()
			{
				this.m_errorCode = TerrainUtility.TerrainMap.ErrorCode.OK;
				this.m_terrainTiles = new Dictionary<TerrainUtility.TerrainMap.TileCoord, Terrain>();
			}

			private void AddTerrainInternal(int x, int z, Terrain terrain)
			{
				bool flag = this.m_terrainTiles.Count == 0;
				if (flag)
				{
					this.m_patchSize = terrain.terrainData.size;
				}
				else
				{
					bool flag2 = terrain.terrainData.size != this.m_patchSize;
					if (flag2)
					{
						this.m_errorCode |= TerrainUtility.TerrainMap.ErrorCode.SizeMismatch;
					}
				}
				this.m_terrainTiles.Add(new TerrainUtility.TerrainMap.TileCoord(x, z), terrain);
			}

			private bool TryToAddTerrain(int tileX, int tileZ, Terrain terrain)
			{
				bool result = false;
				bool flag = terrain != null;
				if (flag)
				{
					Terrain terrain2 = this.GetTerrain(tileX, tileZ);
					bool flag2 = terrain2 != null;
					if (flag2)
					{
						bool flag3 = terrain2 != terrain;
						if (flag3)
						{
							this.m_errorCode |= TerrainUtility.TerrainMap.ErrorCode.Overlapping;
						}
					}
					else
					{
						this.AddTerrainInternal(tileX, tileZ, terrain);
						result = true;
					}
				}
				return result;
			}

			private void ValidateTerrain(int tileX, int tileZ)
			{
				Terrain terrain = this.GetTerrain(tileX, tileZ);
				bool flag = terrain != null;
				if (flag)
				{
					Terrain terrain2 = this.GetTerrain(tileX - 1, tileZ);
					Terrain terrain3 = this.GetTerrain(tileX + 1, tileZ);
					Terrain terrain4 = this.GetTerrain(tileX, tileZ + 1);
					Terrain terrain5 = this.GetTerrain(tileX, tileZ - 1);
					bool flag2 = terrain2;
					if (flag2)
					{
						bool flag3 = !Mathf.Approximately(terrain.transform.position.x, terrain2.transform.position.x + terrain2.terrainData.size.x) || !Mathf.Approximately(terrain.transform.position.z, terrain2.transform.position.z);
						if (flag3)
						{
							this.m_errorCode |= TerrainUtility.TerrainMap.ErrorCode.EdgeAlignmentMismatch;
						}
					}
					bool flag4 = terrain3;
					if (flag4)
					{
						bool flag5 = !Mathf.Approximately(terrain.transform.position.x + terrain.terrainData.size.x, terrain3.transform.position.x) || !Mathf.Approximately(terrain.transform.position.z, terrain3.transform.position.z);
						if (flag5)
						{
							this.m_errorCode |= TerrainUtility.TerrainMap.ErrorCode.EdgeAlignmentMismatch;
						}
					}
					bool flag6 = terrain4;
					if (flag6)
					{
						bool flag7 = !Mathf.Approximately(terrain.transform.position.x, terrain4.transform.position.x) || !Mathf.Approximately(terrain.transform.position.z + terrain.terrainData.size.z, terrain4.transform.position.z);
						if (flag7)
						{
							this.m_errorCode |= TerrainUtility.TerrainMap.ErrorCode.EdgeAlignmentMismatch;
						}
					}
					bool flag8 = terrain5;
					if (flag8)
					{
						bool flag9 = !Mathf.Approximately(terrain.transform.position.x, terrain5.transform.position.x) || !Mathf.Approximately(terrain.transform.position.z, terrain5.transform.position.z + terrain5.terrainData.size.z);
						if (flag9)
						{
							this.m_errorCode |= TerrainUtility.TerrainMap.ErrorCode.EdgeAlignmentMismatch;
						}
					}
				}
			}

			private TerrainUtility.TerrainMap.ErrorCode Validate()
			{
				foreach (TerrainUtility.TerrainMap.TileCoord current in this.m_terrainTiles.Keys)
				{
					this.ValidateTerrain(current.tileX, current.tileZ);
				}
				return this.m_errorCode;
			}
		}

		public class TerrainGroups : Dictionary<int, TerrainUtility.TerrainMap>
		{
		}

		internal static bool HasValidTerrains()
		{
			return Terrain.activeTerrains != null && Terrain.activeTerrains.Length != 0;
		}

		internal static void ClearConnectivity()
		{
			Terrain[] activeTerrains = Terrain.activeTerrains;
			for (int i = 0; i < activeTerrains.Length; i++)
			{
				Terrain terrain = activeTerrains[i];
				terrain.SetNeighbors(null, null, null, null);
			}
		}

		internal static TerrainUtility.TerrainGroups CollectTerrains(bool onlyAutoConnectedTerrains = true)
		{
			bool flag = !TerrainUtility.HasValidTerrains();
			TerrainUtility.TerrainGroups result;
			if (flag)
			{
				result = null;
			}
			else
			{
				TerrainUtility.TerrainGroups terrainGroups = new TerrainUtility.TerrainGroups();
				Terrain[] activeTerrains = Terrain.activeTerrains;
				for (int i = 0; i < activeTerrains.Length; i++)
				{
					Terrain t = activeTerrains[i];
					bool flag2 = onlyAutoConnectedTerrains && !t.allowAutoConnect;
					if (!flag2)
					{
						bool flag3 = !terrainGroups.ContainsKey(t.groupingID);
						if (flag3)
						{
							TerrainUtility.TerrainMap terrainMap = TerrainUtility.TerrainMap.CreateFromPlacement(t, (Terrain x) => x.groupingID == t.groupingID && (!onlyAutoConnectedTerrains || x.allowAutoConnect), true);
							bool flag4 = terrainMap != null;
							if (flag4)
							{
								terrainGroups.Add(t.groupingID, terrainMap);
							}
						}
					}
				}
				result = ((terrainGroups.Count != 0) ? terrainGroups : null);
			}
			return result;
		}

		[RequiredByNativeCode]
		public static void AutoConnect()
		{
			bool flag = !TerrainUtility.HasValidTerrains();
			if (!flag)
			{
				TerrainUtility.ClearConnectivity();
				TerrainUtility.TerrainGroups terrainGroups = TerrainUtility.CollectTerrains(true);
				bool flag2 = terrainGroups == null;
				if (!flag2)
				{
					foreach (KeyValuePair<int, TerrainUtility.TerrainMap> current in terrainGroups)
					{
						TerrainUtility.TerrainMap value = current.Value;
						foreach (KeyValuePair<TerrainUtility.TerrainMap.TileCoord, Terrain> current2 in value.m_terrainTiles)
						{
							TerrainUtility.TerrainMap.TileCoord key = current2.Key;
							Terrain terrain = value.GetTerrain(key.tileX, key.tileZ);
							Terrain terrain2 = value.GetTerrain(key.tileX - 1, key.tileZ);
							Terrain terrain3 = value.GetTerrain(key.tileX + 1, key.tileZ);
							Terrain terrain4 = value.GetTerrain(key.tileX, key.tileZ + 1);
							Terrain terrain5 = value.GetTerrain(key.tileX, key.tileZ - 1);
							terrain.SetNeighbors(terrain2, terrain4, terrain3, terrain5);
						}
					}
				}
			}
		}
	}
}
