using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	internal struct ListDragAndDropArgs : IListDragAndDropArgs
	{
		public object target
		{
			[IsReadOnly]
			get;
			set;
		}

		public int insertAtIndex
		{
			[IsReadOnly]
			get;
			set;
		}

		public DragAndDropPosition dragAndDropPosition
		{
			[IsReadOnly]
			get;
			set;
		}

		public IDragAndDropData dragAndDropData
		{
			get
			{
				return DragAndDropUtility.dragAndDrop.data;
			}
		}
	}
}
