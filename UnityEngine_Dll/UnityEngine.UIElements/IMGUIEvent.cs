using System;

namespace UnityEngine.UIElements
{
	public class IMGUIEvent : EventBase<IMGUIEvent>
	{
		public static IMGUIEvent GetPooled(Event systemEvent)
		{
			IMGUIEvent pooled = EventBase<IMGUIEvent>.GetPooled();
			pooled.imguiEvent = systemEvent;
			return pooled;
		}

		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable);
		}

		public IMGUIEvent()
		{
			this.LocalInit();
		}
	}
}
