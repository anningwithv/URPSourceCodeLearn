using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	internal class ListViewReorderableDragAndDropController : IListViewDragAndDropController, IDragAndDropController<object, IListDragAndDropArgs>, IReorderable<object>
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly ListViewReorderableDragAndDropController.<>c <>9 = new ListViewReorderableDragAndDropController.<>c();

			public static Func<int, int> <>9__13_0;

			internal int <OnDrop>b__13_0(int i)
			{
				return i;
			}
		}

		protected readonly ListView m_ListView;

		public bool enableReordering
		{
			get;
			set;
		}

		public Action<ItemMoveArgs<object>> onItemMoved
		{
			get;
			set;
		}

		public ListViewReorderableDragAndDropController(ListView listView)
		{
			this.m_ListView = listView;
			this.enableReordering = true;
		}

		public virtual bool CanStartDrag(IEnumerable<object> items)
		{
			return this.enableReordering;
		}

		public virtual StartDragArgs SetupDragAndDrop(IEnumerable<object> items)
		{
			string text = string.Empty;
			foreach (object current in items)
			{
				bool flag = string.IsNullOrEmpty(text);
				if (!flag)
				{
					text = "<Multiple>";
					break;
				}
				int selectedIndex = this.m_ListView.selectedIndex;
				ListView.RecycledItem expr_3E = this.m_ListView.GetRecycledItemFromIndex(selectedIndex);
				Label label = (expr_3E != null) ? expr_3E.element.Q(null, null) : null;
				text = ((label != null) ? label.text : string.Format("Item {0}", selectedIndex));
			}
			return new StartDragArgs(text, this.m_ListView);
		}

		public virtual DragVisualMode HandleDragAndDrop(IListDragAndDropArgs args)
		{
			bool flag = args.dragAndDropPosition == DragAndDropPosition.OverItem || !this.enableReordering;
			DragVisualMode result;
			if (flag)
			{
				result = DragVisualMode.Rejected;
			}
			else
			{
				result = ((args.dragAndDropData.userData == this.m_ListView) ? DragVisualMode.Move : DragVisualMode.Rejected);
			}
			return result;
		}

		public virtual void OnDrop(IListDragAndDropArgs args)
		{
			int num = 0;
			IEnumerable<int> arg_2D_0 = this.m_ListView.selectedIndices;
			Func<int, int> arg_2D_1;
			if ((arg_2D_1 = ListViewReorderableDragAndDropController.<>c.<>9__13_0) == null)
			{
				arg_2D_1 = (ListViewReorderableDragAndDropController.<>c.<>9__13_0 = new Func<int, int>(ListViewReorderableDragAndDropController.<>c.<>9.<OnDrop>b__13_0));
			}
			int[] array = arg_2D_0.OrderBy(arg_2D_1).ToArray<int>();
			for (int i = array.Length - 1; i >= 0; i--)
			{
				int num2 = array[i];
				bool flag = num2 < args.insertAtIndex;
				if (flag)
				{
					num--;
				}
				this.m_ListView.itemsSource.RemoveAt(num2);
			}
			DragAndDropPosition dragAndDropPosition = args.dragAndDropPosition;
			DragAndDropPosition dragAndDropPosition2 = dragAndDropPosition;
			if (dragAndDropPosition2 - DragAndDropPosition.BetweenItems > 1)
			{
				throw new ArgumentException(string.Format("{0} is not supported by {1}.", args.dragAndDropPosition, "ListViewReorderableDragAndDropController"));
			}
			this.InsertRange(args.insertAtIndex + num);
			this.m_ListView.Refresh();
		}

		private void InsertRange(int index)
		{
			List<int> list = new List<int>();
			object[] array = this.m_ListView.selectedItems.ToArray<object>();
			int[] array2 = this.m_ListView.selectedIndices.ToArray<int>();
			for (int i = 0; i < array.Length; i++)
			{
				object obj = array[i];
				this.m_ListView.itemsSource.Insert(index, obj);
				Action<ItemMoveArgs<object>> expr_4D = this.onItemMoved;
				if (expr_4D != null)
				{
					expr_4D(new ItemMoveArgs<object>
					{
						item = obj,
						newIndex = index,
						previousIndex = array2[i]
					});
				}
				list.Add(index);
				index++;
			}
			this.m_ListView.SetSelectionWithoutNotify(list);
		}
	}
}
