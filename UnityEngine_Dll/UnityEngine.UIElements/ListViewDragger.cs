using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	internal class ListViewDragger : DragEventsProcessor
	{
		internal struct DragPosition : IEquatable<ListViewDragger.DragPosition>
		{
			public int insertAtIndex;

			public ListView.RecycledItem recycledItem;

			public DragAndDropPosition dragAndDropPosition;

			public bool Equals(ListViewDragger.DragPosition other)
			{
				return this.insertAtIndex == other.insertAtIndex && object.Equals(this.recycledItem, other.recycledItem) && this.dragAndDropPosition == other.dragAndDropPosition;
			}

			public override bool Equals(object obj)
			{
				return obj is ListViewDragger.DragPosition && this.Equals((ListViewDragger.DragPosition)obj);
			}

			public override int GetHashCode()
			{
				int num = this.insertAtIndex;
				num = (num * 397 ^ ((this.recycledItem != null) ? this.recycledItem.GetHashCode() : 0));
				return num * 397 ^ (int)this.dragAndDropPosition;
			}
		}

		private ListViewDragger.DragPosition m_LastDragPosition;

		private readonly VisualElement m_DragHoverBar;

		private readonly List<VisualElement> m_PickedElements = new List<VisualElement>();

		public const int k_EmptyIndex = -1;

		private const int k_AutoScrollAreaSize = 5;

		private const int k_BetweenElementsAreaSize = 5;

		private const int k_PanSpeed = 20;

		private const int k_DragHoverBarHeight = 2;

		private ListView targetListView
		{
			get
			{
				return this.m_Target as ListView;
			}
		}

		private ScrollView targetScrollView
		{
			get
			{
				return this.targetListView.m_ScrollView;
			}
		}

		public IListViewDragAndDropController dragAndDropController
		{
			get;
			set;
		}

		public ListViewDragger(ListView listView) : base(listView)
		{
			this.m_DragHoverBar = new VisualElement();
			this.m_DragHoverBar.AddToClassList(ListView.dragHoverBarUssClassName);
			this.m_DragHoverBar.style.width = this.targetListView.localBound.width;
			this.m_DragHoverBar.style.visibility = Visibility.Hidden;
			this.m_DragHoverBar.pickingMode = PickingMode.Ignore;
			this.targetListView.RegisterCallback<GeometryChangedEvent>(delegate(GeometryChangedEvent e)
			{
				this.m_DragHoverBar.style.width = this.targetListView.localBound.width;
			}, TrickleDown.NoTrickleDown);
			this.targetListView.m_ScrollView.contentViewport.Add(this.m_DragHoverBar);
		}

		protected override bool CanStartDrag(Vector3 pointerPosition)
		{
			bool flag = this.dragAndDropController == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !this.targetListView.selectedItems.Any<object>();
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = !this.targetScrollView.contentContainer.worldBound.Contains(pointerPosition);
					result = (!flag3 && this.dragAndDropController.CanStartDrag(this.targetListView.selectedItems));
				}
			}
			return result;
		}

		protected override StartDragArgs StartDrag(Vector3 pointerPosition)
		{
			return this.dragAndDropController.SetupDragAndDrop(this.targetListView.selectedItems);
		}

		protected override DragVisualMode UpdateDrag(Vector3 pointerPosition)
		{
			ListViewDragger.DragPosition dragPosition = default(ListViewDragger.DragPosition);
			DragVisualMode visualMode = this.GetVisualMode(pointerPosition, ref dragPosition);
			bool flag = visualMode == DragVisualMode.Rejected;
			if (flag)
			{
				this.ClearDragAndDropUI();
			}
			else
			{
				this.ApplyDragAndDropUI(dragPosition);
			}
			return visualMode;
		}

		private DragVisualMode GetVisualMode(Vector3 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			bool flag = this.dragAndDropController == null;
			DragVisualMode result;
			if (flag)
			{
				result = DragVisualMode.Rejected;
			}
			else
			{
				this.HandleDragAndScroll(pointerPosition);
				bool flag2 = !this.TryGetDragPosition(pointerPosition, ref dragPosition);
				if (flag2)
				{
					result = DragVisualMode.Rejected;
				}
				else
				{
					ListDragAndDropArgs listDragAndDropArgs = this.MakeDragAndDropArgs(dragPosition);
					result = this.dragAndDropController.HandleDragAndDrop(listDragAndDropArgs);
				}
			}
			return result;
		}

		protected override void OnDrop(Vector3 pointerPosition)
		{
			ListViewDragger.DragPosition dragPosition = default(ListViewDragger.DragPosition);
			bool flag = !this.TryGetDragPosition(pointerPosition, ref dragPosition);
			if (!flag)
			{
				ListDragAndDropArgs listDragAndDropArgs = this.MakeDragAndDropArgs(dragPosition);
				bool flag2 = this.dragAndDropController.HandleDragAndDrop(listDragAndDropArgs) != DragVisualMode.Rejected;
				if (flag2)
				{
					this.dragAndDropController.OnDrop(listDragAndDropArgs);
				}
			}
		}

		protected void HandleDragAndScroll(Vector2 pointerPosition)
		{
			bool flag = pointerPosition.y < this.targetScrollView.worldBound.yMin + 5f;
			bool flag2 = pointerPosition.y > this.targetScrollView.worldBound.yMax - 5f;
			bool flag3 = flag | flag2;
			if (flag3)
			{
				this.targetScrollView.scrollOffset += (flag ? Vector2.down : Vector2.up) * 20f;
			}
		}

		protected void ApplyDragAndDropUI(ListViewDragger.DragPosition dragPosition)
		{
			bool flag = this.m_LastDragPosition.Equals(dragPosition);
			if (!flag)
			{
				this.ClearDragAndDropUI();
				this.m_LastDragPosition = dragPosition;
				switch (dragPosition.dragAndDropPosition)
				{
				case DragAndDropPosition.OverItem:
					dragPosition.recycledItem.element.AddToClassList(ListView.itemDragHoverUssClassName);
					break;
				case DragAndDropPosition.BetweenItems:
				{
					bool flag2 = dragPosition.insertAtIndex == 0;
					if (flag2)
					{
						this.PlaceHoverBarAt(0f);
					}
					else
					{
						this.PlaceHoverBarAtElement(this.targetListView.GetRecycledItemFromIndex(dragPosition.insertAtIndex - 1).element);
					}
					break;
				}
				case DragAndDropPosition.OutsideItems:
				{
					ListView.RecycledItem recycledItemFromIndex = this.targetListView.GetRecycledItemFromIndex(this.targetListView.itemsSource.Count - 1);
					bool flag3 = recycledItemFromIndex != null;
					if (flag3)
					{
						this.PlaceHoverBarAtElement(recycledItemFromIndex.element);
					}
					else
					{
						this.PlaceHoverBarAt(0f);
					}
					break;
				}
				default:
					throw new ArgumentOutOfRangeException("dragAndDropPosition", dragPosition.dragAndDropPosition, "Unsupported dragAndDropPosition value");
				}
			}
		}

		protected bool TryGetDragPosition(Vector2 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			ListView.RecycledItem recycledItem = this.GetRecycledItem(pointerPosition);
			bool flag = recycledItem != null;
			bool result;
			if (flag)
			{
				bool flag2 = recycledItem.element.worldBound.yMax - pointerPosition.y < 5f;
				if (flag2)
				{
					dragPosition.insertAtIndex = recycledItem.index + 1;
					dragPosition.dragAndDropPosition = DragAndDropPosition.BetweenItems;
					result = true;
				}
				else
				{
					bool flag3 = pointerPosition.y - recycledItem.element.worldBound.yMin > 5f;
					if (flag3)
					{
						Vector2 scrollOffset = this.targetScrollView.scrollOffset;
						this.targetScrollView.ScrollTo(recycledItem.element);
						bool flag4 = scrollOffset != this.targetScrollView.scrollOffset;
						if (flag4)
						{
							result = this.TryGetDragPosition(pointerPosition, ref dragPosition);
						}
						else
						{
							dragPosition.recycledItem = recycledItem;
							dragPosition.insertAtIndex = -1;
							dragPosition.dragAndDropPosition = DragAndDropPosition.OverItem;
							result = true;
						}
					}
					else
					{
						dragPosition.insertAtIndex = recycledItem.index;
						dragPosition.dragAndDropPosition = DragAndDropPosition.BetweenItems;
						result = true;
					}
				}
			}
			else
			{
				bool flag5 = !this.targetListView.worldBound.Contains(pointerPosition);
				if (flag5)
				{
					result = false;
				}
				else
				{
					dragPosition.dragAndDropPosition = DragAndDropPosition.OutsideItems;
					bool flag6 = pointerPosition.y >= this.targetScrollView.contentContainer.worldBound.yMax;
					if (flag6)
					{
						dragPosition.insertAtIndex = this.targetListView.itemsSource.Count;
					}
					else
					{
						dragPosition.insertAtIndex = 0;
					}
					result = true;
				}
			}
			return result;
		}

		private ListDragAndDropArgs MakeDragAndDropArgs(ListViewDragger.DragPosition dragPosition)
		{
			object target = null;
			ListView.RecycledItem recycledItem = dragPosition.recycledItem;
			bool flag = recycledItem != null;
			if (flag)
			{
				target = this.targetListView.itemsSource[recycledItem.index];
			}
			return new ListDragAndDropArgs
			{
				target = target,
				insertAtIndex = dragPosition.insertAtIndex,
				dragAndDropPosition = dragPosition.dragAndDropPosition
			};
		}

		private void PlaceHoverBarAtElement(VisualElement element)
		{
			VisualElement contentViewport = this.targetScrollView.contentViewport;
			this.PlaceHoverBarAt(Mathf.Min(contentViewport.WorldToLocal(element.worldBound).yMax, contentViewport.localBound.yMax - 2f));
		}

		private void PlaceHoverBarAt(float top)
		{
			this.m_DragHoverBar.style.top = top;
			this.m_DragHoverBar.style.visibility = Visibility.Visible;
		}

		protected override void ClearDragAndDropUI()
		{
			this.m_LastDragPosition = default(ListViewDragger.DragPosition);
			foreach (ListView.RecycledItem current in this.targetListView.Pool)
			{
				current.element.RemoveFromClassList(ListView.itemDragHoverUssClassName);
			}
			this.m_DragHoverBar.style.visibility = Visibility.Hidden;
		}

		private ListView.RecycledItem GetRecycledItem(Vector3 pointerPosition)
		{
			ListView.RecycledItem result;
			foreach (ListView.RecycledItem current in this.targetListView.Pool)
			{
				bool flag = current.element.worldBound.Contains(pointerPosition);
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}
	}
}
