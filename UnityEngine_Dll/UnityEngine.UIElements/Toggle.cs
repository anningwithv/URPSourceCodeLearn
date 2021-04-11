using System;

namespace UnityEngine.UIElements
{
	public class Toggle : BaseField<bool>
	{
		public new class UxmlFactory : UxmlFactory<Toggle, Toggle.UxmlTraits>
		{
		}

		public new class UxmlTraits : BaseFieldTraits<bool, UxmlBoolAttributeDescription>
		{
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((Toggle)ve).text = this.m_Text.GetValueFromBag(bag, cc);
			}
		}

		public new static readonly string ussClassName = "unity-toggle";

		public new static readonly string labelUssClassName = Toggle.ussClassName + "__label";

		public new static readonly string inputUssClassName = Toggle.ussClassName + "__input";

		public static readonly string noTextVariantUssClassName = Toggle.ussClassName + "--no-text";

		public static readonly string checkmarkUssClassName = Toggle.ussClassName + "__checkmark";

		public static readonly string textUssClassName = Toggle.ussClassName + "__text";

		private Label m_Label;

		public string text
		{
			get
			{
				Label expr_07 = this.m_Label;
				return (expr_07 != null) ? expr_07.text : null;
			}
			set
			{
				bool flag = !string.IsNullOrEmpty(value);
				if (flag)
				{
					bool flag2 = this.m_Label == null;
					if (flag2)
					{
						this.m_Label = new Label
						{
							pickingMode = PickingMode.Ignore
						};
						this.m_Label.AddToClassList(Toggle.textUssClassName);
						base.RemoveFromClassList(Toggle.noTextVariantUssClassName);
						base.visualInput.Add(this.m_Label);
					}
					this.m_Label.text = value;
				}
				else
				{
					bool flag3 = this.m_Label != null;
					if (flag3)
					{
						this.m_Label.RemoveFromHierarchy();
						base.AddToClassList(Toggle.noTextVariantUssClassName);
						this.m_Label = null;
					}
				}
			}
		}

		public Toggle() : this(null)
		{
		}

		public Toggle(string label) : base(label, null)
		{
			base.AddToClassList(Toggle.ussClassName);
			base.AddToClassList(Toggle.noTextVariantUssClassName);
			base.visualInput.AddToClassList(Toggle.inputUssClassName);
			base.labelElement.AddToClassList(Toggle.labelUssClassName);
			VisualElement visualElement = new VisualElement
			{
				name = "unity-checkmark",
				pickingMode = PickingMode.Ignore
			};
			visualElement.AddToClassList(Toggle.checkmarkUssClassName);
			base.visualInput.Add(visualElement);
			base.visualInput.pickingMode = PickingMode.Position;
			this.text = null;
			this.AddManipulator(new Clickable(new Action<EventBase>(this.OnClickEvent)));
		}

		public override void SetValueWithoutNotify(bool newValue)
		{
			if (newValue)
			{
				base.visualInput.pseudoStates |= PseudoStates.Checked;
				base.pseudoStates |= PseudoStates.Checked;
			}
			else
			{
				base.visualInput.pseudoStates &= ~PseudoStates.Checked;
				base.pseudoStates &= ~PseudoStates.Checked;
			}
			base.SetValueWithoutNotify(newValue);
		}

		private void OnClickEvent(EventBase evt)
		{
			bool flag = evt.eventTypeId == EventBase<MouseUpEvent>.TypeId();
			if (flag)
			{
				IMouseEvent mouseEvent = (IMouseEvent)evt;
				bool flag2 = mouseEvent.button == 0;
				if (flag2)
				{
					this.OnClick();
				}
			}
			else
			{
				bool flag3 = evt.eventTypeId == EventBase<PointerUpEvent>.TypeId() || evt.eventTypeId == EventBase<ClickEvent>.TypeId();
				if (flag3)
				{
					IPointerEvent pointerEvent = (IPointerEvent)evt;
					bool flag4 = pointerEvent.button == 0;
					if (flag4)
					{
						this.OnClick();
					}
				}
			}
		}

		private void OnClick()
		{
			this.value = !this.value;
		}

		protected override void ExecuteDefaultActionAtTarget(EventBase evt)
		{
			base.ExecuteDefaultActionAtTarget(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = base.eventInterpreter.IsActivationEvent(evt);
				if (flag2)
				{
					this.OnClick();
					evt.StopPropagation();
				}
			}
		}
	}
}
