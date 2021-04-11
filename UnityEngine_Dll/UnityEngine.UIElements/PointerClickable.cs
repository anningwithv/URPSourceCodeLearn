using System;

namespace UnityEngine.UIElements
{
	internal class PointerClickable : Clickable
	{
		public Vector2 lastPointerPosition
		{
			get
			{
				return base.lastMousePosition;
			}
		}

		public PointerClickable(Action handler) : base(handler)
		{
		}

		public PointerClickable(Action<EventBase> handler) : base(handler)
		{
		}

		public PointerClickable(Action handler, long delay, long interval) : base(handler, delay, interval)
		{
		}

		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.RegisterCallbacksOnTarget();
		}

		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.UnregisterCallbacksFromTarget();
		}

		protected void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = !base.CanStartManipulation(evt);
			if (!flag)
			{
				bool flag2 = evt.pointerId != PointerId.mousePointerId;
				if (flag2)
				{
					this.ProcessDownEvent(evt, evt.localPosition, evt.pointerId);
					evt.PreventDefault();
				}
				else
				{
					evt.StopImmediatePropagation();
				}
			}
		}

		protected void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = evt.pointerId != PointerId.mousePointerId && base.active;
			if (flag)
			{
				this.ProcessMoveEvent(evt, evt.localPosition);
			}
		}

		protected void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = evt.pointerId != PointerId.mousePointerId && base.active && base.CanStopManipulation(evt);
			if (flag)
			{
				this.ProcessUpEvent(evt, evt.localPosition, evt.pointerId);
			}
		}
	}
}
