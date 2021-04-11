using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	internal sealed class GUIScrollGroup : GUILayoutGroup
	{
		public float calcMinWidth;

		public float calcMaxWidth;

		public float calcMinHeight;

		public float calcMaxHeight;

		public float clientWidth;

		public float clientHeight;

		public bool allowHorizontalScroll = true;

		public bool allowVerticalScroll = true;

		public bool needsHorizontalScrollbar;

		public bool needsVerticalScrollbar;

		public GUIStyle horizontalScrollbar;

		public GUIStyle verticalScrollbar;

		[RequiredByNativeCode]
		public GUIScrollGroup()
		{
		}

		public override void CalcWidth()
		{
			float minWidth = this.minWidth;
			float maxWidth = this.maxWidth;
			bool flag = this.allowHorizontalScroll;
			if (flag)
			{
				this.minWidth = 0f;
				this.maxWidth = 0f;
			}
			base.CalcWidth();
			this.calcMinWidth = this.minWidth;
			this.calcMaxWidth = this.maxWidth;
			bool flag2 = this.allowHorizontalScroll;
			if (flag2)
			{
				bool flag3 = this.minWidth > 32f;
				if (flag3)
				{
					this.minWidth = 32f;
				}
				bool flag4 = minWidth != 0f;
				if (flag4)
				{
					this.minWidth = minWidth;
				}
				bool flag5 = maxWidth != 0f;
				if (flag5)
				{
					this.maxWidth = maxWidth;
					this.stretchWidth = 0;
				}
			}
		}

		public override void SetHorizontal(float x, float width)
		{
			float num = this.needsVerticalScrollbar ? (width - this.verticalScrollbar.fixedWidth - (float)this.verticalScrollbar.margin.left) : width;
			bool flag = this.allowHorizontalScroll && num < this.calcMinWidth;
			if (flag)
			{
				this.needsHorizontalScrollbar = true;
				this.minWidth = this.calcMinWidth;
				this.maxWidth = this.calcMaxWidth;
				base.SetHorizontal(x, this.calcMinWidth);
				this.rect.width = width;
				this.clientWidth = this.calcMinWidth;
			}
			else
			{
				this.needsHorizontalScrollbar = false;
				bool flag2 = this.allowHorizontalScroll;
				if (flag2)
				{
					this.minWidth = this.calcMinWidth;
					this.maxWidth = this.calcMaxWidth;
				}
				base.SetHorizontal(x, num);
				this.rect.width = width;
				this.clientWidth = num;
			}
		}

		public override void CalcHeight()
		{
			float minHeight = this.minHeight;
			float maxHeight = this.maxHeight;
			bool flag = this.allowVerticalScroll;
			if (flag)
			{
				this.minHeight = 0f;
				this.maxHeight = 0f;
			}
			base.CalcHeight();
			this.calcMinHeight = this.minHeight;
			this.calcMaxHeight = this.maxHeight;
			bool flag2 = this.needsHorizontalScrollbar;
			if (flag2)
			{
				float num = this.horizontalScrollbar.fixedHeight + (float)this.horizontalScrollbar.margin.top;
				this.minHeight += num;
				this.maxHeight += num;
			}
			bool flag3 = this.allowVerticalScroll;
			if (flag3)
			{
				bool flag4 = this.minHeight > 32f;
				if (flag4)
				{
					this.minHeight = 32f;
				}
				bool flag5 = minHeight != 0f;
				if (flag5)
				{
					this.minHeight = minHeight;
				}
				bool flag6 = maxHeight != 0f;
				if (flag6)
				{
					this.maxHeight = maxHeight;
					this.stretchHeight = 0;
				}
			}
		}

		public override void SetVertical(float y, float height)
		{
			float num = height;
			bool flag = this.needsHorizontalScrollbar;
			if (flag)
			{
				num -= this.horizontalScrollbar.fixedHeight + (float)this.horizontalScrollbar.margin.top;
			}
			bool flag2 = this.allowVerticalScroll && num < this.calcMinHeight;
			if (flag2)
			{
				bool flag3 = !this.needsHorizontalScrollbar && !this.needsVerticalScrollbar;
				if (flag3)
				{
					this.clientWidth = this.rect.width - this.verticalScrollbar.fixedWidth - (float)this.verticalScrollbar.margin.left;
					bool flag4 = this.clientWidth < this.calcMinWidth;
					if (flag4)
					{
						this.clientWidth = this.calcMinWidth;
					}
					float width = this.rect.width;
					this.SetHorizontal(this.rect.x, this.clientWidth);
					this.CalcHeight();
					this.rect.width = width;
				}
				float minHeight = this.minHeight;
				float maxHeight = this.maxHeight;
				this.minHeight = this.calcMinHeight;
				this.maxHeight = this.calcMaxHeight;
				base.SetVertical(y, this.calcMinHeight);
				this.minHeight = minHeight;
				this.maxHeight = maxHeight;
				this.rect.height = height;
				this.clientHeight = this.calcMinHeight;
			}
			else
			{
				bool flag5 = this.allowVerticalScroll;
				if (flag5)
				{
					this.minHeight = this.calcMinHeight;
					this.maxHeight = this.calcMaxHeight;
				}
				base.SetVertical(y, num);
				this.rect.height = height;
				this.clientHeight = num;
			}
		}
	}
}
