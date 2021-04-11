using System;

namespace UnityEngine.UIElements
{
	public abstract class PointerEventBase<T> : EventBase<T>, IPointerEvent, IPointerEventInternal where T : PointerEventBase<T>, new()
	{
		public int pointerId
		{
			get;
			protected set;
		}

		public string pointerType
		{
			get;
			protected set;
		}

		public bool isPrimary
		{
			get;
			protected set;
		}

		public int button
		{
			get;
			protected set;
		}

		public int pressedButtons
		{
			get;
			protected set;
		}

		public Vector3 position
		{
			get;
			protected set;
		}

		public Vector3 localPosition
		{
			get;
			protected set;
		}

		public Vector3 deltaPosition
		{
			get;
			protected set;
		}

		public float deltaTime
		{
			get;
			protected set;
		}

		public int clickCount
		{
			get;
			protected set;
		}

		public float pressure
		{
			get;
			protected set;
		}

		public float tangentialPressure
		{
			get;
			protected set;
		}

		public float altitudeAngle
		{
			get;
			protected set;
		}

		public float azimuthAngle
		{
			get;
			protected set;
		}

		public float twist
		{
			get;
			protected set;
		}

		public Vector2 radius
		{
			get;
			protected set;
		}

		public Vector2 radiusVariance
		{
			get;
			protected set;
		}

		public EventModifiers modifiers
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

		bool IPointerEventInternal.triggeredByOS
		{
			get;
			set;
		}

		bool IPointerEventInternal.recomputeTopElementUnderPointer
		{
			get;
			set;
		}

		public override IEventHandler currentTarget
		{
			get
			{
				return base.currentTarget;
			}
			internal set
			{
				base.currentTarget = value;
				VisualElement visualElement = this.currentTarget as VisualElement;
				bool flag = visualElement != null;
				if (flag)
				{
					this.localPosition = visualElement.WorldToLocal(this.position);
				}
				else
				{
					this.localPosition = this.position;
				}
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
			base.propagateToIMGUI = false;
			this.pointerId = 0;
			this.pointerType = PointerType.unknown;
			this.isPrimary = false;
			this.button = -1;
			this.pressedButtons = 0;
			this.position = Vector3.zero;
			this.localPosition = Vector3.zero;
			this.deltaPosition = Vector3.zero;
			this.deltaTime = 0f;
			this.clickCount = 0;
			this.pressure = 0f;
			this.tangentialPressure = 0f;
			this.altitudeAngle = 0f;
			this.azimuthAngle = 0f;
			this.twist = 0f;
			this.radius = Vector2.zero;
			this.radiusVariance = Vector2.zero;
			this.modifiers = EventModifiers.None;
			((IPointerEventInternal)this).triggeredByOS = false;
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = false;
		}

		private static bool IsMouse(Event systemEvent)
		{
			EventType rawType = systemEvent.rawType;
			return rawType == EventType.MouseMove || rawType == EventType.MouseDown || rawType == EventType.MouseUp || rawType == EventType.MouseDrag || rawType == EventType.ContextClick || rawType == EventType.MouseEnterWindow || rawType == EventType.MouseLeaveWindow;
		}

		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = !PointerEventBase<T>.IsMouse(systemEvent) && systemEvent.rawType != EventType.DragUpdated;
			if (flag)
			{
				Debug.Assert(false, string.Concat(new string[]
				{
					"Unexpected event type: ",
					systemEvent.rawType.ToString(),
					" (",
					systemEvent.type.ToString(),
					")"
				}));
			}
			UnityEngine.PointerType pointerType = systemEvent.pointerType;
			UnityEngine.PointerType pointerType2 = pointerType;
			if (pointerType2 != UnityEngine.PointerType.Touch)
			{
				if (pointerType2 != UnityEngine.PointerType.Pen)
				{
					pooled.pointerType = PointerType.mouse;
					pooled.pointerId = PointerId.mousePointerId;
				}
				else
				{
					pooled.pointerType = PointerType.pen;
					pooled.pointerId = PointerId.penPointerIdBase;
				}
			}
			else
			{
				pooled.pointerType = PointerType.touch;
				pooled.pointerId = PointerId.touchPointerIdBase;
			}
			pooled.isPrimary = true;
			pooled.altitudeAngle = 0f;
			pooled.azimuthAngle = 0f;
			pooled.twist = 0f;
			pooled.radius = Vector2.zero;
			pooled.radiusVariance = Vector2.zero;
			pooled.imguiEvent = systemEvent;
			bool flag2 = systemEvent.rawType == EventType.MouseDown;
			if (flag2)
			{
				PointerDeviceState.PressButton(PointerId.mousePointerId, systemEvent.button);
				pooled.button = systemEvent.button;
			}
			else
			{
				bool flag3 = systemEvent.rawType == EventType.MouseUp;
				if (flag3)
				{
					PointerDeviceState.ReleaseButton(PointerId.mousePointerId, systemEvent.button);
					pooled.button = systemEvent.button;
				}
				else
				{
					bool flag4 = systemEvent.rawType == EventType.MouseMove;
					if (flag4)
					{
						pooled.button = -1;
					}
				}
			}
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(pooled.pointerId);
			pooled.position = systemEvent.mousePosition;
			pooled.localPosition = systemEvent.mousePosition;
			pooled.deltaPosition = systemEvent.delta;
			pooled.clickCount = systemEvent.clickCount;
			pooled.modifiers = systemEvent.modifiers;
			UnityEngine.PointerType pointerType3 = systemEvent.pointerType;
			UnityEngine.PointerType pointerType4 = pointerType3;
			if (pointerType4 - UnityEngine.PointerType.Touch > 1)
			{
				pooled.pressure = ((pooled.pressedButtons == 0) ? 0f : 0.5f);
			}
			else
			{
				pooled.pressure = systemEvent.pressure;
			}
			pooled.tangentialPressure = 0f;
			pooled.triggeredByOS = true;
			return pooled;
		}

