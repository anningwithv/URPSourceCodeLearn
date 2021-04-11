using System;

namespace UnityEngine.UIElements
{
	public abstract class CommandEventBase<T> : EventBase<T>, ICommandEvent where T : CommandEventBase<T>, new()
	{
		private string m_CommandName;

		public string commandName
		{
			get
			{
				bool flag = this.m_CommandName == null && base.imguiEvent != null;
				string commandName;
				if (flag)
				{
					commandName = base.imguiEvent.commandName;
				}
				else
				{
					commandName = this.m_CommandName;
				}
				return commandName;
			}
			protected set
			{
				this.m_CommandName = value;
			}
		}

		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable);
			this.commandName = null;
		}

		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.imguiEvent = systemEvent;
			return pooled;
		}

		public static T GetPooled(string commandName)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.commandName = commandName;
			return pooled;
		}

		protected CommandEventBase()
		{
			this.LocalInit();
		}
	}
}
