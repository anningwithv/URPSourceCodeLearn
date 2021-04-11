using System;

namespace UnityEngine
{
	internal struct SliderHandler
	{
		private readonly Rect position;

		private readonly float currentValue;

		private readonly float size;

		private readonly float start;

		private readonly float end;

		private readonly GUIStyle slider;

		private readonly GUIStyle thumb;

		private readonly GUIStyle thumbExtent;

		private readonly bool horiz;

		private readonly int id;

		public SliderHandler(Rect position, float currentValue, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id, GUIStyle thumbExtent = null)
		{
			this.position = position;
			this.currentValue = currentValue;
			this.size = size;
			this.start = start;
			this.end = end;
			this.slider = slider;
			this.thumb = thumb;
			this.thumbExtent = thumbExtent;
			this.horiz = horiz;
			this.id = id;
		}

		public float Handle()
		{
			bool flag = this.slider == null || this.thumb == null;
			float result;
			if (flag)
			{
				result = this.currentValue;
			}
			else
			{
				EventType eventType = this.CurrentEventType();
				EventType eventType2 = eventType;
				switch (eventType2)
				{
				case EventType.MouseDown:
					result = this.OnMouseDown();
					return result;
				case EventType.MouseUp:
					result = this.OnMouseUp();
					return result;
				case EventType.MouseMove:
					break;
				case EventType.MouseDrag:
					result = this.OnMouseDrag();
					return result;
				default:
					if (eventType2 == EventType.Repaint)
					{
						result = this.OnRepaint();
						return result;
					}
					break;
				}
				result = this.currentValue;
			}
			return result;
		}

		private float OnMouseDown()
		{
			Rect rect = this.ThumbSelectionRect();
			bool flag = GUIUtility.HitTest(rect, this.CurrentEvent());
			Rect zero = Rect.zero;
			zero.xMin = Math.Min(this.position.xMin, rect.xMin);
			zero.xMax = Math.Max(this.position.xMax, rect.xMax);
			zero.yMin = Math.Min(this.position.yMin, rect.yMin);
			zero.yMax = Math.Max(this.position.yMax, rect.yMax);
			bool flag2 = this.IsEmptySlider() || (!GUIUtility.HitTest(zero, this.CurrentEvent()) && !flag);
			float result;
			if (flag2)
			{
				result = this.currentValue;
			}
			else
			{
				GUI.scrollTroughSide = 0;
				GUIUtility.hotControl = this.id;
				this.CurrentEvent().Use();
				bool flag3 = flag;
				if (flag3)
				{
					this.StartDraggingWithValue(this.ClampedCurrentValue());
					result = this.currentValue;
				}
				else
				{
					GUI.changed = true;
					bool flag4 = this.SupportsPageMovements();
					if (flag4)
					{
						this.SliderState().isDragging = false;
						GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(250.0);
						GUI.scrollTroughSide = this.CurrentScrollTroughSide();
						result = this.PageMovementValue();
					}
					else
					{
						float num = this.ValueForCurrentMousePosition();
						this.StartDraggingWithValue(num);
						result = this.Clamp(num);
					}
				}
			}
			return result;
		}

		private float OnMouseDrag()
		{
			bool flag = GUIUtility.hotControl != this.id;
			float result;
			if (flag)
			{
				result = this.currentValue;
			}
			else
			{
				SliderState sliderState = this.SliderState();
				bool flag2 = !sliderState.isDragging;
				if (flag2)
				{
					result = this.currentValue;
				}
				else
				{
					GUI.changed = true;
					this.CurrentEvent().Use();
					float num = this.MousePosition() - sliderState.dragStartPos;
					float value = sliderState.dragStartValue + num / this.ValuesPerPixel();
					result = this.Clamp(value);
				}
			}
			return result;
		}

		private float OnMouseUp()
		{
			bool flag = GUIUtility.hotControl == this.id;
			if (flag)
			{
				this.CurrentEvent().Use();
				GUIUtility.hotControl = 0;
			}
			return this.currentValue;
		}

		private float OnRepaint()
		{
			bool flag = GUIUtility.HitTest(this.position, this.CurrentEvent());
			this.slider.Draw(this.position, GUIContent.none, this.id, false, flag);
			bool flag2 = !this.IsEmptySlider() && this.currentValue >= Mathf.Min(this.start, this.end) && this.currentValue <= Mathf.Max(this.start, this.end);
			if (flag2)
			{
				bool flag3 = this.thumbExtent != null;
				if (flag3)
				{
					this.thumbExtent.Draw(this.ThumbExtRect(), GUIContent.none, this.id, false, flag);
				}
				this.thumb.Draw(this.ThumbRect(), GUIContent.none, this.id, false, flag);
			}
			bool flag4 = GUIUtility.hotControl != this.id || !flag || this.IsEmptySlider();
			float result;
			if (flag4)
			{
				result = this.currentValue;
			}
			else
			{
				bool flag5 = GUIUtility.HitTest(this.ThumbRect(), this.CurrentEvent());
				if (flag5)
				{
					bool flag6 = GUI.scrollTroughSide != 0;
					if (flag6)
					{
						GUIUtility.hotControl = 0;
					}
					result = this.currentValue;
				}
				else
				{
					GUI.InternalRepaintEditorWindow();
					bool flag7 = SystemClock.now < GUI.nextScrollStepTime;
					if (flag7)
					{
						result = this.currentValue;
					}
					else
					{
						bool flag8 = this.CurrentScrollTroughSide() != GUI.scrollTroughSide;
						if (flag8)
						{
							result = this.currentValue;
						}
						else
						{
							GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(30.0);
							bool flag9 = this.SupportsPageMovements();
							if (flag9)
							{
								this.SliderState().isDragging = false;
								GUI.changed = true;
								result = this.PageMovementValue();
							}
							else
							{
								result = this.ClampedCurrentValue();
							}
						}
					}
				}
			}
			return result;
		}

