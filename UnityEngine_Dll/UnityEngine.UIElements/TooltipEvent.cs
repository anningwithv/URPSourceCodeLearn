using System;

namespace UnityEngine.UIElements
{
	public class TooltipEvent : EventBase<TooltipEvent>
	{
		public string tooltip
		{
			get;
			set;
		}

		public Rect rect
		{
			get;
			set;
		}

		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown);
			this.rect = default(Rect);
			this.tooltip = string.Empty;
		}

		internal static TooltipEvent GetPooled(string tooltip, Rect rect)
		{
			TooltipEvent pooled = EventBase<TooltipEvent>.GetPooled();
			pooled.tooltip = tooltip;
			pooled.rect = rect;
			return pooled;
		}

		public TooltipEvent()
		{
			this.LocalInit();
		}
	}
}
