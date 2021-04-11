using System;

namespace UnityEngine.UIElements
{
	public class ContextualMenuManipulator : MouseManipulator
	{
		private Action<ContextualMenuPopulateEvent> m_MenuBuilder;

		public ContextualMenuManipulator(Action<ContextualMenuPopulateEvent> menuBuilder)
		{
			this.m_MenuBuilder = menuBuilder;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.RightMouse
			});
			bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
			if (flag)
			{
				base.activators.Add(new ManipulatorActivationFilter
				{
					button = MouseButton.LeftMouse,
					modifiers = EventModifiers.Control
				});
			}
		}

		protected override void RegisterCallbacksOnTarget()
		{
			bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
			if (flag)
			{
				base.target.RegisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseUpDownEvent), TrickleDown.NoTrickleDown);
			}
			else
			{
				base.target.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUpDownEvent), TrickleDown.NoTrickleDown);
			}
			base.target.RegisterCallback<KeyUpEvent>(new EventCallback<KeyUpEvent>(this.OnKeyUpEvent), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<ContextualMenuPopulateEvent>(new EventCallback<ContextualMenuPopulateEvent>(this.OnContextualMenuEvent), TrickleDown.NoTrickleDown);
		}

		protected override void UnregisterCallbacksFromTarget()
		{
			bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
			if (flag)
			{
				base.target.UnregisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseUpDownEvent), TrickleDown.NoTrickleDown);
			}
			else
			{
				base.target.UnregisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUpDownEvent), TrickleDown.NoTrickleDown);
			}
			base.target.UnregisterCallback<KeyUpEvent>(new EventCallback<KeyUpEvent>(this.OnKeyUpEvent), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<ContextualMenuPopulateEvent>(new EventCallback<ContextualMenuPopulateEvent>(this.OnContextualMenuEvent), TrickleDown.NoTrickleDown);
		}

		private void OnMouseUpDownEvent(IMouseEvent evt)
		{
			bool flag = base.CanStartManipulation(evt);
			if (flag)
			{
				bool flag2 = base.target.elementPanel != null && base.target.elementPanel.contextualMenuManager != null;
				if (flag2)
				{
					EventBase eventBase = evt as EventBase;
					base.target.elementPanel.contextualMenuManager.DisplayMenu(eventBase, base.target);
					eventBase.StopPropagation();
					eventBase.PreventDefault();
				}
			}
		}

		private void OnKeyUpEvent(KeyUpEvent evt)
		{
			bool flag = evt.keyCode == KeyCode.Menu;
			if (flag)
			{
				bool flag2 = base.target.elementPanel != null && base.target.elementPanel.contextualMenuManager != null;
				if (flag2)
				{
					base.target.elementPanel.contextualMenuManager.DisplayMenu(evt, base.target);
					evt.StopPropagation();
					evt.PreventDefault();
				}
			}
		}

		private void OnContextualMenuEvent(ContextualMenuPopulateEvent evt)
		{
			bool flag = this.m_MenuBuilder != null;
			if (flag)
			{
				this.m_MenuBuilder(evt);
			}
		}
	}
}
