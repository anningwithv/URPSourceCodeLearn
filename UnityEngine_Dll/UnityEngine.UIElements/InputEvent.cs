using System;

namespace UnityEngine.UIElements
{
	public class InputEvent : EventBase<InputEvent>
	{
		public string previousData
		{
			get;
			protected set;
		}

		public string newData
		{
			get;
			protected set;
		}

		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown);
			this.previousData = null;
			this.newData = null;
		}

		public static InputEvent GetPooled(string previousData, string newData)
		{
			InputEvent pooled = EventBase<InputEvent>.GetPooled();
			pooled.previousData = previousData;
			pooled.newData = newData;
			return pooled;
		}

		public InputEvent()
		{
			this.LocalInit();
		}
	}
}
