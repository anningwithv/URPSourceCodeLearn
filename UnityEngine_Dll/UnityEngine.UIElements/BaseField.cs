using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public abstract class BaseField<TValueType> : BindableElement, INotifyValueChanged<TValueType>
	{
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			private UxmlStringAttributeDescription m_Label = new UxmlStringAttributeDescription
			{
				name = "label"
			};

			public UxmlTraits()
			{
				base.focusIndex.defaultValue = 0;
				base.focusable.defaultValue = true;
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((BaseField<TValueType>)ve).label = this.m_Label.GetValueFromBag(bag, cc);
			}
		}

		public static readonly string ussClassName = "unity-base-field";

		public static readonly string labelUssClassName = BaseField<TValueType>.ussClassName + "__label";

		public static readonly string inputUssClassName = BaseField<TValueType>.ussClassName + "__input";

		public static readonly string noLabelVariantUssClassName = BaseField<TValueType>.ussClassName + "--no-label";

		public static readonly string labelDraggerVariantUssClassName = BaseField<TValueType>.labelUssClassName + "--with-dragger";

		private VisualElement m_VisualInput;

		[SerializeField]
		private TValueType m_Value;

		internal VisualElement visualInput
		{
			get
			{
				return this.m_VisualInput;
			}
			set
			{
				bool flag = this.m_VisualInput != null;
				if (flag)
				{
					bool flag2 = this.m_VisualInput.parent == this;
					if (flag2)
					{
						this.m_VisualInput.RemoveFromHierarchy();
					}
					this.m_VisualInput = null;
				}
				bool flag3 = value != null;
				if (flag3)
				{
					this.m_VisualInput = value;
				}
				else
				{
					this.m_VisualInput = new VisualElement
					{
						pickingMode = PickingMode.Ignore
					};
				}
				this.m_VisualInput.focusable = true;
				this.m_VisualInput.AddToClassList(BaseField<TValueType>.inputUssClassName);
				base.Add(this.m_VisualInput);
			}
		}

		protected TValueType rawValue
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		public virtual TValueType value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_Value, value);
				if (flag)
				{
					bool flag2 = base.panel != null;
					if (flag2)
					{
						using (ChangeEvent<TValueType> pooled = ChangeEvent<TValueType>.GetPooled(this.m_Value, value))
						{
							pooled.target = this;
							this.SetValueWithoutNotify(value);
							this.SendEvent(pooled);
						}
					}
					else
					{
						this.SetValueWithoutNotify(value);
					}
				}
			}
		}

		public Label labelElement
		{
			get;
			private set;
		}

		public string label
		{
			get
			{
				return this.labelElement.text;
			}
			set
			{
				bool flag = this.labelElement.text != value;
				if (flag)
				{
					this.labelElement.text = value;
					bool flag2 = string.IsNullOrEmpty(this.labelElement.text);
					if (flag2)
					{
						base.AddToClassList(BaseField<TValueType>.noLabelVariantUssClassName);
						this.labelElement.RemoveFromHierarchy();
					}
					else
					{
						bool flag3 = !base.Contains(this.labelElement);
						if (flag3)
						{
							base.Insert(0, this.labelElement);
							base.RemoveFromClassList(BaseField<TValueType>.noLabelVariantUssClassName);
						}
					}
				}
			}
		}

		internal BaseField(string label)
		{
			base.isCompositeRoot = true;
			base.focusable = true;
			base.tabIndex = 0;
			base.excludeFromFocusRing = true;
			base.delegatesFocus = true;
			base.AddToClassList(BaseField<TValueType>.ussClassName);
			this.labelElement = new Label
			{
				focusable = true,
				tabIndex = -1
			};
			this.labelElement.AddToClassList(BaseField<TValueType>.labelUssClassName);
			bool flag = label != null;
			if (flag)
			{
				this.label = label;
			}
			else
			{
				base.AddToClassList(BaseField<TValueType>.noLabelVariantUssClassName);
			}
			this.m_VisualInput = null;
		}

		protected BaseField(string label, VisualElement visualInput) : this(label)
		{
			this.visualInput = visualInput;
		}

		public virtual void SetValueWithoutNotify(TValueType newValue)
		{
			this.m_Value = newValue;
			bool flag = !string.IsNullOrEmpty(base.viewDataKey);
			if (flag)
			{
				base.SaveViewData();
			}
			base.MarkDirtyRepaint();
		}

		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			bool flag = this.m_VisualInput != null;
			if (flag)
			{
				string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
				TValueType value = this.m_Value;
				base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
				bool flag2 = !EqualityComparer<TValueType>.Default.Equals(value, this.m_Value);
				if (flag2)
				{
					using (ChangeEvent<TValueType> pooled = ChangeEvent<TValueType>.GetPooled(value, this.m_Value))
					{
						pooled.target = this;
						this.SetValueWithoutNotify(this.m_Value);
						this.SendEvent(pooled);
					}
				}
			}
		}
	}
}
