using System;

namespace UnityEngine.UIElements
{
	public class MouseEnterEvent : MouseEventBase<MouseEnterEvent>
	{
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable);
		}

		public MouseEnterEvent()
		{
			this.LocalInit();
		}
	}
}
