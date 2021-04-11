using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	internal class TreeView : VisualElement
	{
		public new class UxmlFactory : UxmlFactory<TreeView, TreeView.UxmlTraits>
		{
		}

		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			private readonly UxmlIntAttributeDescription m_ItemHeight = new UxmlIntAttributeDescription
			{
				name = "item-height",
				defaultValue = ListView.s_DefaultItemHeight
			};

			private readonly UxmlBoolAttributeDescription m_ShowBorder = new UxmlBoolAttributeDescription
			{
				name = "show-border",
				defaultValue = false
			};

			private readonly UxmlEnumAttributeDescription<SelectionType> m_SelectionType = new UxmlEnumAttributeDescription<SelectionType>
			{
				name = "selection-type",
				defaultValue = SelectionType.Single
			};

			private readonly UxmlEnumAttributeDescription<AlternatingRowBackground> m_ShowAlternatingRowBackgrounds = new UxmlEnumAttributeDescription<AlternatingRowBackground>
			{
				name = "show-alternating-row-backgrounds",
				defaultValue = AlternatingRowBackground.None
			};

			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				int itemHeight = 0;
				bool flag = this.m_ItemHeight.TryGetValueFromBag(bag, cc, ref itemHeight);
				if (flag)
				{
					((TreeView)ve).itemHeight = itemHeight;
				}
				((TreeView)ve).showBorder = this.m_ShowBorder.GetValueFromBag(bag, cc);
				((TreeView)ve).selectionType = this.m_SelectionType.GetValueFromBag(bag, cc);
				((TreeView)ve).showAlternatingRowBackgrounds = this.m_ShowAlternatingRowBackgrounds.GetValueFromBag(bag, cc);
			}
		}

		private struct TreeViewItemWrapper
		{
			public int depth;

			public ITreeViewItem item;

			public int id
			{
				get
				{
					return this.item.id;
				}
			}

			public bool hasChildren
			{
				get
				{
					return this.item.hasChildren;
				}
			}
		}

		private static readonly string s_ListViewName = "unity-tree-view__list-view";

		private static readonly string s_ItemName = "unity-tree-view__item";

		private static readonly string s_ItemToggleName = "unity-tree-view__item-toggle";

		private static readonly string s_ItemIndentsContainerName = "unity-tree-view__item-indents";

		private static readonly string s_ItemIndentName = "unity-tree-view__item-indent";

		private static readonly string s_ItemContentContainerName = "unity-tree-view__item-content";

		private Func<VisualElement> m_MakeItem;

		private List<ITreeViewItem> m_SelectedItems;

		private Action<VisualElement, ITreeViewItem> m_BindItem;

		private IList<ITreeViewItem> m_RootItems;

		[SerializeField]
		private List<int> m_ExpandedItemIds;

		private List<TreeView.TreeViewItemWrapper> m_ItemWrappers;

		private readonly ListView m_ListView;

		private readonly ScrollView m_ScrollView;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action<IEnumerable<ITreeViewItem>> onItemsChosen;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action<IEnumerable<ITreeViewItem>> onSelectionChange;

		public Func<VisualElement> makeItem
		{
			get
			{
				return this.m_MakeItem;
			}
			set
			{
				bool flag = this.m_MakeItem == value;
				if (!flag)
				{
					this.m_MakeItem = value;
					this.ListViewRefresh();
				}
			}
		}

		public ITreeViewItem selectedItem
		{
			get
			{
				return (this.m_SelectedItems.Count == 0) ? null : this.m_SelectedItems.First<ITreeViewItem>();
			}
		}

		public IEnumerable<ITreeViewItem> selectedItems
		{
			get
			{
				bool flag = this.m_SelectedItems != null;
				IEnumerable<ITreeViewItem> selectedItems;
				if (flag)
				{
					selectedItems = this.m_SelectedItems;
				}
				else
				{
					this.m_SelectedItems = new List<ITreeViewItem>();
					foreach (ITreeViewItem current in this.items)
					{
						foreach (int current2 in this.m_ListView.currentSelectionIds)
						{
							bool flag2 = current.id == current2;
							if (flag2)
							{
								this.m_SelectedItems.Add(current);
							}
						}
					}
					selectedItems = this.m_SelectedItems;
				}
				return selectedItems;
			}
		}

		public Action<VisualElement, ITreeViewItem> bindItem
		{
			get
			{
				return this.m_BindItem;
			}
			set
			{
				this.m_BindItem = value;
				this.ListViewRefresh();
			}
		}

		public Action<VisualElement, ITreeViewItem> unbindItem
		{
			get;
			set;
		}

		public IList<ITreeViewItem> rootItems
		{
			get
			{
				return this.m_RootItems;
			}
			set
			{
				this.m_RootItems = value;
				this.Refresh();
			}
		}

		public IEnumerable<ITreeViewItem> items
		{
			get
			{
				return TreeView.GetAllItems(this.m_RootItems);
			}
		}

		public float resolvedItemHeight
		{
			get
			{
				return this.m_ListView.resolvedItemHeight;
			}
		}

		public int itemHeight
		{
			get
			{
				return this.m_ListView.itemHeight;
			}
			set
			{
				this.m_ListView.itemHeight = value;
			}
		}

		public bool horizontalScrollingEnabled
		{
			get
			{
				return this.m_ListView.horizontalScrollingEnabled;
			}
			set
			{
				this.m_ListView.horizontalScrollingEnabled = value;
			}
		}

		public bool showBorder
		{
			get
			{
				return this.m_ListView.showBorder;
			}
			set
			{
				this.m_ListView.showBorder = value;
			}
		}

		public SelectionType selectionType
		{
			get
			{
				return this.m_ListView.selectionType;
			}
			set
			{
				this.m_ListView.selectionType = value;
			}
		}

		public AlternatingRowBackground showAlternatingRowBackgrounds
		{
			get
			{
				return this.m_ListView.showAlternatingRowBackgrounds;
			}
			set
			{
				this.m_ListView.showAlternatingRowBackgrounds = value;
			}
		}

		public TreeView()
		{
			this.m_SelectedItems = null;
			this.m_ExpandedItemIds = new List<int>();
			this.m_ItemWrappers = new List<TreeView.TreeViewItemWrapper>();
			this.m_ListView = new ListView();
			this.m_ListView.name = TreeView.s_ListViewName;
			this.m_ListView.itemsSource = this.m_ItemWrappers;
			this.m_ListView.viewDataKey = TreeView.s_ListViewName;
			this.m_ListView.AddToClassList(TreeView.s_ListViewName);
			base.hierarchy.Add(this.m_ListView);
			this.m_ListView.makeItem = new Func<VisualElement>(this.MakeTreeItem);
			this.m_ListView.bindItem = new Action<VisualElement, int>(this.BindTreeItem);
			this.m_ListView.unbindItem = new Action<VisualElement, int>(this.UnbindTreeItem);
			this.m_ListView.getItemId = new Func<int, int>(this.GetItemId);
			this.m_ListView.onItemsChosen += new Action<IEnumerable<object>>(this.OnItemsChosen);
			this.m_ListView.onSelectionChange += new Action<IEnumerable<object>>(this.OnSelectionChange);
			this.m_ScrollView = this.m_ListView.m_ScrollView;
			this.m_ScrollView.contentContainer.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
			base.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnTreeViewMouseUp), TrickleDown.TrickleDown);
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
		}

		public TreeView(IList<ITreeViewItem> items, int itemHeight, Func<VisualElement> makeItem, Action<VisualElement, ITreeViewItem> bindItem) : this()
		{
			this.m_ListView.itemHeight = itemHeight;
			this.m_MakeItem = makeItem;
			this.m_BindItem = bindItem;
			this.m_RootItems = items;
			this.Refresh();
		}

		public void Refresh()
		{
			this.RegenerateWrappers();
			this.ListViewRefresh();
		}

		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			this.Refresh();
		}

		public static IEnumerable<ITreeViewItem> GetAllItems(IEnumerable<ITreeViewItem> rootItems)
		{
			bool flag = rootItems == null;
			if (flag)
			{
				yield break;
			}
			Stack<IEnumerator<ITreeViewItem>> stack = new Stack<IEnumerator<ITreeViewItem>>();
			IEnumerator<ITreeViewItem> enumerator = rootItems.GetEnumerator();
			while (true)
			{
				bool flag2 = enumerator.MoveNext();
				bool flag3 = !flag2;
				if (flag3)
				{
					bool flag4 = stack.Count > 0;
					if (!flag4)
					{
						break;
					}
					enumerator = stack.Pop();
				}
				else
				{
					ITreeViewItem treeViewItem = enumerator.Current;
					yield return treeViewItem;
					bool hasChildren = treeViewItem.hasChildren;
					if (hasChildren)
					{
						stack.Push(enumerator);
						enumerator = treeViewItem.children.GetEnumerator();
					}
					treeViewItem = null;
				}
			}
			yield break;
		}

		public void OnKeyDown(KeyDownEvent evt)
		{
			int selectedIndex = this.m_ListView.selectedIndex;
			bool flag = true;
			KeyCode keyCode = evt.keyCode;
			KeyCode keyCode2 = keyCode;
			if (keyCode2 != KeyCode.RightArrow)
			{
				if (keyCode2 != KeyCode.LeftArrow)
				{
					flag = false;
				}
				else
				{
					bool flag2 = this.IsExpandedByIndex(selectedIndex);
					if (flag2)
					{
						this.CollapseItemByIndex(selectedIndex);
					}
				}
			}
			else
			{
				bool flag3 = !this.IsExpandedByIndex(selectedIndex);
				if (flag3)
				{
					this.ExpandItemByIndex(selectedIndex);
				}
			}
			bool flag4 = flag;
			if (flag4)
			{
				evt.StopPropagation();
			}
		}

		public void SetSelection(int id)
		{
			this.SetSelection(new int[]
			{
				id
			});
		}

		public void SetSelection(IEnumerable<int> ids)
		{
			this.SetSelectionInternal(ids, true);
		}

		public void SetSelectionWithoutNotify(IEnumerable<int> ids)
		{
			this.SetSelectionInternal(ids, false);
		}

		internal void SetSelectionInternal(IEnumerable<int> ids, bool sendNotification)
		{
			bool flag = ids == null;
			if (!flag)
			{
				List<int> indices = (from id in ids
				select this.GetItemIndex(id, true)).ToList<int>();
				this.ListViewRefresh();
				this.m_ListView.SetSelectionInternal(indices, sendNotification);
			}
		}

		public void AddToSelection(int id)
		{
			int itemIndex = this.GetItemIndex(id, true);
			this.ListViewRefresh();
			this.m_ListView.AddToSelection(itemIndex);
		}

		public void RemoveFromSelection(int id)
		{
			int itemIndex = this.GetItemIndex(id, false);
			this.m_ListView.RemoveFromSelection(itemIndex);
		}

		private int GetItemIndex(int id, bool expand = false)
		{
			ITreeViewItem treeViewItem = this.FindItem(id);
			bool flag = treeViewItem == null;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("id", id, "TreeView: Item id not found.");
			}
			if (expand)
			{
				bool flag2 = false;
				for (ITreeViewItem parent = treeViewItem.parent; parent != null; parent = parent.parent)
				{
					bool flag3 = !this.m_ExpandedItemIds.Contains(parent.id);
					if (flag3)
					{
						this.m_ExpandedItemIds.Add(parent.id);
						flag2 = true;
					}
				}
				bool flag4 = flag2;
				if (flag4)
				{
					this.RegenerateWrappers();
				}
			}
			int i;
			for (i = 0; i < this.m_ItemWrappers.Count; i++)
			{
				bool flag5 = this.m_ItemWrappers[i].id == id;
				if (flag5)
				{
					break;
				}
			}
			return i;
		}

		public void ClearSelection()
		{
			this.m_ListView.ClearSelection();
		}

		public void ScrollTo(VisualElement visualElement)
		{
			this.m_ListView.ScrollTo(visualElement);
		}

		public void ScrollToItem(int id)
		{
			int itemIndex = this.GetItemIndex(id, true);
			this.Refresh();
			this.m_ListView.ScrollToItem(itemIndex);
		}

		public bool IsExpanded(int id)
		{
			return this.m_ExpandedItemIds.Contains(id);
		}

		public void CollapseItem(int id)
		{
			bool flag = this.FindItem(id) == null;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("id", id, "TreeView: Item id not found.");
			}
			for (int i = 0; i < this.m_ItemWrappers.Count; i++)
			{
				bool flag2 = this.m_ItemWrappers[i].item.id == id;
				if (flag2)
				{
					bool flag3 = this.IsExpandedByIndex(i);
					if (flag3)
					{
						this.CollapseItemByIndex(i);
						return;
					}
				}
			}
			bool flag4 = !this.m_ExpandedItemIds.Contains(id);
			if (flag4)
			{
				return;
			}
			this.m_ExpandedItemIds.Remove(id);
			this.Refresh();
		}

		public void ExpandItem(int id)
		{
			bool flag = this.FindItem(id) == null;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("id", id, "TreeView: Item id not found.");
			}
			for (int i = 0; i < this.m_ItemWrappers.Count; i++)
			{
				bool flag2 = this.m_ItemWrappers[i].item.id == id;
				if (flag2)
				{
					bool flag3 = !this.IsExpandedByIndex(i);
					if (flag3)
					{
						this.ExpandItemByIndex(i);
						return;
					}
				}
			}
			bool flag4 = this.m_ExpandedItemIds.Contains(id);
			if (flag4)
			{
				return;
			}
			this.m_ExpandedItemIds.Add(id);
			this.Refresh();
		}

		public ITreeViewItem FindItem(int id)
		{
			ITreeViewItem result;
			foreach (ITreeViewItem current in this.items)
			{
				bool flag = current.id == id;
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		private void ListViewRefresh()
		{
			this.m_ListView.Refresh();
		}

		private void OnItemsChosen(IEnumerable<object> chosenItems)
		{
			bool flag = this.onItemsChosen == null;
			if (!flag)
			{
				List<ITreeViewItem> list = new List<ITreeViewItem>();
				foreach (object current in chosenItems)
				{
					TreeView.TreeViewItemWrapper treeViewItemWrapper = (TreeView.TreeViewItemWrapper)current;
					list.Add(treeViewItemWrapper.item);
				}
				this.onItemsChosen(list);
			}
		}

		private void OnSelectionChange(IEnumerable<object> selectedListItems)
		{
			bool flag = this.m_SelectedItems == null;
			if (flag)
			{
				this.m_SelectedItems = new List<ITreeViewItem>();
			}
			this.m_SelectedItems.Clear();
			foreach (object current in selectedListItems)
			{
				this.m_SelectedItems.Add(((TreeView.TreeViewItemWrapper)current).item);
			}
			Action<IEnumerable<ITreeViewItem>> expr_68 = this.onSelectionChange;
			if (expr_68 != null)
			{
				expr_68(this.m_SelectedItems);
			}
		}

		private void OnTreeViewMouseUp(MouseUpEvent evt)
		{
			this.m_ScrollView.contentContainer.Focus();
		}

		private void OnItemMouseUp(MouseUpEvent evt)
		{
			bool flag = (evt.modifiers & EventModifiers.Alt) == EventModifiers.None;
			if (!flag)
			{
				VisualElement e = evt.currentTarget as VisualElement;
				Toggle toggle = e.Q(TreeView.s_ItemToggleName, null);
				int index = (int)toggle.userData;
				ITreeViewItem item = this.m_ItemWrappers[index].item;
				bool flag2 = this.IsExpandedByIndex(index);
				bool flag3 = !item.hasChildren;
				if (!flag3)
				{
					HashSet<int> hashSet = new HashSet<int>(this.m_ExpandedItemIds);
					bool flag4 = flag2;
					if (flag4)
					{
						hashSet.Remove(item.id);
					}
					else
					{
						hashSet.Add(item.id);
					}
					foreach (ITreeViewItem current in TreeView.GetAllItems(item.children))
					{
						bool hasChildren = current.hasChildren;
						if (hasChildren)
						{
							bool flag5 = flag2;
							if (flag5)
							{
								hashSet.Remove(current.id);
							}
							else
							{
								hashSet.Add(current.id);
							}
						}
					}
					this.m_ExpandedItemIds = hashSet.ToList<int>();
					this.Refresh();
					evt.StopPropagation();
				}
			}
		}

		private VisualElement MakeTreeItem()
		{
			VisualElement visualElement = new VisualElement
			{
				name = TreeView.s_ItemName,
				style = 
				{
					flexDirection = FlexDirection.Row
				}
			};
			visualElement.AddToClassList(TreeView.s_ItemName);
			visualElement.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnItemMouseUp), TrickleDown.NoTrickleDown);
			VisualElement visualElement2 = new VisualElement
			{
				name = TreeView.s_ItemIndentsContainerName,
				style = 
				{
					flexDirection = FlexDirection.Row
				}
			};
			visualElement2.AddToClassList(TreeView.s_ItemIndentsContainerName);
			visualElement.hierarchy.Add(visualElement2);
			Toggle toggle = new Toggle
			{
				name = TreeView.s_ItemToggleName
			};
			toggle.AddToClassList(Foldout.toggleUssClassName);
			toggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.ToggleExpandedState));
			visualElement.hierarchy.Add(toggle);
			VisualElement visualElement3 = new VisualElement
			{
				name = TreeView.s_ItemContentContainerName,
				style = 
				{
					flexGrow = 1f
				}
			};
			visualElement3.AddToClassList(TreeView.s_ItemContentContainerName);
			visualElement.Add(visualElement3);
			bool flag = this.m_MakeItem != null;
			if (flag)
			{
				visualElement3.Add(this.m_MakeItem());
			}
			return visualElement;
		}

		private void UnbindTreeItem(VisualElement element, int index)
		{
			bool flag = this.unbindItem == null;
			if (!flag)
			{
				ITreeViewItem item = this.m_ItemWrappers[index].item;
				VisualElement arg = element.Q(TreeView.s_ItemContentContainerName, null).ElementAt(0);
				this.unbindItem(arg, item);
			}
		}

		private void BindTreeItem(VisualElement element, int index)
		{
			ITreeViewItem item = this.m_ItemWrappers[index].item;
			VisualElement visualElement = element.Q(TreeView.s_ItemIndentsContainerName, null);
			visualElement.Clear();
			for (int i = 0; i < this.m_ItemWrappers[index].depth; i++)
			{
				VisualElement visualElement2 = new VisualElement();
				visualElement2.AddToClassList(TreeView.s_ItemIndentName);
				visualElement.Add(visualElement2);
			}
			Toggle toggle = element.Q(TreeView.s_ItemToggleName, null);
			toggle.SetValueWithoutNotify(this.IsExpandedByIndex(index));
			toggle.userData = index;
			bool hasChildren = item.hasChildren;
			if (hasChildren)
			{
				toggle.visible = true;
			}
			else
			{
				toggle.visible = false;
			}
			bool flag = this.m_BindItem == null;
			if (!flag)
			{
				VisualElement arg = element.Q(TreeView.s_ItemContentContainerName, null).ElementAt(0);
				this.m_BindItem(arg, item);
			}
		}

		private int GetItemId(int index)
		{
			return this.m_ItemWrappers[index].id;
		}

		private bool IsExpandedByIndex(int index)
		{
			return this.m_ExpandedItemIds.Contains(this.m_ItemWrappers[index].id);
		}

		private void CollapseItemByIndex(int index)
		{
			bool flag = !this.m_ItemWrappers[index].item.hasChildren;
			if (!flag)
			{
				this.m_ExpandedItemIds.Remove(this.m_ItemWrappers[index].item.id);
				int num = 0;
				int num2 = index + 1;
				int depth = this.m_ItemWrappers[index].depth;
				while (num2 < this.m_ItemWrappers.Count && this.m_ItemWrappers[num2].depth > depth)
				{
					num++;
					num2++;
				}
				this.m_ItemWrappers.RemoveRange(index + 1, num);
				this.ListViewRefresh();
				base.SaveViewData();
			}
		}

		private void ExpandItemByIndex(int index)
		{
			bool flag = !this.m_ItemWrappers[index].item.hasChildren;
			if (!flag)
			{
				List<TreeView.TreeViewItemWrapper> collection = new List<TreeView.TreeViewItemWrapper>();
				this.CreateWrappers(this.m_ItemWrappers[index].item.children, this.m_ItemWrappers[index].depth + 1, ref collection);
				this.m_ItemWrappers.InsertRange(index + 1, collection);
				this.m_ExpandedItemIds.Add(this.m_ItemWrappers[index].item.id);
				this.ListViewRefresh();
				base.SaveViewData();
			}
		}

		private void ToggleExpandedState(ChangeEvent<bool> evt)
		{
			Toggle toggle = evt.target as Toggle;
			int index = (int)toggle.userData;
			bool flag = this.IsExpandedByIndex(index);
			Assert.AreNotEqual<bool>(flag, evt.newValue);
			bool flag2 = flag;
			if (flag2)
			{
				this.CollapseItemByIndex(index);
			}
			else
			{
				this.ExpandItemByIndex(index);
			}
			this.m_ScrollView.contentContainer.Focus();
		}

		private void CreateWrappers(IEnumerable<ITreeViewItem> treeViewItems, int depth, ref List<TreeView.TreeViewItemWrapper> wrappers)
		{
			foreach (ITreeViewItem current in treeViewItems)
			{
				TreeView.TreeViewItemWrapper item = new TreeView.TreeViewItemWrapper
				{
					depth = depth,
					item = current
				};
				wrappers.Add(item);
				bool flag = this.m_ExpandedItemIds.Contains(current.id) && current.hasChildren;
				if (flag)
				{
					this.CreateWrappers(current.children, depth + 1, ref wrappers);
				}
			}
		}

		private void RegenerateWrappers()
		{
			this.m_ItemWrappers.Clear();
			bool flag = this.m_RootItems == null;
			if (!flag)
			{
				this.CreateWrappers(this.m_RootItems, 0, ref this.m_ItemWrappers);
			}
		}

		private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			int itemHeight = this.m_ListView.itemHeight;
			int itemHeight2;
			bool flag = !this.m_ListView.m_ItemHeightIsInline && e.customStyle.TryGetValue(ListView.s_ItemHeightProperty, out itemHeight2);
			if (flag)
			{
				this.m_ListView.m_ItemHeight = itemHeight2;
			}
			bool flag2 = this.m_ListView.m_ItemHeight != itemHeight;
			if (flag2)
			{
				this.m_ListView.Refresh();
			}
		}
	}
}
