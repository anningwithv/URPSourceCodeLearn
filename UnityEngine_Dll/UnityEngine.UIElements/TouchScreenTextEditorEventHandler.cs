using System;

namespace UnityEngine.UIElements
{
	internal class TouchScreenTextEditorEventHandler : TextEditorEventHandler
	{
		private IVisualElementScheduledItem m_TouchKeyboardPoller = null;

		public TouchScreenTextEditorEventHandler(TextEditorEngine editorEngine, ITextInputField textInputField) : base(editorEngine, textInputField)
		{
		}

		private void PollTouchScreenKeyboard()
		{
			bool flag = TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (flag)
			{
				bool flag2 = this.m_TouchKeyboardPoller == null;
				if (flag2)
				{
					VisualElement expr_32 = base.textInputField as VisualElement;
					this.m_TouchKeyboardPoller = ((expr_32 != null) ? expr_32.schedule.Execute(new Action(this.DoPollTouchScreenKeyboard)).Every(100L) : null);
				}
				else
				{
					this.m_TouchKeyboardPoller.Resume();
				}
			}
		}

		private void DoPollTouchScreenKeyboard()
		{
			bool flag = TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (flag)
			{
				bool flag2 = base.textInputField.editorEngine.keyboardOnScreen != null;
				if (flag2)
				{
					base.textInputField.UpdateText(base.textInputField.CullString(base.textInputField.editorEngine.keyboardOnScreen.text));
					bool flag3 = !base.textInputField.isDelayed;
					if (flag3)
					{
						base.textInputField.UpdateValueFromText();
					}
					bool flag4 = base.textInputField.editorEngine.keyboardOnScreen.status > TouchScreenKeyboard.Status.Visible;
					if (flag4)
					{
						base.textInputField.editorEngine.keyboardOnScreen = null;
						this.m_TouchKeyboardPoller.Pause();
						bool isDelayed = base.textInputField.isDelayed;
						if (isDelayed)
						{
							base.textInputField.UpdateValueFromText();
						}
					}
				}
			}
		}

		public override void ExecuteDefaultActionAtTarget(EventBase evt)
		{
			base.ExecuteDefaultActionAtTarget(evt);
			long num = EventBase<MouseDownEvent>.TypeId();
			bool flag = !base.textInputField.isReadOnly && evt.eventTypeId == num && base.editorEngine.keyboardOnScreen == null;
			if (flag)
			{
				base.textInputField.SyncTextEngine();
				base.textInputField.UpdateText(base.editorEngine.text);
				base.editorEngine.keyboardOnScreen = TouchScreenKeyboard.Open(base.textInputField.text, TouchScreenKeyboardType.Default, true, base.editorEngine.multiline, base.textInputField.isPasswordField);
				bool flag2 = base.editorEngine.keyboardOnScreen != null;
				if (flag2)
				{
					this.PollTouchScreenKeyboard();
				}
				base.editorEngine.UpdateScrollOffset();
				evt.StopPropagation();
			}
		}
	}
}
