using System;
using Unity.Collections;
using Unity.Profiling;
using UnityEngine.TextCore;

namespace UnityEngine.UIElements.UIR
{
	internal static class MeshBuilder
	{
		internal struct AllocMeshData
		{
			internal delegate MeshWriteData Allocator(uint vertexCount, uint indexCount, ref MeshBuilder.AllocMeshData allocatorData);

			internal MeshBuilder.AllocMeshData.Allocator alloc;

			internal Texture texture;

			internal Material material;

			internal MeshGenerationContext.MeshFlags flags;

			internal MeshWriteData Allocate(uint vertexCount, uint indexCount)
			{
				return this.alloc(vertexCount, indexCount, ref this);
			}
		}

		private struct ClipCounts
		{
			public int firstClippedIndex;

			public int firstDegenerateIndex;

			public int lastClippedIndex;

			public int clippedTriangles;

			public int addedTriangles;

			public int degenerateTriangles;
		}

		private static ProfilerMarker s_VectorGraphics9Slice = new ProfilerMarker("UIR.MakeVector9Slice");

		private static ProfilerMarker s_VectorGraphicsStretch = new ProfilerMarker("UIR.MakeVectorStretch");

		private static readonly ushort[] slicedQuadIndices = new ushort[]
		{
			0,
			4,
			1,
			4,
			5,
			1,
			1,
			5,
			2,
			5,
			6,
			2,
			2,
			6,
			3,
			6,
			7,
			3,
			4,
			8,
			5,
			8,
			9,
			5,
			5,
			9,
			6,
			9,
			10,
			6,
			6,
			10,
			7,
			10,
			11,
			7,
			8,
			12,
			9,
			12,
			13,
			9,
			9,
			13,
			10,
			13,
			14,
			10,
			10,
			14,
			11,
			14,
			15,
			11
		};

		private static readonly float[] k_TexCoordSlicesX = new float[4];

		private static readonly float[] k_TexCoordSlicesY = new float[4];

		private static readonly float[] k_PositionSlicesX = new float[4];

		private static readonly float[] k_PositionSlicesY = new float[4];

		internal static void MakeBorder(MeshGenerationContextUtils.BorderParams borderParams, float posZ, MeshBuilder.AllocMeshData meshAlloc)
		{
			Tessellation.TessellateBorder(borderParams, posZ, meshAlloc);
		}

		internal static void MakeSolidRect(MeshGenerationContextUtils.RectangleParams rectParams, float posZ, MeshBuilder.AllocMeshData meshAlloc)
		{
			bool flag = !rectParams.HasRadius(Tessellation.kEpsilon);
			if (flag)
			{
				MeshBuilder.MakeQuad(rectParams.rect, Rect.zero, rectParams.color, posZ, meshAlloc);
			}
			else
			{
				Tessellation.TessellateRect(rectParams, posZ, meshAlloc, false);
			}
		}

		internal static void MakeTexturedRect(MeshGenerationContextUtils.RectangleParams rectParams, float posZ, MeshBuilder.AllocMeshData meshAlloc)
		{
			bool flag = (float)rectParams.leftSlice <= Mathf.Epsilon && (float)rectParams.topSlice <= Mathf.Epsilon && (float)rectParams.rightSlice <= Mathf.Epsilon && (float)rectParams.bottomSlice <= Mathf.Epsilon;
			if (flag)
			{
				bool flag2 = !rectParams.HasRadius(Tessellation.kEpsilon);
				if (flag2)
				{
					MeshBuilder.MakeQuad(rectParams.rect, rectParams.uv, rectParams.color, posZ, meshAlloc);
				}
				else
				{
					Tessellation.TessellateRect(rectParams, posZ, meshAlloc, true);
				}
			}
			else
			{
				bool flag3 = rectParams.texture == null;
				if (flag3)
				{
					MeshBuilder.MakeQuad(rectParams.rect, rectParams.uv, rectParams.color, posZ, meshAlloc);
				}
				else
				{
					MeshBuilder.MakeSlicedQuad(ref rectParams, posZ, meshAlloc);
				}
			}
		}

		private static Vertex ConvertTextVertexToUIRVertex(MeshInfo info, int index, Vector2 offset)
		{
			return new Vertex
			{
				position = new Vector3(info.vertices[index].x + offset.x, info.vertices[index].y + offset.y, 0f),
				uv = info.uvs0[index],
				tint = info.colors32[index],
				idsFlags = new Color32(0, 0, 0, 1)
			};
		}

		private static Vertex ConvertTextVertexToUIRVertex(UnityEngine.UIElements.TextVertex textVertex, Vector2 offset)
		{
			return new Vertex
			{
				position = new Vector3(textVertex.position.x + offset.x, textVertex.position.y + offset.y, 0f),
				uv = textVertex.uv0,
				tint = textVertex.color,
				idsFlags = new Color32(0, 0, 0, 1)
			};
		}

		private static int LimitTextVertices(int vertexCount, bool logTruncation = true)
		{
			bool flag = vertexCount <= 49152;
			int result;
			if (flag)
			{
				result = vertexCount;
			}
			else
			{
				if (logTruncation)
				{
					Debug.LogError(string.Format("Generated text will be truncated because it exceeds {0} vertices.", 49152));
				}
				result = 49152;
			}
			return result;
		}

