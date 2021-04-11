using System;

namespace UnityEngine.UIElements
{
	internal interface ITextInputField : IEventHandler, ITextElement
	{
		bool hasFocus
		{
			get;
		}

		bool doubleClickSelectsWord
		{
			get;
		}

		bool tripleClickSelectsLine
		{
			get;
		}

		bool isReadOnly
		{
			get;
		}

		bool isDelayed
		{
			get;
		}

		bool isPasswordField
		{
			get;
		}

		TextEditorEngine editorEngine
		{
			get;
		}

		void SyncTextEngine();

		bool AcceptCharacter(char c);

		string CullString(string s);

		void UpdateText(string value);

		void UpdateValueFromText();
	}
}
