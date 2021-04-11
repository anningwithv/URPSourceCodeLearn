using System;

namespace UnityEngine.UIElements
{
	internal class TextEditorEventHandler
	{
		protected TextEditorEngine editorEngine
		{
			get;
			private set;
		}

		protected ITextInputField textInputField
		{
			get;
			private set;
		}

		protected TextEditorEventHandler(TextEditorEngine editorEngine, ITextInputField textInputField)
		{
			this.editorEngine = editorEngine;
			this.textInputField = textInputField;
			this.textInputField.SyncTextEngine();
		}

		public virtual void ExecuteDefaultActionAtTarget(EventBase evt)
		{
		}

		public virtual void ExecuteDefaultAction(EventBase evt)
		{
			bool flag = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
			if (flag)
			{
				this.editorEngine.OnFocus();
				this.editorEngine.SelectAll();
			}
			else
			{
				bool flag2 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
				if (flag2)
				{
					this.editorEngine.OnLostFocus();
					this.editorEngine.SelectNone();
				}
			}
		}
	}
}