		public static T GetPooled(Touch touch, EventModifiers modifiers = EventModifiers.None)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.pointerId = touch.fingerId + PointerId.touchPointerIdBase;
			pooled.pointerType = PointerType.touch;
			bool flag = false;
			for (int i = PointerId.touchPointerIdBase; i < PointerId.touchPointerIdBase + PointerId.touchPointerCount; i++)
			{
				bool flag2 = i != pooled.pointerId && PointerDeviceState.GetPressedButtons(i) != 0;
				if (flag2)
				{
					flag = true;
					break;
				}
			}
			pooled.isPrimary = !flag;
			bool flag3 = touch.phase == TouchPhase.Began;
			if (flag3)
			{
				PointerDeviceState.PressButton(pooled.pointerId, 0);
				pooled.button = 0;
			}
			else
			{
				bool flag4 = touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
				if (flag4)
				{
					PointerDeviceState.ReleaseButton(pooled.pointerId, 0);
					pooled.button = 0;
				}
				else
				{
					pooled.button = -1;
				}
			}
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(pooled.pointerId);
			pooled.position = touch.position;
			pooled.localPosition = touch.position;
			pooled.deltaPosition = touch.deltaPosition;
			pooled.deltaTime = touch.deltaTime;
			pooled.clickCount = touch.tapCount;
			pooled.pressure = ((Mathf.Abs(touch.maximumPossiblePressure) > Mathf.Epsilon) ? (touch.pressure / touch.maximumPossiblePressure) : 1f);
			pooled.tangentialPressure = 0f;
			pooled.altitudeAngle = touch.altitudeAngle;
			pooled.azimuthAngle = touch.azimuthAngle;
			pooled.twist = 0f;
			pooled.radius = new Vector2(touch.radius, touch.radius);
			pooled.radiusVariance = new Vector2(touch.radiusVariance, touch.radiusVariance);
			pooled.modifiers = modifiers;
			pooled.triggeredByOS = true;
			return pooled;
		}

		internal static T GetPooled(IPointerEvent triggerEvent, Vector2 position, int pointerId)
		{
			bool flag = triggerEvent != null;
			T result;
			if (flag)
			{
				result = PointerEventBase<T>.GetPooled(triggerEvent);
			}
			else
			{
				T pooled = EventBase<T>.GetPooled();
				pooled.position = position;
				pooled.localPosition = position;
				pooled.pointerId = pointerId;
				pooled.pointerType = PointerType.GetPointerType(pointerId);
				result = pooled;
			}
			return result;
		}

