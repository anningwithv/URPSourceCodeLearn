using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[VisibleToOtherModules(new string[]
	{
		"UnityEngine.UIElementsModule",
		"Unity.UIElements"
	})]
	internal class GUILayoutGroup : GUILayoutEntry
	{
		public List<GUILayoutEntry> entries = new List<GUILayoutEntry>();

		public bool isVertical = true;

		public bool resetCoords = false;

		public float spacing = 0f;

		public bool sameSize = true;

		public bool isWindow = false;

		public int windowID = -1;

		private int m_Cursor = 0;

		protected int m_StretchableCountX = 100;

		protected int m_StretchableCountY = 100;

		protected bool m_UserSpecifiedWidth = false;

		protected bool m_UserSpecifiedHeight = false;

		protected float m_ChildMinWidth = 100f;

		protected float m_ChildMaxWidth = 100f;

		protected float m_ChildMinHeight = 100f;

		protected float m_ChildMaxHeight = 100f;

		protected int m_MarginLeft;

		protected int m_MarginRight;

		protected int m_MarginTop;

		protected int m_MarginBottom;

		private static readonly GUILayoutEntry none = new GUILayoutEntry(0f, 1f, 0f, 1f, GUIStyle.none);

		public override int marginLeft
		{
			get
			{
				return this.m_MarginLeft;
			}
		}

		public override int marginRight
		{
			get
			{
				return this.m_MarginRight;
			}
		}

		public override int marginTop
		{
			get
			{
				return this.m_MarginTop;
			}
		}

		public override int marginBottom
		{
			get
			{
				return this.m_MarginBottom;
			}
		}

		public GUILayoutGroup() : base(0f, 0f, 0f, 0f, GUIStyle.none)
		{
		}

		public GUILayoutGroup(GUIStyle _style, GUILayoutOption[] options) : base(0f, 0f, 0f, 0f, _style)
		{
			bool flag = options != null;
			if (flag)
			{
				this.ApplyOptions(options);
			}
			this.m_MarginLeft = _style.margin.left;
			this.m_MarginRight = _style.margin.right;
			this.m_MarginTop = _style.margin.top;
			this.m_MarginBottom = _style.margin.bottom;
		}

		public override void ApplyOptions(GUILayoutOption[] options)
		{
			bool flag = options == null;
			if (!flag)
			{
				base.ApplyOptions(options);
				for (int i = 0; i < options.Length; i++)
				{
					GUILayoutOption gUILayoutOption = options[i];
					GUILayoutOption.Type type = gUILayoutOption.type;
					GUILayoutOption.Type type2 = type;
					switch (type2)
					{
					case GUILayoutOption.Type.fixedWidth:
					case GUILayoutOption.Type.minWidth:
					case GUILayoutOption.Type.maxWidth:
						this.m_UserSpecifiedHeight = true;
						break;
					case GUILayoutOption.Type.fixedHeight:
					case GUILayoutOption.Type.minHeight:
					case GUILayoutOption.Type.maxHeight:
						this.m_UserSpecifiedWidth = true;
						break;
					default:
						if (type2 == GUILayoutOption.Type.spacing)
						{
							this.spacing = (float)((int)gUILayoutOption.value);
						}
						break;
					}
				}
			}
		}

		protected override void ApplyStyleSettings(GUIStyle style)
		{
			base.ApplyStyleSettings(style);
			RectOffset margin = style.margin;
			this.m_MarginLeft = margin.left;
			this.m_MarginRight = margin.right;
			this.m_MarginTop = margin.top;
			this.m_MarginBottom = margin.bottom;
		}

		public void ResetCursor()
		{
			this.m_Cursor = 0;
		}

		public Rect PeekNext()
		{
			bool flag = this.m_Cursor < this.entries.Count;
			Rect result;
			if (flag)
			{
				GUILayoutEntry gUILayoutEntry = this.entries[this.m_Cursor];
				result = gUILayoutEntry.rect;
			}
			else
			{
				bool flag2 = Event.current.type == EventType.Repaint;
				if (flag2)
				{
					throw new ArgumentException(string.Concat(new string[]
					{
						"Getting control ",
						this.m_Cursor.ToString(),
						"'s position in a group with only ",
						this.entries.Count.ToString(),
						" controls when doing ",
						Event.current.rawType.ToString(),
						"\nAborting"
					}));
				}
				result = GUILayoutEntry.kDummyRect;
			}
			return result;
		}

		public GUILayoutEntry GetNext()
		{
			bool flag = this.m_Cursor < this.entries.Count;
			GUILayoutEntry result;
			if (flag)
			{
				GUILayoutEntry gUILayoutEntry = this.entries[this.m_Cursor];
				this.m_Cursor++;
				result = gUILayoutEntry;
			}
			else
			{
				bool flag2 = Event.current.type == EventType.Repaint;
				if (flag2)
				{
					throw new ArgumentException(string.Concat(new string[]
					{
						"Getting control ",
						this.m_Cursor.ToString(),
						"'s position in a group with only ",
						this.entries.Count.ToString(),
						" controls when doing ",
						Event.current.rawType.ToString(),
						"\nAborting"
					}));
				}
				result = GUILayoutGroup.none;
			}
			return result;
		}

		public Rect GetLast()
		{
			bool flag = this.m_Cursor == 0;
			Rect result;
			if (flag)
			{
				bool flag2 = Event.current.type == EventType.Repaint;
				if (flag2)
				{
					Debug.LogError("You cannot call GetLast immediately after beginning a group.");
				}
				result = GUILayoutEntry.kDummyRect;
			}
			else
			{
				bool flag3 = this.m_Cursor <= this.entries.Count;
				if (flag3)
				{
					GUILayoutEntry gUILayoutEntry = this.entries[this.m_Cursor - 1];
					result = gUILayoutEntry.rect;
				}
				else
				{
					bool flag4 = Event.current.type == EventType.Repaint;
					if (flag4)
					{
						Debug.LogError(string.Concat(new string[]
						{
							"Getting control ",
							this.m_Cursor.ToString(),
							"'s position in a group with only ",
							this.entries.Count.ToString(),
							" controls when doing ",
							Event.current.rawType.ToString()
						}));
					}
					result = GUILayoutEntry.kDummyRect;
				}
			}
			return result;
		}

		public void Add(GUILayoutEntry e)
		{
			this.entries.Add(e);
		}

		public override void CalcWidth()
		{
			bool flag = this.entries.Count == 0;
			if (flag)
			{
				this.maxWidth = (this.minWidth = (float)base.style.padding.horizontal);
			}
			else
			{
				int num = 0;
				int num2 = 0;
				this.m_ChildMinWidth = 0f;
				this.m_ChildMaxWidth = 0f;
				this.m_StretchableCountX = 0;
				bool flag2 = true;
				bool flag3 = this.isVertical;
				if (flag3)
				{
					foreach (GUILayoutEntry current in this.entries)
					{
						current.CalcWidth();
						bool consideredForMargin = current.consideredForMargin;
						if (consideredForMargin)
						{
							bool flag4 = !flag2;
							if (flag4)
							{
								num = Mathf.Min(current.marginLeft, num);
								num2 = Mathf.Min(current.marginRight, num2);
							}
							else
							{
								num = current.marginLeft;
								num2 = current.marginRight;
								flag2 = false;
							}
							this.m_ChildMinWidth = Mathf.Max(current.minWidth + (float)current.marginHorizontal, this.m_ChildMinWidth);
							this.m_ChildMaxWidth = Mathf.Max(current.maxWidth + (float)current.marginHorizontal, this.m_ChildMaxWidth);
						}
						this.m_StretchableCountX += current.stretchWidth;
					}
					this.m_ChildMinWidth -= (float)(num + num2);
					this.m_ChildMaxWidth -= (float)(num + num2);
				}
				else
				{
					int num3 = 0;
					foreach (GUILayoutEntry current2 in this.entries)
					{
						current2.CalcWidth();
						bool consideredForMargin2 = current2.consideredForMargin;
						if (consideredForMargin2)
						{
							bool flag5 = !flag2;
							int num4;
							if (flag5)
							{
								num4 = ((num3 > current2.marginLeft) ? num3 : current2.marginLeft);
							}
							else
							{
								num4 = 0;
								flag2 = false;
							}
							this.m_ChildMinWidth += current2.minWidth + this.spacing + (float)num4;
							this.m_ChildMaxWidth += current2.maxWidth + this.spacing + (float)num4;
							num3 = current2.marginRight;
							this.m_StretchableCountX += current2.stretchWidth;
						}
						else
						{
							this.m_ChildMinWidth += current2.minWidth;
							this.m_ChildMaxWidth += current2.maxWidth;
							this.m_StretchableCountX += current2.stretchWidth;
						}
					}
					this.m_ChildMinWidth -= this.spacing;
					this.m_ChildMaxWidth -= this.spacing;
					bool flag6 = this.entries.Count != 0;
					if (flag6)
					{
						num = this.entries[0].marginLeft;
						num2 = num3;
					}
					else
					{
						num2 = (num = 0);
					}
				}
				bool flag7 = base.style != GUIStyle.none || this.m_UserSpecifiedWidth;
				float num5;
				float num6;
				if (flag7)
				{
					num5 = (float)Mathf.Max(base.style.padding.left, num);
					num6 = (float)Mathf.Max(base.style.padding.right, num2);
				}
				else
				{
					this.m_MarginLeft = num;
					this.m_MarginRight = num2;
					num6 = (num5 = 0f);
				}
				this.minWidth = Mathf.Max(this.minWidth, this.m_ChildMinWidth + num5 + num6);
				bool flag8 = this.maxWidth == 0f;
				if (flag8)
				{
					this.stretchWidth += this.m_StretchableCountX + (base.style.stretchWidth ? 1 : 0);
					this.maxWidth = this.m_ChildMaxWidth + num5 + num6;
				}
				else
				{
					this.stretchWidth = 0;
				}
				this.maxWidth = Mathf.Max(this.maxWidth, this.minWidth);
				bool flag9 = base.style.fixedWidth != 0f;
				if (flag9)
				{
					this.maxWidth = (this.minWidth = base.style.fixedWidth);
					this.stretchWidth = 0;
				}
			}
		}

		public override void SetHorizontal(float x, float width)
		{
			base.SetHorizontal(x, width);
			bool flag = this.resetCoords;
			if (flag)
			{
				x = 0f;
			}
			RectOffset padding = base.style.padding;
			bool flag2 = this.isVertical;
			if (flag2)
			{
				bool flag3 = base.style != GUIStyle.none;
				if (flag3)
				{
					foreach (GUILayoutEntry current in this.entries)
					{
						float num = (float)Mathf.Max(current.marginLeft, padding.left);
						float x2 = x + num;
						float num2 = width - (float)Mathf.Max(current.marginRight, padding.right) - num;
						bool flag4 = current.stretchWidth != 0;
						if (flag4)
						{
							current.SetHorizontal(x2, num2);
						}
						else
						{
							current.SetHorizontal(x2, Mathf.Clamp(num2, current.minWidth, current.maxWidth));
						}
					}
				}
				else
				{
					float num3 = x - (float)this.marginLeft;
					float num4 = width + (float)base.marginHorizontal;
					foreach (GUILayoutEntry current2 in this.entries)
					{
						bool flag5 = current2.stretchWidth != 0;
						if (flag5)
						{
							current2.SetHorizontal(num3 + (float)current2.marginLeft, num4 - (float)current2.marginHorizontal);
						}
						else
						{
							current2.SetHorizontal(num3 + (float)current2.marginLeft, Mathf.Clamp(num4 - (float)current2.marginHorizontal, current2.minWidth, current2.maxWidth));
						}
					}
				}
			}
			else
			{
				bool flag6 = base.style != GUIStyle.none;
				if (flag6)
				{
					float num5 = (float)padding.left;
					float num6 = (float)padding.right;
					bool flag7 = this.entries.Count != 0;
					if (flag7)
					{
						num5 = Mathf.Max(num5, (float)this.entries[0].marginLeft);
						num6 = Mathf.Max(num6, (float)this.entries[this.entries.Count - 1].marginRight);
					}
					x += num5;
					width -= num6 + num5;
				}
				float num7 = width - this.spacing * (float)(this.entries.Count - 1);
				float t = 0f;
				bool flag8 = this.m_ChildMinWidth != this.m_ChildMaxWidth;
				if (flag8)
				{
					t = Mathf.Clamp((num7 - this.m_ChildMinWidth) / (this.m_ChildMaxWidth - this.m_ChildMinWidth), 0f, 1f);
				}
				float num8 = 0f;
				bool flag9 = num7 > this.m_ChildMaxWidth;
				if (flag9)
				{
					bool flag10 = this.m_StretchableCountX > 0;
					if (flag10)
					{
						num8 = (num7 - this.m_ChildMaxWidth) / (float)this.m_StretchableCountX;
					}
				}
				int num9 = 0;
				bool flag11 = true;
				foreach (GUILayoutEntry current3 in this.entries)
				{
					float num10 = Mathf.Lerp(current3.minWidth, current3.maxWidth, t);
					num10 += num8 * (float)current3.stretchWidth;
					bool consideredForMargin = current3.consideredForMargin;
					if (consideredForMargin)
					{
						int num11 = current3.marginLeft;
						bool flag12 = flag11;
						if (flag12)
						{
							num11 = 0;
							flag11 = false;
						}
						int num12 = (num9 > num11) ? num9 : num11;
						x += (float)num12;
						num9 = current3.marginRight;
					}
					current3.SetHorizontal(Mathf.Round(x), Mathf.Round(num10));
					x += num10 + this.spacing;
				}
			}
		}

		public override void CalcHeight()
		{
			bool flag = this.entries.Count == 0;
			if (flag)
			{
				this.maxHeight = (this.minHeight = (float)base.style.padding.vertical);
			}
			else
			{
				int num = 0;
				int num2 = 0;
				this.m_ChildMinHeight = 0f;
				this.m_ChildMaxHeight = 0f;
				this.m_StretchableCountY = 0;
				bool flag2 = this.isVertical;
				if (flag2)
				{
					int num3 = 0;
					bool flag3 = true;
					foreach (GUILayoutEntry current in this.entries)
					{
						current.CalcHeight();
						bool consideredForMargin = current.consideredForMargin;
						if (consideredForMargin)
						{
							bool flag4 = !flag3;
							int num4;
							if (flag4)
							{
								num4 = Mathf.Max(num3, current.marginTop);
							}
							else
							{
								num4 = 0;
								flag3 = false;
							}
							this.m_ChildMinHeight += current.minHeight + this.spacing + (float)num4;
							this.m_ChildMaxHeight += current.maxHeight + this.spacing + (float)num4;
							num3 = current.marginBottom;
							this.m_StretchableCountY += current.stretchHeight;
						}
						else
						{
							this.m_ChildMinHeight += current.minHeight;
							this.m_ChildMaxHeight += current.maxHeight;
							this.m_StretchableCountY += current.stretchHeight;
						}
					}
					this.m_ChildMinHeight -= this.spacing;
					this.m_ChildMaxHeight -= this.spacing;
					bool flag5 = this.entries.Count != 0;
					if (flag5)
					{
						num = this.entries[0].marginTop;
						num2 = num3;
					}
					else
					{
						num = (num2 = 0);
					}
				}
				else
				{
					bool flag6 = true;
					foreach (GUILayoutEntry current2 in this.entries)
					{
						current2.CalcHeight();
						bool consideredForMargin2 = current2.consideredForMargin;
						if (consideredForMargin2)
						{
							bool flag7 = !flag6;
							if (flag7)
							{
								num = Mathf.Min(current2.marginTop, num);
								num2 = Mathf.Min(current2.marginBottom, num2);
							}
							else
							{
								num = current2.marginTop;
								num2 = current2.marginBottom;
								flag6 = false;
							}
							this.m_ChildMinHeight = Mathf.Max(current2.minHeight, this.m_ChildMinHeight);
							this.m_ChildMaxHeight = Mathf.Max(current2.maxHeight, this.m_ChildMaxHeight);
						}
						this.m_StretchableCountY += current2.stretchHeight;
					}
				}
				bool flag8 = base.style != GUIStyle.none || this.m_UserSpecifiedHeight;
				float num5;
				float num6;
				if (flag8)
				{
					num5 = (float)Mathf.Max(base.style.padding.top, num);
					num6 = (float)Mathf.Max(base.style.padding.bottom, num2);
				}
				else
				{
					this.m_MarginTop = num;
					this.m_MarginBottom = num2;
					num6 = (num5 = 0f);
				}
				this.minHeight = Mathf.Max(this.minHeight, this.m_ChildMinHeight + num5 + num6);
				bool flag9 = this.maxHeight == 0f;
				if (flag9)
				{
					this.stretchHeight += this.m_StretchableCountY + (base.style.stretchHeight ? 1 : 0);
					this.maxHeight = this.m_ChildMaxHeight + num5 + num6;
				}
				else
				{
					this.stretchHeight = 0;
				}
				this.maxHeight = Mathf.Max(this.maxHeight, this.minHeight);
				bool flag10 = base.style.fixedHeight != 0f;
				if (flag10)
				{
					this.maxHeight = (this.minHeight = base.style.fixedHeight);
					this.stretchHeight = 0;
				}
			}
		}

		public override void SetVertical(float y, float height)
		{
			base.SetVertical(y, height);
			bool flag = this.entries.Count == 0;
			if (!flag)
			{
				RectOffset padding = base.style.padding;
				bool flag2 = this.resetCoords;
				if (flag2)
				{
					y = 0f;
				}
				bool flag3 = this.isVertical;
				if (flag3)
				{
					bool flag4 = base.style != GUIStyle.none;
					if (flag4)
					{
						float num = (float)padding.top;
						float num2 = (float)padding.bottom;
						bool flag5 = this.entries.Count != 0;
						if (flag5)
						{
							num = Mathf.Max(num, (float)this.entries[0].marginTop);
							num2 = Mathf.Max(num2, (float)this.entries[this.entries.Count - 1].marginBottom);
						}
						y += num;
						height -= num2 + num;
					}
					float num3 = height - this.spacing * (float)(this.entries.Count - 1);
					float t = 0f;
					bool flag6 = this.m_ChildMinHeight != this.m_ChildMaxHeight;
					if (flag6)
					{
						t = Mathf.Clamp((num3 - this.m_ChildMinHeight) / (this.m_ChildMaxHeight - this.m_ChildMinHeight), 0f, 1f);
					}
					float num4 = 0f;
					bool flag7 = num3 > this.m_ChildMaxHeight;
					if (flag7)
					{
						bool flag8 = this.m_StretchableCountY > 0;
						if (flag8)
						{
							num4 = (num3 - this.m_ChildMaxHeight) / (float)this.m_StretchableCountY;
						}
					}
					int num5 = 0;
					bool flag9 = true;
					foreach (GUILayoutEntry current in this.entries)
					{
						float num6 = Mathf.Lerp(current.minHeight, current.maxHeight, t);
						num6 += num4 * (float)current.stretchHeight;
						bool consideredForMargin = current.consideredForMargin;
						if (consideredForMargin)
						{
							int num7 = current.marginTop;
							bool flag10 = flag9;
							if (flag10)
							{
								num7 = 0;
								flag9 = false;
							}
							int num8 = (num5 > num7) ? num5 : num7;
							y += (float)num8;
							num5 = current.marginBottom;
						}
						current.SetVertical(Mathf.Round(y), Mathf.Round(num6));
						y += num6 + this.spacing;
					}
				}
				else
				{
					bool flag11 = base.style != GUIStyle.none;
					if (flag11)
					{
						foreach (GUILayoutEntry current2 in this.entries)
						{
							float num9 = (float)Mathf.Max(current2.marginTop, padding.top);
							float y2 = y + num9;
							float num10 = height - (float)Mathf.Max(current2.marginBottom, padding.bottom) - num9;
							bool flag12 = current2.stretchHeight != 0;
							if (flag12)
							{
								current2.SetVertical(y2, num10);
							}
							else
							{
								current2.SetVertical(y2, Mathf.Clamp(num10, current2.minHeight, current2.maxHeight));
							}
						}
					}
					else
					{
						float num11 = y - (float)this.marginTop;
						float num12 = height + (float)base.marginVertical;
						foreach (GUILayoutEntry current3 in this.entries)
						{
							bool flag13 = current3.stretchHeight != 0;
							if (flag13)
							{
								current3.SetVertical(num11 + (float)current3.marginTop, num12 - (float)current3.marginVertical);
							}
							else
							{
								current3.SetVertical(num11 + (float)current3.marginTop, Mathf.Clamp(num12 - (float)current3.marginVertical, current3.minHeight, current3.maxHeight));
							}
						}
					}
				}
			}
		}

		public override string ToString()
		{
			string text = "";
			string text2 = "";
			for (int i = 0; i < GUILayoutEntry.indent; i++)
			{
				text2 += " ";
			}
			text = string.Concat(new string[]
			{
				text,
				base.ToString(),
				" Margins: ",
				this.m_ChildMinHeight.ToString(),
				" {\n"
			});
			GUILayoutEntry.indent += 4;
			foreach (GUILayoutEntry current in this.entries)
			{
				string arg_9E_0 = text;
				GUILayoutEntry expr_8D = current;
				text = arg_9E_0 + ((expr_8D != null) ? expr_8D.ToString() : null) + "\n";
			}
			text = text + text2 + "}";
			GUILayoutEntry.indent -= 4;
			return text;
		}
	}
}
