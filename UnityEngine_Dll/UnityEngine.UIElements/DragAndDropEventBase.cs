using System;

namespace UnityEngine.UIElements
{
	public abstract class DragAndDropEventBase<T> : MouseEventBase<T>, IDragAndDropEvent where T : DragAndDropEventBase<T>, new()
	{
	}
}
