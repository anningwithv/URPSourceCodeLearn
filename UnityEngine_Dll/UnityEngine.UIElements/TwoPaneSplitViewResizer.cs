using System;

namespace UnityEngine.UIElements
{
	internal class TwoPaneSplitViewResizer : MouseManipulator
	{
		private Vector2 m_Start;

		protected bool m_Active;

		private TwoPaneSplitView m_SplitView;

		private int m_Direction;

		private TwoPaneSplitViewOrientation m_Orientation;

		private VisualElement fixedPane
		{
			get
			{
				return this.m_SplitView.fixedPane;
			}
		}

		private VisualElement flexedPane
		{
			get
			{
				return this.m_SplitView.flexedPane;
			}
		}

		private float fixedPaneMinDimension
		{
			get
			{
				bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				float value;
				if (flag)
				{
					value = this.fixedPane.resolvedStyle.minWidth.value;
				}
				else
				{
					value = this.fixedPane.resolvedStyle.minHeight.value;
				}
				return value;
			}
		}

		private float flexedPaneMinDimension
		{
			get
			{
				bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				float value;
				if (flag)
				{
					value = this.flexedPane.resolvedStyle.minWidth.value;
				}
				else
				{
					value = this.flexedPane.resolvedStyle.minHeight.value;
				}
				return value;
			}
		}

		public TwoPaneSplitViewResizer(TwoPaneSplitView splitView, int dir, TwoPaneSplitViewOrientation orientation)
		{
			this.m_Orientation = orientation;
			this.m_SplitView = splitView;
			this.m_Direction = dir;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
			this.m_Active = false;
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

		public void ApplyDelta(float delta)
		{
			float num = (this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal) ? this.fixedPane.resolvedStyle.width : this.fixedPane.resolvedStyle.height;
			float num2 = num + delta;
			bool flag = num2 < num && num2 < this.fixedPaneMinDimension;
			if (flag)
			{
				num2 = this.fixedPaneMinDimension;
			}
			float num3 = (this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal) ? this.m_SplitView.resolvedStyle.width : this.m_SplitView.resolvedStyle.height;
			num3 -= this.flexedPaneMinDimension;
			bool flag2 = num2 > num && num2 > num3;
			if (flag2)
			{
				num2 = num3;
			}
			bool flag3 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag3)
			{
				this.fixedPane.style.width = num2;
				bool flag4 = this.m_SplitView.fixedPaneIndex == 0;
				if (flag4)
				{
					base.target.style.left = num2;
				}
				else
				{
					base.target.style.left = this.m_SplitView.resolvedStyle.width - num2;
				}
			}
			else
			{
				this.fixedPane.style.height = num2;
				bool flag5 = this.m_SplitView.fixedPaneIndex == 0;
				if (flag5)
				{
					base.target.style.top = num2;
				}
				else
				{
					base.target.style.top = this.m_SplitView.resolvedStyle.height - num2;
				}
			}
		}

		protected void OnMouseDown(MouseDownEvent e)
		{
			bool active = this.m_Active;
			if (active)
			{
				e.StopImmediatePropagation();
			}
			else
			{
				bool flag = base.CanStartManipulation(e);
				if (flag)
				{
					this.m_Start = e.localMousePosition;
					this.m_Active = true;
					base.target.CaptureMouse();
					e.StopPropagation();
				}
			}
		}

		protected void OnMouseMove(MouseMoveEvent e)
		{
			bool flag = !this.m_Active || !base.target.HasMouseCapture();
			if (!flag)
			{
				Vector2 vector = e.localMousePosition - this.m_Start;
				float num = vector.x;
				bool flag2 = this.m_Orientation == TwoPaneSplitViewOrientation.Vertical;
				if (flag2)
				{
					num = vector.y;
				}
				float delta = (float)this.m_Direction * num;
				this.ApplyDelta(delta);
				e.StopPropagation();
			}
		}

		protected void OnMouseUp(MouseUpEvent e)
		{
			bool flag = !this.m_Active || !base.target.HasMouseCapture() || !base.CanStopManipulation(e);
			if (!flag)
			{
				this.m_Active = false;
				base.target.ReleaseMouse();
				e.StopPropagation();
			}
		}
	}
}
