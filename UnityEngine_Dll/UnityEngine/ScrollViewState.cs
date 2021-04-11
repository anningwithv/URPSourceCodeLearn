using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	internal class ScrollViewState
	{
		public Rect position;

		public Rect visibleRect;

		public Rect viewRect;

		public Vector2 scrollPosition;

		public bool apply;

		public bool isDuringTouchScroll;

		public Vector2 touchScrollStartMousePosition;

		public Vector2 touchScrollStartPosition;

		public Vector2 velocity;

		public float previousTimeSinceStartup;

		[RequiredByNativeCode]
		public ScrollViewState()
		{
		}

		public void ScrollTo(Rect pos)
		{
			this.ScrollTowards(pos, float.PositiveInfinity);
		}

		public bool ScrollTowards(Rect pos, float maxDelta)
		{
			Vector2 b = this.ScrollNeeded(pos);
			bool flag = b.sqrMagnitude < 0.0001f;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = maxDelta == 0f;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = b.magnitude > maxDelta;
					if (flag3)
					{
						b = b.normalized * maxDelta;
					}
					this.scrollPosition += b;
					this.apply = true;
					result = true;
				}
			}
			return result;
		}

		private Vector2 ScrollNeeded(Rect pos)
		{
			Rect rect = this.visibleRect;
			rect.x += this.scrollPosition.x;
			rect.y += this.scrollPosition.y;
			float num = pos.width - this.visibleRect.width;
			bool flag = num > 0f;
			if (flag)
			{
				pos.width -= num;
				pos.x += num * 0.5f;
			}
			num = pos.height - this.visibleRect.height;
			bool flag2 = num > 0f;
			if (flag2)
			{
				pos.height -= num;
				pos.y += num * 0.5f;
			}
			Vector2 zero = Vector2.zero;
			bool flag3 = pos.xMax > rect.xMax;
			if (flag3)
			{
				zero.x += pos.xMax - rect.xMax;
			}
			else
			{
				bool flag4 = pos.xMin < rect.xMin;
				if (flag4)
				{
					zero.x -= rect.xMin - pos.xMin;
				}
			}
			bool flag5 = pos.yMax > rect.yMax;
			if (flag5)
			{
				zero.y += pos.yMax - rect.yMax;
			}
			else
			{
				bool flag6 = pos.yMin < rect.yMin;
				if (flag6)
				{
					zero.y -= rect.yMin - pos.yMin;
				}
			}
			Rect rect2 = this.viewRect;
			rect2.width = Mathf.Max(rect2.width, this.visibleRect.width);
			rect2.height = Mathf.Max(rect2.height, this.visibleRect.height);
			zero.x = Mathf.Clamp(zero.x, rect2.xMin - this.scrollPosition.x, rect2.xMax - this.visibleRect.width - this.scrollPosition.x);
			zero.y = Mathf.Clamp(zero.y, rect2.yMin - this.scrollPosition.y, rect2.yMax - this.visibleRect.height - this.scrollPosition.y);
			return zero;
		}
	}
}
