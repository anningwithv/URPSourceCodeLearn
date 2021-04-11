using System;

namespace UnityEngine.UIElements
{
	public class MinMaxSlider : BaseField<Vector2>
	{
		public new class UxmlFactory : UxmlFactory<MinMaxSlider, MinMaxSlider.UxmlTraits>
		{
		}

		public new class UxmlTraits : BaseField<Vector2>.UxmlTraits
		{
			private UxmlFloatAttributeDescription m_MinValue = new UxmlFloatAttributeDescription
			{
				name = "min-value",
				defaultValue = 0f
			};

			private UxmlFloatAttributeDescription m_MaxValue = new UxmlFloatAttributeDescription
			{
				name = "max-value",
				defaultValue = 10f
			};

			private UxmlFloatAttributeDescription m_LowLimit = new UxmlFloatAttributeDescription
			{
				name = "low-limit",
				defaultValue = -3.40282347E+38f
			};

			private UxmlFloatAttributeDescription m_HighLimit = new UxmlFloatAttributeDescription
			{
				name = "high-limit",
				defaultValue = 3.40282347E+38f
			};

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				MinMaxSlider minMaxSlider = (MinMaxSlider)ve;
				minMaxSlider.minValue = this.m_MinValue.GetValueFromBag(bag, cc);
				minMaxSlider.maxValue = this.m_MaxValue.GetValueFromBag(bag, cc);
				minMaxSlider.lowLimit = this.m_LowLimit.GetValueFromBag(bag, cc);
				minMaxSlider.highLimit = this.m_HighLimit.GetValueFromBag(bag, cc);
			}
		}

		private enum DragState
		{
			NoThumb,
			MinThumb,
			MiddleThumb,
			MaxThumb
		}

		private Vector2 m_DragElementStartPos;

		private Vector2 m_ValueStartPos;

		private Rect m_DragMinThumbRect;

		private Rect m_DragMaxThumbRect;

		private MinMaxSlider.DragState m_DragState;

		private float m_MinLimit;

		private float m_MaxLimit;

		internal const float kDefaultHighValue = 10f;

		public new static readonly string ussClassName = "unity-min-max-slider";

		public new static readonly string labelUssClassName = MinMaxSlider.ussClassName + "__label";

		public new static readonly string inputUssClassName = MinMaxSlider.ussClassName + "__input";

		public static readonly string trackerUssClassName = MinMaxSlider.ussClassName + "__tracker";

		public static readonly string draggerUssClassName = MinMaxSlider.ussClassName + "__dragger";

		public static readonly string minThumbUssClassName = MinMaxSlider.ussClassName + "__min-thumb";

		public static readonly string maxThumbUssClassName = MinMaxSlider.ussClassName + "__max-thumb";

		internal VisualElement dragElement
		{
			get;
			private set;
		}

		private VisualElement dragMinThumb
		{
			get;
			set;
		}

		private VisualElement dragMaxThumb
		{
			get;
			set;
		}

		internal ClampedDragger<float> clampedDragger
		{
			get;
			private set;
		}

		public float minValue
		{
			get
			{
				return this.value.x;
			}
			set
			{
				base.value = this.ClampValues(new Vector2(value, base.rawValue.y));
			}
		}

		public float maxValue
		{
			get
			{
				return this.value.y;
			}
			set
			{
				base.value = this.ClampValues(new Vector2(base.rawValue.x, value));
			}
		}

		public override Vector2 value
		{
			get
			{
				return base.value;
			}
			set
			{
				base.value = this.ClampValues(value);
			}
		}

		public float range
		{
			get
			{
				return Math.Abs(this.highLimit - this.lowLimit);
			}
		}

