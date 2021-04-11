using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	internal class ClampedDragger<T> : PointerClickable where T : IComparable<T>
	{
		[Flags]
		public enum DragDirection
		{
			None = 0,
			LowToHigh = 1,
			HighToLow = 2,
			Free = 4
		}

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action dragging;

		public ClampedDragger<T>.DragDirection dragDirection
		{
			get;
			set;
		}

		private BaseSlider<T> slider
		{
			get;
			set;
		}

		public Vector2 startMousePosition
		{
			get;
			private set;
		}

		public Vector2 delta
		{
			get
			{
				return base.lastMousePosition - this.startMousePosition;
			}
		}

		public ClampedDragger(BaseSlider<T> slider, Action clickHandler, Action dragHandler) : base(clickHandler, 250L, 30L)
		{
			this.dragDirection = ClampedDragger<T>.DragDirection.None;
			this.slider = slider;
			this.dragging += dragHandler;
		}

		protected override void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.startMousePosition = localPosition;
			this.dragDirection = ClampedDragger<T>.DragDirection.None;
			base.ProcessDownEvent(evt, localPosition, pointerId);
		}

		protected override void ProcessMoveEvent(EventBase evt, Vector2 localPosition)
		{
			base.ProcessMoveEvent(evt, localPosition);
			bool flag = this.dragDirection == ClampedDragger<T>.DragDirection.None;
			if (flag)
			{
				this.dragDirection = ClampedDragger<T>.DragDirection.Free;
			}
			bool flag2 = this.dragDirection == ClampedDragger<T>.DragDirection.Free;
			if (flag2)
			{
				Action expr_33 = this.dragging;
				if (expr_33 != null)
				{
					expr_33();
				}
			}
		}
	}
}
