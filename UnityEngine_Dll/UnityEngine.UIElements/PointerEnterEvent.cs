using System;

namespace UnityEngine.UIElements
{
	public sealed class PointerEnterEvent : PointerEventBase<PointerEnterEvent>
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

		public PointerEnterEvent()
		{
			this.LocalInit();
		}
	}
}
