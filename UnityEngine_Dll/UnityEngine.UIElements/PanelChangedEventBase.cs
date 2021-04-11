using System;

namespace UnityEngine.UIElements
{
	public abstract class PanelChangedEventBase<T> : EventBase<T>, IPanelChangedEvent where T : PanelChangedEventBase<T>, new()
	{
		public IPanel originPanel
		{
			get;
			private set;
		}

		public IPanel destinationPanel
		{
			get;
			private set;
		}

		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		private void LocalInit()
		{
			this.originPanel = null;
			this.destinationPanel = null;
		}

		public static T GetPooled(IPanel originPanel, IPanel destinationPanel)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.originPanel = originPanel;
			pooled.destinationPanel = destinationPanel;
			return pooled;
		}

		protected PanelChangedEventBase()
		{
			this.LocalInit();
		}
	}
}
