using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public abstract class EventBase : IDisposable
	{
		[Flags]
		internal enum EventPropagation
		{
			None = 0,
			Bubbles = 1,
			TricklesDown = 2,
			Cancellable = 4
		}

		[Flags]
		private enum LifeCycleStatus
		{
			None = 0,
			PropagationStopped = 1,
			ImmediatePropagationStopped = 2,
			DefaultPrevented = 4,
			Dispatching = 8,
			Pooled = 16,
			IMGUIEventIsValid = 32,
			StopDispatch = 64,
			PropagateToIMGUI = 128,
			Dispatched = 512,
			Processed = 1024,
			ProcessedByFocusController = 2048
		}

		private static long s_LastTypeId = 0L;

		private static ulong s_NextEventId = 0uL;

		private PropagationPaths m_Path;

		private IEventHandler m_Target;

		private IEventHandler m_CurrentTarget;

		private Event m_ImguiEvent;

		public virtual long eventTypeId
		{
			get
			{
				return -1L;
			}
		}

		public long timestamp
		{
			get;
			private set;
		}

		internal ulong eventId
		{
			get;
			private set;
		}

		internal ulong triggerEventId
		{
			get;
			private set;
		}

		internal EventBase.EventPropagation propagation
		{
			get;
			set;
		}

		internal PropagationPaths path
		{
			get
			{
				bool flag = this.m_Path == null;
				if (flag)
				{
					PropagationPaths.Type type = this.tricklesDown ? PropagationPaths.Type.TrickleDown : PropagationPaths.Type.None;
					type |= (this.bubbles ? PropagationPaths.Type.BubbleUp : PropagationPaths.Type.None);
					this.m_Path = PropagationPaths.Build(this.leafTarget as VisualElement, type);
					EventDebugger.LogPropagationPaths(this, this.m_Path);
				}
				return this.m_Path;
			}
			set
			{
				bool flag = value != null;
				if (flag)
				{
					this.m_Path = PropagationPaths.Copy(value);
				}
			}
		}

		private EventBase.LifeCycleStatus lifeCycleStatus
		{
			get;
			set;
		}

		public bool bubbles
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.Bubbles) > EventBase.EventPropagation.None;
			}
			protected set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.Bubbles;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.Bubbles;
				}
			}
		}

		public bool tricklesDown
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.TricklesDown) > EventBase.EventPropagation.None;
			}
			protected set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.TricklesDown;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.TricklesDown;
				}
			}
		}

		internal IEventHandler leafTarget
		{
			get;
			private set;
		}

		public IEventHandler target
		{
			get
			{
				return this.m_Target;
			}
			set
			{
				this.m_Target = value;
				bool flag = this.leafTarget == null;
				if (flag)
				{
					this.leafTarget = value;
				}
			}
		}

		internal List<IEventHandler> skipElements
		{
			[CompilerGenerated]
			get
			{
				return this.<skipElements>k__BackingField;
			}
		}

		public bool isPropagationStopped
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.PropagationStopped) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.PropagationStopped;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.PropagationStopped;
				}
			}
		}

		public bool isImmediatePropagationStopped
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.ImmediatePropagationStopped) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.ImmediatePropagationStopped;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.ImmediatePropagationStopped;
				}
			}
		}

		public bool isDefaultPrevented
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.DefaultPrevented) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.DefaultPrevented;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.DefaultPrevented;
				}
			}
		}

		public PropagationPhase propagationPhase
		{
			get;
			internal set;
		}

		public virtual IEventHandler currentTarget
		{
			get
			{
				return this.m_CurrentTarget;
			}
			internal set
			{
				this.m_CurrentTarget = value;
				bool flag = this.imguiEvent != null;
				if (flag)
				{
					VisualElement visualElement = this.currentTarget as VisualElement;
					bool flag2 = visualElement != null;
					if (flag2)
					{
						this.imguiEvent.mousePosition = visualElement.WorldToLocal(this.originalMousePosition);
					}
					else
					{
						this.imguiEvent.mousePosition = this.originalMousePosition;
					}
				}
			}
		}

		public bool dispatch
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Dispatching) > EventBase.LifeCycleStatus.None;
			}
			internal set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Dispatching;
					this.dispatched = true;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Dispatching;
				}
			}
		}

		private bool dispatched
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Dispatched) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Dispatched;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Dispatched;
				}
			}
		}

		internal bool processed
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Processed) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Processed;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Processed;
				}
			}
		}

		internal bool processedByFocusController
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.ProcessedByFocusController) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.ProcessedByFocusController;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.ProcessedByFocusController;
				}
			}
		}

		internal bool stopDispatch
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.StopDispatch) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.StopDispatch;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.StopDispatch;
				}
			}
		}

		internal bool propagateToIMGUI
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.PropagateToIMGUI) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.PropagateToIMGUI;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.PropagateToIMGUI;
				}
			}
		}

		private bool imguiEventIsValid
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.IMGUIEventIsValid) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.IMGUIEventIsValid;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.IMGUIEventIsValid;
				}
			}
		}

		public Event imguiEvent
		{
			get
			{
				return this.imguiEventIsValid ? this.m_ImguiEvent : null;
			}
			protected set
			{
				bool flag = this.m_ImguiEvent == null;
				if (flag)
				{
					this.m_ImguiEvent = new Event();
				}
				bool flag2 = value != null;
				if (flag2)
				{
					this.m_ImguiEvent.CopyFrom(value);
					this.imguiEventIsValid = true;
					this.originalMousePosition = value.mousePosition;
				}
				else
				{
					this.imguiEventIsValid = false;
				}
			}
		}

		public Vector2 originalMousePosition
		{
			get;
			private set;
		}

		internal EventDebugger eventLogger
		{
			get;
			set;
		}

		internal bool log
		{
			get
			{
				return this.eventLogger != null;
			}
		}

		protected bool pooled
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Pooled) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Pooled;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Pooled;
				}
			}
		}

		protected static long RegisterEventType()
		{
			return EventBase.s_LastTypeId += 1L;
		}

		internal void SetTriggerEventId(ulong id)
		{
			this.triggerEventId = id;
		}

		[Obsolete("Override PreDispatch(IPanel panel) instead.")]
		protected virtual void PreDispatch()
		{
		}

		protected internal virtual void PreDispatch(IPanel panel)
		{
			this.PreDispatch();
		}

		[Obsolete("Override PostDispatch(IPanel panel) instead.")]
		protected virtual void PostDispatch()
		{
		}

		protected internal virtual void PostDispatch(IPanel panel)
		{
			this.PostDispatch();
			this.processed = true;
		}

		internal bool Skip(IEventHandler h)
		{
			return this.skipElements.Contains(h);
		}

		public void StopPropagation()
		{
			this.isPropagationStopped = true;
		}

		public void StopImmediatePropagation()
		{
			this.isPropagationStopped = true;
			this.isImmediatePropagationStopped = true;
		}

		public void PreventDefault()
		{
			bool flag = (this.propagation & EventBase.EventPropagation.Cancellable) == EventBase.EventPropagation.Cancellable;
			if (flag)
			{
				this.isDefaultPrevented = true;
			}
		}

		internal void MarkReceivedByDispatcher()
		{
			Debug.Assert(!this.dispatched, "Events cannot be dispatched more than once.");
			this.dispatched = true;
		}

		protected virtual void Init()
		{
			this.LocalInit();
		}

		private void LocalInit()
		{
			this.timestamp = Panel.TimeSinceStartupMs();
			this.triggerEventId = 0uL;
			ulong expr_1C = EventBase.s_NextEventId;
			EventBase.s_NextEventId = expr_1C + 1uL;
			this.eventId = expr_1C;
			this.propagation = EventBase.EventPropagation.None;
			PropagationPaths expr_39 = this.m_Path;
			if (expr_39 != null)
			{
				expr_39.Release();
			}
			this.m_Path = null;
			this.leafTarget = null;
			this.target = null;
			this.skipElements.Clear();
			this.isPropagationStopped = false;
			this.isImmediatePropagationStopped = false;
			this.isDefaultPrevented = false;
			this.propagationPhase = PropagationPhase.None;
			this.originalMousePosition = Vector2.zero;
			this.m_CurrentTarget = null;
			this.dispatch = false;
			this.stopDispatch = false;
			this.propagateToIMGUI = true;
			this.dispatched = false;
			this.processed = false;
			this.processedByFocusController = false;
			this.imguiEventIsValid = false;
			this.pooled = false;
			this.eventLogger = null;
		}

		protected EventBase()
		{
			this.<skipElements>k__BackingField = new List<IEventHandler>();
			base..ctor();
			this.m_ImguiEvent = null;
			this.LocalInit();
		}

		internal abstract void Acquire();

		public abstract void Dispose();
	}
	public abstract class EventBase<T> : EventBase where T : EventBase<T>, new()
	{
		private static readonly long s_TypeId = EventBase.RegisterEventType();

		private static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(100);

		private int m_RefCount;

		public override long eventTypeId
		{
			get
			{
				return EventBase<T>.s_TypeId;
			}
		}

		protected EventBase()
		{
			this.m_RefCount = 0;
		}

		public static long TypeId()
		{
			return EventBase<T>.s_TypeId;
		}

		protected override void Init()
		{
			base.Init();
			bool flag = this.m_RefCount != 0;
			if (flag)
			{
				Debug.Log("Event improperly released.");
				this.m_RefCount = 0;
			}
		}

		public static T GetPooled()
		{
			T t = EventBase<T>.s_Pool.Get();
			t.Init();
			t.pooled = true;
			t.Acquire();
			return t;
		}

		internal static T GetPooled(EventBase e)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = e != null;
			if (flag)
			{
				pooled.SetTriggerEventId(e.eventId);
			}
			return pooled;
		}

		private static void ReleasePooled(T evt)
		{
			bool pooled = evt.pooled;
			if (pooled)
			{
				evt.Init();
				EventBase<T>.s_Pool.Release(evt);
				evt.pooled = false;
			}
		}

		internal override void Acquire()
		{
			this.m_RefCount++;
		}

		public sealed override void Dispose()
		{
			int num = this.m_RefCount - 1;
			this.m_RefCount = num;
			bool flag = num == 0;
			if (flag)
			{
				EventBase<T>.ReleasePooled((T)((object)this));
			}
		}
	}
}
