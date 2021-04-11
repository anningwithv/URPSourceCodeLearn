using System;

namespace UnityEngine.UIElements
{
	internal class EventCallbackRegistry
	{
		private static readonly EventCallbackListPool s_ListPool = new EventCallbackListPool();

		private EventCallbackList m_Callbacks;

		private EventCallbackList m_TemporaryCallbacks;

		private int m_IsInvoking;

		private static EventCallbackList GetCallbackList(EventCallbackList initializer = null)
		{
			return EventCallbackRegistry.s_ListPool.Get(initializer);
		}

		private static void ReleaseCallbackList(EventCallbackList toRelease)
		{
			EventCallbackRegistry.s_ListPool.Release(toRelease);
		}

		public EventCallbackRegistry()
		{
			this.m_IsInvoking = 0;
		}

		private EventCallbackList GetCallbackListForWriting()
		{
			bool flag = this.m_IsInvoking > 0;
			EventCallbackList result;
			if (flag)
			{
				bool flag2 = this.m_TemporaryCallbacks == null;
				if (flag2)
				{
					bool flag3 = this.m_Callbacks != null;
					if (flag3)
					{
						this.m_TemporaryCallbacks = EventCallbackRegistry.GetCallbackList(this.m_Callbacks);
					}
					else
					{
						this.m_TemporaryCallbacks = EventCallbackRegistry.GetCallbackList(null);
					}
				}
				result = this.m_TemporaryCallbacks;
			}
			else
			{
				bool flag4 = this.m_Callbacks == null;
				if (flag4)
				{
					this.m_Callbacks = EventCallbackRegistry.GetCallbackList(null);
				}
				result = this.m_Callbacks;
			}
			return result;
		}

		private EventCallbackList GetCallbackListForReading()
		{
			bool flag = this.m_TemporaryCallbacks != null;
			EventCallbackList result;
			if (flag)
			{
				result = this.m_TemporaryCallbacks;
			}
			else
			{
				result = this.m_Callbacks;
			}
			return result;
		}

		private bool ShouldRegisterCallback(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			bool flag = callback == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				EventCallbackList callbackListForReading = this.GetCallbackListForReading();
				bool flag2 = callbackListForReading != null;
				result = (!flag2 || !callbackListForReading.Contains(eventTypeId, callback, phase));
			}
			return result;
		}

		private bool UnregisterCallback(long eventTypeId, Delegate callback, TrickleDown useTrickleDown)
		{
			bool flag = callback == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				EventCallbackList callbackListForWriting = this.GetCallbackListForWriting();
				CallbackPhase phase = (useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp;
				result = callbackListForWriting.Remove(eventTypeId, callback, phase);
			}
			return result;
		}

		public void RegisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			long eventTypeId = EventBase<TEventType>.TypeId();
			CallbackPhase phase = (useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp;
			EventCallbackList eventCallbackList = this.GetCallbackListForReading();
			bool flag2 = eventCallbackList == null || !eventCallbackList.Contains(eventTypeId, callback, phase);
			if (flag2)
			{
				eventCallbackList = this.GetCallbackListForWriting();
				eventCallbackList.Add(new EventCallbackFunctor<TEventType>(callback, phase));
			}
		}

		public void RegisterCallback<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TCallbackArgs userArgs, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			long eventTypeId = EventBase<TEventType>.TypeId();
			CallbackPhase phase = (useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp;
			EventCallbackList eventCallbackList = this.GetCallbackListForReading();
			bool flag2 = eventCallbackList != null;
			if (flag2)
			{
				EventCallbackFunctor<TEventType, TCallbackArgs> eventCallbackFunctor = eventCallbackList.Find(eventTypeId, callback, phase) as EventCallbackFunctor<TEventType, TCallbackArgs>;
				bool flag3 = eventCallbackFunctor != null;
				if (flag3)
				{
					eventCallbackFunctor.userArgs = userArgs;
					return;
				}
			}
			eventCallbackList = this.GetCallbackListForWriting();
			eventCallbackList.Add(new EventCallbackFunctor<TEventType, TCallbackArgs>(callback, userArgs, phase));
		}

		public bool UnregisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			long eventTypeId = EventBase<TEventType>.TypeId();
			return this.UnregisterCallback(eventTypeId, callback, useTrickleDown);
		}

		public bool UnregisterCallback<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			long eventTypeId = EventBase<TEventType>.TypeId();
			return this.UnregisterCallback(eventTypeId, callback, useTrickleDown);
		}

		internal bool TryGetUserArgs<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TrickleDown useTrickleDown, out TCallbackArgs userArgs) where TEventType : EventBase<TEventType>, new()
		{
			userArgs = default(TCallbackArgs);
			bool flag = callback == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				EventCallbackList callbackListForReading = this.GetCallbackListForReading();
				long eventTypeId = EventBase<TEventType>.TypeId();
				CallbackPhase phase = (useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp;
				EventCallbackFunctor<TEventType, TCallbackArgs> eventCallbackFunctor = callbackListForReading.Find(eventTypeId, callback, phase) as EventCallbackFunctor<TEventType, TCallbackArgs>;
				bool flag2 = eventCallbackFunctor == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					userArgs = eventCallbackFunctor.userArgs;
					result = true;
				}
			}
			return result;
		}

		public void InvokeCallbacks(EventBase evt)
		{
			bool flag = this.m_Callbacks == null;
			if (!flag)
			{
				this.m_IsInvoking++;
				for (int i = 0; i < this.m_Callbacks.Count; i++)
				{
					bool isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
					if (isImmediatePropagationStopped)
					{
						break;
					}
					this.m_Callbacks[i].Invoke(evt);
				}
				this.m_IsInvoking--;
				bool flag2 = this.m_IsInvoking == 0;
				if (flag2)
				{
					bool flag3 = this.m_TemporaryCallbacks != null;
					if (flag3)
					{
						EventCallbackRegistry.ReleaseCallbackList(this.m_Callbacks);
						this.m_Callbacks = EventCallbackRegistry.GetCallbackList(this.m_TemporaryCallbacks);
						EventCallbackRegistry.ReleaseCallbackList(this.m_TemporaryCallbacks);
						this.m_TemporaryCallbacks = null;
					}
				}
			}
		}

		public bool HasTrickleDownHandlers()
		{
			return this.m_Callbacks != null && this.m_Callbacks.trickleDownCallbackCount > 0;
		}

		public bool HasBubbleHandlers()
		{
			return this.m_Callbacks != null && this.m_Callbacks.bubbleUpCallbackCount > 0;
		}
	}
}
