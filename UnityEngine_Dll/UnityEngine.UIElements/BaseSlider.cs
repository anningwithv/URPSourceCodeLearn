using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public abstract class BaseSlider<TValueType> : BaseField<TValueType> where TValueType : IComparable<TValueType>
	{
		internal enum SliderKey
		{
			None,
			Lowest,
			LowerPage,
			Lower,
			Higher,
			HigherPage,
			Highest
		}

		[SerializeField]
		private TValueType m_LowValue;

		[SerializeField]
		private TValueType m_HighValue;

		private float m_PageSize;

		private bool m_ShowInputField = false;

		private Rect m_DragElementStartPos;

		private SliderDirection m_Direction;

		internal const float kDefaultPageSize = 0f;

		internal const bool kDefaultShowInputField = false;

		public new static readonly string ussClassName = "unity-base-slider";

		public new static readonly string labelUssClassName = BaseSlider<TValueType>.ussClassName + "__label";

		public new static readonly string inputUssClassName = BaseSlider<TValueType>.ussClassName + "__input";

		public static readonly string horizontalVariantUssClassName = BaseSlider<TValueType>.ussClassName + "--horizontal";

		public static readonly string verticalVariantUssClassName = BaseSlider<TValueType>.ussClassName + "--vertical";

		public static readonly string dragContainerUssClassName = BaseSlider<TValueType>.ussClassName + "__drag-container";

		public static readonly string trackerUssClassName = BaseSlider<TValueType>.ussClassName + "__tracker";

		public static readonly string draggerUssClassName = BaseSlider<TValueType>.ussClassName + "__dragger";

		public static readonly string draggerBorderUssClassName = BaseSlider<TValueType>.ussClassName + "__dragger-border";

		public static readonly string textFieldClassName = BaseSlider<TValueType>.ussClassName + "__text-field";

		internal VisualElement dragContainer
		{
			get;
			private set;
		}

		internal VisualElement dragElement
		{
			get;
			private set;
		}

		internal VisualElement dragBorderElement
		{
			get;
			private set;
		}

		internal TextField inputTextField
		{
			get;
			private set;
		}

		public TValueType lowValue
		{
			get
			{
				return this.m_LowValue;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_LowValue, value);
				if (flag)
				{
					this.m_LowValue = value;
					this.ClampValue();
					this.UpdateDragElementPosition();
					base.SaveViewData();
				}
			}
		}

		public TValueType highValue
		{
			get
			{
				return this.m_HighValue;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_HighValue, value);
				if (flag)
				{
					this.m_HighValue = value;
					this.ClampValue();
					this.UpdateDragElementPosition();
					base.SaveViewData();
				}
			}
		}

		public TValueType range
		{
			get
			{
				return this.SliderRange();
			}
		}

		public virtual float pageSize
		{
			get
			{
				return this.m_PageSize;
			}
			set
			{
				this.m_PageSize = value;
			}
		}

		public virtual bool showInputField
		{
			get
			{
				return this.m_ShowInputField;
			}
			set
			{
				bool flag = this.m_ShowInputField != value;
				if (flag)
				{
					this.m_ShowInputField = value;
					this.UpdateTextFieldVisibility();
				}
			}
		}

		internal bool clamped
		{
			get;
			set;
		}

		internal ClampedDragger<TValueType> clampedDragger
		{
			get;
			private set;
		}

		public override TValueType value
		{
			get
			{
				return base.value;
			}
			set
			{
				TValueType value2 = this.clamped ? this.GetClampedValue(value) : value;
				base.value = value2;
			}
		}

		public SliderDirection direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				this.m_Direction = value;
				bool flag = this.m_Direction == SliderDirection.Horizontal;
				if (flag)
				{
					base.RemoveFromClassList(BaseSlider<TValueType>.verticalVariantUssClassName);
					base.AddToClassList(BaseSlider<TValueType>.horizontalVariantUssClassName);
				}
				else
				{
					base.RemoveFromClassList(BaseSlider<TValueType>.horizontalVariantUssClassName);
					base.AddToClassList(BaseSlider<TValueType>.verticalVariantUssClassName);
				}
			}
		}

		private TValueType Clamp(TValueType value, TValueType lowBound, TValueType highBound)
		{
			TValueType result = value;
			bool flag = lowBound.CompareTo(value) > 0;
			if (flag)
			{
				result = lowBound;
			}
			else
			{
				bool flag2 = highBound.CompareTo(value) < 0;
				if (flag2)
				{
					result = highBound;
				}
			}
			return result;
		}

		private TValueType GetClampedValue(TValueType newValue)
		{
			TValueType tValueType = this.lowValue;
			TValueType tValueType2 = this.highValue;
			bool flag = tValueType.CompareTo(tValueType2) > 0;
			if (flag)
			{
				TValueType tValueType3 = tValueType;
				tValueType = tValueType2;
				tValueType2 = tValueType3;
			}
			return this.Clamp(newValue, tValueType, tValueType2);
		}

		public override void SetValueWithoutNotify(TValueType newValue)
		{
			TValueType valueWithoutNotify = this.clamped ? this.GetClampedValue(newValue) : newValue;
			base.SetValueWithoutNotify(valueWithoutNotify);
			this.UpdateDragElementPosition();
			this.UpdateTextFieldValue();
		}

		internal BaseSlider(string label, TValueType start, TValueType end, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
		{
			this.<clamped>k__BackingField = true;
			base..ctor(label, null);
			base.AddToClassList(BaseSlider<TValueType>.ussClassName);
			base.labelElement.AddToClassList(BaseSlider<TValueType>.labelUssClassName);
			base.visualInput.AddToClassList(BaseSlider<TValueType>.inputUssClassName);
			this.direction = direction;
			this.pageSize = pageSize;
			this.lowValue = start;
			this.highValue = end;
			base.pickingMode = PickingMode.Ignore;
			base.visualInput.pickingMode = PickingMode.Position;
			this.dragContainer = new VisualElement
			{
				name = "unity-drag-container"
			};
			this.dragContainer.AddToClassList(BaseSlider<TValueType>.dragContainerUssClassName);
			base.visualInput.Add(this.dragContainer);
			VisualElement visualElement = new VisualElement
			{
				name = "unity-tracker"
			};
			visualElement.AddToClassList(BaseSlider<TValueType>.trackerUssClassName);
			this.dragContainer.Add(visualElement);
			this.dragBorderElement = new VisualElement
			{
				name = "unity-dragger-border"
			};
			this.dragBorderElement.AddToClassList(BaseSlider<TValueType>.draggerBorderUssClassName);
			this.dragContainer.Add(this.dragBorderElement);
			this.dragElement = new VisualElement
			{
				name = "unity-dragger"
			};
			this.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.UpdateDragElementPosition), TrickleDown.NoTrickleDown);
			this.dragElement.AddToClassList(BaseSlider<TValueType>.draggerUssClassName);
			this.dragContainer.Add(this.dragElement);
			this.clampedDragger = new ClampedDragger<TValueType>(this, new Action(this.SetSliderValueFromClick), new Action(this.SetSliderValueFromDrag));
			base.visualInput.AddManipulator(this.clampedDragger);
			base.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
			this.UpdateTextFieldVisibility();
		}

		protected static float GetClosestPowerOfTen(float positiveNumber)
		{
			bool flag = positiveNumber <= 0f;
			float result;
			if (flag)
			{
				result = 1f;
			}
			else
			{
				result = Mathf.Pow(10f, (float)Mathf.RoundToInt(Mathf.Log10(positiveNumber)));
			}
			return result;
		}

		protected static float RoundToMultipleOf(float value, float roundingValue)
		{
			bool flag = roundingValue == 0f;
			float result;
			if (flag)
			{
				result = value;
			}
			else
			{
				result = Mathf.Round(value / roundingValue) * roundingValue;
			}
			return result;
		}

		private void ClampValue()
		{
			this.value = base.rawValue;
		}

		internal abstract TValueType SliderLerpUnclamped(TValueType a, TValueType b, float interpolant);

		internal abstract float SliderNormalizeValue(TValueType currentValue, TValueType lowerValue, TValueType higherValue);

		internal abstract TValueType SliderRange();

		internal abstract TValueType ParseStringToValue(string stringValue);

		internal abstract void ComputeValueFromKey(BaseSlider<TValueType>.SliderKey sliderKey, bool isShift);

		private void SetSliderValueFromDrag()
		{
			bool flag = this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.Free;
			if (!flag)
			{
				Vector2 delta = this.clampedDragger.delta;
				bool flag2 = this.direction == SliderDirection.Horizontal;
				if (flag2)
				{
					this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.m_DragElementStartPos.x + delta.x);
				}
				else
				{
					this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.m_DragElementStartPos.y + delta.y);
				}
			}
		}

		private void ComputeValueAndDirectionFromDrag(float sliderLength, float dragElementLength, float dragElementPos)
		{
			float num = sliderLength - dragElementLength;
			bool flag = Mathf.Abs(num) < Mathf.Epsilon;
			if (!flag)
			{
				float interpolant = Mathf.Max(0f, Mathf.Min(dragElementPos, num)) / num;
				this.value = this.SliderLerpUnclamped(this.lowValue, this.highValue, interpolant);
			}
		}

		private void SetSliderValueFromClick()
		{
			bool flag = this.clampedDragger.dragDirection == ClampedDragger<TValueType>.DragDirection.Free;
			if (!flag)
			{
				bool flag2 = this.clampedDragger.dragDirection == ClampedDragger<TValueType>.DragDirection.None;
				if (flag2)
				{
					bool flag3 = Mathf.Approximately(this.pageSize, 0f);
					if (flag3)
					{
						float x = (this.direction == SliderDirection.Horizontal) ? (this.clampedDragger.startMousePosition.x - this.dragElement.resolvedStyle.width / 2f) : this.dragElement.transform.position.x;
						float y = (this.direction == SliderDirection.Horizontal) ? this.dragElement.transform.position.y : (this.clampedDragger.startMousePosition.y - this.dragElement.resolvedStyle.height / 2f);
						Vector3 position = new Vector3(x, y, 0f);
						this.dragElement.transform.position = position;
						this.dragBorderElement.transform.position = position;
						this.m_DragElementStartPos = new Rect(x, y, this.dragElement.resolvedStyle.width, this.dragElement.resolvedStyle.height);
						this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.Free;
						bool flag4 = this.direction == SliderDirection.Horizontal;
						if (flag4)
						{
							this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.m_DragElementStartPos.x);
						}
						else
						{
							this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.m_DragElementStartPos.y);
						}
						return;
					}
					this.m_DragElementStartPos = new Rect(this.dragElement.transform.position.x, this.dragElement.transform.position.y, this.dragElement.resolvedStyle.width, this.dragElement.resolvedStyle.height);
				}
				bool flag5 = this.direction == SliderDirection.Horizontal;
				if (flag5)
				{
					this.ComputeValueAndDirectionFromClick(this.dragContainer.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.dragElement.transform.position.x, this.clampedDragger.lastMousePosition.x);
				}
				else
				{
					this.ComputeValueAndDirectionFromClick(this.dragContainer.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.dragElement.transform.position.y, this.clampedDragger.lastMousePosition.y);
				}
			}
		}

		private void OnKeyDown(KeyDownEvent evt)
		{
			BaseSlider<TValueType>.SliderKey sliderKey = BaseSlider<TValueType>.SliderKey.None;
			bool flag = this.direction == SliderDirection.Horizontal;
			bool flag2 = (flag && evt.keyCode == KeyCode.Home) || (!flag && evt.keyCode == KeyCode.End);
			if (flag2)
			{
				sliderKey = BaseSlider<TValueType>.SliderKey.Lowest;
			}
			else
			{
				bool flag3 = (flag && evt.keyCode == KeyCode.End) || (!flag && evt.keyCode == KeyCode.Home);
				if (flag3)
				{
					sliderKey = BaseSlider<TValueType>.SliderKey.Highest;
				}
				else
				{
					bool flag4 = (flag && evt.keyCode == KeyCode.PageUp) || (!flag && evt.keyCode == KeyCode.PageDown);
					if (flag4)
					{
						sliderKey = BaseSlider<TValueType>.SliderKey.LowerPage;
					}
					else
					{
						bool flag5 = (flag && evt.keyCode == KeyCode.PageDown) || (!flag && evt.keyCode == KeyCode.PageUp);
						if (flag5)
						{
							sliderKey = BaseSlider<TValueType>.SliderKey.HigherPage;
						}
						else
						{
							bool flag6 = (flag && evt.keyCode == KeyCode.LeftArrow) || (!flag && evt.keyCode == KeyCode.DownArrow);
							if (flag6)
							{
								sliderKey = BaseSlider<TValueType>.SliderKey.Lower;
							}
							else
							{
								bool flag7 = (flag && evt.keyCode == KeyCode.RightArrow) || (!flag && evt.keyCode == KeyCode.UpArrow);
								if (flag7)
								{
									sliderKey = BaseSlider<TValueType>.SliderKey.Higher;
								}
							}
						}
					}
				}
			}
			bool flag8 = sliderKey == BaseSlider<TValueType>.SliderKey.None;
			if (!flag8)
			{
				this.ComputeValueFromKey(sliderKey, evt.shiftKey);
				evt.StopPropagation();
			}
		}

		internal virtual void ComputeValueAndDirectionFromClick(float sliderLength, float dragElementLength, float dragElementPos, float dragElementLastPos)
		{
			float num = sliderLength - dragElementLength;
			bool flag = Mathf.Abs(num) < Mathf.Epsilon;
			if (!flag)
			{
				bool flag2 = dragElementLastPos < dragElementPos && this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.LowToHigh;
				if (flag2)
				{
					this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.HighToLow;
					float interpolant = Mathf.Max(0f, Mathf.Min(dragElementPos - this.pageSize, num)) / num;
					this.value = this.SliderLerpUnclamped(this.lowValue, this.highValue, interpolant);
				}
				else
				{
					bool flag3 = dragElementLastPos > dragElementPos + dragElementLength && this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.HighToLow;
					if (flag3)
					{
						this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.LowToHigh;
						float interpolant2 = Mathf.Max(0f, Mathf.Min(dragElementPos + this.pageSize, num)) / num;
						this.value = this.SliderLerpUnclamped(this.lowValue, this.highValue, interpolant2);
					}
				}
			}
		}

		public void AdjustDragElement(float factor)
		{
			bool flag = factor < 1f;
			this.dragElement.visible = flag;
			bool flag2 = flag;
			if (flag2)
			{
				IStyle style = this.dragElement.style;
				this.dragElement.visible = true;
				bool flag3 = this.direction == SliderDirection.Horizontal;
				if (flag3)
				{
					float b = (base.resolvedStyle.minWidth == StyleKeyword.Auto) ? 0f : base.resolvedStyle.minWidth.value;
					style.width = Mathf.Round(Mathf.Max(this.dragContainer.layout.width * factor, b));
				}
				else
				{
					float b2 = (base.resolvedStyle.minHeight == StyleKeyword.Auto) ? 0f : base.resolvedStyle.minHeight.value;
					style.height = Mathf.Round(Mathf.Max(this.dragContainer.layout.height * factor, b2));
				}
			}
			this.dragBorderElement.visible = this.dragElement.visible;
		}

		private void UpdateDragElementPosition(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateDragElementPosition();
			}
		}

		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			this.UpdateDragElementPosition();
		}

		private bool SameValues(float a, float b, float epsilon)
		{
			return Mathf.Abs(b - a) < epsilon;
		}

		private void UpdateDragElementPosition()
		{
			bool flag = base.panel == null;
			if (!flag)
			{
				float num = this.SliderNormalizeValue(this.value, this.lowValue, this.highValue);
				float epsilon = base.scaledPixelsPerPoint * 0.5f;
				bool flag2 = this.direction == SliderDirection.Horizontal;
				if (flag2)
				{
					float width = this.dragElement.resolvedStyle.width;
					float num2 = -this.dragElement.resolvedStyle.marginLeft - this.dragElement.resolvedStyle.marginRight;
					float num3 = this.dragContainer.layout.width - width + num2;
					float num4 = num * num3;
					bool flag3 = float.IsNaN(num4);
					if (!flag3)
					{
						float x = this.dragElement.transform.position.x;
						bool flag4 = !this.SameValues(x, num4, epsilon);
						if (flag4)
						{
							Vector3 position = new Vector3(num4, 0f, 0f);
							this.dragElement.transform.position = position;
							this.dragBorderElement.transform.position = position;
						}
					}
				}
				else
				{
					float height = this.dragElement.resolvedStyle.height;
					float num5 = this.dragContainer.resolvedStyle.height - height;
					float num6 = num * num5;
					bool flag5 = float.IsNaN(num6);
					if (!flag5)
					{
						float y = this.dragElement.transform.position.y;
						bool flag6 = !this.SameValues(y, num6, epsilon);
						if (flag6)
						{
							Vector3 position2 = new Vector3(0f, num6, 0f);
							this.dragElement.transform.position = position2;
							this.dragBorderElement.transform.position = position2;
						}
					}
				}
			}
		}

		protected override void ExecuteDefaultAction(EventBase evt)
		{
			base.ExecuteDefaultAction(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<GeometryChangedEvent>.TypeId();
				if (flag2)
				{
					this.UpdateDragElementPosition((GeometryChangedEvent)evt);
				}
			}
		}

		private void UpdateTextFieldVisibility()
		{
			bool showInputField = this.showInputField;
			if (showInputField)
			{
				bool flag = this.inputTextField == null;
				if (flag)
				{
					this.inputTextField = new TextField
					{
						name = "unity-text-field"
					};
					this.inputTextField.AddToClassList(BaseSlider<TValueType>.textFieldClassName);
					this.inputTextField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnTextFieldValueChange));
					this.inputTextField.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnTextFieldFocusOut), TrickleDown.NoTrickleDown);
					base.visualInput.Add(this.inputTextField);
					this.UpdateTextFieldValue();
				}
			}
			else
			{
				bool flag2 = this.inputTextField != null && this.inputTextField.panel != null;
				if (flag2)
				{
					bool flag3 = this.inputTextField.panel != null;
					if (flag3)
					{
						this.inputTextField.RemoveFromHierarchy();
					}
					this.inputTextField.UnregisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnTextFieldValueChange));
					this.inputTextField.UnregisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnTextFieldFocusOut), TrickleDown.NoTrickleDown);
					this.inputTextField = null;
				}
			}
		}

		private void UpdateTextFieldValue()
		{
			bool flag = this.inputTextField == null;
			if (!flag)
			{
				this.inputTextField.SetValueWithoutNotify(string.Format("{0:0.##}", this.value));
			}
		}

		private void OnTextFieldFocusOut(FocusOutEvent evt)
		{
			this.UpdateTextFieldValue();
		}

		private void OnTextFieldValueChange(ChangeEvent<string> evt)
		{
			TValueType clampedValue = this.GetClampedValue(this.ParseStringToValue(evt.newValue));
			bool flag = !EqualityComparer<TValueType>.Default.Equals(clampedValue, this.value);
			if (flag)
			{
				this.value = clampedValue;
				evt.StopPropagation();
				bool flag2 = base.elementPanel != null;
				if (flag2)
				{
					this.OnViewDataReady();
				}
			}
		}
	}
}
