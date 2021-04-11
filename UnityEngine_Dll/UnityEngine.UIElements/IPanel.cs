using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public interface IPanel : IDisposable
	{
		VisualElement visualTree
		{
			get;
		}

		EventDispatcher dispatcher
		{
			get;
		}

		ContextType contextType
		{
			get;
		}

		FocusController focusController
		{
			get;
		}

		ContextualMenuManager contextualMenuManager
		{
			get;
		}

		VisualElement Pick(Vector2 point);

		VisualElement PickAll(Vector2 point, List<VisualElement> picked);
	}
}
