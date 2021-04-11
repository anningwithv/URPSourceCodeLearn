using System;

namespace UnityEngine.UIElements
{
	public static class VisualElementExtensions
	{
		public static Vector2 WorldToLocal(this VisualElement ele, Vector2 p)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			return VisualElement.MultiplyMatrix44Point2(ele.worldTransformInverse, p);
		}

		public static Vector2 LocalToWorld(this VisualElement ele, Vector2 p)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			return VisualElement.MultiplyMatrix44Point2(ele.worldTransform, p);
		}

		public static Rect WorldToLocal(this VisualElement ele, Rect r)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			Vector2 position = VisualElement.MultiplyMatrix44Point2(ele.worldTransformInverse, r.position);
			r.position = position;
			r.size = ele.worldTransformInverse.MultiplyVector(r.size);
			return r;
		}

		public static Rect LocalToWorld(this VisualElement ele, Rect r)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			Matrix4x4 worldTransform = ele.worldTransform;
			r.position = VisualElement.MultiplyMatrix44Point2(worldTransform, r.position);
			r.size = worldTransform.MultiplyVector(r.size);
			return r;
		}

		public static Vector2 ChangeCoordinatesTo(this VisualElement src, VisualElement dest, Vector2 point)
		{
			return dest.WorldToLocal(src.LocalToWorld(point));
		}

		public static Rect ChangeCoordinatesTo(this VisualElement src, VisualElement dest, Rect rect)
		{
			return dest.WorldToLocal(src.LocalToWorld(rect));
		}

		public static void StretchToParentSize(this VisualElement elem)
		{
			bool flag = elem == null;
			if (flag)
			{
				throw new ArgumentNullException("elem");
			}
			IStyle style = elem.style;
			style.position = Position.Absolute;
			style.left = 0f;
			style.top = 0f;
			style.right = 0f;
			style.bottom = 0f;
		}

		public static void StretchToParentWidth(this VisualElement elem)
		{
			bool flag = elem == null;
			if (flag)
			{
				throw new ArgumentNullException("elem");
			}
			IStyle style = elem.style;
			style.position = Position.Absolute;
			style.left = 0f;
			style.right = 0f;
		}

		public static void AddManipulator(this VisualElement ele, IManipulator manipulator)
		{
			bool flag = manipulator != null;
			if (flag)
			{
				manipulator.target = ele;
			}
		}

		public static void RemoveManipulator(this VisualElement ele, IManipulator manipulator)
		{
			bool flag = manipulator != null;
			if (flag)
			{
				manipulator.target = null;
			}
		}
	}
}
