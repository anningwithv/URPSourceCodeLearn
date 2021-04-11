using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public class Scroller : VisualElement
	{
		public new class UxmlFactory : UxmlFactory<Scroller, Scroller.UxmlTraits>
		{
		}

		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
			{
				name = "low-value",
				obsoleteNames = new string[]
				{
					"lowValue"
				}
			};

			private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
			{
				name = "high-value",
				obsoleteNames = new string[]
				{
					"highValue"
				}
			};

			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Vertical
			};

			private UxmlFloatAttributeDescription m_Value = new UxmlFloatAttributeDescription
			{
				name = "value"
			};

			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Scroller scroller = (Scroller)ve;
				scroller.slider.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				scroller.slider.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				scroller.direction = this.m_Direction.GetValueFromBag(bag, cc);
				scroller.value = this.m_Value.GetValueFromBag(bag, cc);
			}
		}

		internal const float kDefaultPageSize = 20f;

		public static readonly string ussClassName = "unity-scroller";

		public static readonly string horizontalVariantUssClassName = Scroller.ussClassName + "--horizontal";

		public static readonly string verticalVariantUssClassName = Scroller.ussClassName + "--vertical";

		public static readonly string sliderUssClassName = Scroller.ussClassName + "__slider";

		public static readonly string lowButtonUssClassName = Scroller.ussClassName + "__low-button";

		public static readonly string highButtonUssClassName = Scroller.ussClassName + "__high-button";

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action<float> valueChanged;

		public Slider slider
		{
			get;
			private set;
		}

		public RepeatButton lowButton
		{
			get;
			private set;
		}

		public RepeatButton highButton
		{
			get;
			private set;
		}

		public float value
		{
			get
			{
				return this.slider.value;
			}
			set
			{
				this.slider.value = value;
			}
		}

		public float lowValue
		{
			get
			{
				return this.slider.lowValue;
			}
			set
			{
				this.slider.lowValue = value;
			}
		}

		public float highValue
		{
			get
			{
				return this.slider.highValue;
			}
			set
			{
				this.slider.highValue = value;
			}
		}

		public SliderDirection direction
		{
			get
			{
				return (base.resolvedStyle.flexDirection == FlexDirection.Row) ? SliderDirection.Horizontal : SliderDirection.Vertical;
			}
			set
			{
				this.slider.direction = value;
				bool flag = value == SliderDirection.Horizontal;
				if (flag)
				{
					base.style.flexDirection = FlexDirection.Row;
					base.AddToClassList(Scroller.horizontalVariantUssClassName);
					base.RemoveFromClassList(Scroller.verticalVariantUssClassName);
				}
				else
				{
					base.style.flexDirection = FlexDirection.Column;
					base.AddToClassList(Scroller.verticalVariantUssClassName);
					base.RemoveFromClassList(Scroller.horizontalVariantUssClassName);
				}
			}
		}

		public Scroller() : this(0f, 0f, null, SliderDirection.Vertical)
		{
		}

		public Scroller(float lowValue, float highValue, Action<float> valueChanged, SliderDirection direction = SliderDirection.Vertical)
		{
			base.AddToClassList(Scroller.ussClassName);
			this.slider = new Slider(lowValue, highValue, direction, 20f)
			{
				name = "unity-slider",
				viewDataKey = "Slider"
			};
			this.slider.AddToClassList(Scroller.sliderUssClassName);
			this.slider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnSliderValueChange));
			this.lowButton = new RepeatButton(new Action(this.ScrollPageUp), 250L, 30L)
			{
				name = "unity-low-button"
			};
			this.lowButton.AddToClassList(Scroller.lowButtonUssClassName);
			base.Add(this.lowButton);
			this.highButton = new RepeatButton(new Action(this.ScrollPageDown), 250L, 30L)
			{
				name = "unity-high-button"
			};
			this.highButton.AddToClassList(Scroller.highButtonUssClassName);
			base.Add(this.highButton);
			base.Add(this.slider);
			this.direction = direction;
			this.valueChanged = valueChanged;
		}

		public void Adjust(float factor)
		{
			base.SetEnabled(factor < 1f);
			this.slider.AdjustDragElement(factor);
		}

		private void OnSliderValueChange(ChangeEvent<float> evt)
		{
			this.value = evt.newValue;
			Action<float> expr_14 = this.valueChanged;
			if (expr_14 != null)
			{
				expr_14(this.slider.value);
			}
			base.IncrementVersion(VersionChangeType.Repaint);
		}

		public void ScrollPageUp()
		{
			this.ScrollPageUp(1f);
		}

		public void ScrollPageDown()
		{
			this.ScrollPageDown(1f);
		}

		public void ScrollPageUp(float factor)
		{
			this.value -= factor * (this.slider.pageSize * ((this.slider.lowValue < this.slider.highValue) ? 1f : -1f));
		}

		public void ScrollPageDown(float factor)
		{
			this.value += factor * (this.slider.pageSize * ((this.slider.lowValue < this.slider.highValue) ? 1f : -1f));
		}
	}
}
