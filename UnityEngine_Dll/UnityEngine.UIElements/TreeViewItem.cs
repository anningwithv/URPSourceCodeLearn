using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal class TreeViewItem<T> : ITreeViewItem
	{
		internal TreeViewItem<T> m_Parent;

		private List<ITreeViewItem> m_Children;

		public int id
		{
			get;
			private set;
		}

		public ITreeViewItem parent
		{
			get
			{
				return this.m_Parent;
			}
		}

		public IEnumerable<ITreeViewItem> children
		{
			get
			{
				return this.m_Children;
			}
		}

		public bool hasChildren
		{
			get
			{
				return this.m_Children != null && this.m_Children.Count > 0;
			}
		}

		public T data
		{
			get;
			private set;
		}

		public TreeViewItem(int id, T data, List<TreeViewItem<T>> children = null)
		{
			this.id = id;
			this.data = data;
			bool flag = children != null;
			if (flag)
			{
				foreach (TreeViewItem<T> current in children)
				{
					this.AddChild(current);
				}
			}
		}

		public void AddChild(ITreeViewItem child)
		{
			TreeViewItem<T> treeViewItem = child as TreeViewItem<T>;
			bool flag = treeViewItem == null;
			if (!flag)
			{
				bool flag2 = this.m_Children == null;
				if (flag2)
				{
					this.m_Children = new List<ITreeViewItem>();
				}
				this.m_Children.Add(treeViewItem);
				treeViewItem.m_Parent = this;
			}
		}

		public void AddChildren(IList<ITreeViewItem> children)
		{
			foreach (ITreeViewItem current in children)
			{
				this.AddChild(current);
			}
		}

		public void RemoveChild(ITreeViewItem child)
		{
			bool flag = this.m_Children == null;
			if (!flag)
			{
				TreeViewItem<T> treeViewItem = child as TreeViewItem<T>;
				bool flag2 = treeViewItem == null;
				if (!flag2)
				{
					this.m_Children.Remove(treeViewItem);
				}
			}
		}
	}
}