		public float lowLimit
		{
			get
			{
				return this.m_MinLimit;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_MinLimit, value);
				if (flag)
				{
					bool flag2 = value > this.m_MaxLimit;
					if (flag2)
					{
						throw new ArgumentException("lowLimit is greater than highLimit");
					}
					this.m_MinLimit = value;
					this.value = base.rawValue;
					this.UpdateDragElementPosition();
					bool flag3 = !string.IsNullOrEmpty(base.viewDataKey);
					if (flag3)
					{
						base.SaveViewData();
					}
				}
			}
		}

		public float highLimit
		{
			get
			{
				return this.m_MaxLimit;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_MaxLimit, value);
				if (flag)
				{
					bool flag2 = value < this.m_MinLimit;
					if (flag2)
					{
						throw new ArgumentException("highLimit is smaller than lowLimit");
					}
					this.m_MaxLimit = value;
					this.value = base.rawValue;
					this.UpdateDragElementPosition();
					bool flag3 = !string.IsNullOrEmpty(base.viewDataKey);
					if (flag3)
					{
						base.SaveViewData();
					}
				}
			}
		}

		public override void SetValueWithoutNotify(Vector2 newValue)
		{
			base.SetValueWithoutNotify(this.ClampValues(newValue));
			this.UpdateDragElementPosition();
		}

		public MinMaxSlider() : this(null, 0f, 10f, -3.40282347E+38f, 3.40282347E+38f)
		{
		}

		public MinMaxSlider(float minValue, float maxValue, float minLimit, float maxLimit) : this(null, minValue, maxValue, minLimit, maxLimit)
		{
		}

		public MinMaxSlider(string label, float minValue = 0f, float maxValue = 10f, float minLimit = -3.40282347E+38f, float maxLimit = 3.40282347E+38f) : base(label, null)
		{
			this.lowLimit = minLimit;
			this.highLimit = maxLimit;
			this.minValue = minValue;
			this.maxValue = maxValue;
			base.AddToClassList(MinMaxSlider.ussClassName);
			base.labelElement.AddToClassList(MinMaxSlider.labelUssClassName);
			base.visualInput.AddToClassList(MinMaxSlider.inputUssClassName);
			base.pickingMode = PickingMode.Ignore;
			this.m_DragState = MinMaxSlider.DragState.NoThumb;
			base.visualInput.pickingMode = PickingMode.Position;
			VisualElement visualElement = new VisualElement
			{
				name = "unity-tracker"
			};
			visualElement.AddToClassList(MinMaxSlider.trackerUssClassName);
			base.visualInput.Add(visualElement);
			this.dragElement = new VisualElement
			{
				name = "unity-dragger"
			};
			this.dragElement.AddToClassList(MinMaxSlider.draggerUssClassName);
			this.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.UpdateDragElementPosition), TrickleDown.NoTrickleDown);
			base.visualInput.Add(this.dragElement);
			this.dragMinThumb = new VisualElement
			{
				name = "unity-thumb-min"
			};
			this.dragMaxThumb = new VisualElement
			{
				name = "unity-thumb-max"
			};
			this.dragMinThumb.AddToClassList(MinMaxSlider.minThumbUssClassName);
			this.dragMaxThumb.AddToClassList(MinMaxSlider.maxThumbUssClassName);
			this.dragElement.Add(this.dragMinThumb);
			this.dragElement.Add(this.dragMaxThumb);
			this.clampedDragger = new ClampedDragger<float>(null, new Action(this.SetSliderValueFromClick), new Action(this.SetSliderValueFromDrag));
			base.visualInput.AddManipulator(this.clampedDragger);
			this.m_MinLimit = minLimit;
			this.m_MaxLimit = maxLimit;
			base.rawValue = this.ClampValues(new Vector2(minValue, maxValue));
			this.UpdateDragElementPosition();
		}

		private Vector2 ClampValues(Vector2 valueToClamp)
		{
			bool flag = this.m_MinLimit > this.m_MaxLimit;
			if (flag)
			{
				this.m_MinLimit = this.m_MaxLimit;
			}
			Vector2 result = default(Vector2);
			bool flag2 = valueToClamp.y > this.m_MaxLimit;
			if (flag2)
			{
				valueToClamp.y = this.m_MaxLimit;
			}
			result.x = Mathf.Clamp(valueToClamp.x, this.m_MinLimit, valueToClamp.y);
			result.y = Mathf.Clamp(valueToClamp.y, valueToClamp.x, this.m_MaxLimit);
			return result;
		}

		private void UpdateDragElementPosition(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateDragElementPosition();
			}
		}

		private void UpdateDragElementPosition()
		{
			bool flag = base.panel == null;
			if (!flag)
			{
				float num = -this.dragElement.resolvedStyle.marginLeft - this.dragElement.resolvedStyle.marginRight;
				int num2 = this.dragElement.resolvedStyle.unitySliceLeft + this.dragElement.resolvedStyle.unitySliceRight;
				float num3 = Mathf.Round(this.SliderLerpUnclamped((float)this.dragElement.resolvedStyle.unitySliceLeft, base.visualInput.layout.width + num - (float)this.dragElement.resolvedStyle.unitySliceRight, this.SliderNormalizeValue(this.minValue, this.lowLimit, this.highLimit)) - (float)this.dragElement.resolvedStyle.unitySliceLeft);
				float num4 = Mathf.Round(this.SliderLerpUnclamped((float)this.dragElement.resolvedStyle.unitySliceLeft, base.visualInput.layout.width + num - (float)this.dragElement.resolvedStyle.unitySliceRight, this.SliderNormalizeValue(this.maxValue, this.lowLimit, this.highLimit)) + (float)this.dragElement.resolvedStyle.unitySliceRight);
				this.dragElement.style.width = Mathf.Max((float)num2, num4 - num3);
				this.dragElement.style.left = num3;
				this.m_DragMinThumbRect = new Rect(this.dragElement.resolvedStyle.left, this.dragElement.layout.yMin, (float)this.dragElement.resolvedStyle.unitySliceLeft, this.dragElement.resolvedStyle.height);
				this.m_DragMaxThumbRect = new Rect(this.dragElement.resolvedStyle.left + (this.dragElement.resolvedStyle.width - (float)this.dragElement.resolvedStyle.unitySliceRight), this.dragElement.layout.yMin, (float)this.dragElement.resolvedStyle.unitySliceRight, this.dragElement.resolvedStyle.height);
				this.dragMaxThumb.style.left = this.dragElement.resolvedStyle.width - (float)this.dragElement.resolvedStyle.unitySliceRight;
				this.dragMaxThumb.style.top = 0f;
				this.dragMinThumb.style.width = this.m_DragMinThumbRect.width;
				this.dragMinThumb.style.height = this.m_DragMinThumbRect.height;
				this.dragMinThumb.style.left = 0f;
				this.dragMinThumb.style.top = 0f;
				this.dragMaxThumb.style.width = this.m_DragMaxThumbRect.width;
				this.dragMaxThumb.style.height = this.m_DragMaxThumbRect.height;
			}
		}

		internal float SliderLerpUnclamped(float a, float b, float interpolant)
		{
			return Mathf.LerpUnclamped(a, b, interpolant);
		}

		internal float SliderNormalizeValue(float currentValue, float lowerValue, float higherValue)
		{
			return (currentValue - lowerValue) / (higherValue - lowerValue);
		}

		private float ComputeValueFromPosition(float positionToConvert)
		{
			float interpolant = this.SliderNormalizeValue(positionToConvert, (float)this.dragElement.resolvedStyle.unitySliceLeft, base.visualInput.layout.width - (float)this.dragElement.resolvedStyle.unitySliceRight);
			return this.SliderLerpUnclamped(this.lowLimit, this.highLimit, interpolant);
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

		private void SetSliderValueFromDrag()
		{
			bool flag = this.clampedDragger.dragDirection != ClampedDragger<float>.DragDirection.Free;
			if (!flag)
			{
				float x = this.m_DragElementStartPos.x;
				float dragElementEndPos = x + this.clampedDragger.delta.x;
				this.ComputeValueFromDraggingThumb(x, dragElementEndPos);
			}
		}

		private void SetSliderValueFromClick()
		{
			bool flag = this.clampedDragger.dragDirection == ClampedDragger<float>.DragDirection.Free;
			if (!flag)
			{
				bool flag2 = this.m_DragMinThumbRect.Contains(this.clampedDragger.startMousePosition);
				if (flag2)
				{
					this.m_DragState = MinMaxSlider.DragState.MinThumb;
				}
				else
				{
					bool flag3 = this.m_DragMaxThumbRect.Contains(this.clampedDragger.startMousePosition);
					if (flag3)
					{
						this.m_DragState = MinMaxSlider.DragState.MaxThumb;
					}
					else
					{
						bool flag4 = this.dragElement.layout.Contains(this.clampedDragger.startMousePosition);
						if (flag4)
						{
							this.m_DragState = MinMaxSlider.DragState.MiddleThumb;
						}
						else
						{
							this.m_DragState = MinMaxSlider.DragState.NoThumb;
						}
					}
				}
				bool flag5 = this.m_DragState == MinMaxSlider.DragState.NoThumb;
				if (flag5)
				{
					this.m_DragElementStartPos = new Vector2(this.clampedDragger.startMousePosition.x, this.dragElement.resolvedStyle.top);
					this.clampedDragger.dragDirection = ClampedDragger<float>.DragDirection.Free;
					this.ComputeValueDragStateNoThumb((float)this.dragElement.resolvedStyle.unitySliceLeft, base.visualInput.layout.width - (float)this.dragElement.resolvedStyle.unitySliceRight, this.m_DragElementStartPos.x);
					this.m_DragState = MinMaxSlider.DragState.MiddleThumb;
					this.m_ValueStartPos = this.value;
				}
				else
				{
					this.m_ValueStartPos = this.value;
					this.clampedDragger.dragDirection = ClampedDragger<float>.DragDirection.Free;
					this.m_DragElementStartPos = this.clampedDragger.startMousePosition;
				}
			}
		}

		private void ComputeValueDragStateNoThumb(float lowLimitPosition, float highLimitPosition, float dragElementPos)
		{
			bool flag = dragElementPos < lowLimitPosition;
			float num;
			if (flag)
			{
				num = this.lowLimit;
			}
			else
			{
				bool flag2 = dragElementPos > highLimitPosition;
				if (flag2)
				{
					num = this.highLimit;
				}
				else
				{
					num = this.ComputeValueFromPosition(dragElementPos);
				}
			}
			float num2 = this.maxValue - this.minValue;
			float num3 = num - num2;
			float y = num;
			bool flag3 = num3 < this.lowLimit;
			if (flag3)
			{
				num3 = this.lowLimit;
				y = num3 + num2;
			}
			this.value = new Vector2(num3, y);
		}

		private void ComputeValueFromDraggingThumb(float dragElementStartPos, float dragElementEndPos)
		{
			float num = this.ComputeValueFromPosition(dragElementStartPos);
			float num2 = this.ComputeValueFromPosition(dragElementEndPos);
			float num3 = num2 - num;
			switch (this.m_DragState)
			{
			case MinMaxSlider.DragState.MinThumb:
			{
				float num4 = this.m_ValueStartPos.x + num3;
				bool flag = num4 > this.maxValue;
				if (flag)
				{
					num4 = this.maxValue;
				}
				else
				{
					bool flag2 = num4 < this.lowLimit;
					if (flag2)
					{
						num4 = this.lowLimit;
					}
				}
				this.value = new Vector2(num4, this.maxValue);
				break;
			}
			case MinMaxSlider.DragState.MiddleThumb:
			{
				Vector2 value = this.value;
				value.x = this.m_ValueStartPos.x + num3;
				value.y = this.m_ValueStartPos.y + num3;
				float num5 = this.m_ValueStartPos.y - this.m_ValueStartPos.x;
				bool flag3 = value.x < this.lowLimit;
				if (flag3)
				{
					value.x = this.lowLimit;
					value.y = this.lowLimit + num5;
				}
				else
				{
					bool flag4 = value.y > this.highLimit;
					if (flag4)
					{
						value.y = this.highLimit;
						value.x = this.highLimit - num5;
					}
				}
				this.value = value;
				break;
			}
			case MinMaxSlider.DragState.MaxThumb:
			{
				float num6 = this.m_ValueStartPos.y + num3;
				bool flag5 = num6 < this.minValue;
				if (flag5)
				{
					num6 = this.minValue;
				}
				else
				{
					bool flag6 = num6 > this.highLimit;
					if (flag6)
					{
						num6 = this.highLimit;
					}
				}
				this.value = new Vector2(this.minValue, num6);
				break;
			}
			}
		}
	}
}
