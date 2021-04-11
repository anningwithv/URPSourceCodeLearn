using System;

namespace UnityEngine.UIElements
{
	public class TextField : TextInputBaseField<string>
	{
		public new class UxmlFactory : UxmlFactory<TextField, TextField.UxmlTraits>
		{
		}

		public new class UxmlTraits : TextInputBaseField<string>.UxmlTraits
		{
			private UxmlBoolAttributeDescription m_Multiline = new UxmlBoolAttributeDescription
			{
				name = "multiline"
			};

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				TextField textField = (TextField)ve;
				textField.multiline = this.m_Multiline.GetValueFromBag(bag, cc);
				base.Init(ve, bag, cc);
			}
		}

		private class TextInput : TextInputBaseField<string>.TextInputBase
		{
			private bool m_Multiline;

			private TextField parentTextField
			{
				get
				{
					return (TextField)base.parent;
				}
			}

			public bool multiline
			{
				get
				{
					return this.m_Multiline;
				}
				set
				{
					this.m_Multiline = value;
					bool flag = !value;
					if (flag)
					{
						base.text = base.text.Replace("\n", "");
					}
				}
			}

			public override bool isPasswordField
			{
				set
				{
					base.isPasswordField = value;
					if (value)
					{
						this.multiline = false;
					}
				}
			}

			protected override string StringToValue(string str)
			{
				return str;
			}

			public void SelectRange(int cursorIndex, int selectionIndex)
			{
				bool flag = base.editorEngine != null;
				if (flag)
				{
					base.editorEngine.cursorIndex = cursorIndex;
					base.editorEngine.selectIndex = selectionIndex;
				}
			}

			internal override void SyncTextEngine()
			{
				bool flag = this.parentTextField != null;
				if (flag)
				{
					base.editorEngine.multiline = this.multiline;
					base.editorEngine.isPasswordField = this.isPasswordField;
				}
				base.SyncTextEngine();
			}

