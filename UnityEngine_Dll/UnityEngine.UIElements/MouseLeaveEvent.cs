using System;

namespace UnityEngine.UIElements
{
	public class MouseLeaveEvent : MouseEventBase<MouseLeaveEvent>
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

		public MouseLeaveEvent()
		{
			this.LocalInit();
		}
	}
}
