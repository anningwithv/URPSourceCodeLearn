using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public sealed class EventDispatcher
	{
		private struct EventRecord
		{
			public EventBase m_Event;

			public IPanel m_Panel;
		}

		private struct DispatchContext
		{
			public uint m_GateCount;

			public Queue<EventDispatcher.EventRecord> m_Queue;
		}

		private ClickDetector m_ClickDetector = new ClickDetector();

		private List<IEventDispatchingStrategy> m_DispatchingStrategies;

		private static readonly ObjectPool<Queue<EventDispatcher.EventRecord>> k_EventQueuePool = new ObjectPool<Queue<EventDispatcher.EventRecord>>(100);

		private Queue<EventDispatcher.EventRecord> m_Queue;

		private uint m_GateCount;

		private Stack<EventDispatcher.DispatchContext> m_DispatchContexts;

		private static EventDispatcher s_EditorEventDispatcher;

		private DebuggerEventDispatchingStrategy m_DebuggerEventDispatchingStrategy;

		private static readonly IEventDispatchingStrategy[] s_EditorStrategies = new IEventDispatchingStrategy[]
		{
			new PointerCaptureDispatchingStrategy(),
			new MouseCaptureDispatchingStrategy(),
			new KeyboardEventDispatchingStrategy(),
			new PointerEventDispatchingStrategy(),
			new MouseEventDispatchingStrategy(),
			new CommandEventDispatchingStrategy(),
			new IMGUIEventDispatchingStrategy(),
			new DefaultDispatchingStrategy()
		};

		private bool m_Immediate;

		internal PointerDispatchState pointerState
		{
			[CompilerGenerated]
			get
			{
				return this.<pointerState>k__BackingField;
			}
		}

		internal static EventDispatcher editorDispatcher
		{
			get
			{
				bool flag = EventDispatcher.s_EditorEventDispatcher == null;
				if (flag)
				{
					EventDispatcher.s_EditorEventDispatcher = EventDispatcher.CreateDefault();
				}
				return EventDispatcher.s_EditorEventDispatcher;
			}
		}

		private bool dispatchImmediately
		{
			get
			{
				return this.m_Immediate || this.m_GateCount == 0u;
			}
		}

		internal static void ClearEditorDispatcher()
		{
			EventDispatcher.s_EditorEventDispatcher = null;
		}

		internal static EventDispatcher CreateDefault()
		{
			return new EventDispatcher(EventDispatcher.s_EditorStrategies);
		}

		internal static EventDispatcher CreateForRuntime(IList<IEventDispatchingStrategy> strategies)
		{
			return new EventDispatcher(strategies);
		}

		[Obsolete("Please use EventDispatcher.CreateDefault().")]
		internal EventDispatcher() : this(EventDispatcher.s_EditorStrategies)
		{
		}

		private EventDispatcher(IList<IEventDispatchingStrategy> strategies)
		{
			this.<pointerState>k__BackingField = new PointerDispatchState();
			this.m_DispatchContexts = new Stack<EventDispatcher.DispatchContext>();
			this.m_Immediate = false;
			base..ctor();
			this.m_DispatchingStrategies = new List<IEventDispatchingStrategy>();
			this.m_DebuggerEventDispatchingStrategy = new DebuggerEventDispatchingStrategy();
			this.m_DispatchingStrategies.Add(this.m_DebuggerEventDispatchingStrategy);
			this.m_DispatchingStrategies.AddRange(strategies);
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
		}

		internal void Dispatch(EventBase evt, IPanel panel, DispatchMode dispatchMode)
		{
			evt.MarkReceivedByDispatcher();
			bool flag = evt.eventTypeId == EventBase<IMGUIEvent>.TypeId();
			if (flag)
			{
				Event imguiEvent = evt.imguiEvent;
				bool flag2 = imguiEvent.rawType == EventType.Repaint;
				if (flag2)
				{
					return;
				}
			}
			bool flag3 = this.dispatchImmediately || dispatchMode == DispatchMode.Immediate;
			if (flag3)
			{
				this.ProcessEvent(evt, panel);
			}
			else
			{
				evt.Acquire();
				this.m_Queue.Enqueue(new EventDispatcher.EventRecord
				{
					m_Event = evt,
					m_Panel = panel
				});
			}
		}

		internal void PushDispatcherContext()
		{
			this.ProcessEventQueue();
			this.m_DispatchContexts.Push(new EventDispatcher.DispatchContext
			{
				m_GateCount = this.m_GateCount,
				m_Queue = this.m_Queue
			});
			this.m_GateCount = 0u;
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
		}

		internal void PopDispatcherContext()
		{
			Debug.Assert(this.m_GateCount == 0u, "All gates should have been opened before popping dispatch context.");
			Debug.Assert(this.m_Queue.Count == 0, "Queue should be empty when popping dispatch context.");
			EventDispatcher.k_EventQueuePool.Release(this.m_Queue);
			this.m_GateCount = this.m_DispatchContexts.Peek().m_GateCount;
			this.m_Queue = this.m_DispatchContexts.Peek().m_Queue;
			this.m_DispatchContexts.Pop();
		}

		internal void CloseGate()
		{
			this.m_GateCount += 1u;
		}

		internal void OpenGate()
		{
			Debug.Assert(this.m_GateCount > 0u);
			bool flag = this.m_GateCount > 0u;
			if (flag)
			{
				this.m_GateCount -= 1u;
			}
			bool flag2 = this.m_GateCount == 0u;
			if (flag2)
			{
				this.ProcessEventQueue();
			}
		}

		private void ProcessEventQueue()
		{
			Queue<EventDispatcher.EventRecord> queue = this.m_Queue;
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
			ExitGUIException ex = null;
			try
			{
				while (queue.Count > 0)
				{
					EventDispatcher.EventRecord eventRecord = queue.Dequeue();
					EventBase @event = eventRecord.m_Event;
					IPanel panel = eventRecord.m_Panel;
					try
					{
						this.ProcessEvent(@event, panel);
					}
					catch (ExitGUIException ex2)
					{
						Debug.Assert(ex == null);
						ex = ex2;
					}
					finally
					{
						@event.Dispose();
					}
				}
			}
			finally
			{
				EventDispatcher.k_EventQueuePool.Release(queue);
			}
			bool flag = ex != null;
			if (flag)
			{
				throw ex;
			}
		}

		private void ProcessEvent(EventBase evt, IPanel panel)
		{
			Event imguiEvent = evt.imguiEvent;
			bool flag = imguiEvent != null && imguiEvent.rawType == EventType.Used;
			using (new EventDispatcherGate(this))
			{
				evt.PreDispatch(panel);
				bool flag2 = !evt.stopDispatch && !evt.isPropagationStopped;
				if (flag2)
				{
					this.ApplyDispatchingStrategies(evt, panel, flag);
				}
				bool flag3 = evt.path != null;
				if (flag3)
				{
					foreach (VisualElement current in evt.path.targetElements)
					{
						evt.target = current;
						EventDispatchUtilities.ExecuteDefaultAction(evt, panel);
					}
					evt.target = evt.leafTarget;
				}
				else
				{
					EventDispatchUtilities.ExecuteDefaultAction(evt, panel);
				}
				this.m_DebuggerEventDispatchingStrategy.PostDispatch(evt, panel);
				evt.PostDispatch(panel);
				this.m_ClickDetector.ProcessEvent(evt);
				Debug.Assert(flag || evt.isPropagationStopped || imguiEvent == null || imguiEvent.rawType != EventType.Used, "Event is used but not stopped.");
			}
		}

		private void ApplyDispatchingStrategies(EventBase evt, IPanel panel, bool imguiEventIsInitiallyUsed)
		{
			foreach (IEventDispatchingStrategy current in this.m_DispatchingStrategies)
			{
				bool flag = current.CanDispatchEvent(evt);
				if (flag)
				{
					current.DispatchEvent(evt, panel);
					Debug.Assert(imguiEventIsInitiallyUsed || evt.isPropagationStopped || evt.imguiEvent == null || evt.imguiEvent.rawType != EventType.Used, "Unexpected condition: !evt.isPropagationStopped && evt.imguiEvent.rawType == EventType.Used.");
					bool flag2 = evt.stopDispatch || evt.isPropagationStopped;
					if (flag2)
					{
						break;
					}
				}
			}
		}
	}
}
