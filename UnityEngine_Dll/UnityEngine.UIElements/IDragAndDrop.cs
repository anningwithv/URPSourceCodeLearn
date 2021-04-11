using System;

namespace UnityEngine.UIElements
{
	internal interface IDragAndDrop
	{
		IDragAndDropData data
		{
			get;
		}

		void StartDrag(StartDragArgs args);

		void AcceptDrag();

		void SetVisualMode(DragVisualMode visualMode);
	}
}
