using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	public class Slider : BaseSlider<float>
	{
		public new class UxmlFactory : UxmlFactory<Slider, Slider.UxmlTraits>
		{
		}

		public new class UxmlTraits : BaseFieldTraits<float, UxmlFloatAttributeDescription>
		{
			private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
			{
				name = "low-value"
			};

			private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
			{
				name = "high-value",
				defaultValue = 10f
			};

			private UxmlFloatAttributeDescription m_PageSize = new UxmlFloatAttributeDescription
			{
				name = "page-size",
				defaultValue = 0f
			};

			private UxmlBoolAttributeDescription m_ShowInputField = new UxmlBoolAttributeDescription
			{
				name = "show-input-field",
				defaultValue = false
			};

			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Horizontal
			};

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				Slider slider = (Slider)ve;
				slider.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				slider.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				slider.direction = this.m_Direction.GetValueFromBag(bag, cc);
				slider.pageSize = this.m_PageSize.GetValueFromBag(bag, cc);
				slider.showInputField = this.m_ShowInputField.GetValueFromBag(bag, cc);
				base.Init(ve, bag, cc);
			}
		}

		internal const float kDefaultHighValue = 10f;

		public new static readonly string ussClassName = "unity-slider";

		public new static readonly string labelUssClassName = Slider.ussClassName + "__label";

		public new static readonly string inputUssClassName = Slider.ussClassName + "__input";

		public Slider() : this(null, 0f, 10f, SliderDirection.Horizontal, 0f)
		{
		}

		public Slider(float start, float end, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f) : this(null, start, end, direction, pageSize)
		{
		}

		public Slider(string label, float start = 0f, float end = 10f, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f) : base(label, start, end, direction, pageSize)
		{
			base.AddToClassList(Slider.ussClassName);
			base.labelElement.AddToClassList(Slider.labelUssClassName);
			base.visualInput.AddToClassList(Slider.inputUssClassName);
		}

		internal override float SliderLerpUnclamped(float a, float b, float interpolant)
		{
			return (float)Mathf.RoundToInt(Mathf.LerpUnclamped(a, b, interpolant) * 100f) / 100f;
		}

		internal override float SliderNormalizeValue(float currentValue, float lowerValue, float higherValue)
		{
			return (currentValue - lowerValue) / (higherValue - lowerValue);
		}

		internal override float SliderRange()
		{
			return Math.Abs(base.highValue - base.lowValue);
		}

		internal override float ParseStringToValue(string stringValue)
		{
			float num;
			bool flag = float.TryParse(stringValue.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, out num);
			float result;
			if (flag)
			{
				result = num;
			}
			else
			{
				result = 0f;
			}
			return result;
		}

		internal override void ComputeValueFromKey(BaseSlider<float>.SliderKey sliderKey, bool isShift)
		{
			if (sliderKey != BaseSlider<float>.SliderKey.None)
			{
				if (sliderKey != BaseSlider<float>.SliderKey.Lowest)
				{
					if (sliderKey != BaseSlider<float>.SliderKey.Highest)
					{
						bool flag = sliderKey == BaseSlider<float>.SliderKey.LowerPage || sliderKey == BaseSlider<float>.SliderKey.HigherPage;
						float num = BaseSlider<float>.GetClosestPowerOfTen(Mathf.Abs((base.highValue - base.lowValue) * 0.01f));
						bool flag2 = flag;
						if (flag2)
						{
							num *= this.pageSize;
						}
						else if (isShift)
						{
							num *= 10f;
						}
						bool flag3 = sliderKey == BaseSlider<float>.SliderKey.Lower || sliderKey == BaseSlider<float>.SliderKey.LowerPage;
						if (flag3)
						{
							num = -num;
						}
						this.value = BaseSlider<float>.RoundToMultipleOf(this.value + num * 0.5001f, Mathf.Abs(num));
					}
					else
					{
						this.value = base.highValue;
					}
				}
				else
				{
					this.value = base.lowValue;
				}
			}
		}
	}
}