			protected override void ExecuteDefaultActionAtTarget(EventBase evt)
			{
				base.ExecuteDefaultActionAtTarget(evt);
				bool flag = evt == null;
				if (!flag)
				{
					bool flag2 = evt.eventTypeId == EventBase<KeyDownEvent>.TypeId();
					if (flag2)
					{
						KeyDownEvent keyDownEvent = evt as KeyDownEvent;
						bool flag3 = !this.parentTextField.isDelayed || (!this.multiline && ((keyDownEvent != null && keyDownEvent.keyCode == KeyCode.KeypadEnter) || (keyDownEvent != null && keyDownEvent.keyCode == KeyCode.Return)));
						if (flag3)
						{
							this.parentTextField.value = base.text;
						}
						bool multiline = this.multiline;
						if (multiline)
						{
							char? c = (keyDownEvent != null) ? new char?(keyDownEvent.character) : null;
							int? num = c.HasValue ? new int?((int)c.GetValueOrDefault()) : null;
							int num2 = 9;
							bool flag4 = (num.GetValueOrDefault() == num2 & num.HasValue) && keyDownEvent.modifiers == EventModifiers.None;
							if (flag4)
							{
								if (keyDownEvent != null)
								{
									keyDownEvent.StopPropagation();
								}
								if (keyDownEvent != null)
								{
									keyDownEvent.PreventDefault();
								}
							}
							else
							{
								c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : null);
								num = (c.HasValue ? new int?((int)c.GetValueOrDefault()) : null);
								num2 = 3;
								bool arg_1EE_0;
								if (!(num.GetValueOrDefault() == num2 & num.HasValue) || keyDownEvent == null || !keyDownEvent.shiftKey)
								{
									c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : null);
									num = (c.HasValue ? new int?((int)c.GetValueOrDefault()) : null);
									num2 = 10;
									arg_1EE_0 = ((num.GetValueOrDefault() == num2 & num.HasValue) && keyDownEvent != null && keyDownEvent.shiftKey);
								}
								else
								{
									arg_1EE_0 = true;
								}
								bool flag5 = arg_1EE_0;
								if (flag5)
								{
									base.parent.Focus();
								}
							}
						}
						else
						{
							char? c = (keyDownEvent != null) ? new char?(keyDownEvent.character) : null;
							int? num = c.HasValue ? new int?((int)c.GetValueOrDefault()) : null;
							int num2 = 3;
							bool arg_2B8_0;
							if (!(num.GetValueOrDefault() == num2 & num.HasValue))
							{
								c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : null);
								num = (c.HasValue ? new int?((int)c.GetValueOrDefault()) : null);
								num2 = 10;
								arg_2B8_0 = (num.GetValueOrDefault() == num2 & num.HasValue);
							}
							else
							{
								arg_2B8_0 = true;
							}
							bool flag6 = arg_2B8_0;
							if (flag6)
							{
								base.parent.Focus();
							}
						}
					}
					else
					{
						bool flag7 = evt.eventTypeId == EventBase<ExecuteCommandEvent>.TypeId();
						if (flag7)
						{
							ExecuteCommandEvent executeCommandEvent = evt as ExecuteCommandEvent;
							string commandName = executeCommandEvent.commandName;
							bool flag8 = !this.parentTextField.isDelayed && (commandName == "Paste" || commandName == "Cut");
							if (flag8)
							{
								this.parentTextField.value = base.text;
							}
						}
						else
						{
							NavigationDirection navigationDirection;
							bool flag9 = base.eventInterpreter.IsActivationEvent(evt) || base.eventInterpreter.IsCancellationEvent(evt) || (base.eventInterpreter.IsNavigationEvent(evt, out navigationDirection) && navigationDirection != NavigationDirection.Previous && navigationDirection != NavigationDirection.Next);
							if (flag9)
							{
								evt.StopPropagation();
								evt.PreventDefault();
							}
						}
					}
				}
			}

			protected override void ExecuteDefaultAction(EventBase evt)
			{
				base.ExecuteDefaultAction(evt);
				bool arg_4B_0;
				if (this.parentTextField.isDelayed)
				{
					long? num = (evt != null) ? new long?(evt.eventTypeId) : null;
					long num2 = EventBase<BlurEvent>.TypeId();
					arg_4B_0 = (num.GetValueOrDefault() == num2 & num.HasValue);
				}
				else
				{
					arg_4B_0 = false;
				}
				bool flag = arg_4B_0;
				if (flag)
				{
					this.parentTextField.value = base.text;
				}
			}
		}

		private int m_VisualInputTabIndex;

		public new static readonly string ussClassName = "unity-text-field";

		public new static readonly string labelUssClassName = TextField.ussClassName + "__label";

		public new static readonly string inputUssClassName = TextField.ussClassName + "__input";

		private TextField.TextInput textInput
		{
			get
			{
				return (TextField.TextInput)base.textInputBase;
			}
		}

		public bool multiline
		{
			get
			{
				return this.textInput.multiline;
			}
			set
			{
				this.textInput.multiline = value;
			}
		}

		public override string value
		{
			get
			{
				return base.value;
			}
			set
			{
				base.value = value;
				base.text = base.rawValue;
			}
		}

		public void SelectRange(int rangeCursorIndex, int selectionIndex)
		{
			this.textInput.SelectRange(rangeCursorIndex, selectionIndex);
		}

		public TextField() : this(null)
		{
		}

		public TextField(int maxLength, bool multiline, bool isPasswordField, char maskChar) : this(null, maxLength, multiline, isPasswordField, maskChar)
		{
		}

		public TextField(string label) : this(label, -1, false, false, '*')
		{
		}

		public TextField(string label, int maxLength, bool multiline, bool isPasswordField, char maskChar) : base(label, maxLength, maskChar, new TextField.TextInput())
		{
			base.AddToClassList(TextField.ussClassName);
			base.labelElement.AddToClassList(TextField.labelUssClassName);
			base.visualInput.AddToClassList(TextField.inputUssClassName);
			base.pickingMode = PickingMode.Ignore;
			this.SetValueWithoutNotify("");
			this.multiline = multiline;
			base.isPasswordField = isPasswordField;
		}

		public override void SetValueWithoutNotify(string newValue)
		{
			base.SetValueWithoutNotify(newValue);
			base.text = base.rawValue;
		}

		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			base.text = base.rawValue;
		}

		protected override void ExecuteDefaultActionAtTarget(EventBase evt)
		{
			base.ExecuteDefaultActionAtTarget(evt);
			bool multiline = this.multiline;
			if (multiline)
			{
				long? num = (evt != null) ? new long?(evt.eventTypeId) : null;
				long num2 = EventBase<FocusInEvent>.TypeId();
				bool arg_AA_0;
				if (!(num.GetValueOrDefault() == num2 & num.HasValue) || ((evt != null) ? evt.leafTarget : null) != this)
				{
					num = ((evt != null) ? new long?(evt.eventTypeId) : null);
					num2 = EventBase<FocusInEvent>.TypeId();
					arg_AA_0 = ((num.GetValueOrDefault() == num2 & num.HasValue) && ((evt != null) ? evt.leafTarget : null) == base.labelElement);
				}
				else
				{
					arg_AA_0 = true;
				}
				bool flag = arg_AA_0;
				if (flag)
				{
					this.m_VisualInputTabIndex = base.visualInput.tabIndex;
					base.visualInput.tabIndex = -1;
				}
				else
				{
					num = ((evt != null) ? new long?(evt.eventTypeId) : null);
					num2 = EventBase<BlurEvent>.TypeId();
					bool arg_166_0;
					if (!(num.GetValueOrDefault() == num2 & num.HasValue) || ((evt != null) ? evt.leafTarget : null) != this)
					{
						num = ((evt != null) ? new long?(evt.eventTypeId) : null);
						num2 = EventBase<BlurEvent>.TypeId();
						arg_166_0 = ((num.GetValueOrDefault() == num2 & num.HasValue) && ((evt != null) ? evt.leafTarget : null) == base.labelElement);
					}
					else
					{
						arg_166_0 = true;
					}
					bool flag2 = arg_166_0;
					if (flag2)
					{
						base.visualInput.tabIndex = this.m_VisualInputTabIndex;
					}
				}
			}
		}
	}
}
