using System;

namespace UnityEngine.UIElements
{
	public class DragEnterEvent : DragAndDropEventBase<DragEnterEvent>
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

		public DragEnterEvent()
		{
			this.LocalInit();
		}
	}
}
