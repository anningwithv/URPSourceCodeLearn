using System;

namespace UnityEngine.UIElements
{
	internal interface IReorderable<T>
	{
		bool enableReordering
		{
			get;
			set;
		}

		Action<ItemMoveArgs<T>> onItemMoved
		{
			get;
			set;
		}
	}
}
