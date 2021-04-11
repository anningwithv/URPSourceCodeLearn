using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public class DropdownMenu
	{
		private List<DropdownMenuItem> menuItems = new List<DropdownMenuItem>();

		private DropdownMenuEventInfo m_DropdownMenuEventInfo;

		public List<DropdownMenuItem> MenuItems()
		{
			return this.menuItems;
		}

		public void AppendAction(string actionName, Action<DropdownMenuAction> action, Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback, object userData = null)
		{
			DropdownMenuAction item = new DropdownMenuAction(actionName, action, actionStatusCallback, userData);
			this.menuItems.Add(item);
		}

		public void AppendAction(string actionName, Action<DropdownMenuAction> action, DropdownMenuAction.Status status = DropdownMenuAction.Status.Normal)
		{
			bool flag = status == DropdownMenuAction.Status.Normal;
			if (flag)
			{
				this.AppendAction(actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysEnabled), null);
			}
			else
			{
				bool flag2 = status == DropdownMenuAction.Status.Disabled;
				if (flag2)
				{
					this.AppendAction(actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysDisabled), null);
				}
				else
				{
					this.AppendAction(actionName, action, (DropdownMenuAction e) => status, null);
				}
			}
		}

		public void InsertAction(int atIndex, string actionName, Action<DropdownMenuAction> action, Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback, object userData = null)
		{
			DropdownMenuAction item = new DropdownMenuAction(actionName, action, actionStatusCallback, userData);
			this.menuItems.Insert(atIndex, item);
		}

		public void InsertAction(int atIndex, string actionName, Action<DropdownMenuAction> action, DropdownMenuAction.Status status = DropdownMenuAction.Status.Normal)
		{
			bool flag = status == DropdownMenuAction.Status.Normal;
			if (flag)
			{
				this.InsertAction(atIndex, actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysEnabled), null);
			}
			else
			{
				bool flag2 = status == DropdownMenuAction.Status.Disabled;
				if (flag2)
				{
					this.InsertAction(atIndex, actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysDisabled), null);
				}
				else
				{
					this.InsertAction(atIndex, actionName, action, (DropdownMenuAction e) => status, null);
				}
			}
		}

		public void AppendSeparator(string subMenuPath = null)
		{
			bool flag = this.menuItems.Count > 0 && !(this.menuItems[this.menuItems.Count - 1] is DropdownMenuSeparator);
			if (flag)
			{
				DropdownMenuSeparator item = new DropdownMenuSeparator(subMenuPath ?? string.Empty);
				this.menuItems.Add(item);
			}
		}

		public void InsertSeparator(string subMenuPath, int atIndex)
		{
			bool flag = atIndex > 0 && atIndex <= this.menuItems.Count && !(this.menuItems[atIndex - 1] is DropdownMenuSeparator);
			if (flag)
			{
				DropdownMenuSeparator item = new DropdownMenuSeparator(subMenuPath ?? string.Empty);
				this.menuItems.Insert(atIndex, item);
			}
		}

		public void RemoveItemAt(int index)
		{
			this.menuItems.RemoveAt(index);
		}

		public void PrepareForDisplay(EventBase e)
		{
			this.m_DropdownMenuEventInfo = ((e != null) ? new DropdownMenuEventInfo(e) : null);
			bool flag = this.menuItems.Count == 0;
			if (!flag)
			{
				foreach (DropdownMenuItem current in this.menuItems)
				{
					DropdownMenuAction dropdownMenuAction = current as DropdownMenuAction;
					bool flag2 = dropdownMenuAction != null;
					if (flag2)
					{
						dropdownMenuAction.UpdateActionStatus(this.m_DropdownMenuEventInfo);
					}
				}
				bool flag3 = this.menuItems[this.menuItems.Count - 1] is DropdownMenuSeparator;
				if (flag3)
				{
					this.menuItems.RemoveAt(this.menuItems.Count - 1);
				}
			}
		}
	}
}
