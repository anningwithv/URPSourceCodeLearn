using System;

namespace UnityEngine.UIElements
{
	internal interface IVisualElementPanelActivatable
	{
		VisualElement element
		{
			get;
		}

		bool CanBeActivated();

		void OnPanelActivate();

		void OnPanelDeactivate();
	}
}
