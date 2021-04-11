using System;

namespace UnityEngine
{
	internal class GUILayoutEntry
	{
		public float minWidth;

		public float maxWidth;

		public float minHeight;

		public float maxHeight;

		public Rect rect = new Rect(0f, 0f, 0f, 0f);

		public int stretchWidth;

		public int stretchHeight;

		public bool consideredForMargin = true;

		private GUIStyle m_Style = GUIStyle.none;

		internal static Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		protected static int indent = 0;

		public GUIStyle style
		{
			get
			{
				return this.m_Style;
			}
			set
			{
				this.m_Style = value;
				this.ApplyStyleSettings(value);
			}
		}

		public virtual int marginLeft
		{
			get
			{
				return this.style.margin.left;
			}
		}

		public virtual int marginRight
		{
			get
			{
				return this.style.margin.right;
			}
		}

		public virtual int marginTop
		{
			get
			{
				return this.style.margin.top;
			}
		}

		public virtual int marginBottom
		{
			get
			{
				return this.style.margin.bottom;
			}
		}

		public int marginHorizontal
		{
			get
			{
				return this.marginLeft + this.marginRight;
			}
		}

		public int marginVertical
		{
			get
			{
				return this.marginBottom + this.marginTop;
			}
		}

		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			bool flag = _style == null;
			if (flag)
			{
				_style = GUIStyle.none;
			}
			this.style = _style;
		}

		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style, GUILayoutOption[] options)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			this.style = _style;
			this.ApplyOptions(options);
		}

		public virtual void CalcWidth()
		{
		}

		public virtual void CalcHeight()
		{
		}

		public virtual void SetHorizontal(float x, float width)
		{
			this.rect.x = x;
			this.rect.width = width;
		}

		public virtual void SetVertical(float y, float height)
		{
			this.rect.y = y;
			this.rect.height = height;
		}

		protected virtual void ApplyStyleSettings(GUIStyle style)
		{
			this.stretchWidth = ((style.fixedWidth == 0f && style.stretchWidth) ? 1 : 0);
			this.stretchHeight = ((style.fixedHeight == 0f && style.stretchHeight) ? 1 : 0);
			this.m_Style = style;
		}

		public virtual void ApplyOptions(GUILayoutOption[] options)
		{
			bool flag = options == null;
			if (!flag)
			{
				for (int i = 0; i < options.Length; i++)
				{
					GUILayoutOption gUILayoutOption = options[i];
					switch (gUILayoutOption.type)
					{
					case GUILayoutOption.Type.fixedWidth:
						this.minWidth = (this.maxWidth = (float)gUILayoutOption.value);
						this.stretchWidth = 0;
						break;
					case GUILayoutOption.Type.fixedHeight:
						this.minHeight = (this.maxHeight = (float)gUILayoutOption.value);
						this.stretchHeight = 0;
						break;
					case GUILayoutOption.Type.minWidth:
					{
						this.minWidth = (float)gUILayoutOption.value;
						bool flag2 = this.maxWidth < this.minWidth;
						if (flag2)
						{
							this.maxWidth = this.minWidth;
						}
						break;
					}
					case GUILayoutOption.Type.maxWidth:
					{
						this.maxWidth = (float)gUILayoutOption.value;
						bool flag3 = this.minWidth > this.maxWidth;
						if (flag3)
						{
							this.minWidth = this.maxWidth;
						}
						this.stretchWidth = 0;
						break;
					}
					case GUILayoutOption.Type.minHeight:
					{
						this.minHeight = (float)gUILayoutOption.value;
						bool flag4 = this.maxHeight < this.minHeight;
						if (flag4)
						{
							this.maxHeight = this.minHeight;
						}
						break;
					}
					case GUILayoutOption.Type.maxHeight:
					{
						this.maxHeight = (float)gUILayoutOption.value;
						bool flag5 = this.minHeight > this.maxHeight;
						if (flag5)
						{
							this.minHeight = this.maxHeight;
						}
						this.stretchHeight = 0;
						break;
					}
					case GUILayoutOption.Type.stretchWidth:
						this.stretchWidth = (int)gUILayoutOption.value;
						break;
					case GUILayoutOption.Type.stretchHeight:
						this.stretchHeight = (int)gUILayoutOption.value;
						break;
					}
				}
				bool flag6 = this.maxWidth != 0f && this.maxWidth < this.minWidth;
				if (flag6)
				{
					this.maxWidth = this.minWidth;
				}
				bool flag7 = this.maxHeight != 0f && this.maxHeight < this.minHeight;
				if (flag7)
				{
					this.maxHeight = this.minHeight;
				}
			}
		}

		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < GUILayoutEntry.indent; i++)
			{
				text += " ";
			}
			return string.Concat(new string[]
			{
				text,
				UnityString.Format("{1}-{0} (x:{2}-{3}, y:{4}-{5})", new object[]
				{
					(this.style != null) ? this.style.name : "NULL",
					base.GetType(),
					this.rect.x,
					this.rect.xMax,
					this.rect.y,
					this.rect.yMax
				}),
				"   -   W: ",
				this.minWidth.ToString(),
				"-",
				this.maxWidth.ToString(),
				(this.stretchWidth != 0) ? "+" : "",
				", H: ",
				this.minHeight.ToString(),
				"-",
				this.maxHeight.ToString(),
				(this.stretchHeight != 0) ? "+" : ""
			});
		}
	}
}
