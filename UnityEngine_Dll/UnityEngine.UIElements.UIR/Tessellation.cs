using System;
using Unity.Collections;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	internal static class Tessellation
	{
		private enum TessellationType
		{
			EdgeHorizontal,
			EdgeVertical,
			EdgeCorner,
			Content
		}

		internal static float kEpsilon = 0.001f;

		internal static ushort kSubdivisions = 6;

		private static ProfilerMarker s_MarkerTessellateRect = new ProfilerMarker("TessellateRect");

		private static ProfilerMarker s_MarkerTessellateBorder = new ProfilerMarker("TessellateBorder");

		public static void TessellateRect(MeshGenerationContextUtils.RectangleParams rectParams, float posZ, MeshBuilder.AllocMeshData meshAlloc, bool computeUVs)
		{
			bool flag = rectParams.rect.width < Tessellation.kEpsilon || rectParams.rect.height < Tessellation.kEpsilon;
			if (!flag)
			{
				Tessellation.s_MarkerTessellateRect.Begin();
				Vector2 rhs = new Vector2(rectParams.rect.width * 0.5f, rectParams.rect.height * 0.5f);
				rectParams.topLeftRadius = Vector2.Min(rectParams.topLeftRadius, rhs);
				rectParams.topRightRadius = Vector2.Min(rectParams.topRightRadius, rhs);
				rectParams.bottomRightRadius = Vector2.Min(rectParams.bottomRightRadius, rhs);
				rectParams.bottomLeftRadius = Vector2.Min(rectParams.bottomLeftRadius, rhs);
				ushort num = 0;
				ushort num2 = 0;
				Tessellation.CountRectTriangles(ref rectParams, ref num, ref num2);
				MeshWriteData meshWriteData = meshAlloc.Allocate((uint)num, (uint)num2);
				num = 0;
				num2 = 0;
				Tessellation.TessellateRectInternal(ref rectParams, posZ, meshWriteData, ref num, ref num2, false);
				if (computeUVs)
				{
					Tessellation.ComputeUVs(rectParams.rect, rectParams.uv, meshWriteData.uvRegion, meshWriteData.m_Vertices);
				}
				Debug.Assert((int)num == meshWriteData.vertexCount);
				Debug.Assert((int)num2 == meshWriteData.indexCount);
				Tessellation.s_MarkerTessellateRect.End();
			}
		}

		public static void TessellateBorder(MeshGenerationContextUtils.BorderParams borderParams, float posZ, MeshBuilder.AllocMeshData meshAlloc)
		{
			bool flag = borderParams.rect.width < Tessellation.kEpsilon || borderParams.rect.height < Tessellation.kEpsilon;
			if (!flag)
			{
				Tessellation.s_MarkerTessellateBorder.Begin();
				Vector2 vector = new Vector2(borderParams.rect.width * 0.5f, borderParams.rect.height * 0.5f);
				borderParams.topLeftRadius = Vector2.Min(borderParams.topLeftRadius, vector);
				borderParams.topRightRadius = Vector2.Min(borderParams.topRightRadius, vector);
				borderParams.bottomRightRadius = Vector2.Min(borderParams.bottomRightRadius, vector);
				borderParams.bottomLeftRadius = Vector2.Min(borderParams.bottomLeftRadius, vector);
				borderParams.leftWidth = Mathf.Min(borderParams.leftWidth, vector.x);
				borderParams.topWidth = Mathf.Min(borderParams.topWidth, vector.y);
				borderParams.rightWidth = Mathf.Min(borderParams.rightWidth, vector.x);
				borderParams.bottomWidth = Mathf.Min(borderParams.bottomWidth, vector.y);
				ushort num = 0;
				ushort num2 = 0;
				Tessellation.CountBorderTriangles(ref borderParams, ref num, ref num2);
				MeshWriteData meshWriteData = meshAlloc.Allocate((uint)num, (uint)num2);
				num = 0;
				num2 = 0;
				Tessellation.TessellateBorderInternal(ref borderParams, posZ, meshWriteData, ref num, ref num2, false);
				Debug.Assert((int)num == meshWriteData.vertexCount);
				Debug.Assert((int)num2 == meshWriteData.indexCount);
				Tessellation.s_MarkerTessellateBorder.End();
			}
		}

		private static void CountRectTriangles(ref MeshGenerationContextUtils.RectangleParams rectParams, ref ushort vertexCount, ref ushort indexCount)
		{
			Tessellation.TessellateRectInternal(ref rectParams, 0f, null, ref vertexCount, ref indexCount, true);
		}

		private static void CountBorderTriangles(ref MeshGenerationContextUtils.BorderParams border, ref ushort vertexCount, ref ushort indexCount)
		{
			Tessellation.TessellateBorderInternal(ref border, 0f, null, ref vertexCount, ref indexCount, true);
		}

		private static void TessellateRectInternal(ref MeshGenerationContextUtils.RectangleParams rectParams, float posZ, MeshWriteData mesh, ref ushort vertexCount, ref ushort indexCount, bool countOnly = false)
		{
			bool flag = !rectParams.HasRadius(Tessellation.kEpsilon);
			if (flag)
			{
				Tessellation.TessellateQuad(rectParams.rect, 0f, 0f, 0f, Tessellation.TessellationType.Content, rectParams.color, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
			}
			else
			{
				Tessellation.TessellateRoundedCorners(ref rectParams, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
			}
		}

		private static void TessellateBorderInternal(ref MeshGenerationContextUtils.BorderParams border, float posZ, MeshWriteData mesh, ref ushort vertexCount, ref ushort indexCount, bool countOnly = false)
		{
			Tessellation.TessellateRoundedBorders(ref border, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
		}

		private static void TessellateRoundedCorners(ref MeshGenerationContextUtils.RectangleParams rectParams, float posZ, MeshWriteData mesh, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			Vector2 vector = new Vector2(rectParams.rect.width * 0.5f, rectParams.rect.height * 0.5f);
			Rect rect = new Rect(rectParams.rect.x, rectParams.rect.y, vector.x, vector.y);
			Tessellation.TessellateRoundedCorner(rect, rectParams.color, posZ, rectParams.topLeftRadius, mesh, ref vertexCount, ref indexCount, countOnly);
			ushort num = vertexCount;
			ushort num2 = indexCount;
			Tessellation.TessellateRoundedCorner(rect, rectParams.color, posZ, rectParams.topRightRadius, mesh, ref vertexCount, ref indexCount, countOnly);
			bool flag = !countOnly;
			if (flag)
			{
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), true);
				Tessellation.FlipWinding(mesh.m_Indices, (int)num2, (int)(indexCount - num2));
			}
			num = vertexCount;
			num2 = indexCount;
			Tessellation.TessellateRoundedCorner(rect, rectParams.color, posZ, rectParams.bottomRightRadius, mesh, ref vertexCount, ref indexCount, countOnly);
			bool flag2 = !countOnly;
			if (flag2)
			{
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), true);
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), false);
			}
			num = vertexCount;
			num2 = indexCount;
			Tessellation.TessellateRoundedCorner(rect, rectParams.color, posZ, rectParams.bottomLeftRadius, mesh, ref vertexCount, ref indexCount, countOnly);
			bool flag3 = !countOnly;
			if (flag3)
			{
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), false);
				Tessellation.FlipWinding(mesh.m_Indices, (int)num2, (int)(indexCount - num2));
			}
		}

		private static void TessellateRoundedBorders(ref MeshGenerationContextUtils.BorderParams border, float posZ, MeshWriteData mesh, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			Vector2 vector = new Vector2(border.rect.width * 0.5f, border.rect.height * 0.5f);
			Rect rect = new Rect(border.rect.x, border.rect.y, vector.x, vector.y);
			Color32 leftColor = border.leftColor;
			Color32 topColor = border.topColor;
			Color32 topColor2 = border.bottomColor;
			Color32 leftColor2 = border.rightColor;
			Tessellation.TessellateRoundedBorder(rect, leftColor, topColor, posZ, border.topLeftRadius, border.leftWidth, border.topWidth, mesh, ref vertexCount, ref indexCount, countOnly);
			ushort num = vertexCount;
			ushort num2 = indexCount;
			Tessellation.TessellateRoundedBorder(rect, leftColor2, topColor, posZ, border.topRightRadius, border.rightWidth, border.topWidth, mesh, ref vertexCount, ref indexCount, countOnly);
			bool flag = !countOnly;
			if (flag)
			{
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), true);
				Tessellation.FlipWinding(mesh.m_Indices, (int)num2, (int)(indexCount - num2));
			}
			num = vertexCount;
			num2 = indexCount;
			Tessellation.TessellateRoundedBorder(rect, leftColor2, topColor2, posZ, border.bottomRightRadius, border.rightWidth, border.bottomWidth, mesh, ref vertexCount, ref indexCount, countOnly);
			bool flag2 = !countOnly;
			if (flag2)
			{
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), true);
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), false);
			}
			num = vertexCount;
			num2 = indexCount;
			Tessellation.TessellateRoundedBorder(rect, leftColor, topColor2, posZ, border.bottomLeftRadius, border.leftWidth, border.bottomWidth, mesh, ref vertexCount, ref indexCount, countOnly);
			bool flag3 = !countOnly;
			if (flag3)
			{
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), false);
				Tessellation.FlipWinding(mesh.m_Indices, (int)num2, (int)(indexCount - num2));
			}
		}

		private static void TessellateRoundedCorner(Rect rect, Color32 color, float posZ, Vector2 radius, MeshWriteData mesh, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			Vector2 center = rect.position + radius;
			Rect zero = Rect.zero;
			bool flag = radius == Vector2.zero;
			if (flag)
			{
				Tessellation.TessellateQuad(rect, 0f, 0f, 0f, Tessellation.TessellationType.Content, color, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
			}
			else
			{
				Tessellation.TessellateFilledFan(Tessellation.TessellationType.Content, center, radius, 0f, 0f, color, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
				bool flag2 = radius.x < rect.width;
				if (flag2)
				{
					zero = new Rect(rect.x + radius.x, rect.y, rect.width - radius.x, rect.height);
					Tessellation.TessellateQuad(zero, 0f, 0f, 0f, Tessellation.TessellationType.Content, color, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
				}
				bool flag3 = radius.y < rect.height;
				if (flag3)
				{
					zero = new Rect(rect.x, rect.y + radius.y, (radius.x < rect.width) ? radius.x : rect.width, rect.height - radius.y);
					Tessellation.TessellateQuad(zero, 0f, 0f, 0f, Tessellation.TessellationType.Content, color, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
				}
			}
		}

		private static void TessellateRoundedBorder(Rect rect, Color32 leftColor, Color32 topColor, float posZ, Vector2 radius, float leftWidth, float topWidth, MeshWriteData mesh, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			bool flag = leftWidth < Tessellation.kEpsilon && topWidth < Tessellation.kEpsilon;
			if (!flag)
			{
				leftWidth = Mathf.Max(0f, leftWidth);
				topWidth = Mathf.Max(0f, topWidth);
				radius.x = Mathf.Clamp(radius.x, 0f, rect.width);
				radius.y = Mathf.Clamp(radius.y, 0f, rect.height);
				Vector2 center = rect.position + radius;
				Rect zero = Rect.zero;
				bool flag2 = radius.x < Tessellation.kEpsilon || radius.y < Tessellation.kEpsilon;
				if (flag2)
				{
					bool flag3 = leftWidth > Tessellation.kEpsilon;
					if (flag3)
					{
						zero = new Rect(rect.x, rect.y, leftWidth, rect.height);
						Tessellation.TessellateQuad(zero, topWidth, leftWidth, topWidth, Tessellation.TessellationType.EdgeVertical, leftColor, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
					}
					bool flag4 = topWidth > Tessellation.kEpsilon;
					if (flag4)
					{
						zero = new Rect(rect.x, rect.y, rect.width, topWidth);
						Tessellation.TessellateQuad(zero, leftWidth, leftWidth, topWidth, Tessellation.TessellationType.EdgeHorizontal, topColor, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
					}
				}
				else
				{
					bool flag5 = Tessellation.LooseCompare(radius.x, leftWidth) == 0 && Tessellation.LooseCompare(radius.y, topWidth) == 0;
					if (flag5)
					{
						bool flag6 = leftColor.InternalEquals(topColor);
						if (flag6)
						{
							Tessellation.TessellateFilledFan(Tessellation.TessellationType.EdgeCorner, center, radius, leftWidth, topWidth, leftColor, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
						}
						else
						{
							Tessellation.TessellateFilledFan(center, radius, leftWidth, topWidth, leftColor, topColor, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
						}
					}
					else
					{
						bool flag7 = Tessellation.LooseCompare(radius.x, leftWidth) > 0 && Tessellation.LooseCompare(radius.y, topWidth) > 0;
						if (flag7)
						{
							bool flag8 = leftColor.InternalEquals(topColor);
							if (flag8)
							{
								Tessellation.TessellateBorderedFan(center, radius, leftWidth, topWidth, leftColor, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
							}
							else
							{
								Tessellation.TessellateBorderedFan(center, radius, leftWidth, topWidth, leftColor, topColor, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
							}
						}
						else
						{
							zero = new Rect(rect.x, rect.y, Mathf.Max(radius.x, leftWidth), Mathf.Max(radius.y, topWidth));
							bool flag9 = leftColor.InternalEquals(topColor);
							if (flag9)
							{
								Tessellation.TessellateComplexBorderCorner(zero, radius, leftWidth, topWidth, leftColor, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
							}
							else
							{
								Tessellation.TessellateComplexBorderCorner(zero, radius, leftWidth, topWidth, leftColor, topColor, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
							}
						}
					}
					float num = Mathf.Max(radius.y, topWidth);
					zero = new Rect(rect.x, rect.y + num, leftWidth, rect.height - num);
					Tessellation.TessellateQuad(zero, 0f, leftWidth, topWidth, Tessellation.TessellationType.EdgeVertical, leftColor, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
					num = Mathf.Max(radius.x, leftWidth);
					zero = new Rect(rect.x + num, rect.y, rect.width - num, topWidth);
					Tessellation.TessellateQuad(zero, 0f, leftWidth, topWidth, Tessellation.TessellationType.EdgeHorizontal, topColor, posZ, mesh, ref vertexCount, ref indexCount, countOnly);
				}
			}
		}

		private static Vector2 IntersectEllipseWithLine(float a, float b, Vector2 dir)
		{
			Debug.Assert(dir.x > 0f || dir.y > 0f);
			bool flag = a < Mathf.Epsilon || b < Mathf.Epsilon;
			Vector2 result;
			if (flag)
			{
				result = new Vector2(0f, 0f);
			}
			else
			{
				bool flag2 = (double)dir.y < 0.001 * (double)dir.x;
				if (flag2)
				{
					result = new Vector2(a, 0f);
				}
				else
				{
					bool flag3 = (double)dir.x < 0.001 * (double)dir.y;
					if (flag3)
					{
						result = new Vector2(0f, b);
					}
					else
					{
						float num = dir.y / dir.x;
						float num2 = b / a;
						float num3 = b * (num2 + num - Mathf.Sqrt(2f * num * num2)) / (num * num + num2 * num2);
						float y = num * num3;
						result = new Vector2(num3, y);
					}
				}
			}
			return result;
		}

		private static float GetCenteredEllipseLineIntersectionTheta(float a, float b, Vector2 dir)
		{
			return Mathf.Atan2(dir.y * a, dir.x * b);
		}

		private static Vector2 IntersectLines(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
		{
			Vector2 vector = p3 - p2;
			Vector2 vector2 = p2 - p0;
			Vector2 vector3 = p1 - p0;
			float num = vector.x * vector3.y - vector3.x * vector.y;
			bool flag = Mathf.Approximately(num, 0f);
			Vector2 result;
			if (flag)
			{
				result = new Vector2(float.NaN, float.NaN);
			}
			else
			{
				float num2 = vector.x * vector2.y - vector2.x * vector.y;
				float d = num2 / num;
				Vector2 vector4 = p0 + vector3 * d;
				result = vector4;
			}
			return result;
		}

		private static int LooseCompare(float a, float b)
		{
			bool flag = a < b - Tessellation.kEpsilon;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				bool flag2 = a > b + Tessellation.kEpsilon;
				if (flag2)
				{
					result = 1;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		private static void TessellateComplexBorderCorner(Rect rect, Vector2 radius, float leftWidth, float topWidth, Color32 color, float posZ, MeshWriteData mesh, ref ushort refVertexCount, ref ushort refIndexCount, bool countOnly)
		{
			bool flag = rect.width < Tessellation.kEpsilon || rect.height < Tessellation.kEpsilon;
			if (!flag)
			{
				int num = Tessellation.LooseCompare(leftWidth, radius.x);
				int num2 = Tessellation.LooseCompare(topWidth, radius.y);
				Debug.Assert(num != num2 || (num > 0 && num2 > 0));
				ushort num3 = refVertexCount;
				ushort num4 = refIndexCount;
				int num5 = (int)(Tessellation.kSubdivisions - 1);
				if (countOnly)
				{
					int num6 = num5;
					bool flag2 = num2 != 0;
					if (flag2)
					{
						num6++;
					}
					bool flag3 = num != 0;
					if (flag3)
					{
						num6++;
					}
					num3 += (ushort)(num6 + 3);
					num4 += (ushort)(num6 * 3);
					refIndexCount = num4;
					refVertexCount = num3;
				}
				else
				{
					Color32 idsFlags = new Color32(0, 0, 0, 5);
					Color32 idsFlags2 = new Color32(0, 0, 0, 0);
					Vector2 uv = new Vector2(leftWidth, topWidth);
					ushort nextIndex = num3;
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(leftWidth, topWidth, posZ),
						tint = color,
						uv = uv,
						idsFlags = idsFlags
					});
					num3 += 1;
					ushort nextIndex2 = num3;
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(leftWidth, topWidth, posZ),
						tint = color,
						uv = uv,
						idsFlags = idsFlags
					});
					num3 += 1;
					bool flag4 = num2 < 0;
					if (flag4)
					{
						mesh.SetNextVertex(new Vertex
						{
							position = new Vector3(rect.xMax, rect.yMax, posZ),
							tint = color,
							uv = uv,
							idsFlags = idsFlags
						});
						mesh.SetNextVertex(new Vertex
						{
							position = new Vector3(0f, rect.yMax, posZ),
							tint = color,
							idsFlags = idsFlags2
						});
						num3 += 2;
						mesh.SetNextIndex(nextIndex2);
						mesh.SetNextIndex(num3 - 2);
						mesh.SetNextIndex(num3 - 1);
						num4 += 3;
					}
					else
					{
						mesh.SetNextVertex(new Vertex
						{
							position = new Vector3(0f, rect.yMax, posZ),
							tint = color,
							idsFlags = idsFlags2
						});
						num3 += 1;
					}
					bool flag5 = num2 > 0;
					if (flag5)
					{
						mesh.SetNextVertex(new Vertex
						{
							position = new Vector3(0f, radius.y, posZ),
							tint = color,
							idsFlags = idsFlags2
						});
						num3 += 1;
						mesh.SetNextIndex(nextIndex2);
						mesh.SetNextIndex(num3 - 2);
						mesh.SetNextIndex(num3 - 1);
						num4 += 3;
					}
					float num7 = 1.57079637f / (float)num5;
					for (int i = 1; i < num5; i++)
					{
						float f = (float)i * num7;
						Vector2 vector = new Vector2(radius.x - Mathf.Cos(f) * radius.x, radius.y - Mathf.Sin(f) * radius.y);
						mesh.SetNextVertex(new Vertex
						{
							position = new Vector3(vector.x, vector.y, posZ),
							tint = color,
							idsFlags = idsFlags2
						});
						num3 += 1;
						mesh.SetNextIndex(nextIndex2);
						mesh.SetNextIndex(num3 - 2);
						mesh.SetNextIndex(num3 - 1);
						num4 += 3;
					}
					bool flag6 = num > 0;
					if (flag6)
					{
						mesh.SetNextVertex(new Vertex
						{
							position = new Vector3(radius.x, 0f, posZ),
							tint = color,
							idsFlags = idsFlags2
						});
						num3 += 1;
						mesh.SetNextIndex(nextIndex2);
						mesh.SetNextIndex(num3 - 2);
						mesh.SetNextIndex(num3 - 1);
						num4 += 3;
					}
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(rect.xMax, 0f, posZ),
						tint = color,
						idsFlags = idsFlags2
					});
					num3 += 1;
					mesh.SetNextIndex(nextIndex);
					mesh.SetNextIndex(num3 - 2);
					mesh.SetNextIndex(num3 - 1);
					num4 += 3;
					bool flag7 = num < 0;
					if (flag7)
					{
						mesh.SetNextVertex(new Vertex
						{
							position = new Vector3(rect.xMax, rect.yMax, posZ),
							tint = color,
							uv = uv,
							idsFlags = idsFlags
						});
						num3 += 1;
						mesh.SetNextIndex(nextIndex);
						mesh.SetNextIndex(num3 - 2);
						mesh.SetNextIndex(num3 - 1);
						num4 += 3;
					}
					refIndexCount = num4;
					refVertexCount = num3;
				}
			}
		}

		private static void TessellateComplexBorderCorner(Rect rect, Vector2 radius, float leftWidth, float topWidth, Color32 leftColor, Color32 topColor, float posZ, MeshWriteData mesh, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			bool flag = rect.width < Tessellation.kEpsilon || rect.height < Tessellation.kEpsilon;
			if (!flag)
			{
				int num = Tessellation.LooseCompare(leftWidth, radius.x);
				int num2 = Tessellation.LooseCompare(topWidth, radius.y);
				Debug.Assert(num != num2 || (num > 0 && num2 > 0));
				if (countOnly)
				{
					vertexCount += Tessellation.kSubdivisions;
					vertexCount += 2;
					vertexCount += 3;
					int num3 = 2;
					num3 += (int)(Tessellation.kSubdivisions - 1);
					bool flag2 = num != 0;
					if (flag2)
					{
						vertexCount += 1;
						num3++;
					}
					bool flag3 = num2 != 0;
					if (flag3)
					{
						vertexCount += 1;
						num3++;
					}
					indexCount += (ushort)(num3 * 3);
				}
				else
				{
					Vector2 vector = new Vector2(rect.x + leftWidth, rect.y + topWidth);
					Vector2 vector2 = new Vector2(rect.x, rect.y);
					Vector2 vector3 = new Vector2(rect.x, rect.y + radius.y);
					Vector2 vector4 = new Vector2(rect.x + radius.x, rect.y);
					Vector2 a = new Vector2(vector4.x, vector3.y);
					Vector2 vector5 = Tessellation.IntersectLines(vector3, vector4, vector, vector2);
					Vector2 vector6 = Tessellation.IntersectEllipseWithLine(radius.x, radius.y, vector - vector2);
					Vector2 vector7 = new Vector2(rect.xMax, rect.y);
					Vector2 vector8 = new Vector2(rect.x, rect.yMax);
					Vector2 vector9 = new Vector2(rect.xMax, rect.yMax);
					float centeredEllipseLineIntersectionTheta = Tessellation.GetCenteredEllipseLineIntersectionTheta(radius.x, radius.y, radius - vector6);
					vector6.x += rect.x;
					vector6.y += rect.y;
					int num4 = (int)(Tessellation.kSubdivisions - 1);
					int num5 = Mathf.Clamp(Mathf.RoundToInt(centeredEllipseLineIntersectionTheta / 1.57079637f * (float)num4), 1, num4 - 1);
					int num6 = num4 - num5;
					Color32 idsFlags = new Color32(0, 0, 0, 5);
					Color32 idsFlags2 = new Color32(0, 0, 0, 0);
					Vector2 uv = new Vector2(leftWidth, topWidth);
					ushort nextIndex = vertexCount;
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector5.x, vector5.y, posZ),
						tint = leftColor,
						idsFlags = idsFlags2
					});
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector.x, vector.y, posZ),
						tint = leftColor,
						uv = uv,
						idsFlags = idsFlags
					});
					vertexCount += 2;
					bool flag4 = num2 < 0;
					if (flag4)
					{
						mesh.SetNextVertex(new Vertex
						{
							position = new Vector3(vector9.x, vector9.y, posZ),
							tint = leftColor,
							uv = uv,
							idsFlags = idsFlags
						});
						vertexCount += 1;
						mesh.SetNextIndex(nextIndex);
						mesh.SetNextIndex(vertexCount - 2);
						mesh.SetNextIndex(vertexCount - 1);
						indexCount += 3;
					}
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector8.x, vector8.y, posZ),
						tint = leftColor,
						idsFlags = idsFlags2
					});
					vertexCount += 1;
					mesh.SetNextIndex(nextIndex);
					mesh.SetNextIndex(vertexCount - 2);
					mesh.SetNextIndex(vertexCount - 1);
					indexCount += 3;
					bool flag5 = num2 > 0;
					if (flag5)
					{
						mesh.SetNextVertex(new Vertex
						{
							position = new Vector3(vector3.x, vector3.y, posZ),
							tint = leftColor,
							idsFlags = idsFlags2
						});
						vertexCount += 1;
						mesh.SetNextIndex(nextIndex);
						mesh.SetNextIndex(vertexCount - 2);
						mesh.SetNextIndex(vertexCount - 1);
						indexCount += 3;
					}
					float num7 = centeredEllipseLineIntersectionTheta / (float)num5;
					for (int i = 1; i < num5; i++)
					{
						float f = (float)i * num7;
						Vector2 vector10 = a - new Vector2(Mathf.Cos(f), Mathf.Sin(f)) * radius;
						mesh.SetNextVertex(new Vertex
						{
							position = new Vector3(vector10.x, vector10.y, posZ),
							tint = leftColor,
							idsFlags = idsFlags2
						});
						vertexCount += 1;
						mesh.SetNextIndex(nextIndex);
						mesh.SetNextIndex(vertexCount - 2);
						mesh.SetNextIndex(vertexCount - 1);
						indexCount += 3;
					}
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector6.x, vector6.y, posZ),
						tint = leftColor,
						idsFlags = idsFlags2
					});
					vertexCount += 1;
					mesh.SetNextIndex(nextIndex);
					mesh.SetNextIndex(vertexCount - 2);
					mesh.SetNextIndex(vertexCount - 1);
					indexCount += 3;
					ushort nextIndex2 = vertexCount;
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector5.x, vector5.y, posZ),
						tint = topColor,
						idsFlags = idsFlags2
					});
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector6.x, vector6.y, posZ),
						tint = topColor,
						idsFlags = idsFlags2
					});
					vertexCount += 2;
					float num8 = (1.57079637f - centeredEllipseLineIntersectionTheta) / (float)num6;
					for (int j = 1; j < num6; j++)
					{
						float f2 = centeredEllipseLineIntersectionTheta + (float)j * num8;
						Vector2 vector11 = a - new Vector2(Mathf.Cos(f2), Mathf.Sin(f2)) * radius;
						mesh.SetNextVertex(new Vertex
						{
							position = new Vector3(vector11.x, vector11.y, posZ),
							tint = topColor,
							idsFlags = idsFlags2
						});
						vertexCount += 1;
						mesh.SetNextIndex(nextIndex2);
						mesh.SetNextIndex(vertexCount - 2);
						mesh.SetNextIndex(vertexCount - 1);
						indexCount += 3;
					}
					bool flag6 = num > 0;
					if (flag6)
					{
						mesh.SetNextVertex(new Vertex
						{
							position = new Vector3(vector4.x, vector4.y, posZ),
							tint = topColor,
							idsFlags = idsFlags2
						});
						vertexCount += 1;
						mesh.SetNextIndex(nextIndex2);
						mesh.SetNextIndex(vertexCount - 2);
						mesh.SetNextIndex(vertexCount - 1);
						indexCount += 3;
					}
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector7.x, vector7.y, posZ),
						tint = topColor,
						idsFlags = idsFlags2
					});
					vertexCount += 1;
					mesh.SetNextIndex(nextIndex2);
					mesh.SetNextIndex(vertexCount - 2);
					mesh.SetNextIndex(vertexCount - 1);
					indexCount += 3;
					bool flag7 = num < 0;
					if (flag7)
					{
						mesh.SetNextVertex(new Vertex
						{
							position = new Vector3(vector9.x, vector9.y, posZ),
							tint = topColor,
							uv = uv,
							idsFlags = idsFlags
						});
						vertexCount += 1;
						mesh.SetNextIndex(nextIndex2);
						mesh.SetNextIndex(vertexCount - 2);
						mesh.SetNextIndex(vertexCount - 1);
						indexCount += 3;
					}
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector.x, vector.y, posZ),
						tint = topColor,
						uv = uv,
						idsFlags = idsFlags
					});
					vertexCount += 1;
					mesh.SetNextIndex(nextIndex2);
					mesh.SetNextIndex(vertexCount - 2);
					mesh.SetNextIndex(vertexCount - 1);
					indexCount += 3;
				}
			}
		}

		private static void TessellateQuad(Rect rect, float miterOffset, float leftWidth, float topWidth, Tessellation.TessellationType tessellationType, Color32 color, float posZ, MeshWriteData mesh, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			bool flag = (rect.width < Tessellation.kEpsilon || rect.height < Tessellation.kEpsilon) && tessellationType != Tessellation.TessellationType.EdgeHorizontal && tessellationType != Tessellation.TessellationType.EdgeVertical;
			if (!flag)
			{
				if (countOnly)
				{
					vertexCount += 4;
					indexCount += 6;
				}
				else
				{
					Vector3 position = new Vector3(rect.x, rect.y, posZ);
					Vector3 position2 = new Vector3(rect.xMax, rect.y, posZ);
					Vector3 position3 = new Vector3(rect.x, rect.yMax, posZ);
					Vector3 position4 = new Vector3(rect.xMax, rect.yMax, posZ);
					Vector2 vector = new Vector2(leftWidth, topWidth);
					Vector2 uv2;
					Vector2 uv;
					Vector2 uv4;
					Vector2 uv3;
					Color32 color2;
					Color32 idsFlags;
					Color32 color3;
					Color32 color4;
					switch (tessellationType)
					{
					case Tessellation.TessellationType.EdgeHorizontal:
						position3.x += miterOffset;
						uv = (uv2 = Vector2.zero);
						uv3 = (uv4 = vector);
						color2 = new Color32(0, 0, 0, 0);
						idsFlags = color2;
						color3 = new Color32(0, 0, 0, 5);
						color4 = new Color32(0, 0, 0, 6);
						break;
					case Tessellation.TessellationType.EdgeVertical:
						position2.y += miterOffset;
						uv4 = (uv2 = Vector2.zero);
						uv3 = (uv = vector);
						color3 = new Color32(0, 0, 0, 0);
						idsFlags = color3;
						color2 = new Color32(0, 0, 0, 5);
						color4 = new Color32(0, 0, 0, 7);
						break;
					case Tessellation.TessellationType.EdgeCorner:
						uv = (uv2 = (uv4 = (uv3 = Vector2.zero)));
						color4 = new Color32(0, 0, 0, 0);
						color2 = (idsFlags = (color3 = color4));
						break;
					case Tessellation.TessellationType.Content:
						uv = (uv2 = (uv4 = (uv3 = Vector2.zero)));
						color4 = new Color32(0, 0, 0, 0);
						color2 = (idsFlags = (color3 = color4));
						break;
					default:
						throw new NotImplementedException();
					}
					mesh.SetNextVertex(new Vertex
					{
						position = position,
						uv = uv2,
						tint = color,
						idsFlags = idsFlags
					});
					mesh.SetNextVertex(new Vertex
					{
						position = position2,
						uv = uv,
						tint = color,
						idsFlags = color2
					});
					mesh.SetNextVertex(new Vertex
					{
						position = position3,
						uv = uv4,
						tint = color,
						idsFlags = color3
					});
					mesh.SetNextVertex(new Vertex
					{
						position = position4,
						uv = uv3,
						tint = color,
						idsFlags = color4
					});
					mesh.SetNextIndex(vertexCount);
					mesh.SetNextIndex(vertexCount + 1);
					mesh.SetNextIndex(vertexCount + 2);
					mesh.SetNextIndex(vertexCount + 3);
					mesh.SetNextIndex(vertexCount + 2);
					mesh.SetNextIndex(vertexCount + 1);
					vertexCount += 4;
					indexCount += 6;
				}
			}
		}

		private static void TessellateFilledFan(Vector2 center, Vector2 radius, float leftWidth, float topWidth, Color32 leftColor, Color32 topColor, float posZ, MeshWriteData mesh, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			if (countOnly)
			{
				vertexCount += Tessellation.kSubdivisions + 3;
				indexCount += (Tessellation.kSubdivisions - 1) * 3;
			}
			else
			{
				Color32 idsFlags = new Color32(0, 0, 0, 5);
				Color32 idsFlags2 = new Color32(0, 0, 0, 0);
				Vector2 uv = new Vector2(leftWidth, topWidth);
				float centeredEllipseLineIntersectionTheta = Tessellation.GetCenteredEllipseLineIntersectionTheta(radius.x, radius.y, radius);
				int num = (int)(Tessellation.kSubdivisions - 1);
				int num2 = Mathf.Clamp(Mathf.RoundToInt(centeredEllipseLineIntersectionTheta / 1.57079637f * (float)num), 1, num - 1);
				int num3 = num - num2;
				ushort nextIndex = vertexCount;
				Vector2 vector = new Vector2(center.x - radius.x, center.y);
				mesh.SetNextVertex(new Vertex
				{
					position = new Vector3(center.x, center.y, posZ),
					tint = leftColor,
					idsFlags = idsFlags,
					uv = uv
				});
				mesh.SetNextVertex(new Vertex
				{
					position = new Vector3(vector.x, vector.y, posZ),
					tint = leftColor,
					idsFlags = idsFlags2
				});
				vertexCount += 2;
				float num4 = centeredEllipseLineIntersectionTheta / (float)num2;
				for (int i = 1; i <= num2; i++)
				{
					float f = num4 * (float)i;
					vector = center - new Vector2(Mathf.Cos(f), Mathf.Sin(f)) * radius;
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector.x, vector.y, posZ),
						tint = leftColor,
						idsFlags = idsFlags2
					});
					vertexCount += 1;
					mesh.SetNextIndex(nextIndex);
					mesh.SetNextIndex(vertexCount - 2);
					mesh.SetNextIndex(vertexCount - 1);
					indexCount += 3;
				}
				ushort nextIndex2 = vertexCount;
				mesh.SetNextVertex(new Vertex
				{
					position = new Vector3(center.x, center.y, posZ),
					tint = topColor,
					idsFlags = idsFlags,
					uv = uv
				});
				mesh.SetNextVertex(new Vertex
				{
					position = new Vector3(vector.x, vector.y, posZ),
					tint = topColor,
					idsFlags = idsFlags2
				});
				vertexCount += 2;
				float num5 = (1.57079637f - centeredEllipseLineIntersectionTheta) / (float)num3;
				for (int j = 1; j <= num3; j++)
				{
					float f2 = centeredEllipseLineIntersectionTheta + num5 * (float)j;
					vector = center - new Vector2(Mathf.Cos(f2), Mathf.Sin(f2)) * radius;
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector.x, vector.y, posZ),
						tint = topColor,
						idsFlags = idsFlags2
					});
					vertexCount += 1;
					mesh.SetNextIndex(nextIndex2);
					mesh.SetNextIndex(vertexCount - 2);
					mesh.SetNextIndex(vertexCount - 1);
					indexCount += 3;
				}
			}
		}

		private static void TessellateFilledFan(Tessellation.TessellationType tessellationType, Vector2 center, Vector2 radius, float leftWidth, float topWidth, Color32 color, float posZ, MeshWriteData mesh, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			if (countOnly)
			{
				vertexCount += Tessellation.kSubdivisions + 1;
				indexCount += (Tessellation.kSubdivisions - 1) * 3;
			}
			else
			{
				bool flag = tessellationType == Tessellation.TessellationType.EdgeCorner;
				Color32 color2;
				Color32 idsFlags;
				if (flag)
				{
					color2 = new Color32(0, 0, 0, 5);
					idsFlags = new Color32(0, 0, 0, 0);
				}
				else
				{
					color2 = new Color32(0, 0, 0, 0);
					idsFlags = color2;
				}
				Vector2 uv = new Vector2(leftWidth, topWidth);
				Vector2 vector = new Vector2(center.x - radius.x, center.y);
				ushort num = vertexCount;
				mesh.SetNextVertex(new Vertex
				{
					position = new Vector3(center.x, center.y, posZ),
					tint = color,
					idsFlags = color2,
					uv = uv
				});
				mesh.SetNextVertex(new Vertex
				{
					position = new Vector3(vector.x, vector.y, posZ),
					tint = color,
					idsFlags = idsFlags
				});
				vertexCount += 2;
				for (int i = 1; i < (int)Tessellation.kSubdivisions; i++)
				{
					float f = 1.57079637f * (float)i / (float)(Tessellation.kSubdivisions - 1);
					vector = center + new Vector2(-Mathf.Cos(f), -Mathf.Sin(f)) * radius;
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector.x, vector.y, posZ),
						tint = color,
						idsFlags = idsFlags
					});
					vertexCount += 1;
					mesh.SetNextIndex(num);
					mesh.SetNextIndex((ushort)((int)num + i));
					mesh.SetNextIndex((ushort)((int)num + i + 1));
					indexCount += 3;
				}
				num += Tessellation.kSubdivisions + 1;
			}
		}

		private static void TessellateBorderedFan(Vector2 center, Vector2 outerRadius, float leftWidth, float topWidth, Color32 leftColor, Color32 topColor, float posZ, MeshWriteData mesh, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			if (countOnly)
			{
				vertexCount += Tessellation.kSubdivisions * 2 + 2;
				indexCount += (Tessellation.kSubdivisions - 1) * 6;
			}
			else
			{
				Color32 idsFlags = new Color32(0, 0, 0, 5);
				Color32 color = new Color32(0, 0, 0, 0);
				Vector2 vector = new Vector2(outerRadius.x - leftWidth, outerRadius.y - topWidth);
				Vector2 uv = new Vector2(leftWidth, topWidth);
				Vector2 dir = new Vector2(leftWidth, topWidth);
				Vector2 b = Tessellation.IntersectEllipseWithLine(outerRadius.x, outerRadius.y, dir);
				Vector2 b2 = Tessellation.IntersectEllipseWithLine(vector.x, vector.y, dir);
				float centeredEllipseLineIntersectionTheta = Tessellation.GetCenteredEllipseLineIntersectionTheta(outerRadius.x, outerRadius.y, outerRadius - b);
				float centeredEllipseLineIntersectionTheta2 = Tessellation.GetCenteredEllipseLineIntersectionTheta(vector.x, vector.y, vector - b2);
				float num = 0.5f * (centeredEllipseLineIntersectionTheta + centeredEllipseLineIntersectionTheta2);
				int num2 = (int)(Tessellation.kSubdivisions - 1);
				int num3 = Mathf.Clamp(Mathf.RoundToInt(num * 0.636619747f * (float)num2), 1, num2 - 1);
				int num4 = num2 - num3;
				float num5 = centeredEllipseLineIntersectionTheta / (float)num3;
				float num6 = centeredEllipseLineIntersectionTheta2 / (float)num3;
				Vector2 vector2 = new Vector2(center.x - outerRadius.x, center.y);
				Vector2 vector3 = new Vector2(center.x - vector.x, center.y);
				mesh.SetNextVertex(new Vertex
				{
					position = new Vector3(vector3.x, vector3.y, posZ),
					tint = leftColor,
					idsFlags = idsFlags,
					uv = uv
				});
				mesh.SetNextVertex(new Vertex
				{
					position = new Vector3(vector2.x, vector2.y, posZ),
					tint = leftColor,
					idsFlags = color
				});
				vertexCount += 2;
				for (int i = 1; i <= num3; i++)
				{
					float f = (float)i * num5;
					float f2 = (float)i * num6;
					vector2 = center - new Vector2(Mathf.Cos(f), Mathf.Sin(f)) * outerRadius;
					vector3 = center - new Vector2(Mathf.Cos(f2), Mathf.Sin(f2)) * vector;
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector3.x, vector3.y, posZ),
						tint = leftColor,
						idsFlags = idsFlags,
						uv = uv
					});
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector2.x, vector2.y, posZ),
						tint = leftColor,
						idsFlags = color
					});
					vertexCount += 2;
					mesh.SetNextIndex(vertexCount - 4);
					mesh.SetNextIndex(vertexCount - 3);
					mesh.SetNextIndex(vertexCount - 2);
					mesh.SetNextIndex(vertexCount - 3);
					mesh.SetNextIndex(vertexCount - 1);
					mesh.SetNextIndex(vertexCount - 2);
					indexCount += 6;
				}
				float num7 = (1.57079637f - centeredEllipseLineIntersectionTheta) / (float)num4;
				float num8 = (1.57079637f - centeredEllipseLineIntersectionTheta2) / (float)num4;
				color = new Color32(0, 0, 0, 0);
				idsFlags = color;
				Vector2 vector4 = center - new Vector2(Mathf.Cos(centeredEllipseLineIntersectionTheta), Mathf.Sin(centeredEllipseLineIntersectionTheta)) * outerRadius;
				Vector2 vector5 = center - new Vector2(Mathf.Cos(centeredEllipseLineIntersectionTheta2), Mathf.Sin(centeredEllipseLineIntersectionTheta2)) * vector;
				mesh.SetNextVertex(new Vertex
				{
					position = new Vector3(vector5.x, vector5.y, posZ),
					tint = topColor,
					idsFlags = idsFlags,
					uv = uv
				});
				mesh.SetNextVertex(new Vertex
				{
					position = new Vector3(vector4.x, vector4.y, posZ),
					tint = topColor,
					idsFlags = color
				});
				vertexCount += 2;
				for (int j = 1; j <= num4; j++)
				{
					float f3 = centeredEllipseLineIntersectionTheta + (float)j * num7;
					float f4 = centeredEllipseLineIntersectionTheta2 + (float)j * num8;
					vector4 = center - new Vector2(Mathf.Cos(f3), Mathf.Sin(f3)) * outerRadius;
					vector5 = center - new Vector2(Mathf.Cos(f4), Mathf.Sin(f4)) * vector;
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector5.x, vector5.y, posZ),
						tint = topColor,
						idsFlags = idsFlags,
						uv = uv
					});
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector4.x, vector4.y, posZ),
						tint = topColor,
						idsFlags = color
					});
					vertexCount += 2;
					mesh.SetNextIndex(vertexCount - 4);
					mesh.SetNextIndex(vertexCount - 3);
					mesh.SetNextIndex(vertexCount - 2);
					mesh.SetNextIndex(vertexCount - 3);
					mesh.SetNextIndex(vertexCount - 1);
					mesh.SetNextIndex(vertexCount - 2);
					indexCount += 6;
				}
			}
		}

		private static void TessellateBorderedFan(Vector2 center, Vector2 radius, float leftWidth, float topWidth, Color32 color, float posZ, MeshWriteData mesh, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			if (countOnly)
			{
				vertexCount += Tessellation.kSubdivisions * 2;
				indexCount += (Tessellation.kSubdivisions - 1) * 6;
			}
			else
			{
				Color32 idsFlags = new Color32(0, 0, 0, 5);
				Color32 idsFlags2 = new Color32(0, 0, 0, 0);
				Vector2 uv = new Vector2(leftWidth, topWidth);
				float num = radius.x - leftWidth;
				float num2 = radius.y - topWidth;
				Vector2 vector = new Vector2(center.x - radius.x, center.y);
				Vector2 vector2 = new Vector2(center.x - num, center.y);
				ushort num3 = vertexCount;
				mesh.SetNextVertex(new Vertex
				{
					position = new Vector3(vector2.x, vector2.y, posZ),
					tint = color,
					idsFlags = idsFlags,
					uv = uv
				});
				mesh.SetNextVertex(new Vertex
				{
					position = new Vector3(vector.x, vector.y, posZ),
					tint = color,
					idsFlags = idsFlags2
				});
				vertexCount += 2;
				for (int i = 1; i < (int)Tessellation.kSubdivisions; i++)
				{
					float num4 = (float)i / (float)(Tessellation.kSubdivisions - 1);
					float f = 1.57079637f * num4;
					vector = center + new Vector2(-Mathf.Cos(f), -Mathf.Sin(f)) * radius;
					vector2 = center + new Vector2(-num * Mathf.Cos(f), -num2 * Mathf.Sin(f));
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector2.x, vector2.y, posZ),
						tint = color,
						idsFlags = idsFlags,
						uv = uv
					});
					mesh.SetNextVertex(new Vertex
					{
						position = new Vector3(vector.x, vector.y, posZ),
						tint = color,
						idsFlags = idsFlags2
					});
					vertexCount += 2;
					int num5 = i * 2;
					mesh.SetNextIndex((ushort)((int)num3 + (num5 - 2)));
					mesh.SetNextIndex((ushort)((int)num3 + (num5 - 1)));
					mesh.SetNextIndex((ushort)((int)num3 + num5));
					mesh.SetNextIndex((ushort)((int)num3 + (num5 - 1)));
					mesh.SetNextIndex((ushort)((int)num3 + (num5 + 1)));
					mesh.SetNextIndex((ushort)((int)num3 + num5));
					indexCount += 6;
				}
				num3 += Tessellation.kSubdivisions * 2;
			}
		}

		private static void MirrorVertices(Rect rect, NativeSlice<Vertex> vertices, int vertexStart, int vertexCount, bool flipHorizontal)
		{
			if (flipHorizontal)
			{
				for (int i = 0; i < vertexCount; i++)
				{
					Vertex vertex = vertices[vertexStart + i];
					vertex.position.x = rect.xMax - (vertex.position.x - rect.xMax);
					vertex.uv.x = -vertex.uv.x;
					vertices[vertexStart + i] = vertex;
				}
			}
			else
			{
				for (int j = 0; j < vertexCount; j++)
				{
					Vertex vertex2 = vertices[vertexStart + j];
					vertex2.position.y = rect.yMax - (vertex2.position.y - rect.yMax);
					vertex2.uv.y = -vertex2.uv.y;
					vertices[vertexStart + j] = vertex2;
				}
			}
		}

		private static void FlipWinding(NativeSlice<ushort> indices, int indexStart, int indexCount)
		{
			for (int i = 0; i < indexCount; i += 3)
			{
				ushort value = indices[indexStart + i];
				indices[indexStart + i] = indices[indexStart + i + 1];
				indices[indexStart + i + 1] = value;
			}
		}

		private static void ComputeUVs(Rect tessellatedRect, Rect textureRect, Rect uvRegion, NativeSlice<Vertex> vertices)
		{
			Vector2 position = tessellatedRect.position;
			Vector2 b = new Vector2(1f / tessellatedRect.width, 1f / tessellatedRect.height);
			for (int i = 0; i < vertices.Length; i++)
			{
				Vertex vertex = vertices[i];
				Vector2 vector = vertex.position;
				vector -= position;
				vector *= b;
				vertex.uv.x = (vector.x * textureRect.width + textureRect.xMin) * uvRegion.width + uvRegion.xMin;
				vertex.uv.y = ((1f - vector.y) * textureRect.height + textureRect.yMin) * uvRegion.height + uvRegion.yMin;
				vertices[i] = vertex;
			}
		}
	}
}
