using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/UI/Canvas.h"), NativeHeader("Modules/UI/RectTransformUtil.h"), NativeHeader("Runtime/Transform/RectTransform.h"), NativeHeader("Runtime/Camera/Camera.h"), StaticAccessor("UI", StaticAccessorType.DoubleColon)]
	public sealed class RectTransformUtility
	{
		private static readonly Vector3[] s_Corners = new Vector3[4];

		public static Vector2 PixelAdjustPoint(Vector2 point, Transform elementTransform, Canvas canvas)
		{
			Vector2 result;
			RectTransformUtility.PixelAdjustPoint_Injected(ref point, elementTransform, canvas, out result);
			return result;
		}

		public static Rect PixelAdjustRect(RectTransform rectTransform, Canvas canvas)
		{
			Rect result;
			RectTransformUtility.PixelAdjustRect_Injected(rectTransform, canvas, out result);
			return result;
		}

		private static bool PointInRectangle(Vector2 screenPoint, RectTransform rect, Camera cam, Vector4 offset)
		{
			return RectTransformUtility.PointInRectangle_Injected(ref screenPoint, rect, cam, ref offset);
		}

		private RectTransformUtility()
		{
		}

		public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint)
		{
			return RectTransformUtility.RectangleContainsScreenPoint(rect, screenPoint, null);
		}

		public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint, Camera cam)
		{
			return RectTransformUtility.RectangleContainsScreenPoint(rect, screenPoint, cam, Vector4.zero);
		}

		public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint, Camera cam, Vector4 offset)
		{
			return RectTransformUtility.PointInRectangle(screenPoint, rect, cam, offset);
		}

		public static bool ScreenPointToWorldPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector3 worldPoint)
		{
			worldPoint = Vector2.zero;
			Ray ray = RectTransformUtility.ScreenPointToRay(cam, screenPoint);
			Plane plane = new Plane(rect.rotation * Vector3.back, rect.position);
			float distance;
			bool flag = !plane.Raycast(ray, out distance);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				worldPoint = ray.GetPoint(distance);
				result = true;
			}
			return result;
		}

		public static bool ScreenPointToLocalPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector2 localPoint)
		{
			localPoint = Vector2.zero;
			Vector3 position;
			bool flag = RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPoint, cam, out position);
			bool result;
			if (flag)
			{
				localPoint = rect.InverseTransformPoint(position);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public static Ray ScreenPointToRay(Camera cam, Vector2 screenPos)
		{
			bool flag = cam != null;
			Ray result;
			if (flag)
			{
				result = cam.ScreenPointToRay(screenPos);
			}
			else
			{
				Vector3 origin = screenPos;
				origin.z -= 100f;
				result = new Ray(origin, Vector3.forward);
			}
			return result;
		}

		public static Vector2 WorldToScreenPoint(Camera cam, Vector3 worldPoint)
		{
			bool flag = cam == null;
			Vector2 result;
			if (flag)
			{
				result = new Vector2(worldPoint.x, worldPoint.y);
			}
			else
			{
				result = cam.WorldToScreenPoint(worldPoint);
			}
			return result;
		}

		public static Bounds CalculateRelativeRectTransformBounds(Transform root, Transform child)
		{
			RectTransform[] componentsInChildren = child.GetComponentsInChildren<RectTransform>(false);
			bool flag = componentsInChildren.Length != 0;
			Bounds result;
			if (flag)
			{
				Vector3 vector = new Vector3(3.40282347E+38f, 3.40282347E+38f, 3.40282347E+38f);
				Vector3 vector2 = new Vector3(-3.40282347E+38f, -3.40282347E+38f, -3.40282347E+38f);
				Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					componentsInChildren[i].GetWorldCorners(RectTransformUtility.s_Corners);
					for (int j = 0; j < 4; j++)
					{
						Vector3 lhs = worldToLocalMatrix.MultiplyPoint3x4(RectTransformUtility.s_Corners[j]);
						vector = Vector3.Min(lhs, vector);
						vector2 = Vector3.Max(lhs, vector2);
					}
					i++;
				}
				Bounds bounds = new Bounds(vector, Vector3.zero);
				bounds.Encapsulate(vector2);
				result = bounds;
			}
			else
			{
				result = new Bounds(Vector3.zero, Vector3.zero);
			}
			return result;
		}

		public static Bounds CalculateRelativeRectTransformBounds(Transform trans)
		{
			return RectTransformUtility.CalculateRelativeRectTransformBounds(trans, trans);
		}

		public static void FlipLayoutOnAxis(RectTransform rect, int axis, bool keepPositioning, bool recursive)
		{
			bool flag = rect == null;
			if (!flag)
			{
				if (recursive)
				{
					for (int i = 0; i < rect.childCount; i++)
					{
						RectTransform rectTransform = rect.GetChild(i) as RectTransform;
						bool flag2 = rectTransform != null;
						if (flag2)
						{
							RectTransformUtility.FlipLayoutOnAxis(rectTransform, axis, false, true);
						}
					}
				}
				Vector2 pivot = rect.pivot;
				pivot[axis] = 1f - pivot[axis];
				rect.pivot = pivot;
				if (!keepPositioning)
				{
					Vector2 anchoredPosition = rect.anchoredPosition;
					anchoredPosition[axis] = -anchoredPosition[axis];
					rect.anchoredPosition = anchoredPosition;
					Vector2 anchorMin = rect.anchorMin;
					Vector2 anchorMax = rect.anchorMax;
					float num = anchorMin[axis];
					anchorMin[axis] = 1f - anchorMax[axis];
					anchorMax[axis] = 1f - num;
					rect.anchorMin = anchorMin;
					rect.anchorMax = anchorMax;
				}
			}
		}

		public static void FlipLayoutAxes(RectTransform rect, bool keepPositioning, bool recursive)
		{
			bool flag = rect == null;
			if (!flag)
			{
				if (recursive)
				{
					for (int i = 0; i < rect.childCount; i++)
					{
						RectTransform rectTransform = rect.GetChild(i) as RectTransform;
						bool flag2 = rectTransform != null;
						if (flag2)
						{
							RectTransformUtility.FlipLayoutAxes(rectTransform, false, true);
						}
					}
				}
				rect.pivot = RectTransformUtility.GetTransposed(rect.pivot);
				rect.sizeDelta = RectTransformUtility.GetTransposed(rect.sizeDelta);
				if (!keepPositioning)
				{
					rect.anchoredPosition = RectTransformUtility.GetTransposed(rect.anchoredPosition);
					rect.anchorMin = RectTransformUtility.GetTransposed(rect.anchorMin);
					rect.anchorMax = RectTransformUtility.GetTransposed(rect.anchorMax);
				}
			}
		}

		private static Vector2 GetTransposed(Vector2 input)
		{
			return new Vector2(input.y, input.x);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PixelAdjustPoint_Injected(ref Vector2 point, Transform elementTransform, Canvas canvas, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PixelAdjustRect_Injected(RectTransform rectTransform, Canvas canvas, out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool PointInRectangle_Injected(ref Vector2 screenPoint, RectTransform rect, Camera cam, ref Vector4 offset);
	}
}
