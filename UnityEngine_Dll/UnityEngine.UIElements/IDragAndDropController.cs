using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal interface IDragAndDropController<TItem, in TArgs>
	{
		bool CanStartDrag(IEnumerable<TItem> items);

		StartDragArgs SetupDragAndDrop(IEnumerable<TItem> items);

		DragVisualMode HandleDragAndDrop(TArgs args);

		void OnDrop(TArgs args);
	}
}