		internal static void MakeText(MeshInfo meshInfo, Vector2 offset, MeshBuilder.AllocMeshData meshAlloc)
		{
			int num = MeshBuilder.LimitTextVertices(meshInfo.vertexCount, true);
			int num2 = num / 4;
			MeshWriteData meshWriteData = meshAlloc.Allocate((uint)(num2 * 4), (uint)(num2 * 6));
			int i = 0;
			int num3 = 0;
			int num4 = 0;
			while (i < num2)
			{
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(meshInfo, num3, offset));
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(meshInfo, num3 + 1, offset));
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(meshInfo, num3 + 2, offset));
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(meshInfo, num3 + 3, offset));
				meshWriteData.SetNextIndex((ushort)num3);
				meshWriteData.SetNextIndex((ushort)(num3 + 1));
				meshWriteData.SetNextIndex((ushort)(num3 + 2));
				meshWriteData.SetNextIndex((ushort)(num3 + 2));
				meshWriteData.SetNextIndex((ushort)(num3 + 3));
				meshWriteData.SetNextIndex((ushort)num3);
				i++;
				num3 += 4;
				num4 += 6;
			}
		}

		internal static void MakeText(NativeArray<UnityEngine.UIElements.TextVertex> uiVertices, Vector2 offset, MeshBuilder.AllocMeshData meshAlloc)
		{
			int num = MeshBuilder.LimitTextVertices(uiVertices.Length, true);
			int num2 = num / 4;
			MeshWriteData meshWriteData = meshAlloc.Allocate((uint)(num2 * 4), (uint)(num2 * 6));
			int i = 0;
			int num3 = 0;
			while (i < num2)
			{
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(uiVertices[num3], offset));
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(uiVertices[num3 + 1], offset));
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(uiVertices[num3 + 2], offset));
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(uiVertices[num3 + 3], offset));
				meshWriteData.SetNextIndex((ushort)num3);
				meshWriteData.SetNextIndex((ushort)(num3 + 1));
				meshWriteData.SetNextIndex((ushort)(num3 + 2));
				meshWriteData.SetNextIndex((ushort)(num3 + 2));
				meshWriteData.SetNextIndex((ushort)(num3 + 3));
				meshWriteData.SetNextIndex((ushort)num3);
				i++;
				num3 += 4;
			}
		}

		internal static void UpdateText(NativeArray<UnityEngine.UIElements.TextVertex> uiVertices, Vector2 offset, Matrix4x4 transform, Color32 xformClipPages, Color32 idsFlags, Color32 opacityPageSVGSettingIndex, NativeSlice<Vertex> vertices)
		{
			int num = MeshBuilder.LimitTextVertices(uiVertices.Length, false);
			Debug.Assert(num == vertices.Length);
			idsFlags.a = 1;
			for (int i = 0; i < num; i++)
			{
				UnityEngine.UIElements.TextVertex textVertex = uiVertices[i];
				vertices[i] = new Vertex
				{
					position = transform.MultiplyPoint3x4(new Vector3(textVertex.position.x + offset.x, textVertex.position.y + offset.y, 0f)),
					uv = textVertex.uv0,
					tint = textVertex.color,
					xformClipPages = xformClipPages,
					idsFlags = idsFlags,
					opacityPageSVGSettingIndex = opacityPageSVGSettingIndex
				};
			}
		}

		private static void MakeQuad(Rect rcPosition, Rect rcTexCoord, Color color, float posZ, MeshBuilder.AllocMeshData meshAlloc)
		{
			MeshWriteData meshWriteData = meshAlloc.Allocate(4u, 6u);
			float x = rcPosition.x;
			float xMax = rcPosition.xMax;
			float yMax = rcPosition.yMax;
			float y = rcPosition.y;
			Rect uvRegion = meshWriteData.uvRegion;
			float x2 = rcTexCoord.x * uvRegion.width + uvRegion.xMin;
			float x3 = rcTexCoord.xMax * uvRegion.width + uvRegion.xMin;
			float y2 = rcTexCoord.y * uvRegion.height + uvRegion.yMin;
			float y3 = rcTexCoord.yMax * uvRegion.height + uvRegion.yMin;
			meshWriteData.SetNextVertex(new Vertex
			{
				position = new Vector3(x, yMax, posZ),
				tint = color,
				uv = new Vector2(x2, y2)
			});
			meshWriteData.SetNextVertex(new Vertex
			{
				position = new Vector3(xMax, yMax, posZ),
				tint = color,
				uv = new Vector2(x3, y2)
			});
			meshWriteData.SetNextVertex(new Vertex
			{
				position = new Vector3(x, y, posZ),
				tint = color,
				uv = new Vector2(x2, y3)
			});
			meshWriteData.SetNextVertex(new Vertex
			{
				position = new Vector3(xMax, y, posZ),
				tint = color,
				uv = new Vector2(x3, y3)
			});
			meshWriteData.SetNextIndex(0);
			meshWriteData.SetNextIndex(2);
			meshWriteData.SetNextIndex(1);
			meshWriteData.SetNextIndex(1);
			meshWriteData.SetNextIndex(2);
			meshWriteData.SetNextIndex(3);
		}

		internal static void MakeSlicedQuad(ref MeshGenerationContextUtils.RectangleParams rectParams, float posZ, MeshBuilder.AllocMeshData meshAlloc)
		{
			MeshWriteData meshWriteData = meshAlloc.Allocate(16u, 54u);
			float num = 1f;
			Texture2D texture2D = rectParams.texture as Texture2D;
			bool flag = texture2D != null;
			if (flag)
			{
				num = texture2D.pixelsPerPoint;
			}
			float num2 = (float)rectParams.texture.width;
			float num3 = (float)rectParams.texture.height;
			float num4 = num / num2;
			float num5 = num / num3;
			float num6 = Mathf.Max(0f, (float)rectParams.leftSlice);
			float num7 = Mathf.Max(0f, (float)rectParams.rightSlice);
			float num8 = Mathf.Max(0f, (float)rectParams.bottomSlice);
			float num9 = Mathf.Max(0f, (float)rectParams.topSlice);
			float num10 = Mathf.Clamp(num6 * num4, 0f, 1f);
			float num11 = Mathf.Clamp(num7 * num4, 0f, 1f);
			float num12 = Mathf.Clamp(num8 * num5, 0f, 1f);
			float num13 = Mathf.Clamp(num9 * num5, 0f, 1f);
			MeshBuilder.k_TexCoordSlicesX[0] = rectParams.uv.min.x;
			MeshBuilder.k_TexCoordSlicesX[1] = rectParams.uv.min.x + num10;
			MeshBuilder.k_TexCoordSlicesX[2] = rectParams.uv.max.x - num11;
			MeshBuilder.k_TexCoordSlicesX[3] = rectParams.uv.max.x;
			MeshBuilder.k_TexCoordSlicesY[0] = rectParams.uv.max.y;
			MeshBuilder.k_TexCoordSlicesY[1] = rectParams.uv.max.y - num12;
			MeshBuilder.k_TexCoordSlicesY[2] = rectParams.uv.min.y + num13;
			MeshBuilder.k_TexCoordSlicesY[3] = rectParams.uv.min.y;
			Rect uvRegion = meshWriteData.uvRegion;
			for (int i = 0; i < 4; i++)
			{
				MeshBuilder.k_TexCoordSlicesX[i] = MeshBuilder.k_TexCoordSlicesX[i] * uvRegion.width + uvRegion.xMin;
				MeshBuilder.k_TexCoordSlicesY[i] = (rectParams.uv.min.y + rectParams.uv.max.y - MeshBuilder.k_TexCoordSlicesY[i]) * uvRegion.height + uvRegion.yMin;
			}
			float num14 = num6 + num7;
			bool flag2 = num14 > rectParams.rect.width;
			if (flag2)
			{
				float num15 = rectParams.rect.width / num14;
				num6 *= num15;
				num7 *= num15;
			}
			float num16 = num8 + num9;
			bool flag3 = num16 > rectParams.rect.height;
			if (flag3)
			{
				float num17 = rectParams.rect.height / num16;
				num8 *= num17;
				num9 *= num17;
			}
			MeshBuilder.k_PositionSlicesX[0] = rectParams.rect.x;
			MeshBuilder.k_PositionSlicesX[1] = rectParams.rect.x + num6;
			MeshBuilder.k_PositionSlicesX[2] = rectParams.rect.xMax - num7;
			MeshBuilder.k_PositionSlicesX[3] = rectParams.rect.xMax;
			MeshBuilder.k_PositionSlicesY[0] = rectParams.rect.yMax;
			MeshBuilder.k_PositionSlicesY[1] = rectParams.rect.yMax - num8;
			MeshBuilder.k_PositionSlicesY[2] = rectParams.rect.y + num9;
			MeshBuilder.k_PositionSlicesY[3] = rectParams.rect.y;
			for (int j = 0; j < 16; j++)
			{
				int num18 = j % 4;
				int num19 = j / 4;
				meshWriteData.SetNextVertex(new Vertex
				{
					position = new Vector3(MeshBuilder.k_PositionSlicesX[num18], MeshBuilder.k_PositionSlicesY[num19], posZ),
					uv = new Vector2(MeshBuilder.k_TexCoordSlicesX[num18], MeshBuilder.k_TexCoordSlicesY[num19]),
					tint = rectParams.color
				});
			}
			meshWriteData.SetAllIndices(MeshBuilder.slicedQuadIndices);
		}

		internal static void MakeVectorGraphics(MeshGenerationContextUtils.RectangleParams rectParams, int settingIndexOffset, MeshBuilder.AllocMeshData meshAlloc, out int finalVertexCount, out int finalIndexCount)
		{
			VectorImage vectorImage = rectParams.vectorImage;
			Debug.Assert(vectorImage != null);
			finalVertexCount = 0;
			finalIndexCount = 0;
			int num = vectorImage.vertices.Length;
			Vertex[] array = new Vertex[num];
			for (int i = 0; i < num; i++)
			{
				VectorImageVertex vectorImageVertex = vectorImage.vertices[i];
				array[i] = new Vertex
				{
					position = vectorImageVertex.position,
					tint = vectorImageVertex.tint,
					uv = vectorImageVertex.uv,
					opacityPageSVGSettingIndex = new Color32(0, 0, (byte)(vectorImageVertex.settingIndex >> 8), (byte)vectorImageVertex.settingIndex)
				};
			}
			bool flag = (float)rectParams.leftSlice <= Mathf.Epsilon && (float)rectParams.topSlice <= Mathf.Epsilon && (float)rectParams.rightSlice <= Mathf.Epsilon && (float)rectParams.bottomSlice <= Mathf.Epsilon;
			if (flag)
			{
				MeshBuilder.MakeVectorGraphicsStretchBackground(array, vectorImage.indices, vectorImage.size.x, vectorImage.size.y, rectParams.rect, rectParams.uv, rectParams.scaleMode, rectParams.color, settingIndexOffset, meshAlloc, out finalVertexCount, out finalIndexCount);
			}
			else
			{
				Vector4 sliceLTRB = new Vector4((float)rectParams.leftSlice, (float)rectParams.topSlice, (float)rectParams.rightSlice, (float)rectParams.bottomSlice);
				MeshBuilder.MakeVectorGraphics9SliceBackground(array, vectorImage.indices, vectorImage.size.x, vectorImage.size.y, rectParams.rect, sliceLTRB, true, rectParams.color, settingIndexOffset, meshAlloc);
			}
		}

		internal static void MakeVectorGraphicsStretchBackground(Vertex[] svgVertices, ushort[] svgIndices, float svgWidth, float svgHeight, Rect targetRect, Rect sourceUV, ScaleMode scaleMode, Color tint, int settingIndexOffset, MeshBuilder.AllocMeshData meshAlloc, out int finalVertexCount, out int finalIndexCount)
		{
			Vector2 vector = new Vector2(svgWidth * sourceUV.width, svgHeight * sourceUV.height);
			Vector2 vector2 = new Vector2(sourceUV.xMin * svgWidth, sourceUV.yMin * svgHeight);
			Rect rect = new Rect(vector2, vector);
			bool flag = sourceUV.xMin != 0f || sourceUV.yMin != 0f || sourceUV.width != 1f || sourceUV.height != 1f;
			float num = vector.x / vector.y;
			float num2 = targetRect.width / targetRect.height;
			Vector2 vector3;
			Vector2 vector4;
			switch (scaleMode)
			{
			case ScaleMode.StretchToFill:
				vector3 = new Vector2(0f, 0f);
				vector4.x = targetRect.width / vector.x;
				vector4.y = targetRect.height / vector.y;
				break;
			case ScaleMode.ScaleAndCrop:
			{
				vector3 = new Vector2(0f, 0f);
				bool flag2 = num2 > num;
				if (flag2)
				{
					vector4.x = (vector4.y = targetRect.width / vector.x);
					float num3 = targetRect.height / vector4.y;
					float num4 = rect.height / 2f - num3 / 2f;
					vector3.y -= num4 * vector4.y;
					rect.y += num4;
					rect.height = num3;
					flag = true;
				}
				else
				{
					bool flag3 = num2 < num;
					if (flag3)
					{
						vector4.x = (vector4.y = targetRect.height / vector.y);
						float num5 = targetRect.width / vector4.x;
						float num6 = rect.width / 2f - num5 / 2f;
						vector3.x -= num6 * vector4.x;
						rect.x += num6;
						rect.width = num5;
						flag = true;
					}
					else
					{
						vector4.x = (vector4.y = targetRect.width / vector.x);
					}
				}
				break;
			}
			case ScaleMode.ScaleToFit:
			{
				bool flag4 = num2 > num;
				if (flag4)
				{
					vector4.x = (vector4.y = targetRect.height / vector.y);
					vector3.x = (targetRect.width - vector.x * vector4.x) * 0.5f;
					vector3.y = 0f;
				}
				else
				{
					vector4.x = (vector4.y = targetRect.width / vector.x);
					vector3.x = 0f;
					vector3.y = (targetRect.height - vector.y * vector4.y) * 0.5f;
				}
				break;
			}
			default:
				throw new NotImplementedException();
			}
			MeshBuilder.s_VectorGraphicsStretch.Begin();
			vector3 -= vector2 * vector4;
			int num7 = svgVertices.Length;
			int num8 = svgIndices.Length;
			MeshBuilder.ClipCounts clipCounts = default(MeshBuilder.ClipCounts);
			Vector4 zero = Vector4.zero;
			bool flag5 = flag;
			if (flag5)
			{
				bool flag6 = rect.width <= 0f || rect.height <= 0f;
				if (flag6)
				{
					finalVertexCount = (finalIndexCount = 0);
					MeshBuilder.s_VectorGraphicsStretch.End();
					return;
				}
				zero = new Vector4(rect.xMin, rect.yMin, rect.xMax, rect.yMax);
				clipCounts = MeshBuilder.UpperBoundApproximateRectClippingResults(svgVertices, svgIndices, zero);
				num7 += clipCounts.clippedTriangles * 6;
				num8 += clipCounts.addedTriangles * 3;
				num8 -= clipCounts.degenerateTriangles * 3;
			}
			MeshWriteData meshWriteData = meshAlloc.alloc((uint)num7, (uint)num8, ref meshAlloc);
			bool flag7 = flag;
			if (flag7)
			{
				MeshBuilder.RectClip(svgVertices, svgIndices, zero, meshWriteData, clipCounts, ref num7);
			}
			else
			{
				meshWriteData.SetAllIndices(svgIndices);
			}
			Debug.Assert(meshWriteData.currentVertex == 0);
			Rect uvRegion = meshWriteData.uvRegion;
			int num9 = svgVertices.Length;
			for (int i = 0; i < num9; i++)
			{
				Vertex vertex = svgVertices[i];
				vertex.position.x = vertex.position.x * vector4.x + vector3.x;
				vertex.position.y = vertex.position.y * vector4.y + vector3.y;
				vertex.uv.x = vertex.uv.x * uvRegion.width + uvRegion.xMin;
				vertex.uv.y = vertex.uv.y * uvRegion.height + uvRegion.yMin;
				vertex.tint *= tint;
				uint num10 = (uint)(((int)vertex.opacityPageSVGSettingIndex.b << 8 | (int)vertex.opacityPageSVGSettingIndex.a) + settingIndexOffset);
				vertex.opacityPageSVGSettingIndex.b = (byte)(num10 >> 8);
				vertex.opacityPageSVGSettingIndex.a = (byte)num10;
				meshWriteData.SetNextVertex(vertex);
			}
			for (int j = num9; j < num7; j++)
			{
				Vertex vertex2 = meshWriteData.m_Vertices[j];
				vertex2.position.x = vertex2.position.x * vector4.x + vector3.x;
				vertex2.position.y = vertex2.position.y * vector4.y + vector3.y;
				vertex2.uv.x = vertex2.uv.x * uvRegion.width + uvRegion.xMin;
				vertex2.uv.y = vertex2.uv.y * uvRegion.height + uvRegion.yMin;
				vertex2.tint *= tint;
				uint num11 = (uint)(((int)vertex2.opacityPageSVGSettingIndex.b << 8 | (int)vertex2.opacityPageSVGSettingIndex.a) + settingIndexOffset);
				vertex2.opacityPageSVGSettingIndex.b = (byte)(num11 >> 8);
				vertex2.opacityPageSVGSettingIndex.a = (byte)num11;
				meshWriteData.SetNextVertex(vertex2);
			}
			finalVertexCount = meshWriteData.vertexCount;
			finalIndexCount = meshWriteData.indexCount;
			MeshBuilder.s_VectorGraphicsStretch.End();
		}

		private static void MakeVectorGraphics9SliceBackground(Vertex[] svgVertices, ushort[] svgIndices, float svgWidth, float svgHeight, Rect targetRect, Vector4 sliceLTRB, bool stretch, Color tint, int settingIndexOffset, MeshBuilder.AllocMeshData meshAlloc)
		{
			MeshWriteData meshWriteData = meshAlloc.alloc((uint)svgVertices.Length, (uint)svgIndices.Length, ref meshAlloc);
			meshWriteData.SetAllIndices(svgIndices);
			bool flag = !stretch;
			if (flag)
			{
				throw new NotImplementedException("Support for repeating 9-slices is not done yet");
			}
			MeshBuilder.s_VectorGraphics9Slice.Begin();
			Rect uvRegion = meshWriteData.uvRegion;
			int num = svgVertices.Length;
			Vector2 vector = new Vector2(1f / (svgWidth - sliceLTRB.z - sliceLTRB.x), 1f / (svgHeight - sliceLTRB.w - sliceLTRB.y));
			Vector2 vector2 = new Vector2(targetRect.width - svgWidth, targetRect.height - svgHeight);
			for (int i = 0; i < num; i++)
			{
				Vertex vertex = svgVertices[i];
				Vector2 vector3;
				vector3.x = Mathf.Clamp01((vertex.position.x - sliceLTRB.x) * vector.x);
				vector3.y = Mathf.Clamp01((vertex.position.y - sliceLTRB.y) * vector.y);
				vertex.position.x = vertex.position.x + vector3.x * vector2.x;
				vertex.position.y = vertex.position.y + vector3.y * vector2.y;
				vertex.uv.x = vertex.uv.x * uvRegion.width + uvRegion.xMin;
				vertex.uv.y = vertex.uv.y * uvRegion.height + uvRegion.yMin;
				vertex.tint *= tint;
				uint num2 = (uint)(((int)vertex.opacityPageSVGSettingIndex.b << 8 | (int)vertex.opacityPageSVGSettingIndex.a) + settingIndexOffset);
				vertex.opacityPageSVGSettingIndex.b = (byte)(num2 >> 8);
				vertex.opacityPageSVGSettingIndex.a = (byte)num2;
				meshWriteData.SetNextVertex(vertex);
			}
			MeshBuilder.s_VectorGraphics9Slice.End();
		}

		private static MeshBuilder.ClipCounts UpperBoundApproximateRectClippingResults(Vertex[] vertices, ushort[] indices, Vector4 clipRectMinMax)
		{
			MeshBuilder.ClipCounts clipCounts = default(MeshBuilder.ClipCounts);
			clipCounts.firstClippedIndex = 2147483647;
			clipCounts.firstDegenerateIndex = -1;
			clipCounts.lastClippedIndex = -1;
			int num = indices.Length;
			for (int i = 0; i < num; i += 3)
			{
				Vector3 position = vertices[(int)indices[i]].position;
				Vector3 position2 = vertices[(int)indices[i + 1]].position;
				Vector3 position3 = vertices[(int)indices[i + 2]].position;
				Vector4 vector;
				vector.x = ((position.x < position2.x) ? position.x : position2.x);
				vector.x = ((vector.x < position3.x) ? vector.x : position3.x);
				vector.y = ((position.y < position2.y) ? position.y : position2.y);
				vector.y = ((vector.y < position3.y) ? vector.y : position3.y);
				vector.z = ((position.x > position2.x) ? position.x : position2.x);
				vector.z = ((vector.z > position3.x) ? vector.z : position3.x);
				vector.w = ((position.y > position2.y) ? position.y : position2.y);
				vector.w = ((vector.w > position3.y) ? vector.w : position3.y);
				bool flag = vector.x >= clipRectMinMax.x && vector.z <= clipRectMinMax.z && vector.y >= clipRectMinMax.y && vector.w <= clipRectMinMax.w;
				if (flag)
				{
					clipCounts.firstDegenerateIndex = -1;
				}
				else
				{
					clipCounts.firstClippedIndex = ((clipCounts.firstClippedIndex < i) ? clipCounts.firstClippedIndex : i);
					clipCounts.lastClippedIndex = i + 2;
					bool flag2 = vector.x >= clipRectMinMax.z || vector.z <= clipRectMinMax.x || vector.y >= clipRectMinMax.w || vector.w <= clipRectMinMax.y;
					if (flag2)
					{
						clipCounts.firstDegenerateIndex = ((clipCounts.firstDegenerateIndex == -1) ? i : clipCounts.firstDegenerateIndex);
						clipCounts.degenerateTriangles++;
					}
					clipCounts.firstDegenerateIndex = -1;
					clipCounts.clippedTriangles++;
					clipCounts.addedTriangles += 4;
				}
			}
			return clipCounts;
		}

		private unsafe static void RectClip(Vertex[] vertices, ushort[] indices, Vector4 clipRectMinMax, MeshWriteData mwd, MeshBuilder.ClipCounts cc, ref int newVertexCount)
		{
			int num = cc.lastClippedIndex;
			bool flag = cc.firstDegenerateIndex != -1 && cc.firstDegenerateIndex < num;
			if (flag)
			{
				num = cc.firstDegenerateIndex;
			}
			ushort num2 = (ushort)vertices.Length;
			for (int i = 0; i < cc.firstClippedIndex; i++)
			{
				mwd.SetNextIndex(indices[i]);
			}
			ushort* ptr = stackalloc ushort[3];
			Vertex* ptr2 = stackalloc Vertex[3];
			for (int j = cc.firstClippedIndex; j < num; j += 3)
			{
				*ptr = indices[j];
				ptr[1] = indices[j + 1];
				ptr[2] = indices[j + 2];
				*ptr2 = vertices[(int)(*ptr)];
				ptr2[1] = vertices[(int)ptr[1]];
				ptr2[2] = vertices[(int)ptr[2]];
				Vector4 vector;
				vector.x = ((ptr2->position.x < ptr2[1].position.x) ? ptr2->position.x : ptr2[1].position.x);
				vector.x = ((vector.x < ptr2[2].position.x) ? vector.x : ptr2[2].position.x);
				vector.y = ((ptr2->position.y < ptr2[1].position.y) ? ptr2->position.y : ptr2[1].position.y);
				vector.y = ((vector.y < ptr2[2].position.y) ? vector.y : ptr2[2].position.y);
				vector.z = ((ptr2->position.x > ptr2[1].position.x) ? ptr2->position.x : ptr2[1].position.x);
				vector.z = ((vector.z > ptr2[2].position.x) ? vector.z : ptr2[2].position.x);
				vector.w = ((ptr2->position.y > ptr2[1].position.y) ? ptr2->position.y : ptr2[1].position.y);
				vector.w = ((vector.w > ptr2[2].position.y) ? vector.w : ptr2[2].position.y);
				bool flag2 = vector.x >= clipRectMinMax.x && vector.z <= clipRectMinMax.z && vector.y >= clipRectMinMax.y && vector.w <= clipRectMinMax.w;
				if (flag2)
				{
					mwd.SetNextIndex(*ptr);
					mwd.SetNextIndex(ptr[1]);
					mwd.SetNextIndex(ptr[2]);
				}
				else
				{
					bool flag3 = vector.x >= clipRectMinMax.z || vector.z <= clipRectMinMax.x || vector.y >= clipRectMinMax.w || vector.w <= clipRectMinMax.y;
					if (!flag3)
					{
						MeshBuilder.RectClipTriangle(ptr2, ptr, clipRectMinMax, mwd, ref num2);
					}
				}
			}
			int num3 = indices.Length;
			for (int k = cc.lastClippedIndex + 1; k < num3; k++)
			{
				mwd.SetNextIndex(indices[k]);
			}
			newVertexCount = (int)num2;
			mwd.m_Vertices = mwd.m_Vertices.Slice(0, newVertexCount);
			mwd.m_Indices = mwd.m_Indices.Slice(0, mwd.currentIndex);
		}

		private unsafe static void RectClipTriangle(Vertex* vt, ushort* it, Vector4 clipRectMinMax, MeshWriteData mwd, ref ushort nextNewVertex)
		{
			Vertex* ptr = stackalloc Vertex[13];
			int num = 0;
			for (int i = 0; i < 3; i++)
			{
				bool flag = vt[i].position.x >= clipRectMinMax.x && vt[i].position.y >= clipRectMinMax.y && vt[i].position.x <= clipRectMinMax.z && vt[i].position.y <= clipRectMinMax.w;
				if (flag)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = vt[i];
				}
			}
			bool flag2 = num == 3;
			if (flag2)
			{
				mwd.SetNextIndex(*it);
				mwd.SetNextIndex(it[1]);
				mwd.SetNextIndex(it[2]);
			}
			else
			{
				Vector3 vertexBaryCentricCoordinates = MeshBuilder.GetVertexBaryCentricCoordinates(vt, clipRectMinMax.x, clipRectMinMax.y);
				Vector3 vertexBaryCentricCoordinates2 = MeshBuilder.GetVertexBaryCentricCoordinates(vt, clipRectMinMax.z, clipRectMinMax.y);
				Vector3 vertexBaryCentricCoordinates3 = MeshBuilder.GetVertexBaryCentricCoordinates(vt, clipRectMinMax.x, clipRectMinMax.w);
				Vector3 vertexBaryCentricCoordinates4 = MeshBuilder.GetVertexBaryCentricCoordinates(vt, clipRectMinMax.z, clipRectMinMax.w);
				bool flag3 = vertexBaryCentricCoordinates.x >= -1E-07f && vertexBaryCentricCoordinates.x <= 1.00000012f && vertexBaryCentricCoordinates.y >= -1E-07f && vertexBaryCentricCoordinates.y <= 1.00000012f && vertexBaryCentricCoordinates.z >= -1E-07f && vertexBaryCentricCoordinates.z <= 1.00000012f;
				if (flag3)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangle(vt, clipRectMinMax.x, clipRectMinMax.y, vertexBaryCentricCoordinates);
				}
				bool flag4 = vertexBaryCentricCoordinates2.x >= -1E-07f && vertexBaryCentricCoordinates2.x <= 1.00000012f && vertexBaryCentricCoordinates2.y >= -1E-07f && vertexBaryCentricCoordinates2.y <= 1.00000012f && vertexBaryCentricCoordinates2.z >= -1E-07f && vertexBaryCentricCoordinates2.z <= 1.00000012f;
				if (flag4)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangle(vt, clipRectMinMax.z, clipRectMinMax.y, vertexBaryCentricCoordinates2);
				}
				bool flag5 = vertexBaryCentricCoordinates3.x >= -1E-07f && vertexBaryCentricCoordinates3.x <= 1.00000012f && vertexBaryCentricCoordinates3.y >= -1E-07f && vertexBaryCentricCoordinates3.y <= 1.00000012f && vertexBaryCentricCoordinates3.z >= -1E-07f && vertexBaryCentricCoordinates3.z <= 1.00000012f;
				if (flag5)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangle(vt, clipRectMinMax.x, clipRectMinMax.w, vertexBaryCentricCoordinates3);
				}
				bool flag6 = vertexBaryCentricCoordinates4.x >= -1E-07f && vertexBaryCentricCoordinates4.x <= 1.00000012f && vertexBaryCentricCoordinates4.y >= -1E-07f && vertexBaryCentricCoordinates4.y <= 1.00000012f && vertexBaryCentricCoordinates4.z >= -1E-07f && vertexBaryCentricCoordinates4.z <= 1.00000012f;
				if (flag6)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangle(vt, clipRectMinMax.z, clipRectMinMax.w, vertexBaryCentricCoordinates4);
				}
				float num2 = MeshBuilder.IntersectSegments(vt->position.x, vt->position.y, vt[1].position.x, vt[1].position.y, clipRectMinMax.x, clipRectMinMax.y, clipRectMinMax.z, clipRectMinMax.y);
				bool flag7 = num2 != 3.40282347E+38f;
				if (flag7)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangleEdge(vt, 0, 1, num2);
				}
				num2 = MeshBuilder.IntersectSegments(vt[1].position.x, vt[1].position.y, vt[2].position.x, vt[2].position.y, clipRectMinMax.x, clipRectMinMax.y, clipRectMinMax.z, clipRectMinMax.y);
				bool flag8 = num2 != 3.40282347E+38f;
				if (flag8)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangleEdge(vt, 1, 2, num2);
				}
				num2 = MeshBuilder.IntersectSegments(vt[2].position.x, vt[2].position.y, vt->position.x, vt->position.y, clipRectMinMax.x, clipRectMinMax.y, clipRectMinMax.z, clipRectMinMax.y);
				bool flag9 = num2 != 3.40282347E+38f;
				if (flag9)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangleEdge(vt, 2, 0, num2);
				}
				num2 = MeshBuilder.IntersectSegments(vt->position.x, vt->position.y, vt[1].position.x, vt[1].position.y, clipRectMinMax.z, clipRectMinMax.y, clipRectMinMax.z, clipRectMinMax.w);
				bool flag10 = num2 != 3.40282347E+38f;
				if (flag10)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangleEdge(vt, 0, 1, num2);
				}
				num2 = MeshBuilder.IntersectSegments(vt[1].position.x, vt[1].position.y, vt[2].position.x, vt[2].position.y, clipRectMinMax.z, clipRectMinMax.y, clipRectMinMax.z, clipRectMinMax.w);
				bool flag11 = num2 != 3.40282347E+38f;
				if (flag11)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangleEdge(vt, 1, 2, num2);
				}
				num2 = MeshBuilder.IntersectSegments(vt[2].position.x, vt[2].position.y, vt->position.x, vt->position.y, clipRectMinMax.z, clipRectMinMax.y, clipRectMinMax.z, clipRectMinMax.w);
				bool flag12 = num2 != 3.40282347E+38f;
				if (flag12)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangleEdge(vt, 2, 0, num2);
				}
				num2 = MeshBuilder.IntersectSegments(vt->position.x, vt->position.y, vt[1].position.x, vt[1].position.y, clipRectMinMax.x, clipRectMinMax.w, clipRectMinMax.z, clipRectMinMax.w);
				bool flag13 = num2 != 3.40282347E+38f;
				if (flag13)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangleEdge(vt, 0, 1, num2);
				}
				num2 = MeshBuilder.IntersectSegments(vt[1].position.x, vt[1].position.y, vt[2].position.x, vt[2].position.y, clipRectMinMax.x, clipRectMinMax.w, clipRectMinMax.z, clipRectMinMax.w);
				bool flag14 = num2 != 3.40282347E+38f;
				if (flag14)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangleEdge(vt, 1, 2, num2);
				}
				num2 = MeshBuilder.IntersectSegments(vt[2].position.x, vt[2].position.y, vt->position.x, vt->position.y, clipRectMinMax.x, clipRectMinMax.w, clipRectMinMax.z, clipRectMinMax.w);
				bool flag15 = num2 != 3.40282347E+38f;
				if (flag15)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangleEdge(vt, 2, 0, num2);
				}
				num2 = MeshBuilder.IntersectSegments(vt->position.x, vt->position.y, vt[1].position.x, vt[1].position.y, clipRectMinMax.x, clipRectMinMax.y, clipRectMinMax.x, clipRectMinMax.w);
				bool flag16 = num2 != 3.40282347E+38f;
				if (flag16)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangleEdge(vt, 0, 1, num2);
				}
				num2 = MeshBuilder.IntersectSegments(vt[1].position.x, vt[1].position.y, vt[2].position.x, vt[2].position.y, clipRectMinMax.x, clipRectMinMax.y, clipRectMinMax.x, clipRectMinMax.w);
				bool flag17 = num2 != 3.40282347E+38f;
				if (flag17)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangleEdge(vt, 1, 2, num2);
				}
				num2 = MeshBuilder.IntersectSegments(vt[2].position.x, vt[2].position.y, vt->position.x, vt->position.y, clipRectMinMax.x, clipRectMinMax.y, clipRectMinMax.x, clipRectMinMax.w);
				bool flag18 = num2 != 3.40282347E+38f;
				if (flag18)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangleEdge(vt, 2, 0, num2);
				}
				bool flag19 = num == 0;
				if (!flag19)
				{
					float* ptr2 = stackalloc float[num];
					*ptr2 = 0f;
					for (int j = 1; j < num; j++)
					{
						ptr2[j] = Mathf.Atan2(ptr[j].position.y - ptr->position.y, ptr[j].position.x - ptr->position.x);
						bool flag20 = ptr2[j] < 0f;
						if (flag20)
						{
							ptr2[j] += 6.28318548f;
						}
					}
					int* ptr3 = stackalloc int[num];
					*ptr3 = 0;
					uint num3 = 0u;
					for (int k = 1; k < num; k++)
					{
						int num4 = -1;
						float num5 = 3.40282347E+38f;
						for (int l = 1; l < num; l++)
						{
							bool flag21 = ((ulong)num3 & (ulong)(1L << (l & 31))) == 0uL && ptr2[l] < num5;
							if (flag21)
							{
								num5 = ptr2[l];
								num4 = l;
							}
						}
						ptr3[k] = num4;
						num3 |= 1u << num4;
					}
					ushort num6 = nextNewVertex;
					for (int m = 0; m < num; m++)
					{
						mwd.m_Vertices[(int)num6 + m] = ptr[ptr3[m]];
					}
					nextNewVertex += (ushort)num;
					int num7 = num - 2;
					bool flag22 = false;
					Vector3 position = mwd.m_Vertices[(int)num6].position;
					for (int n = 0; n < num7; n++)
					{
						int num8 = (int)num6 + n + 1;
						int num9 = (int)num6 + n + 2;
						bool flag23 = !flag22;
						if (flag23)
						{
							float num10 = ptr2[ptr3[n + 1]];
							float num11 = ptr2[ptr3[n + 2]];
							bool flag24 = num11 - num10 >= 3.14159274f;
							if (flag24)
							{
								num8 = (int)(num6 + 1);
								num9 = (int)num6 + num - 1;
								flag22 = true;
							}
						}
						Vector3 position2 = mwd.m_Vertices[num8].position;
						Vector3 position3 = mwd.m_Vertices[num9].position;
						Vector3 vector = Vector3.Cross(position2 - position, position3 - position);
						mwd.SetNextIndex(num6);
						bool flag25 = vector.z < 0f;
						if (flag25)
						{
							mwd.SetNextIndex((ushort)num9);
							mwd.SetNextIndex((ushort)num8);
						}
						else
						{
							mwd.SetNextIndex((ushort)num8);
							mwd.SetNextIndex((ushort)num9);
						}
					}
				}
			}
		}

		private unsafe static Vector3 GetVertexBaryCentricCoordinates(Vertex* vt, float x, float y)
		{
			float num = vt[1].position.x - vt->position.x;
			float num2 = vt[1].position.y - vt->position.y;
			float num3 = vt[2].position.x - vt->position.x;
			float num4 = vt[2].position.y - vt->position.y;
			float num5 = x - vt->position.x;
			float num6 = y - vt->position.y;
			float num7 = num * num + num2 * num2;
			float num8 = num * num3 + num2 * num4;
			float num9 = num3 * num3 + num4 * num4;
			float num10 = num5 * num + num6 * num2;
			float num11 = num5 * num3 + num6 * num4;
			float num12 = num7 * num9 - num8 * num8;
			Vector3 vector;
			vector.y = (num9 * num10 - num8 * num11) / num12;
			vector.z = (num7 * num11 - num8 * num10) / num12;
			vector.x = 1f - vector.y - vector.z;
			return vector;
		}

		private unsafe static Vertex InterpolateVertexInTriangle(Vertex* vt, float x, float y, Vector3 uvw)
		{
			Vertex result = *vt;
			result.position.x = x;
			result.position.y = y;
			result.tint = vt->tint * uvw.x + vt[1].tint * uvw.y + vt[2].tint * uvw.z;
			result.uv = vt->uv * uvw.x + vt[1].uv * uvw.y + vt[2].uv * uvw.z;
			return result;
		}

		private unsafe static Vertex InterpolateVertexInTriangleEdge(Vertex* vt, int e0, int e1, float t)
		{
			Vertex result = *vt;
			result.position.x = vt[e0].position.x + t * (vt[e1].position.x - vt[e0].position.x);
			result.position.y = vt[e0].position.y + t * (vt[e1].position.y - vt[e0].position.y);
			result.tint = Color.LerpUnclamped(vt[e0].tint, vt[e1].tint, t);
			result.uv = Vector2.LerpUnclamped(vt[e0].uv, vt[e1].uv, t);
			return result;
		}

		private static float IntersectSegments(float ax, float ay, float bx, float by, float cx, float cy, float dx, float dy)
		{
			float num = (ax - dx) * (by - dy) - (ay - dy) * (bx - dx);
			float num2 = (ax - cx) * (by - cy) - (ay - cy) * (bx - cx);
			bool flag = num * num2 >= 0f;
			float result;
			if (flag)
			{
				result = 3.40282347E+38f;
			}
			else
			{
				float num3 = (cx - ax) * (dy - ay) - (cy - ay) * (dx - ax);
				float num4 = num3 + num2 - num;
				bool flag2 = num3 * num4 >= 0f;
				if (flag2)
				{
					result = 3.40282347E+38f;
				}
				else
				{
					result = num3 / (num3 - num4);
				}
			}
			return result;
		}
	}
}
