using System;

namespace UnityEngine.UIElements
{
	public class DragLeaveEvent : DragAndDropEventBase<DragLeaveEvent>
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

		public DragLeaveEvent()
		{
			this.LocalInit();
		}
	}
}
