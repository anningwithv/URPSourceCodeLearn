using System;

namespace UnityEngine.UIElements
{
	public sealed class PointerLeaveEvent : PointerEventBase<PointerLeaveEvent>
	{
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
		}

		public PointerLeaveEvent()
		{
			this.LocalInit();
		}
	}
}
