using System;

namespace UnityEngine.UIElements
{
	public class GeometryChangedEvent : EventBase<GeometryChangedEvent>
	{
		public Rect oldRect
		{
			get;
			private set;
		}

		public Rect newRect
		{
			get;
			private set;
		}

		internal int layoutPass
		{
			get;
			set;
		}

		public static GeometryChangedEvent GetPooled(Rect oldRect, Rect newRect)
		{
			GeometryChangedEvent pooled = EventBase<GeometryChangedEvent>.GetPooled();
			pooled.oldRect = oldRect;
			pooled.newRect = newRect;
			return pooled;
		}

		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		private void LocalInit()
		{
			this.oldRect = Rect.zero;
			this.newRect = Rect.zero;
			this.layoutPass = 0;
		}

		public GeometryChangedEvent()
		{
			this.LocalInit();
		}
	}
}
