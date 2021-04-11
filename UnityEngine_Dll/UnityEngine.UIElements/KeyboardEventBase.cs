using System;

namespace UnityEngine.UIElements
{
	public abstract class KeyboardEventBase<T> : EventBase<T>, IKeyboardEvent where T : KeyboardEventBase<T>, new()
	{
		public EventModifiers modifiers
		{
			get;
			protected set;
		}

		public char character
		{
			get;
			protected set;
		}

		public KeyCode keyCode
		{
			get;
			protected set;
		}

		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		public bool actionKey
		{
			get
			{
				bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
				bool result;
				if (flag)
				{
					result = this.commandKey;
				}
				else
				{
					result = this.ctrlKey;
				}
				return result;
			}
		}

		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable);
			this.modifiers = EventModifiers.None;
			this.character = '\0';
			this.keyCode = KeyCode.None;
		}

		public static T GetPooled(char c, KeyCode keyCode, EventModifiers modifiers)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.modifiers = modifiers;
			pooled.character = c;
			pooled.keyCode = keyCode;
			return pooled;
		}

		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.imguiEvent = systemEvent;
			bool flag = systemEvent != null;
			if (flag)
			{
				pooled.modifiers = systemEvent.modifiers;
				pooled.character = systemEvent.character;
				pooled.keyCode = systemEvent.keyCode;
			}
			return pooled;
		}

		protected KeyboardEventBase()
		{
			this.LocalInit();
		}
	}
}