		private EventType CurrentEventType()
		{
			return this.CurrentEvent().GetTypeForControl(this.id);
		}

		private int CurrentScrollTroughSide()
		{
			float num = this.horiz ? this.CurrentEvent().mousePosition.x : this.CurrentEvent().mousePosition.y;
			float num2 = this.horiz ? this.ThumbRect().x : this.ThumbRect().y;
			return (num > num2) ? 1 : -1;
		}

		private bool IsEmptySlider()
		{
			return this.start == this.end;
		}

		private bool SupportsPageMovements()
		{
			return this.size != 0f && GUI.usePageScrollbars;
		}

		private float PageMovementValue()
		{
			float num = this.currentValue;
			int num2 = (this.start > this.end) ? -1 : 1;
			bool flag = this.MousePosition() > this.PageUpMovementBound();
			if (flag)
			{
				num += this.size * (float)num2 * 0.9f;
			}
			else
			{
				num -= this.size * (float)num2 * 0.9f;
			}
			return this.Clamp(num);
		}

		private float PageUpMovementBound()
		{
			bool flag = this.horiz;
			float result;
			if (flag)
			{
				result = this.ThumbRect().xMax - this.position.x;
			}
			else
			{
				result = this.ThumbRect().yMax - this.position.y;
			}
			return result;
		}

		private Event CurrentEvent()
		{
			return Event.current;
		}

		private float ValueForCurrentMousePosition()
		{
			bool flag = this.horiz;
			float result;
			if (flag)
			{
				result = (this.MousePosition() - this.ThumbRect().width * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
			}
			else
			{
				result = (this.MousePosition() - this.ThumbRect().height * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
			}
			return result;
		}

		private float Clamp(float value)
		{
			return Mathf.Clamp(value, this.MinValue(), this.MaxValue());
		}

		private Rect ThumbSelectionRect()
		{
			return this.ThumbRect();
		}

		private void StartDraggingWithValue(float dragStartValue)
		{
			SliderState sliderState = this.SliderState();
			sliderState.dragStartPos = this.MousePosition();
			sliderState.dragStartValue = dragStartValue;
			sliderState.isDragging = true;
		}

		private SliderState SliderState()
		{
			return (SliderState)GUIUtility.GetStateObject(typeof(SliderState), this.id);
		}

		private Rect ThumbExtRect()
		{
			return new Rect(0f, 0f, this.thumbExtent.fixedWidth, this.thumbExtent.fixedHeight)
			{
				center = this.ThumbRect().center
			};
		}

		private Rect ThumbRect()
		{
			return this.horiz ? this.HorizontalThumbRect() : this.VerticalThumbRect();
		}

		private Rect VerticalThumbRect()
		{
			Rect rect = this.thumb.margin.Remove(this.slider.padding.Remove(this.position));
			float width = (this.thumb.fixedWidth != 0f) ? this.thumb.fixedWidth : rect.width;
			float num = this.ThumbSize();
			float num2 = this.ValuesPerPixel();
			bool flag = this.start < this.end;
			Rect result;
			if (flag)
			{
				result = new Rect(rect.x, (this.ClampedCurrentValue() - this.start) * num2 + rect.y, width, this.size * num2 + num);
			}
			else
			{
				result = new Rect(rect.x, (this.ClampedCurrentValue() + this.size - this.start) * num2 + rect.y, width, this.size * -num2 + num);
			}
			return result;
		}

		private Rect HorizontalThumbRect()
		{
			Rect rect = this.thumb.margin.Remove(this.slider.padding.Remove(this.position));
			float height = (this.thumb.fixedHeight != 0f) ? this.thumb.fixedHeight : rect.height;
			float num = this.ThumbSize();
			float num2 = this.ValuesPerPixel();
			bool flag = this.start < this.end;
			Rect result;
			if (flag)
			{
				result = new Rect((this.ClampedCurrentValue() - this.start) * num2 + rect.x, rect.y, this.size * num2 + num, height);
			}
			else
			{
				result = new Rect((this.ClampedCurrentValue() + this.size - this.start) * num2 + rect.x, rect.y, this.size * -num2 + num, height);
			}
			return result;
		}

		private float ClampedCurrentValue()
		{
			return this.Clamp(this.currentValue);
		}

		private float MousePosition()
		{
			bool flag = this.horiz;
			float result;
			if (flag)
			{
				result = this.CurrentEvent().mousePosition.x - this.position.x;
			}
			else
			{
				result = this.CurrentEvent().mousePosition.y - this.position.y;
			}
			return result;
		}

		private float ValuesPerPixel()
		{
			bool flag = this.horiz;
			float result;
			if (flag)
			{
				result = (this.position.width - (float)this.slider.padding.horizontal - this.ThumbSize()) / (this.end - this.start);
			}
			else
			{
				result = (this.position.height - (float)this.slider.padding.vertical - this.ThumbSize()) / (this.end - this.start);
			}
			return result;
		}

		private float ThumbSize()
		{
			bool flag = this.horiz;
			float result;
			if (flag)
			{
				result = ((this.thumb.fixedWidth != 0f) ? this.thumb.fixedWidth : ((float)this.thumb.padding.horizontal));
			}
			else
			{
				result = ((this.thumb.fixedHeight != 0f) ? this.thumb.fixedHeight : ((float)this.thumb.padding.vertical));
			}
			return result;
		}

		private float MaxValue()
		{
			return Mathf.Max(this.start, this.end) - this.size;
		}

		private float MinValue()
		{
			return Mathf.Min(this.start, this.end);
		}
	}
}
