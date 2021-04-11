using System;

namespace UnityEngine.UIElements
{
	public class WheelEvent : MouseEventBase<WheelEvent>
	{
		public Vector3 delta
		{
			get;
			private set;
		}

		public new static WheelEvent GetPooled(Event systemEvent)
		{
			WheelEvent pooled = MouseEventBase<WheelEvent>.GetPooled(systemEvent);
			pooled.imguiEvent = systemEvent;
			bool flag = systemEvent != null;
			if (flag)
			{
				pooled.delta = systemEvent.delta;
			}
			return pooled;
		}

		internal static WheelEvent GetPooled(Vector3 delta)
		{
			WheelEvent pooled = EventBase<WheelEvent>.GetPooled();
			pooled.delta = delta;
			return pooled;
		}

		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		private void LocalInit()
		{
			this.delta = Vector3.zero;
		}

		public WheelEvent()
		{
			this.LocalInit();
		}
	}
}
