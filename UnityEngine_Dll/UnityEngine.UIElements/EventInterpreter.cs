using System;

namespace UnityEngine.UIElements
{
	internal class EventInterpreter : IEventInterpreter
	{
		internal static readonly EventInterpreter s_Instance = new EventInterpreter();

		public virtual bool IsActivationEvent(EventBase evt)
		{
			bool flag = evt.eventTypeId == EventBase<KeyDownEvent>.TypeId();
			bool result;
			if (flag)
			{
				KeyDownEvent keyDownEvent = (KeyDownEvent)evt;
				result = (keyDownEvent.keyCode == KeyCode.KeypadEnter || keyDownEvent.keyCode == KeyCode.Return);
			}
			else
			{
				result = false;
			}
			return result;
		}

		public virtual bool IsCancellationEvent(EventBase evt)
		{
			bool flag = evt.eventTypeId == EventBase<KeyDownEvent>.TypeId();
			bool result;
			if (flag)
			{
				KeyDownEvent keyDownEvent = (KeyDownEvent)evt;
				result = (keyDownEvent.keyCode == KeyCode.Escape);
			}
			else
			{
				result = false;
			}
			return result;
		}

		public virtual bool IsNavigationEvent(EventBase evt, out NavigationDirection direction)
		{
			bool flag = evt.eventTypeId == EventBase<KeyDownEvent>.TypeId();
			bool result;
			if (flag)
			{
				result = ((direction = this.GetNavigationDirection((KeyDownEvent)evt)) > NavigationDirection.None);
			}
			else
			{
				direction = NavigationDirection.None;
				result = false;
			}
			return result;
		}

		private NavigationDirection GetNavigationDirection(KeyDownEvent keyDownEvent)
		{
			EventInterpreter.<>c__DisplayClass4_0 <>c__DisplayClass4_;
			<>c__DisplayClass4_.keyDownEvent = keyDownEvent;
			KeyCode keyCode = <>c__DisplayClass4_.keyDownEvent.keyCode;
			KeyCode keyCode2 = keyCode;
			NavigationDirection result;
			if (keyCode2 != KeyCode.Tab)
			{
				switch (keyCode2)
				{
				case KeyCode.UpArrow:
					result = NavigationDirection.Up;
					return result;
				case KeyCode.DownArrow:
					result = NavigationDirection.Down;
					return result;
				case KeyCode.RightArrow:
					result = NavigationDirection.Right;
					return result;
				case KeyCode.LeftArrow:
					result = NavigationDirection.Left;
					return result;
				case KeyCode.Home:
					result = NavigationDirection.Home;
					return result;
				case KeyCode.End:
					result = NavigationDirection.End;
					return result;
				case KeyCode.PageUp:
					result = NavigationDirection.PageUp;
					return result;
				case KeyCode.PageDown:
					result = NavigationDirection.PageDown;
					return result;
				}
				result = NavigationDirection.None;
			}
			else
			{
				result = (EventInterpreter.<GetNavigationDirection>g__Shift|4_0(ref <>c__DisplayClass4_) ? NavigationDirection.Previous : NavigationDirection.Next);
			}
			return result;
		}
	}
}
