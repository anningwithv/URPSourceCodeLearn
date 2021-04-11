using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	internal class InternalStaticBatchingUtility
	{
		public class StaticBatcherGOSorter
		{
			public virtual long GetMaterialId(Renderer renderer)
			{
				bool flag = renderer == null || renderer.sharedMaterial == null;
				long result;
				if (flag)
				{
					result = 0L;
				}
				else
				{
					result = (long)renderer.sharedMaterial.GetInstanceID();
				}
				return result;
			}

			public int GetLightmapIndex(Renderer renderer)
			{
				bool flag = renderer == null;
				int result;
				if (flag)
				{
					result = -1;
				}
				else
				{
					result = renderer.lightmapIndex;
				}
				return result;
			}

			public static Renderer GetRenderer(GameObject go)
			{
				bool flag = go == null;
				Renderer result;
				if (flag)
				{
					result = null;
				}
				else
				{
					MeshFilter meshFilter = go.GetComponent(typeof(MeshFilter)) as MeshFilter;
					bool flag2 = meshFilter == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						result = meshFilter.GetComponent<Renderer>();
					}
				}
				return result;
			}

			public virtual long GetRendererId(Renderer renderer)
			{
				bool flag = renderer == null;
				long result;
				if (flag)
				{
					result = -1L;
				}
				else
				{
					result = (long)renderer.GetInstanceID();
				}
				return result;
			}
		}

		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly InternalStaticBatchingUtility.<>c <>9 = new InternalStaticBatchingUtility.<>c();

			public static Func<Material, bool> <>9__5_0;

			internal bool <CombineGameObjects>b__5_0(Material m)
			{
				return m != null && m.shader != null && m.shader.disableBatching > DisableBatchingType.False;
			}
		}

		private const int MaxVerticesInBatch = 64000;

		private const string CombinedMeshPrefix = "Combined Mesh";

		public static void CombineRoot(GameObject staticBatchRoot, InternalStaticBatchingUtility.StaticBatcherGOSorter sorter)
		{
			InternalStaticBatchingUtility.Combine(staticBatchRoot, false, false, sorter);
		}

		public static void Combine(GameObject staticBatchRoot, bool combineOnlyStatic, bool isEditorPostprocessScene, InternalStaticBatchingUtility.StaticBatcherGOSorter sorter)
		{
			GameObject[] array = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
			List<GameObject> list = new List<GameObject>();
			GameObject[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				GameObject gameObject = array2[i];
				bool flag = staticBatchRoot != null;
				if (!flag)
				{
					goto IL_53;
				}
				bool flag2 = !gameObject.transform.IsChildOf(staticBatchRoot.transform);
				if (!flag2)
				{
					goto IL_53;
				}
				IL_75:
				i++;
				continue;
				IL_53:
				bool flag3 = combineOnlyStatic && !gameObject.isStaticBatchable;
				if (flag3)
				{
					goto IL_75;
				}
				list.Add(gameObject);
				goto IL_75;
			}
			array = list.ToArray();
			InternalStaticBatchingUtility.CombineGameObjects(array, staticBatchRoot, isEditorPostprocessScene, sorter);
		}

		public static GameObject[] SortGameObjectsForStaticbatching(GameObject[] gos, InternalStaticBatchingUtility.StaticBatcherGOSorter sorter)
		{
			gos = gos.OrderBy(delegate(GameObject x)
			{
				Renderer renderer = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetRenderer(x);
				return sorter.GetMaterialId(renderer);
			}).ThenBy(delegate(GameObject y)
			{
				Renderer renderer = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetRenderer(y);
				return sorter.GetLightmapIndex(renderer);
			}).ThenBy(delegate(GameObject z)
			{
				Renderer renderer = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetRenderer(z);
				return sorter.GetRendererId(renderer);
			}).ToArray<GameObject>();
			return gos;
		}

		public static void CombineGameObjects(GameObject[] gos, GameObject staticBatchRoot, bool isEditorPostprocessScene, InternalStaticBatchingUtility.StaticBatcherGOSorter sorter)
		{
			Matrix4x4 lhs = Matrix4x4.identity;
			Transform staticBatchRootTransform = null;
			bool flag = staticBatchRoot;
			if (flag)
			{
				lhs = staticBatchRoot.transform.worldToLocalMatrix;
				staticBatchRootTransform = staticBatchRoot.transform;
			}
			int batchIndex = 0;
			int num = 0;
			List<MeshSubsetCombineUtility.MeshContainer> list = new List<MeshSubsetCombineUtility.MeshContainer>();
			gos = InternalStaticBatchingUtility.SortGameObjectsForStaticbatching(gos, sorter ?? new InternalStaticBatchingUtility.StaticBatcherGOSorter());
			GameObject[] array = gos;
			for (int i = 0; i < array.Length; i++)
			{
				GameObject gameObject = array[i];
				MeshFilter meshFilter = gameObject.GetComponent(typeof(MeshFilter)) as MeshFilter;
				bool flag2 = meshFilter == null;
				if (!flag2)
				{
					Mesh sharedMesh = meshFilter.sharedMesh;
					bool flag3 = sharedMesh == null || (!isEditorPostprocessScene && !sharedMesh.canAccess);
					if (!flag3)
					{
						Renderer component = meshFilter.GetComponent<Renderer>();
						bool flag4 = component == null || !component.enabled;
						if (!flag4)
						{
							bool flag5 = component.staticBatchIndex != 0;
							if (!flag5)
							{
								Material[] array2 = component.sharedMaterials;
								IEnumerable<Material> arg_121_0 = array2;
								Func<Material, bool> arg_121_1;
								if ((arg_121_1 = InternalStaticBatchingUtility.<>c.<>9__5_0) == null)
								{
									arg_121_1 = (InternalStaticBatchingUtility.<>c.<>9__5_0 = new Func<Material, bool>(InternalStaticBatchingUtility.<>c.<>9.<CombineGameObjects>b__5_0));
								}
								bool flag6 = arg_121_0.Any(arg_121_1);
								if (!flag6)
								{
									int vertexCount = sharedMesh.vertexCount;
									bool flag7 = vertexCount == 0;
									if (!flag7)
									{
										MeshRenderer meshRenderer = component as MeshRenderer;
										bool flag8 = meshRenderer != null;
										if (flag8)
										{
											bool flag9 = meshRenderer.additionalVertexStreams != null;
											if (flag9)
											{
												bool flag10 = vertexCount != meshRenderer.additionalVertexStreams.vertexCount;
												if (flag10)
												{
													goto IL_421;
												}
											}
											bool flag11 = meshRenderer.enlightenVertexStream != null;
											if (flag11)
											{
												bool flag12 = vertexCount != meshRenderer.enlightenVertexStream.vertexCount;
												if (flag12)
												{
													goto IL_421;
												}
											}
										}
										bool flag13 = num + vertexCount > 64000;
										if (flag13)
										{
											InternalStaticBatchingUtility.MakeBatch(list, staticBatchRootTransform, batchIndex++);
											list.Clear();
											num = 0;
										}
										MeshSubsetCombineUtility.MeshInstance meshInstance = default(MeshSubsetCombineUtility.MeshInstance);
										meshInstance.meshInstanceID = sharedMesh.GetInstanceID();
										meshInstance.rendererInstanceID = component.GetInstanceID();
										bool flag14 = meshRenderer != null;
										if (flag14)
										{
											bool flag15 = meshRenderer.additionalVertexStreams != null;
											if (flag15)
											{
												meshInstance.additionalVertexStreamsMeshInstanceID = meshRenderer.additionalVertexStreams.GetInstanceID();
											}
											bool flag16 = meshRenderer.enlightenVertexStream != null;
											if (flag16)
											{
												meshInstance.enlightenVertexStreamMeshInstanceID = meshRenderer.enlightenVertexStream.GetInstanceID();
											}
										}
										meshInstance.transform = lhs * meshFilter.transform.localToWorldMatrix;
										meshInstance.lightmapScaleOffset = component.lightmapScaleOffset;
										meshInstance.realtimeLightmapScaleOffset = component.realtimeLightmapScaleOffset;
										MeshSubsetCombineUtility.MeshContainer meshContainer = default(MeshSubsetCombineUtility.MeshContainer);
										meshContainer.gameObject = gameObject;
										meshContainer.instance = meshInstance;
										meshContainer.subMeshInstances = new List<MeshSubsetCombineUtility.SubMeshInstance>();
										list.Add(meshContainer);
										bool flag17 = array2.Length > sharedMesh.subMeshCount;
										if (flag17)
										{
											Debug.LogWarning(string.Concat(new string[]
											{
												"Mesh '",
												sharedMesh.name,
												"' has more materials (",
												array2.Length.ToString(),
												") than subsets (",
												sharedMesh.subMeshCount.ToString(),
												")"
											}), component);
											Material[] array3 = new Material[sharedMesh.subMeshCount];
											for (int j = 0; j < sharedMesh.subMeshCount; j++)
											{
												array3[j] = component.sharedMaterials[j];
											}
											component.sharedMaterials = array3;
											array2 = array3;
										}
										for (int k = 0; k < Math.Min(array2.Length, sharedMesh.subMeshCount); k++)
										{
											MeshSubsetCombineUtility.SubMeshInstance item = default(MeshSubsetCombineUtility.SubMeshInstance);
											item.meshInstanceID = meshFilter.sharedMesh.GetInstanceID();
											item.vertexOffset = num;
											item.subMeshIndex = k;
											item.gameObjectInstanceID = gameObject.GetInstanceID();
											item.transform = meshInstance.transform;
											meshContainer.subMeshInstances.Add(item);
										}
										num += sharedMesh.vertexCount;
									}
								}
							}
						}
					}
				}
				IL_421:;
			}
			InternalStaticBatchingUtility.MakeBatch(list, staticBatchRootTransform, batchIndex);
		}

		private static void MakeBatch(List<MeshSubsetCombineUtility.MeshContainer> meshes, Transform staticBatchRootTransform, int batchIndex)
		{
			bool flag = meshes.Count < 2;
			if (!flag)
			{
				List<MeshSubsetCombineUtility.MeshInstance> list = new List<MeshSubsetCombineUtility.MeshInstance>();
				List<MeshSubsetCombineUtility.SubMeshInstance> list2 = new List<MeshSubsetCombineUtility.SubMeshInstance>();
				foreach (MeshSubsetCombineUtility.MeshContainer current in meshes)
				{
					list.Add(current.instance);
					list2.AddRange(current.subMeshInstances);
				}
				string text = "Combined Mesh";
				text = text + " (root: " + ((staticBatchRootTransform != null) ? staticBatchRootTransform.name : "scene") + ")";
				bool flag2 = batchIndex > 0;
				if (flag2)
				{
					text = text + " " + (batchIndex + 1).ToString();
				}
				Mesh mesh = StaticBatchingHelper.InternalCombineVertices(list.ToArray(), text);
				StaticBatchingHelper.InternalCombineIndices(list2.ToArray(), mesh);
				int num = 0;
				foreach (MeshSubsetCombineUtility.MeshContainer current2 in meshes)
				{
					MeshFilter meshFilter = (MeshFilter)current2.gameObject.GetComponent(typeof(MeshFilter));
					meshFilter.sharedMesh = mesh;
					int count = current2.subMeshInstances.Count;
					Renderer component = current2.gameObject.GetComponent<Renderer>();
					component.SetStaticBatchInfo(num, count);
					component.staticBatchRootTransform = staticBatchRootTransform;
					component.enabled = false;
					component.enabled = true;
					MeshRenderer meshRenderer = component as MeshRenderer;
					bool flag3 = meshRenderer != null;
					if (flag3)
					{
						meshRenderer.additionalVertexStreams = null;
						meshRenderer.enlightenVertexStream = null;
					}
					num += count;
				}
			}
		}
	}
}
