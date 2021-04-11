using System;

namespace UnityEngine.UIElements
{
	public interface IMouseEvent
	{
		EventModifiers modifiers
		{
			get;
		}

		Vector2 mousePosition
		{
			get;
		}

		Vector2 localMousePosition
		{
			get;
		}

		Vector2 mouseDelta
		{
			get;
		}

		int clickCount
		{
			get;
		}

		int button
		{
			get;
		}

		int pressedButtons
		{
			get;
		}

		bool shiftKey
		{
			get;
		}

		bool ctrlKey
		{
			get;
		}

		bool commandKey
		{
			get;
		}

		bool altKey
		{
			get;
		}

		bool actionKey
		{
			get;
		}
	}
}