		public static T GetPooled(IPointerEvent triggerEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = triggerEvent != null;
			if (flag)
			{
				pooled.pointerId = triggerEvent.pointerId;
				pooled.pointerType = triggerEvent.pointerType;
				pooled.isPrimary = triggerEvent.isPrimary;
				pooled.button = triggerEvent.button;
				pooled.pressedButtons = triggerEvent.pressedButtons;
				pooled.position = triggerEvent.position;
				pooled.localPosition = triggerEvent.localPosition;
				pooled.deltaPosition = triggerEvent.deltaPosition;
				pooled.deltaTime = triggerEvent.deltaTime;
				pooled.clickCount = triggerEvent.clickCount;
				pooled.pressure = triggerEvent.pressure;
				pooled.tangentialPressure = triggerEvent.tangentialPressure;
				pooled.altitudeAngle = triggerEvent.altitudeAngle;
				pooled.azimuthAngle = triggerEvent.azimuthAngle;
				pooled.twist = triggerEvent.twist;
				pooled.radius = triggerEvent.radius;
				pooled.radiusVariance = triggerEvent.radiusVariance;
				pooled.modifiers = triggerEvent.modifiers;
				IPointerEventInternal pointerEventInternal = triggerEvent as IPointerEventInternal;
				bool flag2 = pointerEventInternal != null;
				if (flag2)
				{
					pooled.triggeredByOS = pointerEventInternal.triggeredByOS;
				}
			}
			return pooled;
		}

		internal static T GetPooled(IMouseEvent triggerEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = triggerEvent != null;
			if (flag)
			{
				pooled.pointerId = PointerId.mousePointerId;
				pooled.pointerType = PointerType.mouse;
				pooled.isPrimary = true;
				pooled.button = triggerEvent.button;
				pooled.pressedButtons = triggerEvent.pressedButtons;
				pooled.position = triggerEvent.mousePosition;
				pooled.localPosition = triggerEvent.mousePosition;
				pooled.deltaPosition = triggerEvent.mouseDelta;
				pooled.deltaTime = 0f;
				pooled.clickCount = triggerEvent.clickCount;
				pooled.pressure = ((triggerEvent.pressedButtons == 0) ? 0f : 0.5f);
				pooled.tangentialPressure = 0f;
				pooled.altitudeAngle = 0f;
				pooled.azimuthAngle = 0f;
				pooled.twist = 0f;
				pooled.radius = default(Vector2);
				pooled.radiusVariance = default(Vector2);
				pooled.modifiers = triggerEvent.modifiers;
				IMouseEventInternal mouseEventInternal = triggerEvent as IMouseEventInternal;
				bool flag2 = mouseEventInternal != null;
				if (flag2)
				{
					pooled.triggeredByOS = mouseEventInternal.triggeredByOS;
				}
			}
			return pooled;
		}

		internal new static T GetPooled(EventBase e)
		{
			IPointerEvent pointerEvent = e as IPointerEvent;
			bool flag = pointerEvent != null;
			T pooled;
			if (flag)
			{
				pooled = PointerEventBase<T>.GetPooled(pointerEvent);
			}
			else
			{
				IMouseEvent mouseEvent = e as IMouseEvent;
				bool flag2 = mouseEvent != null;
				if (flag2)
				{
					pooled = PointerEventBase<T>.GetPooled(mouseEvent);
				}
				else
				{
					pooled = EventBase<T>.GetPooled(e);
				}
			}
			return pooled;
		}

		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool triggeredByOS = ((IPointerEventInternal)this).triggeredByOS;
			if (triggeredByOS)
			{
				PointerDeviceState.SavePointerPosition(this.pointerId, this.position, panel);
			}
		}

		protected internal override void PostDispatch(IPanel panel)
		{
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				panel.ProcessPointerCapture(i);
			}
			bool flag = !panel.ShouldSendCompatibilityMouseEvents(this) && ((IPointerEventInternal)this).triggeredByOS;
			if (flag)
			{
				BaseVisualElementPanel expr_3C = panel as BaseVisualElementPanel;
				if (expr_3C != null)
				{
					expr_3C.CommitElementUnderPointers();
				}
			}
			base.PostDispatch(panel);
		}

		protected PointerEventBase()
		{
			this.LocalInit();
		}
	}
}
