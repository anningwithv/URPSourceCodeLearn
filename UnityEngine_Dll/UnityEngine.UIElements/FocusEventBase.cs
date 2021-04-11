using System;

namespace UnityEngine.UIElements
{
	public abstract class FocusEventBase<T> : EventBase<T>, IFocusEvent where T : FocusEventBase<T>, new()
	{
		public Focusable relatedTarget
		{
			get;
			private set;
		}

		public FocusChangeDirection direction
		{
			get;
			private set;
		}

		protected FocusController focusController
		{
			get;
			private set;
		}

		internal bool IsFocusDelegated
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
			base.propagation = EventBase.EventPropagation.TricklesDown;
			this.relatedTarget = null;
			this.direction = FocusChangeDirection.unspecified;
			this.focusController = null;
		}

		public static T GetPooled(IEventHandler target, Focusable relatedTarget, FocusChangeDirection direction, FocusController focusController, bool bIsFocusDelegated = false)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.target = target;
			pooled.relatedTarget = relatedTarget;
			pooled.direction = direction;
			pooled.focusController = focusController;
			pooled.IsFocusDelegated = bIsFocusDelegated;
			return pooled;
		}

		protected FocusEventBase()
		{
			this.LocalInit();
		}
	}
}
