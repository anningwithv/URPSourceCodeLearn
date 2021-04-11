using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal class EventDebuggerEventRecord
	{
		public string eventBaseName
		{
			get;
			private set;
		}

		public long eventTypeId
		{
			get;
			private set;
		}

		public ulong eventId
		{
			get;
			private set;
		}

		private ulong triggerEventId
		{
			get;
			set;
		}

		private long timestamp
		{
			get;
			set;
		}

		public IEventHandler target
		{
			get;
			set;
		}

		private List<IEventHandler> skipElements
		{
			get;
			set;
		}

		public bool hasUnderlyingPhysicalEvent
		{
			get;
			private set;
		}

		private bool isPropagationStopped
		{
			get;
			set;
		}

		private bool isImmediatePropagationStopped
		{
			get;
			set;
		}

		private bool isDefaultPrevented
		{
			get;
			set;
		}

		public PropagationPhase propagationPhase
		{
			get;
			private set;
		}

		private IEventHandler currentTarget
		{
			get;
			set;
		}

		private bool dispatch
		{
			get;
			set;
		}

		private Vector2 originalMousePosition
		{
			get;
			set;
		}

		public EventModifiers modifiers
		{
			get;
			private set;
		}

		public Vector2 mousePosition
		{
			get;
			private set;
		}

		public int clickCount
		{
			get;
			private set;
		}

		public int button
		{
			get;
			private set;
		}

		public int pressedButtons
		{
			get;
			private set;
		}

		public Vector3 delta
		{
			get;
			private set;
		}

		public char character
		{
			get;
			private set;
		}

		public KeyCode keyCode
		{
			get;
			private set;
		}

		public string commandName
		{
			get;
			private set;
		}

		private void Init(EventBase evt)
		{
			this.eventBaseName = evt.GetType().Name;
			this.eventTypeId = evt.eventTypeId;
			this.eventId = evt.eventId;
			this.triggerEventId = evt.triggerEventId;
			this.timestamp = evt.timestamp;
			this.target = evt.target;
			this.skipElements = evt.skipElements;
			this.isPropagationStopped = evt.isPropagationStopped;
			this.isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
			this.isDefaultPrevented = evt.isDefaultPrevented;
			IMouseEvent mouseEvent = evt as IMouseEvent;
			IMouseEventInternal mouseEventInternal = evt as IMouseEventInternal;
			this.hasUnderlyingPhysicalEvent = (mouseEvent != null && mouseEventInternal != null && mouseEventInternal.triggeredByOS);
			this.propagationPhase = evt.propagationPhase;
			this.originalMousePosition = evt.originalMousePosition;
			this.currentTarget = evt.currentTarget;
			this.dispatch = evt.dispatch;
			bool flag = mouseEvent != null;
			if (flag)
			{
				this.modifiers = mouseEvent.modifiers;
				this.mousePosition = mouseEvent.mousePosition;
				this.button = mouseEvent.button;
				this.pressedButtons = mouseEvent.pressedButtons;
				this.clickCount = mouseEvent.clickCount;
			}
			IPointerEvent pointerEvent = evt as IPointerEvent;
			IPointerEventInternal pointerEventInternal = evt as IPointerEventInternal;
			this.hasUnderlyingPhysicalEvent = (pointerEvent != null && pointerEventInternal != null && pointerEventInternal.triggeredByOS);
			bool flag2 = pointerEvent != null;
			if (flag2)
			{
				this.modifiers = pointerEvent.modifiers;
				this.mousePosition = pointerEvent.position;
				this.button = pointerEvent.button;
				this.pressedButtons = pointerEvent.pressedButtons;
				this.clickCount = pointerEvent.clickCount;
			}
			IKeyboardEvent keyboardEvent = evt as IKeyboardEvent;
			bool flag3 = keyboardEvent != null;
			if (flag3)
			{
				this.character = keyboardEvent.character;
				this.keyCode = keyboardEvent.keyCode;
			}
			ICommandEvent commandEvent = evt as ICommandEvent;
			bool flag4 = commandEvent != null;
			if (flag4)
			{
				this.commandName = commandEvent.commandName;
			}
		}

		public EventDebuggerEventRecord(EventBase evt)
		{
			this.Init(evt);
		}

		public string TimestampString()
		{
			long ticks = (long)((float)this.timestamp / 1000f * 1E+07f);
			return new DateTime(ticks).ToString("HH:mm:ss.ffffff");
		}
	}
}
