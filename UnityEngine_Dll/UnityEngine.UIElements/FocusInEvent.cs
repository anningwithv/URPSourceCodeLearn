using System;

namespace UnityEngine.UIElements
{
	public class FocusInEvent : FocusEventBase<FocusInEvent>
	{
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown);
		}

		public FocusInEvent()
		{
			this.LocalInit();
		}
	}
}
