using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public class Clickable : PointerManipulator
	{
		private readonly long m_Delay;

		private readonly long m_Interval;

		private IVisualElementScheduledItem m_Repeater;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action<EventBase> clickedWithEventInfo;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action clicked;

		protected bool active
		{
			get;
			set;
		}

		public Vector2 lastMousePosition
		{
			get;
			private set;
		}

		public Clickable(Action handler, long delay, long interval) : this(handler)
		{
			this.m_Delay = delay;
			this.m_Interval = interval;
			this.active = false;
		}

		public Clickable(Action<EventBase> handler)
		{
			this.clickedWithEventInfo = handler;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
		}

		public Clickable(Action handler)
		{
			this.clicked = handler;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
			this.active = false;
		}

		private void OnTimer(TimerState timerState)
		{
			bool flag = (this.clicked != null || this.clickedWithEventInfo != null) && this.IsRepeatable();
			if (flag)
			{
				bool flag2 = base.target.ContainsPoint(this.lastMousePosition);
				if (flag2)
				{
					this.Invoke(null);
					base.target.pseudoStates |= PseudoStates.Active;
				}
				else
				{
					base.target.pseudoStates &= ~PseudoStates.Active;
				}
			}
		}

		private bool IsRepeatable()
		{
			return this.m_Delay > 0L || this.m_Interval > 0L;
		}

		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseDown), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<MouseMoveEvent>(new EventCallback<MouseMoveEvent>(this.OnMouseMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUp), TrickleDown.NoTrickleDown);
		}

		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<MouseMoveEvent>(new EventCallback<MouseMoveEvent>(this.OnMouseMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUp), TrickleDown.NoTrickleDown);
		}

		protected void Invoke(EventBase evt)
		{
			Action expr_07 = this.clicked;
			if (expr_07 != null)
			{
				expr_07();
			}
			Action<EventBase> expr_19 = this.clickedWithEventInfo;
			if (expr_19 != null)
			{
				expr_19(evt);
			}
		}

		protected void OnMouseDown(MouseDownEvent evt)
		{
			bool flag = base.CanStartManipulation(evt);
			if (flag)
			{
				this.ProcessDownEvent(evt, evt.localMousePosition, PointerId.mousePointerId);
			}
		}

		protected void OnMouseMove(MouseMoveEvent evt)
		{
			bool active = this.active;
			if (active)
			{
				this.ProcessMoveEvent(evt, evt.localMousePosition);
			}
		}

		protected void OnMouseUp(MouseUpEvent evt)
		{
			bool flag = this.active && base.CanStopManipulation(evt);
			if (flag)
			{
				this.ProcessUpEvent(evt, evt.localMousePosition, PointerId.mousePointerId);
			}
		}

		internal void SimulateSingleClick(EventBase evt, int delayMs = 100)
		{
			base.target.pseudoStates |= PseudoStates.Active;
			base.target.schedule.Execute(delegate
			{
				base.target.pseudoStates &= ~PseudoStates.Active;
			}).ExecuteLater((long)delayMs);
			this.Invoke(evt);
		}

		protected virtual void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = true;
			base.target.CapturePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(PointerId.mousePointerId);
			}
			this.lastMousePosition = localPosition;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				bool flag3 = base.target.ContainsPoint(localPosition);
				if (flag3)
				{
					this.Invoke(evt);
				}
				bool flag4 = this.m_Repeater == null;
				if (flag4)
				{
					this.m_Repeater = base.target.schedule.Execute(new Action<TimerState>(this.OnTimer)).Every(this.m_Interval).StartingIn(this.m_Delay);
				}
				else
				{
					this.m_Repeater.ExecuteLater(this.m_Delay);
				}
			}
			base.target.pseudoStates |= PseudoStates.Active;
			evt.StopImmediatePropagation();
		}

		protected virtual void ProcessMoveEvent(EventBase evt, Vector2 localPosition)
		{
			this.lastMousePosition = localPosition;
			bool flag = base.target.ContainsPoint(localPosition);
			if (flag)
			{
				base.target.pseudoStates |= PseudoStates.Active;
			}
			else
			{
				base.target.pseudoStates &= ~PseudoStates.Active;
			}
			evt.StopPropagation();
		}

		protected virtual void ProcessUpEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = false;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(PointerId.mousePointerId);
			}
			base.target.pseudoStates &= ~PseudoStates.Active;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				IVisualElementScheduledItem expr_62 = this.m_Repeater;
				if (expr_62 != null)
				{
					expr_62.Pause();
				}
			}
			else
			{
				bool flag3 = base.target.ContainsPoint(localPosition);
				if (flag3)
				{
					this.Invoke(evt);
				}
			}
			evt.StopPropagation();
		}
	}
}
