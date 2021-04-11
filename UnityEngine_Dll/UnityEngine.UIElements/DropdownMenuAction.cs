using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public class DropdownMenuAction : DropdownMenuItem
	{
		[Flags]
		public enum Status
		{
			None = 0,
			Normal = 1,
			Disabled = 2,
			Checked = 4,
			Hidden = 8
		}

		private readonly Action<DropdownMenuAction> actionCallback;

		private readonly Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback;

		public string name
		{
			[CompilerGenerated]
			get
			{
				return this.<name>k__BackingField;
			}
		}

		public DropdownMenuAction.Status status
		{
			get;
			private set;
		}

		public DropdownMenuEventInfo eventInfo
		{
			get;
			private set;
		}

		public object userData
		{
			get;
			private set;
		}

		public static DropdownMenuAction.Status AlwaysEnabled(DropdownMenuAction a)
		{
			return DropdownMenuAction.Status.Normal;
		}

		public static DropdownMenuAction.Status AlwaysDisabled(DropdownMenuAction a)
		{
			return DropdownMenuAction.Status.Disabled;
		}

		public DropdownMenuAction(string actionName, Action<DropdownMenuAction> actionCallback, Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback, object userData = null)
		{
			this.<name>k__BackingField = actionName;
			this.actionCallback = actionCallback;
			this.actionStatusCallback = actionStatusCallback;
			this.userData = userData;
		}

		public void UpdateActionStatus(DropdownMenuEventInfo eventInfo)
		{
			this.eventInfo = eventInfo;
			Func<DropdownMenuAction, DropdownMenuAction.Status> expr_10 = this.actionStatusCallback;
			this.status = ((expr_10 != null) ? expr_10(this) : DropdownMenuAction.Status.Hidden);
		}

		public void Execute()
		{
			Action<DropdownMenuAction> expr_07 = this.actionCallback;
			if (expr_07 != null)
			{
				expr_07(this);
			}
		}
	}
}
