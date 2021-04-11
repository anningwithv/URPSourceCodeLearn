using System;

namespace UnityEngine.UIElements
{
	internal struct ItemMoveArgs<T>
	{
		public T item;

		public int newIndex;

		public int previousIndex;
	}
}
